using ClosedXML.Excel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using Web_Assets_Management.DBArea;

namespace Web_Assets_Management.Controllers
{
    [Authorize]
    public class ExportsController : Controller
    {
        AssetsArea AssetsArea = new AssetsArea();
        UsersArea UsersArea = new UsersArea();
        ReportsArea ReportsArea = new ReportsArea();


        public IActionResult ExportReportsExcel()
        {
            if (HttpContext.Session.GetString("Auth").Equals("madmin"))
            {
                var den = ReportsArea.GetReports();
                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("ReportsList");
                    worksheet.Cell(1, 1).Value = "Date";
                    worksheet.Cell(1, 2).Value = "EId";
                    worksheet.Cell(1, 3).Value = "Auth";
                    worksheet.Cell(1, 4).Value = "Information";
                    worksheet.Cell(1, 5).Value = "Url";

                    int RowCount = 2;
                    foreach (var item in den)
                    {
                        worksheet.Cell(RowCount, 1).Value = item.Date;
                        worksheet.Cell(RowCount, 2).Value = item.EId;
                        worksheet.Cell(RowCount, 3).Value = item.Auth;
                        worksheet.Cell(RowCount, 4).Value = item.Information;
                        worksheet.Cell(RowCount, 5).Value = item.Url;
                        RowCount++;
                    }

                    using (var stream = new MemoryStream())
                    {
                        workbook.SaveAs(stream);
                        var content = stream.ToArray();
                        return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "reports.xlsx");
                    }
                }
            }
            else
            {
                var url = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}{HttpContext.Request.Path}{HttpContext.Request.QueryString}";
                ReportsArea.AddReports(DateTime.Now, Convert.ToInt32(HttpContext.Session.GetString("EId")), HttpContext.Session.GetString("Auth"), url);

                return RedirectToAction("AccessDenied", "Access");
            }
        }
        public IActionResult ExportAssetsExcel()
        {
            var den = AssetsArea.GetAssets();
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("AssetsList");
                worksheet.Cell(1, 1).Value = "AssetsNumber";
                worksheet.Cell(1, 2).Value = "Name";
                worksheet.Cell(1, 3).Value = "Price";
                worksheet.Cell(1, 4).Value = "CategorieName";
                worksheet.Cell(1, 5).Value = "Vendor";

                int RowCount = 2;
                foreach (var item in den)
                {
                    worksheet.Cell(RowCount, 1).Value = item.AssetsNumber;
                    worksheet.Cell(RowCount, 2).Value = item.Name;
                    worksheet.Cell(RowCount, 3).Value = item.Price;
                    worksheet.Cell(RowCount, 4).Value = item.CategorieName;
                    worksheet.Cell(RowCount, 5).Value = item.Vendor;
                    RowCount++;
                }

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "assets.xlsx");
                }
            }
        }

        public IActionResult ExportUsersExcel()
        {
            if (HttpContext.Session.GetString("Auth").Equals("madmin"))
            {
                var den = UsersArea.GetUsers();
                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("UsersList");
                    worksheet.Cell(1, 1).Value = "ID";
                    worksheet.Cell(1, 2).Value = "Password";
                    worksheet.Cell(1, 3).Value = "Auth";


                    int RowCount = 2;
                    foreach (var item in den)
                    {
                        worksheet.Cell(RowCount, 1).Value = item.EId;
                        worksheet.Cell(RowCount, 2).Value = item.Password;
                        worksheet.Cell(RowCount, 3).Value = item.Auth;
                        RowCount++;
                    }

                    using (var stream = new MemoryStream())
                    {
                        workbook.SaveAs(stream);
                        var content = stream.ToArray();
                        return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "users.xlsx");
                    }
                }
            }
            else
            {
                var url = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}{HttpContext.Request.Path}{HttpContext.Request.QueryString}";
                ReportsArea.AddReports(DateTime.Now, Convert.ToInt32(HttpContext.Session.GetString("EId")), HttpContext.Session.GetString("Auth"), url);

                return RedirectToAction("AccessDenied", "Access");
            }

        }

        public IActionResult Downloads()
        {
            return View();
        }
    }
}

