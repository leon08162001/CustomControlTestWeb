using System;
using System.Configuration;
using System.ComponentModel;
using System.Web;
using System.Text;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.Design.WebControls;
using System.Drawing;
using System.Drawing.Design;
using Microsoft.VisualBasic;
using System.Collections;

/// <summary>
/// 允許輸入一組整數字或小數的自定義輸入控制項。
/// </summary>
/// <remarks>
/// <list type='bullet'>
/// <item><description>請使用<bold>Text</bold>屬性取得輸入值。</description></item>
/// </list>
/// </remarks>
namespace APTemplate
{
    /// <summary>
    /// 列舉-Menu的樣式。
    /// </summary>
    public enum eMenuType
    {
        Basic = 0,
        Basic_Bounce_Arrows = 1,
        Basic_Red_Highlight = 2,
        Blue_Tones = 3,
        Drop_Shadow = 4,
        Gradient_Background = 5,
        Gray_Scale = 6,
        Stretch_Buttons_Tan = 7,
        Thick_Borders = 8,
        Tree_Menu = 9
    }


    /// <summary>
    /// 列舉-Menu的陳列方向。
    /// </summary>
    public enum eMenuPosition
    {
        Horizontal = 0,
        Vertical = 1
    }

    /// <summary>
    /// 列舉-資料來源類型。
    /// </summary>
    public enum eDataType
    {
        DataBase = 0,
        XML = 1
    }

    /// <summary>
    /// 自定義的Menu控制項
    /// </summary>
    [ToolboxData("<{0}:Menu runat=server></{0}:Menu>")]
    [ToolboxBitmap(typeof(System.Web.UI.WebControls.Menu))]
    [Designer(typeof(MenuDesigner))]
    public class Menu : CompositeControl, INamingContainer
    {
        protected int _MenuWidth = 150;
        protected int _MenuItemWidth = 145;
        protected StringBuilder SB = new StringBuilder(5000);
        protected eMenuType _MenuType = eMenuType.Basic;
        protected eMenuPosition _MenuPosition = eMenuPosition.Horizontal;
        protected eDataType _DataType = eDataType.XML;
        protected TargetType _Target = TargetType.self;
        protected string _FirstLevelMenuTable = "";
        protected string _SecondLevelMenuTable = "";
        protected string _ThirdLevelMenuTable = "";
        protected string _ForthLevelMenuTable = "";
        protected string _FifthLevelMenuTable = "";
        protected string _SixthLevelMenuTable = "";
        protected string _SeventhLevelMenuTable = "";
        protected string _EighthLevelMenuTable = "";
        protected string _NinethLevelMenuTable = "";
        protected string _TenthLevelMenuTable = "";
        protected string _ConnectionKey = "default";
        protected string _MenuTableBaseName = "";
        protected string _ClientScript = "";
        protected Byte _MenuLevel = 1;
        protected DataSet DS = new DataSet();
        private int j;
        protected string _FrameName = "";
        private string _TargetStr = "_self";
        protected Color _MainItemBgColor;
        protected Color _SubItemBgColor;
        protected Color _MainItemForeColor;
        protected Color _SubItemForeColor;
        protected Color _ItemHoverBgColor;
        protected bool _HasCheckBox = false;
        protected string _CheckBoxBoundField = "";

        #region Public Properties & Methods

        /// <summary>
        ///Menu的寬度。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("Menu的寬度。")]
        public virtual int MenuWidth
        {
            get
            {
                return _MenuWidth;
            }
            set { _MenuWidth = value; }
        }

        /// <summary>
        ///MenuItem的寬度。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("MenuItem的寬度。")]
        public virtual int MenuItemWidth
        {
            get
            {
                return _MenuItemWidth;
            }
            set { _MenuItemWidth = value; }
        }

        /// <summary>
        ///Menu的樣式。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("Menu的樣式。")]
        public eMenuType MenuType
        {
            get { return _MenuType; }
            set { _MenuType = value; }
        }

        /// <summary>
        ///Menu的陳列方向。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("Menu的陳列方向。")]
        public eMenuPosition MenuPosition
        {
            get { return _MenuPosition; }
            set { _MenuPosition = value; }
        }

        /// <summary>
        ///資料來源類型。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("資料來源類型。")]
        public eDataType DataType
        {
            get { return _DataType; }
            set
            {
                _DataType = value;
                if (value == eDataType.XML)
                {
                    _FirstLevelMenuTable = "";
                    _SecondLevelMenuTable = "";
                    _ThirdLevelMenuTable = "";
                    _ForthLevelMenuTable = "";
                }
            }
        }

        /// <summary>
        ///MenuItem前是否加入checkbox。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("MenuItem前是否加入checkbox。")]
        public virtual bool HasCheckBox
        {
            get
            {
                return _HasCheckBox;
            }
            set { _HasCheckBox = value; }
        }

