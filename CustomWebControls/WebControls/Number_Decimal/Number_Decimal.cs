using System;
using System.Configuration;
using System.ComponentModel;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Drawing;

namespace APTemplate
{
    /// <summary>
    /// 允許輸入一組整數字或小數的自定義輸入控制項。
    /// </summary>
    /// <remarks>
    /// <list type="bullet">
    /// <item><description>請使用<bold>Text</bold>屬性取得輸入值。</description></item>
    /// </list>
    /// </remarks>
    [ToolboxData("<{0}:Number_Decimal runat=server></{0}:Number_Decimal>")]
    [ToolboxBitmap(typeof(TextBox))]
    [ValidationProperty("Text")]
    public class Number_Decimal : TextBoxBase
    {
        /// <summary>
        /// 儲存小數位數的長度
        /// </summary>
        protected DecimalType _decimailLen = DecimalType.無;
        /// <summary>
        /// 儲存小數位數的捨去進位方式
        /// </summary>
        protected CarryType _CarryType = CarryType.保留;
        private int CarryValue = 0;
        private int DigitLen = 0;
        protected PlaceHolder PlaceHolder1 = new PlaceHolder();
        /// <summary>
        /// 建立Number_Decimal控制項所需的table
        /// </summary>
        protected Table Table1 = new Table();
        /// <summary>
        /// 建立Number_Decimal控制項所需的row
        /// </summary>
        protected TableRow TableRow1 = new TableRow();
        /// <summary>
        /// 建立Number_Decimal控制項所需的cell
        /// </summary>
        protected TableCell TableCell1 = new TableCell();
        /// <summary>
        /// 建立Number_Decimal控制項所需的cell
        /// </summary>
        protected TableCell TableCell2 = new TableCell();
        protected OperatorFormat _OperatorFormat = OperatorFormat.等於;
        protected Double _ValueToCompare = -1;
        protected Boolean _IsNeedComma = false;

        #region Public Properties & Methods

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
        /// 第一個輸入方塊的內容值(為Decimal型態數字)。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("第一個輸入方塊的內容值(為Decimal型態數字)。")]
        public decimal Text
        {
            get
            {
                if (TextBox1.Text == "")
                    return 0;
                else
                {
                    decimal val;
                    Decimal.TryParse(TextBox1.Text.Replace(",", ""), out val);
                    return val;
                }
            }
            set { TextBox1.Text = value.ToString(); }
        }

        /// <summary>
        /// Decimal控制項的比較運算子。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("Decimal控制項的比較運算子。")]
        public OperatorFormat OperatorFormat
        {
            get { return _OperatorFormat; }
            set { _OperatorFormat = value; }
        }

        /// <summary>
        /// Decimal控制項的比較值(以Decimal控制項值與比較值做比較)。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("Decimal控制項的比較值(以Decimal控制項值與比較值做比較)。")]
        public Double ValueToCompare
        {
            get { return _ValueToCompare; }
            set { _ValueToCompare = value; }
        }

        /// <summary>
        /// 是否顯示為千分位符號數字。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("是否顯示為千分位符號數字。")]
        public Boolean IsNeedComma
        {
            get { return _IsNeedComma; }
            set { _IsNeedComma = value; }
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
            RequiredFieldValidator RFV = null;
            RegularExpressionValidator REV = null;
            CompareValidator CV = null;
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
                RFV.ErrorMessage = "請輸入數字!";
                RFV.ValidationGroup = this.ValidationGroup;
                PlaceHolder1.Controls.Add(RFV);
            }

            if (NeedValidation)
            {
                REV = new RegularExpressionValidator();
                REV.ControlToValidate = TextBox1.ID;
                REV.Font.Size = FontUnit.Small;
                REV.Display = ValidatorDisplay.Dynamic;
                REV.ErrorMessage = "非數字格式內容!";
                //REV.ValidationExpression = @"^(\-)?\d*(\.\d+)?$";
                //REV.ValidationExpression = @"^\$?([0-9]{1,3},([0-9]{3},)*[0-9]{3}|[0-9]+)(\.\d+)?$";
                REV.ValidationExpression = @"^\s*[-\+]?([0-9]{1,3},([0-9]{3},)*[0-9]{3}|[0-9]+)(\.\d+)?$";
                REV.ValidationGroup = this.ValidationGroup;
                PlaceHolder1.Controls.Add(REV);

                if (this.ValueToCompare > -1)
                {
                    CV = new CompareValidator();
                    CV.ControlToValidate = TextBox1.ID; ;
                    CV.ValueToCompare = this.ValueToCompare.ToString();
                    CV.Type = ValidationDataType.Double;
                    CV.Operator = this.OperatorFormat == OperatorFormat.大於 ? ValidationCompareOperator.GreaterThan :
                                                this.OperatorFormat == OperatorFormat.大於等於 ? ValidationCompareOperator.GreaterThanEqual :
                                                this.OperatorFormat == OperatorFormat.小於 ? ValidationCompareOperator.LessThan :
                                                this.OperatorFormat == OperatorFormat.小於等於 ? ValidationCompareOperator.LessThanEqual :
                                                this.OperatorFormat == OperatorFormat.不等於 ? ValidationCompareOperator.NotEqual : ValidationCompareOperator.Equal;
                    CV.Font.Size = FontUnit.Small;
                    CV.Display = ValidatorDisplay.Dynamic;
                    CV.ErrorMessage = "數字值必須" + this.OperatorFormat.ToString() + this.ValueToCompare + "!";
                    CV.ValidationGroup = this.ValidationGroup;
                    PlaceHolder1.Controls.Add(CV);
                }
            }

