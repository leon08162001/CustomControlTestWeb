using System;
using System.Configuration;
using System.ComponentModel;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.Design;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Drawing.Design;
using AjaxPro;

namespace APTemplate
{
    /// <summary>
    /// 列舉-要驗證的資料格式。
    /// </summary>
    public enum OptionValidationFormat
    {
        /// <summary>
        /// 驗證對象為不驗證
        /// </summary>
        無,
        /// <summary>
        /// 驗證對象為整數
        /// </summary>
        整數,
        /// <summary>
        /// 驗證對象為小數
        /// </summary>
        小數,
        /// <summary>
        /// 驗證對象為郵件信箱
        /// </summary>
        郵件信箱,
        /// <summary>
        /// 驗證對象為國民身分證
        /// </summary>
        國民身分證,
        /// <summary>
        /// 驗證對象為日期
        /// </summary>
        日期
    }
    /// <summary>
    /// 含有一個TextBox和一個按鈕來另開視窗的自定義輸入控制項。
    /// </summary>
    /// <remarks>
    /// <list type="bullet">
    /// <item><description>主要功能為透過另開輸入視窗，輸入完畢後將值回傳到TextBox。</description></item>
    /// <item><description>TextBox可包含的值格式為整數、小數、郵件信箱、日期或任意值。</description></item>
    /// <item><description>請設定<bold>ValidationFormat</bold>屬性定義TextBox可接受的值格式。</description></item>
    /// <item><description>請使用<bold>Text</bold>屬性取得輸入值。</description></item>
    /// </list>
    /// </remarks>
    [ToolboxData("<{0}:TextBox_PopupWindow runat=server></{0}:TextBox_PopupWindow>")]
    [ToolboxBitmap(typeof(TextBox))]
    [ValidationProperty("Text")]
    public class TextBox_PopupWindow : TextBoxBase
    {
        protected OptionValidationFormat _ValidationFormat = OptionValidationFormat.無;
        protected WindowType _WindowType = WindowType.Normal;
        protected int _WindowWidth = 1024;
        protected int _WindowHeight = 700;
        protected string _Features = "status=yes,scrollbars=yes";
        protected DecimalType _decimailLen = DecimalType.無;
        protected CarryType _CarryType = CarryType.保留;
        private int CarryValue = 0;
        private int DigitLen = 0;
        private RequiredFieldValidator RFV = null;
        private RegularExpressionValidator REV = null;
        private CustomValidator valid = null;
        private object obj = new object();
        protected PlaceHolder PlaceHolder1 = new PlaceHolder();
        protected LinkButton LinkButton1 = new LinkButton();
        protected System.Web.UI.WebControls.Image Img1 = new System.Web.UI.WebControls.Image();
        protected System.Web.UI.WebControls.Image Img2 = new System.Web.UI.WebControls.Image();
        protected Table Table1 = new Table();
        protected Table Table2 = new Table();
        protected TableRow TableRow1 = new TableRow();
        protected TableRow TableRow2 = new TableRow();
        protected TableCell TableCell1 = new TableCell();
        protected TableCell TableCell2 = new TableCell();
        protected TableCell TableCell3 = new TableCell();
        protected TableCell TableCell4 = new TableCell();
        protected TableCell TableCell5 = new TableCell();
        protected TableCell TableCell6 = new TableCell();
        protected TableCell TableCell7 = new TableCell();

        #region Public Properties & Methods

        /// <summary>
        ///按鈕另開視窗的視窗類型。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("按鈕另開視窗的視窗類型。")]
        public WindowType WindowType
        {
            get { return _WindowType; }
            set { _WindowType = value; }
        }

        /// <summary>
        ///開啟視窗的寬度。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("開啟視窗的寬度。")]
        public int WindowWidth
        {
            get { return _WindowWidth; }
            set { _WindowWidth = value; }
        }

        /// <summary>
        ///開啟視窗的高度。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("開啟視窗的高度。")]
        public int WindowHeight
        {
            get { return _WindowHeight; }
            set { _WindowHeight = value; }
        }

        /// <summary>
        ///開啟視窗的特性。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("開啟視窗的特性。")]
        public string Features
        {
            get { return _Features; }
            set { _Features = value; }
        }

        /// <summary>
        ///開啟視窗所導向的位置。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("開啟視窗所導向的位置。")]
        [UrlProperty(), Editor(typeof(UrlEditor), typeof(UITypeEditor))]
        public string WindowUrl
        {
            get { return LinkButton1.PostBackUrl; }
            set { LinkButton1.PostBackUrl = value; }
        }

