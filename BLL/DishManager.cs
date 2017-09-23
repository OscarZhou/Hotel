
using DAL;
using Models;
using System.Collections.Generic;

namespace BLL
{
    public class DishManager
    {
        private DishService objDishService = new DishService();

        public List<Dish> GetDishes(int count)
        {
            return objDishService.GetDishes(count);
        }

        public int AddDish(Dish objDish)
        {
            return objDishService.AddDish(objDish);
        }

        public List<Dish> GetDishesByCategory(string categoryName)
        {
            return objDishService.GetDishesByCategory(categoryName);
        }

        public int ModifyDish(Dish objDish)
        {
            return objDishService.ModifyDish(objDish);
        }

        public int DeleteDish(Dish objDish)
        {
            return objDishService.DeleteDish(objDish);
        }

        public Dish GetDish(string dishId)
        {
            return objDishService.GetDish(dishId);
        }
    }
}
