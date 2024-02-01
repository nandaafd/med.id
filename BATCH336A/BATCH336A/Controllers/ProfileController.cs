using Microsoft.AspNetCore.Mvc;
using BATCH336A.Models;
using BATCH336A.ViewModel;
using System.Diagnostics;
using System;
using System.Drawing.Printing;
using BATCH336A.AddOns;
namespace BATCH336A.Controllers
{
    public class ProfileController : Controller
    {
        private VMResponse response = new VMResponse();
        private readonly CourierModel profile;
        public IActionResult Index()
        {
            
            return View();
        }
    }
}
