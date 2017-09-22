using System.Web.Mvc;

namespace HotelPro.Controllers
{
    public class EnvironmentController : Controller
    {
        //
        // GET: /Environment/

        public ActionResult Index()
        {
            return View("Index");
        }

        public ActionResult ViewInfo()
        {
            return View("Detail");
        }
    }
}
