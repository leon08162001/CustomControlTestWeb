using System;
using System.Text;
using System.ComponentModel;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Web;
using System.Web.UI;
using System.Drawing;
using System.Drawing.Design;
using System.Web.UI.Design;
using System.Web.UI.WebControls;
using Microsoft.VisualBasic;
using AjaxPro;

namespace APTemplate
{
    /// <summary>
    /// 自定義的頁籤控制項
    /// </summary>
    [
     ToolboxData(@"<{0}:TabsView runat=""server""></{0}:TabsView>"),
     Designer(typeof(TabsViewDesigner)),
     ToolboxBitmap(typeof(TabsView), "Resources.Control_ToolBar.bmp")
    ]
    public class TabsView : CompositeControl, INamingContainer
    {
        #region declarations
        private List<TabPage> tabs = new List<TabPage>();
        private bool _IsUseTabBackImage = false;
        private string _TabPageLeftBackImageUrl = "";
        private string _SelectedTabPageLeftBackImageUrl = "";
        private string _TabPageCenterBackImageUrl = "";
        private string _SelectedTabPageCenterBackImageUrl = "";
        private string _TabPageRightBackImageUrl = "";
        private string _SelectedTabPageRightBackImageUrl = "";
        private Unit _TabPageRightBackImageWidth = Unit.Pixel(0);
        private Unit _TabPageLeftBackImageWidth = Unit.Pixel(0);
        private Unit _TabPageLeftBackImageHeight = Unit.Pixel(0);
        private Unit _TabPageRightBackImageHeight = Unit.Pixel(0);
        private Color _TabTextColor;
        private Color _TabBackColor;
        private string TabBgColor = "";
        private string SelectedTabBgColor = "";
        private bool _TabTextBold = false;
        private bool _TabTextItalic = false;
        private FontUnit _TabTextSize = FontUnit.Medium;
        private bool _TabTextUnderline = false;
        private bool _CauseValidationByTabChange = false;
        private string _TabTextFontName = "";
        private readonly object TabSelectionChangingObject = new object();
        public delegate void TabSelectionChangingHandler(object sender, TabSelectionChangingEventArgs e);


        #endregion

        #region Public Properties & Methods

