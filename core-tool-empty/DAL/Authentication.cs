using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using core_tool_empty.DALEntity;
using core_tool_empty.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;

namespace core_tool_empty.DAL
{
    public class Authentication : IAuthentication
    {
        public readonly UserManager<IdentityUser> _userManager;
        public readonly SignInManager<IdentityUser> _signInManager;
        public Authentication(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
        }
        public async Task<IdentityResult> CreateUserAsync(Register register)
        {
            var user = new IdentityUser()
            {
                Email = register.Email,
                UserName = register.Email
            };
            var result = await this._userManager.CreateAsync(user, register.Password);
            return result;
        }

        public async Task<SignInResult> LoginUserAsync(Login login)
        {
            var result = await this._signInManager.PasswordSignInAsync(login.UserId, login.Password, true, false);
            return result;
        }

        public async Task<IList<AuthenticationScheme>> GetExternalAuthentications()
        {
            return (await this._signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public AuthenticationProperties GetAuthenticationProperties(string provider, string redirectionUrl)
        {
            return this._signInManager.ConfigureExternalAuthenticationProperties(provider, redirectionUrl);
        }
    }
}
