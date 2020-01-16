using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.Design;
using System.Drawing.Design;
using System.Web.Caching;
using System.Drawing;

namespace APTemplate
{
    [DefaultProperty("Text")]
    [ToolboxData("<{0}:Upload runat=server></{0}:Upload>")]
    [ToolboxBitmap(typeof(Upload), "Resources.Control_upload.bmp")]
    public class Upload : WebControl
    {
        protected string _UploadDir = "Upload";
        protected bool _IsWithProgress = false;
        protected string _ProgressText = "上傳中,請稍候...";
        protected string _NofileUploadMessage = "No file uploaded";
        protected Unit _ProgressWidth = new Unit(250);
        protected string _ProgressImageUrl = "";
        protected Font _ProgressTextFont = new Font("微軟正黑體", 9, FontStyle.Regular);
        protected List<FileFilterItem> _Items = new List<FileFilterItem>();
        protected bool _IsWithProgressPercent = false;
        protected string _ProgressPercentPageUrl = "";
        protected string _ScriptMethodNameForProgressPercent = "";
        protected bool _IsNeedConfirmMessage = false;
        protected string _ConfirmMessage = "";
        protected bool _IsShowUploadButton = true;
        protected bool _IsUseVirtualPath = true;

        /// <summary>
        ///上傳檔案存檔資料夾。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Bindable(true),
         Description("上傳檔案存檔資料夾。")]
        public string UploadDir
        {
            get { return _UploadDir; }
            set { _UploadDir = value; }
        }

        /// <summary>
        /// 上傳檔案篩選項目。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("上傳檔案篩選項目。")]
        [PersistenceMode(PersistenceMode.InnerProperty), DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [NotifyParentProperty(true)]
        [Editor(typeof(FileFilterItemCollectionEditor), typeof(UITypeEditor))]
        public List<FileFilterItem> FileFilterItems
        {
            get
            {
                return _Items;
            }
        }

        /// <summary>
        /// 上傳時是否顯示ProgressBar。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("上傳時是否顯示ProgressBar。")]
        public bool IsWithProgress
        {
            get
            {
                return _IsWithProgress;
            }

            set
            {
                _IsWithProgress = value;
            }
        }

        /// <summary>
        /// 上傳時是否顯示進度百分比(若為true,須另外寫ajax程式)。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("上傳時是否顯示進度百分比(若為true,須另外寫ajax程式)。")]
        public bool IsWithProgressPercent
        {
            get
            {
                return _IsWithProgressPercent;
            }

            set
            {
                _IsWithProgressPercent = value;
            }
        }

        /// <summary>
        /// 上傳前是否顯示確認上傳訊息。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("上傳前是否顯示確認上傳訊息。")]
        public bool IsNeedConfirmMessage
        {
            get
            {
                return _IsNeedConfirmMessage;
            }

            set
            {
                _IsNeedConfirmMessage = value;
            }
        }

        /// <summary>
        /// 是否顯示上傳按鈕。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("是否顯示上傳按鈕。")]
        public bool IsShowUploadButton
        {
            get
            {
                return _IsShowUploadButton;
            }

            set
            {
                _IsShowUploadButton = value;
            }
        }


        /// <summary>
        /// 是否使用虛擬目錄方式存放檔案(若為True,將以虛擬目錄結合UploadDir為最後存放目錄路徑,否則以UploadDir指定的目錄路徑)。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("是否顯示上傳按鈕。")]
        public bool IsUseVirtualPath
        {
            get
            {
                return _IsUseVirtualPath;
            }
            set
            {
                _IsUseVirtualPath = value;
            }
        }

        /// <summary>
        ///上傳進度顯示的文字。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Bindable(true),
         Description("上傳進度顯示的文字。")]
        public string ProgressText
        {
            get { return _ProgressText; }
            set { _ProgressText = value; }
        }

