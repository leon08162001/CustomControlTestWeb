using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Drawing;
using System.Drawing.Design;
using System.Web.UI.WebControls;
using System.Web.Caching;
using MessagingToolkit.QRCode.Codec;

namespace APTemplate
{
    [DefaultProperty("QRCodeText")]
    [ToolboxData("<{0}:QRCode runat=server></{0}:QRCode>")]
    [ToolboxBitmap(typeof(QRCode), "Resources.qrcode.bmp")]
    public class QRCode : CompositeControl, INamingContainer
    {
        public enum QRImageFormat
        {
            JPEG, PNG, GIF, BMP
        }

        public enum QRImageSize
        {
            SS = 1, S = 2, M = 3, L = 4, XL = 5, XXL = 6
        }

        public enum ENCODE_MODE
        {
            ALPHA_NUMERIC = QRCodeEncoder.ENCODE_MODE.ALPHA_NUMERIC,
            NUMERIC = QRCodeEncoder.ENCODE_MODE.NUMERIC,
            BYTE = QRCodeEncoder.ENCODE_MODE.BYTE,
        }

        public enum ERROR_CORRECTION
        {
            L = QRCodeEncoder.ERROR_CORRECTION.L,
            M = QRCodeEncoder.ERROR_CORRECTION.M,
            Q = QRCodeEncoder.ERROR_CORRECTION.Q,
            H = QRCodeEncoder.ERROR_CORRECTION.H,
        }

        public enum QRCodeVersion
        {
            Ver1 = 1, Ver2 = 2, Ver3 = 3, Ver4 = 4, Ver5 = 5,
            Ver6 = 6, Ver7 = 7, Ver8 = 8, Ver9 = 9, Ver10 = 10,
            Ver11 = 11, Ver12 = 12, Ver13 = 13, Ver14 = 14, Ver15 = 15,
            Ver16 = 16, Ver17 = 17, Ver18 = 18, Ver19 = 19, Ver20 = 20,
            Ver21 = 21, Ver22 = 22, Ver23 = 23, Ver24 = 24, Ver25 = 25,
            Ver26 = 26, Ver27 = 27, Ver28 = 28, Ver29 = 29, Ver30 = 30,
            Ver31 = 31, Ver32 = 32, Ver33 = 33, Ver34 = 34, Ver35 = 35,
            Ver36 = 36, Ver37 = 37, Ver38 = 38, Ver39 = 39, Ver40 = 40
        }

        protected QRImageFormat _ImageFormat = QRImageFormat.JPEG;
        protected QRImageSize _ImageSize = QRImageSize.S;
        protected ENCODE_MODE _ENCODE_MODE = ENCODE_MODE.BYTE;
        protected ERROR_CORRECTION _ERROR_CORRECTION = ERROR_CORRECTION.L;
        protected QRCodeVersion _QRCodeVersion = QRCodeVersion.Ver3;
        protected string _QRCodeText = "";
        protected Unit _ImageWidth = 0;
        protected Unit _ImageHeight = 0;
        protected Table Table1 = new Table();
        protected TableRow TableRow1 = new TableRow();
        protected TableCell TableCell1 = new TableCell();
        protected System.Web.UI.WebControls.Image QRCodeImg = new System.Web.UI.WebControls.Image();

        #region Public Properties
        /// <summary>
        /// QRCode控制項的QRCode碼。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("QRCode控制項的QRCode碼。")]
        public String QRCodeText
        {
            get { return _QRCodeText; }
            set { _QRCodeText = value; }
        }
        /// <summary>
        /// QRCode條碼圖形的尺寸。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("QRCode條碼圖形的尺寸。")]
        public QRImageSize ImageSize
        {
            get { return _ImageSize; }
            set { _ImageSize = value; }
        }
        /// <summary>
        /// QRCode條碼圖形的寬度(預設0根據ImageSize及Version設定自動決定寬度)。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("QRCode條碼圖形的寬度(預設0根據ImageSize及Version設定自動決定寬度)。")]
        public Unit ImageWidth
        {
            get { return _ImageWidth; }
            set { _ImageWidth = value; }
        }
        /// <summary>
        /// QRCode條碼圖形的高度(預設0根據ImageSize及Version設定自動決定高度)。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("QRCode條碼圖形的高度(預設0根據ImageSize及Version設定自動決定高度)。")]
        public Unit ImageHeight
        {
            get { return _ImageHeight; }
            set { _ImageHeight = value; }
        }
        /// <summary>
        /// 產生QRCode圖檔的格式。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("產生QRCode圖檔的格式。")]
        public QRImageFormat ImageFormat
        {
            get { return _ImageFormat; }
            set { _ImageFormat = value; }
        }
        [DefaultValue(""),
         Category("自訂"),
         Description("QRCode控制項的編碼方式。")]
        public ENCODE_MODE EncodeMode
        {
            get { return _ENCODE_MODE; }
            set { _ENCODE_MODE = value; }
        }
        [DefaultValue(""),
         Category("自訂"),
         Description("QRCode控制項的錯誤糾正。")]
        public ERROR_CORRECTION ErrorCorrection
        {
            get { return _ERROR_CORRECTION; }
            set { _ERROR_CORRECTION = value; }
        }
         [DefaultValue(""),
         Category("自訂"),
         Description("QRCode控制項的接受的資料長度版本。")]
        public QRCodeVersion Version
        {
            get { return _QRCodeVersion; }
            set { _QRCodeVersion = value; }
        }
        
