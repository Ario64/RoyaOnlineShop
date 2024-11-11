﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using OnlineShop.Core.Services.Interfaces;


namespace OnlineShop.Core.Security;

public class PermissionCheckerAttribute : AuthorizeAttribute, IAuthorizationFilter
{
    private IPermissionService _permissionService;
    private int _permissionId = 0;

    public PermissionCheckerAttribute(int permissionId)
    {
        _permissionId = permissionId;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        _permissionService = (IPermissionService)context.HttpContext.RequestServices.GetService(typeof(IPermissionService));

        if (context.HttpContext.User.Identity.IsAuthenticated)
        {
            string userName = context.HttpContext.User.Identity.Name;

            if (!_permissionService.CheckPermission(userName, _permissionId))
            {
                context.Result = new RedirectResult("/Login?" + context.HttpContext.Request.Path);
            }
        }
        else
        {
            context.Result = new RedirectResult("/Login");
        }
    }
}