
using DAL.DBHelper;
using Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DAL
{
    public class NewsService
    {
        /// <summary>
        /// Query data by specifying definite number
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public List<News> GetNews(int count)
        {
            string sql = "SELECT TOP @COUNT NewsId, NewsTitle, NewsContents, PublishTime, CategoryId, CategoryName FROM [dbo].[News] INNER JOIN [dbo].[NewsCategory] ON NewsCateogry.CategoryId = News.CategoryId ORDER BY PublishTime DESC";
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@COUNT", count)
            };

            List<News> list = new List<News>();
            SqlDataReader objReader = SQLHelper.GetReader(sql, param);
            while (objReader.Read())
            {
                list.Add(new News()
                {
                    CategoryId = Convert.ToInt32( objReader["CategoryId"]),
                    NewsContents = objReader["NewsContents"].ToString(),
                    NewsId = Convert.ToInt32(objReader["NewsId"]),
                    NewsTitle = objReader["NewsTitle"].ToString(),
                    PublishTime = Convert.ToDateTime(objReader["PublishTime"]),
                    CategoryName = objReader["CategoryName"].ToString()
                });
            }

            objReader.Close();
            return list;
        }

        /// <summary>
        /// Publish News
        /// </summary>
        /// <param name="objNews"></param>
        /// <returns></returns>
        public int PublishNews(News objNews)
        {
            string sql =
                "INSERT INTO [dbo].[News] (NewsTitle, NewsContents, CategoryId) VALUES(@NewTitle, @NewsContent, @CategoryId)";
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@NewsTitle", objNews.NewsTitle), 
                new SqlParameter("@NewsContents", objNews.NewsContents),
                new SqlParameter("@CategoryId", objNews.CategoryId) 
            };

            int result = SQLHelper.Update(sql, param);
            return result;
        }

        /// <summary>
        /// Get News detail by NewsId
        /// </summary>
        /// <param name="newsId"></param>
        /// <returns></returns>
        public News GetNewsById(string newsId)
        {
            string sql = "SELECT NewsId, NewsTitle, NewsContents, CategoryId, PublishTime FROM [dbo].[News] WHERE NewsId = @NewsId";
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@NewsId", newsId) 
            };
            News objNews = null;
            SqlDataReader objReader = SQLHelper.GetReader(sql, param);
            if (objReader.Read())
            {
                objNews = new News()
                {
                    NewsId = Convert.ToInt32(objReader["NewsId"]),
                    NewsTitle = objReader["NewsTitle"].ToString(),
                    NewsContents = objReader["NewsContents"].ToString(),
                    CategoryId = Convert.ToInt32(objReader["CategoryId"]),
                    PublishTime = Convert.ToDateTime(objReader["PublishTime"])
                };
                
            }
            objReader.Close();
            return objNews;
        }

        /// <summary>
        /// Query all News
        /// </summary>
        /// <returns></returns>
        public List<News> GetNews()
        {
            string sql = "SELECT NewsId, NewsTitle, NewsContents, PublishTime, CategoryId, CategoryName FROM [dbo].[News] INNER JOIN [dbo].[NewsCategory] ON NewsCateogry.CategoryId = News.CategoryId ORDER BY PublishTime DESC";

            List<News> list = new List<News>();
            SqlDataReader objReader = SQLHelper.GetReader(sql);
            while (objReader.Read())
            {
                list.Add(new News()
                {
                    CategoryId = Convert.ToInt32( objReader["CategoryId"]),
                    NewsContents = objReader["NewsContents"].ToString(),
                    NewsId = Convert.ToInt32(objReader["NewsId"]),
                    NewsTitle = objReader["NewsTitle"].ToString(),
                    PublishTime = Convert.ToDateTime(objReader["PublishTime"]),
                    CategoryName = objReader["CategoryName"].ToString()
                });
            }

            objReader.Close();
            return list;
        }


        /// <summary>
        /// Modify News
        /// </summary>
        /// <param name="objNews"></param>
        /// <returns></returns>
        public int ModifyNews(News objNews)
        {
            string sql =
                "Update [dbo].[News] SET NewsTitle = @NewsTitle, NewsContents = @NewsContents, CategoryId = @CategoryId";
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@NewsTitle", objNews.NewsTitle),
                new SqlParameter("@NewsContents", objNews.NewsContents),
                new SqlParameter("@CategoryId", objNews.CategoryId) 
            };

            int result = SQLHelper.Update(sql, param);
            return result;
        }

        /// <summary>
        /// Delete News
        /// </summary>
        /// <param name="objNews"></param>
        /// <returns></returns>
        public int DeleteNews(News objNews)
        {
            string sql = "DELETE FROM [dbo].[News] WHERE NewsId = @NewsId";
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@NewsId", objNews.NewsId) 
            };

            int result = SQLHelper.Update(sql, param);
            return result;
        }
    }
}
