using System;
using Android.Content;
using Android.Views;
using Xamarin.Forms;
//using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using XamFood.Custom;
using XamFood.Droid.Renderers;

[assembly: Xamarin.Forms.ExportRenderer(handler: typeof(Xamarin.Forms.SearchBar), target: typeof(CustomSearchBarRenderer))]
namespace XamFood.Droid.Renderers
{
    public class CustomSearchBarRenderer : SearchBarRenderer
    {
        public CustomSearchBarRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<SearchBar> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
                (Control.FindViewById(Resources.GetIdentifier("android:id/search_plate", null, null))).
                    SetBackgroundColor(Android.Graphics.Color.Transparent);
        }
    }
}