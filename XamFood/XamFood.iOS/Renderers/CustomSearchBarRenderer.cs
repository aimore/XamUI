using System;
using System.ComponentModel;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using XamFood.Custom;
using XamFood.iOS.Renderers;

[assembly: ExportRenderer(typeof(SearchBar), typeof(CustomSearchBarRenderer))]
namespace XamFood.iOS.Renderers
{
    public class CustomSearchBarRenderer : SearchBarRenderer
    {
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            //Override needed, otherwise the original Xamarin code will force show the Cancel button on the right side of the entry field
            if (e.PropertyName == SearchBar.TextProperty.PropertyName)
            {
                Control.Text = Element.Text;
            }

            if (e.PropertyName != SearchBar.CancelButtonColorProperty.PropertyName && e.PropertyName != SearchBar.TextProperty.PropertyName)
                base.OnElementPropertyChanged(sender, e);
        }

        //For custom search icon
        //protected override void OnElementChanged(ElementChangedEventArgs<SearchBar> e)
        //{
        //    base.OnElementChanged(e);
        //    if (Control == null)
        //        return;
        //    try
        //    {
        //        var img = UIImage.FromBundle("searchIcon");
        //        Control.SetImageforSearchBarIcon(iconImage: UIImage.FromBundle("searchIcon"),
        //            icon: UISearchBarIcon.Search,
        //            state: UIControlState.Normal);
        //    }
        //    catch (Exception ex)
        //    {

        //    }

        //}
    }
}