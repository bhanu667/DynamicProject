using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminLTE1.Models;
using AdminLTE1.Security;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;

namespace AdminLTE1.Controllers
{
    
    public class UserDetailController : Controller
    {
        private readonly AddDbContext _context;
        private readonly IConfiguration _configuration;

        public UserDetailController(AddDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        
        [HttpGet]
        public IActionResult GetUser(Guid UserId)
        {
            var user = _context.Users.Where(x=> x.Id == UserId.ToString()).ToList();
            var UserList = (from users in _context.Users
                             select new SelectListItem
                             {
                                 Value = users.Id,
                                 Text = users.UserName
                             }).ToList();
            ViewBag.UserList = UserList;
            var qs = Request.QueryString.ToString();
            //TempData["qs"] = qs;
            Encryption encryption = new Encryption();
            string sandbox = _configuration["Encryption:UniqueKey"];
           var quc = encryption.EncryptString(qs,sandbox);
            Event et = new Event();
            et.Url = quc;
            _context.Event.Add(et);
            _context.SaveChanges();
            return View(user);

        }

        

        //public IActionResult Index()
        //{
        //    var qrs = TempData["qs"];
        //    return View();
        //}
    }
}