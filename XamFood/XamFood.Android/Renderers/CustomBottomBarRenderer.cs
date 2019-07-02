using Android.Content;
using Android.Graphics.Drawables;
using Android.Support.Design.BottomAppBar;
using Android.Support.Design.Internal;
using Android.Support.Design.Widget;
using Android.Support.V4.Content;
using Android.Util;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms.Platform.Android.AppCompat;
using XamFood.Custom;
using XamFood.Droid.Renderers;
using static Android.Widget.ImageView;
using RelativeLayout = Android.Widget.RelativeLayout;
using View = Android.Views.View;

[assembly: ExportRenderer(typeof(CustomTabBar), typeof(CustomBottomBarRenderer))]
namespace XamFood.Droid.Renderers
{
    public class CustomBottomBarRenderer : TabbedPageRenderer
    {
        FloatingActionButton floatingActionButton;
        CoordinatorLayout coordinatorLayout;

        public CustomBottomBarRenderer(Context context) : base(context)
        {

        }

        protected override void OnElementChanged(ElementChangedEventArgs<TabbedPage> e)
        {
            base.OnElementChanged(e);
            var metrics = Resources.DisplayMetrics;
            var width = metrics.WidthPixels;
            floatingActionButton = new FloatingActionButton(this.Context);
            var layoutParams = new LayoutParams(LayoutParams.WrapContent,LayoutParams.WrapContent);
            LayoutParams paramss = new LayoutParams(
                LayoutParams.WrapContent,
                LayoutParams.WrapContent
            );
            MarginLayoutParams marginLayout = new MarginLayoutParams(LayoutParams.WrapContent, LayoutParams.WrapContent);
            marginLayout.SetMargins((int)(width / 2.3), 0, 0, 40);
            floatingActionButton.BackgroundTintList = Context.GetColorStateList(Resource.Color.blueColor);
            floatingActionButton.Click += FloatingActionButton_Click;

            floatingActionButton.LayoutParameters = marginLayout;
            floatingActionButton.SetBackgroundColor(Android.Graphics.Color.Blue);
            floatingActionButton.SetImageResource(Resource.Drawable.plus_white);
            floatingActionButton.SetForegroundGravity(GravityFlags.Center);
            floatingActionButton.Elevation = 6;
            var layout = (GetChildAt(0) as Android.Widget.RelativeLayout);
            RelativeLayout.LayoutParams p = new RelativeLayout.LayoutParams(RelativeLayout.LayoutParams.WrapContent, RelativeLayout.LayoutParams.WrapContent);
            p.AddRule(LayoutRules.AlignBottom);

            //layout.AddView(floatingActionButton);
            var bottomNavigationView = (GetChildAt(0) as Android.Widget.RelativeLayout).GetChildAt(1) as BottomNavigationView;

            //ViewGroupUtils.removeView(bottomNavigationView);
            //var b =  LayoutInflater.From(this.Context).Inflate(Resource.Layout.MaterialBottomAppBar, null);
            //layout.AddView(b,1);
            bottomNavigationView.SetClipChildren(false);
            var menuView = bottomNavigationView.GetChildAt(0) as BottomNavigationMenuView;
            BottomNavigationItemView item2 = (BottomNavigationItemView)menuView.GetChildAt(1);
            BottomNavigationItemView item3= (BottomNavigationItemView)menuView.GetChildAt(2);

            item2.SetPadding(0, 0, 100, 0);
            item3.SetPadding(100,0,0,0);

            //item2.SetPaddingRelative(0, 0, 300, 0);

            //for (int i = 0; i < menuView.ChildCount; i++)
            //{
            //    BottomNavigationItemView item = (BottomNavigationItemView)menuView.GetChildAt(i);
            //    item.SetPadding(0, 0, 50, 0);
            //    //noinspection RestrictedApi
            //    item.SetShifting(false);
            //    // set once again checked value, so view will be updated
            //    //noinspection RestrictedApi
            //    item.SetChecked(item.ItemData.IsChecked);
            //}
            bottomNavigationView.AddView(floatingActionButton);


        }

        private void FloatingActionButton_Click(object sender, System.EventArgs e)
        {
            //View view = (View)sender;
            //Snackbar.Make(view, "Replace with your own action", Snackbar.LengthLong)
            //    .SetAction("Action", (Android.Views.View.IOnClickListener)null).Show();
            Toast toast = Toast.MakeText(this.Context, "Replace with your own action", ToastLength.Long);
            toast.SetGravity(GravityFlags.Center, 0, 0);
            toast.Show();
        }

        //protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        //{
        //    base.OnElementPropertyChanged(sender, e);
        //}

        CoordinatorLayout GetLayout()
        {
            coordinatorLayout = new CoordinatorLayout(this.Context);
            coordinatorLayout.LayoutParameters = new CoordinatorLayout.LayoutParams(LayoutParams.MatchParent, LayoutParams.MatchParent); //Width, Height;
            var bottombar = new BottomAppBar(this.Context);
            bottombar.LayoutParameters = new BottomAppBar.LayoutParams(LayoutParams.MatchParent, LayoutParams.WrapContent); //Width, Height;
            bottombar.SetForegroundGravity(GravityFlags.Bottom);
            bottombar.Id = 666;
            bottombar.BackgroundTint = ContextCompat.GetColorStateList(this.Context, Resource.Color.colorPrimary);
            bottombar.FabAlignmentMode = BottomAppBar.FabAlignmentModeCenter;
            bottombar.NavigationIcon = Context.GetDrawable(Resource.Drawable.homeIcon);
            floatingActionButton = new FloatingActionButton(this.Context);

            //var layoutParams = fab.LayoutParameters;
            CoordinatorLayout.LayoutParams layoutParams = (CoordinatorLayout.LayoutParams)floatingActionButton.LayoutParameters;
            layoutParams.Width = LayoutParams.WrapContent;
            layoutParams.Height = LayoutParams.WrapContent;
            layoutParams.AnchorId = bottombar.Id;
            floatingActionButton.LayoutParameters = layoutParams;
            floatingActionButton.SetImageResource(Resource.Drawable.plus);
            return null;
        }
    }


    public class ViewGroupUtils
    {

        public static ViewGroup getParent(View view)
        {
            return (ViewGroup)view.Parent;
        }

        public static void removeView(View view)
        {
            ViewGroup parent = getParent(view);
            if (parent != null)
            {
                parent.RemoveView(view);
            }
        }

        public static void replaceView(View currentView, View newView)
        {
            ViewGroup parent = getParent(currentView);
            if (parent == null)
            {
                return;
            }
            int index = parent.IndexOfChild(currentView);
            removeView(currentView);
            removeView(newView);
            parent.AddView(newView, index);
        }
    }
}