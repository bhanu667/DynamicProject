using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AdminLTE1.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace AdminLTE1.Controllers
{
    public class TeacherController : Controller
    {
        private readonly AddDbContext _context;
        private readonly IHostingEnvironment _hostEnvironment;
        public TeacherController(AddDbContext context, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            _hostEnvironment = hostingEnvironment;
        }

        public IActionResult TeacherList()
        {
            var result = _context.Teachers.ToList();
            // RegisterModel model = new RegisterModel();
            return View();
        }

        public IActionResult AddTeacher()
        {
            // RegisterModel model = new RegisterModel();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddTeacher(Teachers model)
        {
            if (ModelState.IsValid)
            {
                var obj = new Teachers();
                obj.TeacherName = model.TeacherName;
                obj.Subject = model.Subject;
                obj.ClassId = model.ClassId;
                if (model.TeacherImageFile != null)
                {
                    string wwwRootPath = _hostEnvironment.WebRootPath;
                    string fileName = Path.GetFileNameWithoutExtension(model.TeacherImageFile.FileName);
                    string extension = Path.GetExtension(model.TeacherImageFile.FileName);
                    obj.TeacherImage = DateTime.Now.ToString("yymmssfff") + extension;
                    string path = Path.Combine(wwwRootPath, "Images", obj.TeacherImage);
                    var fileStream = new FileStream(path, FileMode.Create);
                    model.TeacherImageFile.CopyTo(fileStream);
                }
                _context.Teachers.Add(obj);
                _context.SaveChanges();
                //return RedirectToAction("TeacherList");
            }
            return Json("Added Successfully");
        }
    }
}