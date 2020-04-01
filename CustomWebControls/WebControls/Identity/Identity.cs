using System;
using System.Configuration;
using System.ComponentModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Drawing;
using AjaxPro;

namespace APTemplate
{
    /// <summary>
    /// 自定義的身分證輸入控制項。
    /// </summary>
    /// <remarks>
    /// <list type="bullet">
    /// <item><description>每當使用者輸入完身分證資料後，以Ajax技術向server端回傳驗證身分證資料，並回傳結果。</description></item>
    /// <item><description>請使用<bold>Text</bold>屬性取得使用者所輸入的值。</description></item>
    /// </list>
    /// </remarks>
    [ToolboxData("<{0}:Identity runat=server></{0}:Identity>")]
    [ToolboxBitmap(typeof(TextBox))]
    [ValidationProperty("Text")]
    public class Identity : TextBoxBase
    {
        protected PlaceHolder PlaceHolder1 = new PlaceHolder();
        /// <summary>
        /// 建立Identity控制項所需的table
        /// </summary>
        protected Table Table1 = new Table();
        /// <summary>
        /// 建立Identity控制項所需的row
        /// </summary>
        protected TableRow TableRow1 = new TableRow();
        /// <summary>
        /// 建立Identity控制項所需的cell
        /// </summary>
        protected TableCell TableCell1 = new TableCell();
        /// <summary>
        /// 建立Identity控制項所需的cell
        /// </summary>
        protected TableCell TableCell2 = new TableCell();

        #region Public Properties & Methods

        /// <summary>
        /// 檢查身份證字號
        /// </summary>
        /// <param name="ID">身份證字號</param>
        /// <returns></returns>
        [AjaxMethod]
        public bool CheckID(string ID)
        {
            if (ID.Length != 10 && ID.Length != 11)
            {
                return false;
            }
            else
            {
                if (ID.Length == 11)    //11碼不查核
                {
                    return true;
                }
                else    //10碼
                {
                    int FirstChar;
                    if (int.TryParse(ID.Substring(0, 1), out FirstChar)) //10碼若第一碼為數字
                    {
                        return true;
                    }
                    else //10碼若第一碼為非數字按身分證號碼查核
                    {
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
                }
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

        #endregion

        #region Protected Properties & Methods

        protected override void OnInit(EventArgs e)
        {
            TextBox1.ID = TextBoxID == null || TextBoxID == "" ? "TextBox1" : TextBoxID;
            Label1.ID = "Label1";
        }

        protected override void OnLoad(EventArgs e)
        {
            AjaxPro.Utility.RegisterTypeForAjax(typeof(APTemplate.Identity));
            RequiredFieldValidator RFV = null;
            CustomValidator valid = null;
            TextBox1.CausesValidation = this.NeedValidation;
            if (NeedValue)
            {
                RFV = new RequiredFieldValidator();
                RFV.ControlToValidate = TextBox1.ID;
                RFV.ErrorMessage = Title + "=> 請輸入ID值!";
                RFV.Font.Size = FontUnit.Small;
                RFV.Attributes["style"] += "display:none;";
                RFV.Display = ValidatorDisplay.Dynamic;
                RFV.ValidationGroup = this.ValidationGroup;
                PlaceHolder1.Controls.Add(RFV);
            }

            if (NeedValidation)
            {
                valid = new CustomValidator();
                valid.ControlToValidate = TextBox1.ID;
                valid.IsValid = true;
                valid.Display = ValidatorDisplay.Dynamic;
                valid.ClientValidationFunction = "Identity_ClientValidate_" + this.ClientID;
                valid.Font.Size = FontUnit.Small;
                valid.Attributes["style"] += "display:none;";
                valid.ErrorMessage = Title + "=> 身份證字號格式不正確!";
                valid.ValidationGroup = this.ValidationGroup;
                PlaceHolder1.Controls.Add(valid);
            }

            SetValidationType(base.ValidationType, new BaseValidator[] { RFV, valid }, PlaceHolder1);
        }

        protected override void OnPreRender(EventArgs e)
        {
            if ((NeedValidation || NeedValue) && ValidationType == OptionValidationType.Alert)
            {
                TextBox1.Attributes["onchange"] = "AlertValidation();";
                TextBox1.Attributes["onblur"] = "UpdateErrorMessage();";
            }
            if (!Page.IsStartupScriptRegistered(TextBox1.ClientID + "_ValidScript"))
            {
                string script = "<script language=javascript> \n" +
                  "\t function Identity_ClientValidate_" +this.ClientID + "(source, arguments) { \n" +
                  "\t if(checkEng(arguments.Value.substr(0,1))) { \n" +
                  "\t\t document.getElementById('" + TextBox1.ClientID + "').value = arguments.Value.substr(0,1).toUpperCase() + arguments.Value.substr(1); \n" +
                  "\t\t arguments.Value = document.getElementById('" + TextBox1.ClientID + "').value; \n" +
                  "} \n" +
                  "\t var res = APTemplate.Identity.CheckID(arguments.Value); \n" +
                  "\t arguments.IsValid=(res.value); \n" +
                  "} \n" +
                  "\t function checkEng(str) { \n" +
                  "\t return str.match(/^[a-zA-Z]*$/); \n" +
                  "} \n" +
                  "</script>";
                string ClientScript = script;
                Page.ClientScript.RegisterStartupScript(this.GetType(), TextBox1.ClientID + "_ValidScript", ClientScript);
            }
            base.OnPreRender(e);
        }

        protected override void CreateChildControls()
        {
            this.Controls.Clear();
            Title = Title == "" ? "身分證ID : " : Title;
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

        #region Private Properties & Methods

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