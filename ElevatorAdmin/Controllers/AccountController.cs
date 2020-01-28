using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Core.CustomAttributes;
using Core.Utilities;
using DataLayer.Entities.Users;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Service.Repos.User;
using WebFramework.Base;

namespace ElevatorAdmin.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly UserRepository _userRepository;
        private readonly UsersRoleRepository _usersRoleRepository;
        private readonly SignInManager<Users> _signInManager;
        private readonly UserManager<Users> _userManager;
        private readonly RoleRepository _roleRepository;
        

        public AccountController(SignInManager<Users> signInManager,
            UserManager<Users> userManager,
            UserRepository userRepository,
            UsersRoleRepository usersRoleRepository,
            RoleRepository roleRepository,
            UsersAccessRepository usersAccessRepository
            )
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _userRepository = userRepository;
            _usersRoleRepository = usersRoleRepository;
            _roleRepository = roleRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [AllowAccess]
        [AllowAnonymous()]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAccess]
        [AllowAnonymous()]
        public async Task<IActionResult> Login(string userName, string password)
        {
            var model = _userRepository.TableNoTracking.FirstOrDefault(a => a.UserName == userName);

            if (model == null)
            {
                TempData.AddResult(SweetAlertExtenstion.Error("کاربری با این نام کاربری یافت نشد!"));
                return RedirectToAction("Login");
            }

            if (model.IsActive == false)
            {
                TempData.AddResult(SweetAlertExtenstion.Error("شما فعال نیستید!"));
                return RedirectToAction("Login");
            }

            var result = await _signInManager.PasswordSignInAsync(model, password, true, false);

            if (result.Succeeded)
            {
                //await _userRepository.SetUserClaims(userName);
                return RedirectToAction("Index", "Chat");
            }
            TempData.AddResult(SweetAlertExtenstion.Error("کلمه عبور یا نام کاربری نادرست است"));
            return RedirectToAction("Index");
        }

        [AllowAccess]
        [AllowAnonymous()]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Redirect("/");
        }

    }
}
