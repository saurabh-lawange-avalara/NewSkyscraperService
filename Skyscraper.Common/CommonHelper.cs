using Avalara.Skyscraper.Model;
using Avalara.Skyscraper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Avalara.Skyscraper.Common
{
    public class CommonHelper
    {
        internal const string EfileAndEPayConfirmationPdfTag = "EfileAndEPayConfirmationPdf";
        internal const string EfileAndEPayConfirmationPngTag = "EfileAndEPayConfirmationPng";
        internal const string ManagedEfileAndEPayConfirmationPngTag = "ManagedEfileAndEPayConfirmationPng";
        internal const string EFileConfirmationPdfTag = "ConfirmationPdf";
        internal const string EFileConfirmationPngTag = "ConfirmationPng";
        internal const string EFileManagedConfirmationPngTag = "ManagedConfirmationPng";
        internal const string EPayConfirmationPdfTag = "EPayConfirmationPdf";
        internal const string EPayConfirmationPngTag = "EPayConfirmationPng";
        internal const string ManagedEPayConfirmationPngTag = "ManagedEPayConfirmationPng";

        public static bool IsJobComplete(string status)
        {
            switch (status.ToUpper().Trim())
            {
                case "UNPROCESSED":
                case "PROCESSING":
                case "SCRAPING":
                    return false;
                case "SUCCESS":
                case "FAILED":
                case "CANCELLED":
                case "AWAITINGCONFIRMATION":
                    return true;
                default:
                    return false;
            }
        }

        public static List<SkyScraperResourceModel> FindConfirmations(List<SkyScraperResourceModel> imageList)
        {
            //If there is a combined confirmation present then just return that
            var confirmation = GetCombinedConfirmation(imageList);
            if (confirmation != null && confirmation.Count > 0)
            {
                return confirmation;
            }

            //Else find Efile and/or Epay and return list.
            var listConfirmations = GetEFileConfirmation(imageList);
            listConfirmations.AddRange(GetEpayConfirmation(imageList));

            //remove nulls
            if (listConfirmations != null)
            {
                listConfirmations.RemoveAll(e => e == null);
            }

            return listConfirmations;
        }


        public static List<SkyScraperResourceModel> GetCombinedConfirmation(List<SkyScraperResourceModel> imageList)
        {
            var temp = imageList.Where(e => !string.IsNullOrEmpty(e.Tags) &&
            e.Tags.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
            .Any(f => f.Trim().Equals(EfileAndEPayConfirmationPdfTag, StringComparison.OrdinalIgnoreCase))).ToList();
            if (temp.Count > 0)
            {
                return temp;
            }
            temp = imageList.Where(e => !string.IsNullOrEmpty(e.Tags) &&
            e.Tags.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
            .Any(f => f.Trim().Equals(EfileAndEPayConfirmationPngTag, StringComparison.OrdinalIgnoreCase))).ToList();
            if (temp.Count > 0)
            {
                return temp;
            }
            temp = imageList.Where(e => !string.IsNullOrEmpty(e.Tags) &&
            e.Tags.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
            .Any(f => f.Trim().Equals(ManagedEfileAndEPayConfirmationPngTag, StringComparison.OrdinalIgnoreCase))).ToList();
            if (temp.Count > 0)
            {
                return temp;
            }
            return temp;
        }

        public static List<SkyScraperResourceModel> GetEFileConfirmation(List<SkyScraperResourceModel> imageList)
        {
            var temp = imageList.Where(e => !string.IsNullOrEmpty(e.Tags) && 
            e.Tags.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
            .Any(f => f.Trim().Equals(EFileConfirmationPdfTag, StringComparison.OrdinalIgnoreCase))).ToList();
            if (temp.Count > 0)
            {
                return temp;
            }
            temp = imageList.Where(e => !string.IsNullOrEmpty(e.Tags) && 
            e.Tags.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
            .Any(f => f.Trim().Equals(EFileConfirmationPngTag, StringComparison.OrdinalIgnoreCase))).ToList();
            if (temp.Count > 0)
            {
                return temp;
            }
            temp = imageList.Where(e => !string.IsNullOrEmpty(e.Tags) && 
            e.Tags.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
            .Any(f => f.Trim().Equals(EFileManagedConfirmationPngTag, StringComparison.OrdinalIgnoreCase))).ToList();
            if (temp.Count > 0)
            {
                return temp;
            }
            return temp;
        }

        public static List<SkyScraperResourceModel> GetEpayConfirmation(List<SkyScraperResourceModel> imageList)
        {
            var temp = imageList.Where(e => !string.IsNullOrEmpty(e.Tags) && 
            e.Tags.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
            .Any(f => f.Trim().Equals(EPayConfirmationPdfTag, StringComparison.OrdinalIgnoreCase))).ToList();
            if (temp.Count > 0)
            {
                return temp;
            }
            temp = imageList.Where(e => !string.IsNullOrEmpty(e.Tags) && 
            e.Tags.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
            .Any(f => f.Trim().Equals(EPayConfirmationPngTag, StringComparison.OrdinalIgnoreCase))).ToList();
            if (temp.Count > 0)
            {
                return temp;
            }
            temp = imageList.Where(e => !string.IsNullOrEmpty(e.Tags) &&
            e.Tags.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
            .Any(f => f.Trim().Equals(ManagedEPayConfirmationPngTag, StringComparison.OrdinalIgnoreCase))).ToList();
            if (temp.Count > 0)
            {
                return temp;
            }
            return temp;
        }

        public static FilingFrequency GetFilingFrequencyFromCode(string frequencyCode)
        {
            if (string.IsNullOrEmpty(frequencyCode))
            {
                return 0;
            }

            switch (frequencyCode)
            {
                case "M": return FilingFrequency.Monthly;
                case "BM": return FilingFrequency.Bimonthly;
                case "Q": return FilingFrequency.Quarterly;
                case "SA": return FilingFrequency.SemiAnnually;
                case "A": return FilingFrequency.Annually;
                case "O": return FilingFrequency.Occasional;
                case "IQ": return FilingFrequency.InverseQuarterly;
                default: return 0;
            }
        }


        public static bool CheckIfTestJob(Dictionary<string, long> testjobs, string username, string password, string mode, JobType jobTypeId, out long testJobId, out string msg)
        {
            bool isTestJobRequest = false;
            testJobId = 0;
            msg = string.Empty;
            string[] arrPassword = { "success", "failure", "exception" };
            if (!string.IsNullOrEmpty(username) && username.Equals("test", StringComparison.OrdinalIgnoreCase) && !string.IsNullOrEmpty(password) && arrPassword.Any(e => e.Equals(password, StringComparison.OrdinalIgnoreCase)))
            {
                isTestJobRequest = true;
                if (password.Equals("exception", StringComparison.OrdinalIgnoreCase))
                {
                    msg = "Exception occured.Scraper is not available.";
                }
                else
                {
                    string configKey = string.Empty;
                    if (mode == null)
                        configKey = $"{jobTypeId.ToString()}_{password.ToUpper()}";
                    else
                        configKey = $"{mode.ToUpper()}_{password.ToUpper()}";

                    try
                    {
                        if (testjobs.TryGetValue(configKey, out testJobId) && testJobId > 0)
                        {
                            // Do Nothing
                        }
                        else
                        {
                            msg = "Invalid test Job request.";
                        }
                    }
                    catch
                    {

                    }
                }
            }
            return isTestJobRequest;
        }

        public static bool IsWebFileModeAllowed(string mode, AllowedModesOnTest allowedModes)
        {
            if (allowedModes.IsFileModeDisabledInTest)
            {
                string[] supportedModes = allowedModes.SupportedModesInTest.ToLower().Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                if (!supportedModes.Contains(mode, StringComparer.OrdinalIgnoreCase))
                {
                    return false;
                }
                //if (!mode.Equals(WebFileMode.REVIEW.ToString(), StringComparison.OrdinalIgnoreCase)||!mode.Equals(WebFileMode.CONFIRMATION.ToString(),StringComparison.OrdinalIgnoreCase))
                //{
                //    return false;
                //}
            }
            return true;
        }

    }
}
