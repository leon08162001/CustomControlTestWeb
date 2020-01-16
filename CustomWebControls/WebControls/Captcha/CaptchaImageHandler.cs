using System;
using System.Web;
using System.Drawing;

namespace APTemplate
{
    public class CaptchaImageHandler : IHttpHandler
    {

        #region IHttpHandler 成員

        public bool IsReusable
        {
            get { return true; }
        }

        public void ProcessRequest(HttpContext context)
        {
            HttpApplication app = context.ApplicationInstance;

            //get the unique GUID of the captcha; this must be passed in via the querystring
            string guid = app.Request.QueryString["guid"];
            CaptchaImage ci = (CaptchaImage)(HttpRuntime.Cache.Get(guid));

            //write the image to the HTTP output stream as an array of bytes
            Bitmap b = ci.RenderImage();
            b.Save(app.Context.Response.OutputStream, System.Drawing.Imaging.ImageFormat.Jpeg);
            b.Dispose();
            app.Response.ContentType = "image/jpeg";
            app.Response.StatusCode = 200;
            context.ApplicationInstance.CompleteRequest();
        }
        #endregion
    }
}