        /// <summary>
        ///沒有檔案上傳時顯示的訊息。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Bindable(true),
         Description("沒有檔案上傳時顯示的訊息。")]
        public string NofileUploadMessage
        {
            get { return _NofileUploadMessage; }
            set { _NofileUploadMessage = value; }
        }

        /// <summary>
        ///確認上傳訊息文字。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Bindable(true),
         Description("確認上傳訊息文字。")]
        public string ConfirmMessage
        {
            get { return _ConfirmMessage; }
            set { _ConfirmMessage = value; }
        }

        /// <summary>
        /// Progress的寬度。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("Progress的寬度。")]
        public Unit ProgressWidth
        {
            get
            {
                return _ProgressWidth;
            }
            set { _ProgressWidth = value; }
        }

         /// <summary>
        ///處理ProgressPercent功能的網頁位置。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("處理ProgressPercent功能的網頁位置。")]
        [UrlProperty("*.aspx")]
        [Editor(typeof(UrlEditor), typeof(UITypeEditor))]
        public string ProgressPercentPageUrl
        {
            get { return _ProgressPercentPageUrl; }
            set { _ProgressPercentPageUrl = value; }
        }

        /// <summary>
        ///ProgressBar圖片位置。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("Progress圖片位置。")]
        [UrlProperty()]
        [Editor(typeof(ImageUrlEditor), typeof(UITypeEditor))]
        public string ProgressImageUrl
        {
            get { return _ProgressImageUrl; }
            set { _ProgressImageUrl = value; }
        }

        /// <summary>
        ///Progress文字字型。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("Progress文字字型。"),
         TypeConverter(typeof(FontConverter)),
         Editor(typeof(FontEditor), typeof(UITypeEditor))]
        public Font ProgressTextFont
        {
            get { return _ProgressTextFont; }
            set { _ProgressTextFont = value; }
        }

         /// <summary>
        ///執行ProgressPercent功能所需的Javascript方法名稱。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("執行ProgressPercent功能所需的Javascript方法名稱。")]
        public string ScriptMethodNameForProgressPercent
        {
            get { return _ScriptMethodNameForProgressPercent; }
            set { _ScriptMethodNameForProgressPercent = value; }
        }
        
        protected override HtmlTextWriterTag TagKey
        {
            get
            {
                return HtmlTextWriterTag.Iframe;
            }
        }

        protected override void OnInit(EventArgs e)
        {
            Page.RegisterRequiresControlState(this);
            base.OnInit(e);
        }

        protected override void Render(HtmlTextWriter writer)
        {
            if (!this.DesignMode)
            {
                //Context.Cache[this.ClientID + "_UploadDir"] = this.UploadDir;
                //Context.Cache[this.ClientID + "_IsUseVirtualPath"] = this.IsUseVirtualPath;
                //Context.Cache[this.ClientID + "_IsWithProgress"] = this.IsWithProgress;
                //Context.Cache[this.ClientID + "_IsWithProgressPercent"] = this.IsWithProgressPercent;
                //Context.Cache[this.ClientID + "_ProgressText"] = this.ProgressText;
                //Context.Cache[this.ClientID + "_NofileUploadMessage"] = this.NofileUploadMessage;
                //Context.Cache[this.ClientID + "_ProgressTextFont"] = this.ProgressTextFont;
                //Context.Cache[this.ClientID + "_ProgressWidth"] = this.ProgressWidth;
                //Context.Cache[this.ClientID + "_ProgressImageUrl"] = this.ProgressImageUrl.Replace("~/", "");
                //Context.Cache[this.ClientID + "_FileFilterItems"] = this.FileFilterItems;
                //Context.Cache[this.ClientID + "_ProgressPercentPageUrl"] = this.ProgressPercentPageUrl.Replace("~/", "");
                //Context.Cache[this.ClientID + "_ScriptMethodNameForProgressPercent"] = this.ScriptMethodNameForProgressPercent.Trim();
                //Context.Cache[this.ClientID + "_IsNeedConfirmMessage"] = this.IsNeedConfirmMessage;
                //Context.Cache[this.ClientID + "_ConfirmMessage"] = this.ConfirmMessage;
                //Context.Cache[this.ClientID + "_IsShowUploadButton"] = this.IsShowUploadButton;
                HttpContext.Current.Cache.Insert(this.ClientID + "_UploadDir", this.UploadDir, null, DateTime.Now.AddMinutes(600), Cache.NoSlidingExpiration);
                HttpContext.Current.Cache.Insert(this.ClientID + "_IsUseVirtualPath", this.IsUseVirtualPath, null, DateTime.Now.AddMinutes(600), Cache.NoSlidingExpiration);
                HttpContext.Current.Cache.Insert(this.ClientID + "_IsWithProgress", this.IsWithProgress, null, DateTime.Now.AddMinutes(600), Cache.NoSlidingExpiration);
                HttpContext.Current.Cache.Insert(this.ClientID + "_IsWithProgressPercent", this.IsWithProgressPercent, null, DateTime.Now.AddMinutes(600), Cache.NoSlidingExpiration);
                HttpContext.Current.Cache.Insert(this.ClientID + "_ProgressText", this.ProgressText, null, DateTime.Now.AddMinutes(600), Cache.NoSlidingExpiration);
                HttpContext.Current.Cache.Insert(this.ClientID + "_NofileUploadMessage", this.NofileUploadMessage, null, DateTime.Now.AddMinutes(600), Cache.NoSlidingExpiration);
                HttpContext.Current.Cache.Insert(this.ClientID + "_ProgressTextFont", this.ProgressTextFont, null, DateTime.Now.AddMinutes(600), Cache.NoSlidingExpiration);
                HttpContext.Current.Cache.Insert(this.ClientID + "_ProgressWidth", this.ProgressWidth, null, DateTime.Now.AddMinutes(600), Cache.NoSlidingExpiration);
                HttpContext.Current.Cache.Insert(this.ClientID + "_ProgressImageUrl", this.ProgressImageUrl.Replace("~/", ""), null, DateTime.Now.AddMinutes(600), Cache.NoSlidingExpiration);
                HttpContext.Current.Cache.Insert(this.ClientID + "_FileFilterItems", this.FileFilterItems, null, DateTime.Now.AddMinutes(600), Cache.NoSlidingExpiration);
                HttpContext.Current.Cache.Insert(this.ClientID + "_ProgressPercentPageUrl", this.ProgressPercentPageUrl.Replace("~/", ""), null, DateTime.Now.AddMinutes(600), Cache.NoSlidingExpiration);
                HttpContext.Current.Cache.Insert(this.ClientID + "_ScriptMethodNameForProgressPercent", this.ScriptMethodNameForProgressPercent.Trim(), null, DateTime.Now.AddMinutes(600), Cache.NoSlidingExpiration);
                HttpContext.Current.Cache.Insert(this.ClientID + "_IsNeedConfirmMessage", this.IsNeedConfirmMessage, null, DateTime.Now.AddMinutes(600), Cache.NoSlidingExpiration);
                HttpContext.Current.Cache.Insert(this.ClientID + "_ConfirmMessage", this.ConfirmMessage, null, DateTime.Now.AddMinutes(600), Cache.NoSlidingExpiration);
                HttpContext.Current.Cache.Insert(this.ClientID + "_IsShowUploadButton", this.IsShowUploadButton, null, DateTime.Now.AddMinutes(600), Cache.NoSlidingExpiration);
            }
            base.Render(writer);
        }

        protected override void AddAttributesToRender(HtmlTextWriter writer)
        {
            writer.AddAttribute(HtmlTextWriterAttribute.Id, "Frame_" + this.ClientID);
            writer.AddAttribute("frameborder", "0");
            //if (this.IsWithProgress)
            //{
            //    //writer.AddAttribute(HtmlTextWriterAttribute.Width, this.Width.Value == 0 ? (430 + this.ProgressWidth.Value).ToString() : (this.Width.Value + this.ProgressWidth.Value).ToString());
            //}
            //else
            //{
            //    //writer.AddAttribute(HtmlTextWriterAttribute.Width, this.Width.Value == 0 ? "590" : this.Width.Value.ToString());
            //}
            writer.AddAttribute(HtmlTextWriterAttribute.Width,"100%");
            writer.AddAttribute(HtmlTextWriterAttribute.Height, this.Height.Value == 0 ? "28" : this.Height.Value.ToString());
            writer.AddAttribute("marginwidth", "0");
            writer.AddAttribute("marginheight", "0");
            writer.AddAttribute("scrolling", "no");
            writer.AddAttribute("src", "Upload.aspx?id=" + this.ClientID);
        }
    }
}
