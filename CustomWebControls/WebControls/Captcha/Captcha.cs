using System;
using System.ComponentModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Collections;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Design;
using System.Web.UI.Design.WebControls;
using AjaxPro;

namespace APTemplate
{
    /// <summary>
    /// 自定義的圖形驗證碼控制項。
    /// </summary>
    [DefaultProperty("Text")]
    [ToolboxData("<{0}:Captcha runat=server></{0}:Captcha>")]
    [ToolboxBitmap(typeof(Captcha), "Resources.CaptchaControl.bmp")]
    public class Captcha : CompositeControl, INamingContainer
    {
        public enum Layout
        {
            Horizontal,
            Vertical
        }

        //public enum CacheType
        //{
        //  HttpRuntime,
        //  Session
        //}

        private int _timeoutSecondsMax = 0;
        private int _timeoutSecondsMin = 0;
        private string _TitleText = "請鍵入圖形碼:";
        private Font _font = new Font(FontFamily.GenericSerif, 28);
        private CaptchaImage _captcha = new CaptchaImage();
        private string _prevguid = "";
        protected Table Table1 = new Table();
        protected TableRow TableRow1 = new TableRow();
        protected TableCell TableCell1 = new TableCell();
        protected TableCell TableCell2 = new TableCell();
        protected System.Web.UI.WebControls.Image Image1 = new System.Web.UI.WebControls.Image();
        protected Label Label1 = new Label();
        protected Literal Literal1 = new Literal();
        protected TextBox TextBox1 = new TextBox();
        protected ImageButton RefreshImg = new ImageButton();
        protected CompareValidator CV1 = null;
        protected RequiredFieldValidator RFV = null;
        protected PlaceHolder PH1 = new PlaceHolder();
        //protected Unit _TextWidth = Unit.Pixel(60);
        protected bool _IsShowTitle = true;
        protected string _CaptchaTextID = null;

        #region Public Properties & Methods

        public override bool Enabled
        {
            get
            {
                return base.Enabled;
            }
            set
            {
                base.Enabled = value;
            }
        }

        ///<summary>
        ///是否顯示圖形驗證碼控制項標題文字.
        ///</summary>
        [DefaultValue(""),
        Description("是否顯示圖形驗證碼控制項標題文字."),
         Category("自訂")]
        public bool IsShowTitle
        {
            get { return _IsShowTitle; }
            set { _IsShowTitle = value; }
        }

        ///<summary>
        ///圖形驗證碼控制項所呈現的標題文字.
        ///</summary>
        [DefaultValue(""),
        Description("圖形驗證碼控制項所呈現的標題文字."),
         Category("自訂")]
        public string TitleText
        {
            get { return _TitleText; }
            set { _TitleText = value; }
        }

         ///<summary>
        ///圖形驗證碼控制項輸入方塊的ID.
        ///</summary>
        [DefaultValue(""),
        Description("圖形驗證碼控制項輸入方塊的ID."),
         Category("自訂")]
        public string CaptchaTextID
        {
            get { return _CaptchaTextID; }
            set { _CaptchaTextID = value; }
        }
        

        ///<summary>
        ///圖形驗證碼控制項所輸入的文字.
        ///</summary>
        [DefaultValue(""),
        Description("圖形驗證碼控制項所輸入的文字."),
         Category("自訂")]
        public string CaptchaText
        {
            get { return TextBox1.Text; }
            set { TextBox1.Text = value; }
        }

        /////<summary>
        /////圖形驗證碼控制項輸入方塊的寬度.
        /////</summary>
        //[DefaultValue(""),
        // Description("圖形驗證碼控制項輸入方塊的寬度."),
        // Category("自訂")]
        //public Unit TextWidth
        //{
        //  get { return _TextWidth; }
        //  set { _TextWidth = value; }
        //}

        ///<summary>
        ///圖形驗證碼的字型設定.
        ///</summary>
        [Description("圖形驗證碼的字型設定."),
        Category("自訂")]
        public Font CaptchaFont
        {
            get { return _font; }
            set
            {
                _font = value;
                _captcha.Font = _font;
            }
        }

        ///<summary>
        ///出現在圖形驗證碼控制項的文字總集合.
        ///</summary>
        [DefaultValue(""),
        Description("出現在圖形驗證碼控制項的文字總集合."),
        Category("自訂")]
        public string CaptchaChars
        {
            get { return _captcha.TextChars; }
            set { _captcha.TextChars = value; }
        }

