
using DAL.DBHelper;
using Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class DishService
    {
        /// <summary>
        /// Get featured dishes with definite data
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public List<Dish> GetDishes(int count)
        {
            string sql =
                "SELECT TOP (@COUNT) DishId, DishName, UnitPrice, Dish.CategoryId, CategoryName, DishImg FROM [dbo].[Dish] INNER JOIN [dbo].[DishCategory] ON DishCategory.CategoryId = Dish.CategoryId";

            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@COUNT", count) 
            };

            List<Dish> list = new List<Dish>();
            SqlDataReader objReader = SQLHelper.GetReader(sql, param);
            while (objReader.Read())
            {
                list.Add(new Dish()
                {
                    CategoryId = Convert.ToInt32(objReader["CategoryId"]),
                    CategoryName = objReader["CategoryName"].ToString(),
                    DishId = Convert.ToInt32(objReader["DishId"]),
                    DishName = objReader["DishName"].ToString(),
                    UnitPrice = Convert.ToDouble(objReader["UnitPrice"]),
                    DishImage = objReader["DishImg"].ToString()
                });
            }
            objReader.Close();
            return list;
        }

        /// <summary>
        /// Add dishes
        /// </summary>
        /// <param name="objDish"></param>
        /// <returns></returns>
        public int AddDish(Dish objDish)
        {
            string sql =
                "INSERT INTO [dbo].[Dish] (DishName, UnitPrice, CategoryId, DishImg) VALUES (@DishName, @UnitPrice, @CategoryId, @DishImg); SELECT @@IDENTITY";

            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@DishName", objDish.DishName),
                new SqlParameter("@UnitPrice", objDish.UnitPrice),
                new SqlParameter("@CategoryId", objDish.CategoryId),
                new SqlParameter("@DishImg", objDish.DishImage)
            };

            int result = SQLHelper.Update(sql, param);
            return result;
        }

        /// <summary>
        /// Get the dishes by category
        /// </summary>
        /// <param name="categoryName"></param>
        /// <returns></returns>
        public List<Dish> GetDishesByCategory(string categoryName)
        {
            string sql =
                "SELECT DishId, DishName, UnitPrice, CategoryId, CategoryName FROM [dbo].[Dish] INNER JOIN [dbo].[DishCategory] ON DishCategory.CategoryId = Dish.CategoryId WHERE CategoryName = @CategoryName";

            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@CategoryName", categoryName) 
            };

            List<Dish> list = null;
            SqlDataReader objReader = SQLHelper.GetReader(sql, param);
            while (objReader.Read())
            {
                list.Add(new Dish()
                {
                    CategoryId = Convert.ToInt32(objReader["CategoryId"]),
                    CategoryName = objReader["CategoryName"].ToString(),
                    DishId = Convert.ToInt32(objReader["DishId"]),
                    DishName = objReader["DishName"].ToString(),
                    UnitPrice = Convert.ToDouble(objReader["UnitPrice"])
                });
            }
            objReader.Close();
            return list;
        }

        /// <summary>
        /// Modify dish information
        /// </summary>
        /// <param name="objDish"></param>
        /// <returns></returns>
        public int ModifyDish(Dish objDish)
        {
            string sql =
                "UPDATE [dbo].[Dish] SET DishName = @DishName, UnitPrice = @UnitPrice, CategoryId = @CategoryId, DishImg=@DishImg WHERE DishId = @DishId";
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@DishName", objDish.DishName),
                new SqlParameter("@UnitPrice", objDish.UnitPrice), 
                new SqlParameter("@CategoryId", objDish.CategoryId),
                new SqlParameter("@DishId", objDish.DishId),
                new SqlParameter("@DishImg", objDish.DishImage) 
            };

            int result = SQLHelper.Update(sql, param);
            return result;
            
        }

        /// <summary>
        /// Delete dish item
        /// </summary>
        /// <param name="objDish"></param>
        /// <returns></returns>
        public int DeleteDish(Dish objDish)
        {
            string sql = "DELETE FROM [dbo].[Dish] WHERE DishId = @DishId";
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@DishId", objDish.DishId) 
            };

            int result = SQLHelper.Update(sql, param);
            return result;
        }

        /// <summary>
        /// Get the dish by dishId
        /// </summary>
        /// <param name="dishId"></param>
        /// <returns></returns>
        public Dish GetDish(string dishId)
        {
            string sql = "SELECT DishId, DishName, UnitPrice, CategoryId, DishImg FROM [dbo].[Dish] WHERE DishId = @DishId";
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@DishId", dishId)
            };

            Dish objDish = null;
            SqlDataReader objReader = SQLHelper.GetReader(sql, param);
            if (objReader.Read())
            {
                objDish = new Dish()
                {
                    CategoryId = Convert.ToInt32(objReader["CategoryId"]),
                    DishImage = objReader["DishImg"]!=null?objReader["DishImg"].ToString():"",
                    DishId = Convert.ToInt32(objReader["DishId"]),
                    DishName = objReader["DishName"].ToString(),
                    UnitPrice = Convert.ToDouble(objReader["UnitPrice"])
                };
            }
            objReader.Close();
            return objDish;
        }


        public List<DishCategory> GetDishCategories()
        {
            string sql = "SELECT CategoryId, CategoryName FROM [dbo].[DishCategory]";
            
            List<DishCategory> list = new List<DishCategory>();
            SqlDataReader objReader = SQLHelper.GetReader(sql);
            while (objReader.Read())
            {
                list.Add(new DishCategory()
                {
                    CategoryId = Convert.ToInt32(objReader["CategoryId"]),
                    CategoryName = objReader["CategoryName"].ToString()
                });
            }
            objReader.Close();
            return list;
        }


        public List<Dish> GetDishInfo(string categoryId, int pageSize, int pageIndex, out int totalCount)
        {
            string tableName = " Dish ";
            string id = " DishId ";
            string innerjoin = " inner join DishCategory on Dish.CategoryId = DishCategory.CategoryId ";
            string where = " and Dish.CategoryId = ";
            string fkid = categoryId;

            DataSet ds = Common.GetList(pageSize, pageIndex, tableName, id, innerjoin, where, fkid, out totalCount);
            List<Dish> objDishes = new List<Dish>();
            if (ds!=null && ds.Tables.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    objDishes.Add(new Dish()
                    {
                        CategoryId = Convert.ToInt32(categoryId),
                        CategoryName = dr["CategoryName"].ToString(),
                        DishId = Convert.ToInt32(dr["DishId"]),
                        DishImage = dr["DishImg"].ToString(),
                        DishName = dr["DishName"].ToString(),
                        UnitPrice = Convert.ToDouble(dr["UnitPrice"])
                    });
                }
            }

            return objDishes;

        }
    }
}
