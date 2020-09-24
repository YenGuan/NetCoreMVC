using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using NetCoreIdentity.Model;
using NetCoreIdentity.Web.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace NetCoreIdentity.Web.Filters
{
    public class FunctionAuthorizeAttribute : AuthorizeAttribute, IAsyncAuthorizationFilter
    {
        private readonly UserManager<NetCoreIdentityUser> _userManager;
        private readonly UnitOfWork<NetCoreIdentityContext> _unitOfWork;

        public FunctionAuthorizeAttribute(UserManager<NetCoreIdentityUser> userManager, IUnitOfWork<NetCoreIdentityContext> unit)
        {
            _userManager = userManager;
            _unitOfWork = (UnitOfWork<NetCoreIdentityContext>)unit;
        }
        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            ControllerActionDescriptor descriptor = context?.ActionDescriptor as ControllerActionDescriptor;
            if (descriptor == null || context.HttpContext.User == null)
            {
                //throw new NullReferenceException();
                return;
            }    
            //string actionName = descriptor.ActionName;
            //string ctrlName = descriptor.ControllerName;
            //string userId = context.HttpContext.User?.FindFirst(ClaimTypes.NameIdentifier).Value;
            var repo = _unitOfWork.GetRepository<AppFunction>();
            var function = await repo.FirstOrDefaultAsync(m => m.ControllerName == descriptor.ControllerName);
            if (function == null)
            {
                context.Result = new ForbidResult();
                return;
            }
            var cUser = await _userManager.FindByIdAsync(context.HttpContext.User?.FindFirst(ClaimTypes.NameIdentifier).Value);
            var rolesHasPermit = function.FunctionRoles.Select(m => m.Role).ToList();
            foreach (var role in rolesHasPermit)
            {
                if (await _userManager.IsInRoleAsync(cUser, role.Name))
                {
                    return;
                }
            }
            context.Result = new ForbidResult();
        }
    }
}
