using System;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using XamFood.Views;

namespace XamFood.Custom
{
    public class CustomTabBar : Xamarin.Forms.TabbedPage
    {
        public CustomTabBar()
        {
            SelectedTabColor = Color.Blue;
            On<Xamarin.Forms.PlatformConfiguration.Android>().SetToolbarPlacement(ToolbarPlacement.Bottom);
            var navigationPage =new HomePage();
            navigationPage.IconImageSource = "homeIcon.png";
            navigationPage.Title = "";
            var navigationPage2 = new ContentPage();
            navigationPage2.IconImageSource = "heartIcon.png";
            navigationPage2.Title = "";
            var navigationPage3 = new LocationPage();
            navigationPage3.IconImageSource = "pinIcon.png";
            navigationPage3.Title = "";
            var navigationPage4 = new ContentPage();
            navigationPage4.IconImageSource = "userIcon.png";
            navigationPage4.Title = "";
            Children.Add(navigationPage);
            Children.Add(navigationPage2);
            Children.Add(navigationPage3);
            Children.Add(navigationPage4);
        }
    }
}
