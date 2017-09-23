
using DAL;
using Models;
using System.Collections.Generic;

namespace BLL
{
    public class NewsManager
    {
        private NewsService objNewsService = new NewsService();
        public List<News> GetNews(int count)
        {
            return objNewsService.GetNews(count);
        }

        public int PublishNews(News objNews)
        {
            return objNewsService.PublishNews(objNews);
        }

        public News GetNewsById(string newsId)
        {
            return objNewsService.GetNewsById(newsId);
        }

        public List<News> GetNews()
        {
            return objNewsService.GetNews();
        }

        public int ModifyNews(News objNews)
        {
            return objNewsService.ModifyNews(objNews);
        }

        public int DeleteNews(News objNews)
        {
            return objNewsService.DeleteNews(objNews);
        }
    }
}