        #endregion

        #region Protected Methods

         protected override void CreateChildControls()
         {
             EnsureChildControls();
             //QRCodeImg
             string ImgUrl = "";
             if (this.DesignMode)
             {
                 ImgUrl += "QRCodeImage.aspx?QRCode=" + this.QRCodeText + "&ImageFormat=" + Convert.ToInt16(this.ImageFormat) + "&ImageSize=" + Convert.ToInt16(this.ImageSize) + "&EncodeMode=" + Convert.ToInt16(this.EncodeMode) + "&ErrorCorrection=" + Convert.ToInt16(this.ErrorCorrection) + "&QRCodeVersion=" + Convert.ToInt16(this.Version);
             }
             else
             {
                 string sGuid = Guid.NewGuid().ToString();
                 HttpContext.Current.Cache.Insert(this.ClientID + "_" + sGuid + "_QRCode", this.QRCodeText, null, DateTime.Now.AddMinutes(20), Cache.NoSlidingExpiration);
                 HttpContext.Current.Cache.Insert(this.ClientID + "_" + sGuid + "_ImageFormat", Convert.ToInt16(this.ImageFormat), null, DateTime.Now.AddMinutes(20), Cache.NoSlidingExpiration);
                 HttpContext.Current.Cache.Insert(this.ClientID + "_" + sGuid + "_ImageSize", Convert.ToInt16(this.ImageSize), null, DateTime.Now.AddMinutes(20), Cache.NoSlidingExpiration);
                 HttpContext.Current.Cache.Insert(this.ClientID + "_" + sGuid + "_EncodeMode", Convert.ToInt16(this.EncodeMode), null, DateTime.Now.AddMinutes(20), Cache.NoSlidingExpiration);
                 HttpContext.Current.Cache.Insert(this.ClientID + "_" + sGuid + "_ErrorCorrection", Convert.ToInt16(this.ErrorCorrection), null, DateTime.Now.AddMinutes(20), Cache.NoSlidingExpiration);
                 HttpContext.Current.Cache.Insert(this.ClientID + "_" + sGuid + "_QRCodeVersion", Convert.ToInt16(this.Version), null, DateTime.Now.AddMinutes(20), Cache.NoSlidingExpiration);
                 ImgUrl += "QRCodeImage.aspx?id=" + this.ClientID + "_" + sGuid;
             }
             QRCodeImg.ImageUrl = ImgUrl;
             QRCodeImg.AlternateText = "產出QRCode影像時發生錯誤";
             if (ImageWidth != Unit.Pixel(0))
             {
                 QRCodeImg.Width = this.ImageWidth;
             }
             if (ImageHeight != Unit.Pixel(0))
             {
                 QRCodeImg.Height = this.ImageHeight;
             }
             QRCodeImg.BorderWidth = Unit.Pixel(0);

             //TableCell1
             TableCell1.Controls.Add(QRCodeImg);

             //TableRow1
             TableRow1.Controls.Add(TableCell1);
             //Table1
             if (!this.DesignMode)
             {
                 if (Context.Request.Browser.Type.IndexOf("IE") != -1 && float.Parse(Context.Request.Browser.Version) < 8)
                 {
                     Table1.Attributes["style"] += "display:inline;vertical-align:top;";
                 }
                 else
                 {
                     Table1.Attributes["style"] += "display:inline-block;vertical-align:top;";
                 }
             }
             else
             {
                 Table1.Attributes["style"] += "display:inline;vertical-align:top;";
             }
             Table1.CssClass = CssClass;
             Table1.Controls.Add(TableRow1);

             this.Controls.Add(Table1);
         }

         //protected override void Render(HtmlTextWriter writer)
         //{
         //    if (!this.DesignMode)
         //    {
         //        HttpContext.Current.Cache.Insert(this.ClientID + "_QRCode", this.QRCodeText, null, DateTime.Now.AddMinutes(600), Cache.NoSlidingExpiration);
         //    }
         //    base.Render(writer);
         //}
         protected override void RenderContents(HtmlTextWriter writer)
         {
             Table1.RenderControl(writer);
         }

        #endregion         
    }
}