        /// <summary>
        /// Tab標籤集合。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("Tab標籤集合。"), RefreshProperties(RefreshProperties.Repaint)]
        [PersistenceMode(PersistenceMode.InnerProperty), DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [NotifyParentProperty(true)]
        [Editor(typeof(TabPagesCollectionEditor), typeof(UITypeEditor))]
        public virtual List<TabPage> Tabs
        {
            get
            {
                if (tabs == null)
                {
                    tabs = new List<TabPage>();
                }
                return tabs;
            }
        }

        /// <summary>
        ///Tab標籤是否使用背景圖片。
        /// </summary>
        [DefaultValue(false),
         Category("自訂"),
         Description("Tab標籤是否使用背景圖片。")]
        public bool IsUseTabBackImage
        {
            get { return _IsUseTabBackImage; }
            set { _IsUseTabBackImage = value; }
        }

        /// <summary>
        ///Tab標籤的文字顏色。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("Tab標籤的文字顏色。")]
        [Editor(typeof(ColorEditor), typeof(UITypeEditor))]
        public Color TabTextColor
        {
            get
            {
                if (_TabTextColor.IsEmpty)
                    return Color.FromArgb(0, 0, 0);
                else
                    return _TabTextColor;
            }
            set { _TabTextColor = value; }
        }

        /// <summary>
        ///Tab標籤文字是否粗體。
        /// </summary>
        [DefaultValue(false),
         Category("自訂"),
         Description("Tab標籤文字是否粗體。")]
        public bool TabTextBold
        {
            get { return _TabTextBold; }
            set { _TabTextBold = value; }
        }

        /// <summary>
        ///Tab標籤文字是否斜體。
        /// </summary>
        [DefaultValue(false),
         Category("自訂"),
         Description("Tab標籤文字是否斜體。")]
        public bool TabTextItalic
        {
            get { return _TabTextItalic; }
            set { _TabTextItalic = value; }
        }

        /// <summary>
        ///Tab標籤文字字體大小。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("Tab標籤文字字體大小。")]
        public FontUnit TabTextSize
        {
            get { return _TabTextSize; }
            set { _TabTextSize = value; }
        }

        /// <summary>
        ///Tab標籤文字字體樣式。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("Tab標籤文字字體樣式。")]
        public string TabTextFontName
        {
            get { return _TabTextFontName; }
            set { _TabTextFontName = value; }
        }

        /// <summary>
        ///Tab標籤文字是否有底線。
        /// </summary>
        [DefaultValue(false),
         Category("自訂"),
         Description("Tab標籤文字是否有底線。")]
        public bool TabTextUnderline
        {
            get { return _TabTextUnderline; }
            set { _TabTextUnderline = value; }
        }

        /// <summary>
        ///切換Tab是否造成驗證表單(前提AutoPostBack屬性必須設為true)。
        /// </summary>
        [DefaultValue(false),
         Category("自訂"),
         Description("切換Tab是否造成驗證表單(前提AutoPostBack屬性必須設為true)。")]
        public bool CauseValidationByTabChange
        {
            get { return _CauseValidationByTabChange; }
            set { _CauseValidationByTabChange = value; }
        }



        /// <summary>
        ///Tab標籤的背景顏色。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("Tab標籤的背景顏色。")]
        [Editor(typeof(ColorEditor), typeof(UITypeEditor))]
        public Color TabBackColor
        {
            get
            {
                if (_TabBackColor.IsEmpty)
                    return Color.FromArgb(239, 239, 239);
                else
                    return _TabBackColor;
            }
            set { _TabBackColor = value; }
        }

        /// <summary>
        ///Tab標籤左邊背景圖片位置。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("Tab標籤左邊背景圖片位置。")]
        [UrlProperty("")]
        [Editor(typeof(ImageUrlEditor), typeof(UITypeEditor))]
        public string TabPageLeftBackImageUrl
        {
            get { return _TabPageLeftBackImageUrl; }
            set { _TabPageLeftBackImageUrl = value; }
        }

        /// <summary>
        ///目前Tab標籤左邊背景圖片位置。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("目前Tab標籤左邊背景圖片位置。")]
        [UrlProperty("")]
        [Editor(typeof(ImageUrlEditor), typeof(UITypeEditor))]
        public string SelectedTabPageLeftBackImageUrl
        {
            get { return _SelectedTabPageLeftBackImageUrl; }
            set { _SelectedTabPageLeftBackImageUrl = value; }
        }

        /// <summary>
        /// Tab標籤中間背景圖片位置。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("Tab標籤左邊中間圖片位置。")]
        [UrlProperty()]
        [Editor(typeof(ImageUrlEditor), typeof(UITypeEditor))]
        public string TabPageCenterBackImageUrl
        {
            get { return _TabPageCenterBackImageUrl; }
            set { _TabPageCenterBackImageUrl = value; }
        }

        /// <summary>
        /// 目前Tab標籤中間背景圖片位置。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("目前Tab標籤左邊中間圖片位置。")]
        [UrlProperty()]
        [Editor(typeof(ImageUrlEditor), typeof(UITypeEditor))]
        public string SelectedTabPageCenterBackImageUrl
        {
            get { return _SelectedTabPageCenterBackImageUrl; }
            set { _SelectedTabPageCenterBackImageUrl = value; }
        }

        /// <summary>
        ///Tab標籤右邊背景圖片位置。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("Tab標籤右邊背景圖片位置。")]
        [UrlProperty()]
        [Editor(typeof(ImageUrlEditor), typeof(UITypeEditor))]
        public string TabPageRightBackImageUrl
        {
            get { return _TabPageRightBackImageUrl; }
            set { _TabPageRightBackImageUrl = value; }
        }

        /// <summary>
        ///目前Tab標籤右邊背景圖片位置。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("目前Tab標籤右邊背景圖片位置。")]
        [UrlProperty()]
        [Editor(typeof(ImageUrlEditor), typeof(UITypeEditor))]
        public string SelectedTabPageRightBackImageUrl
        {
            get { return _SelectedTabPageRightBackImageUrl; }
            set { _SelectedTabPageRightBackImageUrl = value; }
        }

        /// <summary>
        /// Tab標籤左邊背景圖片寬度。
        /// </summary>
        [Category("自訂")]
        [DefaultValue(0)]
        [Description("Tab標籤左邊背景圖片寬度")]
        public Unit TabPageLeftBackImageWidth
        {
            get { return _TabPageLeftBackImageWidth; }
            set { _TabPageLeftBackImageWidth = value; }
        }

        /// <summary>
        /// Tab標籤左邊背景圖片高度。
        /// </summary>
        [Category("自訂")]
        [DefaultValue(0)]
        [Description("Tab標籤左邊背景圖片高度")]
        public Unit TabPageLeftBackImageHeight
        {
            get { return _TabPageLeftBackImageHeight; }
            set { _TabPageLeftBackImageHeight = value; }
        }

        /// <summary>
        /// Tab標籤右邊背景圖片寬度。
        /// </summary>
        [Category("自訂")]
        [DefaultValue(0)]
        [Description("Tab標籤右邊背景圖片寬度")]
        public Unit TabPageRightBackImageWidth
        {
            get { return _TabPageRightBackImageWidth; }
            set { _TabPageRightBackImageWidth = value; }
        }

        /// <summary>
        /// Tab標籤右邊背景圖片高度。
        /// </summary>
        [Category("自訂")]
        [DefaultValue(0)]
        [Description("Tab標籤右邊背景圖片高度")]
        public Unit TabPageRightBackImageHeight
        {
            get { return _TabPageRightBackImageHeight; }
            set { _TabPageRightBackImageHeight = value; }
        }

        /// <summary>
        /// 切換Tab標籤時是否要執行PostBack行為。
        /// </summary>
        [Category("自訂"),
         Description("切換Tab標籤時是否要執行PostBack行為。"),
        DefaultValue(false)]
        public bool AutoPostBack
        {
            set
            {
                this.ViewState["AutoPostBack"] = value;
            }
            get
            {
                object val = this.ViewState["AutoPostBack"];
                if (val == null) return false;
                return Convert.ToBoolean(val);
            }
        }

        public Color TabButtonBorderColor
        {
            get
            {
                object val = this.ViewState["TabButtonBorderColor"];
                if (val == null) return Color.FromName("#d4d0c8");
                return (Color)val;
            }
            set
            {
                this.ViewState["TabButtonBorderColor"] = value;
            }
        }

        public void Refresh()
        {
            CreateChildControls();
        }


        /// <summary>
        ///目前所選擇的標籤索引。
        /// </summary>
        [DefaultValue(0),
         Category("自訂"),
         Description("目前所選擇的標籤索引。")]
        public int CurrentTabIndex
        {
            get
            {
                object val = this.ViewState["CurrentTabIndex"];
                if (val == null) return 0;
                return Convert.ToInt32(val);
            }
            set
            {
                object val = ViewState["CurrentTabIndex"];
                if (val != null)
                {
                    int oldIndex = Convert.ToInt32(val);
                    int newIndex = Convert.ToInt32(value);

                    if (oldIndex != newIndex)
                    {
                        TabSelectionChangingEventArgs e = new TabSelectionChangingEventArgs(oldIndex, newIndex, this);
                        TabSelectionChangingHandler handler = (TabSelectionChangingHandler)Events[TabSelectionChangingObject];
                        if (handler != null)
                            handler(this, e);
                    }
                }
                this.ViewState["CurrentTabIndex"] = value;
            }
        }

        /// <summary>
        ///被選取的Tab標籤的背景顏色。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("被選取的Tab標籤的背景顏色。")]
        [Editor(typeof(ColorEditor), typeof(UITypeEditor))]
        public Color SelectedTabBackColor
        {
            set
            {
                this.ViewState["SelectedTabBackColor"] = value;
            }
            get
            {
                object val = this.ViewState["SelectedTabBackColor"];
                if (val == null) return this.TabBackColor;
                return (Color)val;
            }
        }

        #region Public events

        public event TabSelectionChangingHandler TabSelectionChanging
        {
            add { Events.AddHandler(TabSelectionChangingObject, value); }
            remove { Events.RemoveHandler(TabSelectionChangingObject, value); }
        }

        #endregion

        #endregion

        #region Protected Properties & Methods

        protected override void OnLoad(EventArgs e)
        {
            RegisterAjaxType();
            if (this.Page.Request[this.ClientID + "$hf"] != null)
                CurrentTabIndex = int.Parse(this.Page.Request[this.ClientID + "$hf"]);
            else
                CurrentTabIndex = 0;
        }

        protected override void CreateChildControls()
        {
            this.Controls.Clear();
            Table tbl = new Table();
            string js = "";
            tbl.CellPadding = tbl.CellSpacing = 0;
            tbl.Attributes["style"] = "display:inline-block;vertical-align:top;";

            if (Tabs.Count > 0)
            {
                //建立Tab View的表頭.
                js = CreateTabHeaders(ref tbl);
                this.Page.ClientScript.RegisterArrayDeclaration("tabButtons_" + this.ClientID, js);

                //建立Tab View的內容.        
                js = CreateTabContents(ref tbl);
                this.Page.ClientScript.RegisterArrayDeclaration("tabContents_" + this.ClientID, js);

                //when the post back is performed than select the current tab.
                //During designer editing Page.Request is null therefore we have to
                //check for current Http context.
                if (!this.DesignMode /*HttpContext.Current != null*/)
                {
                    //if (!AutoPostBack)
                    //{
                    //if (this.Page.Request[this.ClientID + "$hf"] != null)
                    //  CurrentTabIndex = int.Parse(this.Page.Request[this.ClientID + "$hf"]);

                    //SelectTab();
                    //}
                    //else
                    //{
                    //  SelectTab();
                    //}
                }
            }
            this.Controls.Add(tbl);
        }

        protected override void OnPreRender(EventArgs e)
        {
            SelectTab();
            RegisterJavaScript.RegisterContolIncludeScript(this.Page);
        }

        #endregion

        #region Private Properties & Methods

        private void SelectTab()
        {
            SelectedTabBgColor = Conversion.Hex(this.SelectedTabBackColor.R).PadLeft(2, '0') + Conversion.Hex(this.SelectedTabBackColor.G).PadLeft(2, '0') + Conversion.Hex(this.SelectedTabBackColor.B).PadLeft(2, '0');
            TabBgColor = Conversion.Hex(this.TabBackColor.R).PadLeft(2, '0') + Conversion.Hex(this.TabBackColor.G).PadLeft(2, '0') + Conversion.Hex(this.TabBackColor.B).PadLeft(2, '0');
            string Script = "<script language='JavaScript'>SelectTab(" + CurrentTabIndex + ",'" +
                              this.ClientID + "','" + this.ClientID + "_hf',{0},'" +
                              this.Page.ResolveClientUrl(TabPageLeftBackImageUrl) + "','" + this.Page.ResolveClientUrl(TabPageCenterBackImageUrl) + "','" + this.Page.ResolveClientUrl(TabPageRightBackImageUrl) + "','" +
                              this.Page.ResolveClientUrl(SelectedTabPageLeftBackImageUrl) + "','" + this.Page.ResolveClientUrl(SelectedTabPageCenterBackImageUrl) + "','" + this.Page.ResolveClientUrl(SelectedTabPageRightBackImageUrl) + "','" +
                              TabBgColor + "','" + SelectedTabBgColor + "')" + ";</script>";
            Script = string.Format(Script, IsUseTabBackImage.ToString().ToLower());
            //if( (ScriptManager.GetCurrent(base.Page)!=null && ScriptManager.GetCurrent(base.Page).IsInAsyncPostBack))
            //  ScriptManager.RegisterStartupScript(this,this.Page.GetType(), "_SelectTab", Script,false);
            //else
            //this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "_SelectTab", Script,false);
            if (RegisterJavaScript.HasScriptManager(base.Page))
                ScriptManager.RegisterStartupScript(this, this.Page.GetType(), "_SelectTab", Script, false);
            else
                base.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "_SelectTab", Script, false);


        }

