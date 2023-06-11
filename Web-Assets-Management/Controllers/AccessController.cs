using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Web_Assets_Management.DBArea;
using Web_Assets_Management.Models;
using Web_Assets_Management.Models.ViewModels;
using Microsoft.AspNetCore.Http;

namespace Web_Assets_Management.Controllers
{
    public class AccessController : Controller
    {
        [HttpPost]
        public async Task<IActionResult> Login(LoginVM login)
        {
            if (!((Convert.ToString(login.EId).Equals(null)||(login.Password==null))||login.Password.Length>=25))
            {
                UsersArea UsersArea = new UsersArea();
                bool valid = Convert.ToBoolean(UsersArea.UsersAuth(Convert.ToInt32(login.EId), login.Password.ToString()));
                if (valid.Equals(true))
                {
                    List<Users> users = UsersArea.GetUsersAuth(Convert.ToInt32(login.EId));
                    foreach (var item in users)
                    {
                        HttpContext.Session.SetString("Auth", item.Auth);
                        HttpContext.Session.SetString("EId", Convert.ToString(item.EId));
                    }
                    List<Claim> claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.NameIdentifier,Convert.ToString(login.EId)),
                    new Claim("OtherProperties","Example Role")
                };

                    ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    AuthenticationProperties properties = new AuthenticationProperties()
                    {
                        AllowRefresh = true
                    };
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), properties);
                    return RedirectToAction("Index", "Home");
                }
            }  
            ViewData["ValidateMessage"] = "The username or password incorrect.";
            ModelState.Clear();
            return View();
        }
        public IActionResult Login()
        {
            ClaimsPrincipal claimsUser = HttpContext.User;
            if (claimsUser.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
