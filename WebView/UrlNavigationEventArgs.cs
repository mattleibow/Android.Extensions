namespace Android.Extensions.WebView
{
    using System;

    public class UrlNavigationEventArgs : EventArgs
    {
        public UrlNavigationEventArgs(string url)
        {
            this.Url = url;
        }

        public bool Handled { get; set; }
        public string Url { get; private set; }
    }
}