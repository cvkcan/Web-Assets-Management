using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Threading.Tasks;
using Web_Assets_Management.Models;
using Web_Assets_Management.DBArea;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace Web_Assets_Management.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        CategoriesArea CategoriesArea = new CategoriesArea();
        AssetsArea AssetsArea = new AssetsArea();
        UsersArea UsersArea = new UsersArea();
        VendorsArea VendorsArea = new VendorsArea();
        ReportsArea ReportsArea = new ReportsArea();

        readonly IConfiguration _configuration;
        public HomeController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpPost]
        public IActionResult DeleteStock(Assets assets)
        {
            AssetsArea.DeleteAssets(assets.AssetsNumber);
            TempData["inf"] = $"Product with ID {assets.AssetsNumber} has been deleted.";
            Console.WriteLine($"Deleted {assets.AssetsNumber}!");
            return RedirectToAction("Stock");
        }

        [HttpPost]
        public IActionResult UpdateStock(Assets assets)
        {
            bool state = AssetsArea.UpdateAssets(assets.AssetsNumber, assets.Name, assets.Price, assets.CategorieName, assets.Vendor);
            if (state.Equals(true))
            {
                TempData["UpdateSuccessed"] = $"Product with ID {assets.AssetsNumber} has been updated.";
                Console.WriteLine($"Updated {assets.AssetsNumber} !!!");
            }
            else
            {
                Console.WriteLine(state.ToString());
                TempData["UpdateFailed"] = "Something went wrong.";
            }
            return RedirectToAction("Stock");
        }
        public IActionResult Stock(string value)
        {
            ReturnCategoriesAndVendors();
            if (!string.IsNullOrEmpty(value))
            {
                var assetsByCategory = AssetsArea.SearchByValue(value);
                return View(assetsByCategory);
            }
            else
            {
                return View(AssetsArea.GetAssets());
            }
        }
        [HttpPost]
        public IActionResult Stock(string value, string CategorieName)
        {
            if (!(string.IsNullOrEmpty(value)||string.IsNullOrEmpty(CategorieName)))
            {
                var assetsByCategory = AssetsArea.SearchCategoriesByValue(value, CategorieName);
                return View(assetsByCategory);
            }
            else if (!string.IsNullOrEmpty(value))
            {
                var assetsByCategory = AssetsArea.SearchByValue(value);
                return View(assetsByCategory);
            }
            else
            {
                return Redirect("/Home/Stock");
            }
        }

        public IActionResult StockBy(string CategorieName)
        {
            return View("Stock", AssetsArea.GetAssetsByCategorie(CategorieName));
        }

        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("login", "access");
        }

        public IActionResult Index()
        {
            return View();
        }

       
        public IActionResult Categories()
        {
            return View(CategoriesArea.GetCategories());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Assets()
        {
            ReturnCategoriesAndVendors();
            return CheckValid();
        }

        [HttpPost]
        public IActionResult Categorie(Assets assets)
        {
            ModelState.Clear();
            ViewBag.categorie = CategoriesArea.GetCategories();
            ViewBag.vendor = VendorsArea.GetVendors();
            bool state=(bool)CategoriesArea.AddCategories(assets.CategorieName);
            if (state.Equals(true))
            {
                Console.WriteLine($"Added {assets.CategorieName} !!!");
                ViewBag.Success2 = $"{assets.CategorieName} has been added.";
                return View("Assets");
            }
            else
            {
                ViewBag.Error2 = "Something went wrong. Check the fields in the Form.";
                return View("Assets");
            }
        }

        [HttpPost]
        public IActionResult Assets(Assets assets)
        {
            ModelState.Clear();
            ViewBag.categorie = CategoriesArea.GetCategories();
            ViewBag.vendor = VendorsArea.GetVendors(); 
            bool status=(bool)AssetsArea.AddAssets(assets.AssetsNumber,assets.Name,assets.Price,assets.CategorieName, assets.Vendor);
            if (status.Equals(true))
            {
                Console.WriteLine($"Added {assets.AssetsNumber} !!!");
                ViewBag.Success = $"Product with ID {assets.AssetsNumber} has been added.";
                return View();
            }
            else
            {
                ViewBag.Error = "Something went wrong. Check the fields in the Form.";
                return View();
            }

        }

        [HttpPost]
        public IActionResult AssetsCategorie()
        {
            return View();
        }

        [NonAction]
        public IActionResult CheckValid()
        {
            if (HttpContext.Session.GetString("Auth").Equals("madmin"))
            {
                return View();
            }
            else
            {
                var url = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}{HttpContext.Request.Path}{HttpContext.Request.QueryString}";
                ReportsArea.AddReports(DateTime.Now, Convert.ToInt32(HttpContext.Session.GetString("EId")), HttpContext.Session.GetString("Auth"),url);
                
                return RedirectToAction("AccessDenied", "Access");
            }
        }

        public void ReturnCategoriesAndVendors()
        {
            ViewBag.categorie = CategoriesArea.GetCategories();
            ViewBag.vendor = VendorsArea.GetVendors();
        }
    }
}