        /// <summary>
        ///要驗證的資料格式。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("要驗證的資料格式。")]
        public OptionValidationFormat ValidationFormat
        {
            get { return _ValidationFormat; }
            set { _ValidationFormat = value; }
        }

        /// <summary>
        /// 小數位數的長度。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("小數位數的長度。")]
        public DecimalType DecimalLength
        {
            get { return _decimailLen; }
            set { _decimailLen = value; }
        }

        /// <summary>
        /// 小數位數的捨去進位方式。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("小數位數的捨去進位方式。")]
        public CarryType Carry
        {
            get { return _CarryType; }
            set { _CarryType = value; }
        }

        /// <summary>
        /// 第一個輸入方塊的內容值。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("第一個輸入方塊的內容值。")]
        public string Text
        {
            get { return TextBox1.Text; }
            set { TextBox1.Text = value; }
        }

        /// <summary>
        ///輸入方塊的輸入形式。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("輸入方塊的輸入形式。")]
        public virtual TextBoxMode TextMode
        {
            get
            {
                if (ViewState["TextMode"] == null)
                    return TextBoxMode.SingleLine;
                else
                    return (TextBoxMode)ViewState["TextMode"];
            }
            set { ViewState["TextMode"] = value; }
        }

        /// <summary>
        /// 檢查身份證字號
        /// </summary>
        /// <param name="ID">身份證字號</param>
        /// <returns></returns>
        [AjaxMethod]
        public bool CheckID(string ID)
        {
            if (ID.Length != 10)
            {
                return false;
            }
            if (!isNumeric(ID.Substring(1, 9)))
            {
                return false;
            }
            string ChkEnglish = "ABCDEFGHJKLMNPQRSTUVXYWZIO";
            int ChkNum = ChkEnglish.IndexOf(ID.Substring(0, 1)) + 10;
            if (ChkNum < 10)
            {
                return false;
            }
            ID = ChkNum.ToString() + ID.Substring(1, 9);
            ChkNum = Int32.Parse(ID.Substring(0, 1));
            for (int i = 1; i < 10; i++)
            {
                ChkNum += Int32.Parse(ID.Substring(i, 1)) * (10 - i);
            }
            ChkNum = ChkNum % 10;
            if (ChkNum == 0)
            {
                ChkNum = 10;
            }
            ChkNum = 10 - ChkNum;
            if (Int32.Parse(ID.Substring(10, 1)) != ChkNum)
            {
                return false;
            }
            return true;
        }

        #endregion

        #region Protected Properties & Methods

        protected override void OnInit(EventArgs e)
        {
            TextBox1.ID = TextBoxID == null || TextBoxID == "" ? "TextBox1" : TextBoxID;
            Label1.ID = "Label1";
        }

        protected override void OnLoad(EventArgs e)
        {
            AjaxPro.Utility.RegisterTypeForAjax(typeof(APTemplate.TextBox_PopupWindow));
            TextBox1.CausesValidation = this.NeedValidation;
            switch (DecimalLength)
            {
                case DecimalType.無:
                    DigitLen = 0;
                    break;
                case DecimalType.一位:
                    DigitLen = 1;
                    break;
                case DecimalType.二位:
                    DigitLen = 2;
                    break;
                case DecimalType.三位:
                    DigitLen = 3;
                    break;
                case DecimalType.四位:
                    DigitLen = 4;
                    break;
                case DecimalType.五位:
                    DigitLen = 5;
                    break;
            }

            switch (Carry)
            {
                case CarryType.保留:
                    CarryValue = 0;
                    break;
                case CarryType.四捨五入:
                    CarryValue = 1;
                    break;
                case CarryType.無條件進入:
                    CarryValue = 2;
                    break;
                case CarryType.無條件捨去:
                    CarryValue = 3;
                    break;
            }

            if (NeedValue)
            {
                RFV = new RequiredFieldValidator();
                RFV.ControlToValidate = TextBox1.ID;
                RFV.Font.Size = FontUnit.Small;
                RFV.Display = ValidatorDisplay.Dynamic;
                SetRequiredFieldErrorMessageByValidationFormat(RFV);
                RFV.ValidationGroup = this.ValidationGroup;
                PlaceHolder1.Controls.Add(RFV);
            }

            if (NeedValidation)
            {
                REV = new RegularExpressionValidator();
                REV.ControlToValidate = TextBox1.ID;
                REV.Font.Size = FontUnit.Small;
                REV.Display = ValidatorDisplay.Dynamic;
                SetRegularExpressionErrorMessageByValidationFormat(REV);
                REV.ValidationGroup = this.ValidationGroup;
                PlaceHolder1.Controls.Add(REV);
            }

            if (ValidationFormat != OptionValidationFormat.國民身分證)
                SetValidationType(base.ValidationType, new BaseValidator[] { RFV, REV }, PlaceHolder1);
            else
                SetValidationType(base.ValidationType, new BaseValidator[] { RFV, valid }, PlaceHolder1);
        }

