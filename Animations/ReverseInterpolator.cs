namespace Android.Extensions.Animations
{
    using System;
    using Android.Views.Animations;

    public class ReverseInterpolator : Java.Lang.Object, IInterpolator
    {
        private readonly IInterpolator _iterpolator;

        public ReverseInterpolator(IInterpolator iterpolator)
        {
            this._iterpolator = iterpolator;
        }

        public float GetInterpolation(float input)
        {
            return this._iterpolator.GetInterpolation(Math.Abs(input - 1f));
        }
    }
}