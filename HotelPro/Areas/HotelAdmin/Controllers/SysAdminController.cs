using BLL;
using Models;
using System.Web.Mvc;
using System.Web.Security;

namespace HotelPro.Areas.HotelAdmin.Controllers
{
    public class SysAdminController : Controller
    {
        //
        // GET: /HotelAdmin/SysAdmin/

        public ActionResult Index()
        {
            return View("AdminLogin");
        }

        [Authorize]
        public ActionResult AdminMain()
        {
            return View();
        }
        // Admin login
        public ActionResult LoginUser(SysAdmin objSysAdmin)
        {
            // Get parameter to validate
            if (ModelState.IsValid)
            {
                //调用BLL业务逻辑层登录方法
                objSysAdmin = new SysAdminManager().Login(objSysAdmin);
                //if(登录成功 )1.存值到session 2.当前登录用户发放票据 3.返回主页()
                if (objSysAdmin != null)
                {
                    Session["currentAdmin"] = objSysAdmin.LoginName;
                    FormsAuthentication.SetAuthCookie(objSysAdmin.LoginName, true);
                    return View("AdminMain");
                }
                else
                {
                    return Content("<script>alert('用户名或密码错误!');location.href=" + Url.Action("Index") + "</script>");
                }
            }
            else
            {
                return View("AdminLogin");
            }
            
        }

        /// <summary>
        /// Exit system
        /// </summary>
        /// <returns></returns>
        public ActionResult ExitSystem()
        {
            Session["currentAdmin"] = null;
            Session.Abandon();
            FormsAuthentication.SignOut();
            return View("AdminLogin");
        }

        public ActionResult SuccessedLogin()
        {
            return View("AdminMain");
        }

        public ActionResult AddDish()
        {
            return View();
        }
    }
}