        /// <summary>
        ///checkbox繫結的資料庫欄位。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("checkbox繫結的資料庫欄位。")]
        public virtual string CheckBoxBoundField
        {
            get
            {
                return _CheckBoxBoundField;
            }
            set { _CheckBoxBoundField = value; }
        }

        /// <summary>
        ///執行超連結網址的視窗位置。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("執行超連結網址的視窗位置。")]
        public TargetType Target
        {
            get { return _Target; }
            set
            {
                _Target = value;
                _TargetStr = _Target == TargetType.blank ? "_blank" : _Target == TargetType.parent ? "_parent" : _Target == TargetType.self ? "_self" : _Target == TargetType.top ? "_top" : _FrameName;
            }
        }

        /// <summary>
        ///視窗或框架名稱。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("視窗或框架名稱。")]
        public string FrameName
        {
            get { return _FrameName; }
            set
            {
                _FrameName = value;
                if (Target == TargetType.Frame)
                    _TargetStr = _FrameName;
            }
        }

        /// <summary>
        ///資料庫連線Key。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("資料庫連線Key。")]
        public string ConnectionKey
        {
            get { return _ConnectionKey; }
            set { _ConnectionKey = value; }
        }

        /// <summary>
        ///有關Menu資料表基底名稱。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("有關Menu資料表基底名稱。")]
        public string MenuTableBaseName
        {
            get { return _MenuTableBaseName; }
            set { _MenuTableBaseName = value; }
        }

        /// <summary>
        ///選單的層數。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("選單的層數。")]
        public Byte MenuLevel
        {
            get { return _MenuLevel; }
            set { _MenuLevel = value; }
        }

        /// <summary>
        ///按一下超連結所執行的javascript。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("按一下超連結所執行的javascript。")]
        public string ClientScript
        {
            get { return _ClientScript; }
            set
            {
                if (value != "")
                {
                    _ClientScript = value;
                }
                else
                {
                    _ClientScript = "return true;";
                }
            }
        }

        /// <summary>
        ///主選單選項的背景顏色。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("主選單選項的背景顏色。")]
        [Editor(typeof(ColorEditor), typeof(UITypeEditor))]
        public virtual Color MainItemBgColor
        {
            get
            {
                if (_MainItemBgColor.IsEmpty)
                    return Color.FromArgb(255, 255, 255);
                else
                    return _MainItemBgColor;
            }
            set { _MainItemBgColor = value; }
        }

        /// <summary>
        ///次選單選項的背景顏色。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("次選單選項的背景顏色。")]
        [Editor(typeof(ColorEditor), typeof(UITypeEditor))]
        public virtual Color SubItemBgColor
        {
            get
            {
                if (_SubItemBgColor.IsEmpty)
                    return Color.FromArgb(239, 239, 239);
                else
                    return _SubItemBgColor;
            }
            set { _SubItemBgColor = value; }
        }

        /// <summary>
        ///主選單選項的文字顏色。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("主選單選項的文字顏色。")]
        [Editor(typeof(ColorEditor), typeof(UITypeEditor))]
        public virtual Color MainItemForeColor
        {
            get
            {
                if (_MainItemForeColor.IsEmpty)
                    return Color.FromArgb(51, 51, 51);
                else
                    return _MainItemForeColor;
            }
            set { _MainItemForeColor = value; }
        }

        /// <summary>
        ///次選單選項的背景顏色。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("次選單選項的文字顏色。")]
        [Editor(typeof(ColorEditor), typeof(UITypeEditor))]
        public virtual Color SubItemForeColor
        {
            get
            {
                if (_SubItemForeColor.IsEmpty)
                    return Color.FromArgb(85, 85, 85);
                else
                    return _SubItemForeColor;
            }
            set { _SubItemForeColor = value; }
        }

        /// <summary>
        ///滑鼠移到選單選項上時的背景顏色。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("滑鼠移到選單選項上時的背景顏色。")]
        [Editor(typeof(ColorEditor), typeof(UITypeEditor))]
        public virtual Color ItemHoverBgColor
        {
            get
            {
                if (_ItemHoverBgColor.IsEmpty)
                    return Color.FromArgb(239, 239, 239);
                else
                    return _ItemHoverBgColor;
            }
            set { _ItemHoverBgColor = value; }
        }

        #endregion

        #region Protected Properties & Methods

        protected override void CreateChildControls()
        {
            EnsureChildControls();
        }

        protected override void RenderContents(HtmlTextWriter writer)
        {
            if (MenuType != eMenuType.Tree_Menu)
            {
                SB.Append(@"<div class='imcm imde' id='imouter1'>");
                SB.Append("<ul id='imenus1'>");
                CreateMenu();
                writer.WriteLine(SB.ToString());
            }
            else
            {
                CreateMenu();
                writer.WriteLine(SB.ToString());
            }
        }