            SetValidationType(base.ValidationType, new BaseValidator[] { RFV, REV, CV }, PlaceHolder1);
        }

        protected override void CreateChildControls()
        {
            EnsureChildControls();
            Title = Title == "" ? "請輸入數字 : " : Title;
            base.Label1.Font.Size = this.Font.Size.IsEmpty ? FontUnit.Small : this.Font.Size;
            Label1.Text = NeedValue ? "<font color=red>*</font>" + this.Title : this.Title;
            Label1.Width = this.TitleWidth;
            Label1.Visible = this.IsShowTitle;
            TableCell1.Visible = this.IsShowTitle;
            Label1.Attributes["style"] += "text-align:" + _TitleAlign + ";";
            TextBox1.Width = this.TextBoxWidth;
            TextBox1.Height = this.TextBoxHeight;
            TextBox1.MaxLength = this.TextLength;
            if (this.ReadOnly) { TextBox1.Attributes.Add("readonly", this.ReadOnly.ToString().ToLower()); }
            TextBox1.Text = Text == 0 ? "" : Convert.ToString(Text);
            TextBox1.ForeColor = this.TextForeColor;
            TextBox1.BackColor = this.TextBackColor;
            TextBox1.Attributes["style"] += "text-align:" + _TextAlign + ";";

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
            TableCell1.BorderWidth = this.HasBorder ? 1 : 0;
            TableCell1.BorderStyle = BorderStyle.Solid;
            TableCell1.BorderColor = Color.FromArgb(99, 99, 99);
            TableCell1.BackColor = this.TitleBackColor;
            TableCell1.ForeColor = this.TitleForeColor;
            TableCell2.BorderWidth = this.HasBorder ? 1 : 0;
            TableCell2.BorderStyle = BorderStyle.Solid;
            TableCell2.BorderColor = Color.FromArgb(99, 99, 99);

            TableCell1.Controls.Add(Label1);
            TableCell2.Controls.Add(TextBox1);
            TableCell2.Controls.Add(PlaceHolder1);
            TableRow1.Controls.Add(TableCell1);
            TableRow1.Controls.Add(TableCell2);
            Table1.Controls.Add(TableRow1);
            this.Controls.Add(Table1);
        }

        protected override void RenderContents(HtmlTextWriter writer)
        {
            Table1.RenderControl(writer);
        }

