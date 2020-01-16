using System;
using System.Configuration;
using System.ComponentModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Drawing;

namespace APTemplate
{
    /// <summary>
    /// 只允許輸入數字或小數的自定義輸入控制項，可輸入兩組數字。
    /// </summary>
    /// <remarks>
    /// <list type="bullet">
    /// <item><description>兩組數字的比較運算子預設是第二組數字大餘等於第一組數字。</description></item>
    /// <item><description>請設定<bold>Operator</bold>屬性改變預設比較形式。</description></item>
    /// <item><description>請使用<bold>FirstText</bold>屬性取得第一個輸入值;<bold>SecondText</bold>屬性取得第二個輸入值。</description></item>
    /// </list>
    /// </remarks>
    [ToolboxData("<{0}:NumberRange runat=server></{0}:NumberRange>")]
    [ToolboxBitmap(typeof(TextBox))]
    public class NumberRange : TextBoxBase
    {
        /// <summary>
        /// 儲存第二個輸入方塊的控制項ID。
        /// </summary>
        protected string _TextBoxID1 = null;
        /// <summary>
        /// 第一個輸入方塊與第二個數入方塊的數值比對運算子。
        /// </summary>
        protected RangeOperator _operator = RangeOperator.大於等於;
        protected PlaceHolder PlaceHolder1 = new PlaceHolder();
        /// <summary>
        /// 控制項內的第二個輸入方塊。
        /// </summary>
        protected TextBox TextBox2 = new TextBox();
        protected PlaceHolder PlaceHolder2 = new PlaceHolder();
        /// <summary>
        /// 包裝控制項的Table元素。
        /// </summary>
        protected Table Table1 = new Table();
        /// <summary>
        /// 包裝控制項第一個輸入方塊和第二個輸入方塊的Row元素。
        /// </summary>
        protected TableRow TableRow1 = new TableRow();
        /// <summary>
        /// 包裝控制項第一個輸入方塊的Cell元素。
        /// </summary>
        protected TableCell TableCell1 = new TableCell();
        /// <summary>
        /// 包裝控制項第二個輸入方塊的Cell元素。
        /// </summary>
        protected TableCell TableCell2 = new TableCell();
        /// <summary>
        /// 儲存第二個輸入方塊的網頁上的Name名稱。
        /// </summary>
        protected string _SecondTextUniqueID = "";
        /// <summary>
        /// 儲存第二個輸入方塊的網頁上的ID名稱。
        /// </summary>
        protected string _SecondTextClientID = "";
        protected LiteralControl _SeperatorLine = new LiteralControl("<span style='vertical-align:top;'>&nbsp;—&nbsp;</span>");
        protected Boolean _IsNeedComma = false;

        /// <summary>
        /// 控制項內的第三個輸入方塊(儲存第一個輸入方塊輸入的無千分位符號的數字)。
        /// </summary>
        protected TextBox TextBox3 = new TextBox();
        /// <summary>
        /// 控制項內的第四個輸入方塊(儲存第二個輸入方塊輸入的無千分位符號的數字)。
        /// </summary>
        protected TextBox TextBox4 = new TextBox();

        #region Public Properties & Methods

        /// <summary>
        /// 第二個輸入方塊的控制項ID。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("第二個輸入方塊的控制項ID。")]
        public string TextBoxID1
        {
            get
            {
                return _TextBoxID1;
            }
            set { _TextBoxID1 = value; }
        }

        /// <summary>
        /// 第一個輸入方塊的內容值(為Int64型態數字)。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("第一個輸入方塊的內容值(為Int64型態數字)。")]
        public Int64 FirstText
        {
            get
            {
                if (TextBox1.Text == "")
                    return 0;
                else
                {
                    Int64 val;
                    Int64.TryParse(TextBox1.Text.Replace(",", ""), out val);
                    return val;
                }
            }
            set
            {
                TextBox1.Text = value.ToString();
            }
        }

        /// <summary>
        /// 第二個輸入方塊的內容值(為Int64型態數字)。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("第二個輸入方塊的內容值(為Int64型態數字)。")]
        public Int64 SecondText
        {
            get
            {
                if (TextBox2.Text == "")
                    return 0;
                else
                {
                    Int64 val;
                    Int64.TryParse(TextBox2.Text.Replace(",", ""), out val);
                    return val;
                }
            }
            set
            {
                TextBox2.Text = value.ToString();
            }
        }

