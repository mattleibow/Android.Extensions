namespace Android.Extensions.WebView
{
    using System;
    using Android.Webkit;

    public class ReceivedErrorEventArgs : EventArgs
    {
        public WebView WebView { get; private set; }
        public ClientError ErrorCode { get; private set; }
        public string Description { get; private set; }
        public string FailingUrl { get; private set; }

        public ReceivedErrorEventArgs(WebView webView, ClientError errorCode, string description, string failingUrl)
        {
            this.WebView = webView;
            this.ErrorCode = errorCode;
            this.Description = description;
            this.FailingUrl = failingUrl;
        }
    }
}