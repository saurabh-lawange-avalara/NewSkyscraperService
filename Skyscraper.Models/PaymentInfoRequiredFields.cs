using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Avalara.Skyscraper.Models
{
    public class PaymentInfoRequiredFields 
    {
        /// <summary>
        /// payment method (ACHDebit/ACHCredit)
        /// </summary>
        [Required]
        public string PaymentMethod { get; set; }

        /// <summary>
        /// Payment due date on DOR.
        /// </summary>
        [Required]
        public DateTime? PaymentDate { get; set; }
        /// <summary>
        /// Name of the Bank.Required if ACHDebit
        /// </summary>
        public String BankName { get; set; }

        /// <summary>
        /// Bank Account number.Required if ACHDebit
        /// </summary>
        public String BankAccountNum { get; set; }

        /// <summary>
        /// Bank Routing number.Required if ACHDebit
        /// </summary>
        public String BankRoutingNum { get; set; }

        /// <summary>
        /// Pipe seperated values of supported Account type e.g Checking | Personal Checking. Need to send any value from pipe  if ACHDebit
        /// </summary>
        public String AccountType { get; set; }

        /// <summary>
        /// Other required fields to process payment.Ordered Dictionary
        /// </summary>
        public OrderedDictionary AdditionalFields { get; set; }
    }
}
