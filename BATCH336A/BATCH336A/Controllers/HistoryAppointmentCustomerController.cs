using BATCH336A.AddOns;
using BATCH336A.DataAccess;
using BATCH336A.DataModel;
using BATCH336A.Models;
using BATCH336A.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using PuppeteerSharp;
using PuppeteerSharp.Media;
using System.Data;

namespace BATCH336A.Controllers
{
    public class HistoryAppointmentCustomerController : Controller
    {
        public HistoryCustomerAppointmentModel history;
        private readonly MenuModel? menuModel;
        private readonly RoleModel role;

        private readonly int pageSize;

        public HistoryAppointmentCustomerController(IConfiguration _config, IWebHostEnvironment _webHostEnv) 
        {
            history = new HistoryCustomerAppointmentModel(_config);
            menuModel = new MenuModel(_config, _webHostEnv);
            role = new RoleModel(_config);
            pageSize = int.Parse(_config["PageSize"]); 
        }
        public IActionResult Index()
        {
            ViewBag.Role = role.GetAll() != null ? role.GetAll() : new List<VMMRole>();
            ViewBag.Menu = menuModel.GetAll() != null ? menuModel.GetAll() : new List<VMMMenu>();
            return View();
        }
        public IActionResult History(string? filter, int? pageNumber, int? currPageSize, string? orderBy, string? sortBy)
        {
            int? biodataId = HttpContext.Session.GetInt32("userBiodataId");
            List<VMMHistoryCustomer>? data;
            if (biodataId != null)
            {
                data = (filter == null)
                    ? history.GetAll((long)biodataId)
                    : history.GetByFilter(filter).Where(h => h.ParentBiodata == biodataId || h.BiodataId == biodataId).ToList();
            }
            else
            {
                data = data = new List<VMMHistoryCustomer>();
            }
            if (data != null)
            {
                if (sortBy == "desc")
                {
                    switch (orderBy)
                    {
                        case "kedatangan":
                            data = data?.OrderByDescending(h => h.AppointmentDate).ToList();
                            break;
                        case "nama":
                            data = data?.OrderByDescending(h => h.AppointmentCustomerName).ToList();
                            break;
                        case "createon":
                            data = data?.OrderByDescending(h => h.AppointmentCreateOn).ToList();
                            break;
                        default:
                            data = data?.OrderByDescending(h => h.AppointmentDate).ToList();
                            break;
                    }
                }
                else
                {
                    switch (orderBy)
                    {
                        case "kedatangan":
                            data = data?.OrderBy(h => h.AppointmentDate).ToList();
                            break;
                        case "nama":
                            data = data?.OrderBy(h => h.AppointmentCustomerName).ToList();
                            break;
                        case "createon":
                            data = data?.OrderBy(h => h.AppointmentCreateOn).ToList();
                            break;
                        default:
                            data = data?.OrderBy(h => h.AppointmentDate).ToList();
                            break;
                    }
                }
            }
            else
            {
                data = new List<VMMHistoryCustomer>();
            }
            
            ViewBag.Role = role.GetAll() != null ? role.GetAll() : new List<VMMRole>();
            ViewBag.Menu = menuModel.GetAll() != null ? menuModel.GetAll() : new List<VMMMenu>();
            ViewBag.Filter = filter != null ? filter : "";
            ViewBag.OrderBy = orderBy != null ? orderBy : "";
            ViewBag.PageSize = currPageSize ?? pageSize;
            ViewBag.SortBy = sortBy != null ? sortBy : "";
            ViewBag.ErrMsg = HttpContext.Session.GetString("errMsg");

            return View(Pagination<VMMHistoryCustomer>.Create(data, pageNumber ?? 1, currPageSize ?? pageSize) );
        }

        public IActionResult Print(int id)
        {
            VMMHistoryCustomer data = history.GetById(id);
            return View(data);
        }
        

    }
}
