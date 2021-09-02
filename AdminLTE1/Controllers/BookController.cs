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
    public class BookController : Controller
    {
        private readonly AddDbContext _context;
        private readonly IHostingEnvironment _hostEnvironment;

        public BookController(AddDbContext context, IHostingEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        public IActionResult BookList()
        {
            try
            {
                var result = _context.Books.ToList();                
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return View();
        }

        [HttpGet]
        public IActionResult AddBook()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddBook(Books model)
        {
            if (ModelState.IsValid)
            {
                var book = new Books();

                book.BookName = model.BookName;
                book.Author = model.Author;
                book.Description = model.Description;
                book.ClassId = model.ClassId;
                book.IsActive = model.IsActive;
                if (model.BookImageFile != null)
                {                   
                    string wwwRootPath = _hostEnvironment.WebRootPath;
                    string fileName = Path.GetFileNameWithoutExtension(model.BookImageFile.FileName);
                    string extension = Path.GetExtension(model.BookImageFile.FileName);
                    book.BookImage = DateTime.Now.ToString("yymmssfff") + extension;


                    string path = Path.Combine(wwwRootPath, "Images", book.BookImage);
                    var fileStream = new FileStream(path, FileMode.Create);
                    model.BookImageFile.CopyTo(fileStream);
                }
                _context.Books.Add(book);
                _context.SaveChanges();
            }                        
                return Json("Book Added Successfully");
        }
    }
}