        ///<summary>
        ///產生圖形驗證碼的長度.
        ///</summary>
        [DefaultValue(5),
        Description("產生圖形驗證碼的長度."),
        Category("自訂")]
        public int CaptchaLength
        {
            get { return _captcha.TextLength; }
            set { _captcha.TextLength = value; }
        }

        ///<summary>
        ///進入網頁後，輸入及送出圖形驗證碼的最小時間限制(低於此時間可能是機器人行為，0為不設定).
        ///</summary>
        [DefaultValue(0),
        Description("進入網頁後，輸入及送出圖形驗證碼的最小時間限制(低於此時間可能是機器人行為，0為不設定)."),
        Category("自訂")]
        public int CaptchaMinTimeout
        {
            get { return _timeoutSecondsMin; }
            set
            {
                if (value > 0 && value >= CaptchaMaxTimeout)
                    throw new ArgumentOutOfRangeException("CaptchaTimeout", "CaptchaMinTimeout值必須小於CaptchaMaxTimeout值!");
                _timeoutSecondsMin = value;
            }
        }

        ///<summary>
        ///進入網頁後，輸入及送出圖形驗證碼的最大時間限制(高於此時間可能是駭客行為，0為不設定).
        ///</summary>
        [DefaultValue(0),
         Description("進入網頁後，輸入及送出圖形驗證碼的最大時間限制(高於此時間可能是駭客行為，0為不設定)."),
         Category("自訂")]
        public int CaptchaMaxTimeout
        {
            get { return _timeoutSecondsMax; }
            set
            {
                if (value > 0 && value <= CaptchaMinTimeout)
                    throw new ArgumentOutOfRangeException("CaptchaTimeout", "CaptchaMaxTimeout值必須大於CaptchaMinTimeout值!");
                _timeoutSecondsMax = value;
            }
        }

        ///<summary>
        ///圖形驗證碼控制項圖形的高度.
        ///</summary>
        [DefaultValue(30),
        Description("圖形驗證碼控制項圖形的高度."),
        Category("自訂")]
        public int CaptchaHeight
        {
            get { return _captcha.Height; }
            set { _captcha.Height = value; }
        }

        ///<summary>
        ///圖形驗證碼控制項圖形的寬度.
        ///</summary>
        [DefaultValue(125),
        Description("圖形驗證碼控制項圖形的寬度."),
        Category("自訂")]
        public int CaptchaWidth
        {
            get { return _captcha.Width; }
            set { _captcha.Width = value; }
        }

        ///<summary>
        ///圖形驗證碼文字扭曲的程度.
        ///</summary>
        [DefaultValue(typeof(CaptchaImage.FontWarpFactor), "None"),
        Description("圖形驗證碼文字扭曲的程度."),
        Category("自訂")]
        public CaptchaImage.FontWarpFactor CaptchaFontWarping
        {
            get { return _captcha.FontWarp; }
            set { _captcha.FontWarp = value; }
        }

        ///<summary>
        ///圖形驗證碼背景雜訊的程度.
        ///</summary>
        [DefaultValue(typeof(CaptchaImage.BackgroundNoiseLevel), "Low"),
        Description("圖形驗證碼背景雜訊的程度."),
        Category("自訂")]
        public CaptchaImage.BackgroundNoiseLevel CaptchaBackgroundNoise
        {
            get { return _captcha.BackgroundNoise; }
            set { _captcha.BackgroundNoise = value; }
        }

        [DefaultValue(typeof(CaptchaImage.LineNoiseLevel), "None"),
        Description("Add line noise to the CAPTCHA image"),
        Category("自訂")]
        public CaptchaImage.LineNoiseLevel CaptchaLineNoise
        {
            get { return _captcha.LineNoise; }
            set { _captcha.LineNoise = value; }
        }

        [AjaxMethod]
        public string RefreshCaptcha(string AllBtnClientID, int CaptchaMinTimeout, int CaptchaMaxTimeout)
        {
            string IDs = "";
            string MinTimeStamp = ((DateTime.Now.AddSeconds(CaptchaMinTimeout).ToUniversalTime().Ticks - new DateTime(1970, 1, 1).Ticks) / 10000).ToString();
            string MaxTimeStamp = ((DateTime.Now.AddSeconds(CaptchaMaxTimeout).ToUniversalTime().Ticks - new DateTime(1970, 1, 1).Ticks) / 10000).ToString();
            string[] AllBtnID = AllBtnClientID.Split(new Char[] { ',' });
            foreach (string ID in AllBtnID)
            {
                IDs += "'" + ID + "',";
            }
            IDs = IDs.Substring(0, IDs.Length - 1);
            return "[[" + IDs + "],['" + MinTimeStamp + "','" + MaxTimeStamp + "']]";
        }

