using System;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.UI.Design.WebControls;
using System.Drawing;
using System.Drawing.Design;
using System.Resources;
using System.Reflection;
using AjaxPro;
using System.IO;

namespace APTemplate
{
    /// <summary>
    /// 自定義的工具列控制項
    /// </summary>
    [ToolboxData("<{0}:ToolBar runat=server></{0}:ToolBar>")]
    [ToolboxBitmap(typeof(ToolBar), "Resources.Control_ToolBar.bmp")]
    [Designer(typeof(ToolBarDesigner))]
    public class ToolBar : CompositeControl, IPostBackEventHandler
    {
        /// <summary>
        /// 建立工具列控制項所需的table
        /// </summary>
        protected Table Table1 = new Table();
        /// <summary>
        /// 建立工具列控制項所需的row
        /// </summary>
        protected TableRow TableRow1 = new TableRow();

        /// <summary>
        /// 工具列內圖片按鈕(工具內建的圖片控制項)的寬度
        /// </summary>
        protected Unit _ButtonWidth = Unit.Pixel(20);
        /// <summary>
        /// 工具列內圖片按鈕(工具內建的圖片控制項)的高度
        /// </summary>
        protected Unit _ButtonHeight = Unit.Pixel(20);
        /// <summary>
        /// 工具列內所有加入的項目的集合(label,textbox,button)
        /// </summary>
        protected List<ToolBarItem> _Items = new List<ToolBarItem>();
        /// <summary>
        /// 工具列內所有加入的CustomControl的集合
        /// </summary>
        protected List<CompositeControl> _CustomControls = new List<CompositeControl>();
        /// <summary>
        /// 工具列內所有加入的圖片按鈕的集合
        /// </summary>
        protected List<System.Web.UI.WebControls.Image> _ControlImages = new List<System.Web.UI.WebControls.Image>();
        /// <summary>
        /// 工具列內所有加入的label的集合
        /// </summary>
        protected List<Label> _Labels = new List<Label>();
        /// <summary>
        /// 工具列內所有加入的textbox的集合
        /// </summary>
        protected List<TextBox> _TextBoxs = new List<TextBox>();
        /// <summary>
        /// 建立工具列控制項所需的cell集合
        /// </summary>
        protected List<TableCell> _TableCells = new List<TableCell>();
        /// <summary>
        /// 記錄目前所選按鈕的資訊
        /// </summary>
        protected HtmlInputHidden Hideen_ActiveBtn = new HtmlInputHidden();
        /// <summary>
        /// 點擊工具列內圖片按鈕時的事件
        /// </summary>
        public event EventHandler Click;
        /// <summary>
        /// 點擊工具列內Button_ConfirmYesNo按鈕時的事件
        /// </summary>
        public event EventHandler Button_ConfirmYesNoClick;
        /// <summary>
        /// 點擊工具列內Button_Normal時的事件
        /// </summary>
        public event EventHandler Button_NormalClick;

        #region Public Properties & Methods

        /// <summary>
        ///工具列按鈕的寬度。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("工具列按鈕的寬度。")]
        public Unit ButtonWidth
        {
            get { return _ButtonWidth; }
            set { _ButtonWidth = value; }
        }

        /// <summary>
        ///工具列按鈕的高度。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("工具列按鈕的高度。")]
        public Unit ButtonHeight
        {
            get { return _ButtonHeight; }
            set { _ButtonHeight = value; }
        }

