using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AdminLTE1.Helper;
using AdminLTE1.Models;
using AdminLTE1.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AdminLTE1.Controllers
{
    [AuthorizeActionFilter]
    public class UserController : Controller
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly AddDbContext _context;
        private readonly IHostingEnvironment _hostEnvironment;

        public UserController(UserManager<ApplicationUser> userManager, AddDbContext context, IHostingEnvironment hostingEnvironment)
        {
            _userManager = userManager;
            _context = context;
            _hostEnvironment = hostingEnvironment;
        }
        public async Task<IActionResult> ListUser()
        {
            var users = await _userManager.Users.ToListAsync();
           //ar role = users.FirstOrDefault().Role
            return View(users);
        }


        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                ViewBag.ErrorMessage = $"Role with ID = {id} cannot be found";
                return View("NotFound");
            }
            else
            {
                var result = await _userManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("ListUser");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View("ListUser");

            }
        }


        [HttpGet]
        public async Task<IActionResult> EditUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {id} cannot be found";
                return View("NotFound");
            }
            var userRoles = await _userManager.GetRolesAsync(user);
            var model = new EditUserViewModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                UserName = user.UserName,
                Address1 = user.Address1,
                Address2 = user.Address2,
                ProfilePicture = user.ProfilePicture,
                CId = user.CId,
                StateId = user.StateId,
                CityId = user.CityId
                //  ProfilePictureFile = user.ProfilePictureFile
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditUser(EditUserViewModel model)
        {
            var user = await _userManager.FindByIdAsync(model.Id);
            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {model.Id} cannot be found";
                return View("NotFound");
            }
            else
            {
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.Email = model.Email;
                user.UserName = model.UserName;
                user.Address1 = model.Address1;
                user.Address2 = model.Address2;
                user.ProfilePicture = model.ProfilePicture;
                user.CId = model.CId;
                user.StateId = model.StateId;
                user.CityId = model.CityId;
                //user.ProfilePictureFile = model.ProfilePictureFile;


                if (model.ProfilePictureFile != null)
                {
                    //var paths = Path.Combine(_hostEnvironment.WebRootPath, "Upload",model.ProfilePicture);

                    //if (System.IO.File.Exists(paths))
                    //{
                    //    // If file found, delete it    
                    //    System.IO.File.Delete(Path.Combine(paths));
                    //}


                    string wwwRootPath = _hostEnvironment.WebRootPath;
                    string fileName = Path.GetFileNameWithoutExtension(model.ProfilePictureFile.FileName);
                    string extension = Path.GetExtension(model.ProfilePictureFile.FileName);
                    user.ProfilePicture = DateTime.Now.ToString("yymmssfff") + extension;

                    string path = Path.Combine(wwwRootPath, "Upload", user.ProfilePicture);
                    var fileStream = new FileStream(path, FileMode.Create);
                    model.ProfilePictureFile.CopyTo(fileStream);
                    await _userManager.UpdateAsync(user);              
                }
            }
            //}
            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return RedirectToAction("ListUser");
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
            return View(model);
        }

    }

}
    
