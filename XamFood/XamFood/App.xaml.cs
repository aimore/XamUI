using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XamFood
{
    public partial class App : Application
    {
        public static Resources.Home Home { get; set; } 

        public App()
        {
            InitializeComponent();
            MainPage = new Custom.CustomTabBar();
        }

        protected override async void OnStart()
        {
            // Handle when your app starts
            var service = await Services.AppResourceService.GetAppText();
            Home = Services.AppResourceService.TextResource.Home;
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
