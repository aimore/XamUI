using System;
using Xamarin.Forms;

namespace XamHeroes
{
    public class CustomPage : ContentPage
    {
        public CustomPage()
        {
            BackgroundColor = Color.White;
            Content = new HeroCard();
        }
    }
}
