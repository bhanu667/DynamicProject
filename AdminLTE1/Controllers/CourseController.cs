using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminLTE1.Models;
using Microsoft.AspNetCore.Mvc;

namespace AdminLTE1.Controllers
{
    public class CourseController : Controller
    {
        private readonly AddDbContext _context;

        public CourseController(AddDbContext context)
        {
            _context = context;
        }

        public IActionResult CourseList()
        {
            var result = _context.Teachers.ToList();
            return View();
        }

        public IActionResult AddCourse()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddCourse(Course model)
        {
            if (ModelState.IsValid)
            {
                var obj = new Course();
                obj.CourseName = model.CourseName;
                obj.ClassId = model.ClassId;               
                _context.Course.Add(obj);
                _context.SaveChanges();
                return Json("Added Successfully");
            }
            return View();
        }

    }
}