        protected override void OnPreRender(EventArgs e)
        {
            string MainItem_BgColor = Conversion.Hex(this.MainItemBgColor.R).PadLeft(2, '0') + Conversion.Hex(this.MainItemBgColor.G).PadLeft(2, '0') + Conversion.Hex(this.MainItemBgColor.B).PadLeft(2, '0');
            string SubItem_BgColor = Conversion.Hex(this.SubItemBgColor.R).PadLeft(2, '0') + Conversion.Hex(this.SubItemBgColor.G).PadLeft(2, '0') + Conversion.Hex(this.SubItemBgColor.B).PadLeft(2, '0');
            string MainItem_ForeColor = Conversion.Hex(this.MainItemForeColor.R).PadLeft(2, '0') + Conversion.Hex(this.MainItemForeColor.G).PadLeft(2, '0') + Conversion.Hex(this.MainItemForeColor.B).PadLeft(2, '0');
            string SubItem_ForeColor = Conversion.Hex(this.SubItemForeColor.R).PadLeft(2, '0') + Conversion.Hex(this.SubItemForeColor.G).PadLeft(2, '0') + Conversion.Hex(this.SubItemForeColor.B).PadLeft(2, '0');
            string Item_HoverBgColor = Conversion.Hex(this.ItemHoverBgColor.R).PadLeft(2, '0') + Conversion.Hex(this.ItemHoverBgColor.G).PadLeft(2, '0') + Conversion.Hex(this.ItemHoverBgColor.B).PadLeft(2, '0');
            if (this.HasCheckBox)
            { Page.ClientScript.RegisterClientScriptResource(typeof(RegisterJavaScript), "APTemplate.Resources.ControlJS.Menu.js"); }
            try
            {
                string UrlPath = PublicFunc.GetPath(this.Page);
                string CssStr = APTemplate.Properties.Resources.ResourceManager.GetString(MenuType.ToString());
                CssStr = CssStr.Replace("<#MainItem_BgColor#>", MainItem_BgColor);
                CssStr = CssStr.Replace("<#SubItem_BgColor#>", SubItem_BgColor);
                CssStr = CssStr.Replace("<#MainItem_ForeColor#>", MainItem_ForeColor);
                CssStr = CssStr.Replace("<#SubItem_ForeColor#>", SubItem_ForeColor);
                CssStr = CssStr.Replace("<#Item_HoverBgColor#>", Item_HoverBgColor);
                int SearchIdx = 0;
                string SearchStr = "<#UrlPath#>ControlImages/";
                while (CssStr.IndexOf(SearchStr, SearchIdx) > -1)
                {
                    int StartIdx = CssStr.IndexOf(SearchStr, SearchIdx) + SearchStr.Length;
                    int EndIdx = CssStr.IndexOf(");", StartIdx);
                    if (EndIdx - StartIdx >= 5)
                    {
                        string ImgName = CssStr.Substring(StartIdx, EndIdx - StartIdx);
                        string ImgUrl = this.Page != null ? this.Page.ClientScript.GetWebResourceUrl(this.GetType(), "APTemplate.Resources." + ImgName) : "";
                        CssStr = CssStr.Replace(SearchStr + ImgName, ImgUrl);
                        SearchIdx = EndIdx + ");".Length;
                    }
                }
                CssStr = "<style>" + CssStr + "</style>";
                LiteralControl style = new LiteralControl(CssStr);
                Parent.Page.Header.Controls.Add(style);
                string JavaScript = APTemplate.Properties.Resources.ResourceManager.GetString(MenuType + "_" + MenuPosition);
                SearchIdx = 0;
                while (JavaScript.IndexOf(SearchStr, SearchIdx) > -1)
                {
                    int StartIdx = JavaScript.IndexOf(SearchStr, SearchIdx) + SearchStr.Length;
                    int EndIdx = JavaScript.IndexOf("';", StartIdx);
                    if (EndIdx - StartIdx >= 5)
                    {
                        string ImgName = JavaScript.Substring(StartIdx, EndIdx - StartIdx);
                        string ImgUrl = this.Page != null ? this.Page.ClientScript.GetWebResourceUrl(this.GetType(), "APTemplate.Resources." + ImgName) : "";
                        JavaScript = JavaScript.Replace(SearchStr + ImgName, ImgUrl);
                        SearchIdx = EndIdx + "';".Length;
                    }
                }
                if (MenuType == eMenuType.Tree_Menu)
                { JavaScript += "qm_create(0,true,0,500,'all',false,true,false,false);"; }
                Page.ClientScript.RegisterStartupScript(typeof(Menu), "Menu", JavaScript, true);
            }
            catch (Exception ex)
            { }
            finally
            {
            }
        }

        protected override HtmlTextWriterTag TagKey
        {
            get
            {
                if (MenuType != eMenuType.Tree_Menu)
                    return HtmlTextWriterTag.Table;
                else
                    return HtmlTextWriterTag.Div;
            }
        }

