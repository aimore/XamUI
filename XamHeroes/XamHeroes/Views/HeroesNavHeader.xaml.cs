using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace XamHeroes
{
    public partial class HeroesNavHeader : Grid
    {
        static ViewModels.HeroesPageViewModel vm;
        public HeroesNavHeader()
        {
            InitializeComponent();
        }

        void Handle_Tapped(object sender, System.EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("SEARCH TAPPED");
            if (vm == null)
                vm = (XamHeroes.ViewModels.HeroesPageViewModel)(sender as View).BindingContext;
            vm.IsSearching = !vm.IsSearching;
        }
    }
}
