using BATCH336A.Models;
using BATCH336A.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace BATCH336A.Controllers
{
    public class EducationLevelController : Controller
    {
        private readonly EducationLevelModel edulevel;
        private readonly MenuModel? menuModel;
        private readonly RoleModel role;
        private VMResponse response = new VMResponse();
        public EducationLevelController(IConfiguration _config, IWebHostEnvironment _webhost)
        {
            edulevel = new EducationLevelModel(_config);
            menuModel = new MenuModel(_config, _webhost);
            role = new RoleModel(_config);
        }
        public IActionResult Index(string? filter)
        {
            List<VMEducationLevel>? dataCategory = (filter == null) ? edulevel.GetAll() : edulevel.Search(filter);
            ViewBag.Title = "Education Level";
            ViewBag.Filter = filter;
            ViewBag.Role = role.GetAll();
            ViewBag.Menu = menuModel.GetAll();
            return View(dataCategory);
        }
        public IActionResult Add()
        {
            ViewBag.Title = "Education Level";
            ViewBag.Role = role.GetAll();
            ViewBag.Menu = menuModel.GetAll();

            return View();
        }
        [HttpPost]
        public async Task<VMResponse> Add(VMEducationLevel data)
        {
            if (!ModelState.IsValid)
            {
                response.statusCode = System.Net.HttpStatusCode.BadRequest;
                response.data = null;
                response.message = "Request tidak valid";
                return response;
            }
            response = await edulevel.CreateAsync(data);
            return response;
        }

        public IActionResult Edit(int id)
        {
            VMEducationLevel? categoryEditById = edulevel.GetById(id);
            ViewBag.Filter = id;
            ViewBag.Title = "Edit Education Level";
            ViewBag.Role = role.GetAll();
            ViewBag.Menu = menuModel.GetAll();

            return View(categoryEditById);
        }
        [HttpPost]
        public async Task<VMResponse> Edit(VMEducationLevel data)
        {
            if (!ModelState.IsValid)
            {
                response.statusCode = System.Net.HttpStatusCode.BadRequest;
                response.data = null;
                response.message = "Request tidak valid";
                return response;
            }
            response = await edulevel.UpdateAsync(data);
            return response;
        }

        public IActionResult Delete(int id)
        {
            ViewBag.Title = "Delete Category";
            ViewBag.EduName = edulevel.GetById(id).Name;
            ViewBag.Role = role.GetAll();
            ViewBag.Menu = menuModel.GetAll();

            return View(id);
        }

        [HttpPost]
        public async Task<VMResponse> Delete(int id, int userId)
        {
            response = await edulevel.DeleteAsync(id, userId);
            return response;
        }
    }
}
