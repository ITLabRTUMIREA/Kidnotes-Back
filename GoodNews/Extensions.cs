using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GoodNews
{
    public static class Extensions
    {
        public static int UserId(this ControllerBase controller) => int.Parse(controller.User.FindFirst(ClaimTypes.NameIdentifier).Value);
    }
}
