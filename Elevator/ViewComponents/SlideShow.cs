using Microsoft.AspNetCore.Mvc;
using Service.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Elevator.ViewComponents
{
    public class SlideShow : ViewComponent
    {

        private readonly SlideShowRepository _slideShowRepository;

        public SlideShow(SlideShowRepository slideShowRepository)
        {
            _slideShowRepository = slideShowRepository;
        }


        public IViewComponentResult Invoke()
        {
            return View(_slideShowRepository.GetLastItems());
        }

    }
}
