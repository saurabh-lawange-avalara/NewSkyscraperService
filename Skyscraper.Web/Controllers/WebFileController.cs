using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using Avalara.Skyscraper.Common;
using Avalara.Skyscraper.Common.Extensions;
using Avalara.Skyscraper.Models;
using Avalara.Skyscraper.Services;
using Avalara.Skyscraper.Web.Common;
using log4net;
using Microsoft.AspNetCore.Mvc;

namespace Avalara.Skyscraper.Web.Controllers
{
    [Route("api/[controller]")]
    [AuthorizeComplianceUser]
    public class WebFileController : BaseController
    {
        private ISkyscraperService _service;
        public WebFileController(ISkyscraperService service, ILog logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            try
            {
                _logger.Info("In Get WebFile Job response by jobId");
                if (id <= 0)
                {
                    _logger.Error("Invalid Job Id");
                    return new JsonResult("Invalid Job Id") { StatusCode = (int)HttpStatusCode.BadRequest };
                }
                HttpContext.GetAvaUser();
                var response = _service.GetWebFile(id);

                if (response == null)
                {
                    return GetApiResponse(HttpStatusCode.NotFound, string.Format("WebFile job {0} not found", id));
                }

                //If job is yet to complete then send 202 with current status details.
                if (!CommonHelper.IsJobComplete(response.Status))
                {
                    dynamic returnObj = new ExpandoObject();
                    returnObj.JobId = response.JobId;
                    returnObj.Status = response.Status.ToString();
                    returnObj.Message = string.Format("WebFile job {0} still processing", response.JobId);
                    returnObj.Error = response.Error;
                    return GetApiResponse(HttpStatusCode.Accepted, returnObj);
                }
                if (response.Status.Equals(JobStatus.FAILED.ToString(), StringComparison.OrdinalIgnoreCase))
                {
                    return GetApiResponse(HttpStatusCode.BadRequest, response);
                }
                return GetApiResponse(HttpStatusCode.OK, response);
            }
            catch (Exception ex)
            {
                return GetApiResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }     

        [HttpPost]
        public IActionResult Post(WebFileModel request)
        {
            List<string> StatesWithSeparatePaymentSites = new List<string> { "TX", "VA" };
            try
            {
                _logger.Info("In Web Filing for " + request.Username);

                #region sanity checks
                if (!string.IsNullOrEmpty(request.S3CFDRefKey))
                {
                    request.CFD = _service.GetCFDFromS3(request.S3CFDRefKey);
                    if (request.CFD == null)
                    {
                        return GetApiResponse(HttpStatusCode.BadRequest, string.Format("Make sure you have posted valid s3 cfd reference key."));
                    }
                }
                if (request.CFD == null || request.CFD.returnsData == null || request.CFD.Header == null
                    || request.CFD.summary == null)
                {
                    return GetApiResponse(HttpStatusCode.BadRequest, "Must provide CFD element with returnsData, Header and Summary");
                }
                //check if taxform code is present in request cfd 
                if (string.IsNullOrEmpty(request.CFD.Header.TaxFormCode))
                {
                    return GetApiResponse(HttpStatusCode.BadRequest, "Must provide TaxFormCode in CFd element Header.");
                }

                //check for valid mode
                var formMetaData = _service.GetFormStatus(request.CFD.Header.TaxFormCode, 2);

                if (formMetaData == null || formMetaData.Count < 1)
                {
                    return GetApiResponse(HttpStatusCode.BadRequest, string.Format("Form Metadata not found for {0}.", request.CFD.Header.TaxFormCode));
                }

                try
                {
                    if (request.Payment == null || string.IsNullOrEmpty(request.Payment.PaymentMethod.ToString()))
                    {
                        return GetApiResponse(HttpStatusCode.BadRequest, "Request must have a valid payment method mentioned(ACHDebit or ACHCredit).");
                    }
                }
                catch
                {
                    throw new Exception("PaymentMethod needs to be specified.It can not be empty.Please provide valid payment method.For more info contact skyscraper support.");
                }

                if (formMetaData.Count > 1 && (string.IsNullOrEmpty(request.SkyScraperRegion) || StatesWithSeparatePaymentSites.Any(x => request.SkyScraperRegion.StartsWith(x))))
                {
                    //ach debit customers will have to specify payment method.Based on which service assigns scraper region

                    if (request.Payment != null) // && request.Payment.ToString() != "MRS"
                    {
                        try
                        {
                            //if payment method is mentioned pick scraper based on it
                            if (!string.IsNullOrEmpty(request.Payment.PaymentMethod))
                            {
                                formMetaData = formMetaData.Where(e => e.PaymentModes.Any(k => k.Name.Equals(request.Payment.PaymentMethod.ToString()))).ToList();
                                if (formMetaData == null || formMetaData.Count < 1)
                                {
                                    //if no scraper found payment method not supported
                                    string requestPaymentMethod = request.Payment.PaymentMethod.ToString();
                                    return GetApiResponse(HttpStatusCode.BadRequest, string.Format("Requested payment method=> {0} not supported.", requestPaymentMethod));
                                }
                            }
                            else
                            {
                                return GetApiResponse(HttpStatusCode.BadRequest, "PaymentMethod needs to be specified.It can not be empty.Please provide valid payment method.For more info contact skyscraper support.");
                            }
                        }
                        catch
                        {
                            throw new Exception("PaymentMethod needs to be specified.It can not be empty.Please provide valid payment method.For more info contact skyscraper support.");
                        }
                    }
                    else
                    {
                        formMetaData = formMetaData.Where(e => e.IsDefaultTaxForm == true).ToList();

                        if (formMetaData == null || formMetaData.Count < 1)
                        {
                            return GetApiResponse(HttpStatusCode.BadRequest, string.Format("Default TaxForm not found for {0}.", request.CFD.Header.TaxFormCode));
                        }
                    }
                }
                else if (!string.IsNullOrEmpty(request.SkyScraperRegion))
                {
                    formMetaData = formMetaData.Where(e => e.ScraperRegion.ToUpper() == request.SkyScraperRegion.ToUpper()).ToList();
                    if (formMetaData == null || formMetaData.Count < 1)
                    {
                        return GetApiResponse(HttpStatusCode.BadRequest, string.Format("Skyscraper region = {0} not found for TaxForm {1}.", request.SkyScraperRegion, request.CFD.Header.TaxFormCode));
                    }
                }

                if (formMetaData[0].IsAvailable != true)
                {
                    return GetApiResponse(HttpStatusCode.BadRequest, string.Format("Scraper not availabe for the form = {0}.", request.CFD.Header.TaxFormCode));
                }

                if (formMetaData[0].FilingModes == null || !(formMetaData[0].FilingModes.Any(e => e.Name.Equals(request.Mode, StringComparison.InvariantCultureIgnoreCase))))
                {
                    _logger.Error("Requested Mode not supported.");
                    return GetApiResponse(HttpStatusCode.BadRequest, "Requested Mode not supported.");
                }

                //check for webfile mode 
                if ((!request.Mode.Equals("fileonly", StringComparison.OrdinalIgnoreCase)) && (!request.Mode.Equals("confirmation", StringComparison.OrdinalIgnoreCase)))
                {
                    if (formMetaData[0].PaymentModes == null || !formMetaData[0].PaymentModes.Any(e => e.Name.Equals(request.Payment.PaymentMethod.ToString(), StringComparison.InvariantCultureIgnoreCase)))
                    {
                        _logger.Error("Requested Payment Method not supported for the form.");
                        return GetApiResponse(HttpStatusCode.BadRequest, "Requested Payment Method not supported for the form.");
                    }

                    //validate required keys for ACH Debit
                    if (request.Payment.PaymentMethod.ToString().Equals("ACHDebit", StringComparison.OrdinalIgnoreCase))
                    {
                        string validationMsg = string.Empty;
                        if (!_service.ValidatePaymentInfoKeys(request.Payment, formMetaData[0].ScraperRegion, out validationMsg))
                        {
                            _logger.Error(validationMsg);
                            return GetApiResponse(HttpStatusCode.BadRequest, validationMsg);
                        }
                    }
                }

                #endregion

                var user = HttpContext.GetAvaUser();
                var jobCreateResponse = _service.SetAndCreateWebFileJob(request, user, formMetaData);
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                _logger.Error(ex.Message);
                return GetApiResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

    }
}