        /// <summary>
        /// Create Tab headers
        /// </summary>
        /// <param name="tbl">Parent table control whole alignmnet of control</param>
        /// <returns></returns>
        private string CreateTabHeaders(ref Table tbl)
        {
            TableRow tr;
            TableCell tcl;
            TableCell tc;
            TableCell tcr;
            LinkButton lkl;
            LinkButton lk;
            LinkButton lkr;
            StringBuilder arrBtns = new StringBuilder();//contains header js
            string lkId;
            int i = 0;
            //使用隱藏欄位儲存目前所選的Tab標籤索引
            HiddenField hf = new HiddenField();
            hf.ID = "hf";

            Table tblTabs = new Table();
            tblTabs.CellPadding = tblTabs.CellSpacing = 0;
            tr = new TableRow();
            VerifyTabIndex();

            foreach (TabPage tp in Tabs)
            {
                tcl = new TableCell();
                tc = new TableCell();
                tcr = new TableCell();
                lkl = new LinkButton();
                lk = new LinkButton();
                lkr = new LinkButton();
                lk.ID = "tp" + i.ToString();
                lk.Text = "&nbsp;" + tp.Text + "&nbsp;";
                lk.Font.Name = this.TabTextFontName == "" ? lk.Font.Name : this.TabTextFontName;
                lk.Font.Bold = this.TabTextBold;
                lk.Font.Italic = this.TabTextItalic;
                lk.Font.Size = this.TabTextSize;
                lk.Font.Underline = this.TabTextUnderline;
                lk.CommandArgument = i.ToString();
                lk.CausesValidation = this.AutoPostBack ? this.CauseValidationByTabChange : false;

                lk.ForeColor = this.TabTextColor;
                if (!this.IsUseTabBackImage)
                { lk.BackColor = this.TabBackColor; }

                if (this.IsUseTabBackImage)
                {
                    string _TabPageLeftBackImageUrl = this.Page.ResolveClientUrl(this.TabPageLeftBackImageUrl);
                    string _TabPageRightBackImageUrl = this.Page.ResolveClientUrl(this.TabPageRightBackImageUrl);
                    string _TabPageCenterBackImageUrl = this.Page.ResolveClientUrl(this.TabPageCenterBackImageUrl);

                    tcl.Width = this.TabPageLeftBackImageWidth;
                    tcl.Height = this.TabPageLeftBackImageHeight;
                    tcl.Attributes["style"] += "background-image:url(" + _TabPageLeftBackImageUrl + "); background-repeat:no-repeat;";
                    lkl.ID = "tp" + i.ToString() + "l";
                    lkl.Width = this.TabPageLeftBackImageWidth;
                    lkl.Attributes["onclick"] = "return false;";

                    tcr.Width = this.TabPageRightBackImageWidth;
                    tcr.Height = this.TabPageRightBackImageHeight;
                    tcr.Attributes["style"] += "background-image:url(" + _TabPageRightBackImageUrl + "); background-repeat:no-repeat;";
                    lkr.ID = "tp" + i.ToString() + "r";
                    lkr.Width = this.TabPageRightBackImageWidth;
                    lkr.Attributes["onclick"] = "return false;";

                    lk.Height = this.TabPageLeftBackImageHeight;
                    tc.Attributes["style"] += "background-image:url(" + _TabPageCenterBackImageUrl + "); background-repeat:repeat-x;";
                }

                if (AutoPostBack) lk.Click += new EventHandler(lk_Click);

                lkId = this.ClientID + "_" + lk.ID;
                arrBtns.AppendFormat("\"{0}\"{1}", lkId, (i == Tabs.Count - 1 ? "" : ","));
                lk.OnClientClick = "document.getElementById('" + this.ClientID + "_" + hf.ClientID + "').value='" + i + "';";
                if (!AutoPostBack)
                {
                    SelectedTabBgColor = Conversion.Hex(this.SelectedTabBackColor.R).PadLeft(2, '0') + Conversion.Hex(this.SelectedTabBackColor.G).PadLeft(2, '0') + Conversion.Hex(this.SelectedTabBackColor.B).PadLeft(2, '0');
                    TabBgColor = Conversion.Hex(this.TabBackColor.R).PadLeft(2, '0') + Conversion.Hex(this.TabBackColor.G).PadLeft(2, '0') + Conversion.Hex(this.TabBackColor.B).PadLeft(2, '0');
                    string Script = "return OnTabClick(this," + i.ToString() + ",'" +
                                    this.ClientID + "','" + this.ClientID + "_" + hf.ClientID + "',{0},'" +
                                    this.Page.ResolveClientUrl(TabPageLeftBackImageUrl) + "','" + this.Page.ResolveClientUrl(TabPageCenterBackImageUrl) + "','" + this.Page.ResolveClientUrl(TabPageRightBackImageUrl) + "','" +
                                    this.Page.ResolveClientUrl(SelectedTabPageLeftBackImageUrl) + "','" + this.Page.ResolveClientUrl(SelectedTabPageCenterBackImageUrl) + "','" + this.Page.ResolveClientUrl(SelectedTabPageRightBackImageUrl) + "','" +
                                    TabBgColor + "','" + SelectedTabBgColor + "');";
                    Script = string.Format(Script, IsUseTabBackImage.ToString().ToLower());
                    lk.OnClientClick += Script;
                }

                tc.Controls.Add(lk);

                if (this.IsUseTabBackImage)
                {
                    tcl.Controls.Add(lkl);
                    tr.Cells.Add(tcl);
                }
                tr.Cells.Add(tc);
                if (this.IsUseTabBackImage)
                {
                    tcr.Controls.Add(lkr);
                    tr.Cells.Add(tcr);
                }
                ++i;
            }
            tblTabs.Rows.Add(tr);


            tc = new TableCell();
            tc.Controls.Add(tblTabs);
            tc.Controls.Add(hf);

            tr = new TableRow();
            tr.Cells.Add(tc);


            tbl.Rows.Add(tr);
            return arrBtns.ToString();
        }

