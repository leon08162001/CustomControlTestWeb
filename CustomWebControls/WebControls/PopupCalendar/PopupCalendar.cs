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
    /// 列舉-日曆控制項的外觀樣式。
    /// </summary>
    public enum CalendarStyle
    {
        /// <summary>
        /// 現代
        /// </summary>
        Modern = 0
    }

    /// <summary>
    /// 列舉-日曆控制項的日期資料樣式。
    /// </summary>
    public enum DateFormat
    {
        /// <summary>
        /// 年/月/日
        /// </summary>
        年月日 = 0,
        /// <summary>
        /// 年/月
        /// </summary>
        年月 = 1,
        /// <summary>
        /// 年
        /// </summary>
        年 = 2,
        /// <summary>
        /// 年/月/日 時:分:秒
        /// </summary>
        年月日時分秒 = 3,
        /// <summary>
        /// 年/月/日 時:分
        /// </summary>
        年月日時分 = 4,
        /// <summary>
        /// 年/月/日 時
        /// </summary>
        年月日時 = 5
    }

    /// <summary>
    /// 自定義日期輸入控制項。
    /// </summary>
    /// <remarks>
    /// <list type="bullet">
    /// <item><description>請使用<bold>FirstDate</bold>屬性取得使用者所選的日期值。</description></item>
    /// </list>
    /// </remarks>
    [ToolboxData("<{0}:PopupCalendar runat=server></{0}:PopupCalendar>")]
    [ToolboxBitmap(typeof(Calendar))]
    [ValidationProperty("FirstDate")]
    public class PopupCalendar : TextBoxBase
    {
        protected PlaceHolder PlaceHolder1 = new PlaceHolder();
        /// <summary>
        /// 建立PopupCalendar控制項的日曆圖片按鈕
        /// </summary>
        protected HtmlImage Img1 = new HtmlImage();
        /// <summary>
        /// 建立PopupCalendar控制項所需的table
        /// </summary>
        protected Table Table1 = new Table();
        /// <summary>
        /// 建立PopupCalendar控制項所需的row
        /// </summary>
        protected TableRow TableRow1 = new TableRow();
        /// <summary>
        /// 建立PopupCalendar控制項所需的cell
        /// </summary>
        protected TableCell TableCell1 = new TableCell();
        /// <summary>
        /// 建立PopupCalendar控制項所需的cell
        /// </summary>
        protected TableCell TableCell2 = new TableCell();
        protected CalendarStyle _CalendarStyle = CalendarStyle.Modern;
        protected DateFormat _DateFormat = DateFormat.年月日;
        protected Boolean _IsShowButton = true;

        #region Public Properties & Methods

        /// <summary>
        /// 第一個輸入方塊的內容值(為DateTime型態)。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("第一個輸入方塊的內容值(為DateTime型態)。")]
        public DateTime FirstDate
        {
            get
            {
                if (TextBox1.Text == "")
                    return Convert.ToDateTime("9999/12/01");
                else if(TextBox1.Text == "-1")
                    return Convert.ToDateTime("9999/12/30");
                else
                    try
                    {
                        if (DateFormat == DateFormat.年月日時)
                            return Convert.ToDateTime(TextBox1.Text + ":00");
                        else if (DateFormat == DateFormat.年)
                            return Convert.ToDateTime(TextBox1.Text + "/01/01");
                        else
                            return Convert.ToDateTime(TextBox1.Text);
                    }
                    catch (Exception ex)
                    { return Convert.ToDateTime("9999/12/01"); }
            }
            set
            {
                if (DateFormat == DateFormat.年月日)
                    TextBox1.Text = value.ToString("yyyy/MM/dd");
                else if (DateFormat == DateFormat.年月)
                    TextBox1.Text = value.ToString("yyyy/MM");
                else if (DateFormat == DateFormat.年)
                    TextBox1.Text = value.ToString("yyyy");
                else if (DateFormat == DateFormat.年月日時分秒)
                    TextBox1.Text = value.ToString("yyyy/MM/dd HH:mm:ss");
                else if (DateFormat == DateFormat.年月日時分)
                    TextBox1.Text = value.ToString("yyyy/MM/dd HH:mm");
                else if (DateFormat == DateFormat.年月日時)
                    TextBox1.Text = value.ToString("yyyy/MM/dd HH");
            }
        }

        /// <summary>
        /// 日曆控制項的外觀樣式。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("日曆控制項的外觀樣式。")]
        public CalendarStyle CalendarStyle
        {
            get { return _CalendarStyle; }
            set { _CalendarStyle = value; }
        }

        /// <summary>
        /// 日曆控制項的資料樣式。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("日曆控制項的日期資料樣式。")]
        public DateFormat DateFormat
        {
            get { return _DateFormat; }
            set { _DateFormat = value; }
        }

        /// <summary>
        /// 是否顯示日曆點擊按鈕。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
        Description("是否顯示日曆點擊按鈕。")]
        public Boolean IsShowButton
        {
            get { return _IsShowButton; }
            set { _IsShowButton = value; }
        }

        #endregion

        #region Protected Properties & Methods

        protected override void OnInit(EventArgs e)
        {
            TextBox1.ID = TextBoxID == null || TextBoxID == "" ? "SelectedDate1" : TextBoxID;
            Label1.ID = "Label1";
        }

        protected override void OnLoad(EventArgs e)
        {
            RequiredFieldValidator RFV = null;
            RegularExpressionValidator REV = null;
            TextBox1.CausesValidation = this.NeedValidation;
            if (NeedValue)
            {
                //產生日期欄位需值驗證控制項
                RFV = new RequiredFieldValidator();
                RFV.ControlToValidate = TextBox1.ID;
                RFV.Font.Size = FontUnit.Small;
                RFV.Attributes["style"] += "display:none;";
                RFV.Display = ValidatorDisplay.Dynamic;
                RFV.ErrorMessage = "請輸入日期值!";
                RFV.ValidationGroup = this.ValidationGroup;
                PlaceHolder1.Controls.Add(RFV);
            }

            if (NeedValidation)
            {
                //產生日期欄位格式驗證控制項

                REV = new RegularExpressionValidator();
                REV.ControlToValidate = TextBox1.ID;
                REV.Font.Size = FontUnit.Small;
                REV.Attributes["style"] += "display:none;";
                REV.Display = ValidatorDisplay.Dynamic;
                REV.ValidationGroup = this.ValidationGroup;
                if (this.DateFormat == DateFormat.年月日)
                {
                    REV.ErrorMessage = Title + "=>日期格式不正確(ex:2000/01/01)!";
                    REV.ValidationExpression = @"^((\d{2}(([02468][048])|([13579][26]))[\-\/\s]?((((0?[13578])|(1[02]))[\-\/\s]?((0?[1-9])|([1-2][0-9])|(3[01])))|(((0?[469])|(11))[\-\/\s]?((0?[1-9])|([1-2][0-9])|(30)))|(0?2[\-\/\s]?((0?[1-9])|([1-2][0-9])))))|(\d{2}(([02468][1235679])|([13579][01345789]))[\-\/\s]?((((0?[13578])|(1[02]))[\-\/\s]?((0?[1-9])|([1-2][0-9])|(3[01])))|(((0?[469])|(11))[\-\/\s]?((0?[1-9])|([1-2][0-9])|(30)))|(0?2[\-\/\s]?((0?[1-9])|(1[0-9])|(2[0-8]))))))(\s(((0?[1-9])|(1[0-2]))\:([0-5][0-9])((\s)|(\:([0-5][0-9])\s))([AM|PM|am|pm]{2,2})))?$";
                }
                else if (this.DateFormat == DateFormat.年月)
                {
                    REV.ErrorMessage = Title + "=>日期格式不正確(ex:2000/01)!";
                    REV.ValidationExpression = @"^[0-9]{4}/(0[123456789]|(10|11|12))";
                }
                else if (this.DateFormat == DateFormat.年)
                {
                    REV.ErrorMessage = Title + "=>日期格式不正確(ex:2000)!";
                    REV.ValidationExpression = @"^[0-9]{4}";
                }
                else if (this.DateFormat == DateFormat.年月日時分秒)
                {
                    REV.ErrorMessage = Title + "=>日期格式不正確(ex:2000/01/01 00:00:00)!";
                    REV.ValidationExpression = @"^((\d{2}(([02468][048])|([13579][26]))[\-\/\s]?((((0?[13578])|(1[02]))[\-\/\s]?((0?[1-9])|([1-2][0-9])|(3[01])))|(((0?[469])|(11))[\-\/\s]?((0?[1-9])|([1-2][0-9])|(30)))|(0?2[\-\/\s]?((0?[1-9])|([1-2][0-9])))))|(\d{2}(([02468][1235679])|([13579][01345789]))[\-\/\s]?((((0?[13578])|(1[02]))[\-\/\s]?((0?[1-9])|([1-2][0-9])|(3[01])))|(((0?[469])|(11))[\-\/\s]?((0?[1-9])|([1-2][0-9])|(30)))|(0?2[\-\/\s]?((0?[1-9])|(1[0-9])|(2[0-8])))))) (([0-1]\d)|([1-9])|(2[0-3]|(0))):(([0-5]\d)|([1-9])):(([0-5]\d)|([1-9]))$";
                }
                else if (this.DateFormat == DateFormat.年月日時分)
                {
                    REV.ErrorMessage = Title + "=>日期格式不正確(ex:2000/01/01 00:00)!";
                    REV.ValidationExpression = @"^((\d{2}(([02468][048])|([13579][26]))[\-\/\s]?((((0?[13578])|(1[02]))[\-\/\s]?((0?[1-9])|([1-2][0-9])|(3[01])))|(((0?[469])|(11))[\-\/\s]?((0?[1-9])|([1-2][0-9])|(30)))|(0?2[\-\/\s]?((0?[1-9])|([1-2][0-9])))))|(\d{2}(([02468][1235679])|([13579][01345789]))[\-\/\s]?((((0?[13578])|(1[02]))[\-\/\s]?((0?[1-9])|([1-2][0-9])|(3[01])))|(((0?[469])|(11))[\-\/\s]?((0?[1-9])|([1-2][0-9])|(30)))|(0?2[\-\/\s]?((0?[1-9])|(1[0-9])|(2[0-8])))))) (([0-1]\d)|([1-9])|(2[0-3]|(0))):(([0-5]\d)|([1-9]))$";
                }
                else if (this.DateFormat == DateFormat.年月日時)
                {
                    REV.ErrorMessage = Title + "=>日期格式不正確(ex:2000/01/01 00)!";
                    REV.ValidationExpression = @"^((\d{2}(([02468][048])|([13579][26]))[\-\/\s]?((((0?[13578])|(1[02]))[\-\/\s]?((0?[1-9])|([1-2][0-9])|(3[01])))|(((0?[469])|(11))[\-\/\s]?((0?[1-9])|([1-2][0-9])|(30)))|(0?2[\-\/\s]?((0?[1-9])|([1-2][0-9])))))|(\d{2}(([02468][1235679])|([13579][01345789]))[\-\/\s]?((((0?[13578])|(1[02]))[\-\/\s]?((0?[1-9])|([1-2][0-9])|(3[01])))|(((0?[469])|(11))[\-\/\s]?((0?[1-9])|([1-2][0-9])|(30)))|(0?2[\-\/\s]?((0?[1-9])|(1[0-9])|(2[0-8])))))) (([0-1]\d)|([1-9])|(2[0-3]|(0)))$";
                }
                PlaceHolder1.Controls.Add(REV);
            }

            SetValidationType(base.ValidationType, new BaseValidator[] { RFV, REV }, PlaceHolder1);
        }

        protected override void CreateChildControls()
        {
            EnsureChildControls();
            Title = Title == "" ? "請輸入西元日期 : " : Title;
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
            if (DateFormat == DateFormat.年月日)
                TextBox1.Text = FirstDate.ToString("yyyy/MM/dd") == "9999/12/01" ? "" : FirstDate.ToString("yyyy/MM/dd") == "9999/12/30" ? "-1" : FirstDate.ToString("yyyy/MM/dd");
            else if (this.DateFormat == DateFormat.年月)
                TextBox1.Text = FirstDate.ToString("yyyy/MM") == "9999/12" ? "" : FirstDate.ToString("yyyy/MM");
            else if (this.DateFormat == DateFormat.年)
                TextBox1.Text = FirstDate.ToString("yyyy");
            else if (this.DateFormat == DateFormat.年月日時分秒)
                TextBox1.Text = FirstDate.ToString("yyyy/MM/dd") == "9999/12/01" ? "" : FirstDate.ToString("yyyy/MM/dd") == "9999/12/30" ? "-1" : FirstDate.ToString("yyyy/MM/dd HH:mm:ss");
            else if (this.DateFormat == DateFormat.年月日時分)
                TextBox1.Text = FirstDate.ToString("yyyy/MM/dd") == "9999/12/01" ? "" : FirstDate.ToString("yyyy/MM/dd") == "9999/12/30" ? "-1" : FirstDate.ToString("yyyy/MM/dd HH:mm");
            else if (this.DateFormat == DateFormat.年月日時)
                TextBox1.Text = FirstDate.ToString("yyyy/MM/dd") == "9999/12/01" ? "" : FirstDate.ToString("yyyy/MM/dd") == "9999/12/30" ? "-1" : FirstDate.ToString("yyyy/MM/dd HH");
            TextBox1.ForeColor = this.TextForeColor;
            TextBox1.BackColor = this.TextBackColor;
            Img1.ID = "CalendarImage1";
            Img1.Alt = "開啟日曆";
            Img1.Src = this.Page != null ? this.Page.ClientScript.GetWebResourceUrl(this.GetType(), "APTemplate.Resources.calendar.gif") : "";
            Img1.Border = 0;
            Img1.Width = 24;
            Img1.Height = 24;
            Img1.Alt = "開啟日曆";
            Img1.Disabled = !this.Enabled;
            Img1.Align = "top";
            Img1.Visible = this.IsShowButton;
            Img1.Attributes["style"] = "cursor:pointer";

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
            TableCell2.Controls.Add(Img1);
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
                TextBox1.Attributes["onchange"] = "AlertValidation();";
                TextBox1.Attributes["onblur"] = "UpdateErrorMessage();";
            }
            string strJS = "";

            if (this.DateFormat == DateFormat.年月日)
            {
                strJS = "dateFormat='yyyy/mm/dd';popUpCalendar(this, document.getElementById('" + TextBox1.ClientID + "'), dateFormat,-1,-1);";
                TextBox1.Text = FirstDate.ToString("yyyy/MM/dd") == "9999/12/01" ? "" : FirstDate.ToString("yyyy/MM/dd") == "9999/12/30" ? "-1" : FirstDate.ToString("yyyy/MM/dd");
            }
            else if (this.DateFormat == DateFormat.年月)
            {
                strJS = "dateFormat='yyyy/mm';popUpCalendar(this, document.getElementById('" + TextBox1.ClientID + "'), dateFormat,-1,-1);";
                TextBox1.Text = FirstDate.ToString("yyyy/MM") == "9999/12" ? "" : FirstDate.ToString("yyyy/MM");
            }
            else if (this.DateFormat == DateFormat.年)
            {
                strJS = "dateFormat='yyyy';popUpCalendar(this, document.getElementById('" + TextBox1.ClientID + "'), dateFormat,-1,-1);";
                TextBox1.Text = FirstDate.ToString("yyyy");
            }
            else if (this.DateFormat == DateFormat.年月日時分秒)
            {
                strJS = "dateFormat='yyyy/mm/dd HH:mm:ss';popUpCalendar(this, document.getElementById('" + TextBox1.ClientID + "'), dateFormat,-1,-1);";
                TextBox1.Text = FirstDate.ToString("yyyy/MM/dd") == "9999/12/01" ? "" : FirstDate.ToString("yyyy/MM/dd") == "9999/12/30" ? "-1" : FirstDate.ToString("yyyy/MM/dd HH:mm:ss");
            }
            else if (this.DateFormat == DateFormat.年月日時分)
            {
                strJS = "dateFormat='yyyy/mm/dd HH:mm';popUpCalendar(this, document.getElementById('" + TextBox1.ClientID + "'), dateFormat,-1,-1);";
                TextBox1.Text = FirstDate.ToString("yyyy/MM/dd") == "9999/12/01" ? "" : FirstDate.ToString("yyyy/MM/dd") == "9999/12/30" ? "-1" : FirstDate.ToString("yyyy/MM/dd HH:mm");
            }
            else if (this.DateFormat == DateFormat.年月日時)
            {
                strJS = "dateFormat='yyyy/mm/dd HH';popUpCalendar(this, document.getElementById('" + TextBox1.ClientID + "'), dateFormat,-1,-1);";
                TextBox1.Text = FirstDate.ToString("yyyy/MM/dd") == "9999/12/01" ? "" : FirstDate.ToString("yyyy/MM/dd") == "9999/12/30" ? "-1" : FirstDate.ToString("yyyy/MM/dd HH");
            }
            if (IsShowButton)
            {
                Img1.Attributes.Add("onclick", strJS);
                TextBox1.Attributes.Add("onclick", strJS);
            }
            else
            {
                TextBox1.Attributes.Add("onclick", strJS);
            }
            base.OnPreRender(e);
        }

        protected override void LoadControlState(object savedState)
        {
            object[] allStates = (object[])savedState;
            FirstDate = Convert.ToDateTime(allStates[0]);
        }

        protected override object SaveControlState()
        {
            object[] allStates = new object[1];
            allStates[0] = FirstDate;
            return allStates;
        }

        #endregion

    }
}