        protected override void AddAttributesToRender(HtmlTextWriter writer)
        {
            if (MenuType != eMenuType.Tree_Menu)
            {
                writer.AddAttribute("class", "imrcmain0 imgl");
            }
            else
            {
                writer.AddAttribute("class", "qmmc");
                writer.AddAttribute("id", "qm0");
            }
            string style = "";
            if (!this.DesignMode)
            {
                if (Context.Request.Browser.Type.IndexOf("IE") != -1 && float.Parse(Context.Request.Browser.Version) < 8)
                {
                    style = "display:inline;vertical-align:top;width:{0}px;z-index:999999;position:relative;";
                }
                else
                {
                    style = "display:inline-block;vertical-align:top;width:{0}px;z-index:999999;position:relative;";
                }
            }
            else
            {
                style = "display:inline;vertical-align:top;width:{0}px;z-index:999999;position:relative;";
            }
            writer.AddAttribute("style", String.Format(style, MenuWidth));
            base.AddAttributesToRender(writer);
        }

        #endregion

        #region Private Properties & Methods

        private void CreateMenu()
        {
            switch (DataType)
            {
                case eDataType.DataBase:
                    if (ConnectionKey == "" || MenuTableBaseName == "")
                        return;
                    else
                    {
                        if (CheckTable(this.MenuLevel) == false)
                        { return; }
                        CreateMenuDS(this.ConnectionKey);
                        CreateMainItems();

                    }
                    break;
                case eDataType.XML:

                    break;
            }
        }

