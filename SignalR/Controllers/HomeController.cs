using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SignalR.Hubs;
using SignalR.Models;

namespace SignalR.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHubContext<ChatHub> hubContext;

        public HomeController(IHubContext<ChatHub> hubContext)
        {
            this.hubContext = hubContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Chat(string Email)
        {
            this.hubContext.Clients.All.SendAsync("ReciveMesage", $"{Email} is joined");

            return View();
        }

        
    }
}