        protected override void CreateChildControls()
        {
            EnsureChildControls();
            Title = Title == "" ? "請輸入標題..." : Title;
            base.Label1.Font.Size = this.Font.Size.IsEmpty ? FontUnit.Small : this.Font.Size;
            Label1.Text = NeedValue ? "<font color=red>*</font>" + this.Title : this.Title;
            Label1.Width = this.TitleWidth;
            Label1.Visible = this.IsShowTitle;
            TableCell1.Visible = this.IsShowTitle;
            Label1.Attributes["style"] += "text-align:" + _TitleAlign + ";";
            TextBox1.Width = this.TextBoxWidth;
            TextBox1.Height = this.TextBoxHeight;
            TextBox1.Wrap = this.TextMode == TextBoxMode.MultiLine ? true : false;
            TextBox1.MaxLength = this.TextLength;
            TextBox1.Attributes["style"] += "text-align:" + _TextAlign + ";";
            if (this.ReadOnly) { TextBox1.Attributes.Add("readonly", this.ReadOnly.ToString().ToLower()); }
            TextBox1.TextMode = this.TextMode;
            TextBox1.Text = Text;
            TextBox1.ForeColor = this.TextForeColor;
            TextBox1.BackColor = this.TextBackColor;

            LinkButton1.ID = "LinkButton1";
            LinkButton1.Text = "...";
            LinkButton1.Style["text-decoration"] = "none";
            Img1.ImageUrl = this.Page != null ? this.Page.ClientScript.GetWebResourceUrl(this.GetType(), "APTemplate.Resources.button_01.gif") : "";
            Img1.Width = Unit.Pixel(3);
            Img1.ImageAlign = ImageAlign.Top;
            Img1.Height = Unit.Pixel(22);
            Img2.ImageUrl = this.Page != null ? this.Page.ClientScript.GetWebResourceUrl(this.GetType(), "APTemplate.Resources.button_03.gif") : "";
            Img2.Width = Unit.Pixel(3);
            Img2.ImageAlign = ImageAlign.Top;
            Img2.Height = Unit.Pixel(22);

            Table1.BorderWidth = 0;
            Table1.CellPadding = 0;
            Table1.CellSpacing = 0;

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

            Table2.BorderWidth = 0;
            Table2.CellPadding = 0;
            Table2.CellSpacing = 0;

            TableCell1.BackColor = this.TitleBackColor;
            TableCell1.ForeColor = this.TitleForeColor;
            TableCell1.BorderWidth = this.HasBorder ? 1 : 0;
            TableCell1.BorderStyle = BorderStyle.Solid;
            TableCell1.BorderColor = Color.FromArgb(99, 99, 99);
            TableCell3.BorderWidth = this.HasBorder ? 1 : 0;
            TableCell3.BorderStyle = BorderStyle.Solid;
            TableCell3.BorderColor = Color.FromArgb(99, 99, 99);
            string ImgUrl = this.Page != null ? this.Page.ClientScript.GetWebResourceUrl(this.GetType(), "APTemplate.Resources.button_02.gif") : "";
            TableCell6.Attributes["style"] = "background-image:url(" + ImgUrl + ");background-repeat:no-repeat;background-position:top;vertical-align:top";

            TableCell1.Controls.Add(Label1);
            TableCell2.Width = Unit.Pixel(2);
            TableCell4.Controls.Add(TextBox1);
            TableCell5.Attributes["style"] = "vertical-align:top;";
            TableCell5.Controls.Add(Img1);
            TableCell6.Controls.Add(LinkButton1);
            TableCell7.Attributes["style"] = "vertical-align:top;";
            TableCell7.Controls.Add(Img2);
            TableCell7.Controls.Add(PlaceHolder1);

            TableRow2.Controls.Add(TableCell4);
            TableRow2.Controls.Add(TableCell5);
            TableRow2.Controls.Add(TableCell6);
            TableRow2.Controls.Add(TableCell7);

            Table2.Controls.Add(TableRow2);

            TableCell3.Controls.Add(Table2);

            TableRow1.Controls.Add(TableCell1);
            TableRow1.Controls.Add(TableCell2);
            TableRow1.Controls.Add(TableCell3);

            Table1.Controls.Add(TableRow1);

            this.Controls.Add(Table1);
        }