        #endregion

        #region Protected Properties & Methods

        protected override void OnInit(EventArgs e)
        {
            TextBox1.ID = _CaptchaTextID == null || _CaptchaTextID == "" ? "TextBox1" : _CaptchaTextID;
        }

        protected override void OnLoad(EventArgs e)
        {
            AjaxPro.Utility.RegisterTypeForAjax(typeof(APTemplate.Captcha));
            TextBox1.Text = "";
            TextBox1.Attributes["AUTOCOMPLETE"] = "off";
            RFV = new RequiredFieldValidator();
            CV1 = new CompareValidator();
            //RFV
            RFV.ControlToValidate = TextBox1.ID;
            RFV.Display = ValidatorDisplay.Dynamic;
            RFV.ErrorMessage = Label1.Text + "=> 圖形驗證碼尚未輸入!";
            RFV.InitialValue = "";
            RFV.Attributes["style"] += "display:'';";
            RFV.Enabled = true;
            PH1.Controls.Add(RFV);

            //CV1
            CV1.ControlToValidate = TextBox1.ID;
            CV1.ValueToCompare = _captcha.Text;
            CV1.Operator = ValidationCompareOperator.Equal;
            CV1.Display = ValidatorDisplay.Dynamic;
            CV1.ErrorMessage = Label1.Text + "=> 與圖形驗證碼不符，請重新輸入!";
            CV1.Enabled = true;
            PH1.Controls.Add(CV1);
        }

        protected override void CreateChildControls()
        {
            EnsureChildControls();
            //Image1
            string ImgUrl = "";
            ImgUrl += "CaptchaImage.aspx";
            if (!IsDesignMode)
            { ImgUrl += "?guid=" + Convert.ToString(_captcha.UniqueId); }
            Image1.ImageUrl = ImgUrl;
            Image1.BorderWidth = Unit.Pixel(0);
            if (ToolTip.Length > 0)
            { Image1.ToolTip = ToolTip; }
            Image1.Width = _captcha.Width;
            Image1.Height = _captcha.Height;

            //Label1
            Label1.Text = this.TitleText;
            Label1.Visible = this.IsShowTitle;

            //Literal1
            Literal1.Text = "<br />";

            //TextBox1
            TextBox1.Attributes["size"] = _captcha.TextLength.ToString();
            TextBox1.MaxLength = _captcha.TextLength;
            TextBox1.AccessKey = AccessKey;
            TextBox1.Enabled = Enabled;
            TextBox1.TabIndex = TabIndex;
            //TextBox1.Width = this.TextWidth;
            TextBox1.Columns = _captcha.TextLength + 1;

            //RefreshImg
            RefreshImg.BorderWidth = 0;
            RefreshImg.Width = Unit.Pixel(16);
            RefreshImg.Height = Unit.Pixel(16);
            RefreshImg.CausesValidation = false;
            RefreshImg.ImageAlign = ImageAlign.AbsBottom;
            RefreshImg.ToolTip = "重新取得驗證碼";
            RefreshImg.ImageUrl = this.Page.ClientScript.GetWebResourceUrl(typeof(Captcha), "APTemplate.Resources.refresh.png");
            RefreshImg.Click += new ImageClickEventHandler(RefreshImg_Click);
            //RefreshImg.OnClientClick = "var res = APTemplate.Captcha.RefreshNewCaptcha();var e= eval('(' + res.value + ')');window.alert(e[0][0])";

            //TableCell1
            TableCell1.Controls.Add(Image1);

            //TableCell2
            TableCell2.Controls.Add(Label1);
            TableCell2.Controls.Add(Literal1);
            TableCell2.Controls.Add(TextBox1);
            TableCell2.Controls.Add(RefreshImg);
            TableCell2.Controls.Add(PH1);

            //TableRow1
            TableRow1.Controls.Add(TableCell1);
            TableRow1.Controls.Add(TableCell2);

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

            Table1.Attributes["style"] += CssStyle();
            Table1.CssClass = CssClass;
            Table1.Controls.Add(TableRow1);

            this.Controls.Add(Table1);
        }

        protected override void RenderContents(HtmlTextWriter writer)
        {
            Table1.RenderControl(writer);
        }

