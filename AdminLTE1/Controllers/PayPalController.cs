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
        public IActionResult FillState(string transactionId, string transactionStatus, DateTime transactionTime)        {            OrderDetails details = new OrderDetails();            details.OrderId = transactionId;
            details.Status = transactionStatus;
            details.CreateTime = transactionTime;
            _context.OrderDetails.Add(details);
            _context.SaveChanges();
            return Json("Saved Successfully");        }


        //[HttpPost]
        //public IActionResult FillState(OrderData transaction)        //{        //    //OrderDetails details = new OrderDetails();        //    //details.OrderId = transactionId;
        //    //details.Status = transactionStatus;
        //    //details.CreateTime = transactionTime;
        //    //_context.OrderDetails.Add(details);
        //    //_context.SaveChanges();
        //    return Json("Saved Successfully");        //}

    }
}