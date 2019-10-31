using System;
using System.Linq;
using CoreGraphics;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using XamHeroes;
using XamHeroes.iOS;

[assembly: ExportRenderer(typeof(CustomPage), typeof(CustomPageRenderer))]
namespace XamHeroes.iOS
{
    public class CustomPageRenderer : PageRenderer
    {
        public bool DismissOnBackgroundTap = true;
        public bool RoundTopCorner = true;
        public static UIViewController Controller { get; set; }
        public UIView ContainerView = new UIView();
        public UIView PullBarView = new UIView();
        public UIViewController ChildViewController { get; private set; }
        float lastY = 0;
        UIPanGestureRecognizer pan;
        float topY = 0;
        float middleY = 400;
        float bottomY = 600;
        float bottomOffset = 64;
        //HACK panOffset To prevent jump when goes from top to down
        float panOffset = 0;
        bool applyPanOffset = false;

        private SheetSize _containerSize = SheetSize.Fixed(300f);
        private SheetSize _actualContainerSize = SheetSize.Fixed(300f);
        private SheetSize[] _orderedSheetSize = { SheetSize.Fixed(300f), SheetSize.FullScreen };

        private CGPoint _firstPanPoint = CGPoint.Empty;
        public UIColor OverlayColor => UIColor.FromRGBA(0, 0, 0, (int)70 * 255);
        private NSLayoutConstraint _containerHeightConstraint;
        private UIScrollView _childScrollView { get; set; }

