namespace Android.Extensions.WebView
{
    using System;
    using Android.Webkit;

    public class PageFinishedEventArgs : EventArgs
    {
        public PageFinishedEventArgs(WebView webview, string url)
        {
            this.WebView = webview;
            this.Url = url;
        }

        public WebView WebView { get; private set; }
        public string Url { get; private set; }
    }
}