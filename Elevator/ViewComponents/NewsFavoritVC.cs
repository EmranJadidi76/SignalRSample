using DataLayer.DTO;
using Microsoft.AspNetCore.Mvc;
using Service.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Elevator.ViewComponents
{
    public class NewsFavoritVC : ViewComponent
    {

        private readonly NewsRepository _newsRepository;

        public NewsFavoritVC(NewsRepository newsRepository)
        {
            _newsRepository = newsRepository;
        }



        public IViewComponentResult Invoke()
        {
            return View(this._newsRepository.GetFavoritNews());
        }

    }
}
