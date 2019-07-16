using System;
using System.Collections.Generic;
using System.Reflection;
using Xamarin.Essentials;

using Xamarin.Forms;

namespace XamFood.Views
{
    public partial class LocationPage : ContentPage
    {
        private Animation _animation;
        private double _initialHeight;
        private double mapHeight;
        private static bool CanExecute = true;
        public LocationPage()
        {
            InitializeComponent();
            //Shell.SetNavBarIsVisible(this, false);
            _initialHeight = Math.Round(DeviceDisplay.MainDisplayInfo.Height / 6);
            //var mapSize = DeviceDisplay.MainDisplayInfo.Height - _initialHeight;
            bottomSheet.HeightRequest = _initialHeight;
          
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);
            mapHeight = height - _initialHeight;
            map.HeightRequest = mapHeight;
        }


        //protected override void OnAppearing()
        //{
        //    base.OnAppearing();
        //    MessagingCenter.Subscribe<Views.MapPage>(this, "Hi", (sender) => {
        //        Handle_Clicked(sender, null);
        //    });
        //}

        async void Handle_Clicked(object sender, System.EventArgs e)
        {
            if (!CanExecute)
                return;
            CanExecute = false;
            try
            {

                if (clamp.Height.Value < _initialHeight)
                {
                    // Move back to original height
                    _animation = new Animation(
                        (d) => clamp.Height = new GridLength(Clamp(d, 0, double.MaxValue)),
                        clamp.Height.Value, _initialHeight, Easing.Linear, () => _animation = null);
                }
                else
                {
                    // Hide the row
                    _animation = new Animation(
                        (d) => clamp.Height = new GridLength(Clamp(d, 0, double.MaxValue)),
                        _initialHeight, 0, Easing.Linear, () => _animation = null);
                }
                _animation.Commit(this, "the animation");

                CanExecute = true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Click too fast! ", ex.Message);
                return;
            }
        }


        // Make sure we don't go below zero
        private double Clamp(double value, double minValue, double maxValue)
        {
            if (value < minValue)
            {
                return minValue;
            }

            if (value > maxValue)
            {
                return maxValue;
            }

            return value;
        }
    }
}
