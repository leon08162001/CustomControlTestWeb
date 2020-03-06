using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Reflection;
using System.Runtime.Caching;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace APTemplate
{
    /// <summary>
    /// 列舉-位置類型
    /// </summary>
    public enum Position
    {
        /// <summary>
        /// 置左對齊
        /// </summary>
        left = 1,
        /// <summary>
        /// 置右對齊
        /// </summary>
        right = 2,
        /// <summary>
        /// 置中對齊
        /// </summary>
        center = 3,
        /// <summary>
        /// 均分對齊
        /// </summary>
        justify = 4
    };

    /// <summary>
    /// 列舉-分頁大小
    /// </summary>
    public enum PageSizeOption
    {
        _10 = 10,
        _15 = 15,
        _25 = 25,
        _50 = 50
    };

    /// <summary>
    /// 自定義的分頁控制項
    /// </summary>
    [Serializable]
    [ToolboxData("<{0}:PageMaker runat=server></{0}:PageMaker>")]
    [ToolboxBitmap(typeof(Repeater))]
    [DefaultEvent("CustomPageSizeCustom")]
    public class PageMaker : CompositeControl
    {
        protected Control _PagedControl = null;
        protected string _position = "left";
        protected string _display = "inline-block";
        private Object mDataSource = null;
        private DataTable _BindTable = null;
        private DataTable SourceTable = null;
        private int PageCount = 0;
        private int _PageSize = 10;
        private int _DefaultPageSize = 0;
        private int _PageIndex = 0;
        private string _PagedControlID;
        private Position _Align;
        private bool IsTriggeredFlag = false;
        private string _SyncPageMakerID = "";

        protected Label PageInfo = new Label();
        protected ImageButton ImageButtonFirstPage = new ImageButton();
        protected ImageButton ImageButtonPrePage = new ImageButton();
        protected ImageButton ImageButtonNextPage = new ImageButton();
        protected ImageButton ImageButtonLastPage = new ImageButton();
        protected Label Label1 = new Label();
        protected TextBox PageTo = new TextBox();
        protected Label Label2 = new Label();
        protected System.Web.UI.WebControls.Image SeparatorImg = new System.Web.UI.WebControls.Image();
        protected DropDownList DropDownListRecord = new DropDownList();
        protected System.Web.UI.WebControls.Image RowsDisplayImg = new System.Web.UI.WebControls.Image();
        protected Boolean _HasCustomPageSizeEvent = false;

        // Delegate  
        public delegate void PageSizeEventHandler(object sender, PageSizeCustomEventArgs e);
        /// <summary>
        /// 自訂分頁時的事件
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("自訂分頁時的事件。")]
        public event PageSizeEventHandler CustomPageSize;

        #region Public Properties & Methods

        /// <summary>
        /// 一個分頁呈現的資料筆數。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("一個分頁呈現的資料筆數。")]
        public int PageSize
        {
            get
            {
                return _PageSize;
            }
            set
            {
                if (value > 0)
                {
                    _PageSize = value;
                }
            }
        }

        /// <summary>
        /// 目前所在的頁索引位置。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("目前所在的頁索引位置。")]
        public int PageIndex
        {
            get
            {
                return _PageIndex;
            }
            set
            {
                _PageIndex = value;
            }
        }

        /// <summary>
        /// 要分頁的控制項如gridview,listview,datalist等控制項。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("要分頁的控制項如gridview,listview,datalist等控制項。")]
        public string PagedControlID
        {
            get { return _PagedControlID; }
            set { _PagedControlID = value; }
        }

        /// <summary>
        /// 分頁控制項的對齊方式。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("分頁控制項的對齊方式。")]
        public Position Align
        {
            get
            {
                if (_Align == null)
                { _Align = Position.left; }
                return _Align;
            }
            set
            {
                _Align = value;
                _position = value == Position.left ? "left" : value == Position.right ? "right" : value == Position.center ? "center" : "justify";
            }
        }

        /// <summary>
        /// 操作換頁時是否同步其他分頁控制項。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
        TypeConverterAttribute(typeof(PageMakerConverter)),
         Description("操作換頁時是否同步其他分頁控制項。")]
        public string SyncPageMakerID
        {
            get { return _SyncPageMakerID; }
            set { _SyncPageMakerID = value; }
        }

        /// <summary>
        /// 重新執行分頁並跳至第一頁。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("重新執行分頁並跳至第一頁。")]
        public void RePaging()
        {
            PageIndex = 0;
            Paging();
            if (PageIndex + 1 == 1 && PageCount == 1)
            {
                ImageButtonNextPage.ImageUrl = this.Page != null ? this.Page.ClientScript.GetWebResourceUrl(this.GetType(), "APTemplate.Resources.nextPageDisabled.gif") : "";
                ImageButtonNextPage.Enabled = false;
                ImageButtonLastPage.ImageUrl = this.Page != null ? this.Page.ClientScript.GetWebResourceUrl(this.GetType(), "APTemplate.Resources.lastPageDisabled.gif") : "";
                ImageButtonLastPage.Enabled = false;
            }
        }

        /// <summary>
        /// 重新執行分頁並跳至指定參數頁。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("重新執行分頁並跳至指定參數頁。")]
        public void RePaging(Int32 PageIdx)
        {
            PageIndex = PageIdx;
            Paging();
            if (PageIndex + 1 == 1 && PageCount == 1)
            {
                ImageButtonNextPage.ImageUrl = this.Page != null ? this.Page.ClientScript.GetWebResourceUrl(this.GetType(), "APTemplate.Resources.nextPageDisabled.gif") : "";
                ImageButtonNextPage.Enabled = false;
                ImageButtonLastPage.ImageUrl = this.Page != null ? this.Page.ClientScript.GetWebResourceUrl(this.GetType(), "APTemplate.Resources.lastPageDisabled.gif") : "";
                ImageButtonLastPage.Enabled = false;
            }
        }

        #endregion

        #region Protected Properties & Methods

        protected override void OnInit(EventArgs e)
        {
            try
            {
                _PagedControl = Parent.FindControl(this.PagedControlID);
                Page.RegisterRequiresControlState(this);
                if (!Page.IsPostBack)
                {
                    if (!this.DesignMode)
                    {
                        MemoryCache.Default.Remove(Context.Session.SessionID + "DataSource");
                    }
                    DefaultPageSize = PageSize;
                    PageSize = DefaultPageSize;
                }
            }
            catch (Exception ex)
            {
                ShowMsg();
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            //this.DataSource = _PagedControl.GetType().GetProperty("DataSource").GetValue(_PagedControl, null);
            if (!Page.IsPostBack)
            {
                PageIndex = 0;
                Paging();
            }
            else
            {
                if (Context.Request.Params["__EVENTTARGET"] != null && !Context.Request.Params["__EVENTTARGET"].ToString().Equals(""))
                {
                    string eventObj = Context.Request.Params["__EVENTTARGET"].ToString();
                    if (eventObj.IndexOf("PageTo") != -1)
                    {
                        GetPageCount();
                        Paging();
                    }
                }
            }
        }

        protected override void CreateChildControls()
        {
            EnsureChildControls();
            ChildControlsSetting();
            this.Controls.Add(PageInfo);
            this.Controls.Add(ImageButtonFirstPage);
            this.Controls.Add(ImageButtonPrePage);
            this.Controls.Add(ImageButtonNextPage);
            this.Controls.Add(ImageButtonLastPage);
            this.Controls.Add(Label1);
            this.Controls.Add(PageTo);
            this.Controls.Add(Label2);
            this.Controls.Add(SeparatorImg);
            this.Controls.Add(DropDownListRecord);
            this.Controls.Add(RowsDisplayImg);
            if (_PagedControl is GridView || _PagedControl is ListView || _PagedControl is DataList || _PagedControl is Repeater)
            {
                SeparatorImg.Visible = true;
                DropDownListRecord.Visible = true;
                RowsDisplayImg.Visible = true;
            }
            else if (_PagedControl is FormView || _PagedControl is DetailsView)
            {
                _PageSize = 1;
                SeparatorImg.Visible = false;
                DropDownListRecord.Visible = false;
                RowsDisplayImg.Visible = false;
            }
            else
            {
                this.Visible = false;
            }
        }

        protected override void OnDataBinding(EventArgs e)
        {
            int result;
            this.Context.Items["DataSource"] = this.Context.Items["DataSource"] == null ? _PagedControl.GetType().GetProperty("DataSource").GetValue(_PagedControl,null) : this.Context.Items["DataSource"];
            this.DataSource = this.Context.Items["DataSource"];
            PageCount = 0;
            //this.PageIndex = 0;
            PageCount = GetPageCount();
            RePaging(this.PageIndex);
            PageMaker PM = GetSyncPageMaker();
            if (PM != null)
            {
                PM.RePaging(PageIndex);
            }
            base.OnDataBinding(e);
        }

        protected override void RenderContents(HtmlTextWriter writer)
        {
            string HtmlCode = @"<div style='text-align:" + _position + "; display:" + _display + "'>" +
                                "<table class='toolbar' cellspacing='1' cellpadding='0' border='0' id='table3'>" +
                            "<tr>" +
                                                "<td style='border-style:none;'>";
            writer.Write(HtmlCode);
            PageInfo.RenderControl(writer);
            HtmlCode = @"</td>" +
                                 "<td style='border-style:none;'>" +
                         "&nbsp;" +
                       "</td>" +
                                 "<td style='border-style:none;'>";
            writer.Write(HtmlCode);
            ImageButtonFirstPage.RenderControl(writer);
            HtmlCode = @"</td>" +
                                 "<td style='border-style:none;'>";
            writer.Write(HtmlCode);
            ImageButtonPrePage.RenderControl(writer);
            HtmlCode = @"</td>" +
                                 "<td style='border-style:none;'>";
            writer.Write(HtmlCode);
            ImageButtonNextPage.RenderControl(writer);
            HtmlCode = @"</td>" +
                                 "<td style='border-style:none;'>";
            ImageButtonLastPage.RenderControl(writer);
            HtmlCode = @"</td>" +
                                 "<td style='width: 70px;border-style:none;'>";
            writer.Write(HtmlCode);
            Label1.RenderControl(writer);
            PageTo.RenderControl(writer);
            Label2.RenderControl(writer);
            HtmlCode = @"</td>" +
                                       "<td style='border-style:none;'>";
            writer.Write(HtmlCode);
            SeparatorImg.RenderControl(writer);
            HtmlCode = "</td>" +
                             "<td style='border-style:none;'>";
            writer.Write(HtmlCode);
            DropDownListRecord.SelectedValue = Convert.ToString((int)PageSize);
            DropDownListRecord.RenderControl(writer);
            RowsDisplayImg.RenderControl(writer);
            HtmlCode = @"</td>" +
                           "</tr>" +
                         "</table>" +
                       "</div>";
            writer.Write(HtmlCode);
        }

        /// <summary>
        /// 跳至第一頁。
        /// </summary>
        protected void ImageButtonFirstPage_Click(object sender, ImageClickEventArgs e)
        {
            PageMaker PM = GetSyncPageMaker();
            PageSize = (int)Convert.ToInt32(DropDownListRecord.SelectedValue);
            PageCount = PageCount == 0 ? GetPageCount() : PageCount;
            PageIndex = 0;
            Paging();
            if (PM != null)
            {
                PM.RePaging(PageIndex);
            }
            IsTriggeredFlag = true;
        }

        /// <summary>
        /// 跳至上一頁。
        /// </summary>
        protected void ImageButtonPrePage_Click(object sender, ImageClickEventArgs e)
        {
            PageMaker PM = GetSyncPageMaker();
            PageSize = (int)Convert.ToInt32(DropDownListRecord.SelectedValue);
            PageCount = PageCount == 0 ? GetPageCount() : PageCount;
            PageIndex -= 1;
            Paging();
            if (PM != null)
            {
                PM.RePaging(PageIndex);
            }
            IsTriggeredFlag = true;
        }

        /// <summary>
        /// 跳至下一頁。
        /// </summary>
        protected void ImageButtonNextPage_Click(object sender, ImageClickEventArgs e)
        {
            PageMaker PM = GetSyncPageMaker();
            PageSize = (int)Convert.ToInt32(DropDownListRecord.SelectedValue);
            PageCount = PageCount == 0 ? GetPageCount() : PageCount;
            PageIndex += 1;
            Paging();
            if (PM != null)
            {
                PM.RePaging(PageIndex);
            }
            IsTriggeredFlag = true;
        }

        /// <summary>
        /// 跳至最後一頁。
        /// </summary>
        protected void ImageButtonLastPage_Click(object sender, ImageClickEventArgs e)
        {
            PageMaker PM = GetSyncPageMaker();
            PageSize = (int)Convert.ToInt32(DropDownListRecord.SelectedValue);
            PageCount = PageCount == 0 ? GetPageCount() : PageCount;
            PageIndex = PageCount > 0 ? PageCount - 1 : 0;
            Paging();
            if (PM != null)
            {
                PM.RePaging(PageIndex);
            }
            IsTriggeredFlag = true;
        }

        /// <summary>
        /// 跳至指定頁。
        /// </summary>
        protected void PageTo_TextChanged(object sender, EventArgs e)
        {
            int result;
            PageMaker PM = GetSyncPageMaker();
            PageSize = (int)Convert.ToInt32(DropDownListRecord.SelectedValue);
            PageCount = PageCount == 0 ? GetPageCount() : PageCount;
            PageIndex = PageTo.Text != "" && Int32.TryParse(PageTo.Text, out result) && result > 0 && result <= PageCount ? Int32.Parse(PageTo.Text) - 1 : PageIndex;
            Paging();
            if (PM != null)
            {
                PM.PageTo.Text = (PageIndex + 1).ToString();
                PM.RePaging(PageIndex);
            }
            IsTriggeredFlag = true;
        }

        /// <summary>
        /// 選擇分頁的筆數。
        /// </summary>
        protected void DropDownListRecord_SelectedIndexChanged(object sender, EventArgs e)
        {
            PageMaker PM = GetSyncPageMaker();
            PageSize = (int)Convert.ToInt32(DropDownListRecord.SelectedValue);
            PageCount = PageCount == 0 ? GetPageCount() : PageCount;
            PageTo.Text = "1";
            PageIndex = 0;
            Paging();
            if (PM != null)
            {
                PM.PageSize = PageSize;
                PM.DropDownListRecord.SelectedValue = PageSize.ToString();
                PM.RePaging(PageIndex);
            }
            IsTriggeredFlag = true;
        }

        protected PageMaker GetSyncPageMaker()
        {
            PageMaker PM = null;
            try
            {
                if (this.SyncPageMakerID != "")
                {
                    PM = (PageMaker)this.Page.FindControl(SyncPageMakerID);
                    return PM;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 執行分頁。
        /// </summary>
        protected void Paging()
        {
            try
            {
                Int32 DataBeginPos = 0;
                //this.DataSource = (DataTable)_PagedControl.GetType().GetProperty("DataSource").GetValue(_PagedControl, null);
                //SourceTable = this.DataSource;
                //SourceTable = (DataTable)this.DataSource;
                if (SourceTable == null || (SourceTable != null && SourceTable.Rows.Count == 0))
                {
                    _PagedControl.GetType().GetProperty("DataSource").SetValue(_PagedControl, SourceTable, null);
                    _PagedControl.DataBind();
                    this.Visible = false;
                    return;
                }
                _BindTable = SourceTable.Clone();
                PageCount = SourceTable.Rows.Count % (int)PageSize > 0 ? SourceTable.Rows.Count / (int)PageSize + 1 : SourceTable.Rows.Count / (int)PageSize;
                DataBeginPos = PageIndex * (int)PageSize;
                for (int i = DataBeginPos; i < DataBeginPos + (int)PageSize; i++)
                {
                    if (i <= SourceTable.Rows.Count - 1)
                    { _BindTable.ImportRow(SourceTable.Rows[i]); }
                    else
                    {
                        break;
                    }
                }
                if (_PagedControl.GetType().GetProperty("PageSize") != null)
                { _PagedControl.GetType().GetProperty("PageSize").SetValue(_PagedControl, (int)PageSize, null); }
                _PagedControl.GetType().GetProperty("DataSource").SetValue(_PagedControl, _BindTable, null);
                _PagedControl.DataBind();
                ResetPageButton();
                PageInfo.Text = "目前:" + (PageIndex + 1) + "/共" + PageCount + "頁";
                PageInfo.Font.Size = this.Font.Size;
                this.Visible = true;
            }
            catch (Exception ex)
            {
                ShowMsg();
            }
        }

        /// <summary>
        /// 重新設定第一頁/上一頁/下一頁/最後一頁按鈕屬性。
        /// </summary>
        protected void ResetPageButton()
        {
            if (PageCount == 0)
            {
                _display = "none";
            }

            if (PageIndex == 0 && PageCount > 1)
            {
                _display = "inline-block";
                ImageButtonFirstPage.Enabled = false;
                ImageButtonPrePage.Enabled = false;
                ImageButtonNextPage.Enabled = true;
                ImageButtonLastPage.Enabled = true;
                ImageButtonFirstPage.ImageUrl = this.Page != null ? this.Page.ClientScript.GetWebResourceUrl(this.GetType(), "APTemplate.Resources.firstPageDisabled.gif") : "";
                ImageButtonPrePage.ImageUrl = this.Page != null ? this.Page.ClientScript.GetWebResourceUrl(this.GetType(), "APTemplate.Resources.prevPageDisabled.gif") : "";
                ImageButtonNextPage.ImageUrl = this.Page != null ? this.Page.ClientScript.GetWebResourceUrl(this.GetType(), "APTemplate.Resources.nextPage.gif") : "";
                ImageButtonLastPage.ImageUrl = this.Page != null ? this.Page.ClientScript.GetWebResourceUrl(this.GetType(), "APTemplate.Resources.lastPage.gif") : "";
            }

            if (PageIndex == 0 && PageCount == 1)
            {
                _display = "inline-block";
                ImageButtonFirstPage.Enabled = false;
                ImageButtonPrePage.Enabled = false;
                ImageButtonNextPage.Enabled = false;
                ImageButtonLastPage.Enabled = false;
                ImageButtonFirstPage.ImageUrl = this.Page != null ? this.Page.ClientScript.GetWebResourceUrl(this.GetType(), "APTemplate.Resources.firstPageDisabled.gif") : "";
                ImageButtonPrePage.ImageUrl = this.Page != null ? this.Page.ClientScript.GetWebResourceUrl(this.GetType(), "APTemplate.Resources.prevPageDisabled.gif") : "";
                ImageButtonNextPage.ImageUrl = this.Page != null ? this.Page.ClientScript.GetWebResourceUrl(this.GetType(), "APTemplate.Resources.nextPageDisabled.gif") : "";
                ImageButtonLastPage.ImageUrl = this.Page != null ? this.Page.ClientScript.GetWebResourceUrl(this.GetType(), "APTemplate.Resources.lastPageDisabled.gif") : "";
            }

            if (PageIndex == PageCount - 1 && PageCount > 1)
            {
                _display = "inline-block";
                ImageButtonFirstPage.Enabled = true;
                ImageButtonPrePage.Enabled = true;
                ImageButtonNextPage.Enabled = false;
                ImageButtonLastPage.Enabled = false;
                ImageButtonFirstPage.ImageUrl = this.Page != null ? this.Page.ClientScript.GetWebResourceUrl(this.GetType(), "APTemplate.Resources.firstPage.gif") : "";
                ImageButtonPrePage.ImageUrl = this.Page != null ? this.Page.ClientScript.GetWebResourceUrl(this.GetType(), "APTemplate.Resources.prevPage.gif") : "";
                ImageButtonNextPage.ImageUrl = this.Page != null ? this.Page.ClientScript.GetWebResourceUrl(this.GetType(), "APTemplate.Resources.nextPageDisabled.gif") : "";
                ImageButtonLastPage.ImageUrl = this.Page != null ? this.Page.ClientScript.GetWebResourceUrl(this.GetType(), "APTemplate.Resources.lastPageDisabled.gif") : "";
            }

            if (PageIndex != 0 && PageIndex < PageCount - 1)
            {
                _display = "inline-block";
                ImageButtonFirstPage.Enabled = true;
                ImageButtonPrePage.Enabled = true;
                ImageButtonNextPage.Enabled = true;
                ImageButtonLastPage.Enabled = true;
                ImageButtonFirstPage.ImageUrl = this.Page != null ? this.Page.ClientScript.GetWebResourceUrl(this.GetType(), "APTemplate.Resources.firstPage.gif") : "";
                ImageButtonPrePage.ImageUrl = this.Page != null ? this.Page.ClientScript.GetWebResourceUrl(this.GetType(), "APTemplate.Resources.prevPage.gif") : "";
                ImageButtonNextPage.ImageUrl = this.Page != null ? this.Page.ClientScript.GetWebResourceUrl(this.GetType(), "APTemplate.Resources.nextPage.gif") : "";
                ImageButtonLastPage.ImageUrl = this.Page != null ? this.Page.ClientScript.GetWebResourceUrl(this.GetType(), "APTemplate.Resources.lastPage.gif") : "";
            }
            string PagePos = Convert.ToString(PageIndex + 1);
        }

        /// <summary>
        /// 取得總頁數。
        /// </summary>
        protected int GetPageCount()
        {

            if (this.DataSource == null)
            {
                SourceTable = null;
            }
            else
            {
                Type t = this.DataSource.GetType();
                if (!t.IsGenericType) //不屬於泛型型別
                {
                    SourceTable = t.Name == "DataTable" ? (DataTable)this.DataSource : ((DataView)this.DataSource).ToTable();
                }
                else if (t.IsGenericType && t.Name.ToLower().IndexOf("list") != -1) //屬於泛型型別,且為list<T>型別
                {
                    Type GenericType = t.GetGenericArguments()[0];
                    MethodInfo GenericMethod = typeof(PageMaker).GetMethod("ToDataTable", BindingFlags.NonPublic | BindingFlags.Instance).MakeGenericMethod(GenericType);
                    SourceTable = (DataTable)GenericMethod.Invoke(this, new object[] { this.DataSource });
                }
            }
            int _PageCount = SourceTable == null ? -1 : SourceTable.Rows.Count % (int)PageSize > 0 ? SourceTable.Rows.Count / (int)PageSize + 1 : SourceTable.Rows.Count / (int)PageSize;
            return _PageCount;
        }

        /// <summary>
        /// 設定分頁的資料來源(須與gridview,listview,datalist等控制項的資料來源相同)。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("設定分頁的資料來源(須與gridview,listview,datalist等控制項的資料來源相同)。")]
        public Object DataSource
        {
            get
            {
                return mDataSource;
            }
            set
            {
                mDataSource = value;
            }
        }

        /// <summary>
        /// 一個分頁呈現的自訂資料筆數。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("一個分頁呈現的自訂資料筆數。")]
        protected int DefaultPageSize
        {
            get
            {
                return _DefaultPageSize;
            }
            set
            {
                _DefaultPageSize = value;
            }
        }

        /// <summary>
        /// 觸發自訂分頁事件
        /// </summary>
        protected virtual void OnCustomPageSize(object sender, PageSizeCustomEventArgs e)
        {
            if (CustomPageSize != null)
            {
                _HasCustomPageSizeEvent = true;
                CustomPageSize(sender, e);
            }
        }

        /// <summary>
        /// 將必要保存的資料載入到ControlState。
        /// </summary>
        protected override void LoadControlState(object savedState)
        {
            object[] allStates = (object[])savedState;
            _PageSize = (int)allStates[0];
            _PageIndex = (int)allStates[1];

            PageTo.Text = (string)allStates[2];
            _DefaultPageSize = (int)allStates[3];
            mDataSource = MemoryCache.Default[Context.Session.SessionID + "DataSource"];
            //mDataSource = HttpContext.Current.Session["DataSource"];
            //mDataSource = (object)allStates[4];
        }

        /// <summary>
        /// 將必要保存的資料儲存到ControlState。
        /// </summary>
        protected override object SaveControlState()
        {
            object[] allStates = new object[5];
            allStates[0] = _PageSize;
            allStates[1] = _PageIndex;
            allStates[2] = PageTo.Text;
            allStates[3] = _DefaultPageSize;
            var policy = new CacheItemPolicy();
            policy.AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(10);
            if (mDataSource != null) MemoryCache.Default.Set(Context.Session.SessionID + "DataSource", mDataSource, policy);
            //HttpContext.Current.Session["DataSource"] = mDataSource;
            //allStates[4] = mDataSource;
            return allStates;
        }

        #endregion

        #region Private Properties & Methods

        /// <summary>
        /// 設定PageMaker內的子控制項。
        /// </summary>
        private void ChildControlsSetting()
        {
            //設定PageInfo
            PageInfo.ID = "PageInfo";
            PageInfo.Font.Size = this.Font.Size;
            PageInfo.Font.Bold = true;

            //設定ImageButtonFirstPage
            ImageButtonFirstPage.ID = "ImageButtonFirstPage";
            ImageButtonFirstPage.Height = Unit.Pixel(22);
            ImageButtonFirstPage.Width = Unit.Pixel(26);
            ImageButtonFirstPage.CausesValidation = false;
            ImageButtonFirstPage.Click += new ImageClickEventHandler(ImageButtonFirstPage_Click);

            //設定ImageButtonPrePage
            ImageButtonPrePage.ID = "ImageButtonPrePage";
            ImageButtonPrePage.Height = Unit.Pixel(22);
            ImageButtonPrePage.Width = Unit.Pixel(26);
            ImageButtonPrePage.CausesValidation = false;
            ImageButtonPrePage.Click += new ImageClickEventHandler(ImageButtonPrePage_Click);

            //設定ImageButtonNextPage
            ImageButtonNextPage.ID = "ImageButtonNextPage";
            ImageButtonNextPage.Height = Unit.Pixel(22);
            ImageButtonNextPage.Width = Unit.Pixel(26);
            ImageButtonNextPage.CausesValidation = false;
            ImageButtonNextPage.Click += new ImageClickEventHandler(ImageButtonNextPage_Click);

            //設定ImageButtonLastPage
            ImageButtonLastPage.ID = "ImageButtonLastPage";
            ImageButtonLastPage.Height = Unit.Pixel(22);
            ImageButtonLastPage.Width = Unit.Pixel(26);
            ImageButtonLastPage.CausesValidation = false;
            ImageButtonLastPage.Click += new ImageClickEventHandler(ImageButtonLastPage_Click);

            //設定Label1
            Label1.ID = "Label1";
            Label1.Text = "到";
            Label1.Font.Size = this.Font.Size;
            Label1.Font.Bold = true;

            //設定PageTo
            PageTo.ID = "PageTo";
            PageTo.Width = Unit.Pixel(25);
            PageTo.AutoPostBack = true;
            PageTo.TextChanged += new EventHandler(PageTo_TextChanged);

            //設定Label2
            Label2.ID = "Label2";
            Label2.Text = "頁";
            Label2.Font.Size = this.Font.Size;
            Label2.Font.Bold = true;

            //SeparatorImg
            SeparatorImg.ImageUrl = this.Page != null ? this.Page.ClientScript.GetWebResourceUrl(this.GetType(), "APTemplate.Resources.separator.gif") : "";
            SeparatorImg.ToolTip = "Separator";
            SeparatorImg.BorderWidth = Unit.Pixel(0);

            //RowsDisplayImg
            RowsDisplayImg.ImageUrl = this.Page != null ? this.Page.ClientScript.GetWebResourceUrl(this.GetType(), "APTemplate.Resources.rowsDisplayed.gif") : "";
            RowsDisplayImg.ToolTip = "Rows Displayed";
            RowsDisplayImg.BorderWidth = Unit.Pixel(0);
            RowsDisplayImg.ImageAlign = ImageAlign.AbsBottom;

            //設定DropDownListRecord
            DropDownListRecord.ID = "DropDownListRecord";
            DropDownListRecord.AutoPostBack = true;
            DropDownListRecord.SelectedIndexChanged += new EventHandler(DropDownListRecord_SelectedIndexChanged);
            //若有預設或自訂分頁數,則將預設或自訂分頁數加入DropDownListRecord
            DropDownListRecord.Items.Clear();
            DropDownListRecord.Items.Insert(0, new ListItem(DefaultPageSize.ToString()));
            OnCustomPageSize(this, new PageSizeCustomEventArgs(DropDownListRecord));
            if (!_HasCustomPageSizeEvent)
            {
                if (10 != DefaultPageSize) DropDownListRecord.Items.Add("10");
                if (15 != DefaultPageSize) DropDownListRecord.Items.Add("15");
                if (25 != DefaultPageSize) DropDownListRecord.Items.Add("25");
                if (50 != DefaultPageSize) DropDownListRecord.Items.Add("50");
            }
            if (1000000 != DefaultPageSize) DropDownListRecord.Items.Add(new ListItem("不分頁", "1000000"));
        }

        /// <summary>
        /// 若分頁錯誤秀alert訊息。
        /// </summary>
        private void ShowMsg()
        {
            Context.Response.Write("<script>alert('PageMaker物件的屬性PagedControlID尚未設定或設定錯誤!')</script>");
        }

        /// <summary>  
        /// Convert a List{T} to a DataTable.  
        /// </summary>  
        private DataTable ToDataTable<T>(List<T> items)
        {
            DataTable tb = new DataTable(typeof(T).Name);
            PropertyInfo[] props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in props)
            {
                Type t = GetCoreType(prop.PropertyType);
                tb.Columns.Add(prop.Name, t);
            }
            foreach (T item in items)
            {
                object[] values = new object[props.Length];
                for (int i = 0; i < props.Length; i++)
                {
                    values[i] = props[i].GetValue(item, null);
                }
                tb.Rows.Add(values);
            }
            return tb;
        }

        /// <summary>  
        /// Determine of specified type is nullable  
        /// </summary>  
        private static bool IsNullable(Type t)
        {
            return !t.IsValueType || (t.IsGenericType && t.GetGenericTypeDefinition() == typeof(Nullable<>));
        }

        /// <summary>  
        /// Return underlying type if type is Nullable otherwise return the type  
        /// </summary>  
        private static Type GetCoreType(Type t)
        {
            if (t != null && IsNullable(t))
            {
                if (!t.IsValueType)
                {
                    return t;
                }
                else
                {
                    return Nullable.GetUnderlyingType(t);
                }
            }
            else
            {
                return t;
            }
        }

        #endregion
    }
}