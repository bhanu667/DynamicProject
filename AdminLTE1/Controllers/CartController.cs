using AdminLTE1.Models;
using AdminLTE1.PayPalHelper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

using Rotativa.AspNetCore;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;


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

        public IActionResult Invoice(string id)
        {
            var inc = _api.OrderAPI.Where(x => x.OrderId == id).FirstOrDefault();
            return View(inc);
        }

        public IActionResult InvoicePdf(int id)
        {
            var inc = _api.OrderAPI.Where(x => x.Id == id).FirstOrDefault();
            return new ViewAsPdf("InvoicePdf", inc);
        }




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

            try
            {



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

                var address = $"{result.payer.payer_info.shipping_address.recipient_name} {result.payer.payer_info.shipping_address.line1} {result.payer.payer_info.shipping_address.city} {result.payer.payer_info.shipping_address.country_code} {result.payer.payer_info.shipping_address.postal_code}";

                lst.OrderId = result.id;
                lst.PayerId = result.payer.payer_info.payer_id;
                lst.Email = result.payer.payer_info.email;
                lst.FirstName = result.payer.payer_info.first_name;
                lst.LastName = result.payer.payer_info.last_name;
                lst.Intent = result.intent;
                lst.State = result.state;
                lst.CountryCode = result.payer.payer_info.country_code;
                lst.PaymentMethod = result.payer.payment_method;
                lst.Amount = result.transactions.FirstOrDefault()?.amount.total;
                lst.ShippingAddress = address;
                lst.TransactionFee = result.transactions.FirstOrDefault().related_resources.FirstOrDefault().sale.transaction_fee.value;
                lst.CreateDate = result.create_time;
                lst.SaleId = result.transactions.FirstOrDefault().related_resources.FirstOrDefault().sale.id;
                _api.OrderAPI.Add(lst);
                _api.SaveChanges();

            }
            catch (System.Exception ex)
            {
                var msg = ex.Message;
            }


            ViewBag.result = result;
            return View("Success");
        }

    }
}
