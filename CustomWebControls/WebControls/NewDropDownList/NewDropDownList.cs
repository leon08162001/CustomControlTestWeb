using System;
using System.Configuration;
using System.ComponentModel;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Drawing.Design;
using System.ComponentModel.Design;

namespace APTemplate
{
    /// <summary>
    /// 自定義下拉式選單控制項。
    /// </summary>
    /// <remarks>
    /// <list type="bullet">
    /// <item><description>自定義下拉式選單控制項。</description></item>
    /// </list>
    /// </remarks>
    [ToolboxData("<{0}:NewDropDownList runat=server></{0}:NewDropDownList>")]
    [ToolboxBitmap(typeof(DropDownList))]
    [ValidationProperty("SelectedValue")]
    public class NewDropDownList : ValidationBase
    {
        /// <summary>
        /// 儲存下拉式選單中是否有初始項目
        /// </summary>
        private bool _HasInitialItem = false;
        /// <summary>
        /// 儲存第一個下拉式選單的控制項ID
        /// </summary>
        protected string _DropDownListID = null;
        protected string _TitleAlign = "left";
        /// <summary>
        /// 儲存第一個下拉式選單中的初始項目值
        /// </summary>
        private string _InitialValue;
        /// <summary>
        /// 儲存第一個下拉式選單中的初始項目文字
        /// </summary>
        private string _InitialText = "--請選擇一個值--";
        private string SelectedVal1;
        /// <summary>
        /// 建立DropDownList_Multiple控制項所需的table
        /// </summary>
        protected Table Table1 = new Table();
        /// <summary>
        /// 建立DropDownList_Multiple控制項所需的row
        /// </summary>
        protected TableRow TableRow = new TableRow();
        /// <summary>
        /// 建立DropDownList_Multiple控制項所需的cell
        /// </summary>
        protected TableCell TableCell1 = new TableCell();
        /// <summary>
        /// 建立DropDownList_Multiple控制項所需的cell
        /// </summary>
        protected TableCell TableCell2 = new TableCell();
        /// <summary>
        /// 建立DropDownList_Multiple控制項的第一個標籤
        /// </summary>
        protected Label Label1 = new Label();
        /// <summary>
        /// 建立DropDownList_Multiple控制項的第一個下拉式選單
        /// </summary>
        protected DropDownList DropDownList1 = new DropDownList();
        protected PlaceHolder PlaceHolder1 = new PlaceHolder();
        /// <summary>
        /// 內部儲存第一個下拉式選單的UniqueID
        /// </summary>
        protected string _SelectedUniqueID = "";
        /// <summary>
        /// 內部儲存第一個下拉式選單的ClientID
        /// </summary>
        protected string _SelectedClientID = "";

        protected List<ListItem> _Items = new List<ListItem>();

        //private Color _TitleBackColor;

        #region Public Properties & Methods

        /// <summary>
        /// 下拉式選單的控制項ID。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("小數位數的長度。")]
        public string DropDownListID
        {
            get { return _DropDownListID; }
            set { _DropDownListID = value; }
        }

        /// <summary>
        /// 此自訂控制項在網頁上是否可見。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("此自訂控制項在網頁上是否可見。")]
        public new bool Visible
        {
            get { return Table1.Visible; }
            set { Table1.Visible = value; }
        }

        /// <summary>
        /// 此自訂控制項在網頁上是否啟用。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("此自訂控制項在網頁上是否啟用。")]
        public new bool Enabled
        {
            get { return Table1.Enabled; }
            set { Table1.Enabled = value; }
        }

        /// <summary>
        /// 下拉式選單中是否有初始項目。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("下拉式選單中是否有初始項目。")]
        public bool HasInitialItem
        {
            get { return _HasInitialItem; }
            set { _HasInitialItem = value; }
        }

        /// <summary>
        /// 下拉式選單中的初始項目值。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("下拉式選單中的初始項目值。")]
        public string InitialValue
        {
            get
            {
                _InitialValue = HasInitialItem == true ? "-1" : null;
                return _InitialValue;
            }
        }

