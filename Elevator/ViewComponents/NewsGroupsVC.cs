using DataLayer.DTO;
using Microsoft.AspNetCore.Mvc;
using Service.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Elevator.ViewComponents
{
    public class NewsGroupsVC : ViewComponent
    {

        private readonly NewsGroupRepository _newsGroupRepository;

        public NewsGroupsVC(NewsGroupRepository newsGroupRepository)
        {
            _newsGroupRepository = newsGroupRepository;
        }



        public IViewComponentResult Invoke()
        {
            return View(this._newsGroupRepository.Load<NewsGroupDTO>());
        }

    }
}