        protected override object SaveViewState()
        {
            return (object)_captcha.UniqueId;
        }

        protected override void LoadViewState(object savedState)
        {
            if (savedState != null)
                _prevguid = Convert.ToString(savedState);
        }

        protected override void OnPreRender(EventArgs e)
        {
            if (this.Visible)
            {
                GenerateNewCaptcha();
                string ctlClientID = "";
                string MinTimeStamp = ((DateTime.Now.AddSeconds(CaptchaMinTimeout).ToUniversalTime().Ticks - new DateTime(1970, 1, 1).Ticks) / 10000).ToString();
                string MaxTimeStamp = ((DateTime.Now.AddSeconds(CaptchaMaxTimeout).ToUniversalTime().Ticks - new DateTime(1970, 1, 1).Ticks) / 10000).ToString();
                string Script = "var mintimeout,maxtimeout;\n" +
                                "mintimeout=" + MinTimeStamp + ";\n" +
                                "maxtimeout=" + MaxTimeStamp + ";\n";
                if (CaptchaMinTimeout == 0 && CaptchaMaxTimeout == 0)
                { Script = "function checkCapachaTimeout(){if (Page_ClientValidate()==false){return false;};}"; }
                else if ((CaptchaMinTimeout == 0 && CaptchaMaxTimeout != 0) || (CaptchaMinTimeout != 0 && CaptchaMaxTimeout != 0))
                {
                    Script += "function checkCapachaTimeout(){if (Page_ClientValidate()==false){return false;};return CaptchaTimeoutCheck(mintimeout,maxtimeout);}";
                }
                else if (CaptchaMinTimeout != 0 && CaptchaMaxTimeout == 0)
                {
                    Script += "function checkCapachaTimeout(){if (Page_ClientValidate()==false){return false;};return CaptchaTimeoutCheck(mintimeout);}";
                }
                List<Control> AllControls = PublicFunc.GetChildControls(Page);

                foreach (Control Ctl in AllControls)
                {
                    if (Ctl.GetType() == typeof(Button) || Ctl.GetType() == typeof(ImageButton) || Ctl.GetType() == typeof(LinkButton) || Ctl.GetType() == typeof(APTemplate.Button_Normal) || Ctl.GetType() == typeof(APTemplate.Button_ConfirmYesNo))
                    {
                        if (Ctl.NamingContainer.GetType().Name.ToUpper() == "TABSVIEW")
                            continue;
                        if (Convert.ToBoolean(Ctl.GetType().GetProperty("CausesValidation").GetValue(Ctl, null)))
                        {
                            ((WebControl)Ctl).Attributes["onclick"] += ((WebControl)Ctl).Attributes["onclick"] == null || ((WebControl)Ctl).Attributes["onclick"].IndexOf("return checkCapachaTimeout();") == -1 ? "return checkCapachaTimeout();" : "";
                            ctlClientID += Ctl.ClientID + ",";
                            //RefreshImg.OnClientClick = "RefreshNewCaptcha_callback(\"" + ctlClientID + "\"," + CaptchaMinTimeout + "," + CaptchaMaxTimeout + ");";
                            RefreshImg.Attributes["onload"] = "RefreshNewCaptcha_callback(\"" + ctlClientID + "\"," + CaptchaMinTimeout + "," + CaptchaMaxTimeout + ");";
                        }
                    }
                    if (Ctl.GetType() == typeof(HtmlInputSubmit) || Ctl.GetType() == typeof(HtmlInputButton) || Ctl.GetType() == typeof(HtmlButton))
                    {
                        if (Ctl.NamingContainer.GetType().Name.ToUpper() == "TABSVIEW")
                            continue;
                        if (Convert.ToBoolean(Ctl.GetType().GetProperty("CausesValidation").GetValue(Ctl, null)))
                        {
                            ((HtmlControl)Ctl).Attributes["onclick"] += ((HtmlControl)Ctl).Attributes["onclick"] == null || ((HtmlControl)Ctl).Attributes["onclick"].IndexOf("return checkCapachaTimeout();") == -1 ? "return checkCapachaTimeout();" : "";
                            ctlClientID += Ctl.ClientID + ",";
                            //RefreshImg.OnClientClick = "RefreshNewCaptcha_callback(\"" + ctlClientID + "\"," + CaptchaMinTimeout + "," + CaptchaMaxTimeout + ");";
                            RefreshImg.Attributes["onload"] = "RefreshNewCaptcha_callback(\"" + ctlClientID + "\"," + CaptchaMinTimeout + "," + CaptchaMaxTimeout + ");";
                        }
                    }
                }
                Page.ClientScript.RegisterStartupScript(this.GetType(), "capacha_validation", Script, true);
                RegisterJavaScript.RegisterContolIncludeScript(Page);
                string ValidatorEnabledScript = "var RFV = document.getElementById('" + RFV.ClientID + "');" +
                                                "ValidatorEnable(RFV,true);" +
                                                "var CV1 = document.getElementById('" + CV1.ClientID + "');" +
                                                "ValidatorEnable(CV1,true);";
                if (RegisterJavaScript.HasScriptManager(base.Page))
                {
                    ScriptManager.RegisterStartupScript(this, this.Page.GetType(), "ValidatorEnabledScript", ValidatorEnabledScript, true);
                }
                    else
                {
                    Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "ValidatorEnabledScript", ValidatorEnabledScript, true);
                }
            }
            else
                base.OnPreRender(e);
        }

