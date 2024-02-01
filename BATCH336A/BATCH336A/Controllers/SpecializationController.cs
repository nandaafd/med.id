using BATCH336A.AddOns;
using BATCH336A.DataModel;
using BATCH336A.Models;
using BATCH336A.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Drawing.Printing;

namespace BATCH336A.Controllers
{
    public class SpecializationController : Controller
    {
        private VMResponse response = new VMResponse();
        private SpecializationModel specialization;
        private readonly int pageSize;
        private readonly MenuModel? menuModel;
        private readonly RoleModel role;

        public SpecializationController(IConfiguration _config, IWebHostEnvironment environment)
        {
            specialization = new SpecializationModel(_config);
            pageSize = int.Parse(_config["PageSize"]);
            menuModel = new MenuModel(_config, environment);
            role = new RoleModel(_config);
        }
        public IActionResult Index(string? filter, int? pageNumber, int? currPageSize)
        {
            List<VMMSpecialization>? data;
            if (filter == null)
            {
                data = specialization.GetAll();
            }
            else
            {
                data = specialization.GetBy(filter);
            }

            if (data == null)
            {
                data = new List<VMMSpecialization>();
            }

            ViewBag.Title = "Spesialisasi";
            ViewBag.PageSize = (currPageSize ?? pageSize);
            ViewBag.Filter = filter;
            ViewBag.Role = role.GetAll();
            ViewBag.Menu = menuModel.GetAll();

            return View(Pagination<VMMSpecialization>.Create(data, pageNumber ?? 1, ViewBag.PageSize));
        }

        public IActionResult Add()
        {
            ViewBag.Title = "Create New Specialization";
            return View();
        }

        [HttpPost]
        public async Task<VMResponse> Add(VMMSpecialization data)
        {
            response = await specialization.CreateAsync(data);
            return response;
        }
        public IActionResult Edit(int id)
        {
            VMMSpecialization? data = specialization.GetById(id);
            ViewBag.Title = "Edit Specialization";
            return View(data);
        }

        [HttpPost]
        public async Task<VMResponse> Edit(VMMSpecialization data)
        {
            response = await specialization.UpdateAsync(data);
            return response;
        }
        public IActionResult Delete(long id)
        {
            VMMSpecialization data = specialization.GetById(id);
            ViewBag.Title = "Delete Confirmation";
            return View(data);
        }

        [HttpPost]
        public async Task<VMResponse> Delete(long id, long userId)
        {
            response = await specialization.DeleteAsync(id, userId);
            return response;
        }
    }
}
