using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using core_tool_empty.ConfigEntity;
using core_tool_empty.DAL;
using core_tool_empty.DALEntity;
using core_tool_empty.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace core_tool_empty.Controllers
{
    [Route("{controller}/{action}/{id?}")]
    public class HomeController : Controller
    {
        private readonly ICrud _crud = null;
        private readonly IConfiguration _config = null;
        private readonly IOptions<AuthorDetails> _options;
        private readonly IOptionsSnapshot<AuthorDetails> _optionsSnap;
        private readonly IOptionsMonitor<AuthorDetails> _optionsMonitor;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="crud"></param>
        /// <param name="config"></param>
        /// <param name="options"></param>
        /// <param name="optionsSnapshot"></param>
        public HomeController(ICrud crud, IConfiguration config, IOptions<AuthorDetails> options, 
            IOptionsSnapshot<AuthorDetails> optionsSnapshot, IOptionsMonitor<AuthorDetails> optionsMonitor)
        {
            _crud = crud;
            _config = config;
            _options = options;
            _optionsSnap = optionsSnapshot;
            _optionsMonitor = optionsMonitor;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Route("~/")] // ~ is used to override parent route at controller level 
        [Route("~/{controller}/{action}/{id?}")]
        public ViewResult Index()
        {


            Books books = new Books();
            books.Id = TempData["Id"] is object ? (int)TempData["Id"] : 0;

            ViewBag.bookList = this._crud.GetBooks();

            return View(books);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [ActionName("Config")]
        public ViewResult GetConfigValues()
        {
            ViewBag.app_name = this._config["AppName"];
            ViewBag.app_bool = this._config["AppBool"];
            ViewBag.app_bool2 = this._config.GetValue<bool>("AppBool");
            var section = this._config.GetSection("AppObj:obj1:obj11");
            ViewBag.app_obj = section.Get<string[]>();
            ViewBag.connStr = this._config.GetConnectionString("Dev");

            dynamic model = new ExpandoObject();
            model.AuthorDetails = this._options.Value;
            model.AuthorDetailsSnap = this._optionsSnap.Value; //This will throw error if we use in singleton method, to overcome this we will use IOptionMoniter
            model.DeveloperInfo = this._optionsSnap.Get("DeveloperInfo");
            

            return View("Config", model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="books"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Index(Books books)
        {
            int id = this._crud.AddBook(books);
            TempData["Id"] = id;

            return RedirectToAction("Index");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> delete(int id)
        {
            await this._crud.DeleteBook(id);
            return RedirectToAction("Index");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>

        [Route("~/tag-helpers/{a:alpha:minlength(3)?}")]
        public ViewResult taghelpers(string a)
        {
            ViewBag.na = a;
            return View("taghelpers");
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="formCollection"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult tag(IFormCollection formCollection)
        {
            string value = formCollection["name"].ToString();
            return RedirectToAction("taghelpers", new { a = value });
        }
    }
}
