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
    [ToolboxData("<{0}:AjaxUpload runat=server></{0}:AjaxUpload>")]
    [ToolboxBitmap(typeof(AjaxUpload), "Resources.Control_upload.bmp")]
    public class AjaxUpload : WebControl
    {
        protected string _UploadDir = "Upload";
        protected bool _IsWithProgress = false;
        protected string _ProgressText = "上傳中,請稍候...";
        protected string _NofileUploadMessage = "No file uploaded";
        protected Unit _ProgressWidth = new Unit(250);
        protected string _ProgressImageUrl = "";
        protected Font _ProgressTextFont = new Font("微軟正黑體", 9, FontStyle.Regular);
        protected List<FileFilterItem> _FileFilterItems = new List<FileFilterItem>();
        protected bool _IsWithProgressPercent = false;
        protected string _ProgressPercentPageUrl = "";
        protected string _ScriptMethodNameForProgressPercent = "doProgressWork";
        protected bool _IsNeedConfirmMessage = false;
        protected string _ConfirmMessage = "";
        protected bool _IsShowUploadButton = true;
        protected bool _IsAllowMultiFiles = false;
        protected bool _IsUseVirtualPath = true;
        protected string sGuid = Guid.NewGuid().ToString();
        protected List<FileFilterItem> _MultiUploadFileFilterItems;

        public AjaxUpload()
        {

        }

        public AjaxUpload(List<FileFilterItem> FileFilterItems)
        {
            _MultiUploadFileFilterItems = FileFilterItems;
        }

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
                if (_MultiUploadFileFilterItems == null)
                {
                    return _FileFilterItems;
                }
                else
                {
                    return _MultiUploadFileFilterItems;
                }
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
        /// 每個上傳元件是否允許多檔上傳。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("每個上傳元件是否允許多檔上傳。")]
        public bool IsAllowMultiFiles
        {
            get
            {
                return _IsAllowMultiFiles;
            }

            set
            {
                _IsAllowMultiFiles = value;
            }
        }

        /// <summary>
        /// 是否使用虛擬目錄方式存放檔案(若為True,將以虛擬目錄結合UploadDir為最後存放目錄路徑,否則以UploadDir指定的目錄路徑)。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("是否使用虛擬目錄方式存放檔案(若為True,將以虛擬目錄結合UploadDir為最後存放目錄路徑,否則以UploadDir指定的目錄路徑)。")]
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
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            RegisterJavaScript.RegisterContolIncludeScript(Page);
        }

        protected override void Render(HtmlTextWriter writer)
        {
            if (!this.DesignMode)
            {
                HttpContext.Current.Cache.Insert(this.ClientID + "_" + sGuid + "_UploadDir", this.UploadDir, null, DateTime.Now.AddMinutes(120), Cache.NoSlidingExpiration);
                HttpContext.Current.Cache.Insert(this.ClientID + "_" + sGuid + "_IsUseVirtualPath", this.IsUseVirtualPath, null, DateTime.Now.AddMinutes(120), Cache.NoSlidingExpiration);
                HttpContext.Current.Cache.Insert(this.ClientID + "_" + sGuid + "_IsWithProgress", this.IsWithProgress, null, DateTime.Now.AddMinutes(120), Cache.NoSlidingExpiration);
                HttpContext.Current.Cache.Insert(this.ClientID + "_" + sGuid + "_IsWithProgressPercent", this.IsWithProgressPercent, null, DateTime.Now.AddMinutes(120), Cache.NoSlidingExpiration);
                HttpContext.Current.Cache.Insert(this.ClientID + "_" + sGuid + "_ProgressText", this.ProgressText, null, DateTime.Now.AddMinutes(120), Cache.NoSlidingExpiration);
                HttpContext.Current.Cache.Insert(this.ClientID + "_" + sGuid + "_NofileUploadMessage", this.NofileUploadMessage, null, DateTime.Now.AddMinutes(120), Cache.NoSlidingExpiration);
                HttpContext.Current.Cache.Insert(this.ClientID + "_" + sGuid + "_ProgressTextFont", this.ProgressTextFont, null, DateTime.Now.AddMinutes(120), Cache.NoSlidingExpiration);
                HttpContext.Current.Cache.Insert(this.ClientID + "_" + sGuid + "_ProgressWidth", this.ProgressWidth, null, DateTime.Now.AddMinutes(120), Cache.NoSlidingExpiration);
                HttpContext.Current.Cache.Insert(this.ClientID + "_" + sGuid + "_ProgressImageUrl", this.ProgressImageUrl.Replace("~/", ""), null, DateTime.Now.AddMinutes(120), Cache.NoSlidingExpiration);
                HttpContext.Current.Cache.Insert(this.ClientID + "_" + sGuid + "_FileFilterItems", this.FileFilterItems, null, DateTime.Now.AddMinutes(120), Cache.NoSlidingExpiration);
                HttpContext.Current.Cache.Insert(this.ClientID + "_" + sGuid + "_ProgressPercentPageUrl", this.ProgressPercentPageUrl.Replace("~/", ""), null, DateTime.Now.AddMinutes(120), Cache.NoSlidingExpiration);
                HttpContext.Current.Cache.Insert(this.ClientID + "_" + sGuid + "_ScriptMethodNameForProgressPercent", this.ScriptMethodNameForProgressPercent.Trim(), null, DateTime.Now.AddMinutes(120), Cache.NoSlidingExpiration);
                HttpContext.Current.Cache.Insert(this.ClientID + "_" + sGuid + "_IsNeedConfirmMessage", this.IsNeedConfirmMessage, null, DateTime.Now.AddMinutes(120), Cache.NoSlidingExpiration);
                HttpContext.Current.Cache.Insert(this.ClientID + "_" + sGuid + "_ConfirmMessage", this.ConfirmMessage, null, DateTime.Now.AddMinutes(120), Cache.NoSlidingExpiration);
                HttpContext.Current.Cache.Insert(this.ClientID + "_" + sGuid + "_IsShowUploadButton", this.IsShowUploadButton, null, DateTime.Now.AddMinutes(120), Cache.NoSlidingExpiration);
                HttpContext.Current.Cache.Insert(this.ClientID + "_" + sGuid + "_IsAllowMultiFiles", this.IsAllowMultiFiles, null, DateTime.Now.AddMinutes(120), Cache.NoSlidingExpiration);
            }
            base.Render(writer);
        }

        protected override void AddAttributesToRender(HtmlTextWriter writer)
        {
            writer.AddAttribute(HtmlTextWriterAttribute.Id, "Frame_" + this.ClientID);
            writer.AddAttribute("frameborder", "0");
            writer.AddAttribute(HtmlTextWriterAttribute.Width,"100%");
            writer.AddAttribute(HtmlTextWriterAttribute.Height, this.Height.Value == 0 ? "28" : this.Height.Value.ToString());
            writer.AddAttribute("marginwidth", "0");
            writer.AddAttribute("marginheight", "0");
            writer.AddAttribute("scrolling", "no");
            writer.AddAttribute("src", "AjaxUpload.aspx?id=" + this.ClientID + "_" + sGuid);
        }
    }
}
