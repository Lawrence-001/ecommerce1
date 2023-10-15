using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using RestSharp;
using Method = RestSharp.Method;
using e_commerce.Models;
using e_commerce.Models.Mpesa;

namespace e_commerce.Controllers
{
    public class MpesaController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            var amount = HttpContext.Session.GetString("amount");
            Payment payment = new Payment();
            payment.Amount = Convert.ToDecimal(amount);
            //payment.Amount = decimal.Parse(amount);
            return View(payment);
        }
        [HttpPost]
        public IActionResult Index(Payment payment)
        {
            var shortcode = "174379";
            var passkey = "bfb279f9aa9bdbcf158e97dd71a467cd2e0c893059b10f78e6b72ada1ed2c919";
            var timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");
            var passwordBytes = System.Text.Encoding.UTF8.GetBytes(shortcode + passkey + timestamp);
            var encodedPassword = System.Convert.ToBase64String(passwordBytes);
            try
            {
                MpesaTokenModel mtm = new MpesaTokenModel();
                mtm = getMpesaToken();
                StkPushModel stk = new StkPushModel();
                stk.BusinessShortCode = shortcode;
                stk.Password = encodedPassword;
                stk.Timestamp = timestamp;
                stk.TransactionType = "CustomerPayBillOnline";
                //stk.Amount = Int32.Parse(payment.Amount.ToString());
                stk.Amount = payment.Amount;
                stk.PartyA = payment.phoneNumber.Trim();
                stk.PartyB = "174379";
                stk.PhoneNumber = payment.phoneNumber.Trim();
                stk.CallBackURL = "https://mydomain.com/path";
                stk.AccountReference = "CompanyXLTD";
                stk.TransactionDesc = "Payment of X";
                var client = new RestClient("https://sandbox.safaricom.co.ke/mpesa/stkpush/v1/processrequest");
                var request = new RestRequest();
                request.Method = RestSharp.Method.Post;
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("Authorization", "Bearer " + mtm.access_token);
                request.AddParameter("application/json", JsonConvert.SerializeObject(stk), ParameterType.RequestBody);
                RestResponse response = client.Execute(request);
                Console.WriteLine(response.Content);

            }
            catch (Exception exc)
            {

                throw;
            }
            return View();
        }
        [HttpGet("gettoken")]
        public MpesaTokenModel getMpesaToken()
        {
            var client = new RestClient("https://sandbox.safaricom.co.ke/oauth/v1/generate?grant_type=client_credentials");
            var request = new RestRequest();
            request.Method = Method.Get;
            request.AddHeader("Authorization", "Basic cFJZcjZ6anEwaThMMXp6d1FETUxwWkIzeVBDa2hNc2M6UmYyMkJmWm9nMHFRR2xWOQ==");
            request.AddParameter("text/plain", "", ParameterType.RequestBody);
            RestResponse response = client.Execute(request);
            var token = JsonConvert.DeserializeObject<MpesaTokenModel>(response.Content);
            token.expiryTime = DateTime.Now.AddSeconds(Convert.ToDouble(token.expires_in));

            return token;
        }
    }
}