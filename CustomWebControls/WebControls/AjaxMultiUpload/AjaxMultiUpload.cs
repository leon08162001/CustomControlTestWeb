using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.Design;
using System.Drawing.Design;
using System.Web.Caching;
using System.Drawing;
using System.IO;

namespace APTemplate
{
    [DefaultProperty("Text")]
    [ToolboxData("<{0}:AjaxMultiUpload runat=server></{0}:AjaxMultiUpload>")]
    [ToolboxBitmap(typeof(AjaxMultiUpload), "Resources.Control_upload.bmp")]
    public class AjaxMultiUpload : CompositeControl, IPostBackEventHandler
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
        protected string _ScriptMethodNameForProgressPercent = "doProgressWork";
        protected bool _IsNeedConfirmMessage = false;
        protected string _ConfirmMessage = "";
        protected bool _IsShowUploadButton = true;
        protected bool _IsAllowMultiFiles = false;
        protected bool _IsUseVirtualPath = true;
        protected byte _UploadNmbers = 1;
        protected bool _IsTriggerUploadFilesFinishedEvent = false;
        protected bool _IsCausesValidation = false;
        protected string _ValidationGroup = "";

        // Delegate  
        public delegate void MultiUploadFilesFinishedEventHandler(object sender, MultiUploadFilesFinishedEventArgs e);
        /// <summary>
        /// 多檔上傳全部完成時的事件
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("多檔上傳全部完成時的事件。")]
        public event MultiUploadFilesFinishedEventHandler MultiUploadFilesFinished;

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

        /// <summary>
        /// 上傳元件的數目。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("上傳元件的數目。")]
        public byte UploadNmbers
        {
            get { return _UploadNmbers; }
            set { _UploadNmbers = value; }
        }

        /// <summary>
        /// 上傳完畢時是否觸發完成事件。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("上傳完畢時是否觸發完成事件。")]
        public bool IsTriggerUploadFilesFinishedEvent
        {
            get
            {
                return _IsTriggerUploadFilesFinishedEvent;
            }

            set
            {
                _IsTriggerUploadFilesFinishedEvent = value;
            }
        }
        /// <summary>
        /// 按鈕是否導致引發驗證。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("按鈕是否導致引發驗證。")]
        public bool CausesValidation
        {
            get
            {
                return _IsCausesValidation;
            }

            set
            {
                _IsCausesValidation = value;
            }
        }
        /// <summary>
        /// 驗證群組名稱。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("驗證群組名稱。")]
        public virtual String ValidationGroup
        {
            get
            {
                return _ValidationGroup;
            }

            set
            {
                _ValidationGroup = value;
            }
        }

        /// <summary>
        /// 多檔上傳全部完成時事件
        /// </summary>
        protected virtual void OnMultiUploadFilesFinished(object sender, MultiUploadFilesFinishedEventArgs e)
        {
            if (MultiUploadFilesFinished != null)
            {
                MultiUploadFilesFinished(sender, e);
            }
        }

        protected override HtmlTextWriterTag TagKey
        {
            get
            {
                return HtmlTextWriterTag.Div;
            }
        }

        protected override void OnInit(EventArgs e)
        {
            Page.RegisterRequiresControlState(this);
        }

        protected override void CreateChildControls()
        {
            if (this.DesignMode)
            {
                CreateAjaxUploadControls();
            }
        }

        protected override void  OnPreRender(EventArgs e)
        {
            if (!this.DesignMode)
            {
                CreateAjaxUploadControls();
            }
        }

        protected void CreateAjaxUploadControls()
        {
            EnsureChildControls();
            for (byte i = 1; i <= _UploadNmbers; i++)
            {
                AjaxUpload UploadCtrl = new AjaxUpload(this.FileFilterItems);
                UploadCtrl.UploadDir = this.UploadDir;
                UploadCtrl.IsWithProgress = this.IsWithProgress;
                UploadCtrl.IsWithProgressPercent = this.IsWithProgressPercent;
                //UploadCtrl.IsNeedConfirmMessage = this.IsNeedConfirmMessage;
                UploadCtrl.IsShowUploadButton = this.IsShowUploadButton;
                UploadCtrl.IsAllowMultiFiles = this.IsAllowMultiFiles;
                UploadCtrl.IsUseVirtualPath = this.IsUseVirtualPath;
                UploadCtrl.ProgressText = this.ProgressText;
                UploadCtrl.NofileUploadMessage = this.NofileUploadMessage;
                //UploadCtrl.ConfirmMessage = this.ConfirmMessage;
                UploadCtrl.ProgressWidth = this.ProgressWidth;
                UploadCtrl.ProgressPercentPageUrl = this.ProgressPercentPageUrl;
                UploadCtrl.ProgressImageUrl = this.ProgressImageUrl;
                UploadCtrl.ProgressTextFont = this.ProgressTextFont;
                UploadCtrl.ScriptMethodNameForProgressPercent = this.ScriptMethodNameForProgressPercent;
                this.Controls.Add(UploadCtrl);
            }

            Button uploadBtn = new Button();
            uploadBtn.ID = "Button1";
            uploadBtn.Text = "上傳";
            uploadBtn.CausesValidation = this.CausesValidation;
            uploadBtn.ValidationGroup = this.ValidationGroup;
            if (this.CausesValidation)
            {
                uploadBtn.OnClientClick = "if(!Page_ClientValidate('" + uploadBtn.ValidationGroup + "')){ return false;}";
            }
            if (_IsTriggerUploadFilesFinishedEvent)
            {
                HtmlInputHidden HidUploadedFiles = new HtmlInputHidden();
                this.Controls.Add(HidUploadedFiles);

                HtmlLink LinkTriggerUploadedFilesEvent = new HtmlLink();
                LinkTriggerUploadedFilesEvent.Attributes["onclick"] = this.Page.ClientScript.GetPostBackEventReference(this, HidUploadedFiles.UniqueID) + ";return false;";
                LinkTriggerUploadedFilesEvent.Attributes["style"] = "display:none;";
                this.Controls.Add(LinkTriggerUploadedFilesEvent);
                if (this.IsNeedConfirmMessage)
                {
                    CreateConfirmScript();
                    uploadBtn.OnClientClick += "if(doConfirm_" + this.ClientID + "()){upLoad(this,document.getElementById('" + HidUploadedFiles.ClientID + "'),document.getElementById('" + LinkTriggerUploadedFilesEvent.ClientID + "'));}return false;";
                }
                else
                {
                    uploadBtn.OnClientClick += "upLoad(this,document.getElementById('" + HidUploadedFiles.ClientID + "'),document.getElementById('" + LinkTriggerUploadedFilesEvent.ClientID + "'));return false;";
                }
                //uploadBtn.OnClientClick = "upLoad(this,document.getElementById('" + HidUploadedFiles.ClientID + "'),document.getElementById('" + LinkTriggerUploadedFilesEvent.ClientID + "'));return false;";
            }
            else
            {
                if (this.IsNeedConfirmMessage)
                {
                    CreateConfirmScript();
                    uploadBtn.OnClientClick += "if(doConfirm_" + this.ClientID + "()){upLoad(this);}return false;";
                }
                else
                {
                    uploadBtn.OnClientClick += "upLoad(this);return false;";
                }
            }
            this.Controls.Add(uploadBtn);
        }

        /// <summary>
        /// 將必要保存的資料載入到ControlState。
        /// </summary>
        protected override void LoadControlState(object savedState)
        {
            object[] allStates = (object[])savedState;
            _UploadNmbers = (byte)allStates[0];
        }

        /// <summary>
        /// 將必要保存的資料儲存到ControlState。
        /// </summary>
        protected override object SaveControlState()
        {
            object[] allStates = new object[1];
            allStates[0] = _UploadNmbers;
            return allStates;
        }

        #region IPostBackEventHandler 成員

        public void RaisePostBackEvent(string eventArgument)
        {
            DirectoryInfo UploadDir = this.UploadDir.StartsWith(@"\\") ? new DirectoryInfo(this.UploadDir) : new DirectoryInfo(this.Page.Server.MapPath(@"~\" + this.UploadDir));
            FileInfo[] UploadDirFiles = UploadDir.GetFiles();
            string[] StruploadedFiles = this.Page.Request[eventArgument].TrimEnd(";".ToCharArray()).Split(";".ToCharArray());
            List<FileInfo> uploadedFiles = new List<FileInfo>();
            foreach (FileInfo file in UploadDirFiles)
            {
                foreach (string sfile in StruploadedFiles)
                {
                    if (file.Name == sfile)
                    {
                        uploadedFiles.Add(file);
                        break;
                    }
                }
            }
            OnMultiUploadFilesFinished(this, new MultiUploadFilesFinishedEventArgs(uploadedFiles));
        }
        private void CreateConfirmScript()
        {
            if (!Page.ClientScript.IsStartupScriptRegistered("doConfirm_" + this.ClientID))
            {
                string script = "<script language=javascript> \n" +
                  "\t function doConfirm_" + this.ClientID + "() { \n" +
                  "\t return window.confirm(\"" + ConfirmMessage + "\"); \n" +
                  "} \n" +
                  "</script>";
                string ClientScript = script;
                Page.RegisterStartupScript("doConfirm_" + this.ClientID, ClientScript);
            }
        }
        #endregion
    }
}
