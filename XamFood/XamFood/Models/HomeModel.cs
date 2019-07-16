namespace XamFood.Models
{
    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json;


    public class PromoCard
    {
        public string title { get; set; }
        public string address { get; set; }
        public string image { get; set; }
        public string rigthWidget { get; set; }
        public string leftWidget { get; set; }
        public string cardColor { get; set; }
    }

    public class CategoryCard
    {
        public string title { get; set; }
        public string image { get; set; }
    }

    public class Home
    {
        public List<PromoCard> promoCards { get; set; }
        public List<CategoryCard> categoryCards { get; set; }
    }

    public class HomeModel
    {
        public Home home { get; set; }
    }

}
