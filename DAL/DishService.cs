
using DAL.DBHelper;
using Models;
using System;
using System.Collections.Generic;
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
                "INSERT INTO [dbo].[Dish] (DishName, UnitPrice, CategoryId) VALUES (@DishName, @UnitPrice, @CategoryId)";

            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@DishName", objDish.DishName),
                new SqlParameter("@UnitPrice", objDish.UnitPrice),
                new SqlParameter("@CategoryId", objDish.CategoryId) 
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
                "UPDATE [dbo].[Dish] SET DishName = @DishName, UnitPrice = @UnitPrice, CategoryId = @CategoryId WHERE DishId = @DishId";
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@DishName", objDish.DishName),
                new SqlParameter("@UnitPrice", objDish.UnitPrice), 
                new SqlParameter("@CategoryId", objDish.CategoryId),
                new SqlParameter("@DishId", objDish.DishId) 
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
            string sql = "SELECT DishId, DishName, UnitPrice, CategoryId FROM [dbo].[Dish] WHERE DishId = @DishId";
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
                    CategoryName = objReader["CategoryName"].ToString(),
                    DishId = Convert.ToInt32(objReader["DishId"]),
                    DishName = objReader["DishName"].ToString(),
                    UnitPrice = Convert.ToDouble(objReader["UnitPrice"])
                };
            }
            objReader.Close();
            return objDish;
        }
    }
}