        protected override void RenderContents(HtmlTextWriter writer)
        {
            SetJavaScriptByValidationFormat();
            WebScript.OpenWindow(this, LinkButton1.ID, EventType.OnClick, this.WindowType, this.WindowUrl.Replace("~/", ""), null, this.Features, this.WindowWidth, this.WindowHeight);
            Table1.RenderControl(writer);
        }

        protected override void OnPreRender(EventArgs e)
        {
            if (ValidationFormat == OptionValidationFormat.小數)
            { Carry_PreRender(); }
            if (ValidationFormat == OptionValidationFormat.國民身分證)
            {
                if (!Page.IsStartupScriptRegistered(TextBox1.ClientID + "_ValidScript"))
                {
                    string script = "<script language=javascript> \n" +
                      "\t function TextBox_PopupWindow_ClientValidate(source, arguments) { \n" +
                      "\t var res = APTemplate.TextBox_PopupWindow.CheckID(arguments.Value); \n" +
                      "\t arguments.IsValid=(res.value); \n" +
                      "} \n" +
                      "</script>";
                    string ClientScript = script;
                    Page.ClientScript.RegisterStartupScript(this.GetType(), TextBox1.ClientID + "_ValidScript", ClientScript);
                }
            }
            RegisterJavaScript.RegisterContolIncludeScript(Page);
        }

        #endregion

        #region Private Properties & Methods

        private void SetRequiredFieldErrorMessageByValidationFormat(RequiredFieldValidator RFV)
        {
            switch (ValidationFormat)
            {
                case OptionValidationFormat.無:
                    RFV.ErrorMessage = "不可空白!";
                    break;
                case OptionValidationFormat.整數:
                    RFV.ErrorMessage = "請輸入數字!";
                    break;
                case OptionValidationFormat.小數:
                    RFV.ErrorMessage = "請輸入數字!";
                    break;
                case OptionValidationFormat.郵件信箱:
                    RFV.ErrorMessage = "請輸入Email!";
                    break;
                case OptionValidationFormat.國民身分證:
                    RFV.ErrorMessage = "請輸入身分證ID!";
                    break;
                case OptionValidationFormat.日期:
                    RFV.ErrorMessage = "請輸入日期值!";
                    break;
            }
        }

        private void SetRegularExpressionErrorMessageByValidationFormat(RegularExpressionValidator REV)
        {
            switch (ValidationFormat)
            {
                case OptionValidationFormat.無:
                    REV.ErrorMessage = "";
                    REV.ValidationExpression = @".{1,}";
                    break;
                case OptionValidationFormat.整數:
                    REV.ErrorMessage = "非數字格式內容!";
                    REV.ValidationExpression = @"\d*";
                    break;
                case OptionValidationFormat.小數:
                    REV.ErrorMessage = "非數字格式內容!";
                    REV.ValidationExpression = @"^(\-)?\d*(\.\d+)?$";
                    break;
                case OptionValidationFormat.郵件信箱:
                    REV.ErrorMessage = "Email格式不正確!"; ;
                    REV.ValidationExpression = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                                                                     @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                                                                     @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
                    break;
                case OptionValidationFormat.國民身分證:
                    if (ValidationType != OptionValidationType.NoCheck)
                    {
                        valid = new CustomValidator();
                        valid.ControlToValidate = TextBox1.ID;
                        valid.IsValid = true;
                        valid.Display = ValidatorDisplay.Dynamic;
                        valid.ClientValidationFunction = "TextBox_PopupWindow_ClientValidate";
                        valid.Font.Size = FontUnit.Small;
                        valid.ErrorMessage = "身份證字號格式不正確!";
                        PlaceHolder1.Controls.Add(valid);
                    }
                    break;
                case OptionValidationFormat.日期:
                    REV.ErrorMessage = "日期格式不正確(ex:2000/01/01)!";
                    REV.ValidationExpression = @"^((\d{2}(([02468][048])|([13579][26]))[\-\/\s]?((((0?[13578])|(1[02]))[\-\/\s]?((0?[1-9])|([1-2][0-9])|(3[01])))|(((0?[469])|(11))[\-\/\s]?((0?[1-9])|([1-2][0-9])|(30)))|(0?2[\-\/\s]?((0?[1-9])|([1-2][0-9])))))|(\d{2}(([02468][1235679])|([13579][01345789]))[\-\/\s]?((((0?[13578])|(1[02]))[\-\/\s]?((0?[1-9])|([1-2][0-9])|(3[01])))|(((0?[469])|(11))[\-\/\s]?((0?[1-9])|([1-2][0-9])|(30)))|(0?2[\-\/\s]?((0?[1-9])|(1[0-9])|(2[0-8]))))))(\s(((0?[1-9])|(1[0-2]))\:([0-5][0-9])((\s)|(\:([0-5][0-9])\s))([AM|PM|am|pm]{2,2})))?$";
                    break;
            }
        }

