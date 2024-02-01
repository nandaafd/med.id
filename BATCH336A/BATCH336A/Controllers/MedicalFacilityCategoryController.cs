using BATCH336A.Models;
using BATCH336A.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace BATCH336A.Controllers
{
    public class MedicalFacilityCategoryController : Controller
    {
        private readonly MedicalFacilityCategoryModel mefaca;
        private readonly MenuModel? menuModel;
        private readonly RoleModel role;
        private VMResponse response = new VMResponse();

        public MedicalFacilityCategoryController(IConfiguration _config, IWebHostEnvironment _webhost)
        {
            mefaca = new MedicalFacilityCategoryModel(_config);
            menuModel = new MenuModel(_config, _webhost);
            role = new RoleModel(_config);
        }
        public IActionResult Index(string? filter)
        {
            List<VMMMedicalFacilityCategory>? dataCategory = (filter == null) ? mefaca.GetAll() : mefaca.Search(filter);  
            ViewBag.Title = "Medical Facility Category";
            ViewBag.Filter = filter;
            ViewBag.Role = role.GetAll();
            ViewBag.Menu = menuModel.GetAll();
            return View(dataCategory);
        }
        public IActionResult Add() {
            ViewBag.Title = "Medical Category Facility";
            ViewBag.Role = role.GetAll();
            ViewBag.Menu = menuModel.GetAll();

            return View();
        }
        [HttpPost]
        public async Task<VMResponse> Add(VMMMedicalFacilityCategory data)
        {
            if (!ModelState.IsValid) {
                response.statusCode = System.Net.HttpStatusCode.BadRequest;
                response.data = null;
                response.message = "Request tidak valid";
                return response;
            }
            response = await mefaca.CreateAsync(data);
            return response;
        }

        public IActionResult Edit(int id)
        {
            VMMMedicalFacilityCategory? categoryEditById = mefaca.GetById(id);
            ViewBag.Filter = id;
            ViewBag.Title = "Edit Category";
            ViewBag.Role = role.GetAll();
            ViewBag.Menu = menuModel.GetAll();

            return View(categoryEditById);
        }
        [HttpPost]
        public async Task<VMResponse> Edit(VMMMedicalFacilityCategory data)
        {
            if (!ModelState.IsValid)
            {
                response.statusCode = System.Net.HttpStatusCode.BadRequest;
                response.data = null;
                response.message = "Request tidak valid";
                return response;
            }
            response = await mefaca.UpdateAsync(data);
            return response;
        }

        public IActionResult Delete(int id)
        {
            ViewBag.Title = "Delete Category";
            ViewBag.CatName = mefaca.GetById(id).Name;
            ViewBag.Role = role.GetAll();
            ViewBag.Menu = menuModel.GetAll();

            return View(id);
        }

        [HttpPost]
        public async Task<VMResponse> Delete(int id, int userId)
        {
            response = await mefaca.DeleteAsync(id, userId);
            return response;
        }
    }
}
