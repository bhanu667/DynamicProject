using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AdminLTE1.Models;
using AdminLTE1.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AdminLTE1.Controllers
{
    public class PayPalController : Controller
    {
        private readonly AddDbContext _context;

        public PayPalController(AddDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]        public IActionResult FillState([FromBody] OrderData data)        {            OrderDetails details = new OrderDetails();            details.OrderId = data.id;
            details.Status = data.status;
            details.CreateTime = data.update_time;
            _context.OrderDetails.Add(details);
            _context.SaveChanges();
            return Json("Saved Successfully");        }
    }
}