        public CustomPageRenderer()
        {
           
        }
        static bool hasStarted;
        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);
            if (e.OldElement != null || Element == null)
                return;
            else if (hasStarted)
                return;
            hasStarted = true;
            try
            {
                Controller = GetViewControler(Element);
                ChildViewController = Controller;
                this.View.BackgroundColor = UIColor.Clear;
                _containerHeightConstraint = new NSLayoutConstraint();
                SetUpContainerView();
                SetUpDismissView();
                SetUpChildViewController();
                SetUpPullBarView();
                //if (sizes != null && sizes.Count() > 0)
                //SetSizes(sizes, false);
                //Controller.ModalPresentationStyle = UIModalPresentationStyle.OverFullScreen;
               
            }
            catch (Exception ex)
            {

            }

        }

        public static UIViewController GetViewControler(Xamarin.Forms.VisualElement visualElement) 
        {
            var renderer = Platform.GetRenderer(visualElement);
            if (renderer == null)
            {
                renderer = Platform.CreateRenderer(visualElement);
                Platform.SetRenderer(visualElement, renderer);
            }
            return renderer.ViewController;
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
        }

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);
            UIView.Animate(0.3, 0, UIViewAnimationOptions.CurveEaseOut, () =>
            {
                this.View.BackgroundColor = OverlayColor;
                ContainerView.Transform = CGAffineTransform.MakeIdentity();
                _actualContainerSize = SheetSize.Fixed((float)ContainerView.Frame.Height);
            }, null);
        }

        private UIEdgeInsets safeAreaInsets()
        {
            var insets = UIEdgeInsets.Zero;
            insets = UIApplication.SharedApplication.KeyWindow?.SafeAreaInsets ?? insets;

            insets.Top = (float)Math.Max(insets.Top, 20);

            return insets;
        }

        public void SetSizes(SheetSize[] sizes, bool animated = true)
        {
            if (sizes.Length < 0)
                return;
            _orderedSheetSize = sizes;
            _orderedSheetSize.OrderBy(ss => ss.Value);
            Resize(sizes[0]);
        }

        public void Resize(SheetSize toSize, bool animated = true)
        {
            if (animated)
            {
                UIView.Animate(0.2, 0, UIViewAnimationOptions.CurveEaseOut, () =>
                {
                    _containerHeightConstraint.Constant = GetHalfScreen();
                    this.View.LayoutIfNeeded();
                }, null);
            }
            else
            {
                _containerHeightConstraint.Constant = GetHalfScreen();
            }
            _containerSize = toSize;
            _actualContainerSize = toSize;
        }

        private void SetUpContainerView()
        {

            ContainerView.BackgroundColor = UIColor.Clear;
            ContainerView.TranslatesAutoresizingMaskIntoConstraints = false;

            this.View.AddSubview(ContainerView);
            this.View.AddConstraint(NSLayoutConstraint.Create(this.View, NSLayoutAttribute.Bottom, NSLayoutRelation.Equal, ContainerView, NSLayoutAttribute.Bottom, 1, 0));
            this.View.AddConstraint(NSLayoutConstraint.Create(this.View, NSLayoutAttribute.Leading, NSLayoutRelation.Equal, ContainerView, NSLayoutAttribute.Leading, 1, 0));
            this.View.AddConstraint(NSLayoutConstraint.Create(this.View, NSLayoutAttribute.Trailing, NSLayoutRelation.Equal, ContainerView, NSLayoutAttribute.Trailing, 1, 0));
            _containerHeightConstraint = NSLayoutConstraint.Create(ContainerView, NSLayoutAttribute.Height, NSLayoutRelation.Equal, 1, GetHalfScreen());
            _containerHeightConstraint.Priority = 900;

            ContainerView.AddConstraint(_containerHeightConstraint);
            ContainerView.Transform = CGAffineTransform.MakeTranslation(0, UIScreen.MainScreen.Bounds.Height);
        }

        private void SetUpChildViewController()
        {
            ChildViewController.WillMoveToParentViewController(this);
            AddChildViewController(ChildViewController);
            ContainerView.TranslatesAutoresizingMaskIntoConstraints = false;
            ChildViewController.View.TranslatesAutoresizingMaskIntoConstraints = false;
            ContainerView.AddSubview(ChildViewController.View);

            var bottomInset = this.AdditionalSafeAreaInsets.Bottom;

            ContainerView.AddConstraint(NSLayoutConstraint.Create(ContainerView, NSLayoutAttribute.Top, NSLayoutRelation.Equal, ChildViewController.View, NSLayoutAttribute.Top, 1, 0));
            ContainerView.AddConstraint(NSLayoutConstraint.Create(ContainerView, NSLayoutAttribute.Bottom, NSLayoutRelation.Equal, ChildViewController.View, NSLayoutAttribute.Bottom, 1, bottomInset));
            ContainerView.AddConstraint(NSLayoutConstraint.Create(ContainerView, NSLayoutAttribute.Leading, NSLayoutRelation.Equal, ChildViewController.View, NSLayoutAttribute.Leading, 1, 0));
            ContainerView.AddConstraint(NSLayoutConstraint.Create(ContainerView, NSLayoutAttribute.Trailing, NSLayoutRelation.Equal, ChildViewController.View, NSLayoutAttribute.Trailing, 1, 0));


            if (RoundTopCorner)
            {
                ChildViewController.View.Layer.MaskedCorners = CoreAnimation.CACornerMask.MaxXMinYCorner | CoreAnimation.CACornerMask.MinXMinYCorner;
                ChildViewController.View.Layer.CornerRadius = 10;
                ChildViewController.View.Layer.MasksToBounds = true;
            }

            ChildViewController.DidMoveToParentViewController(this);
        }

        private void SetUpDismissView()
        {
            UIView DismissAreaView = new UIView(CGRect.Empty);
            DismissAreaView.BackgroundColor = UIColor.Clear;
            DismissAreaView.TranslatesAutoresizingMaskIntoConstraints = false;
            this.View.AddSubview(DismissAreaView);

            this.View.AddConstraint(NSLayoutConstraint.Create(this.View, NSLayoutAttribute.Top, NSLayoutRelation.Equal, DismissAreaView, NSLayoutAttribute.Top, 1, 0));
            this.View.AddConstraint(NSLayoutConstraint.Create(this.View, NSLayoutAttribute.Leading, NSLayoutRelation.Equal, DismissAreaView, NSLayoutAttribute.Leading, 1, 0));
            this.View.AddConstraint(NSLayoutConstraint.Create(this.View, NSLayoutAttribute.Trailing, NSLayoutRelation.Equal, DismissAreaView, NSLayoutAttribute.Trailing, 1, 0));
            this.View.AddConstraint(NSLayoutConstraint.Create(ContainerView, NSLayoutAttribute.Top, NSLayoutRelation.Equal, DismissAreaView, NSLayoutAttribute.Bottom, 1, 0));

            DismissAreaView.UserInteractionEnabled = true;

            DismissAreaView.AddGestureRecognizer(new UITapGestureRecognizer(() =>
            {
                DismissTapped();
            }));
        }

        private void SetUpPullBarView()
        {
            ContainerView.AddSubview(PullBarView);
            PullBarView.TranslatesAutoresizingMaskIntoConstraints = false;
            PullBarView.AddConstraint(NSLayoutConstraint.Create(PullBarView, NSLayoutAttribute.Height, NSLayoutRelation.Equal, 1, 24));

            ContainerView.AddConstraint(NSLayoutConstraint.Create(ContainerView, NSLayoutAttribute.Top, NSLayoutRelation.Equal, PullBarView, NSLayoutAttribute.Top, 1, 0));
            ContainerView.AddConstraint(NSLayoutConstraint.Create(ContainerView, NSLayoutAttribute.Leading, NSLayoutRelation.Equal, PullBarView, NSLayoutAttribute.Leading, 1, 0));
            ContainerView.AddConstraint(NSLayoutConstraint.Create(ContainerView, NSLayoutAttribute.Trailing, NSLayoutRelation.Equal, PullBarView, NSLayoutAttribute.Trailing, 1, 0));

            var grabView = new UIView(CGRect.Empty);
            grabView.TranslatesAutoresizingMaskIntoConstraints = false;
            PullBarView.AddSubview(grabView);
            PullBarView.AddConstraint(NSLayoutConstraint.Create(grabView, NSLayoutAttribute.CenterX, NSLayoutRelation.Equal, PullBarView, NSLayoutAttribute.CenterX, 1, 0));
            PullBarView.AddConstraint(NSLayoutConstraint.Create(grabView, NSLayoutAttribute.CenterY, NSLayoutRelation.Equal, PullBarView, NSLayoutAttribute.CenterY, 1, 0));
            grabView.AddConstraint(NSLayoutConstraint.Create(grabView, NSLayoutAttribute.Height, NSLayoutRelation.Equal, 1, 4));
            grabView.AddConstraint(NSLayoutConstraint.Create(grabView, NSLayoutAttribute.Width, NSLayoutRelation.Equal, 1, 36));

            grabView.Layer.CornerRadius = 3;
            grabView.Layer.MasksToBounds = true;
            grabView.BackgroundColor = UIColor.FromWhiteAlpha(0.868f, 1);
        }

        void DismissTapped()
        {
            UIView.Animate(0.3, 0, UIViewAnimationOptions.CurveEaseInOut, () =>
            {
                this.View.BackgroundColor = UIColor.Clear;
            }, () => { this.DismissViewController(true, null); });
        }


        private float GetHalfScreen() 
        {
            return (float)(UIScreen.MainScreen.Bounds.Height / 2 + 24);
        }

        public class SheetSize
        {
            public static SheetSize FullScreen = new SheetSize(0);
            public static SheetSize HalfScreen = new SheetSize(-1);

            protected SheetSize(float v)
            {
                this.Value = v;
            }

            public static SheetSize Fixed(float v)
            {
                return new SheetSize(v);
            }

            public float Value { get; private set; }
        }

        public class InitialTouchPanGestureRecognizer : UIPanGestureRecognizer
        {
            public CGPoint initialTouchLocation;

            public InitialTouchPanGestureRecognizer(Action<UIPanGestureRecognizer> action)
            : base(action) { }

            public override void TouchesBegan(NSSet touches, UIEvent evt)
            {
                base.TouchesBegan(touches, evt);
                UITouch touch = touches.AnyObject as UITouch;
                initialTouchLocation = touch.LocationInView(View);
            }
        }
    }
}
