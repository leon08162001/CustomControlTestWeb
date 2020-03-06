using System;
using System.Configuration;
using System.ComponentModel;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Drawing;
using AjaxPro;
using System.Drawing.Design;
using System.Web.Caching;

namespace APTemplate
{
    /// <summary>
    /// 自定義的左右清單控制項
    /// </summary>
    [ToolboxData("<{0}:ListBoxToListBox runat=server></{0}:ListBoxToListBox>")]
    [ToolboxBitmap(typeof(ListBox))]
    [DefaultProperty("FirstListItems")]
    [ParseChildren(true)]
    //[ValidationProperty("SecondListItems")]
    public class ListBoxToListBox : CompositeControl, INamingContainer
    {
        /// <summary>
        /// 建立ListBoxToListBox控制項所需的table
        /// </summary>
        protected Table Table1 = new Table();
        /// <summary>
        /// 建立ListBoxToListBox控制項所需的row
        /// </summary>
        protected TableRow TableRow1 = new TableRow();
        /// <summary>
        /// 建立ListBoxToListBox控制項所需的cell
        /// </summary>
        protected TableCell TableCell1 = new TableCell();
        /// <summary>
        /// 建立ListBoxToListBox控制項所需的cell
        /// </summary>
        protected TableCell TableCell2 = new TableCell();
        /// <summary>
        /// 建立ListBoxToListBox控制項所需的cell
        /// </summary>
        protected TableCell TableCell3 = new TableCell();
        /// <summary>
        /// 建立ListBoxToListBox控制項的加入右邊清單按鈕
        /// </summary>
        protected System.Web.UI.WebControls.Image ImageButton1 = new System.Web.UI.WebControls.Image();
        /// <summary>
        /// 建立ListBoxToListBox控制項的加入左邊清單按鈕
        /// </summary>
        protected System.Web.UI.WebControls.Image ImageButton2 = new System.Web.UI.WebControls.Image();
        /// <summary>
        /// 建立ListBoxToListBox控制項的全部加入右邊清單按鈕
        /// </summary>
        protected System.Web.UI.WebControls.Image ImageButton3 = new System.Web.UI.WebControls.Image();
        /// <summary>
        /// 建立ListBoxToListBox控制項的全部加入左邊清單按鈕
        /// </summary>
        protected System.Web.UI.WebControls.Image ImageButton4 = new System.Web.UI.WebControls.Image();
        /// <summary>
        /// 建立ListBoxToListBox控制項的左邊清單
        /// </summary>
        protected ListBox select1 = new ListBox();
        /// <summary>
        /// 建立ListBoxToListBox控制項的右邊清單
        /// </summary>
        protected ListBox select2 = new ListBox();
        /// <summary>
        /// 建立ListBoxToListBox控制項的左邊清單標題
        /// </summary>
        protected Label LeftTilteLabel = new Label();
        /// <summary>
        /// 建立ListBoxToListBox控制項的右邊清單標題
        /// </summary>
        protected Label RightTilteLabel = new Label();

        protected string _LeftTitleText = "未選擇清單";
        protected string _RightTitleText = "已選擇清單";

        #region Public Properties & Methods

        public void RemoveSession()
        {
            if (HttpContext.Current != null)
            {
                HttpContext.Current.Session.Remove(this.UniqueID);
            }
        }

        public void SaveSession()
        {
            if (HttpContext.Current != null)
            {
                HttpContext.Current.Session[this.UniqueID] = this;
            }
        }

