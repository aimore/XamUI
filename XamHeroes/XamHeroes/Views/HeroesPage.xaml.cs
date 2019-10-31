using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace XamHeroes
{
    public partial class HeroesPage : ContentPage
    {
        ControlTemplate heroesTemplate;
        ControlTemplate loadingTemplate;
        static HeroesResponsiveCollectionView control;
        public HeroesPage()
        {
            InitializeComponent();
            loadingTemplate = (ControlTemplate)Application.Current.Resources["LoadingPage"];
            var content = loadingTemplate.CreateContent();
            contentView.Content = content as Grid;
            heroesTemplate = (ControlTemplate)Application.Current.Resources["HeroesTemplate"];
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            List<Models.Heroes> results;
            var service = new Services.APIService();
            if (HeroesContent.GetHeroes == null)
                results = await service.GetAllHeroes();
            else
                results = HeroesContent.GetHeroes;
            Device.BeginInvokeOnMainThread(() => {
                control = new HeroesResponsiveCollectionView(results);
                Device.StartTimer(TimeSpan.FromSeconds(10), () =>
                {
                    // Do something
                    //System.Diagnostics.Debug.WriteLine("TIMER AFTER 10 SECODS");
                    //pulse.IsEnabled = false;
                    //pulse.IsVisible = false;
                    //pulse.Dispose();
                    contentView.Content = control;
                    return false; // True = Repeat again, False = Stop the timer
                });
            });
        }
    }
}