        /// <summary>
        /// 下拉式選單中的初始項目文字。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("下拉式選單中的初始項目文字。")]
        public string InitialText
        {
            get { return _InitialText; }
            set { _InitialText = value; }
        }

        /// <summary>
        /// 設定標籤的背景顏色。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("設定標籤的背景顏色。")]
        [Editor(typeof(ColorEditor), typeof(UITypeEditor))]
        public Color TitleBackColor
        {
            get
            {
                if (ViewState["TitleBackColor"] == null)
                //if (_TitleBackColor == null)
                    return Color.Aqua;
                else
                    return (Color)ViewState["TitleBackColor"];
                    //return _TitleBackColor;
            }
            set 
            { 
                ViewState["TitleBackColor"] = value;
                //_TitleBackColor = value;
            }
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
        /// 設定標籤寬度。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("設定標籤寬度。")]
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
        /// 設定標籤對齊方式。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("設定標籤對齊方式。")]
        public virtual Align TitleAlign
        {
            get
            {
                if (ViewState["TitleAlign"] == null)
                    return Align.left;
                else
                    return (Align)ViewState["TitleAlign"];
            }
            set
            {
                ViewState["TitleAlign"] = value;
                _TitleAlign = (int)value == 1 ? "left" : (int)value == 2 ? "right" : (int)value == 3 ? "center" : "justify";
            }
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
        /// 下拉式選單標籤文字。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("下拉式選單標籤文字。")]
        public string Title
        {
            get { return Label1.Text; }
            set { Label1.Text = value; }
        }

        /// <summary>
        /// 下拉式選單的選擇項目值。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("下拉式選單的選擇項目值。")]
        public string SelectedValue
        {
            get { return DropDownList1.SelectedValue; }
            set { DropDownList1.SelectedValue = value; }
        }

        /// <summary>
        /// 下拉式選單的選擇項目索引。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("下拉式選單的選擇項目索引。")]
        public int SelectedIndex
        {
            get { return DropDownList1.SelectedIndex; }
            set { DropDownList1.SelectedIndex = value; }
        }

        /// <summary>
        /// 下拉式選單的選擇項目文字。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("下拉式選單的選擇項目文字。")]
        public string SelectedText
        {
            get
            {
                if (DropDownList1.SelectedItem == null)
                    return null;
                else
                    return DropDownList1.SelectedItem.Text;
            }
            set { DropDownList1.SelectedItem.Text = value; }
        }

        /// <summary>
        ///取得內部DropDownList控制項ClientID。
        /// </summary>
        [Browsable(false), ReadOnly(true)]
        public virtual string DropDownListClientID
        {
            get { return this.ClientID + "_" + DropDownList1.ClientID; }
        }

        /// <summary>
        /// 下拉式選單的所有項目。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("下拉式選單的所有項目。")]
        [PersistenceMode(PersistenceMode.InnerProperty), DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [NotifyParentProperty(true)]
        public List<ListItem> Items
        {
            get { return _Items; }
        }

        /// <summary>
        /// 下拉式選單的資料來源。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("下拉式選單的資料來源。")]
        public object DataSource
        {
            get
            {
                EnsureChildControls();
                return DropDownList1.DataSource;
            }
            set
            {
                EnsureChildControls();
                DropDownList1.DataSource = value;
            }
        }

        /// <summary>
        /// 用來作為繫結對象的資料表或檢視表。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("用來作為繫結對象的資料表或檢視表。")]
        public string DataMember
        {
            get
            {
                EnsureChildControls();
                return DropDownList1.DataMember;
            }
            set
            {
                EnsureChildControls();
                DropDownList1.DataMember = value;
            }
        }

        /// <summary>
        /// 在資料來源中提供項目文字的欄位。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("在資料來源中提供項目文字的欄位。")]
        public string DataTextField
        {
            get
            {
                EnsureChildControls();
                return DropDownList1.DataTextField;
            }
            set
            {
                EnsureChildControls();
                DropDownList1.DataTextField = value;
            }
        }

        /// <summary>
        /// 在資料來源中提供項目值的欄位。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("在資料來源中提供項目值的欄位。")]
        public string DataValueField
        {
            get
            {
                EnsureChildControls();
                return DropDownList1.DataValueField;
            }
            set
            {
                EnsureChildControls();
                DropDownList1.DataValueField = value;
            }
        }

        /// <summary>
        /// 套用至文字欄位的格式。例如"{0:d}"
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("套用至文字欄位的格式。例如{0:d}")]
        public string DataTextFormatString
        {
            get
            {
                EnsureChildControls();
                return DropDownList1.DataTextFormatString;
            }
            set
            {
                EnsureChildControls();
                DropDownList1.DataTextFormatString = value;
            }
        }

        #endregion

        #region Protected Properties & Methods

        protected override void OnInit(EventArgs e)
        {
            DropDownList1.ID = DropDownListID == null || DropDownListID == "" ? "DropDownList1" : DropDownListID;
            Page.RegisterRequiresControlState(this);
        }

        protected override void OnLoad(EventArgs e)
        {
            CompareValidator CV1 = null;
            if (NeedValidation)
            {
                CV1 = new CompareValidator();
                CV1.ControlToValidate = DropDownList1.ID;
                CV1.Font.Size = FontUnit.Small;
                CV1.Display = ValidatorDisplay.Dynamic;
                CV1.ValueToCompare = InitialValue;
                CV1.ErrorMessage = "請選擇第一個下拉選單值!";
                CV1.Type = ValidationDataType.String;
                CV1.Operator = ValidationCompareOperator.NotEqual;
                PlaceHolder1.Controls.Add(CV1);
            }

            SetValidationType(base.ValidationType, new BaseValidator[] { CV1 }, PlaceHolder1);
            if (HasInitialItem == true)
            {
                DropDownList1.AppendDataBoundItems = true;
                DropDownList1.Items.Add(new ListItem(InitialText, InitialValue));
            }
            DropDownList1.SelectedValue = SelectedVal1;
        }

        protected override void OnPreRender(EventArgs e)
        {
            RegisterJavaScript.RegisterContolIncludeScript(Page);
            base.OnPreRender(e);
        }

        protected override void CreateChildControls()
        {
            EnsureChildControls();
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
            Label1.Font.Size = this.Font.Size;
            Label1.Attributes["style"] += "text-align:" + _TitleAlign + ";";

            CreateItems();

            TableCell1.Controls.Add(Label1);
            TableCell2.Controls.Add(DropDownList1);
            TableCell2.Controls.Add(PlaceHolder1);
            TableRow.Cells.Add(TableCell1);
            TableRow.Cells.Add(TableCell2);
            Table1.Rows.Add(TableRow);
            this.Controls.Add(Table1);
        }

        protected override void RenderContents(HtmlTextWriter writer)
        {
            Table1.RenderControl(writer);
        }

        protected override void LoadControlState(object savedState)
        {
            object[] allStates = (object[])savedState;
            _SelectedUniqueID = (string)allStates[0];
            _SelectedClientID = (string)allStates[1];
            SelectedVal1 = Context.Request.Form[_SelectedUniqueID] != null ? Context.Request.Form[_SelectedUniqueID].ToString() : null;
        }

        protected override object SaveControlState()
        {
            object[] allStates = new object[2];
            allStates[0] = DropDownList1.UniqueID;
            allStates[1] = DropDownList1.ClientID;
            return allStates;
        }

        #endregion

        #region Private Properties & Methods

        private void ChildControlsSetting()
        {
            //設定Table1
            Table1.ID = "Table1";

            //設定Label1
            Label1.ID = "Label1";

            //DropDownList1
            DropDownList1.ID = "DropDownList1";
            DropDownList1.EnableViewState = true;
        }

        private void CreateItems()
        {
            DropDownList1.Items.Clear();
            if (Items.Count > 0)
            {
                foreach (ListItem Item in Items)
                {
                    DropDownList1.Items.Add(Item);
                }
            }
        }

        #endregion

    }
}