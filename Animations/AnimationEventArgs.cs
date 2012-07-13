namespace Android.Extensions.Animations
{
    using System;
    using Android.Views.Animations;

    public class AnimationEventArgs : EventArgs
    {
        public AnimationEventArgs(Animation animation)
        {
            this.Animation = animation;
        }

        public Animation Animation { get; private set; }
    }
}