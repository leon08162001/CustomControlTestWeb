using System;
using System.Configuration;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.Design;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Drawing;
using AjaxPro;
using System.Drawing.Design;
using System.Web.UI.Design.WebControls;

namespace APTemplate
{
    /// <summary>
    /// 自定義的下拉式日期選單控制項
    /// </summary>
    [ToolboxData("<{0}:DropDownList_Date runat=server></{0}:DropDownList_Date>")]
    [ToolboxBitmap(typeof(DropDownList))]
    public class DropDownList_Date : CompositeControl, INamingContainer
    {
        /// <summary>
        /// 儲存隱藏之日期資料控制項ID
        /// </summary>
        protected string _DateID = null;
        protected string _TitleAlign = "left";
        /// <summary>
        /// 建立DropDownList_Date控制項所需的table
        /// </summary>
        protected Table Table1 = new Table();
        /// <summary>
        /// 建立DropDownList_Date控制項所需的row
        /// </summary>
        protected TableRow TableRow = new TableRow();
        /// <summary>
        /// 建立DropDownList_Date控制項所需的cell
        /// </summary>
        protected TableCell TableCell1 = new TableCell();
        /// <summary>
        /// 建立DropDownList_Date控制項所需的cell
        /// </summary>
        protected TableCell TableCell2 = new TableCell();
        /// <summary>
        /// 建立DropDownList_Date控制項的第一個標籤
        /// </summary>
        protected Label Label1 = new Label();
        /// <summary>
        /// 建立DropDownList_Date控制項的年下拉式選單
        /// </summary>
        protected DropDownList DropDownList1 = new DropDownList();
        /// <summary>
        /// 建立DropDownList_Date控制項的第二個標籤
        /// </summary>
        protected Label Label2 = new Label();
        /// <summary>
        /// DropDownList_Date控制項的月下拉式選單
        /// </summary>
        protected DropDownList DropDownList2 = new DropDownList();
        /// <summary>
        /// 建立DropDownList_Date控制項的第三個標籤
        /// </summary>
        protected Label Label3 = new Label();
        /// <summary>
        /// DropDownList_Date控制項的日下拉式選單
        /// </summary>
        protected DropDownList DropDownList3 = new DropDownList();
        /// <summary>
        /// 儲存使用者選擇的日期資訊
        /// </summary>
        //protected HtmlInputHidden Text1 = new HtmlInputHidden();
        protected TextBox Text1 = new TextBox();
        /// <summary>
        /// 內部儲存UniqueID
        /// </summary>
        protected string _FirstTextUniqueID = "";
        /// <summary>
        /// 內部儲存ClientID
        /// </summary>
        protected string _FirstTextClientID = "";

        #region Public Properties & Methods

