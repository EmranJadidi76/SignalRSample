using DataLayer.DTO;
using Microsoft.AspNetCore.Mvc;
using Service.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Elevator.ViewComponents
{
    public class LastNewsVC : ViewComponent
    {

        private readonly NewsRepository _newsRepository;

        public LastNewsVC(NewsRepository newsRepository)
        {
            _newsRepository = newsRepository;
        }



        public IViewComponentResult Invoke()
        {
            return View(this._newsRepository.Load<NewsDTO>().Skip(0).Take(10).ToList());
        }

    }
}
