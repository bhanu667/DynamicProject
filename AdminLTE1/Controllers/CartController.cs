using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AdminLTE1.Models;
using AdminLTE1.PayPalHelper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace AdminLTE1.Controllers
{
    //[Route("cart")]
    public class CartController : Controller
    {
        public IConfiguration Configuration { get; }

        private readonly AddDbContext _api;

        public CartController(IConfiguration _configuration, AddDbContext api)
        {
            Configuration = _configuration;
            _api = api;
        }


        //[Route("index")]
        //[Route("")]
        //[Route("~/")]
        public IActionResult Index()
        {
            var productModel = new ProductModel();
            ViewBag.products = productModel.FindAll();
            ViewBag.total = productModel.FindAll().Sum(p => p.Price * p.Quantity);
            return View();
        }

        [HttpPost]
        //[Route("checkout")]
        public async Task<IActionResult> Checkout(double total)
        {
            var payPalAPI = new PayPalAPI(Configuration);
            string url = await payPalAPI.getRedirectURLToPayPal(total, "USD");
            return Redirect(url);
        }


        //[Route("success")]
        public async Task<IActionResult> Success([FromQuery(Name = "paymentId")] string paymentId,
            [FromQuery(Name = "PayerID")] string payerID)
        {
            var payPalAPI = new PayPalAPI(Configuration);
            PayPalPaymentExecutedResponse result = await payPalAPI.executedPayment(paymentId, payerID);

            OrderAPI lst = new OrderAPI();

            //PayPalPaymentExecutedResponse lst = JsonConvert.DeserializeObject<PayPalPaymentExecutedResponse>(result);

            Debug.WriteLine("Transaction Details");
            Debug.WriteLine("cart: " + result.cart);
            Debug.WriteLine("create_time: " + result.create_time.ToLongDateString());
            Debug.WriteLine("id: " + result.id);
            Debug.WriteLine("intent: " + result.intent);
            Debug.WriteLine("links 0 - href: " + result.links[0].href);
            Debug.WriteLine("links 0 - method: " + result.links[0].method);
            Debug.WriteLine("links 0 - rel: " + result.links[0].rel);
            Debug.WriteLine("payer_info - first_name: " + result.payer.payer_info.first_name);
            Debug.WriteLine("payer_info - last_name: " + result.payer.payer_info.last_name);
            Debug.WriteLine("payer_info - email: " + result.payer.payer_info.email);
            Debug.WriteLine("payer_info - country_code: " + result.payer.payer_info.country_code);
            Debug.WriteLine("payer_info - shipping_address: " + result.payer.payer_info.shipping_address);
            Debug.WriteLine("payer_info - payer_id: " + result.payer.payer_info.payer_id);
            Debug.WriteLine("state: " + result.state);


            lst.Id = result.id;
            lst.Intent = result.intent;
            lst.State = result.state;
            lst.CreateDate = result.create_time;
            _api.OrderAPI.Add(lst);
            _api.SaveChanges();


            ViewBag.result = result;
            return View("Success");
        }
    }
}