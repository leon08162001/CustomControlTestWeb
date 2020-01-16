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
using System.Drawing.Design;
using System.Web.UI.Design.WebControls;

namespace APTemplate
{
    /// <summary>
    /// 列舉-指示當月最大或最小日期
    /// </summary>
    public enum MaxOrMinDate
    {
        MaxDate = 1,
        MinDate = 2
    };
    /// <summary>
    /// 自定義的下拉式日期選單控制項
    /// </summary>
    [ToolboxData("<{0}:DropDownList_YearMonth runat=server></{0}:DropDownList_YearMonth>")]
    [ToolboxBitmap(typeof(DropDownList))]
    public class DropDownList_YearMonth : ValidationBase, INamingContainer
    {
        /// <summary>
        /// 儲存隱藏之日期資料控制項ID
        /// </summary>
        protected string _DateID = null;
        protected string _TitleAlign = "left";
        protected PlaceHolder PlaceHolder1 = new PlaceHolder();
        CustomValidator valid = null;
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
        /// DropDownList_Date控制項的月下拉式選單
        /// </summary>
        protected DropDownList DropDownList2 = new DropDownList();
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
        /// <summary>
        ///指示當月最大或最小日期
        /// </summary>
        protected MaxOrMinDate _MaxOrMinDate = MaxOrMinDate.MinDate;

        #region Public Properties & Methods

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
                    return this.MaxOrMinDate == MaxOrMinDate.MinDate ? Convert.ToDateTime(DateTime.Today.ToString("yyyy/MM")) : Convert.ToDateTime(DateTime.Today.ToString("yyyy/MM")).AddMonths(1).AddDays(-1);
                else if (Text1.Text.IndexOf("-1") >= 0)
                    return this.MaxOrMinDate == MaxOrMinDate.MinDate ? Convert.ToDateTime("9999/01/01") : Convert.ToDateTime("9999/01/31");
                else
                    return this.MaxOrMinDate == MaxOrMinDate.MinDate ? Convert.ToDateTime(Text1.Text) : Convert.ToDateTime(Text1.Text).AddMonths(1).AddDays(-1);

            }
            set { Text1.Text = value.ToString("yyyy/MM"); }
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

        
        /// <summary>
        /// 以目前日期為基準往前或往後的間隔期間(單位:年)。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description(" 以目前日期為基準往前或往後的間隔期間(單位:年)。")]
        public MaxOrMinDate MaxOrMinDate
        {
            get
            {
                return _MaxOrMinDate;
            }
            set
            {
                _MaxOrMinDate = value;
            }
        }

        #endregion

        #region Protected Properties & Methods

        protected override void OnInit(EventArgs e)
        {
            Text1.ID = DateID == null || DateID == "" ? "Text1" : DateID;
            DropDownList1.ID = "DropDownList1";
            DropDownList2.ID = "DropDownList2";
            Page.RegisterRequiresControlState(this);
        }

        protected override void OnLoad(EventArgs e)
        {
            valid = new CustomValidator();
            valid.ControlToValidate = Text1.ID;
            valid.IsValid = true;
            valid.Display = ValidatorDisplay.Dynamic;
            valid.ClientValidationFunction = "Date_ClientValidate";
            valid.Font.Size = FontUnit.Small;
            valid.ErrorMessage = "=>格式不正確!";
            valid.ValidationGroup = this.ValidationGroup;
            PlaceHolder1.Controls.Add(valid);

            SetValidationType(base.ValidationType, new BaseValidator[] { valid }, PlaceHolder1);
            SetYearAndMonth();
            if (!Page.IsPostBack)
            {
                Text1.Text = Date.ToString("yyyy/MM");
            }
        }