        /// <summary>
        /// 工具列的所有按鈕。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("工具列的所有按鈕。")]
        [PersistenceMode(PersistenceMode.InnerProperty), DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [NotifyParentProperty(true)]
        [Editor(typeof(ToolbarCollectionEditor), typeof(UITypeEditor))]
        public List<ToolBarItem> Items
        {
            get
            {
                return _Items;
            }
        }

        /// <summary>
        /// 工具列的所有自訂控制項(必須是繼承自TextBoxBase類)。
        /// </summary>
        [Category("自訂"),
         Description("工具列的所有自訂控制項(必須是繼承自TextBoxBase類)。")]
        [PersistenceMode(PersistenceMode.InnerProperty), DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [NotifyParentProperty(true)]
        [Editor(typeof(CustomControlCollectionEditor), typeof(UITypeEditor))]
        public List<CompositeControl> CustomControls
        {
            get
            {
                return _CustomControls;
            }
        }

        #region IPostBackEventHandler 成員

        public void RaisePostBackEvent(string eventArgument)
        {
            //int i = int.Parse(eventArgument);	
            //OnClick((ToolBarButton)Items[i]);
        }

        #endregion

        #endregion

        #region Protected Properties & Methods

        protected override void OnInit(EventArgs e)
        {
            if (Page.IsPostBack)
            {
                for (int i = 0; i < _Items.Count; i++)
                {
                    if (_Items[i].GetType() == typeof(ToolBarTextBox))
                    {
                        ((ToolBarTextBox)_Items[i]).ID = ((ToolBarTextBox)_Items[i]).ID == "" ? "TextBox" + i : ((ToolBarTextBox)_Items[i]).ID;
                        ((ToolBarTextBox)_Items[i]).Text = Context.Request.Form[this.ID + "$" + ((ToolBarTextBox)_Items[i]).ID] == null ? "" : Context.Request.Form[this.ID + "$" + ((ToolBarTextBox)_Items[i]).ID].ToString();
                    }
                }
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            RegisterAjaxType();
            if (Page.IsPostBack && Page.Request.Form["__EVENTTARGET"].IndexOf(this.ID) != -1)
            {
                if (Page.Request.Form["__EVENTARGUMENT"] != "")
                {
                    Int32 result;
                    if (Int32.TryParse(Page.Request.Form["__EVENTARGUMENT"], out result))
                    {
                        int i = int.Parse(Page.Request.Form["__EVENTARGUMENT"]);
                        OnClick((ToolBarButton)Items[i]);
                    }
                    else if (Page.Request.Form["__EVENTARGUMENT"].ToString().StartsWith("Button_ConfirmYesNo"))
                    {
                        int i = int.Parse((Page.Request.Form["__EVENTARGUMENT"].ToString().Split(new Char[] { '_' }))[2]);
                        OnButton_ConfirmYesNoClick((Button_ConfirmYesNo)CustomControls[i]);
                    }
                    else if (Page.Request.Form["__EVENTARGUMENT"].ToString().StartsWith("Button_Normal"))
                    {
                        int i = int.Parse((Page.Request.Form["__EVENTARGUMENT"].ToString().Split(new Char[] { '_' }))[2]);
                        OnButton_NormalClick((Button_Normal)CustomControls[i]);
                    }
                }
            }
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            RegisterJavaScript.RegisterContolIncludeScript(Page);
        }

        protected override void CreateChildControls()
        {
            EnsureChildControls();

            //Table1
            Table1.ID = "Table1";
            Table1.BorderWidth = Unit.Pixel(1);
            Table1.BorderStyle = BorderStyle.Outset;
            Table1.CellPadding = 3;
            Table1.CellSpacing = 0;
            Table1.Width = _Items.Count < 1 && _CustomControls.Count < 1 ? ButtonWidth : Table1.Width;

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
            //TableRow1
            TableRow1.ID = "TableRow1";

            //根據UI加入的ToolButton數動態加入TableCell及Image
            CreateButtons();

            Table1.Controls.Add(TableRow1);

            this.Controls.Add(Table1);
        }

        protected override void RenderContents(HtmlTextWriter writer)
        {
            Table1.RenderControl(writer);
        }

        /// <summary>
        /// 觸發Click事件
        /// </summary>
        protected virtual void OnClick(ToolBarButton ToolBarButton)
        {
            if (Click != null)
                Click(ToolBarButton, new EventArgs());
        }

        /// <summary>
        /// 觸發Button_ConfirmYesNoClick事件
        /// </summary>
        protected virtual void OnButton_ConfirmYesNoClick(Button_ConfirmYesNo Button_ConfirmYesNo)
        {
            if (Button_ConfirmYesNoClick != null)
                Button_ConfirmYesNoClick(Button_ConfirmYesNo, new EventArgs());
        }

        /// <summary>
        /// 觸發Button_NormalClick事件
        /// </summary>
        protected virtual void OnButton_NormalClick(Button_Normal Button_Normal)
        {
            if (Button_NormalClick != null)
                Button_NormalClick(Button_Normal, new EventArgs());
        }

        #endregion

        #region Private Properties & Methods

        private void CreateButtons()
        {
            _TableCells.Clear();
            _ControlImages.Clear();
            TableRow1.Cells.Clear();

            if (_Items.Count > 0)
            {
                int i = 0;
                Hideen_ActiveBtn.ID = "HActiveBtn";
                string ActiveBtnID = null;
                if (!this.DesignMode)
                    ActiveBtnID = Context.Request.Form[this.UniqueID + "$" + Hideen_ActiveBtn.UniqueID] == null ? null : Context.Request.Form[this.UniqueID + "$" + Hideen_ActiveBtn.UniqueID];
                foreach (ToolBarItem Item in _Items)
                {
                    TableCell Cell = new TableCell();
                    Cell.ID = "TableCell" + i;
                    _TableCells.Add(Cell);
                    if (Item.GetType() == typeof(ToolBarButton))
                    {
                        ToolBarButton Btn = (ToolBarButton)Item;
                        System.Web.UI.WebControls.Image Image = new System.Web.UI.WebControls.Image();
                        Image.ID = "Image" + i;
                        if (Btn.IsSeperator == false)
                        {
                            Image.Width = ButtonWidth;
                            Image.Height = ButtonHeight;
                            Image.ImageUrl = ActiveBtnID == Image.ID && ActiveBtnID != null && Btn.ActiveImageUrl != "" ? Btn.ActiveImageUrl : Btn.ImageUrl;
                            Image.ToolTip = Btn.ToolTip;
                            Image.Enabled = Btn.Enabled;
                            Image.Visible = Btn.Visible;
                            Image.BorderWidth = Unit.Pixel(1);
                            if (Btn.PostBackUrl == "")
                            {
                                Image.Attributes["onclick"] += Btn.OnClientClick + ";";
                                Image.Attributes["onclick"] += "document.getElementById('" + this.ClientID + "_" + Hideen_ActiveBtn.ClientID + "').value='" + Image.ID + "';";
                                Image.Attributes["onclick"] += Btn.CauseValidation ? "if(typeof(Page_Validators) != 'undefined'){Page_ClientValidate();}" : "";
                                Image.Attributes["onclick"] += Page.ClientScript.GetPostBackEventReference(this, i.ToString()) + ";";
                            }
                            else
                            {
                                PostBackOptions PBO = new PostBackOptions(this);
                                PBO.ActionUrl = Btn.PostBackUrl;
                                Image.Attributes["onclick"] += Btn.OnClientClick + ";";
                                Image.Attributes["onclick"] += "document.getElementById('" + this.ClientID + "_" + Hideen_ActiveBtn.ClientID + "').value='" + Image.ID + "';";
                                Image.Attributes["onclick"] += Btn.CauseValidation ? "if(typeof(Page_Validators) != 'undefined'){Page_ClientValidate();}" : "";
                                Image.Attributes["onclick"] += Page.ClientScript.GetPostBackEventReference(PBO) + ";";
                            }

                            Image.Attributes["onmouseover"] = Btn.OverImageUrl != "" ?
                            "this.style.cursor='hand';this.style.borderStyle='inset';document.getElementById('" + this.ClientID + "_" + Image.ClientID + "').src='" + base.ResolveClientUrl(Btn.OverImageUrl) + "';"
                            : "this.style.cursor='hand';this.style.borderStyle='inset';";
                            if (ActiveBtnID != Image.ID)
                            {
                                Image.Attributes["onmouseout"] = Btn.ImageUrl != "" ?
                                "this.style.cursor='default';this.style.borderStyle='solid';document.getElementById('" + this.ClientID + "_" + Image.ClientID + "').src='" + base.ResolveClientUrl(Btn.ImageUrl) + "';"
                                : "this.style.cursor='default';this.style.borderStyle='solid';";
                            }
                            else
                            {
                                Image.Attributes["onmouseout"] = Btn.ActiveImageUrl != "" ?
                                "this.style.cursor='default';this.style.borderStyle='solid';document.getElementById('" + this.ClientID + "_" + Image.ClientID + "').src='" + base.ResolveClientUrl(Btn.ActiveImageUrl) + "';"
                                : "this.style.cursor='default';this.style.borderStyle='solid';document.getElementById('" + this.ClientID + "_" + Image.ClientID + "').src='" + base.ResolveClientUrl(Btn.ImageUrl) + "';";
                            }

                        }
                        else
                        {
                            Image.ImageUrl = Btn.ImageUrl;
                            Image.Enabled = Btn.Enabled;
                            Image.Visible = Btn.Visible;
                            Image.Attributes["onclick"] = "javascript:return false;";
                        }
                        _ControlImages.Add(Image);
                    }
                    else if (Item.GetType() == typeof(ToolBarLabel))
                    {
                        ToolBarLabel Lbl = (ToolBarLabel)Item;
                        Label Label = new Label();
                        Label.ID = "Label" + i;
                        Label.Text = Lbl.Text;
                        Label.Enabled = Lbl.Enabled;
                        Label.Visible = Lbl.Visible;
                        _Labels.Add(Label);
                    }
                    else if (Item.GetType() == typeof(ToolBarTextBox))
                    {
                        ToolBarTextBox Txt = (ToolBarTextBox)Item;
                        TextBox TextBox = new TextBox();
                        TextBox.ID = Txt.ID == "" ? "TextBox" + i : Txt.ID;
                        TextBox.Text = Txt.Text;
                        TextBox.Width = Txt.Width;
                        TextBox.Enabled = Txt.Enabled;
                        TextBox.Visible = Txt.Visible;
                        _TextBoxs.Add(TextBox);
                    }
                    i++;
                }
                _TableCells[_TableCells.Count - 1].Controls.Add(Hideen_ActiveBtn);
            }

            //將每個Image加入每個TableCell
            for (int i = 0; i < _TableCells.Count; i++)
            {
                if (_Items[i].GetType() == typeof(ToolBarButton))
                {
                    _TableCells[i].Controls.Add(_ControlImages[0]);
                    _ControlImages.RemoveAt(0);
                }
                else if (_Items[i].GetType() == typeof(ToolBarLabel))
                {
                    _TableCells[i].Controls.Add(_Labels[0]);
                    _Labels.RemoveAt(0);
                }
                else if (_Items[i].GetType() == typeof(ToolBarTextBox))
                {
                    _TableCells[i].Controls.Add(_TextBoxs[0]);
                    _TextBoxs.RemoveAt(0);
                }
            }

            //將每個TableCell加入TableRow1
            for (int i = 0; i < _TableCells.Count; i++)
            {
                TableRow1.Controls.Add(_TableCells[i]);
            }

            //將每個CustomControl加入至TableCell
            int idx = 0;
            for (int i = _TableCells.Count; i < _TableCells.Count + _CustomControls.Count; i++)
            {
                TableCell Cell = new TableCell();
                Cell.ID = "TableCell" + i;
                if (_CustomControls[i - _TableCells.Count].GetType() == typeof(CalendarRange))
                {
                    CalendarRange CalendarRange = (CalendarRange)_CustomControls[i - _TableCells.Count];
                    Cell.Controls.Add(CalendarRange);
                }
                if (_CustomControls[i - _TableCells.Count].GetType() == typeof(DecimalRange))
                {
                    DecimalRange DecimalRange = (DecimalRange)_CustomControls[i - _TableCells.Count];
                    Cell.Controls.Add(DecimalRange);
                }
                if (_CustomControls[i - _TableCells.Count].GetType() == typeof(DropDownList_Date))
                {
                    DropDownList_Date DropDownList_Date = (DropDownList_Date)_CustomControls[i - _TableCells.Count];
                    Cell.Controls.Add(DropDownList_Date);
                }
                if (_CustomControls[i - _TableCells.Count].GetType() == typeof(DropDownList_Multiple))
                {
                    DropDownList_Multiple DropDownList_Multiple = (DropDownList_Multiple)_CustomControls[i - _TableCells.Count];
                    Cell.Controls.Add(DropDownList_Multiple);
                }
                if (_CustomControls[i - _TableCells.Count].GetType() == typeof(TextBox_Normal))
                {
                    TextBox_Normal TextBox_Normal = (TextBox_Normal)_CustomControls[i - _TableCells.Count];
                    Cell.Controls.Add(TextBox_Normal);
                }
                if (_CustomControls[i - _TableCells.Count].GetType() == typeof(Email))
                {
                    Email Email = (Email)_CustomControls[i - _TableCells.Count];
                    Cell.Controls.Add(Email);
                }
                if (_CustomControls[i - _TableCells.Count].GetType() == typeof(Identity))
                {
                    Identity Identity = (Identity)_CustomControls[i - _TableCells.Count];
                    Cell.Controls.Add(Identity);
                }
                if (_CustomControls[i - _TableCells.Count].GetType() == typeof(ListBoxToListBox))
                {
                    ListBoxToListBox ListBoxToListBox = (ListBoxToListBox)_CustomControls[i - _TableCells.Count];
                    Cell.Controls.Add(ListBoxToListBox);
                }
                if (_CustomControls[i - _TableCells.Count].GetType() == typeof(Number))
                {
                    Number Number = (Number)_CustomControls[i - _TableCells.Count];
                    Cell.Controls.Add(Number);
                }
                if (_CustomControls[i - _TableCells.Count].GetType() == typeof(Number_Decimal))
                {
                    Number_Decimal Number_Decimal = (Number_Decimal)_CustomControls[i - _TableCells.Count];
                    Cell.Controls.Add(Number_Decimal);
                }
                if (_CustomControls[i - _TableCells.Count].GetType() == typeof(NumberRange))
                {
                    NumberRange NumberRange = (NumberRange)_CustomControls[i - _TableCells.Count];
                    Cell.Controls.Add(NumberRange);
                }
                if (_CustomControls[i - _TableCells.Count].GetType() == typeof(PopupCalendar))
                {
                    PopupCalendar PopupCalendar = (PopupCalendar)_CustomControls[i - _TableCells.Count];
                    Cell.Controls.Add(PopupCalendar);
                }
                if (_CustomControls[i - _TableCells.Count].GetType() == typeof(TextBox_PopupWindow))
                {
                    TextBox_PopupWindow TextBox_PopupWindow = (TextBox_PopupWindow)_CustomControls[i - _TableCells.Count];
                    Cell.Controls.Add(TextBox_PopupWindow);
                }
                if (_CustomControls[i - _TableCells.Count].GetType() == typeof(Button_ConfirmYesNo))
                {
                    Button_ConfirmYesNo Button_ConfirmYesNo = (Button_ConfirmYesNo)_CustomControls[i - _TableCells.Count];
                    Cell.Controls.Add(Button_ConfirmYesNo);
                    if (Button_ConfirmYesNo.PostBackUrl == "")
                    {
                        //Button_ConfirmYesNo.Attributes["onclick"] = Page.ClientScript.GetPostBackEventReference(this, Button_ConfirmYesNo.ButtonID +"_" + idx.ToString()) + ";return false;";
                        Button_ConfirmYesNo.Attributes["onclick"] = Page.ClientScript.GetPostBackEventReference(this, "Button_ConfirmYesNo_" + idx.ToString()) + ";return false;";
                    }
                }
                if (_CustomControls[i - _TableCells.Count].GetType() == typeof(Button_Normal))
                {
                    Button_Normal Button_Normal = (Button_Normal)_CustomControls[i - _TableCells.Count];
                    Cell.Controls.Add(Button_Normal);
                    if (Button_Normal.PostBackUrl == "")
                    {
                        Button_Normal.Attributes["onclick"] = Page.ClientScript.GetPostBackEventReference(this, "Button_Normal_" + idx.ToString()) + ";return false;";
                    }
                }
                if (_CustomControls[i - _TableCells.Count].GetType() == typeof(Button_PopupWindow))
                {
                    Button_PopupWindow Button_PopupWindow = (Button_PopupWindow)_CustomControls[i - _TableCells.Count];
                    Cell.Controls.Add(Button_PopupWindow);
                }
                TableRow1.Controls.Add(Cell);
                idx++;
            }
        }

        private void RegisterAjaxType()
        {
            for (int i = 0; i < _CustomControls.Count; i++)
            {
                if (_CustomControls[i].GetType() == typeof(DropDownList_Multiple))
                { AjaxPro.Utility.RegisterTypeForAjax(typeof(APTemplate.DropDownList_Multiple)); }
                else if (_CustomControls[i].GetType() == typeof(DropDownList_Date))
                { AjaxPro.Utility.RegisterTypeForAjax(typeof(APTemplate.DropDownList_Date)); }
                else if (_CustomControls[i].GetType() == typeof(Identity))
                { AjaxPro.Utility.RegisterTypeForAjax(typeof(APTemplate.Identity)); }
                else if (_CustomControls[i].GetType() == typeof(ListBoxToListBox))
                { AjaxPro.Utility.RegisterTypeForAjax(typeof(APTemplate.ListBoxToListBox)); }
            }
        }

        #endregion

    }

    public class ToolBarDesigner : CompositeControlDesigner
    {
        #region Public Properties & Methods

        /// <summary>
        /// 覆寫基底GetDesignTimeHtml以改寫控制項在設計畫面的初始顯示
        /// </summary>
        public override string GetDesignTimeHtml()
        {
            ToolBar ToolBar = (ToolBar)Component;
            int BtnNum = ToolBar.Items.Count;
            if (BtnNum == 0)
            {
                string imgSrc1 = ToolBar.Page.ClientScript.GetWebResourceUrl(typeof(ToolBar), "APTemplate.Resources.scrollbutton_down.gif");
                string imgSrc2 = ToolBar.Page.ClientScript.GetWebResourceUrl(typeof(ToolBar), "APTemplate.Resources.toolbarempty.gif");
                string HtmlCode = @"<table border='0' cellpadding='0' cellspacing='0' style='display:inline-block;'>" +
                                                     "	<tr>" +
                                                     "		<td><img src='" + imgSrc1 + "' border='0' /></td>" +
                                                     "	</tr>" +
                                                     "	<tr>" +
                                                    "		<td><img src='" + imgSrc2 + "' border='0' /></td>" +
                                                     "	</tr>" +
                                                     "</table>";
                return HtmlCode;
            }
            return base.GetDesignTimeHtml();
        }

        #endregion

    }
}