        /// <summary>
        ///取得內部第二個TextBox控制項ClientID。
        /// </summary>
        [Browsable(false), ReadOnly(true)]
        public virtual string SecondTextBoxClientID
        {
            get
            {
                if (!TextBox2.ClientID.Contains(this.ClientID))
                {
                    return this.ClientID + "_" + TextBox2.ClientID;
                }
                else { return TextBox2.ClientID; }
            }
        }

        /// <summary>
        /// 第一個輸入方塊和第二個輸入方塊內容值比較方式(以第二個輸入方塊為基準)。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("第一個輸入方塊和第二個輸入方塊內容值比較方式(以第二個輸入方塊為基準)。")]
        public RangeOperator Operator
        {
            get { return _operator; }
            set { _operator = value; }
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
            TextBox2.ID = TextBoxID1 == null || TextBoxID1 == "" ? "TextBox2" : TextBoxID1;
            if (IsNeedComma)
            {
                TextBox3.ID = "TextBox3";
                TextBox4.ID = "TextBox4";
            }
            Page.RegisterRequiresControlState(this);
        }

        protected override void OnLoad(EventArgs e)
        {
            RequiredFieldValidator RFV1 = null;
            RegularExpressionValidator REV1 = null;
            RequiredFieldValidator RFV2 = null;
            RegularExpressionValidator REV2 = null;
            CompareValidator CV1 = null;
            TextBox1.CausesValidation = this.NeedValidation;
            TextBox2.CausesValidation = this.NeedValidation;
            if (IsNeedComma)
            {
                TextBox3.CausesValidation = this.NeedValidation;
                TextBox4.CausesValidation = this.NeedValidation;
            }
            if (NeedValue)
            {
                //產生第一個數字需值驗證控制項
                RFV1 = new RequiredFieldValidator();
                RFV1.ControlToValidate = TextBox1.ID;
                RFV1.Font.Size = FontUnit.Small;
                RFV1.Display = ValidatorDisplay.Dynamic;
                RFV1.ErrorMessage = "第一個欄位請輸入數字!";
                RFV1.ValidationGroup = this.ValidationGroup;
                PlaceHolder1.Controls.Add(RFV1);
                //產生第二個數字需值驗證控制項
                RFV2 = new RequiredFieldValidator();
                RFV2.ControlToValidate = TextBox2.ID;
                RFV2.Font.Size = FontUnit.Small;
                RFV2.Display = ValidatorDisplay.Dynamic;
                RFV2.ErrorMessage = "第二個欄位請輸入數字!";
                RFV2.ValidationGroup = this.ValidationGroup;
                PlaceHolder2.Controls.Add(RFV2);
            }

            if (NeedValidation)
            {
                //產生第一個數字格式驗證控制項
                REV1 = new RegularExpressionValidator();
                REV1.ControlToValidate = TextBox1.ID;
                REV1.Font.Size = FontUnit.Small;
                REV1.Display = ValidatorDisplay.Dynamic;
                REV1.ErrorMessage = "非數字格式內容!";
                //REV1.ValidationExpression = @"\d*";
                //REV1.ValidationExpression = @"^\$?([0-9]{1,3},([0-9]{3},)*[0-9]{3}|[0-9]+)(\.[0-9][0-9])?$";
                REV1.ValidationExpression = @"^\s*[-\+]?([0-9]{1,3},([0-9]{3},)*[0-9]{3}|[0-9]+)(\.[0-9][0-9])?$";
                REV1.ValidationGroup = this.ValidationGroup;
                PlaceHolder1.Controls.Add(REV1);
                //產生第二個數字格式驗證控制項
                REV2 = new RegularExpressionValidator();
                REV2.ControlToValidate = TextBox2.ID;
                REV2.Font.Size = FontUnit.Small;
                REV2.Display = ValidatorDisplay.Dynamic;
                REV2.ErrorMessage = "非數字格式內容!";
                //REV2.ValidationExpression = @"\d*";
                //REV2.ValidationExpression = @"^\$?([0-9]{1,3},([0-9]{3},)*[0-9]{3}|[0-9]+)(\.[0-9][0-9])?$";
                REV2.ValidationExpression = @"^\s*[-\+]?([0-9]{1,3},([0-9]{3},)*[0-9]{3}|[0-9]+)(\.[0-9][0-9])?$";
                REV2.ValidationGroup = this.ValidationGroup;
                PlaceHolder2.Controls.Add(REV2);
                //產生第一個數字與第二個數字比較驗證控制項
                CV1 = new CompareValidator();
                if (IsNeedComma)
                {
                    CV1.ControlToValidate = TextBox4.ID;
                    CV1.ControlToCompare = TextBox3.ID;
                }
                else
                {
                    CV1.ControlToValidate = TextBox2.ID;
                    CV1.ControlToCompare = TextBox1.ID;
                }
                CV1.Font.Size = FontUnit.Small;
                CV1.Display = ValidatorDisplay.Dynamic;
                CV1.Type = ValidationDataType.Integer;
                switch (Operator)
                {
                    case RangeOperator.大於等於:
                        CV1.ErrorMessage = "第二欄數字需大於等於第一欄數字!";
                        CV1.Operator = ValidationCompareOperator.GreaterThanEqual;
                        break;
                    case RangeOperator.大於:
                        CV1.ErrorMessage = "第二欄數字需大於第一欄數字!!";
                        CV1.Operator = ValidationCompareOperator.GreaterThan;
                        break;
                    case RangeOperator.小於等於:
                        CV1.ErrorMessage = "第二欄數字需小於等於第一欄數字!!";
                        CV1.Operator = ValidationCompareOperator.LessThanEqual;
                        break;
                    case RangeOperator.小於:
                        CV1.ErrorMessage = "第二欄數字需小於第一欄數字!!";
                        CV1.Operator = ValidationCompareOperator.LessThan;
                        break;
                    case RangeOperator.等於:
                        CV1.ErrorMessage = "第二欄數字需等於第一欄數字!!";
                        CV1.Operator = ValidationCompareOperator.Equal;
                        break;
                    case RangeOperator.不等於:
                        CV1.ErrorMessage = "第二欄數字需不等於第一欄數字!!";
                        CV1.Operator = ValidationCompareOperator.NotEqual;
                        break;
                    case RangeOperator.不驗證:
                        CV1.ErrorMessage = "";
                        CV1.Operator = ValidationCompareOperator.DataTypeCheck;
                        break;
                }
                CV1.ValidationGroup = this.ValidationGroup;
                PlaceHolder2.Controls.Add(CV1);
            }

            SetValidationType(base.ValidationType, new BaseValidator[] { RFV1, REV1 }, PlaceHolder1);
            SetValidationType(base.ValidationType, new BaseValidator[] { RFV2, REV2, CV1 }, PlaceHolder2);
        }

