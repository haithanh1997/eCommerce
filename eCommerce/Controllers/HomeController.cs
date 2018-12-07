using eCommerce.Models;
using eCommerce.Static;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace eCommerce.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {

            //Test SMTP

            //create a object to hold the message
            //MailMessage newMessage = new MailMessage();

            //Create addresses
            //MailAddress senderAddress = new MailAddress("rendoleo317@gmail.com");
            //MailAddress recipentAddress = new MailAddress("nguyenthientam317@gmail.com");

            //Now create the full message
            //newMessage.To.Add(recipentAddress);
            //newMessage.From = senderAddress;
            //newMessage.Subject = "Tieu de";
            //newMessage.Body = "Noi dung";

            //Create the SMTP Client object, which do the actual sending
            //SmtpClient client = new SmtpClient("smtp.gmail.com", 587)
            //{
            //    Credentials = new NetworkCredential("rendoleo317@gmail.com", "tfvzcxzscphvqeki"),
            //    EnableSsl = true
            //};

            //now send the message
            //client.Send(newMessage);

            //Test PAYMENT

            //DITMELOZTHANHMICODE123PAY1000000127.0.0.1Uhttps://google.comhttps://google.comhttps://google.comMIPASSCODEMIKEY

            var transactionId = "DITMELOZTHANH" + (new Random()).Next(100000);
            var totalAmount = "120000";
            var cancelUrl = "https://google.com";               //Cancel payment
            var redirectUrl = "https://google.com";             //Redirect page
            var errorUrl = "https://google.com";                //Error payment
            var payment = new PaymentRequestModel()
            {
                mTransactionID = transactionId,
                merchantCode = "MICODE",
                bankCode = "123PAY",
                totalAmount = totalAmount,
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
            ViewBag.Response = response.Split(',')[2].Replace("\"", "").Replace("/", "");

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}