        #endregion

        #region Private Properties & Methods

        private void RefreshImg_Click(object sender, ImageClickEventArgs e)
        {
            OnPreRender(new EventArgs());
        }

        ///<summary>
        ///are we in design mode?
        ///</summary>
        private bool IsDesignMode
        {
            get { return (HttpContext.Current == null); }
        }

        ///<summary>
        ///returns HTML-ized color strings
        ///</summary>
        private string HtmlColor(System.Drawing.Color color)
        {
            if (color.IsEmpty)
                return "";
            if (color.IsNamedColor)
                return color.ToKnownColor().ToString();
            if (color.IsSystemColor)
                return color.ToString();
            return "#" + color.ToArgb().ToString("x").Substring(2);
        }

        ///<summary>
        ///returns css "style=" tag for this control
        ///based on standard control visual properties
        ///</summary>
        private string CssStyle()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string strColor;
            if (BorderWidth.ToString().Length > 0)
            {
                sb.Append("border-width:");
                sb.Append(BorderWidth.ToString());
                sb.Append(";");
            }
            if (BorderStyle != System.Web.UI.WebControls.BorderStyle.NotSet)
            {
                sb.Append("border-style:");
                sb.Append(BorderStyle.ToString());
                sb.Append(";");
            }
            strColor = HtmlColor(BorderColor);
            if (strColor.Length > 0)
            {
                sb.Append("border-color:");
                sb.Append(strColor);
                sb.Append(";");
            }
            strColor = HtmlColor(BackColor);
            if (strColor.Length > 0)
            {
                sb.Append("background-color:" + strColor + ";");
            }
            strColor = HtmlColor(ForeColor);
            if (strColor.Length > 0)
            {
                sb.Append("color:" + strColor + ";");
            }
            if (Font.Bold)
            {
                sb.Append("font-weight:bold;");
            }
            if (Font.Italic)
            {
                sb.Append("font-style:italic;");
            }
            if (Font.Underline)
            {
                sb.Append("text-decoration:underline;");
            }
            if (Font.Strikeout)
            {
                sb.Append("text-decoration:line-through;");
            }
            if (Font.Overline)
            {
                sb.Append("text-decoration:overline;");
            }

            if (Font.Size.ToString().Length > 0)
            {
                sb.Append("font-size:" + Font.Size.ToString() + ";");
            }

            if (Font.Names.Length > 0)
            {
                sb.Append("font-family:");
                foreach (string strFontFamily in Font.Names)
                {
                    sb.Append(strFontFamily);
                    sb.Append(",");
                }
                sb.Length = sb.Length - 1;
                sb.Append(";");
            }

            if (Height.ToString() != "")
            {
                sb.Append("height:" + Height.ToString() + ";");
            }

            if (Width.ToString() != "")
            {
                sb.Append("width" + Width.ToString() + ";");
            }
            sb.Append("'");

            if (sb.ToString() == " style=''")
                return "";
            else
                return sb.ToString();
        }

        /// <summary>
        /// generate a new captcha and store it in the ASP.NET Cache by unique GUID
        /// </summary>
        private void GenerateNewCaptcha()
        {
            if (!IsDesignMode)
            {
                HttpRuntime.Cache.Add(_captcha.UniqueId, _captcha, null,
                DateTime.Now.AddMinutes(60),
                TimeSpan.Zero, System.Web.Caching.CacheItemPriority.NotRemovable, null);
            }
        }

        #endregion
    }
}