        [AjaxPro.AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
        public Boolean AddListOneToList(string PositionType, int Index, string ControlID)
        {
            try
            {
                ListBoxToListBox obj = (ListBoxToListBox)HttpContext.Current.Session[ControlID];
                if (PositionType.ToLower() != "left" && PositionType.ToLower() != "right")
                {
                    return false;
                }

                if (PositionType.ToLower() == "left")
                {
                    obj.SecondListBox.Items.Add(obj.FirstListBox.Items[Index]);
                    obj.FirstListBox.Items.RemoveAt(Index);
                }

                if (PositionType.ToLower() == "right")
                {
                    obj.FirstListBox.Items.Add(obj.SecondListBox.Items[Index]);
                    obj.SecondListBox.Items.RemoveAt(Index);
                }
                HttpContext.Current.Session[this.UniqueID] = obj;
                HttpContext.Current.Cache.Insert(this.UniqueID + "_firstList", obj.FirstListBox.Items, null, DateTime.Now.AddMinutes(20), Cache.NoSlidingExpiration);
                HttpContext.Current.Cache.Insert(this.UniqueID + "_firstList", obj.SecondListBox.Items, null, DateTime.Now.AddMinutes(20), Cache.NoSlidingExpiration);
                return true;
            }
            catch (Exception Ex)
            {
                return false;
            }
        }

        [AjaxPro.AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
        public Boolean AddListAllToList(string PositionType, string ControlID)
        {
            try
            {
                ListBoxToListBox obj = (ListBoxToListBox)HttpContext.Current.Session[ControlID];
                if (PositionType.ToLower() != "left" && PositionType.ToLower() != "right")
                {
                    return false;
                }

                if (PositionType.ToLower() == "left")
                {
                    for (int i = 0; i < obj.FirstListBox.Items.Count; i++)
                    {
                        obj.SecondListBox.Items.Add(obj.FirstListBox.Items[i]);
                    }
                    obj.FirstListBox.Items.Clear();
                }
                if (PositionType.ToLower() == "right")
                {
                    for (int i = 0; i < obj.SecondListBox.Items.Count; i++)
                    {
                        obj.FirstListBox.Items.Add(obj.SecondListBox.Items[i]);
                    }
                    obj.SecondListBox.Items.Clear();
                }
                HttpContext.Current.Session[this.UniqueID] = obj;
                HttpContext.Current.Cache.Insert(this.UniqueID + "_firstList", obj.FirstListBox.Items, null, DateTime.Now.AddMinutes(20), Cache.NoSlidingExpiration);
                HttpContext.Current.Cache.Insert(this.UniqueID + "_firstList", obj.SecondListBox.Items, null, DateTime.Now.AddMinutes(20), Cache.NoSlidingExpiration);
                return true;
            }
            catch (Exception Ex)
            {
                return false;
            }
        }

        [Browsable(false)]
        public ListBox FirstListBox
        {
            get
            {
                if (HttpContext.Current.Session[this.UniqueID] == null)
                    return select1;
                else
                    return ((ListBoxToListBox)HttpContext.Current.Session[this.UniqueID]).select1;
            }
        }

        [Browsable(false)]
        public ListBox SecondListBox
        {
            get
            {
                if (HttpContext.Current.Session[this.UniqueID] == null)
                    return select2;
                else
                    return ((ListBoxToListBox)HttpContext.Current.Session[this.UniqueID]).select2;
            }
        }
        /// <summary>
        ///左邊清單方塊的標題文字。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("左邊清單方塊的標題文字。")]
        public string LeftTitleText
        {
            get
            {
                return _LeftTitleText;
            }
            set
            {
                _LeftTitleText = value;
            }
        }
        /// <summary>
        ///右邊清單方塊的標題文字。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("右邊清單方塊的標題文字。")]
        public string RightTitleText
        {
            get
            {
                return _RightTitleText;
            }
            set
            {
                _RightTitleText = value;
            }
        }

        /// <summary>
        ///清單方塊的文字顏色。。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("清單方塊的文字顏色。")]
        [Editor(typeof(ColorEditor), typeof(UITypeEditor))]
        public Color ListForeColor
        {
            get
            {
                if (ViewState["ListForeColor"] == null)
                    return Color.Sienna;
                else
                    return (Color)ViewState["ListForeColor"];
            }
            set { ViewState["ListForeColor"] = value; }
        }

        /// <summary>
        ///清單方塊的背景顏色。。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("清單方塊的背景顏色。")]
        [Editor(typeof(ColorEditor), typeof(UITypeEditor))]
        public Color ListBackColor
        {
            get
            {
                if (ViewState["ListBackColor"] == null)
                    return Color.Aqua;
                else
                    return (Color)ViewState["ListBackColor"];
            }
            set { ViewState["ListBackColor"] = value; }
        }

        /// <summary>
        ///清單方塊的寬度。。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("清單方塊的寬度。")]
        public Unit ListWidth
        {
            get
            {
                if (ViewState["ListWidth"] == null)
                    return Unit.Pixel(80);
                else
                    return (Unit)ViewState["ListWidth"];
            }
            set { ViewState["ListWidth"] = value; }
        }

        /// <summary>
        ///清單方塊的高度。。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("清單方塊的高度。")]
        public Unit ListHeight
        {
            get
            {
                if (ViewState["ListHeight"] == null)
                    return Unit.Pixel(120);
                else
                    return (Unit)ViewState["ListHeight"];
            }
            set { ViewState["ListHeight"] = value; }
        }

        /// <summary>
        /// 第一個清單的所有項目。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("第一個清單的所有項目。")]
        [PersistenceMode(PersistenceMode.InnerProperty), DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public ListItemCollection FirstListItems
        {
            get
            {
                return select1.Items;
            }
        }

        /// <summary>
        /// 第二個清單的所有項目。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("第二個清單的所有項目。")]
        [PersistenceMode(PersistenceMode.InnerProperty), DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public ListItemCollection SecondListItems
        {
            get
            {
                return select2.Items;
            }
        }

        #endregion

        #region Protectd Properties & Method

        protected override void OnInit(EventArgs e)
        {
            //added in 2013/10/26
            if (!this.Page.IsPostBack)
            {
                if (!this.DesignMode)
                {
                    HttpContext.Current.Session.Remove(this.UniqueID);
                }
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            //added in 2013/10/26
            if (HttpContext.Current != null)
            {
                if (HttpContext.Current.Session[this.UniqueID] == null)
                {
                    HttpContext.Current.Session.Timeout = 60;
                    HttpContext.Current.Session[this.UniqueID] = this;
                }
            }

            AjaxPro.Utility.RegisterTypeForAjax(typeof(APTemplate.ListBoxToListBox));
            if (Page.IsPostBack)
            {
                select1.Items.Clear();
                select2.Items.Clear();
                ListItem[] Items1 = new ListItem[this.FirstListBox.Items.Count];
                ListItem[] Items2 = new ListItem[this.SecondListBox.Items.Count];
                this.FirstListBox.Items.CopyTo(Items1, 0);
                this.SecondListBox.Items.CopyTo(Items2, 0);
                select1.Items.AddRange(Items1);
                select2.Items.AddRange(Items2);
            }
        }

        protected override void OnPreRender(EventArgs e)
        {
            EnsureChildControls();
            ImageButton1.Attributes.Add("onclick", "AddListOneToListCallBack('" + select1.ClientID + "','" + select2.ClientID + "','left','" + this.UniqueID + "');");
            ImageButton2.Attributes.Add("onclick", "AddListOneToListCallBack('" + select2.ClientID + "','" + select1.ClientID + "','right','" + this.UniqueID + "');");
            ImageButton3.Attributes.Add("onclick", "AddListAllToListCallBack('" + select1.ClientID + "','" + select2.ClientID + "','left','" + this.UniqueID + "');");
            ImageButton4.Attributes.Add("onclick", "AddListAllToListCallBack('" + select2.ClientID + "','" + select1.ClientID + "','right','" + this.UniqueID + "');");
            select1.Attributes.Add("ondblclick", "AddListOneToListCallBack('" + select1.ClientID + "','" + select2.ClientID + "','left','" + this.UniqueID + "');");
            select2.Attributes.Add("ondblclick", "AddListOneToListCallBack('" + select2.ClientID + "','" + select1.ClientID + "','right','" + this.UniqueID + "');");
            select1.Attributes.Add("onkeyup", "if(event.keyCode==13 || event.keyCode==108)AddListOneToListCallBack('" + select1.ClientID + "','" + select2.ClientID + "','left','" + this.UniqueID + "');");
            select2.Attributes.Add("onkeyup", "if(event.keyCode==13 || event.keyCode==108)AddListOneToListCallBack('" + select2.ClientID + "','" + select1.ClientID + "','right','" + this.UniqueID + "');");
            RegisterJavaScript.RegisterContolIncludeScript(Page);
        }

        protected override void CreateChildControls()
        {
            EnsureChildControls();

            //Table1
            Table1.ID = "Table1";
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

            //TableCell2
            TableCell2.ID = "TableCell2";

            //TableCell3
            TableCell3.ID = "TableCell3";

            //select1
            select1.ID = "select1";
            select1.Width = this.ListWidth;
            select1.Height = this.ListHeight;
            select1.ForeColor = this.ListForeColor;
            select1.BackColor = this.ListBackColor;

            //select2
            select2.ID = "select2";
            select2.Width = this.ListWidth;
            select2.Height = this.ListHeight;
            select2.ForeColor = this.ListForeColor;
            select2.BackColor = this.ListBackColor;

            //LeftTilteLabel
            LeftTilteLabel.Text = this.LeftTitleText;
            //RightTilteLabel
            RightTilteLabel.Text = this.RightTitleText;

            //ImageButton1
            ImageButton1.ID = "ImageButton1";
            ImageButton1.ImageUrl = this.Page != null ? this.Page.ClientScript.GetWebResourceUrl(this.GetType(), "APTemplate.Resources.icon-fwd.gif") : "";
            ImageButton1.Width = Unit.Pixel(12);
            ImageButton1.Height = Unit.Pixel(12);
            ImageButton1.Attributes["onmouseover"] += "this.style.cursor='pointer';";
            ImageButton1.Attributes["onmouseout"] += "this.style.cursor='default';";

            //ImageButton2
            ImageButton2.ID = "ImageButton2";
            ImageButton2.ImageUrl = this.Page != null ? this.Page.ClientScript.GetWebResourceUrl(this.GetType(), "APTemplate.Resources.icon-rew.gif") : "";
            ImageButton2.Width = Unit.Pixel(12);
            ImageButton2.Height = Unit.Pixel(12);
            ImageButton2.Attributes["onmouseover"] += "this.style.cursor='pointer';";
            ImageButton2.Attributes["onmouseout"] += "this.style.cursor='default';";

            //ImageButton3
            ImageButton3.ID = "ImageButton3";
            ImageButton3.ImageUrl = this.Page != null ? this.Page.ClientScript.GetWebResourceUrl(this.GetType(), "APTemplate.Resources.icon-ffwd.gif") : "";
            ImageButton3.Width = Unit.Pixel(12);
            ImageButton3.Height = Unit.Pixel(12);
            ImageButton3.Attributes["onmouseover"] += "this.style.cursor='pointer';";
            ImageButton3.Attributes["onmouseout"] += "this.style.cursor='default';";

            //ImageButton4
            ImageButton4.ID = "ImageButton4";
            ImageButton4.ImageUrl = this.Page != null ? this.Page.ClientScript.GetWebResourceUrl(this.GetType(), "APTemplate.Resources.icon-frew.gif") : "";
            ImageButton4.Width = Unit.Pixel(12);
            ImageButton4.Height = Unit.Pixel(12);
            ImageButton4.Attributes["onmouseover"] += "this.style.cursor='pointer';";
            ImageButton4.Attributes["onmouseout"] += "this.style.cursor='default';";

            TableCell1.HorizontalAlign = HorizontalAlign.Center;
            TableCell1.Controls.Add(LeftTilteLabel);
            TableCell1.Controls.Add(new LiteralControl("<br/>"));
            TableCell1.Controls.Add(select1);

            TableCell2.Controls.Add(ImageButton1);
            TableCell2.Controls.Add(new LiteralControl("<br /><br />"));
            TableCell2.Controls.Add(ImageButton2);
            TableCell2.Controls.Add(new LiteralControl("<br /><br />"));
            TableCell2.Controls.Add(ImageButton3);
            TableCell2.Controls.Add(new LiteralControl("<br /><br />"));
            TableCell2.Controls.Add(ImageButton4);

            TableCell3.HorizontalAlign = HorizontalAlign.Center;
            TableCell3.Controls.Add(RightTilteLabel);
            TableCell3.Controls.Add(new LiteralControl("<br/>"));
            TableCell3.Controls.Add(select2);

            TableRow1.Controls.Add(TableCell1);
            TableRow1.Controls.Add(TableCell2);
            TableRow1.Controls.Add(TableCell3);

            Table1.Controls.Add(TableRow1);

            this.Controls.Add(Table1);
        }

        protected override void RenderContents(HtmlTextWriter writer)
        {
            Table1.RenderControl(writer);
        }

        #endregion
    }
}