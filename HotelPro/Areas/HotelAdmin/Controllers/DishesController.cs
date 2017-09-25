using BLL;
using Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace HotelPro.Areas.HotelAdmin.Controllers
{
    public class DishesController : Controller
    {
        //
        // GET: /HotelAdmin/Dishes/

        [Authorize]
        public ActionResult Index()
        {
            List<DishCategory> objDishCategories = new DishManager().GetDishCategories();
            List<SelectListItem> items = new List<SelectListItem>();
            foreach (DishCategory objDishCategory in objDishCategories)
            {
                items.Add(new SelectListItem()
                {
                    Text = objDishCategory.CategoryName,
                    Value = objDishCategory.CategoryId.ToString()
                });
            }
            items[0].Selected = true;
            ViewBag.DishCategory = items;
            
            return View("DishesPublish");
        }

        [Authorize]
        public ActionResult DoAddDish()
        {
            return View("DishesPublish");
        }
    }
}
