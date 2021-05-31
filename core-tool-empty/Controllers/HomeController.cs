using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using core_tool_empty.DAL;
using core_tool_empty.DALEntity;
using core_tool_empty.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.Extensions.Configuration;

namespace core_tool_empty.Controllers
{
    [Route("{controller}/{action}/{id?}")]
    public class HomeController : Controller
    {
        private readonly ICrud _crud = null;
        private readonly IConfiguration _config = null;
        public HomeController(ICrud crud, IConfiguration config)
        {
            _crud = crud;
            _config = config;

        }
        [Route("~/")] // ~ is used to override parent route at controller level 
        [Route("~/{controller}/{action}/{id?}")]
        public ViewResult Index()
        {
            
            
            Books books = new Books();
            books.Id = TempData["Id"] is object ? (int)TempData["Id"] : 0;

            ViewBag.bookList = this._crud.GetBooks();
            
            return View(books);
        }

        [ActionName("Config")]
        public ViewResult GetConfigValues()
        {
            ViewBag.app_name = this._config["AppName"];
            ViewBag.app_bool = this._config["AppBool"];
            ViewBag.app_bool2 = this._config.GetValue<bool>("AppBool");
            var section = this._config.GetSection("AppObj:obj1:obj11");
            ViewBag.app_obj = section.Get<string[]>();
            ViewBag.connStr = this._config.GetConnectionString("Dev");
            return View("Config");
        }

        [HttpPost]

        public ActionResult Index(Books books)
        {
            int id = this._crud.AddBook(books);
            TempData["Id"] = id;

            return RedirectToAction("Index");
        }

        [HttpGet]

        public async Task<ActionResult> delete(int id)
        {
            await this._crud.DeleteBook(id);
            return RedirectToAction("Index");
        }

        [Route("~/tag-helpers/{a:alpha:minlength(3)?}")]
        public ViewResult taghelpers(string a)
        {
            ViewBag.na = a;
            return View("taghelpers");
        }
        [HttpPost]
        public ActionResult tag(IFormCollection formCollection)
        {
            string value = formCollection["name"].ToString();
            return RedirectToAction("taghelpers",new { a = value });
        }
    }
}
