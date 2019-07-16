using System;
using System.Diagnostics;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace XamFood.Views
{

    public class MapPage : Map
    {
        public
            MapPage()
        {
            //FIXME Map currently breaks when set VerticalOptions = LayoutOptions.Fill;

            // You can use MapSpan.FromCenterAndRadius
            VerticalOptions = LayoutOptions.FillAndExpand;
            MoveToRegion(MapSpan.FromCenterAndRadius(new Position(-2.529450, -44.296951), Distance.FromMiles(0.3)));
            var pin = new Pin { Address = "394 Pacific Ave, San Francisco CA", Label = "Joe's Pizza", Position = new Position(-2.529450, -44.296951),
                Type = PinType.Place,
                };
            pin.Clicked += Pin_Clicked;
            Pins.Add(pin);
        }

        private void Pin_Clicked(object sender, EventArgs e)
        {
            //MessagingCenter.Send<MapPage>(this, "Hi");
        }
    }
}