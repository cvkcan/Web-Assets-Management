using Microsoft.AspNetCore.Mvc;

namespace Web_Assets_Management.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult Handle(ClientErrorData code)
        {
            ViewBag.code = code;
            return View();
        }
    }
}