        protected override void OnPreRender(EventArgs e)
        {
            if ((NeedValidation || NeedValue) && ValidationType == OptionValidationType.Alert)
            {
                //TextBox1.Attributes["onkeydown"] = @"if(isNaN(document.getElementById('" + TextBox1.ClientID + "').value)||isTriDecimal(document.getElementById('" + TextBox1.ClientID + "').value," + DigitLen + ")){document.getElementById('" + TextBox1.ClientID + "').value=document.getElementById('" + TextBox1.ClientID + "').value.substr(0,document.getElementById('" + TextBox1.ClientID + "').value.length-1);};";
                //TextBox1.Attributes["onafterpaste"] = @"if(isNaN(document.getElementById('" + TextBox1.ClientID + "').value)||isTriDecimal(document.getElementById('" + TextBox1.ClientID + "').value," + DigitLen + ")){document.getElementById('" + TextBox1.ClientID + "').value=document.getElementById('" + TextBox1.ClientID + "').value.substr(0,document.getElementById('" + TextBox1.ClientID + "').value.length-1);AlertValidation();};";
                TextBox1.Attributes["onkeydown"] = @"if(this.value.length >1){if(isNaN(document.getElementById('" + TextBox1.ClientID + "').value)||isTriDecimal(document.getElementById('" + TextBox1.ClientID + "').value," + DigitLen + ")){document.getElementById('" + TextBox1.ClientID + "').value=document.getElementById('" + TextBox1.ClientID + "').value.substr(0,document.getElementById('" + TextBox1.ClientID + "').value.length-1);};};";
                TextBox1.Attributes["onafterpaste"] = @"if(this.value.length >1){if(isNaN(document.getElementById('" + TextBox1.ClientID + "').value)||isTriDecimal(document.getElementById('" + TextBox1.ClientID + "').value," + DigitLen + ")){document.getElementById('" + TextBox1.ClientID + "').value=document.getElementById('" + TextBox1.ClientID + "').value.substr(0,document.getElementById('" + TextBox1.ClientID + "').value.length-1);AlertValidation();};};";
                TextBox1.Attributes["onchange"] = "if(isNaN(document.getElementById('" + TextBox1.ClientID + "').value.replace(/,/g, \"\")))document.getElementById('" + TextBox1.ClientID + "').value='';AlertValidation();";
                Carry_PreRender();
            }
            else if ((NeedValidation || NeedValue) && ValidationType == OptionValidationType.Label)
            {
                //TextBox1.Attributes["onkeydown"] = @"if(isNaN(document.getElementById('" + TextBox1.ClientID + "').value)||isTriDecimal(document.getElementById('" + TextBox1.ClientID + "').value," + DigitLen + ")){document.getElementById('" + TextBox1.ClientID + "').value=document.getElementById('" + TextBox1.ClientID + "').value.substr(0,document.getElementById('" + TextBox1.ClientID + "').value.length-1);};";
                //TextBox1.Attributes["onafterpaste"] = @"if(isNaN(document.getElementById('" + TextBox1.ClientID + "').value)||isTriDecimal(document.getElementById('" + TextBox1.ClientID + "').value," + DigitLen + ")){document.getElementById('" + TextBox1.ClientID + "').value=document.getElementById('" + TextBox1.ClientID + "').value.substr(0,document.getElementById('" + TextBox1.ClientID + "').value.length-1);};";
                TextBox1.Attributes["onkeydown"] = @"if(this.value.length >1){if(isNaN(document.getElementById('" + TextBox1.ClientID + "').value)||isTriDecimal(document.getElementById('" + TextBox1.ClientID + "').value," + DigitLen + ")){document.getElementById('" + TextBox1.ClientID + "').value=document.getElementById('" + TextBox1.ClientID + "').value.substr(0,document.getElementById('" + TextBox1.ClientID + "').value.length-1);};};";
                TextBox1.Attributes["onafterpaste"] = @"if(this.value.length >1){if(isNaN(document.getElementById('" + TextBox1.ClientID + "').value)||isTriDecimal(document.getElementById('" + TextBox1.ClientID + "').value," + DigitLen + ")){document.getElementById('" + TextBox1.ClientID + "').value=document.getElementById('" + TextBox1.ClientID + "').value.substr(0,document.getElementById('" + TextBox1.ClientID + "').value.length-1);};};";
                Carry_PreRender();
            }
            if (IsNeedComma)
            {
                TextBox1.Attributes["onblur"] = "this.value = comdify(this);";
                TextBox1.Attributes["onfocus"] = "this.value = replaceCommaWithNullCharacter(this);";
            }
            base.OnPreRender(e);
        }

        #endregion

        #region Private Properties & Methods

        private void Carry_PreRender()
        {
            Context.Session["HasValidationSummary"] = null;
            Context.Session["i"] = Context.Session["i"] == null ? 0 : Context.Session["i"];
            string DecimalValidateScript = "Page_Decimals[" + Context.Session["i"] + "]=new Array();Page_Decimals[" + Context.Session["i"] + "][0]=document.getElementById('" + this.ID + "_" + TextBox1.ID + "');Page_Decimals[" + Context.Session["i"] + "][1]=" + CarryValue + ";";
            Page.ClientScript.RegisterStartupScript(typeof(Number_Decimal), "TextBox_PopupWindow" + Convert.ToString(Context.Session["i"]), DecimalValidateScript, true);
            Context.Session["i"] = (int)Context.Session["i"] + 1;

            if (ValidationType == OptionValidationType.Alert || ValidationType == OptionValidationType.Label)
            {
                List<Control> AllControls = PublicFunc.GetChildControls(Page);
                foreach (Control Ctl in AllControls)
                {
                    if (Ctl.GetType() == typeof(Button) || Ctl.GetType() == typeof(ImageButton) || Ctl.GetType() == typeof(LinkButton) || Ctl.GetType() == typeof(APTemplate.Button_Normal) || Ctl.GetType() == typeof(APTemplate.Button_ConfirmYesNo))
                    {
                        ((WebControl)Ctl).Attributes["onclick"] += ((WebControl)Ctl).Attributes["onclick"] == null || ((WebControl)Ctl).Attributes["onclick"].IndexOf("if (Page_ClientValidate('" + this.ValidationGroup + "')==false){return false;}else{CarryProcess();};") == -1 ? "if (Page_ClientValidate('" + this.ValidationGroup + "')==false){return false;}else{CarryProcess();};" : "";
                    }
                    if (Ctl.GetType() == typeof(HtmlInputSubmit))
                    {
                        ((HtmlControl)Ctl).Attributes["onclick"] += ((HtmlControl)Ctl).Attributes["onclick"] == null || ((HtmlControl)Ctl).Attributes["onclick"].IndexOf("if (Page_ClientValidate('" + this.ValidationGroup + "')==false){return false;}else{CarryProcess();};") == -1 ? "if (Page_ClientValidate('" + this.ValidationGroup + "')==false){return false;}else{CarryProcess();};" : "";
                    }
                }
            }
        }

        #endregion
    }
}