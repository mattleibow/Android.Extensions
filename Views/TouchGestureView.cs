namespace Android.Extensions.Views
{
    using System;

    using Android.Content;
    using Android.Runtime;
    using Android.Util;
    using Android.Views;
    using Android.Widget;

    public class TouchGestureView : HorizontalScrollView,
                                    GestureDetector.IOnDoubleTapListener,
                                    GestureDetector.IOnGestureListener
    {
        private const int VelocityUnitPixelsPerSecond = 1000;

        private GestureDetector _gestureScanner;
        private VelocityTracker _velocityTracker;

        public TouchGestureView(IntPtr javaReference, JniHandleOwnership transfer)
            : base(javaReference, transfer)
        {
            this.Init();
        }

        public TouchGestureView(Context context)
            : base(context)
        {
            this.Init();
        }

        public TouchGestureView(Context context, IAttributeSet attrs, int defStyle)
            : base(context, attrs, defStyle)
        {
            this.Init();
        }

        public TouchGestureView(Context context, IAttributeSet attrs)
            : base(context, attrs)
        {
            this.Init();
        }

        public int MaximumVelocity { get; set; }
        protected int Velocity { get; private set; }

        #region IOnDoubleTapListener Members

        public virtual bool OnDoubleTap(MotionEvent e)
        {
            var args = new TouchEventArgs(false, e);

            if (this.DoubleTap != null)
            {
                this.DoubleTap(this, args);
            }

            return args.Handled;
        }

        public virtual bool OnDoubleTapEvent(MotionEvent e)
        {
            var args = new TouchEventArgs(false, e);

            if (this.DoubleTapEvent != null)
            {
                this.DoubleTapEvent(this, args);
            }

            return args.Handled;
        }

        public virtual bool OnSingleTapConfirmed(MotionEvent e)
        {
            var args = new TouchEventArgs(false, e);

            if (this.SingleTapConfirmed != null)
            {
                this.SingleTapConfirmed(this, args);
            }

            return args.Handled;
        }

        #endregion

        #region IOnGestureListener Members

        public virtual bool OnDown(MotionEvent e)
        {
            var args = new TouchEventArgs(false, e);

            if (this.Down != null)
            {
                this.Down(this, args);
            }

            return args.Handled;
        }

        public virtual bool OnFling(MotionEvent e1, MotionEvent e2, float velocityX, float velocityY)
        {
            return this.OnFling(new FlingEventArgs(false, e1, e2, velocityX, velocityY));
        }

        public virtual void OnLongPress(MotionEvent e)
        {
            var args = new TouchEventArgs(false, e);

            if (this.LongPress != null)
            {
                this.LongPress(this, args);
            }
        }

        public virtual bool OnScroll(MotionEvent e1, MotionEvent e2, float distanceX, float distanceY)
        {
            return this.OnScroll(new ScrollEventArgs(false, e1, e2, distanceX, distanceY));
        }

        public virtual void OnShowPress(MotionEvent e)
        {
            var args = new TouchEventArgs(false, e);

            if (this.ShowPress != null)
            {
                this.ShowPress(this, args);
            }
        }

        public virtual bool OnSingleTapUp(MotionEvent e)
        {
            var args = new TouchEventArgs(false, e);

            if (this.SingleTapUp != null)
            {
                this.SingleTapUp(this, args);
            }

            return args.Handled;
        }

        #endregion

        private void Init()
        {
            this._gestureScanner = new GestureDetector(this);

            this.MaximumVelocity = ViewConfiguration.Get(this.Context).ScaledMaximumFlingVelocity;
        }

        public virtual bool OnFling(FlingEventArgs e)
        {
            if (this.FlingGesture != null)
            {
                this.FlingGesture(this, e);
            }

            return e.Handled;
        }

        public virtual bool OnScroll(ScrollEventArgs e)
        {
            if (this.Scroll != null)
            {
                this.Scroll(this, e);
            }

            return e.Handled;
        }

        public override bool OnTouchEvent(MotionEvent e)
        {
            if (this._velocityTracker == null)
            {
                this._velocityTracker = VelocityTracker.Obtain();
            }

            this._velocityTracker.AddMovement(e);

            VelocityTracker velocityTracker = this._velocityTracker;
            velocityTracker.ComputeCurrentVelocity(VelocityUnitPixelsPerSecond, this.MaximumVelocity);
            this.Velocity = (int)velocityTracker.XVelocity;

            if (e.Action == MotionEventActions.Up)
            {
                if (this._velocityTracker != null)
                {
                    this.Velocity = 0;

                    this._velocityTracker.Recycle();
                    this._velocityTracker = null;
                }
            }

            return this._gestureScanner.OnTouchEvent(e) || base.OnTouchEvent(e);
        }

        // Argument to getVelocity for units to give pixels per second (1 = pixels per millisecond).

        public event EventHandler<TouchEventArgs> DoubleTap;
        public event EventHandler<TouchEventArgs> DoubleTapEvent;
        public event EventHandler<TouchEventArgs> SingleTapConfirmed;
        public event EventHandler<TouchEventArgs> Down;
        public event EventHandler<TouchEventArgs> LongPress;
        public event EventHandler<TouchEventArgs> ShowPress;
        public event EventHandler<ScrollEventArgs> Scroll;
        public event EventHandler<FlingEventArgs> FlingGesture;
        public event EventHandler<TouchEventArgs> SingleTapUp;

        #region Nested type: FlingEventArgs

        public class FlingEventArgs : TouchEventArgs
        {
            public FlingEventArgs(bool handled, MotionEvent e1, MotionEvent e2, float velX, float velY)
                : base(handled, e1)
            {
                this.E2 = e2;
                this.VelocityX = velX;
                this.VelocityY = velY;
            }

            public MotionEvent E2 { get; private set; }
            public float VelocityX { get; private set; }
            public float VelocityY { get; private set; }
        }

        #endregion

        #region Nested type: ScrollEventArgs

        public class ScrollEventArgs : TouchEventArgs
        {
            public ScrollEventArgs(bool handled, MotionEvent e1, MotionEvent e2, float distX, float distY)
                : base(handled, e1)
            {
                this.E2 = e2;
                this.DistanceX = distX;
                this.DistanceY = distY;
            }

            public MotionEvent E2 { get; private set; }
            public float DistanceX { get; private set; }
            public float DistanceY { get; private set; }
        }

        #endregion
    }
}