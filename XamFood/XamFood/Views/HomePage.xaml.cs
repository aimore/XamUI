using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;
using XamFood.ViewModels;

namespace XamFood.Views
{

    [DesignTimeVisible(false)]
    public partial class HomePage : ContentPage
    {
        HomePageViewModel viewModel;

        public HomePage()
        {
            InitializeComponent();
            BindingContext = this.viewModel = new HomePageViewModel();
        }
    }
}
