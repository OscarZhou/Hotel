using BLL;
using HotelPro.Models;
using Models;
using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Webdiyer.WebControls.Mvc;

namespace HotelPro.Areas.HotelAdmin.Controllers
{
    public class DishesController : Controller
    {
        //
        // GET: /HotelAdmin/Dishes/
        public ActionResult DishesPublish()
        {
            return View("DishesPublish");
        }

        /// <summary>
        /// publish dish
        /// </summary>
        /// <param name="objDish"></param>
        /// <param name="dishImage"></param>
        /// <returns></returns>
        [Authorize]
        public ActionResult DoAddDish(Dish objDish, HttpPostedFileBase dishImage)
        {
            //【1】判断文件是否上传成功（文件大小，文件名重名）
            try
            {
                //判断是否有文件
                if (dishImage != null && dishImage.FileName != "")
                {
                    //判断文件大小是否符合要求
                    double fileLength = dishImage.ContentLength / (1024.0 * 1024.0);
                    if (fileLength > 2.0)
                    {
                        return
                            Content("<script>alert('图片最大不能超过2mb');location.href='" + Url.Action("DishesPublish") +
                                    "'</script>");
                    }
                    //获取文件名/重命名
                    string fileName = dishImage.FileName;
                    fileName = DateTime.Now.ToString("yyyyMMddmmhhss") + ".png";
                    objDish.DishImage = fileName;
                    //【2】调用BLL进行内容插入到数据库成功
                    int res = new DishManager().AddDish(objDish);
                    if (res > 0)
                    {
                        //【3】成功上传图片到项目文件底下
                        string filePath = Server.MapPath("~/UploadFile/" + fileName);
                        dishImage.SaveAs(filePath);
                        return Content("<script>alert('菜品上传成功');location.href='" + Url.Action("DishesPublish") +
                                       "'</script>");
                    }
                    else
                    {
                        return Content("<script>alert('菜品上传失败');location.href='" + Url.Action("DishesPublish") +
                                       "'</script>");
                    }
                }
                else
                {
                    return Content("<script>alert('请选择上传文件');location.href='" + Url.Action("DishesPublish") +
                                       "'</script>");
                }
            }
            catch (Exception e)
            {
                throw e;
            }


        }

        public ActionResult DoDishQuery(string categoryId, int? index=1)
        {
            if (string.IsNullOrEmpty(categoryId))
            {
                categoryId = "1";
            }
            int pageSize = 2;
            int pageIndex = index ?? 1;
            int totalCount = 0;
            PagedList<Dish> objList =
                new DishManager().GetDishInfo(categoryId, pageSize, pageIndex, out totalCount)
                    .AsQueryable()
                    .ToPagedList(pageIndex, pageSize);

            objList.TotalItemCount = totalCount;
            objList.CurrentPageIndex = pageIndex;

//            ViewBag.Dishes = objList;
            Common info = new Common();
            info.Dishes = objList;

            return View("DishesManage", info);

        }

        public ActionResult ModifyDish(string DishId)
        {
            Dish objDish = new DishManager().GetDish(DishId);
            if (objDish != null)
            {
                return View("DishEdit", objDish);
            }
            else
            {
                return Content("<script>alert('Id 为空');location.href='" + Url.Action("DoDishQuery") +
                                       "'</script>");//不成功就跳转到DoDishQuery返回的view，因为经过DoDishView可以创造出数据
            }
        }

        public ActionResult DoDishModify(Dish objDish, HttpPostedFileBase dishImage)
        {

            //【1】判断文件是否上传成功（文件大小，文件名重名）
            try
            {
                //判断是否有文件
                if (dishImage != null && dishImage.FileName != "")
                {
                    //判断文件大小是否符合要求
                    double fileLength = dishImage.ContentLength / (1024.0 * 1024.0);
                    if (fileLength > 2.0)
                    {
                        return
                            Content("<script>alert('图片最大不能超过2mb');location.href='" + Url.Action("ModifyDish") +
                                    "'</script>");
                    }
                    //获取文件名/重命名
                    string fileName = dishImage.FileName;
                    fileName = DateTime.Now.ToString("yyyyMMddmmhhss") + ".png";
                    objDish.DishImage = fileName;
                    //【2】调用BLL进行内容插入到数据库成功
                    int res = new DishManager().ModifyDish(objDish);
                    if (res > 0)
                    {
                        //【3】成功上传图片到项目文件底下
                        string filePath = Server.MapPath("~/UploadFile/" + fileName);
                        dishImage.SaveAs(filePath);
                        return Content("<script>alert('菜品上传成功');location.href='" + Url.Action("ModifyDish", new {DishId=objDish.DishId}) +
                                       "'</script>");
                    }
                    else
                    {
                        return Content("<script>alert('菜品上传失败');location.href='" + Url.Action("ModifyDish") +
                                       "'</script>");
                    }
                }
                else
                {
                    return Content("<script>alert('请选择上传文件');location.href='" + Url.Action("ModifyDish") +
                                       "'</script>");
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public ActionResult DeleteDish(string DishId)
        {
            Dish objDish = new DishManager().GetDish(DishId);
            string filePath = Server.MapPath("~/UploadFile/" + objDish.DishImage);
            int ret = new DishManager().DeleteDish(DishId);
            if (ret > 0)
            {
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }
                return Content("删除成功");
                
            }
            else
            {
                return Content("删除失败");
            }
        }
            
    }
}
