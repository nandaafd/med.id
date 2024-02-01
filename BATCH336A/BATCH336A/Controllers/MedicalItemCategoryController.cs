using BATCH336A.AddOns;
using BATCH336A.Models;
using BATCH336A.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace BATCH336A.Controllers
{
    public class MedicalItemCategoryController : Controller
    {
        private VMResponse response = new VMResponse();
        private MedicalItemCategoryModel medicalItemCat;
        private readonly int pageSize;
        private readonly MenuModel? menuModel;
        private readonly RoleModel role;

        public MedicalItemCategoryController(IConfiguration _config, IWebHostEnvironment environment)
        {
            medicalItemCat = new MedicalItemCategoryModel(_config);
            pageSize = int.Parse(_config["PageSize"]);
            menuModel = new MenuModel(_config, environment);
            role = new RoleModel(_config);
        }
        public IActionResult Index(string? filter, int? pageNumber, int? currPageSize)
        {
            List<VMMMedicalItemCategory>? data;
            if (filter == null)
            {
                data = medicalItemCat.GetAll();
            }
            else
            {
                data = medicalItemCat.GetBy(filter);
            }

            if (data == null)
            {
                data = new List<VMMMedicalItemCategory>();
            }

            ViewBag.Title = "Kategori Produk Kesehatan";
            ViewBag.PageSize = (currPageSize ?? pageSize);
            ViewBag.Filter = filter;
            ViewBag.Role = role.GetAll();
            ViewBag.Menu = menuModel.GetAll();

            return View(Pagination<VMMMedicalItemCategory>.Create(data, pageNumber ?? 1, ViewBag.PageSize));
        }

        public IActionResult Add()
        {
            ViewBag.Title = "Tambah Kategori Baru";
            return View();
        }

        [HttpPost]
        public async Task<VMResponse> Add(VMMMedicalItemCategory data)
        {
            response = await medicalItemCat.CreateAsync(data);
            return response;
        }
        public IActionResult Edit(int id)
        {
            VMMMedicalItemCategory? data = medicalItemCat.GetById(id);
            ViewBag.Title = "Edit Kategori Produk Kesehatan";
            return View(data);
        }

        [HttpPost]
        public async Task<VMResponse> Edit(VMMMedicalItemCategory data)
        {
            response = await medicalItemCat.UpdateAsync(data);
            return response;
        }
        public IActionResult Delete(long id)
        {
            VMMMedicalItemCategory? data = medicalItemCat.GetById(id);
            ViewBag.Title = "Delete Confirmation";
            return View(data);
        }

        [HttpPost]
        public async Task<VMResponse> Delete(long id, long userId)
        {
            response = await medicalItemCat.DeleteAsync(id, userId);
            return response;
        }
    }
}
