
using DAL.DBHelper;
using Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DAL
{
    public class DishBookService
    {

        /// <summary>
        /// Book the dish
        /// </summary>
        /// <param name="objDishBook"></param>
        /// <returns></returns>
        public int BookDish(DishBook objDishBook)
        {
            string sql =
                "INSERT INTO [dbo].[DishBook] (BookTime, ConsumeTime, ConsumePersons, HotelName, CustomerName, CustomerPhone, CustomerEmail, Comments, OrderStatus, RoomType) VALUES (@BookTime, @ConsumeTime, @ConsumePersons, @HotelName, @CustomerName, @CustomerPhone, @CustomerEmail, @Comments, @OrderStatus, @RoomType)";
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@BookTime", objDishBook.BookTime), 
                new SqlParameter("@ConsumeTime", objDishBook.ConsumeTime), 
                new SqlParameter("@ConsumePersons", objDishBook.ConsumePersons), 
                new SqlParameter("@HotelName", objDishBook.HotelName), 
                new SqlParameter("@CustomerName", objDishBook.CustomerName), 
                new SqlParameter("@CustomerPhone", objDishBook.CustomerPhone), 
                new SqlParameter("@CustomerEmail", objDishBook.CustomerEmail), 
                new SqlParameter("@Comments", objDishBook.Comments), 
                new SqlParameter("@OrderStatus", objDishBook.OrderStatus), 
                new SqlParameter("@RoomType", objDishBook.RoomType), 
            };

            int result = SQLHelper.Update(sql, param);
            return result;
        }

        /// <summary>
        /// Query latest booking information
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public List<DishBook> GetDishBooks(int count)
        {
            string sql =
                "SELECT TOP @COUNT BookId, BookTime, ConsumeTime, ConsumePersons, HotelName, CustomerName, CustomerPhone, CustomerEmail, Comments, OrderStatus, RoomType FROM [dbo].[DishBook] ORDER BY BookTime DESC";

            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@COUNT", count) 
            };

            List<DishBook> list = null;
            SqlDataReader objReader = SQLHelper.GetReader(sql, param);
            while (objReader.Read())
            {
                list.Add(new DishBook()
                {
                    BookId = Convert.ToInt32(objReader["BookId"]),
                    BookTime = Convert.ToDateTime(objReader["BookTime"]),
                    Comments = objReader["Comments"].ToString(),
                    ConsumePersons = Convert.ToInt32(objReader["ComsumePersons"]),
                    ConsumeTime = Convert.ToDateTime(objReader["ConsumeTime"]),
                    CustomerEmail = objReader["CustomerEmail"].ToString(),
                    CustomerName =  objReader["CustomerName"].ToString(),
                    CustomerPhone = objReader["CustomerPhone"].ToString(),
                    HotelName = objReader["HotelName"].ToString(),
                    OrderStatus = Convert.ToInt32(objReader["OrderStatus"])
                });
            }
            objReader.Close();
            return list;
        }


        public int AuditBooking(DishBook objDishBook)
        {
            string sql = "UPDATE [dbo].[DishBook] SET OrderStatus = @OrderStatus WHERE BookId = @BookId";
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@OrderStatus", objDishBook.OrderStatus),
                new SqlParameter("@BookId", objDishBook.BookId) 
            };

            int result = SQLHelper.Update(sql, param);
            return result;

        }
    }
}
