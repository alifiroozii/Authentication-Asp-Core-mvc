using System;
using System.Collections.Generic;
using System.Text;

using Authentication.Core.Interfaces;
using Authentication.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Authentication.Core.Classes
{
    public class RoleArribute : AuthorizeAttribute, IAuthorizationFilter
    {

        string _roleName;

        IUser _iuser;

        public RoleArribute(string rolename)
        {
            _roleName = rolename;
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (context.HttpContext.User.Identity.IsAuthenticated)
            {
                string userName = context.HttpContext.User.Identity.Name;

                _iuser = (IUser)context.HttpContext.RequestServices.GetService(typeof(IUser));

                if(_iuser.checkUserRole(_roleName,userName))
                {
                    context.Result = new RedirectResult("/Account/Login");
                }
                else
                {
                    context.Result = new RedirectResult("/Account/Login");
                }

            }
        }
    }
}