        private void SetJavaScriptByValidationFormat()
        {
            if ((NeedValidation || NeedValue) && ValidationType == OptionValidationType.Alert)
            {
                switch (ValidationFormat)
                {
                    case OptionValidationFormat.整數:
                        TextBox1.Attributes["onkeydown"] = @"this.value=this.value.replace(/\D/g,'');AlertValidation();";
                        TextBox1.Attributes["onafterpaste"] = @"this.value=this.value.replace(/\D/g,'');AlertValidation();";
                        TextBox1.Attributes["onblur"] = "UpdateErrorMessage();";
                        break;
                    case OptionValidationFormat.小數:
                        TextBox1.Attributes["onkeydown"] = @"if(isNaN(document.getElementById('" + TextBox1.ClientID + "').value)||isTriDecimal(document.getElementById('" + TextBox1.ClientID + "').value," + DigitLen + ")){document.getElementById('" + TextBox1.ClientID + "').value=document.getElementById('" + TextBox1.ClientID + "').value.substr(0,document.getElementById('" + TextBox1.ClientID + "').value.length-1);};";
                        TextBox1.Attributes["onafterpaste"] = @"if(isNaN(document.getElementById('" + TextBox1.ClientID + "').value)||isTriDecimal(document.getElementById('" + TextBox1.ClientID + "').value," + DigitLen + ")){document.getElementById('" + TextBox1.ClientID + "').value=document.getElementById('" + TextBox1.ClientID + "').value.substr(0,document.getElementById('" + TextBox1.ClientID + "').value.length-1);AlertValidation();};";
                        TextBox1.Attributes["onchange"] = "if(isNaN(document.getElementById('" + TextBox1.ClientID + "').value))document.getElementById('" + TextBox1.ClientID + "').value='';AlertValidation();";
                        TextBox1.Attributes["onblur"] = "UpdateErrorMessage();";
                        break;
                    case OptionValidationFormat.郵件信箱:
                        TextBox1.Attributes["onchange"] = "AlertValidation();";
                        TextBox1.Attributes["onblur"] = "UpdateErrorMessage();";
                        break;
                    case OptionValidationFormat.國民身分證:
                        TextBox1.Attributes["onchange"] = "AlertValidation();";
                        TextBox1.Attributes["onblur"] = "UpdateErrorMessage();";
                        break;
                    case OptionValidationFormat.日期:
                        TextBox1.Attributes["onchange"] = "AlertValidation();";
                        TextBox1.Attributes["onblur"] = "UpdateErrorMessage();";
                        break;
                }
            }
            else if ((NeedValidation || NeedValue) && ValidationType == OptionValidationType.Label)
            {
                switch (ValidationFormat)
                {
                    case OptionValidationFormat.整數:
                        TextBox1.Attributes["onkeydown"] = @"this.value=this.value.replace(/\D/g,'');";
                        TextBox1.Attributes["onafterpaste"] = @"this.value=this.value.replace(/\D/g,'');";
                        TextBox1.Attributes["onblur"] = "UpdateErrorMessage();";
                        break;
                    case OptionValidationFormat.小數:
                        TextBox1.Attributes["onkeydown"] = @"if(isNaN(document.getElementById('" + TextBox1.ClientID + "').value)||isTriDecimal(document.getElementById('" + TextBox1.ClientID + "').value," + DigitLen + ")){document.getElementById('" + TextBox1.ClientID + "').value=document.getElementById('" + TextBox1.ClientID + "').value.substr(0,document.getElementById('" + TextBox1.ClientID + "').value.length-1);};";
                        TextBox1.Attributes["onafterpaste"] = @"if(isNaN(document.getElementById('" + TextBox1.ClientID + "').value)||isTriDecimal(document.getElementById('" + TextBox1.ClientID + "').value," + DigitLen + ")){document.getElementById('" + TextBox1.ClientID + "').value=document.getElementById('" + TextBox1.ClientID + "').value.substr(0,document.getElementById('" + TextBox1.ClientID + "').value.length-1);};";
                        TextBox1.Attributes["onchange"] = "if(isNaN(document.getElementById('" + TextBox1.ClientID + "').value))document.getElementById('" + TextBox1.ClientID + "').value='';";
                        TextBox1.Attributes["onblur"] = "UpdateErrorMessage();";
                        break;
                    case OptionValidationFormat.郵件信箱:
                        TextBox1.Attributes["onblur"] = "UpdateErrorMessage();";
                        break;
                    case OptionValidationFormat.國民身分證:
                        TextBox1.Attributes["onblur"] = "UpdateErrorMessage();";
                        break;
                    case OptionValidationFormat.日期:
                        TextBox1.Attributes["onblur"] = "UpdateErrorMessage();";
                        break;
                }
            }
        }

