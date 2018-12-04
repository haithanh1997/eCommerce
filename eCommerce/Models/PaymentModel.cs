using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eCommerce.Models
{
    public class PaymentRequestModel
    {
        public string mTransactionID { get; set; }
        public string merchantCode { get; set; }
        public string bankCode { get; set; }
        public string totalAmount { get; set; }
        public string clientIP { get; set; }
        public string custGender { get; set; }
        public string cancelURL { get; set; }
        public string redirectURL { get; set; }
        public string errorURL { get; set; }
        public string passcode { get; set; }
        public string checksum { get; set; }
    }

    public class PaymentResponseModel
    {

    }
}