        /// <summary>
        /// 執行第一個年下拉選單或第二個月下拉選單所連動的第三個日期下拉選單內容。
        /// </summary>
        /// <param name="Year">第一個年下拉選單的選項值。</param>
        /// <param name="Month">第二個月下拉選單的選項值。</param>
        /// <returns>傳回string,代表第三個日期下拉選單內容。</returns>
        [AjaxPro.AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
        public string SetDaysInMonth(int Year, int Month)
        {
            string result = "";
            int DaysInMonth = DateTime.DaysInMonth(Year, Month);
            for (int i = 1; i <= DaysInMonth; i++)
            {
                result += i + ";";
            }
            if (result != "")
            {
                result = result.Substring(0, result.Length - 1);
            }
            return result;
        }

        /// <summary>
        /// 隱藏之日期資料控制項ID。
        /// </summary>
        [Category("自訂"),
         Description("隱藏之日期資料控制項ID。")]
        public string DateID
        {
            get
            {
                return _DateID;
            }
            set { _DateID = value; }
        }

        /// <summary>
        /// 指定的日期。
        /// </summary>
        [Category("自訂"),
         Description("指定的日期。")]
        public DateTime Date
        {
            get
            {
                if (Text1.Text == "")
                    return DateTime.Today;
                else if (Text1.Text.IndexOf("-1") >= 0)
                    return Convert.ToDateTime("9999/01/01");
                else
                    return Convert.ToDateTime(Text1.Text);
            }
            set { Text1.Text = value.ToString(); }
        }

        /// <summary>
        /// 設定第一個標籤的背景顏色。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("設定第一個標籤的背景顏色。")]
        [Editor(typeof(ColorEditor), typeof(UITypeEditor))]
        public Color TitleBackColor
        {
            get
            {
                if (ViewState["TitleBackColor"] == null)
                    return Color.Aqua;
                else
                    return (Color)ViewState["TitleBackColor"];
            }
            set { ViewState["TitleBackColor"] = value; }
        }

        /// <summary>
        /// 設定標籤文字顏色。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("設定標籤文字顏色。")]
        [Editor(typeof(ColorEditor), typeof(UITypeEditor))]
        public Color TitleForeColor
        {
            get
            {
                if (ViewState["TitleForeColor"] == null)
                    return Color.Sienna;
                else
                    return (Color)ViewState["TitleForeColor"];
            }
            set { ViewState["TitleForeColor"] = value; }
        }

        /// <summary>
        /// 設定第一個標籤寬度。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("設定第一個標籤寬度。")]
        public Unit TitleWidth
        {
            get
            {
                if (ViewState["TitleWidth"] == null)
                    return Label1.Width;
                else
                    return (Unit)ViewState["TitleWidth"];
            }
            set { ViewState["TitleWidth"] = value; }
        }

        /// <summary>
        /// 設定是否顯示標籤。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("設定是否顯示標籤。")]
        public bool IsShowTitle
        {
            get
            {
                if (ViewState["IsShowTitle"] == null)
                    return true;
                else
                    return (bool)ViewState["IsShowTitle"];
            }
            set { ViewState["IsShowTitle"] = value; }
        }

        /// <summary>
        /// 第一個下拉式選單標籤文字。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("第一個下拉式選單標籤文字。")]
        public string FirstTitle
        {
            get { return Label1.Text; }
            set { Label1.Text = value; }
        }

        /// <summary>
        /// 第二個下拉式選單標籤文字。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("第二個下拉式選單標籤文字。")]
        public string SecondTitle
        {
            get { return Label2.Text; }
            set { Label2.Text = value; }
        }

        /// <summary>
        /// 第三個下拉式選單標籤文字。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("第三個下拉式選單標籤文字。")]
        public string ThirdTitle
        {
            get { return Label3.Text; }
            set { Label3.Text = value; }
        }

        /// <summary>
        ///控制項是否加上邊框。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("控制項是否加上邊框。")]
        public virtual bool HasBorder
        {
            get
            {
                if (ViewState["HasBorder"] == null)
                    return true;
                else
                    return (bool)ViewState["HasBorder"];
            }
            set { ViewState["HasBorder"] = value; }
        }

        /// <summary>
        ///下拉式選單是否有初始文字。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("下拉式選單是否有初始文字。")]
        public virtual bool HasInitialText
        {
            get
            {
                if (ViewState["HasInitialText"] == null)
                    return false;
                else
                    return (bool)ViewState["HasInitialText"];
            }
            set { ViewState["HasInitialText"] = value; }
        }

        /// <summary>
        /// 下拉式選單的初始文字。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("下拉式選單的初始文字。")]
        public string InitialText
        {
            get
            {
                if (ViewState["InitialText"] == null)
                    return "請選擇";
                else
                    return (string)ViewState["InitialText"];
            }
            set { ViewState["InitialText"] = value; }
        }

        /// <summary>
        /// 以目前日期為基準往前或往後的間隔期間(單位:年)。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description(" 以目前日期為基準往前或往後的間隔期間(單位:年)。")]
        public int Duration
        {
            get
            {
                if (ViewState["Duration"] == null)
                    return 1;
                else
                    return (int)ViewState["Duration"];
            }
            set
            { ViewState["Duration"] = value; }
        }

        #endregion

        #region Protected Properties & Methods

        protected override void OnInit(EventArgs e)
        {
            Text1.ID = DateID == null || DateID == "" ? "Text1" : DateID;
            DropDownList1.ID = "DropDownList1";
            DropDownList2.ID = "DropDownList2";
            DropDownList3.ID = "DropDownList3";
            Page.RegisterRequiresControlState(this);
        }

        protected override void OnLoad(EventArgs e)
        {
            AjaxPro.Utility.RegisterTypeForAjax(typeof(APTemplate.DropDownList_Date));
            SetYearAndMonth();
            if (!Page.IsPostBack)
            {
                Text1.Text = this.HasInitialText ? "-1/-1/-1" : Date.ToString("yyyy/MM/dd");
            }
        }

        protected override void CreateChildControls()
        {
            EnsureChildControls();
            FirstTitle = FirstTitle == "" ? "西元年/月/日: " : FirstTitle;
            Label1.Text = FirstTitle;

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
            TableCell2.ForeColor = this.TitleForeColor;
            Label1.Width = this.TitleWidth;
            Label1.Visible = this.IsShowTitle;
            Label2.Visible = this.IsShowTitle;
            Label3.Visible = this.IsShowTitle;
            TableCell1.Visible = this.IsShowTitle;
            Label1.Font.Size = this.Font.Size.IsEmpty ? FontUnit.Small : this.Font.Size;
            Label2.Font.Size = this.Font.Size.IsEmpty ? FontUnit.Small : this.Font.Size;
            Label3.Font.Size = this.Font.Size.IsEmpty ? FontUnit.Small : this.Font.Size;
            Label1.Attributes["style"] += "text-align:" + _TitleAlign + ";";

            TableCell1.Controls.Add(Label1);
            TableCell2.Controls.Add(DropDownList1);
            TableCell2.Controls.Add(Label2);
            TableCell2.Controls.Add(DropDownList2);
            TableCell2.Controls.Add(Label3);
            TableCell2.Controls.Add(DropDownList3);
            TableCell2.Controls.Add(Text1);
            TableRow.Cells.Add(TableCell1);
            TableRow.Cells.Add(TableCell2);
            Table1.Rows.Add(TableRow);
            this.Controls.Add(Table1);
        }

        protected override void RenderContents(HtmlTextWriter writer)
        {
            Table1.RenderControl(writer);
        }

        protected override void OnPreRender(EventArgs e)
        {
          DropDownList1.Attributes.Add("onchange", "var dateVal=document.getElementById('" + DropDownList3.ClientID + "').value;SetDaysInMonthCallBack(document.getElementById('" + DropDownList1.ClientID + "'),document.getElementById('" + DropDownList2.ClientID + "'),document.getElementById('" + DropDownList3.ClientID + "'),dateVal);SetHiddenCalValue(document.getElementById('" + DropDownList1.ClientID + "'),document.getElementById('" + DropDownList2.ClientID + "'),document.getElementById('" + DropDownList3.ClientID + "'),document.getElementById('" + Text1.ClientID + "'));");
          DropDownList2.Attributes.Add("onchange", "var dateVal=document.getElementById('" + DropDownList3.ClientID + "').value;SetDaysInMonthCallBack(document.getElementById('" + DropDownList1.ClientID + "'),document.getElementById('" + DropDownList2.ClientID + "'),document.getElementById('" + DropDownList3.ClientID + "'),dateVal);SetHiddenCalValue(document.getElementById('" + DropDownList1.ClientID + "'),document.getElementById('" + DropDownList2.ClientID + "'),document.getElementById('" + DropDownList3.ClientID + "'),document.getElementById('" + Text1.ClientID + "'));");
            DropDownList3.Attributes.Add("onchange", "SetHiddenCalValue(document.getElementById('" + DropDownList1.ClientID + "'),document.getElementById('" + DropDownList2.ClientID + "'),document.getElementById('" + DropDownList3.ClientID + "'),document.getElementById('" + Text1.ClientID + "'));");
            RegisterJavaScript.RegisterContolIncludeScript(Page);
        }

        protected override void LoadControlState(object savedState)
        {
            object[] allStates = (object[])savedState;
            _FirstTextUniqueID = (string)allStates[0];
            _FirstTextClientID = (string)allStates[1];
            if (Context.Request.Form[_FirstTextUniqueID].IndexOf("-1") >= 0)
                Date = Convert.ToDateTime("9999/01/01");
            else
                Date = Context.Request.Form[_FirstTextUniqueID] != null && Context.Request.Form[_FirstTextUniqueID] != "" ? Convert.ToDateTime(Context.Request.Form[_FirstTextUniqueID]) : Date;
        }

        protected override object SaveControlState()
        {
            object[] allStates = new object[2];
            allStates[0] = Text1.UniqueID;
            allStates[1] = Text1.ClientID;
            return allStates;
        }

        #endregion

        #region Private Properties & Methods

        private void SetYearAndMonth()
        {
            //清空下拉式選單中的值
            DropDownList1.Items.Clear();
            DropDownList2.Items.Clear();
            DropDownList3.Items.Clear();

            ((IStateManager)DropDownList1.Items).TrackViewState();
            ((IStateManager)DropDownList2.Items).TrackViewState();
            ((IStateManager)DropDownList3.Items).TrackViewState();
            //設定年份
            if (this.Duration > 0)
            {
                for (int i = Duration; i >= -(Duration); i--)
                {
                    int Year = DateTime.Today.AddYears(i).Year;
                    ListItem item = new ListItem(Year.ToString());
                    DropDownList1.Items.Add(item);
                    if (i == 0)
                        DropDownList1.SelectedValue = Year.ToString();
                }

                //設定月份
                int Month = DateTime.Today.Month;
                for (int i = 1; i < 13; i++)
                {
                    DropDownList2.Items.Add(new ListItem(i.ToString()));
                    if (i == Month)
                        DropDownList2.SelectedValue = Month.ToString();
                }

                //設定日期
                int Day = DateTime.Today.Day;
                int DaysInMonth = DateTime.DaysInMonth(Date.Year, Date.Month);
                for (int i = 1; i <= DaysInMonth; i++)
                {
                    DropDownList3.Items.Add(new ListItem(i.ToString()));
                    if (i == Day)
                        DropDownList3.SelectedValue = Day.ToString();
                }
            }

            if (this.HasInitialText)
            {
                DropDownList1.Items.Insert(0, new ListItem(this.InitialText, "-1"));
                DropDownList2.Items.Insert(0, new ListItem(this.InitialText, "-1"));
                DropDownList3.Items.Insert(0, new ListItem(this.InitialText, "-1"));
                if (!Page.IsPostBack)
                {
                    DropDownList1.SelectedIndex = 0;
                    DropDownList2.SelectedIndex = 0;
                    DropDownList3.SelectedIndex = 0;
                }
            }

            if (Page.IsPostBack)
            {
                //將年月日下拉選單選項設為使用者所選日期
                DropDownList1.SelectedValue = Date.Year.ToString() == "9999" ? "-1" : Date.Year.ToString();
                DropDownList2.SelectedValue = Date.Year.ToString() == "9999" ? "-1" : Date.Month.ToString();
                DropDownList3.SelectedValue = Date.Year.ToString() == "9999" ? "-1" : Date.Day.ToString();
            }
        }

        #endregion

    }

    //public class DropDownList_DateDesigner : CompositeControlDesigner
    //{
    //  protected override string  GetErrorDesignTimeHtml(Exception e)
    //  {
    //    return "<div>" + e.Message + "</div>";
    //  }
    //}
}