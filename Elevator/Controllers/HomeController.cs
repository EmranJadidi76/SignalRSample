using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Elevator.Models;
using Service.Repos;
using Microsoft.AspNetCore.Authorization;

namespace Elevator.Controllers
{
    public class HomeController : BaseController
    {

        private readonly SlideShowRepository _slideShowRepository;
        private readonly NewsGroupRepository _newsGroupRepository;

        public HomeController(SlideShowRepository slideShowRepository,
            NewsGroupRepository newsGroupRepository)
        {
            _slideShowRepository = slideShowRepository;
            _newsGroupRepository = newsGroupRepository;
        }


        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";
            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error404()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }



        public IActionResult testMobasher()
        {
            
            ViewBag.MQuery = this._newsGroupRepository.ddd();
            return View();
        }


    }
}
