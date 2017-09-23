using System.Web.Mvc;

namespace HotelPro.Controllers
{
    public class DishController : Controller
    {
        //
        // GET: /Dish/

        public ActionResult Index()
        {
            return View("Index");
        }

    }
}
