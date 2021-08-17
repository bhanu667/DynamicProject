using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AdminLTE1.Areas.Identity.Pages.Account;
using AdminLTE1.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AdminLTE1.Controllers
{
    public class AddUserController : Controller
    {
        private readonly AddDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHostingEnvironment _hostEnvironment;

        public AddUserController(AddDbContext context, UserManager<ApplicationUser> userManager, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            _userManager = userManager;
            _hostEnvironment = hostingEnvironment;
        }
        public IActionResult CreateUser()
        {
            // RegisterModel model = new RegisterModel();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(RegisterModel.InputModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Address1 = model.Address1,
                    Address2 = model.Address2,
                    CId = model.CId,
                    StateId = model.StateId,
                    CityId = model.CityId,
                    ProfilePicture = model.ProfilePicture
                };

                if (model.ProfilePictureFile != null)
                {
                   
                    string wwwRootPath = _hostEnvironment.WebRootPath;
                    string fileName = Path.GetFileNameWithoutExtension(model.ProfilePictureFile.FileName);
                    string extension = Path.GetExtension(model.ProfilePictureFile.FileName);
                    user.ProfilePicture = DateTime.Now.ToString("yymmssfff") + extension;


                    string path = Path.Combine(wwwRootPath, "Upload", user.ProfilePicture);
                    var fileStream = new FileStream(path, FileMode.Create);
                    model.ProfilePictureFile.CopyTo(fileStream);
                    await _userManager.UpdateAsync(user);
                }
            

                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {

                    return RedirectToAction("ListUser", "User");
                }
                return View(model);
            }
            else
                return View(model);
        }
    }

    }