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
    /// 列舉-數字控制項的比較運算子。
    /// </summary>
    public enum OperatorFormat
    {
        /// <summary>
        /// 等於
        /// </summary>
        等於 = 0,
        /// <summary>
        /// 不等於
        /// </summary>
        不等於 = 1,
        /// <summary>
        /// 大於
        /// </summary>
        大於 = 2,
        /// <summary>
        /// 大於等於
        /// </summary>
        大於等於 = 3,
        /// <summary>
        /// 小於
        /// </summary>
        小於 = 4,
        /// <summary>
        /// 小於等於
        /// </summary>
        小於等於 = 5
    }

    /// <summary>
    /// 允許輸入一組數字的自定義輸入控制項。
    /// </summary>
    /// <remarks>
    /// <list type="bullet">
    /// <item><description>請使用<bold>Text</bold>屬性取得輸入值。</description></item>
    /// </list>
    /// </remarks>
    [ToolboxData("<{0}:Number runat=server></{0}:Number>")]
    [ToolboxBitmap(typeof(TextBox))]
    [ValidationProperty("Text")]
    public class Number : TextBoxBase
    {
        protected PlaceHolder PlaceHolder1 = new PlaceHolder();
        /// <summary>
        /// 建立Number控制項所需的table
        /// </summary>
        protected Table Table1 = new Table();
        /// <summary>
        /// 建立Number控制項所需的row
        /// </summary>
        protected TableRow TableRow1 = new TableRow();
        /// <summary>
        /// 建立Number控制項所需的cell
        /// </summary>
        protected TableCell TableCell1 = new TableCell();
        /// <summary>
        /// 建立Number控制項所需的cell
        /// </summary>
        protected TableCell TableCell2 = new TableCell();
        protected OperatorFormat _OperatorFormat = OperatorFormat.等於;
        protected Int64 _ValueToCompare = -1;
        protected Boolean _IsNeedComma = false;

        #region Public Properties & Methods

        /// <summary>
        /// 第一個輸入方塊的內容值(為Int64型態數字)。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("第一個輸入方塊的內容值(為Int64型態數字)。")]
        public Int64 Text
        {
            get
            {
                if (TextBox1.Text == "")
                    return 0;
                else
                {
                    Int64 val;
                    Int64.TryParse(TextBox1.Text.Replace(",",""), out val);
                    return val;
                }
            }
            set { TextBox1.Text = value.ToString(); }
        }

        /// <summary>
        /// 數字控制項的比較運算子。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("數字控制項的比較運算子。")]
        public OperatorFormat OperatorFormat
        {
            get { return _OperatorFormat; }
            set { _OperatorFormat = value; }
        }

        /// <summary>
        /// 數字控制項的比較值(以數字控制項值與比較值做比較)。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("數字控制項的比較值(以數字控制項值與比較值做比較)。")]
        public Int64 ValueToCompare
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
                //REV.ValidationExpression = @"\d*";
                //REV.ValidationExpression = @"^\$?([0-9]{1,3},([0-9]{3},)*[0-9]{3}|[0-9]+)(\.[0-9][0-9])?$";
                REV.ValidationExpression = @"^\s*[-\+]?([0-9]{1,3},([0-9]{3},)*[0-9]{3}|[0-9]+)(\.[0-9][0-9])?$";
                                             
                REV.ValidationGroup = this.ValidationGroup;
                PlaceHolder1.Controls.Add(REV);

                if (this.ValueToCompare > -1)
                {
                    CV = new CompareValidator();
                    CV.ControlToValidate = TextBox1.ID; ;
                    CV.ValueToCompare = this.ValueToCompare.ToString();
                    CV.Type = ValidationDataType.Integer;
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

        protected override void OnPreRender(EventArgs e)
        {
            if ((NeedValidation || NeedValue) && ValidationType == OptionValidationType.Alert)
            {
                TextBox1.Attributes["onkeydown"] = @"this.value = this.value.substring(0,1) == '-'  ? 
                this.value.substring(0,1) + this.value.replace(/\D/g,'') : this.value.replace(/\D/g,'');";
                TextBox1.Attributes["onchange"] = @"AlertValidation();";
                TextBox1.Attributes["onafterpaste"] = @"this.value = this.value.substring(0,1) == '-'  ? 
                this.value.substring(0,1) + this.value.replace(/\D/g,'') : this.value.replace(/\D/g,'');AlertValidation();";
            }
            else if ((NeedValidation || NeedValue) && ValidationType == OptionValidationType.Label)
            {
                TextBox1.Attributes["onkeydown"] = @"this.value = this.value.substring(0,1) == '-'  ? 
                this.value.substring(0,1) + this.value.replace(/\D/g,'') : this.value.replace(/\D/g,'');";
                TextBox1.Attributes["onafterpaste"] = @"this.value = this.value.substring(0,1) == '-'  ? 
                this.value.substring(0,1) + this.value.replace(/\D/g,'') : this.value.replace(/\D/g,'');";
            }
            if (IsNeedComma)
            {
                TextBox1.Attributes["onblur"] = "this.value = comdify(this);";
                TextBox1.Attributes["onfocus"] = "this.value = replaceCommaWithNullCharacter(this);";
            }
            base.OnPreRender(e);
        }

        protected override void RenderContents(HtmlTextWriter writer)
        {
            Table1.RenderControl(writer);
        }

        #endregion

    }
}