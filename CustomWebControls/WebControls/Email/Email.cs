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
    /// 自定義的Email輸入控制項。
    /// </summary>
    /// <remarks>
    /// <list type="bullet">
    /// <item><description>請使用<bold>Text</bold>屬性取得輸入值。</description></item>
    /// </list>
    /// </remarks>
    [ToolboxData("<{0}:Email runat=server></{0}:Email>")]
    [ToolboxBitmap(typeof(TextBox))]
    [ValidationProperty("Text")]
    public class Email : TextBoxBase
    {
        protected PlaceHolder PlaceHolder1 = new PlaceHolder();
        /// <summary>
        /// 建立Email控制項所需的table
        /// </summary>
        protected Table Table1 = new Table();
        /// <summary>
        /// 建立Email控制項所需的row
        /// </summary>
        protected TableRow TableRow1 = new TableRow();
        /// <summary>
        /// 建立Email控制項所需的cell
        /// </summary>
        protected TableCell TableCell1 = new TableCell();
        /// <summary>
        /// 建立Email控制項所需的cell
        /// </summary>
        protected TableCell TableCell2 = new TableCell();

        #region Public Properties & Methods

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
            TextBox1.CausesValidation = this.NeedValidation;
            if (NeedValue)
            {
                RFV = new RequiredFieldValidator();
                RFV.ControlToValidate = TextBox1.ID;
                RFV.Font.Size = FontUnit.Small;
                RFV.Attributes["style"] += "display:'';";
                RFV.Display = ValidatorDisplay.Dynamic;
                RFV.ErrorMessage = Title + "=> 請輸入Email!";
                RFV.ValidationGroup = this.ValidationGroup;
                PlaceHolder1.Controls.Add(RFV);
            }

            if (NeedValidation)
            {
                REV = new RegularExpressionValidator();
                REV.ControlToValidate = TextBox1.ID;
                REV.Font.Size = FontUnit.Small;
                REV.Attributes["style"] += "display:'';";
                REV.Display = ValidatorDisplay.Dynamic;
                REV.ErrorMessage = Title + "=> Email格式不正確!";
                REV.ValidationExpression = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                                           @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                                           @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
                REV.ValidationGroup = this.ValidationGroup;
                PlaceHolder1.Controls.Add(REV);
            }

            if ((NeedValidation || NeedValue) && ValidationType == OptionValidationType.Alert)
            {
                TextBox1.Attributes["onchange"] = "AlertValidation();";
                TextBox1.Attributes["onblur"] = "UpdateErrorMessage();";
            }

            SetValidationType(base.ValidationType, new BaseValidator[] { RFV, REV }, PlaceHolder1);
        }

        protected override void CreateChildControls()
        {
            this.Controls.Clear();
            Title = Title == "" ? "Email: " : Title;
            base.Label1.Font.Size = this.Font.Size.IsEmpty ? FontUnit.Small : this.Font.Size;
            Label1.Text = NeedValue ? "<font color=red>*</font>" + this.Title : this.Title;
            Label1.Width = this.TitleWidth;
            Label1.Visible = this.IsShowTitle;
            TableCell1.Visible = this.IsShowTitle;
            Label1.Attributes["style"] += "text-align:" + _TitleAlign + ";";
            TextBox1.Width = this.TextBoxWidth;
            TextBox1.Height = this.TextBoxHeight;
            TextBox1.MaxLength = this.TextLength;
            TextBox1.Text = Text;
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
            ChildControlsCreated = true;
        }

        protected override void RecreateChildControls()
        {
            if (!ChildControlsCreated)
                base.RecreateChildControls();
        }

        protected override void RenderContents(HtmlTextWriter writer)
        {
            Table1.RenderControl(writer);
        }

        #endregion

    }
}