        private void CreateMainItems()
        {
            try
            {
                DataTable First = DS.Tables[0];
                for (int i = 0; i < First.Rows.Count; i++)
                {
                    j = 0;
                    string NavigateUrl = Convert.ToString(First.Rows[i]["NavigateUrl"]);
                    string Title = Convert.ToString(First.Rows[i]["Title"]);
                    string Value = Convert.ToString(First.Rows[i]["Value"]);
                    string IsChk = "";
                    if (this.HasCheckBox)
                        IsChk = Convert.IsDBNull(First.Rows[i][CheckBoxBoundField]) == true ? "" : Convert.ToBoolean(First.Rows[i][CheckBoxBoundField]) == false ? "" : "checked";
                    if (First.Rows[i].GetChildRows("Rel1").Length > 0)
                    {
                        switch (MenuType)
                        {
                            case eMenuType.Tree_Menu:
                                if (this.HasCheckBox)
                                    SB.Append("<a onmouseover='this.style.textDecoration=\"underline\";' onmouseout='this.style.textDecoration=\"none\";' href='" + NavigateUrl + "' target='" + _TargetStr + "' title='" + Title + "'><input type='checkbox' " + IsChk + " onclick='event.cancelBubble=true;var node=this.parentNode.nextSibling;if(node !=null){if(node.nodeName.toUpperCase()==\"DIV\"){SetChildCheckBoxsChecked(this,node);}};' />" + Value + "</a>");
                                else
                                    SB.Append("<a onmouseover='this.style.textDecoration=\"underline\";' onmouseout='this.style.textDecoration=\"none\";' href='" + NavigateUrl + "' target='" + _TargetStr + "' title='" + Title + "'>" + Value + "</a>");
                                SB.Append("<div>");
                                break;
                            default:
                                if (MenuPosition == eMenuPosition.Horizontal)
                                {
                                    if (this.HasCheckBox)
                                        SB.Append("<li class='imatm' style='width: " + this.MenuItemWidth + "px;'><a onmouseover='this.style.textDecoration=\"underline\";' onmouseout='this.style.textDecoration=\"none\";' href='" + NavigateUrl + "' target='" + _TargetStr + "' title='" + Title + "'><span class='imea imeam'><span></span></span><input type='checkbox' " + IsChk + " onclick='event.cancelBubble=true;var node=this.parentNode.nextSibling;if(node !=null){if(node.nodeName.toUpperCase()==\"DIV\"){SetChildCheckBoxsChecked1(this,node);}};' />" + Value + "</a>");
                                    else
                                        SB.Append("<li class='imatm' style='width: " + this.MenuItemWidth + "px;'><a onmouseover='this.style.textDecoration=\"underline\";' onmouseout='this.style.textDecoration=\"none\";' href='" + NavigateUrl + "' target='" + _TargetStr + "' title='" + Title + "'><span class='imea imeam'><span></span></span>" + Value + "</a>");
                                }
                                else
                                {
                                    if (this.HasCheckBox)
                                        SB.Append("<li class='imatm' style='width: 100%;'><a onmouseover='this.style.textDecoration=\"underline\";' onmouseout='this.style.textDecoration=\"none\";' href='" + NavigateUrl + "' target='" + _TargetStr + "' title='" + Title + "'><span class='imea imeam'><span></span></span><input type='checkbox' " + IsChk + " onclick='event.cancelBubble=true;var node=this.parentNode.nextSibling;if(node !=null){if(node.nodeName.toUpperCase()==\"DIV\"){SetChildCheckBoxsChecked1(this,node);}};' />" + Value + "</a>");
                                    else
                                        SB.Append("<li class='imatm' style='width: 100%;'><a onmouseover='this.style.textDecoration=\"underline\";' onmouseout='this.style.textDecoration=\"none\";' href='" + NavigateUrl + "' target='" + _TargetStr + "' title='" + Title + "'><span class='imea imeam'><span></span></span>" + Value + "</a>");
                                }
                                SB.Append("<div class='imsc'><div class='imsubc' style='width: " + this.MenuItemWidth + "px; top: 0px; left: 0px;'><ul style=''>");
                                break;
                        }
                        j++;
                        CreateSubItems(First.Rows[i], j);
                    }
                    else
                    {
                        switch (MenuType)
                        {
                            case eMenuType.Tree_Menu:
                                if (this.HasCheckBox)
                                    SB.Append("<a onmouseover='this.style.textDecoration=\"underline\";' onmouseout='this.style.textDecoration=\"none\";' href='" + NavigateUrl + "' target='" + _TargetStr + "' title='" + Title + "'><input type='checkbox' " + IsChk + " onclick='event.cancelBubble=true;var node=this.parentNode.nextSibling;if(node !=null){if(node.nodeName.toUpperCase()==\"DIV\"){SetChildCheckBoxsChecked(this,node);}};' />" + Value + "</a>");
                                else
                                    SB.Append("<a onmouseover='this.style.textDecoration=\"underline\";' onmouseout='this.style.textDecoration=\"none\";' href='" + NavigateUrl + "' target='" + _TargetStr + "' title='" + Title + "'>" + Value + "</a>");
                                break;
                            default:
                                if (MenuPosition == eMenuPosition.Horizontal)
                                {
                                    if (this.HasCheckBox)
                                        SB.Append("<li class='imatm' style='width: " + this.MenuItemWidth + "px;'><a class='' onmouseover='this.style.textDecoration=\"underline\";' onmouseout='this.style.textDecoration=\"none\";' href='" + NavigateUrl + "' target='" + _TargetStr + "' title='" + Title + "'><input type='checkbox' " + IsChk + " onclick='event.cancelBubble=true;var node=this.parentNode.nextSibling;if(node !=null){if(node.nodeName.toUpperCase()==\"DIV\"){SetChildCheckBoxsChecked1(this,node);}};' />" + Value + "</a></li>");
                                    else
                                        SB.Append("<li class='imatm' style='width: " + this.MenuItemWidth + "px;'><a class='' onmouseover='this.style.textDecoration=\"underline\";' onmouseout='this.style.textDecoration=\"none\";' href='" + NavigateUrl + "' target='" + _TargetStr + "' title='" + Title + "'>" + Value + "</a></li>");
                                }
                                else
                                {
                                    if (this.HasCheckBox)
                                        SB.Append("<li class='imatm' style='width: 100%;'><a class='' onmouseover='this.style.textDecoration=\"underline\";' onmouseout='this.style.textDecoration=\"none\";' href='" + NavigateUrl + "' target='" + _TargetStr + "' title='" + Title + "'><input type='checkbox' " + IsChk + " onclick='event.cancelBubble=true;var node=this.parentNode.nextSibling;if(node !=null){if(node.nodeName.toUpperCase()==\"DIV\"){SetChildCheckBoxsChecked1(this,node);}};' />" + Value + "</a></li>");
                                    else
                                        SB.Append("<li class='imatm' style='width: 100%;'><a class='' onmouseover='this.style.textDecoration=\"underline\";' onmouseout='this.style.textDecoration=\"none\";' href='" + NavigateUrl + "' target='" + _TargetStr + "' title='" + Title + "'>" + Value + "</a></li>");
                                }
                                break;
                        }
                    }
                }
                switch (MenuType)
                {
                    case eMenuType.Tree_Menu:
                        SB.Append("</div>");
                        break;
                    default:
                        SB.Append("</ul></div></div></li>");
                        break;
                }
            }
            catch (Exception ex)
            {
                SB.Append(ex.Message);
            }
        }

