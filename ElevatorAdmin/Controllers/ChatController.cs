using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Core.CustomAttributes;
using Core.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Repos.User;
using WebFramework.Base;

namespace ElevatorAdmin.Controllers
{
    [Authorize]
    public class ChatController : Controller
    {
        private readonly UserRepository _userRepository;
        public ChatController(UsersAccessRepository usersAccessRepository
            ,UserRepository userRepository) 
        {
            _userRepository = userRepository;
        }

        [AllowAccess]
        public IActionResult Index()
        {
            var userId = int.Parse(User.Identity.FindFirstValue(ClaimTypes.NameIdentifier));
            var model = _userRepository.TableNoTracking.Where(a=>a.Id != userId).ToList();
            return View(model);
        }

        [AllowAccess]
        public IActionResult StartChat(int id)
        {
            ViewBag.UserId = id;

            return View();
        }
        [AllowAccess]
        public IActionResult MyPartial(string message,string User,string ProfilePic)
        {
            ViewBag.User = User;
            ViewBag.Profile = ProfilePic;

            return PartialView("_myPartial",message);
        }
        [AllowAccess]
        public IActionResult RecivePartial(string message, string User, string ProfilePic)
        {

            ViewBag.User = User;
            ViewBag.Profile = ProfilePic;
            return PartialView("_recivePartial",message);
        }
    }
}