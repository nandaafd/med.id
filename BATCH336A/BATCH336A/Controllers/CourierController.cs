using Microsoft.AspNetCore.Mvc;
using BATCH336A.Models;
using BATCH336A.ViewModel;
using System.Diagnostics;
using System;
using System.Drawing.Printing;
using BATCH336A.AddOns;
using Microsoft.EntityFrameworkCore;



namespace BATCH336A.Controllers
{
    public class CourierController : Controller
    {
        private VMResponse response = new VMResponse();
        private readonly CourierModel courier;
        private readonly int pageSize;
        private readonly MenuModel? menuModel;
        private readonly RoleModel role;
        public CourierController(IConfiguration _config, IWebHostEnvironment _webHostEnv)
        {
            courier = new CourierModel(_config);
            pageSize = int.Parse(_config["PageSize"]);
            menuModel = new MenuModel(_config, _webHostEnv);
            role = new RoleModel(_config);
        }

        //public IActionResult Index(string? filter)
        //{
        //    List<VMMCourier>? data = new List<VMMCourier>();
        //    if (filter == null)
        //    {
        //        data = courier.GetAll();
        //    }
        //    else
        //    {
        //        data = courier.GetBy(filter);
        //    }
        //    ViewBag.Title = "Courier Index";
        //    ViewBag.Filter = filter;
        //    return View(data);
        //}

        public IActionResult Index(string? filter, string? orderBy, int? pageNumber, int? currPageSize)
        {
            List<VMMCourier>? data = new List<VMMCourier>();
            if (filter == null)
            {
                data = courier.GetAll();
            }
            else
            {
                data = courier.GetBy(filter);
            }

            ViewBag.PageSize = (currPageSize ?? pageSize);
            ViewBag.Title = "Courier Index";
            ViewBag.Filter = filter;
            ViewBag.Role = role.GetAll();
            ViewBag.Menu = menuModel.GetAll();

            return View(Pagination<VMMCourier>.Create(data, pageNumber ?? 1, ViewBag.PageSize));
        }

        public IActionResult Add()
        {
            ViewBag.Title = "Create New Courier";
            return View();
        }

        [HttpPost]
        public async Task<VMResponse> Add(VMMCourier data)
        {
            response = await courier.CreateAsync(data);
            return response;
        }
        public IActionResult Edit(int id)
        {
            VMMCourier? data = courier.GetById(id);
            ViewBag.Title = "Edit Courier";
            return View(data);
        }

        [HttpPost]
        public async Task<VMResponse> Edit(VMMCourier data)
        {
            response = await courier.UpdateAsync(data);
            return response;
        }
        public IActionResult Delete(long id)
        {
            VMMCourier data = courier.GetById(id);
            ViewBag.Title = "Delete Confirmation";
           
            return View(data);
        }

        [HttpPost]
        public async Task<VMResponse> Delete(int id, int DeletedBy)
        {
            response = await courier.DeleteAsync(id, DeletedBy);
            return response;
        }
        
    }
}