        private void CreateSubItems(DataRow Obj, int j)
        {
            DataRow[] drs = Obj.GetChildRows("Rel" + j);
            for (int i = 0; i < drs.Length; i++)
            {
                string NavigateUrl = Convert.ToString(drs[i]["NavigateUrl"]);
                string Title = Convert.ToString(drs[i]["Title"]);
                string Value = Convert.ToString(drs[i]["Value"]);
                string IsChk = "";
                if (this.HasCheckBox)
                    IsChk = Convert.IsDBNull(drs[i][CheckBoxBoundField]) == true ? "" : Convert.ToBoolean(drs[i][CheckBoxBoundField]) == false ? "" : "checked";
                if (drs[i].GetChildRows("Rel" + (j + 1)).Length > 0)
                {
                    switch (MenuType)
                    {
                        case eMenuType.Tree_Menu:
                            if (this.HasCheckBox)
                                SB.Append("<a onmouseover='this.style.textDecoration=\"underline\";' onmouseout='this.style.textDecoration=\"none\";' href='" + NavigateUrl + "' target='" + _TargetStr + "' title='" + Title + "'><input type='checkbox' " + IsChk + " onclick='event.cancelBubble=true;var node=this.parentNode.nextSibling;if(node !=null){if(node.nodeName.toUpperCase()==\"DIV\"){SetChildCheckBoxsChecked(this,node);}};' />" + Value + "</a>");
                            else
                                SB.Append("<a onmouseover='this.style.textDecoration=\"underline\";' onmouseout='this.style.textDecoration=\"none\";' href='" + NavigateUrl + "' target='" + _TargetStr + "' title='" + Title + "'>" + Value + "</a>");
                            SB.Append("<div>");
                            break;
                        default:
                            if (this.HasCheckBox)
                                SB.Append("<li><a onmouseover='this.style.textDecoration=\"underline\";' onmouseout='this.style.textDecoration=\"none\";' href='" + NavigateUrl + "' target='" + _TargetStr + "' title='" + Title + "'><span class='imea imeas'><span></span></span><input type='checkbox' " + IsChk + " onclick='event.cancelBubble=true;var node=this.parentNode.nextSibling;if(node !=null){if(node.nodeName.toUpperCase()==\"DIV\"){SetChildCheckBoxsChecked1(this,node);}};' />" + Value + "</a>");
                            else
                                SB.Append("<li><a onmouseover='this.style.textDecoration=\"underline\";' onmouseout='this.style.textDecoration=\"none\";' href='" + NavigateUrl + "' target='" + _TargetStr + "' title='" + Title + "'><span class='imea imeas'><span></span></span>" + Value + "</a>");
                            SB.Append("<div class='imsc'>");
                            SB.Append("<div class='imsubc' style='width: " + this.MenuItemWidth + "px;top: -19px;left:  " + (this.MenuItemWidth - 8) + "px;'>");
                            SB.Append("<ul style=''>");
                            break;
                    }
                    j++;
                    CreateSubItems(drs[i], j);
                }
                else
                {
                    switch (MenuType)
                    {
                        case eMenuType.Tree_Menu:
                            if (this.HasCheckBox)
                                SB.Append("<a onmouseover='this.style.textDecoration=\"underline\";' onmouseout='this.style.textDecoration=\"none\";' onmouseup='if(event.button==1){" + ClientScript + ";this.click();}'  href='" + NavigateUrl + "' target='" + _TargetStr + "' title='" + Title + "'><input type='checkbox' " + IsChk + " onclick='event.cancelBubble=true;var node=this.parentNode.nextSibling;if(node !=null){if(node.nodeName.toUpperCase()==\"DIV\"){SetChildCheckBoxsChecked(this,node);}};' />" + Value + "</a>");
                            else
                                SB.Append("<a onmouseover='this.style.textDecoration=\"underline\";' onmouseout='this.style.textDecoration=\"none\";' onmouseup='if(event.button==1){" + ClientScript + ";this.click();}'  href='" + NavigateUrl + "' target='" + _TargetStr + "' title='" + Title + "'>" + Value + "</a>");
                            break;
                        default:
                            if (this.HasCheckBox)
                                SB.Append("<li><a onmouseover='this.style.textDecoration=\"underline\";' onmouseout='this.style.textDecoration=\"none\";' onclick='" + ClientScript + "' href='" + NavigateUrl + "' target='" + _TargetStr + "' title='" + Title + "'><input type='checkbox' " + IsChk + " onclick='event.cancelBubble=true;var node=this.parentNode.nextSibling;if(node !=null){if(node.nodeName.toUpperCase()==\"DIV\"){SetChildCheckBoxsChecked1(this,node);}};' />" + Value + "</a></li>");
                            else
                                SB.Append("<li><a onmouseover='this.style.textDecoration=\"underline\";' onmouseout='this.style.textDecoration=\"none\";' onclick='" + ClientScript + "' href='" + NavigateUrl + "' target='" + _TargetStr + "' title='" + Title + "'>" + Value + "</a></li>");
                            break;
                    }
                }
            }
            switch (MenuType)
            {
                case eMenuType.Tree_Menu:
                    SB.Append("</div>");
                    break;
                default:
                    SB.Append("</ul></div></div></li>");
                    break;
            }
        }

        private void CreateMenuDS(string ConnectionKey)
        {
            try
            {
                string ConnStr = ConfigurationManager.ConnectionStrings[ConnectionKey].ConnectionString;
                SqlConnection Conn = new SqlConnection(ConnStr);
                SqlCommand Cmd = new SqlCommand();
                SqlDataAdapter SqlAdp = new SqlDataAdapter(Cmd);
                Cmd.Connection = Conn;
                for (int i = 1; i <= MenuLevel; i++)
                {
                    Cmd.CommandText += string.Format("select * from {0} order by Sequence;", MenuTableBaseName + i);
                }
                SqlAdp.Fill(DS);
                DataRelation DR;
                switch (MenuLevel)
                {
                    case 1:
                        break;
                    default:
                        for (int i = 1; i < MenuLevel; i++)
                        {
                            DR = new DataRelation("Rel" + i, DS.Tables[i - 1].Columns["Key"], DS.Tables[i].Columns["ParentKey"]);
                            DS.Relations.Add(DR);
                        }
                        break;
                }
            }
            catch (Exception ex)
            {

            }
        }

