﻿using System.Security.Claims;

namespace MyTasks.Persistence.Extensions
{
    public static class CaimsPrincipalExtensions
    {
        public static string GetUserId(this ClaimsPrincipal model)
        {
            return model.FindFirstValue(ClaimTypes.NameIdentifier);
        }
    }
}
