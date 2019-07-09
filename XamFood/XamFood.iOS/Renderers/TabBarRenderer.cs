using System;
using CoreGraphics;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using XamFood.Custom;
using XamFood.iOS.Renderers;

[assembly: ExportRenderer(typeof(CustomTabBar), typeof(TabBarRenderer))]
namespace XamFood.iOS.Renderers
{
    public class TabBarRenderer : TabbedRenderer
    {
        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);
            // Creates image of the Button
            UIImage imageAppButtonButton = UIImage.FromFile("fab");
            // Creates a Button
            var appButton = new UIButton(UIButtonType.Custom);
            // Sets width and height to the Button
            appButton.Frame = new CGRect(0.0f, 0.0f, imageAppButtonButton.Size.Width, imageAppButtonButton.Size.Height);
            // Sets image to the Button
            appButton.SetBackgroundImage(imageAppButtonButton, UIControlState.Normal);
            // Sets the center of the Button to the center of the TabBar.
            //How to set the center of the button depends if the image is circular and its height is bigger than the tabBar
            nfloat heightDifference = imageAppButtonButton.Size.Height - this.TabBar.Frame.Size.Height;
            //if(heightDifference < 0)
            //    cameraButton.Center = TabBar.Center;
            //else
            //{
            //    CGPoint center = TabBar.Center;
            //    center.Y = (center.Y - heightDifference / 2.0f);
            //    cameraButton.Center = center;
            //}
            CGPoint center = TabBar.Center;
            center.Y = 820f;
            appButton.Center = center;
            // Sets an action to the Button
            var eventHandler = new EventHandler(DoSomething);
            appButton.AddTarget(eventHandler, events: UIControlEvent.TouchUpInside);

            //Create shadow effect
            appButton.Layer.ShadowColor = UIColor.Black.CGColor;
            appButton.Layer.ShadowOffset = new CGSize(width: 0.0, height: 5.0);
            appButton.Layer.ShadowOpacity = 0.5f;
            appButton.Layer.ShadowRadius = 2.0f;
            appButton.Layer.MasksToBounds = false;

            // Adds the Button to the view
            View.AddSubview(appButton);
            TabBar.ItemSpacing = 4f;
            TabBar.BarTintColor = UIColor.White;
            TabBar.BackgroundColor = UIColor.White;
            TabBar.TintColor = UIColor.Blue;
            //Apply this for dark mode
            //TabBar.BarStyle = UIBarStyle.Black;
           
            var items = TabBar.Items;
            var item1 = items[1];
            var item2 = items[2];
            item1.TitlePositionAdjustment = new UIOffset(-20f, 0f);
            item2.TitlePositionAdjustment = new UIOffset(20f, 0f);

            //Remove the top line
            TabBar.ShadowImage = new UIImage();
            TabBar.BackgroundImage = new UIImage();
            TabBar.ClipsToBounds = true;
        }

        public async void DoSomething(object sender, System.EventArgs e) {
           await Xamarin.Forms.Application.Current.MainPage.DisplayAlert("AppBottomBar", "click!", "ok");
        }
    }
}