using System;
using System.ComponentModel;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Runtime;
using Android.Support.Design.BottomAppBar;
using Android.Support.Design.Widget;
using Android.Support.V4.Content;
using Android.Support.V4.View;
using Android.Util;
using Android.Views;
using Android.Widget;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms.Platform.Android.AppCompat;
using XamFood.Custom;
using XamFood.Droid.Renderers;

namespace XamFood.Droid.Renderers
{
    public class CustomBottomNavigationView : BottomNavigationView
    {
        private Path mPath;
        private Paint mPaint;
        BottomAppBar bottomAppBar;
        /** the CURVE_CIRCLE_RADIUS represent the radius of the fab button */
        private int CURVE_CIRCLE_RADIUS = 128 / 2;
        // the coordinates of the first curve
        private Point mFirstCurveStartPoint = new Point();
        private Point mFirstCurveEndPoint = new Point();
        private Point mFirstCurveControlPoint1 = new Point();
        private Point mFirstCurveControlPoint2 = new Point();

        //the coordinates of the second curve
        private Point mSecondCurveStartPoint = new Point();
        private Point mSecondCurveEndPoint = new Point();
        private Point mSecondCurveControlPoint1 = new Point();
        private Point mSecondCurveControlPoint2 = new Point();
        private int mNavigationBarWidth;
        private int mNavigationBarHeight;

        public CustomBottomNavigationView(Context context) : base(context)
        {
            init();
        }

        public CustomBottomNavigationView(Context context, IAttributeSet attrs) : base(context, attrs)
        {
            init();
        }

        public CustomBottomNavigationView(Context context, IAttributeSet attrs, int defStyleAttr) : base(context, attrs, defStyleAttr)
        {
            init();
        }

        protected CustomBottomNavigationView(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
            init();
        }

        private void init()
        {
            mPath = new Path();
            mPaint = new Paint();
            mPaint.SetStyle(Paint.Style.FillAndStroke);
            mPaint.Color = Color.Black;
            SetBackgroundColor(Color.Transparent);
        }

        protected override void OnLayout(bool changed, int left, int top, int right, int bottom)
        {
            base.OnLayout(changed, left, top, right, bottom);
        }

        protected override void OnSizeChanged(int w, int h, int oldw, int oldh)
        {
            base.OnSizeChanged(w, h, oldw, oldh);
            // get width and height of navigation bar
            // Navigation bar bounds (width & height)
            mNavigationBarWidth = Width;
            mNavigationBarHeight = Height;
            // the coordinates (x,y) of the start point before curve
            mFirstCurveStartPoint.Set((mNavigationBarWidth / 2) - (CURVE_CIRCLE_RADIUS * 2) - (CURVE_CIRCLE_RADIUS / 3), 0);
            // the coordinates (x,y) of the end point after curve
            mFirstCurveEndPoint.Set(mNavigationBarWidth / 2, CURVE_CIRCLE_RADIUS + (CURVE_CIRCLE_RADIUS / 4));
            // same thing for the second curve
            mSecondCurveStartPoint = mFirstCurveEndPoint;
            mSecondCurveEndPoint.Set((mNavigationBarWidth / 2) + (CURVE_CIRCLE_RADIUS * 2) + (CURVE_CIRCLE_RADIUS / 3), 0);

            // the coordinates (x,y)  of the 1st control point on a cubic curve
            mFirstCurveControlPoint1.Set(mFirstCurveStartPoint.X + CURVE_CIRCLE_RADIUS + (CURVE_CIRCLE_RADIUS / 4), mFirstCurveStartPoint.Y);
            // the coordinates (x,y)  of the 2nd control point on a cubic curve
            mFirstCurveControlPoint2.Set(mFirstCurveEndPoint.X - (CURVE_CIRCLE_RADIUS * 2) + CURVE_CIRCLE_RADIUS, mFirstCurveEndPoint.Y);

            mSecondCurveControlPoint1.Set(mSecondCurveStartPoint.X + (CURVE_CIRCLE_RADIUS * 2) - CURVE_CIRCLE_RADIUS, mSecondCurveStartPoint.Y);
            mSecondCurveControlPoint2.Set(mSecondCurveEndPoint.X - (CURVE_CIRCLE_RADIUS + (CURVE_CIRCLE_RADIUS / 4)), mSecondCurveEndPoint.Y);

            mPath.Reset();
            mPath.MoveTo(0, 0);
            mPath.LineTo(mFirstCurveStartPoint.X, mFirstCurveStartPoint.Y);

            mPath.CubicTo(mFirstCurveControlPoint1.X, mFirstCurveControlPoint1.Y,
                    mFirstCurveControlPoint2.X, mFirstCurveControlPoint2.Y,
                    mFirstCurveEndPoint.X, mFirstCurveEndPoint.Y);

            mPath.CubicTo(mSecondCurveControlPoint1.X, mSecondCurveControlPoint1.Y,
                    mSecondCurveControlPoint2.X, mSecondCurveControlPoint2.Y,
                    mSecondCurveEndPoint.X, mSecondCurveEndPoint.Y);

            mPath.LineTo(mNavigationBarWidth, 0);
            mPath.LineTo(mNavigationBarWidth, mNavigationBarHeight);
            mPath.LineTo(0, mNavigationBarHeight);
            mPath.Close();
        }

        protected override void OnDraw(Canvas canvas)
        {
            base.OnDraw(canvas);
            canvas.DrawPath(mPath, mPaint);
        }
    }
}
