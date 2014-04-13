using iTEC.Services.AccessLayer;
using iTEC.Services.AccessLayer.DelightService;
using iTEC.WebApplication.Models;
using Microsoft.Ajax.Utilities;
using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Hosting;
using System.Web.Http;
using Excel = Microsoft.Office.Interop.Excel;

namespace iTEC.WebApplication.Controllers
{
    public class ChartModel
    {
        public ReportChart Report { get; set; }
        public String Url { get; set; }
    }

    public class BugetModel
    {
        public Int32 Money { get; set; }
    }

    public class ReportController : SafeApiController
    {
        [HttpPost]
        public HttpResponseMessage GetProductVoteReport()
        {
            return SafeAction(() =>
            {
                IDelightServices service = new DelightServices();
                var report = service.GetProductVoteReport();
                if (report == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NoContent);
                }

                var file = DateTime.Now.Ticks.ToString();
                var path = Path.Combine(HostingEnvironment.MapPath("~/Content/Files"), file) + ".xls";
                GenerateReport(path, report);

                var response = Request.CreateResponse<ChartModel>(HttpStatusCode.OK, new ChartModel()
                {
                    Report = report,
                    Url = Path.Combine("/Content/Files", file) + ".xls"
                });
                return response;
            });
        }

        [HttpPost]
        public HttpResponseMessage GetWelcomeMessage()
        {
            return SafeAction(() =>
            {
                IDelightServices service = new DelightServices();
                var message = service.GetMessage();
                if (message == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NoContent);
                }
                var response = Request.CreateResponse<MessageDTO>(HttpStatusCode.OK, message);
                return response;
            });
        }

        [HttpPost]
        public HttpResponseMessage SaveWelcomeMessage(MessageDTO message)
        {
            return SafeAction(() =>
            {
                IDelightServices service = new DelightServices();
                service.SaveMessage(message);
                var response = Request.CreateResponse<Boolean>(HttpStatusCode.OK, true);
                return response;
            });
        }

        [HttpGet]
        public HttpResponseMessage GenerateExcel()
        {
            return SafeAction(() =>
            {
                var response = Request.CreateResponse<Boolean>(HttpStatusCode.OK, true);
                return response;
            });
        }

        [NonAction]
        private void GenerateReport(String file, ReportChart report)
        {
            File.WriteAllText(file, "");
            return;

            object misValue = Missing.Value;

            var xlApp = new Excel.Application();
            var xlWorkBook = xlApp.Workbooks.Add(misValue);

            var xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
            xlWorkSheet.Cells[1, 1] = "Category name";
            xlWorkSheet.Cells[1, 2] = "Category name";
            
            xlWorkBook.SaveAs(file, Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
            xlWorkBook.Close(true, misValue, misValue);
            xlApp.Quit();

            ReleaseObject(xlWorkSheet);
            ReleaseObject(xlWorkBook);
            ReleaseObject(xlApp);
        }

        [NonAction]
        private void ReleaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception)
            {
                obj = null;
            }
            finally
            {
                GC.Collect();
            }
        }

        [HttpPost]
        public HttpResponseMessage StartSession()
        {
            return SafeAction(() =>
            {
                IDelightServices service = new DelightServices();
                service.StartSession();
                var response = Request.CreateResponse<Boolean>(HttpStatusCode.OK, true);
                return response;
            });
        }

        [HttpPost]
        public HttpResponseMessage GetBudgetReport([FromBody]BugetModel model)
        {
            return SafeAction(() =>
            {
                IDelightServices service = new DelightServices();
                var budget = service.GetBudget(model.Money);

                var report = new BudgetReportChart();
                report.Generate(budget.ToArray(), model.Money);

                var file = DateTime.Now.Ticks.ToString();
                var path = Path.Combine(HostingEnvironment.MapPath("~/Content/Files"), file) + ".xls";
                GenerateReport(path, report);

                var response = Request.CreateResponse<ChartModel>(HttpStatusCode.OK, new ChartModel()
                {
                    Report = report,
                    Url = Path.Combine("/Content/Files", file) + ".xls"
                });
                return response;
            }, model);
        }
    }

    public class BudgetReportChart : AreasplineReportChart
    {
        public void Generate(BudgetDTO[] budget, Int32 money)
        {
            Chart = new ChartType() { Type = "areaspline" };
            Title = new Title() { Text = "Budget report" };
            Subtitle = new Title() { Text = "Current session" };
            XAxis = new XAxis() { Categories = budget.Select(x => x.Name).ToArray() };
            YAxis = new YAxis() { Title = new Title() { Text = "Budget" } };
            Tooltip = new Tooltip() { Shared = true, ValueSuffix = " money" };
            Series = new ChartSeries[] {
                new ChartSeries() { Name = "Current", Data = budget.Select(x => (Int32)(x.Value)).ToArray() }
            };
        }
    }
}
