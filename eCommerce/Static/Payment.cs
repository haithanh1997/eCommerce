using eCommerce.Models;
using Newtonsoft.Json;
using System;
using System.Net;

namespace eCommerce.Static
{
    public static class Payment
    {
        public static string PaymentApi(string transactionId, decimal totalAmount = 0)
        {
            try
            {
                //Test PAYMENT

                //DITMELOZTHANHMICODE123PAY1000000127.0.0.1Uhttps://google.comhttps://google.comhttps://google.comMIPASSCODEMIKEY
                var cancelUrl = "http://localhost:58107/Customers/OnlinePayment";               //Cancel payment
                var redirectUrl = "http://localhost:58107/Customers/OnlinePayment";             //Redirect page
                var errorUrl = "http://localhost:58107/Customers/OnlinePayment";                //Error payment
                var payment = new PaymentRequestModel()
                {
                    mTransactionID = transactionId,
                    merchantCode = "MICODE",
                    bankCode = "123PAY",
                    totalAmount = totalAmount.ToString(),
                    clientIP = "127.0.0.1",
                    custGender = "U",
                    cancelURL = cancelUrl,
                    redirectURL = redirectUrl,
                    errorURL = errorUrl,
                    passcode = "MIPASSCODE",
                    checksum = SHA1Convert.Hash(transactionId + "MICODE123PAY" + totalAmount + "127.0.0.1U" + cancelUrl + redirectUrl + errorUrl + "MIPASSCODEMIKEY")
                };

                var body = JsonConvert.SerializeObject(payment);
                var wc = new WebClient();
                wc.Headers.Add("Content-Type: application/json");
                wc.Headers.Add("Accept: application/json");

                var response = wc.UploadString("https://sandbox.123pay.vn/miservice/createOrder1", body);
                return response.Split(',')[2].Replace("\"", "").Replace("/", "");
            }
            catch(Exception ex)
            {
                return null;
            }
        }
    }
}