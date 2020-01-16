using System;
using System.Web;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using MessagingToolkit.QRCode.Codec;

namespace APTemplate
{
    public class QRCodeImageHandler : IHttpHandler
    {
        //Rectangle rect;
        Bitmap QRCodeImg;
        string Id;
        string QRCodeText = "";
        int ImageFormat = 0;
        int ImageSize = 2;
        int EncodeMode = 0;
        int ErrorCorrection = 0;
        int QRCodeVersion = 1;

        #region IHttpHandler 成員

        public bool IsReusable
        {
            get { return true; }
        }

        public void ProcessRequest(HttpContext context)
        {
            HttpApplication app = context.ApplicationInstance;
            Id = app.Request.QueryString["id"];
            if (Id == null)
            {
                
                QRCodeText = app.Request.QueryString["QRCode"];
                ImageFormat = Convert.ToInt16(app.Request.QueryString["ImageFormat"]);
                ImageSize = Convert.ToInt16(app.Request.QueryString["ImageSize"]);
                EncodeMode = Convert.ToInt16(app.Request.QueryString["EncodeMode"]);
                ErrorCorrection = Convert.ToInt16(app.Request.QueryString["ErrorCorrection"]);
                QRCodeVersion = Convert.ToInt16(app.Request.QueryString["QRCodeVersion"]);
            }
            else
            {
                QRCodeText = app.Context.Cache[Id + "_QRCode"].ToString();
                ImageFormat = Convert.ToInt16(app.Context.Cache[Id + "_ImageFormat"]);
                ImageSize = Convert.ToInt16(app.Context.Cache[Id + "_ImageSize"]);
                EncodeMode = Convert.ToInt16(app.Context.Cache[Id + "_EncodeMode"]);
                ErrorCorrection = Convert.ToInt16(app.Context.Cache[Id + "_ErrorCorrection"]);
                QRCodeVersion = Convert.ToInt16(app.Context.Cache[Id + "_QRCodeVersion"]);
                app.Context.Cache.Remove(Id + "_QRCode");
                app.Context.Cache.Remove(Id + "_ImageFormat");
                app.Context.Cache.Remove(Id + "_ImageSize");
                app.Context.Cache.Remove(Id + "_EncodeMode");
                app.Context.Cache.Remove(Id + "_ErrorCorrection");
                app.Context.Cache.Remove(Id + "_QRCodeVersion");
            }
            try
            {
                GenerateQRCodeImage();

                switch (ImageFormat)
                {
                    case 0:
                        QRCodeImg.Save(app.Context.Response.OutputStream, System.Drawing.Imaging.ImageFormat.Jpeg);
                        app.Response.ContentType = "image/jpeg";
                        break;
                    case 1:
                        QRCodeImg.Save(app.Context.Response.OutputStream, System.Drawing.Imaging.ImageFormat.Png);
                        app.Response.ContentType = "image/png";
                        break;
                    case 2:
                        QRCodeImg.Save(app.Context.Response.OutputStream, System.Drawing.Imaging.ImageFormat.Gif);
                        app.Response.ContentType = "image/gif";
                        break;
                    case 3:
                        QRCodeImg.Save(app.Context.Response.OutputStream, System.Drawing.Imaging.ImageFormat.Bmp);
                        app.Response.ContentType = "image/bmp";
                        break;
                    default:
                        QRCodeImg.Save(app.Context.Response.OutputStream, System.Drawing.Imaging.ImageFormat.Jpeg);
                        app.Response.ContentType = "image/jpeg";
                        break;
                }

                QRCodeImg.Dispose();
                app.Response.StatusCode = 200;
                context.ApplicationInstance.CompleteRequest();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void GenerateQRCodeImage()
        {
            MessagingToolkit.QRCode.Codec.QRCodeEncoder qe = new MessagingToolkit.QRCode.Codec.QRCodeEncoder();
            qe.QRCodeEncodeMode = (QRCodeEncoder.ENCODE_MODE)EncodeMode;
            qe.QRCodeErrorCorrect = (QRCodeEncoder.ERROR_CORRECTION)ErrorCorrection;
            // Level 12 L - max 367 alphanumerics
            qe.QRCodeVersion = QRCodeVersion;
            qe.QRCodeScale = ImageSize;
            try
            {
                QRCodeImg = qe.Encode(QRCodeText, System.Text.Encoding.UTF8);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}