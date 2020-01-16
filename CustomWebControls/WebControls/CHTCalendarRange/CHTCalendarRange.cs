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
    /// 可輸入兩組日期區間的自定義日期輸入控制項。
    /// </summary>
    /// <remarks>
    /// <list type="bullet">
    /// <item><description>兩組日期區間的比較運算子預設是第二組日期需大餘等於第一組日期。</description></item>
    /// <item><description>請設定<bold>Operator</bold>屬性改變預設比較形式。</description></item>
    /// <item><description>請使用<bold>FirstDate</bold>屬性取得使用者所選的第一個日期值;<bold>SecondDate</bold>屬性取得使用者所選的第二個日期值。</description></item>
    /// </list>
    /// </remarks>
    [ToolboxData("<{0}:CHTCalendarRange runat=server></{0}:CHTCalendarRange>")]
    [ToolboxBitmap(typeof(Calendar))]
    public class CHTCalendarRange : TextBoxBase
    {
        /// <summary>
        /// 儲存第二個輸入方塊的控制項ID。
        /// </summary>
        protected string _TextBoxID1 = null;
        protected PlaceHolder PlaceHolder1 = new PlaceHolder();
        /// <summary>
        /// 控制項內的第二個輸入方塊。
        /// </summary>
        protected TextBox TextBox2 = new TextBox();
        protected PlaceHolder PlaceHolder2 = new PlaceHolder();
        /// <summary>
        /// 開啟第一個日曆的圖像按鈕。
        /// </summary>
        protected HtmlImage Img1 = new HtmlImage();
        /// <summary>
        /// 開啟第二個日曆的圖像按鈕。
        /// </summary>
        protected HtmlImage Img2 = new HtmlImage();
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
        protected CalendarStyle _CalendarStyle = CalendarStyle.Modern;
        protected DateFormat _DateFormat = DateFormat.年月日;
        protected Boolean _IsShowButton = true;
        protected LiteralControl _SeperatorLine = new LiteralControl("<span style='vertical-align:top;'>&nbsp;—&nbsp;</span>");

        #region Public Properties & Methods

        /// <summary>
        /// 第二個輸入方塊的控制項ID。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("第二個輸入方塊的控制項ID。")]
        public string TextBoxID1
        {
            get { return _TextBoxID1; }
            set { _TextBoxID1 = value; }
        }

        /// <summary>
        /// 第一個輸入方塊的內容值。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("第一個輸入方塊的內容值。")]
        public DateTime FirstDate
        {
            get
            {
                if (TextBox1.Text == "")
                    return Convert.ToDateTime("9999/01/01");
                else if (TextBox1.Text == "-1")
                    return Convert.ToDateTime("9999/01/31");
                else
                    try
                    {
                        if (DateFormat == DateFormat.年月日時)
                        {
                            if (int.Parse(TextBox1.Text.Substring(0, TextBox1.Text.IndexOf("/"))) > 99)
                            {
                                return Convert.ToDateTime(TextBox1.Text + ":00");
                            }
                            else if (int.Parse(TextBox1.Text.Substring(0, TextBox1.Text.IndexOf("/"))) > 29)
                            {
                                return Convert.ToDateTime(TextBox1.Text + ":00").AddYears(-1900);
                            }
                            else
                            {
                                return Convert.ToDateTime(TextBox1.Text + ":00").AddYears(-2000);
                            }
                        }
                        else if (DateFormat == DateFormat.年)
                        {
                            if (int.Parse(TextBox1.Text.Substring(0, TextBox1.Text.IndexOf("/"))) > 99)
                            {
                                return Convert.ToDateTime(TextBox1.Text + "/01/01");
                            }
                            else if (int.Parse(TextBox1.Text.Substring(0, TextBox1.Text.IndexOf("/"))) > 29)
                            {
                                return Convert.ToDateTime(TextBox1.Text + "/01/01").AddYears(-1900);
                            }
                            else
                            {
                                return Convert.ToDateTime(TextBox1.Text + "/01/01").AddYears(-2000);
                            }
                        }
                        else
                        {
                            if (int.Parse(TextBox1.Text.Substring(0, TextBox1.Text.IndexOf("/"))) > 99)
                            {
                                return Convert.ToDateTime(TextBox1.Text);
                            }
                            else if (int.Parse(TextBox1.Text.Substring(0, TextBox1.Text.IndexOf("/"))) > 29)
                            {
                                return Convert.ToDateTime(TextBox1.Text).AddYears(-1900);
                            }
                            else
                            {
                                return Convert.ToDateTime(TextBox1.Text).AddYears(-2000);
                            }
                        }
                    }
                    catch (Exception ex)
                    { return Convert.ToDateTime("9999/01/01"); }
            }
            set
            {
                if (DateFormat == DateFormat.年月日)
                    TextBox1.Text = value.ToString("yyy/MM/dd");
                else if (DateFormat == DateFormat.年月)
                    TextBox1.Text = value.ToString("yyy/MM");
                else if (DateFormat == DateFormat.年)
                    TextBox1.Text = value.ToString("yyy");
                else if (DateFormat == DateFormat.年月日時分秒)
                    TextBox1.Text = value.ToString("yyy/MM/dd HH:mm:ss");
                else if (DateFormat == DateFormat.年月日時分)
                    TextBox1.Text = value.ToString("yyy/MM/dd HH:mm");
                else if (DateFormat == DateFormat.年月日時)
                    TextBox1.Text = value.ToString("yyy/MM/dd HH");
            }
        }

        /// <summary>
        /// 第二個輸入方塊的內容值。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("第二個輸入方塊的內容值。")]
        public DateTime SecondDate
        {
            get
            {
                if (TextBox2.Text == "")
                    return Convert.ToDateTime("9999/01/01");
                else if (TextBox2.Text == "-1")
                    return Convert.ToDateTime("9999/01/31");
                else
                    try
                    {
                        if (DateFormat == DateFormat.年月日時)
                            if (int.Parse(TextBox2.Text.Substring(0, TextBox2.Text.IndexOf("/"))) > 99)
                            {
                                return Convert.ToDateTime(TextBox2.Text + ":00");
                            }
                            else if (int.Parse(TextBox1.Text.Substring(0, TextBox1.Text.IndexOf("/"))) > 29)
                            {
                                return Convert.ToDateTime(TextBox2.Text + ":00").AddYears(-1900);
                            }
                            else
                            {
                                return Convert.ToDateTime(TextBox2.Text + ":00").AddYears(-2000);
                            }
                        else if (DateFormat == DateFormat.年)
                            if (int.Parse(TextBox2.Text.Substring(0, TextBox2.Text.IndexOf("/"))) > 99)
                            {
                                return Convert.ToDateTime(TextBox2.Text + "/01/01");
                            }
                            else if (int.Parse(TextBox1.Text.Substring(0, TextBox1.Text.IndexOf("/"))) > 29)
                            {
                                return Convert.ToDateTime(TextBox2.Text + "/01/01").AddYears(-1900);
                            }
                            else
                            {
                                return Convert.ToDateTime(TextBox2.Text + "/01/01").AddYears(-2000);
                            }
                        else
                            if (int.Parse(TextBox2.Text.Substring(0, TextBox2.Text.IndexOf("/"))) > 99)
                            {
                                return Convert.ToDateTime(TextBox2.Text);
                            }
                            else if (int.Parse(TextBox1.Text.Substring(0, TextBox1.Text.IndexOf("/"))) > 29)
                            {
                                return Convert.ToDateTime(TextBox2.Text).AddYears(-1900);
                            }
                            else
                            {
                                return Convert.ToDateTime(TextBox2.Text).AddYears(-2000);
                            }
                    }
                    catch (Exception ex)
                    { return Convert.ToDateTime("9999/01/01"); }
            }
            set
            {
                if (DateFormat == DateFormat.年月日)
                    TextBox2.Text = value.ToString("yyy/MM/dd");
                else if (DateFormat == DateFormat.年月)
                    TextBox2.Text = value.ToString("yyy/MM");
                else if (DateFormat == DateFormat.年)
                    TextBox2.Text = value.ToString("yyy");
                else if (DateFormat == DateFormat.年月日時分秒)
                    TextBox2.Text = value.ToString("yyy/MM/dd HH:mm:ss");
                else if (DateFormat == DateFormat.年月日時分)
                    TextBox2.Text = value.ToString("yyy/MM/dd HH:mm");
                else if (DateFormat == DateFormat.年月日時)
                    TextBox2.Text = value.ToString("yyy/MM/dd HH");
            }
        }

        /// <summary>
        /// 日曆控制項的樣式。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("日曆控制項的樣式。")]
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

        #endregion

        #region Protected Properties & Methods

        protected override void OnInit(EventArgs e)
        {
            TextBox1.ID = TextBoxID == null || TextBoxID == "" ? "SelectedDate1" : TextBoxID;
            TextBox2.ID = TextBoxID1 == null || TextBoxID1 == "" ? "SelectedDate2" : TextBoxID1;
            Label1.ID = "Label1";
            Page.RegisterRequiresControlState(this);
        }

        protected override void OnLoad(EventArgs e)
        {
            RequiredFieldValidator RFV1 = null;
            RequiredFieldValidator RFV2 = null;
            RegularExpressionValidator REV1 = null;
            RegularExpressionValidator REV2 = null;
            CompareValidator CV1 = null;
            TextBox1.CausesValidation = this.NeedValidation;
            TextBox2.CausesValidation = this.NeedValidation;

            if (NeedValue)
            {
                //產生起始日期需值驗證控制項
                RFV1 = new RequiredFieldValidator();
                RFV1.ControlToValidate = TextBox1.ID;
                RFV1.ErrorMessage = "請輸入起始日期值!";
                RFV1.Font.Size = FontUnit.Small;
                RFV1.Display = ValidatorDisplay.Dynamic;
                RFV1.ValidationGroup = this.ValidationGroup;
                PlaceHolder1.Controls.Add(RFV1);
                //產生結束日期需值驗證控制項
                RFV2 = new RequiredFieldValidator();
                RFV2.ControlToValidate = TextBox2.ID;
                RFV2.ErrorMessage = "請輸入結束日期值!";
                RFV2.Font.Size = FontUnit.Small;
                RFV2.Display = ValidatorDisplay.Dynamic;
                RFV2.ValidationGroup = this.ValidationGroup;
                PlaceHolder2.Controls.Add(RFV2);
            }

            if (NeedValidation)
            {
                //產生起始日期格式驗證控制項
                REV1 = new RegularExpressionValidator();
                REV1.ControlToValidate = TextBox1.ID;
                REV1.Font.Size = FontUnit.Small;
                REV1.Display = ValidatorDisplay.Dynamic;
                REV1.ValidationGroup = this.ValidationGroup;
                if (this.DateFormat == DateFormat.年月日)
                {
                    REV1.ErrorMessage = "起始民國日期格式不正確(ex:100/01/01)!";
                    REV1.ValidationExpression = @"^[0-9]{1,3}/(((0[13578]|(10|12))/(0[1-9]|[1-2][0-9]|3[0-1]))|(02/(0[1-9]|[1-2][0-9]))|((0[469]|11)/(0[1-9]|[1-2][0-9]|30)))$";
                }
                else if (this.DateFormat == DateFormat.年月)
                {
                    REV1.ErrorMessage = Title + "=>起始民國日期格式不正確(ex:100/01)!";
                    REV1.ValidationExpression = @"^[0-9]{1,3}/(0[123456789]|(10|11|12))";
                }
                else if (this.DateFormat == DateFormat.年)
                {
                    REV1.ErrorMessage = Title + "=>起始民國日期格式不正確(ex:100)!";
                    REV1.ValidationExpression = @"^[0-9]{1,3}{4}";
                }
                else if (this.DateFormat == DateFormat.年月日時分秒)
                {
                    REV1.ErrorMessage = Title + "=>起始民國日期格式不正確(ex:100/01/01 00:00:00)!";
                    REV1.ValidationExpression = @"^[0-9]{1,3}/(((0[13578]|(10|12))/(0[1-9]|[1-2][0-9]|3[0-1]))|(02/(0[1-9]|[1-2][0-9]))|((0[469]|11)/(0[1-9]|[1-2][0-9]|30))) (([0-1]\d)|([1-9])|(2[0-3]|(0))):(([0-5]\d)|([1-9])):(([0-5]\d)|([1-9]))$";
                }
                else if (this.DateFormat == DateFormat.年月日時分)
                {
                    REV1.ErrorMessage = Title + "=>起始民國日期格式不正確(ex:100/01/01 00:00)!";
                    REV1.ValidationExpression = @"^[0-9]{1,3}/(((0[13578]|(10|12))/(0[1-9]|[1-2][0-9]|3[0-1]))|(02/(0[1-9]|[1-2][0-9]))|((0[469]|11)/(0[1-9]|[1-2][0-9]|30))) (([0-1]\d)|([1-9])|(2[0-3]|(0))):(([0-5]\d)|([1-9]))$";
                }
                else if (this.DateFormat == DateFormat.年月日時)
                {
                    REV1.ErrorMessage = Title + "=>起始民國日期格式不正確(ex:100/01/01 00)!";
                    REV1.ValidationExpression = @"^[0-9]{1,3}/(((0[13578]|(10|12))/(0[1-9]|[1-2][0-9]|3[0-1]))|(02/(0[1-9]|[1-2][0-9]))|((0[469]|11)/(0[1-9]|[1-2][0-9]|30))) (([0-1]\d)|([1-9])|(2[0-3]|(0)))$";
                }
                PlaceHolder1.Controls.Add(REV1);
                //產生結束日期格式驗證控制項	
                REV2 = new RegularExpressionValidator();
                REV2.ControlToValidate = TextBox2.ID;
                REV2.Font.Size = FontUnit.Small;
                REV2.Display = ValidatorDisplay.Dynamic;
                REV2.ValidationGroup = this.ValidationGroup;
                if (this.DateFormat == DateFormat.年月日)
                {
                    REV2.ErrorMessage = "結束民國日期格式不正確(ex:100/01/01)!";
                    REV2.ValidationExpression = @"^[0-9]{1,3}/(((0[13578]|(10|12))/(0[1-9]|[1-2][0-9]|3[0-1]))|(02/(0[1-9]|[1-2][0-9]))|((0[469]|11)/(0[1-9]|[1-2][0-9]|30)))$";
                }
                else if (this.DateFormat == DateFormat.年月)
                {
                    REV2.ErrorMessage = Title + "=>結束民國日期格式不正確(ex:100/01)!";
                    REV2.ValidationExpression = @"^[0-9]{1,3}/(0[123456789]|(10|11|12))";
                }
                else if (this.DateFormat == DateFormat.年)
                {
                    REV2.ErrorMessage = Title + "=>結束民國日期格式不正確(ex:100)!";
                    REV2.ValidationExpression = @"^[0-9]{1,3}{4}";
                }
                else if (this.DateFormat == DateFormat.年月日時分秒)
                {
                    REV2.ErrorMessage = Title + "=>結束民國日期格式不正確(ex:100/01/01 00:00:00)!";
                    REV2.ValidationExpression = @"^[0-9]{1,3}/(((0[13578]|(10|12))/(0[1-9]|[1-2][0-9]|3[0-1]))|(02/(0[1-9]|[1-2][0-9]))|((0[469]|11)/(0[1-9]|[1-2][0-9]|30))) (([0-1]\d)|([1-9])|(2[0-3]|(0))):(([0-5]\d)|([1-9])):(([0-5]\d)|([1-9]))$";
                }
                else if (this.DateFormat == DateFormat.年月日時分)
                {
                    REV2.ErrorMessage = Title + "=>結束民國日期格式不正確(ex:100/01/01 00:00)!";
                    REV2.ValidationExpression = @"^[0-9]{1,3}/(((0[13578]|(10|12))/(0[1-9]|[1-2][0-9]|3[0-1]))|(02/(0[1-9]|[1-2][0-9]))|((0[469]|11)/(0[1-9]|[1-2][0-9]|30))) (([0-1]\d)|([1-9])|(2[0-3]|(0))):(([0-5]\d)|([1-9]))$";
                }
                else if (this.DateFormat == DateFormat.年月日時)
                {
                    REV2.ErrorMessage = Title + "=>結束民國日期格式不正確(ex:100/01/01 00)!";
                    REV2.ValidationExpression = @"^[0-9]{1,3}/(((0[13578]|(10|12))/(0[1-9]|[1-2][0-9]|3[0-1]))|(02/(0[1-9]|[1-2][0-9]))|((0[469]|11)/(0[1-9]|[1-2][0-9]|30))) (([0-1]\d)|([1-9])|(2[0-3]|(0)))$";
                }
                PlaceHolder2.Controls.Add(REV2);
                //產生比較起始日期與結束日期大小所需驗證控制項
                CV1 = new CompareValidator();
                CV1.ControlToValidate = TextBox2.ID;
                CV1.ControlToCompare = TextBox1.ID;
                CV1.ErrorMessage = "結束日必須大於等於起始日!";
                CV1.Font.Size = FontUnit.Small;
                CV1.Display = ValidatorDisplay.Dynamic;
                CV1.Type = ValidationDataType.String;
                CV1.Operator = ValidationCompareOperator.GreaterThanEqual;
                CV1.ValidationGroup = this.ValidationGroup;
                PlaceHolder2.Controls.Add(CV1);
            }

            if ((NeedValidation || NeedValue) && ValidationType == OptionValidationType.Alert)
            {
                TextBox1.Attributes["onchange"] = "AlertValidation();";
                TextBox1.Attributes["onblur"] = "UpdateErrorMessage();";
                TextBox2.Attributes["onchange"] = "AlertValidation();";
                TextBox2.Attributes["onblur"] = "UpdateErrorMessage();";
            }
            SetValidationType(base.ValidationType, new BaseValidator[] { RFV1, REV1 }, PlaceHolder1);
            SetValidationType(base.ValidationType, new BaseValidator[] { RFV2, REV2, CV1 }, PlaceHolder2);
        }

        protected override void CreateChildControls()
        {
            EnsureChildControls();
            this.Controls.Clear();
            Title = Title == "" ? "請輸入民國日期區間 : " : Title;
            base.Label1.Font.Size = this.Font.Size.IsEmpty ? FontUnit.Small : this.Font.Size;
            Label1.Text = NeedValue ? "<font color=red>*</font>" + this.Title : this.Title;
            Label1.Width = this.TitleWidth;
            Label1.Visible = this.IsShowTitle;
            TableCell1.Visible = this.IsShowTitle;
            Label1.Attributes["style"] += "text-align:" + _TitleAlign + ";";
            TextBox1.Width = this.TextBoxWidth;
            TextBox2.Width = this.TextBoxWidth;
            TextBox1.Height = this.TextBoxHeight;
            TextBox2.Height = this.TextBoxHeight;
            TextBox1.MaxLength = this.TextLength;
            TextBox2.MaxLength = this.TextLength;
            TextBox1.Attributes["style"] += "text-align:" + _TextAlign + ";";
            TextBox2.Attributes["style"] += "text-align:" + _TextAlign + ";";
            if (this.ReadOnly) { TextBox1.Attributes.Add("readonly", this.ReadOnly.ToString().ToLower()); }
            if (this.ReadOnly) { TextBox2.Attributes.Add("readonly", this.ReadOnly.ToString().ToLower()); }
            if (DateFormat == DateFormat.年月日)
            {
                TextBox1.Text = FirstDate.ToString("yyyy/MM/dd") == "9999/01/01" ? "" : FirstDate.ToString("yyyy/MM/dd") == "9999/01/31" ? "-1" : FirstDate.Year > 99 ? FirstDate.ToString("yyy/MM/dd") : FirstDate.ToString("yy/MM/dd");
                TextBox2.Text = SecondDate.ToString("yyyy/MM/dd") == "9999/01/01" ? "" : SecondDate.ToString("yyyy/MM/dd") == "9999/01/31" ? "-1" : SecondDate.Year > 99 ? SecondDate.ToString("yyy/MM/dd") : SecondDate.ToString("yy/MM/dd");
            }
            else if (this.DateFormat == DateFormat.年月)
            {
                TextBox1.Text = FirstDate.ToString("yyyy/MM") == "9999/01" ? "" : FirstDate.Year > 99 ? FirstDate.ToString("yyy/MM") : FirstDate.ToString("yy/MM");
                TextBox2.Text = SecondDate.ToString("yyyy/MM") == "9999/01" ? "" : SecondDate.Year > 99 ? SecondDate.ToString("yyy/MM") : SecondDate.ToString("yy/MM");
            }
            else if (this.DateFormat == DateFormat.年)
            {
                TextBox1.Text = FirstDate.Year > 99 ? FirstDate.ToString("yyy") : FirstDate.ToString("yy");
                TextBox2.Text = SecondDate.Year > 99 ? SecondDate.ToString("yyy") : SecondDate.ToString("yy");
            }
            else if (this.DateFormat == DateFormat.年月日時分秒)
            {
                TextBox1.Text = FirstDate.ToString("yyyy/MM/dd") == "9999/01/01" ? "" : FirstDate.ToString("yyyy/MM/dd") == "9999/01/31" ? "-1" : FirstDate.Year > 99 ? FirstDate.ToString("yyy/MM/dd HH:mm:ss") : FirstDate.ToString("yy/MM/dd HH:mm:ss");
                TextBox2.Text = SecondDate.ToString("yyyy/MM/dd") == "9999/01/01" ? "" : SecondDate.ToString("yyyy/MM/dd") == "9999/01/31" ? "-1" : SecondDate.Year > 99 ? SecondDate.ToString("yyy/MM/dd HH:mm:ss") : SecondDate.ToString("yy/MM/dd HH:mm:ss");
            }
            else if (this.DateFormat == DateFormat.年月日時分)
            {
                TextBox1.Text = FirstDate.ToString("yyyy/MM/dd") == "9999/01/01" ? "" : FirstDate.ToString("yyyy/MM/dd") == "9999/01/31" ? "-1" : FirstDate.Year > 99 ? FirstDate.ToString("yyy/MM/dd HH:mm") : FirstDate.ToString("yy/MM/dd HH:mm");
                TextBox2.Text = SecondDate.ToString("yyyy/MM/dd") == "9999/01/01" ? "" : SecondDate.ToString("yyyy/MM/dd") == "9999/01/31" ? "-1" : SecondDate.Year > 99 ? SecondDate.ToString("yyy/MM/dd HH:mm") : SecondDate.ToString("yy/MM/dd HH:mm");
            }
            else if (this.DateFormat == DateFormat.年月日時)
            {
                TextBox1.Text = FirstDate.ToString("yyyy/MM/dd") == "9999/01/01" ? "" : FirstDate.ToString("yyyy/MM/dd") == "9999/01/31" ? "-1" : FirstDate.Year > 99 ? FirstDate.ToString("yyy/MM/dd HH") : FirstDate.ToString("yy/MM/dd HH");
                TextBox2.Text = SecondDate.ToString("yyyy/MM/dd") == "9999/01/01" ? "" : SecondDate.ToString("yyyy/MM/dd") == "9999/01/31" ? "-1" : SecondDate.Year > 99 ? SecondDate.ToString("yyy/MM/dd HH") : SecondDate.ToString("yy/MM/dd HH");
            }
            TextBox1.ForeColor = this.TextForeColor;
            TextBox1.BackColor = this.TextBackColor;
            TextBox2.ForeColor = this.TextForeColor;
            TextBox2.BackColor = this.TextBackColor;
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

            Img2.ID = "CalendarImage2";
            Img2.Alt = "開啟日曆";
            Img2.Src = this.Page != null ? this.Page.ClientScript.GetWebResourceUrl(this.GetType(), "APTemplate.Resources.calendar.gif") : "";
            Img2.Border = 0;
            Img2.Width = 24;
            Img2.Height = 24;
            Img2.Alt = "開啟日曆";
            Img2.Disabled = !this.Enabled;
            Img2.Align = "top";
            Img2.Visible = this.IsShowButton;
            Img2.Attributes["style"] = "cursor:pointer";

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
            TableCell2.Controls.Add(_SeperatorLine);
            TableCell2.Controls.Add(TextBox2);
            TableCell2.Controls.Add(Img2);
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
            string strJS = "";
            string strJS1 = "";
            if (this.DateFormat == DateFormat.年月日)
            {
                strJS = "dateFormat='yyyy/mm/dd';chtpopUpCalendar(this, document.getElementById('" + TextBox1.ClientID + "'), dateFormat,-1,-1);";
                strJS1 = "dateFormat='yyyy/mm/dd';chtpopUpCalendar(this, document.getElementById('" + TextBox2.ClientID + "'), dateFormat,-1,-1);";
            }
            else if (this.DateFormat == DateFormat.年月)
            {
                strJS = "dateFormat='yyyy/mm';chtpopUpCalendar(this, document.getElementById('" + TextBox1.ClientID + "'), dateFormat,-1,-1);";
                strJS1 = "dateFormat='yyyy/mm';chtpopUpCalendar(this, document.getElementById('" + TextBox2.ClientID + "'), dateFormat,-1,-1);";
            }
            else if (this.DateFormat == DateFormat.年)
            {
                strJS = "dateFormat='yyyy';chtpopUpCalendar(this, document.getElementById('" + TextBox1.ClientID + "'), dateFormat,-1,-1);";
                strJS1 = "dateFormat='yyyy';chtpopUpCalendar(this, document.getElementById('" + TextBox2.ClientID + "'), dateFormat,-1,-1);";
            }
            else if (this.DateFormat == DateFormat.年月日時分秒)
            {
                strJS = "dateFormat='yyyy/mm/dd HH:mm:ss';chtpopUpCalendar(this, document.getElementById('" + TextBox1.ClientID + "'), dateFormat,-1,-1);";
                strJS1 = "dateFormat='yyyy/mm/dd HH:mm:ss';chtpopUpCalendar(this, document.getElementById('" + TextBox2.ClientID + "'), dateFormat,-1,-1);";
            }
            else if (this.DateFormat == DateFormat.年月日時分)
            {
                strJS = "dateFormat='yyyy/mm/dd HH:mm';chtpopUpCalendar(this, document.getElementById('" + TextBox1.ClientID + "'), dateFormat,-1,-1);";
                strJS1 = "dateFormat='yyyy/mm/dd HH:mm';chtpopUpCalendar(this, document.getElementById('" + TextBox2.ClientID + "'), dateFormat,-1,-1);";
            }
            else if (this.DateFormat == DateFormat.年月日時)
            {
                strJS = "dateFormat='yyyy/mm/dd HH';chtpopUpCalendar(this, document.getElementById('" + TextBox1.ClientID + "'), dateFormat,-1,-1);";
                strJS1 = "dateFormat='yyyy/mm/dd HH';chtpopUpCalendar(this, document.getElementById('" + TextBox2.ClientID + "'), dateFormat,-1,-1);";
            }
            if (IsShowButton)
            {
                Img1.Attributes.Add("onclick", strJS);
                Img2.Attributes.Add("onclick", strJS1);
                TextBox1.Attributes.Add("onclick", strJS);
                TextBox2.Attributes.Add("onclick", strJS1);
            }
            else
            {
                TextBox1.Attributes.Add("onclick", strJS);
                TextBox2.Attributes.Add("onclick", strJS1);
            }
            base.OnPreRender(e);
        }

        protected override void LoadControlState(object savedState)
        {
            base.LoadControlState(savedState);
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