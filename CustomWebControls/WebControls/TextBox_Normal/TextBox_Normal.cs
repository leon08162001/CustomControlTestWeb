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
    /// 自定義的標準輸入控制項。
    /// </summary>
    /// <remarks>
    /// <list type="bullet">
    /// <item><description>請使用<bold>Text</bold>屬性取得輸入值。</description></item>
    /// </list>
    /// </remarks>
    [ToolboxData("<{0}:TextBox_Normal runat=server></{0}:TextBox_Normal>")]
    [ToolboxBitmap(typeof(TextBox))]
    [ValidationProperty("Text")]
    public class TextBox_Normal : TextBoxBase
    {
        protected PlaceHolder PlaceHolder1 = new PlaceHolder();
        /// <summary>
        /// 建立標準輸入控制項所需的table
        /// </summary>
        protected Table Table1 = new Table();
        /// <summary>
        /// 建立標準輸入控制項所需的row
        /// </summary>
        protected TableRow TableRow1 = new TableRow();
        /// <summary>
        /// 建立標準輸入控制項所需的cell
        /// </summary>
        protected TableCell TableCell1 = new TableCell();
        /// <summary>
        /// 建立標準輸入控制項所需的cell
        /// </summary>
        protected TableCell TableCell2 = new TableCell();

        #region Public Properties & Methods

        /// <summary>
        ///是否需要驗證資料。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("是否需要驗證資料。")]
        public override bool NeedValidation
        {
            get
            {
                return false;
            }
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
            TextBox1.CausesValidation = this.NeedValidation;
            if (NeedValue)
            {
                RFV = new RequiredFieldValidator();
                RFV.ControlToValidate = TextBox1.ID;
                RFV.Font.Size = FontUnit.Small;
                RFV.Display = ValidatorDisplay.Dynamic;
                RFV.ErrorMessage = "請輸入內容值!";
                RFV.ValidationGroup = this.ValidationGroup;
                PlaceHolder1.Controls.Add(RFV);
            }

            if (NeedValue && ValidationType == OptionValidationType.Alert)
            {
                TextBox1.Attributes["onchange"] = "AlertValidation();";
                TextBox1.Attributes["onblur"] = "UpdateErrorMessage();";
            }

            SetValidationType(base.ValidationType, new BaseValidator[] { RFV }, PlaceHolder1);
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
            TextBox1.MaxLength = this.TextLength;
            TextBox1.Attributes["style"] += "text-align:" + _TextAlign + ";";
            if (this.ReadOnly) { TextBox1.Attributes.Add("readonly", this.ReadOnly.ToString().ToLower()); }
            TextBox1.TextMode = this.TextMode;
            TextBox1.Text = Text;
            TextBox1.ForeColor = this.TextForeColor;
            TextBox1.BackColor = this.TextBackColor;

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

        #endregion

    }
}