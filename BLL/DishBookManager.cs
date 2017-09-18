
using DAL;
using Models;
using System.Collections.Generic;

namespace BLL
{
    public class DishBookManager
    {
        private DishBookService objDishBookService = new DishBookService();

        public int BookDish(DishBook objDishBook)
        {
            return objDishBookService.BookDish(objDishBook);
        }

        public List<DishBook> GetDishBooks(int count)
        {
            return objDishBookService.GetDishBooks(count);
        }

        public int AuditBooking(DishBook objDishBook)
        {
            return objDishBookService.AuditBooking(objDishBook);
        }
    }
}
