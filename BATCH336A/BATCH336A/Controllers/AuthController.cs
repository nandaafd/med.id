using BATCH336A.DataModel;
using BATCH336A.Models;
using BATCH336A.ViewModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;

namespace BATCH336A.Controllers
{
    public class AuthController : Controller
    {
        private readonly UserModel? userModel;
        private readonly BiodataModel biodataModel;
        private readonly RoleModel roleModel;
        private VMResponse? response = new VMResponse();
        public AuthController(IConfiguration _config)
        {
            userModel = new UserModel(_config);
            biodataModel = new BiodataModel(_config);
            roleModel = new RoleModel(_config);
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public VMResponse Login(VMMUser data)
        {
            VMResponse? response = userModel!.GetByEmail(data.Email!);

            if (response != null)
            {
                if (response.statusCode == System.Net.HttpStatusCode.OK)
                {
                    VMMRole? role = new VMMRole();
                    VMMUser? user = JsonConvert.DeserializeObject<VMMUser>(JsonConvert.SerializeObject(response.data));
                    VMMBiodatum? biodata = biodataModel.GetById((int)user!.BiodataId!);
                    if (user.RoleId == null || user.RoleId == 0)
                    {
                        role.Name = "";
                        user.RoleId = 0;
                    }
                    else
                    {
                        role = roleModel.GetById((int)user.RoleId!);
                    }

                    if (user.IsLocked == null || user.IsLocked == false)
                    {
                        if (data.Password == user!.Password)
                        {
                            HttpContext.Session.SetInt32("userId", (int)user.Id!);
                            HttpContext.Session.SetString("userFullname", biodata.Fullname);
                            HttpContext.Session.SetString("userEmail", user.Email!);
                            HttpContext.Session.SetInt32("userBiodataId", (int)user.BiodataId!);
                            HttpContext.Session.SetInt32("userRoleId", (int)user.RoleId);
                            HttpContext.Session.SetString("userRoleName", role.Name!);

                            var claims = new List<Claim>
                            {
                                new Claim(ClaimTypes.Role, user.RoleId.ToString()!),
                            };
                            var identity = new ClaimsIdentity(claims, "CookieAuth");
                            var principal = new ClaimsPrincipal(identity);
                            HttpContext.SignInAsync("CookieAuth", principal);
                            HttpContext.Session.SetString("successMsg", $"Hallo, {HttpContext.Session.GetString("custFullname")}");
                            user.LoginAttempt = 0;
                            user.LastLogin = DateTime.Now;
                            userModel.Update(user);

                        }
                        else
                        {
                            response.statusCode = System.Net.HttpStatusCode.NoContent;

                            if (user.LoginAttempt == null)
                            {
                                user.LoginAttempt = 1;
                                userModel.Update(user);
                            }
                            else
                            {
                                user.LoginAttempt++;
                                userModel.Update(user);
                            }

                            if (user.LoginAttempt >= 5)
                            {
                                user.IsLocked = true;
                                userModel.Update(user);
                            }
                        }
                    }
                    else
                    {
                        response.statusCode = System.Net.HttpStatusCode.Forbidden;
                    }
                }
                else
                {
                    response.statusCode = System.Net.HttpStatusCode.NotFound;
                }
            }

            return response;
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            HttpContext.SignOutAsync("CookieAuth");

            return RedirectToAction("Index", "Home");
        }
    }
}
