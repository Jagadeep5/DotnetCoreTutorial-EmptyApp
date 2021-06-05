using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using core_tool_empty.DALEntity;
using core_tool_empty.Data;
using Microsoft.AspNetCore.Mvc;

namespace core_tool_empty.Controllers
{
    [Route("auth/{action}/{id?}")]
    public class AuthenticationController : Controller
    {
        public readonly IAuthentication _authentication;
        public AuthenticationController(IAuthentication authentication)
        {
            this._authentication = authentication;
        }
        
        public IActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Signup(Register register)
        {
            if (ModelState.IsValid)
            {
                var result = await this._authentication.CreateUserAsync(register);
                ModelState.Clear();
                if (!result.Succeeded)
                {
                    foreach(var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View();
        }

        public IActionResult SignIn()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignIn(Login login)
        {
            var result = await this._authentication.LoginUserAsync(login);
            if (!result.Succeeded)
            {
                if (result.IsNotAllowed)
                    ModelState.AddModelError("", "Not Autherised");

                ModelState.AddModelError("", "Invalid login attempt");

                return View();
            }
            return RedirectToAction("Index", "home");
        }
    }
}