        private bool CheckTable(Byte MenuLevel)
        {
            try
            {
                bool result = true;
                string ConnStr = ConfigurationManager.ConnectionStrings[ConnectionKey].ConnectionString;
                SqlConnection Conn = new SqlConnection(ConnStr);
                SqlCommand Cmd = new SqlCommand();
                SqlDataAdapter SqlAdp = new SqlDataAdapter(Cmd);
                Cmd.Connection = Conn;
                DataSet TempDS = new DataSet();
                for (int i = 1; i <= MenuLevel; i++)
                    Cmd.CommandText += string.Format("select top 1 * from {0};", MenuTableBaseName + i);
                try
                {
                    SqlAdp.Fill(TempDS);
                }
                catch (SqlException ex)
                {
                    result = false;
                }
                catch (Exception ex)
                {
                    result = false;
                }
                return result;
            }
            catch (Exception ex)
            {
                return true;
            }
        }

        #endregion

    }

    public class MenuDesigner : CompositeControlDesigner
    {
        /// <summary>
        /// 覆寫基底GetDesignTimeHtml以改寫控制項在設計畫面的初始顯示
        /// </summary>
        public override string GetDesignTimeHtml()
        {
            Menu Mnu = (Menu)Component;
            StringBuilder SB = new StringBuilder(2000);
            string CSS = "";
            string HTML = "";
            string MainItemBgColor = Conversion.Hex(Mnu.MainItemBgColor.R).PadLeft(2, '0') + Conversion.Hex(Mnu.MainItemBgColor.G).PadLeft(2, '0') + Conversion.Hex(Mnu.MainItemBgColor.B).PadLeft(2, '0');
            string SubItemBgColor = Conversion.Hex(Mnu.SubItemBgColor.R).PadLeft(2, '0') + Conversion.Hex(Mnu.SubItemBgColor.G).PadLeft(2, '0') + Conversion.Hex(Mnu.SubItemBgColor.B).PadLeft(2, '0');
            string MainItemForeColor = Conversion.Hex(Mnu.MainItemForeColor.R).PadLeft(2, '0') + Conversion.Hex(Mnu.MainItemForeColor.G).PadLeft(2, '0') + Conversion.Hex(Mnu.MainItemForeColor.B).PadLeft(2, '0');
            string SubItemForeColor = Conversion.Hex(Mnu.SubItemForeColor.R).PadLeft(2, '0') + Conversion.Hex(Mnu.SubItemForeColor.G).PadLeft(2, '0') + Conversion.Hex(Mnu.SubItemForeColor.B).PadLeft(2, '0');
            if (Mnu.MenuType == eMenuType.Basic)
            {
                CSS = PublicFunc.GetResourcesInfo(APTemplate.Properties.Resources.ResourceManager.GetString("CSS"), "Basic");
                HTML = PublicFunc.GetResourcesInfo(APTemplate.Properties.Resources.ResourceManager.GetString("Html"), "Basic");
            }
            else if (Mnu.MenuType == eMenuType.Basic_Bounce_Arrows)
            {
                CSS = PublicFunc.GetResourcesInfo(APTemplate.Properties.Resources.ResourceManager.GetString("CSS"), "Basic_Bounce_Arrows");
                HTML = PublicFunc.GetResourcesInfo(APTemplate.Properties.Resources.ResourceManager.GetString("Html"), "Basic_Bounce_Arrows");
            }
            else if (Mnu.MenuType == eMenuType.Basic_Red_Highlight)
            {
                CSS = PublicFunc.GetResourcesInfo(APTemplate.Properties.Resources.ResourceManager.GetString("CSS"), "Basic_Red_Highlight");
                HTML = PublicFunc.GetResourcesInfo(APTemplate.Properties.Resources.ResourceManager.GetString("Html"), "Basic_Red_Highlight");
            }
            else if (Mnu.MenuType == eMenuType.Blue_Tones)
            {
                CSS = PublicFunc.GetResourcesInfo(APTemplate.Properties.Resources.ResourceManager.GetString("CSS"), "Blue_Tones");
                HTML = PublicFunc.GetResourcesInfo(APTemplate.Properties.Resources.ResourceManager.GetString("Html"), "Blue_Tones");
            }
            else if (Mnu.MenuType == eMenuType.Drop_Shadow)
            {
                CSS = PublicFunc.GetResourcesInfo(APTemplate.Properties.Resources.ResourceManager.GetString("CSS"), "Drop_Shadow");
                HTML = PublicFunc.GetResourcesInfo(APTemplate.Properties.Resources.ResourceManager.GetString("Html"), "Drop_Shadow");
            }
            else if (Mnu.MenuType == eMenuType.Gradient_Background)
            {
                CSS = PublicFunc.GetResourcesInfo(APTemplate.Properties.Resources.ResourceManager.GetString("CSS"), "Gradient_Background");
                HTML = PublicFunc.GetResourcesInfo(APTemplate.Properties.Resources.ResourceManager.GetString("Html"), "Gradient_Background");
            }
            else if (Mnu.MenuType == eMenuType.Gray_Scale)
            {
                CSS = PublicFunc.GetResourcesInfo(APTemplate.Properties.Resources.ResourceManager.GetString("CSS"), "Gray_Scale");
                HTML = PublicFunc.GetResourcesInfo(APTemplate.Properties.Resources.ResourceManager.GetString("Html"), "Gray_Scale");
            }
            else if (Mnu.MenuType == eMenuType.Stretch_Buttons_Tan)
            {
                CSS = PublicFunc.GetResourcesInfo(APTemplate.Properties.Resources.ResourceManager.GetString("CSS"), "Stretch_Buttons_Tan");
                HTML = PublicFunc.GetResourcesInfo(APTemplate.Properties.Resources.ResourceManager.GetString("Html"), "Stretch_Buttons_Tan");
            }
            else if (Mnu.MenuType == eMenuType.Thick_Borders)
            {
                CSS = PublicFunc.GetResourcesInfo(APTemplate.Properties.Resources.ResourceManager.GetString("CSS"), "Thick_Borders");
                HTML = PublicFunc.GetResourcesInfo(APTemplate.Properties.Resources.ResourceManager.GetString("Html"), "Thick_Borders");
            }
            else if (Mnu.MenuType == eMenuType.Tree_Menu)
            {
                HTML = PublicFunc.GetResourcesInfo(APTemplate.Properties.Resources.ResourceManager.GetString("Html"), "Tree_Menu");
            }

            //參數取代
            int SearchIdx = 0;
            string SearchStr = "<#UrlPath#>/";
            while (CSS.IndexOf(SearchStr, SearchIdx) > -1)
            {
                int StartIdx = CSS.IndexOf(SearchStr, SearchIdx) + SearchStr.Length;
                int EndIdx = CSS.IndexOf(");", StartIdx);
                if (EndIdx - StartIdx >= 5)
                {
                    string ImgName = CSS.Substring(StartIdx, EndIdx - StartIdx);
                    string ImgUrl = Mnu.Page != null ? Mnu.Page.ClientScript.GetWebResourceUrl(Mnu.GetType(), "APTemplate.Resources." + ImgName) : "";
                    CSS = CSS.Replace(SearchStr + ImgName, ImgUrl);
                    SearchIdx = EndIdx + ");".Length;
                }
            }
            CSS = CSS.Replace("<#MainItem_BgColor#>", MainItemBgColor);
            SearchIdx = 0;
            while (HTML.IndexOf(SearchStr, SearchIdx) > -1)
            {
                int StartIdx = HTML.IndexOf(SearchStr, SearchIdx) + SearchStr.Length;
                int EndIdx = HTML.IndexOf(");", StartIdx);
                if (EndIdx - StartIdx >= 5)
                {
                    string ImgName = HTML.Substring(StartIdx, EndIdx - StartIdx);
                    string ImgUrl = Mnu.Page != null ? Mnu.Page.ClientScript.GetWebResourceUrl(Mnu.GetType(), "APTemplate.Resources." + ImgName) : "";
                    HTML = HTML.Replace(SearchStr + ImgName, ImgUrl);
                    SearchIdx = EndIdx + ");".Length;
                }
            }
            HTML = HTML.Replace("<#MainItem_BgColor#>", MainItemBgColor);
            HTML = HTML.Replace("<#SubItem_BgColor#>", SubItemBgColor);
            HTML = HTML.Replace("<#MainItem_ForeColor#>", MainItemForeColor);
            HTML = HTML.Replace("<#SubItem_ForeColor#>", SubItemForeColor);
            if (Mnu.MenuPosition == eMenuPosition.Horizontal)
            { HTML = HTML.Replace("<#MenuWidth#>", Mnu.MenuWidth.ToString()); }
            else
            { HTML = HTML.Replace("<#MenuWidth#>", (Mnu.MenuItemWidth + 30).ToString()); }
            HTML = HTML.Replace("<#MenuItemWidth#>", Mnu.MenuItemWidth.ToString());
            //參數取代
            SB.Append(@"<html>");
            SB.Append("<head>");
            SB.Append("<title>Template</title>");
            SB.Append("<style type='text/css'>");
            SB.Append(CSS);
            SB.Append("</style>");
            SB.Append("</head>");
            SB.Append("<body>");
            SB.Append(HTML);
            SB.Append("</body>");
            SB.Append("</html>");
            return SB.ToString();
        }
    }
}