        protected override void CreateChildControls()
        {
            EnsureChildControls();
            Title = Title == "" ? "請輸入數字區間 : " : Title;
            base.Label1.Font.Size = this.Font.Size.IsEmpty ? FontUnit.Small : this.Font.Size;
            Label1.Text = NeedValue ? "<font color=red>*</font>" + this.Title : this.Title;
            Label1.Width = this.TitleWidth;
            Label1.Visible = this.IsShowTitle;
            TableCell1.Visible = this.IsShowTitle;
            Label1.Attributes["style"] += "text-align:" + _TextAlign + ";";
            TextBox1.Width = this.TextBoxWidth;
            TextBox2.Width = this.TextBoxWidth;
            TextBox1.Height = this.TextBoxHeight;
            TextBox2.Height = this.TextBoxHeight;
            TextBox1.Attributes["style"] += "text-align:" + _TextAlign + ";";
            TextBox2.Attributes["style"] += "text-align:" + _TextAlign + ";";
            if (this.ReadOnly) { TextBox1.Attributes.Add("readonly", this.ReadOnly.ToString().ToLower()); }
            if (this.ReadOnly) { TextBox2.Attributes.Add("readonly", this.ReadOnly.ToString().ToLower()); }
            TextBox1.Text = FirstText == 0 ? "" : Convert.ToString(FirstText);
            TextBox2.Text = SecondText == 0 ? "" : Convert.ToString(SecondText);
            TextBox1.ForeColor = this.TextForeColor;
            TextBox1.BackColor = this.TextBackColor;
            TextBox2.ForeColor = this.TextForeColor;
            TextBox2.BackColor = this.TextBackColor;

            if (IsNeedComma)
            {
                TextBox3.Text = TextBox1.Text;
                TextBox4.Text = TextBox2.Text;
                TextBox3.Attributes["style"] += "display:none;";
                TextBox4.Attributes["style"] += "display:none;";
            }

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
            if (IsNeedComma)
            {
                TableCell2.Controls.Add(TextBox3);
            }
            TableCell2.Controls.Add(PlaceHolder1);
            TableCell2.Controls.Add(_SeperatorLine);
            TableCell2.Controls.Add(TextBox2);
            if (IsNeedComma)
            {
                TableCell2.Controls.Add(TextBox4);
            }
            TableCell2.Controls.Add(PlaceHolder2);
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
                TextBox1.Attributes["onkeydown"] = @"this.value = this.value.substring(0,1) == '-'  ? 
                this.value.substring(0,1) + this.value.replace(/\D/g,'') : this.value.replace(/\D/g,'');";
                TextBox1.Attributes["onafterpaste"] = @"this.value = this.value.substring(0,1) == '-'  ? 
                this.value.substring(0,1) + this.value.replace(/\D/g,'') : this.value.replace(/\D/g,'');AlertValidation();";
                TextBox1.Attributes["onchange"] = "AlertValidation();";
                TextBox2.Attributes["onkeydown"] = @"this.value = this.value.substring(0,1) == '-'  ? 
                this.value.substring(0,1) + this.value.replace(/\D/g,'') : this.value.replace(/\D/g,'');";
                TextBox2.Attributes["onafterpaste"] = @"this.value = this.value.substring(0,1) == '-'  ? 
                this.value.substring(0,1) + this.value.replace(/\D/g,'') : this.value.replace(/\D/g,'');AlertValidation();";
                TextBox2.Attributes["onchange"] = "AlertValidation();";
            }
            else if ((NeedValidation || NeedValue) && ValidationType == OptionValidationType.Label)
            {
                TextBox1.Attributes["onkeydown"] = @"this.value = this.value.substring(0,1) == '-'  ? 
                this.value.substring(0,1) + this.value.replace(/\D/g,'') : this.value.replace(/\D/g,'');";
                TextBox1.Attributes["onafterpaste"] = @"this.value = this.value.substring(0,1) == '-'  ? 
                this.value.substring(0,1) + this.value.replace(/\D/g,'') : this.value.replace(/\D/g,'');";
                TextBox2.Attributes["onkeydown"] = @"this.value = this.value.substring(0,1) == '-'  ? 
                this.value.substring(0,1) + this.value.replace(/\D/g,'') : this.value.replace(/\D/g,'');";
                TextBox2.Attributes["onafterpaste"] = @"this.value = this.value.substring(0,1) == '-'  ? 
                this.value.substring(0,1) + this.value.replace(/\D/g,'') : this.value.replace(/\D/g,'');";
            }
            if (IsNeedComma)
            {
                TextBox1.Attributes["onblur"] = "this.value = comdify(this,'" + TextBox3.ClientID + "');";
                TextBox1.Attributes["onfocus"] = "this.value = replaceCommaWithNullCharacter(this);";
                TextBox2.Attributes["onblur"] = "this.value = comdify(this,'" + TextBox4.ClientID + "');";
                TextBox2.Attributes["onfocus"] = "this.value = replaceCommaWithNullCharacter(this);";
            }
            base.OnPreRender(e);
        }

        protected override void LoadControlState(object savedState)
        {
            object[] allStates = (object[])savedState;
            _SecondTextUniqueID = (string)allStates[2];
            _SecondTextClientID = (string)allStates[3];
            TextBox2.Text = Context.Request.Form[_SecondTextUniqueID] == null ? TextBox2.Text : Context.Request.Form[_SecondTextUniqueID].ToString();
        }

        protected override object SaveControlState()
        {
            object[] allStates = new object[4];
            allStates[0] = TextBox1.UniqueID;
            allStates[1] = TextBox1.ClientID;
            allStates[2] = TextBox2.UniqueID;
            allStates[3] = TextBox2.ClientID;
            return allStates;
        }

        #endregion

    }
}