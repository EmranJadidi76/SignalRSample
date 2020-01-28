using DataLayer.DTO;
using Microsoft.AspNetCore.Mvc;
using Service.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Elevator.ViewComponents
{
    public class NewsTagsVC : ViewComponent
    {

        private readonly NewsTagRepository _newsTagRepository;

        public NewsTagsVC(NewsTagRepository newsTagRepository)
        {
            _newsTagRepository = newsTagRepository;
        }



        public IViewComponentResult Invoke()
        {
            return View(this._newsTagRepository.Load<NewsTagDTO>().Skip(0).Take(15).ToList());
        }

    }
}