        /// <summary>
        /// create tab contents.
        /// </summary>
        /// <param name="tbl">parent table refrence.</param>
        /// <returns></returns>
        private string CreateTabContents(ref Table tbl)
        {
            TableRow tr;
            TableCell tc;
            int i = 0;
            string tpId;
            StringBuilder arrTabPages = new StringBuilder();

            Table tblContents = new Table();
            tblContents.CellPadding = tblContents.CellSpacing = 0;

            tr = new TableRow();
            tc = new TableCell();
            //int i = -1;
            foreach (TabPage tp in Tabs)
            {
                tpId = this.ClientID + "_" + tp.ID;
                tc.Controls.Add(tp);
                if (i == tabs.Count - 1)
                    arrTabPages.AppendFormat("\"{0}\"", tpId);
                else
                    arrTabPages.AppendFormat("\"{0}\",", tpId);
                ++i;

            }
            tr.Cells.Add(tc);
            tblContents.Rows.Add(tr);

            tc = new TableCell();
            tc.Controls.Add(tblContents);
            tr = new TableRow();
            tr.Cells.Add(tc);

            tbl.Rows.Add(tr);
            return arrTabPages.ToString();
        }

        private void lk_Click(object sender, EventArgs e)
        {
            LinkButton lk = (LinkButton)sender;
            this.CurrentTabIndex = Convert.ToInt32(lk.CommandArgument);
            //select the current tab.
            //SelectTab();
        }