        private void Carry_PreRender()
        {
            Context.Session["HasValidationSummary"] = null;
            Context.Session["i"] = Context.Session["i"] == null ? 0 : Context.Session["i"];
            string DecimalValidateScript = "Page_Decimals[" + Context.Session["i"] + "]=new Array();Page_Decimals[" + Context.Session["i"] + "][0]=document.getElementById('" + this.ID + "_" + TextBox1.ID + "');Page_Decimals[" + Context.Session["i"] + "][1]=" + CarryValue + ";";
            Page.ClientScript.RegisterStartupScript(typeof(TextBox_PopupWindow), "TextBox_PopupWindow" + Convert.ToString(Context.Session["i"]), DecimalValidateScript, true);
            Context.Session["i"] = (int)Context.Session["i"] + 1;

            if (ValidationType == OptionValidationType.Alert || ValidationType == OptionValidationType.Label)
            {
                List<Control> AllControls = PublicFunc.GetChildControls(Page);
                foreach (Control Ctl in AllControls)
                {
                    if (Ctl.GetType() == typeof(Button) || Ctl.GetType() == typeof(ImageButton) || Ctl.GetType() == typeof(LinkButton) || Ctl.GetType() == typeof(APTemplate.Button_Normal) || Ctl.GetType() == typeof(APTemplate.Button_ConfirmYesNo))
                    {
                        if (Convert.ToBoolean(Ctl.GetType().GetProperty("CausesValidation").GetValue(Ctl, null)) == true && ((IButtonControl)Ctl).ValidationGroup == this.ValidationGroup)
                        {
                            ((WebControl)Ctl).Attributes["onclick"] += ((WebControl)Ctl).Attributes["onclick"] == null || ((WebControl)Ctl).Attributes["onclick"].IndexOf("if (Page_ClientValidate('" + this.ValidationGroup + "')==false){return false;}else{CarryProcess();};") == -1 ? "if (Page_ClientValidate('" + this.ValidationGroup + "')==false){return false;}else{CarryProcess();};" : "";
                        }
                    }
                    if (Ctl.GetType() == typeof(HtmlInputSubmit) && ((HtmlInputSubmit)Ctl).ValidationGroup == this.ValidationGroup)
                    {
                        ((HtmlControl)Ctl).Attributes["onclick"] += ((HtmlControl)Ctl).Attributes["onclick"] == null || ((HtmlControl)Ctl).Attributes["onclick"].IndexOf("if (Page_ClientValidate('" + this.ValidationGroup + "')==false){return false;}else{CarryProcess();};") == -1 ? "if (Page_ClientValidate('" + this.ValidationGroup + "')==false){return false;}else{CarryProcess();};" : "";
                    }
                }
            }
        }

        /// <summary>
        /// Determines whether the specified STR numeric is numeric.
        /// </summary>
        /// <param name="strNumeric">The STR numeric.</param>
        /// <returns>
        /// 	<c>true</c> if the specified STR numeric is numeric; otherwise, <c>false</c>.
        /// </returns>
        private bool isNumeric(string strNumeric)
        {
            if (strNumeric.Trim() == "")
            {
                return false;
            }
            char[] ca = strNumeric.ToCharArray();
            for (int i = 0; i < ca.Length; i++)
            {
                if (ca[i] > 57 || ca[i] < 48)
                    return false;
            }
            return true;
        }

        #endregion

    }
}