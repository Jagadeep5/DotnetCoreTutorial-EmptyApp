using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using core_tool_empty.DAL;
using core_tool_empty.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.TagHelpers;

namespace core_tool_empty.Controllers
{
    public class HomeController : Controller
    {
        private readonly Crud _crud = null;
        public HomeController(Crud crud)
        {
            _crud = crud;
        }
        public ViewResult Index()
        {
            Books books = new Books();
            return View(books);
        }

        [HttpPost]

        public ActionResult Index(Books books)
        {
            this._crud.AddBook(books);
            return RedirectToAction("Index");
        }

        public ViewResult taghelpers()
        {
            return View("taghelpers");
        }
    }
}
