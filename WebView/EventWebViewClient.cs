namespace Android.Extensions.WebView
{
    using System;
    using Android.Webkit;

    public class EventWebViewClient : WebViewClient
    {
        public override bool ShouldOverrideUrlLoading(WebView view, string url)
        {
            var args = new UrlNavigationEventArgs(url) { Handled = false };
            
            this.OnWebLinkClicked(args);
            
            return args.Handled;
        }

        public event EventHandler<UrlNavigationEventArgs> WebLinkClicked;

        private void OnWebLinkClicked(UrlNavigationEventArgs e)
        {
            var handler = this.WebLinkClicked;

            if (handler != null)
            {
                handler(this, e);
            }
        }

        public override void OnPageFinished(WebView view, string url)
        {
            base.OnPageFinished(view, url);

            this.OnPageFinished(new PageFinishedEventArgs(view, url));
        }

        public event EventHandler<PageFinishedEventArgs> PageFinished;

        private void OnPageFinished(PageFinishedEventArgs e)
        {
            var handler = this.PageFinished;

            if (handler != null)
            {
                handler(this, e);
            }
        }

        public override void OnReceivedError(WebView view, ClientError errorCode, string description, string failingUrl)
        {
            this.OnReceivedError(new ReceivedErrorEventArgs(view, errorCode, description, failingUrl));
        }

        public event EventHandler<ReceivedErrorEventArgs> ReceivedError;

        private void OnReceivedError(ReceivedErrorEventArgs e)
        {
            var handler = this.ReceivedError;

            if (handler != null)
            {
                handler(this, e);
            }
        }
    }
}