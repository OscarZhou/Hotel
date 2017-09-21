using System.Web.Mvc;

namespace HotelPro.Controllers
{
    public class NewsController : Controller
    {
        //
        // GET: /News/

        public ActionResult Index()
        {
            return View("Index");
        }

    }
}
