﻿

using System;

namespace Models
{
    [Serializable]
    public class Dish
    {
        public int DishId { get; set; }

        public string DishName { get; set; }

        public double UnitPrice { get; set; }

        public int CategoryId { get; set; }

        public string DishImage { get; set; }

        public string CategoryName { get; set; }

    }
}
