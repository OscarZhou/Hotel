using BLL;
using Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace HotelPro.Controllers
{
    public class CompanyInfoController : Controller
    {
        //
        // GET: /CompanyInfo/

        public ActionResult Index()
        {
            #region News dynamically query
            List<News> listNews = new NewsManager().GetNews(5);
            ViewBag.list = listNews;
            #endregion

            #region featured dishes dynamically query

            List<Dish> listDish = new DishManager().GetDishes(4);
            ViewBag.dish = listDish;
            #endregion
            
            return View("Index");
        }

        public ActionResult Introduction()
        {
            return View("Introduction");
        }

        public ActionResult Environment()
        {
            return View("Environment");
        }

        public ActionResult EnvironmentDetail()
        {
            return View("EnvironmentDetail");
        }

        public ActionResult AboutUs()
        {
            return View("AboutUs");
        }
}
}