        protected override void CreateChildControls()
        {
            EnsureChildControls();
            FirstTitle = FirstTitle == "" ? "西元年/月: " : FirstTitle;
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
            TableCell1.Visible = this.IsShowTitle;
            Label1.Font.Size = this.Font.Size.IsEmpty ? FontUnit.Small : this.Font.Size;
            Label1.Attributes["style"] += "text-align:" + _TitleAlign + ";";
            Text1.Width = Unit.Pixel(0);
            Text1.Height = Unit.Pixel(0);
            Text1.BorderColor = Color.Transparent;
            TableCell1.Controls.Add(Label1);
            TableCell2.Controls.Clear();
            TableCell2.Controls.Add(DropDownList1);
            TableCell2.Controls.Add(new LiteralControl("<span style='font-size:small'>年</span>"));
            TableCell2.Controls.Add(DropDownList2);
            TableCell2.Controls.Add(new LiteralControl("<span style='font-size:small'>月</span>"));
            TableCell2.Controls.Add(PlaceHolder1);
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
            //if (!Page.IsPostBack)
            //{
            //    SetYearAndMonth();
            //    Text1.Text = this.HasInitialText ? "-1/-1" : Date.ToString("yyyy/MM/dd");
            //}
            string JS = "Javascript:document.getElementById('" + Text1.ClientID + "').value=document.getElementById('" + DropDownList1.ClientID +
                                    "').value + '/' + document.getElementById('" + DropDownList2.ClientID + "').value;DateValidate(document.getElementById('" + Text1.ClientID + "'))";
            string script = "<script language=javascript> \n" +
                 "\t function Date_ClientValidate(source, arguments) { \n" +
                 "\t var val = arguments.Value; \n" +
                 "\t if(val.split('-').length-1==1){ \n" +
                 "\t arguments.IsValid=false;} \n" +
                 "\t else{ \n" +
                 "\t arguments.IsValid=true;} \n" +
                 "} \n" +
                 "</script>";
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Date_ClientValidate", script);
            DropDownList1.Attributes.Add("onchange", JS);
            DropDownList2.Attributes.Add("onchange", JS);
            RegisterJavaScript.RegisterContolIncludeScript(Page);
        }

        protected override void LoadControlState(object savedState)
        {
            object[] allStates = (object[])savedState;
            _FirstTextUniqueID = (string)allStates[0];
            _FirstTextClientID = (string)allStates[1];
            if (Context.Request.Form[_FirstTextUniqueID].IndexOf("-1") >= 0)
                Date = Convert.ToDateTime(this.MaxOrMinDate == MaxOrMinDate.MinDate ? "9999/01/01" : "9999/01/31");
            else
            {
                if (Context.Request.Form[_FirstTextUniqueID] != null && Context.Request.Form[_FirstTextUniqueID] != "")
                {
                    if (this.MaxOrMinDate == MaxOrMinDate.MinDate)
                    {
                        Date = Convert.ToDateTime(Context.Request.Form[_FirstTextUniqueID] + "/01");
                    }
                    else if (this.MaxOrMinDate == MaxOrMinDate.MaxDate)
                    {
                        Date = Convert.ToDateTime(Context.Request.Form[_FirstTextUniqueID] + "/01").AddMonths(1).AddDays(-1);
                    }
                }
                else
                {
                    Date = Date;
                }
            }
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

            ((IStateManager)DropDownList1.Items).TrackViewState();
            ((IStateManager)DropDownList2.Items).TrackViewState();
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
                int Month = Date.Month;
                for (int i = 1; i < 13; i++)
                {
                    DropDownList2.Items.Add(new ListItem(i.ToString()));
                    if (i == Month)
                        DropDownList2.SelectedValue = Month.ToString();
                }
            }

            if (this.HasInitialText)
            {
                DropDownList1.Items.Insert(0, new ListItem(this.InitialText, "-1"));
                DropDownList2.Items.Insert(0, new ListItem(this.InitialText, "-1"));
                if (!Page.IsPostBack)
                {
                    DropDownList1.SelectedIndex = 0;
                    DropDownList2.SelectedIndex = 0;
                }
            }
            if (Page.IsPostBack)
            {
                //將年月下拉選單選項設為使用者所選日期
                DropDownList1.SelectedValue = Date.Year.ToString() == "9999" ? "-1" : Date.Year.ToString();
                DropDownList2.SelectedValue = Date.Year.ToString() == "9999" ? "-1" : Date.Month.ToString();
            }
        }

        #endregion

    }
}