using BATCH336A.AddOns;
using BATCH336A.DataAccess;
using BATCH336A.DataModel;
using BATCH336A.Models;
using BATCH336A.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using PuppeteerSharp;
using PuppeteerSharp.Media;

namespace BATCH336A.Controllers
{
    public class PrintPresciptionController : Controller
    {
        private readonly HistoryCustomerAppointmentModel history;
        private readonly IRazorViewEngine _razorViewEngine;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IServiceProvider _serviceProvider;
        private readonly ITempDataProvider _tempDataProvider;

        public PrintPresciptionController(IConfiguration _config, IRazorViewEngine razorViewEngine, IHttpContextAccessor httpContextAccessor, ITempDataProvider tempDataProvider, IServiceProvider serviceProvider) 
        {
            history = new HistoryCustomerAppointmentModel(_config);
            _razorViewEngine = razorViewEngine;
            _httpContextAccessor = httpContextAccessor;
            _tempDataProvider = tempDataProvider;
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        }

        public IActionResult Print(int id)
        {
            VMMHistoryCustomer data = history.GetById(id);
            return View(data);
        }
        private async Task<string> RenderViewAsync(string viewName, object model)
        {
            var httpContext = _httpContextAccessor.HttpContext ?? new DefaultHttpContext { RequestServices = _serviceProvider };
            var actionContext = new ActionContext(httpContext, new RouteData(), new ActionDescriptor());

            using (var sw = new StringWriter())
            {
                var viewResult = _razorViewEngine.FindView(actionContext, viewName, false);

                if (viewResult.View == null)
                {
                    throw new ArgumentNullException($"{viewName} does not match any available view");
                }

                var viewDictionary = new ViewDataDictionary(new EmptyModelMetadataProvider(), new ModelStateDictionary())
                {
                    Model = model
                };
                var viewContext = new ViewContext(
                    actionContext,
                    viewResult.View,
                    viewDictionary,
                    new TempDataDictionary(actionContext.HttpContext, _tempDataProvider),
                    sw,
                    new HtmlHelperOptions()
                );

                await viewResult.View.RenderAsync(viewContext);
                return sw.ToString();
            }
        }

        public async Task<IActionResult> GetPdf(int id)
        {
            VMMHistoryCustomer? data = history.GetById(id); // Dapatkan data
            var htmlContent = await RenderViewAsync("HistoryAppointmentCustomer/Print", data); // Render view ke HTML
            if(data != null)
            {
                foreach (VMTPrescription item in data.Prescriptions)
                {
                    item.PrintAttempt = item.PrintAttempt + 1;
                    if (HttpContext.Session.GetInt32("userId") != null || HttpContext.Session.GetInt32("userId") != 0)
                    {
                        item.ModifiedBy = HttpContext.Session.GetInt32("userId");
                    }
                    else
                    {
                        item.ModifiedBy = 0;
                    }
                    history?.Update(item);
                }
            }
            else
            {
                HttpContext.Session.SetString("errMsg", "Gagal mencetak resep");
                return RedirectToAction("History","HistoryAppointmentCustomer");
            }


            var fetcher = new BrowserFetcher();
            await fetcher.DownloadAsync();

            using (var browser = await Puppeteer.LaunchAsync(new LaunchOptions()))
            {
                using (var page = await browser.NewPageAsync())
                {
                    await page.SetContentAsync(htmlContent); // Gunakan HTML yang dihasilkan

                    var pdfOptions = new PdfOptions
                    {
                        Format = PaperFormat.A4
                    };

                    var pdf = await page.PdfDataAsync(pdfOptions);
                    return new FileContentResult(pdf, "application/pdf");
                }
            }
        }

    }
}
