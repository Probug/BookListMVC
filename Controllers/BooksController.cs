using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using BookListMVC.Models;
using BookListMVC.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

namespace BookListMVC.Controllers
{
    public class BooksController : Controller
    {
        private readonly BookService _db;

        [BindProperty]
        public Book Book { get; set; }
        public string ID {get; set;}
        public BooksController(BookService db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View(_db.Get());
        }

        // GET: Cars/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Cars/Create/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Book book)
        {
            if (ModelState.IsValid)
            {
                _db.Create(Book);
                return RedirectToAction(nameof(Index));
            }
            return View(Book);
        }


 // GET: Cars/Edit/5
        public IActionResult Edit(string id)
        {

            if (id == null)
            {
                return NotFound();
            }

            Book = _db.Get().Find(x => x.Id == id);

            if (Book == null)
            {
                return NotFound();
            }
            return View(Book);
        }

        // POST: Cars/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(string id, Book book)
        {
            if (id != book.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                _db.Update(id, book);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(Book);
            }
        }        

        #region API Calls
         [HttpGet]
        public IActionResult GetAll ()
        {
            return Json(new {data = _db.Get().ToList()});
        }

        [HttpDelete]
        public IActionResult Delete (string id)
        {
           
            var bookFromDb = _db.Get().Find(x => x.Id == id);

            if (bookFromDb == null)
            {
                return Json( new{ success = false, message = "Error while Deleting"});
            }
 
               _db.Remove(bookFromDb.Id); 
            
            return Json( new{ success = true, message="Delete Successful"});
               
        }
        #endregion
          
    }


}
