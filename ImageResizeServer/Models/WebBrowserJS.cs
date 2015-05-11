using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading;
using System.IO;
using System.Reflection;
using EO.WebBrowser;


namespace ImageResizeServer.Models
{
    public class WebBrowserJS
    {
        public WebBrowserJS(string Url, int BrowserWidth, int BrowserHeight)
        {
            this.m_Url = Url;
            this.m_BrowserWidth = BrowserWidth;
            this.m_BrowserHeight = BrowserHeight;
        }

        private string m_Url = null;
        public string Url
        {
            get { return m_Url; }
            set { m_Url = value; }
        }

        private string m_html;
        public string innerHtml
        {
            get { return m_html; }
        }

        private Image m_capture;
        public Image Capture
        {
            get { return m_capture; }
        }

        private string m_PageTitle;
        public string PageTitle
        {
            get { return m_PageTitle; }
        }

        private int m_BrowserWidth;
        public int BrowserWidth
        {
            get { return m_BrowserWidth; }
            set { m_BrowserWidth = value; }
        }

        private int m_BrowserHeight;
        public int BrowserHeight
        {
            get { return m_BrowserHeight; }
            set { m_BrowserHeight = value; }
        }

        private ThreadRunner m_Runner = new ThreadRunner();
        private bool Livre = false;
        private WebView webView;

        public void GenerateWebSite()
        {
            //Create the WebView inside the ThreadRunner thread
            webView = m_Runner.CreateWebView(m_BrowserWidth, m_BrowserHeight);

            //Post the action to the runner's thread to load a page
            //and capture page image
            //http://localhost:49442/Home/Browse?url=http://pread.ly

            m_Runner.Post((WebView webView2, object args) =>
	        {
                    webView.LoadUrlAndWait(m_Url);
                    webView.SetFocus();
                    while (!webView.CanEvalScript)
                        System.Threading.Thread.Sleep(100);

                    webView.EvalScript("function whenReady() { if ($scope.isMyTurn(privilege)) { } else { window.setTimeout(whenReady, 1000);} } window.setTimeout(whenReady, 1);");

		            return null;
	        }, webView, null);

            m_Runner.SetTimeout(1000, () =>
	        {
                    webView.SetFocus();
                    m_capture = webView.Capture();

                    m_PageTitle = webView.Title;
                    var body = webView.GetDOMWindow().document.body.outerHTML;
                    m_html = webView.GetHtml();
                    Livre = true;
             });

            while (!Livre)
                System.Threading.Thread.Sleep(1000);

            webView.Close(true);
        }
    }

}