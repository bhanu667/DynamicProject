using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminLTE1.Models;
using Microsoft.AspNetCore.Mvc;

namespace AdminLTE1.Controllers
{
    public class PageController : Controller
    {
        private readonly AddDbContext _context;

        public PageController(AddDbContext context)
        {
            _context = context;
        }
        //public IActionResult Index(string Id)
        //{
        //     CMSItems mvm = new CMSItems();
        //     var page = (_context.CMSItems.Where(p => p.PageUrl == Id).ToList());
        //    foreach (var item in page)
        //    {
        //        mvm.Description = item.Description;
        //        mvm.BannerImage = item.BannerImage;
        //    }
        //    return View(mvm);
        //}


        public IActionResult Index(string Id)
        {
            List<CMSItems> mvm = new List<CMSItems>();
            if (Id != null)
            {
                
                var page = (_context.CMSItems.Where(p => p.PageUrl == Id).ToList());
               
                return View(page);
            }
            else
            {
                mvm = (_context.CMSItems).ToList();
            }
            return View(mvm);
        }
    }
}