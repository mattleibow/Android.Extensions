namespace Android.Extensions.Animations
{
    using System;
    using Android.Views.Animations;

    public class AnimationListener : Java.Lang.Object, Animation.IAnimationListener
    {
        public void OnAnimationEnd(Animation animation)
        {
            if (this.AnimationEnd != null)
            {
                this.AnimationEnd(this, new AnimationEventArgs(animation));
            }
        }

        public void OnAnimationRepeat(Animation animation)
        {
            if (this.AnimationRepeat != null)
            {
                this.AnimationRepeat(this, new AnimationEventArgs(animation));
            }
        }

        public void OnAnimationStart(Animation animation)
        {
            if (this.AnimationStart != null)
            {
                this.AnimationStart(this, new AnimationEventArgs(animation));
            }
        }

        public event EventHandler<AnimationEventArgs> AnimationEnd;
        public event EventHandler<AnimationEventArgs> AnimationRepeat;
        public event EventHandler<AnimationEventArgs> AnimationStart;
    }
}