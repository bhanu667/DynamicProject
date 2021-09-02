using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AdminLTE1.Models;
using Newtonsoft.Json;

namespace AdminLTE1.Controllers
{
    public class HomeController : Controller
    {
        private readonly AddDbContext _context;
        public HomeController(AddDbContext context)
        {
            this._context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Index1()
        {
            return View();
        }

        public IActionResult Paypal()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }


        public JsonResult IsProductNameExist(string pageName, int? Id)
        {
            var validateName = _context.CMSItems.FirstOrDefault
                                (x => x.PageName == pageName && x.Id != Id);
            if (validateName != null)
            {
                return Json(false, new JsonSerializerSettings());
            }
            else
            {
                return Json(true, new JsonSerializerSettings());
            }
        }

        //public JsonResult IsBookNameExist(string bookName, int? bookId)
        //{
        //    var validateName = _context.Books.FirstOrDefault
        //                        (x => x.BookName == bookName && x.BookId != bookId);
        //    if (validateName != null)
        //    {
        //        return Json(false, new JsonSerializerSettings());
        //    }
        //    else
        //    {
        //        return Json(true, new JsonSerializerSettings());
        //    }
        //}


        public JsonResult IsPageUrlExist(string pageUrl, int? Id)
        {
            var validateName = _context.CMSItems.FirstOrDefault
                                (x => x.PageUrl == pageUrl && x.Id != Id);
            if (validateName != null)
            {
                return Json(false, new JsonSerializerSettings());
            }
            else
            {
                return Json(true, new JsonSerializerSettings());
            }
        }



        public IActionResult Contact()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public JsonResult Country()
        {
            var coun = _context.Country.
                Select(x => new { value = x.CountryId, text = x.CName }).ToList();
            return Json(coun);
        }

        public JsonResult GetStatesByCountryId(int CId)
        {
            var sta = _context.State.Where(x => x.CId == CId).
                Select(x => new { value = x.StateId, text = x.SName }).ToList();
            return Json(sta);
        }
        public JsonResult GetCitiesByStateId(int StateId)
        {
            var city = _context.City.Where(x => x.SId == StateId).
                Select(x => new { value = x.CityId, text = x.CityName }).ToList();
            return Json(city);
        }
    }
}