        private void VerifyTabIndex()
        {
            if (CurrentTabIndex >= Tabs.Count)
                CurrentTabIndex = Tabs.Count - 1;
            //throw new Exception("Invalid Tab Index");
        }

        private void RegisterAjaxType()
        {
            List<Control> AllControls = PublicFunc.GetChildControls(Page);
            foreach (Control Ctl in AllControls)
            {
                if (Ctl.GetType() == typeof(DropDownList_Multiple))
                { AjaxPro.Utility.RegisterTypeForAjax(typeof(APTemplate.DropDownList_Multiple)); }
                else if (Ctl.GetType() == typeof(DropDownList_Date))
                { AjaxPro.Utility.RegisterTypeForAjax(typeof(APTemplate.DropDownList_Date)); }
                else if (Ctl.GetType() == typeof(Identity))
                { AjaxPro.Utility.RegisterTypeForAjax(typeof(APTemplate.Identity)); }
                else if (Ctl.GetType() == typeof(ListBoxToListBox))
                { AjaxPro.Utility.RegisterTypeForAjax(typeof(APTemplate.ListBoxToListBox)); }
                else if (Ctl.GetType() == typeof(Captcha))
                { AjaxPro.Utility.RegisterTypeForAjax(typeof(APTemplate.Captcha)); }
                else if (Ctl.GetType() == typeof(ToolBar))
                {
                    AjaxPro.Utility.RegisterTypeForAjax(typeof(APTemplate.DropDownList_Multiple));
                    AjaxPro.Utility.RegisterTypeForAjax(typeof(APTemplate.DropDownList_Date));
                    AjaxPro.Utility.RegisterTypeForAjax(typeof(APTemplate.Identity));
                    AjaxPro.Utility.RegisterTypeForAjax(typeof(APTemplate.ListBoxToListBox));
                }
            }
        }

        #endregion

    }

    public class TabSelectionChangingEventArgs : EventArgs
    {
        #region declarations

        private int prevIndex;
        private int newIndex;
        private string newTabName;
        private string prevTabName;

        #endregion

        #region Public Properties & Methods

        public TabSelectionChangingEventArgs(int prevIndex, int newIndex, APTemplate.TabsView TabsView)
        {
            this.prevIndex = prevIndex;
            this.newIndex = newIndex;
            this.prevTabName = TabsView.Tabs[prevIndex].Text;
            this.newTabName = TabsView.Tabs[newIndex].Text;
        }

        public int NewIndex
        {
            get { return newIndex; }
        }

        public int PreviousIndex
        {
            get { return prevIndex; }
        }

        public string NewTabName
        {
            get { return newTabName; }
        }

        public string PrevTabName
        {
            get { return prevTabName; }
        }

        #endregion

    }
}