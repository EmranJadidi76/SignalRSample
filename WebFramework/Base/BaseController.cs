using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Html;

namespace Elevator.Controllers
{
    public class BaseController : Controller
    {

        #region Fields

        /// <summary>
        /// شماره صفحه فعلی
        /// </summary>
        public int PageNumber { get; set; } = 0;


        /// <summary>
        /// تعداد آیتم برای هر صفحه
        /// </summary>
        public int PageSize { get; set; } = 10;


        /// <summary>
        /// تعداد کل آیتم‌ها
        /// </summary>
        public int TotalNumber { get; set; }


        /// <summary>
        /// تعداد صفحات
        /// </summary>
        public int PageCount { get; set; }


        #endregion
        

        public BaseController()
        {
            
        }

        /// <summary>
        /// قبل از اجرا هر اکشنی این تابع اجرا میشود تا بتواند اطلاعات مورد نیاز که 
        /// از صفحه پاس داده شده است اینجا مقداردهی کند
        /// </summary>
        /// <param name="context"></param>
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

           
            this.PageNumber = Request.Query["pageNumber"].Count != 0 ? Convert.ToInt32(Request.Query["pageNumber"][0]) : 0;

        }



        /// <summary>
        /// بعد از هر اکشنی این تابع صدا زده می‌شود
        /// </summary>
        /// <param name="context"></param>
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            base.OnActionExecuted(context);


            ViewBag.pageNumber = this.PageNumber;
            ViewBag.pageSize = this.PageSize;
            

            ViewBag.pageCount = this.TotalNumber / this.PageSize;
            ViewBag.potalNumber = this.TotalNumber;
        }


    }
}
