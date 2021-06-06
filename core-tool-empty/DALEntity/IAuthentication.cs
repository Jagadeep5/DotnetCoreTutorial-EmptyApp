using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using core_tool_empty.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;

namespace core_tool_empty.DALEntity
{
    public interface IAuthentication
    {

        public Task<IdentityResult> CreateUserAsync(Register register);
        public Task<SignInResult> LoginUserAsync(Login login);
        public Task<IList<AuthenticationScheme>> GetExternalAuthentications();
        public AuthenticationProperties GetAuthenticationProperties(string provider, string redirectionUrl);
    }
}
