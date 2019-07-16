using System;
using System.Collections.ObjectModel;
using XamFood.Models;

namespace XamFood.ViewModels
{
    public class HomePageViewModel : BaseViewModel
    {
        public Models.Home HomeModel { get; set; }

        public HomePageViewModel()
        {

            RestaurantList = new ObservableCollection<PromoCard>
            {
                new PromoCard
                {
                    address = "27 Porter Street, 2112 Ryde",
                    image = "https://i.imgur.com/IM3y3mK.png",
                    title = "Lecker Burger",
                    leftWidget = "4.5",
                    rigthWidget = "OPEN",
                    cardColor = "#ffa3a3"
                },
                new PromoCard
                {
                    address = "27 Porter Street, 2112 Ryde",
                    image = @"https://greenwich-pizza-cdn.tillster.com/6fa30d87-fb0c-47ac-ab47-0fa59d59ade0.png",
                    title = "Joe's Pizza",
                    leftWidget = "4.6",
                    rigthWidget = "OPEN",
                    cardColor = "#dcdbff"
                },
                new PromoCard
                {
                    address = "27 Porter Street, 2112 Ryde",
                    image = "https://i.dlpng.com/static/png/210361_preview.png",
                    title = "Ling's Dumpling",
                    leftWidget = "4.8",
                    rigthWidget = "OPEN",
                    cardColor = "#ffe5db"
                },
                new PromoCard
                {
                    address = "Av. dos Franceses, 372 - Alemanha, São Luís - MA",
                    image = "https://pngimage.net/wp-content/uploads/2018/05/caldo-de-mocot%C3%B3-png-2.png",
                    title = "Buteco do Zé",
                    leftWidget = "0.1",
                    rigthWidget = "CLOSED",
                    cardColor = "#8f8f8f"
                }
            };
            CategoryList = new ObservableCollection<CategoryCard>
            {
                new CategoryCard
                {
                    title = "Burger",
                    image = "https://i.imgur.com/AzLZX2Q.jpg"
                },
                 new CategoryCard
                {
                    title = "Italian",
                    image = "https://cdn.websites.hibu.com/babfba3513224691a5365395a2b54ede/dms3rep/multi/tablet/home-image-880x415.png"
                },
                  new CategoryCard
                {
                    title = "Asian",
                    image = "https://minvghvl4c.followcdn.com/wp-content/uploads/2017/11/Ramen.png"
                },
                      new CategoryCard
                {
                    title = "Brazilian",
                    image = "https://media.danmurphys.com.au/dmo/product/908288-1.png"
                }
            };
        }
        public ObservableCollection<PromoCard> RestaurantList { get; set; }
        public ObservableCollection<CategoryCard> CategoryList { get; set; }
    }
}
