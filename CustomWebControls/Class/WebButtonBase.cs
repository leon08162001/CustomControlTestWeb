using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.Design;
using System.Web.UI.WebControls;
using System.Drawing.Design;

namespace APTemplate
{
  /// <summary>
  /// 自定義按鈕控制項的基底類別
  /// </summary>
  [ToolboxData("<{0}:WebButtonBase runat=server></{0}:WebButtonBase>")]
  abstract public class WebButtonBase : CompositeControl, INamingContainer
  {
    /// <summary>
    /// 建立按鈕控制項的按鈕物件
    /// </summary>
    protected LinkButton LinkButton1 = new LinkButton();
		protected Image Img1 = new Image();
		protected Image Img2 = new Image();
		protected Table Tbl = new Table();
		protected TableRow Tr1 = new TableRow();
		protected TableCell Tc1 = new TableCell();
		protected TableCell Tc2 = new TableCell();
		protected TableCell Tc3 = new TableCell();
    /// <summary>
    /// 儲存按鈕在網頁上的可見狀態
    /// </summary>
    protected bool _Visible = true;
    protected string _display = "inline-block";
    /// <summary>
    /// 儲存按鈕ID名稱
    /// </summary>
    protected string _ButtonID = null;
    /// <summary>
    /// 點擊自訂按鈕控制項時的事件
    /// </summary>
    public event EventHandler Click;
    /// <summary>
    /// 點擊自訂按鈕控制項時的事件(可帶入CommandArgument)
    /// </summary>
    public event CommandEventHandler Command;

    protected override void OnLoad(EventArgs e)
    {
      CommandEventArgs CommandEventArgs = new CommandEventArgs(LinkButton1.CommandName, LinkButton1.CommandArgument);
      if (Page.IsPostBack && Page.Request.Form["__EVENTTARGET"] == LinkButton1.UniqueID)
      {
        btn_Click(LinkButton1, e);
        btn_Command(LinkButton1, CommandEventArgs);
      }
    }

		protected override void OnPreRender(EventArgs e)
		{
			RegisterJavaScript.RegisterContolIncludeScript(Page);
		}

		/// <summary>
		///按鈕ID名稱。
		/// </summary>
		[Category("自訂")]
		[DefaultValue("")]
    [Description("按鈕ID名稱")]
    public string ButtonID
    {
      get
      {
        return _ButtonID;
      }
      set { _ButtonID = value; }
    }

		/// <summary>
		///按鈕寬度。
		/// </summary>
		[Category("自訂")]
    [DefaultValue("200")]
    [Description("按鈕寬度")]
    public override Unit Width
    {
      get
      {
        EnsureChildControls();
        return LinkButton1.Width;
      }
      set
      {
        EnsureChildControls();
        LinkButton1.Width = value;
      }
    }

		/// <summary>
		///按鈕文字。
		/// </summary>
		[Category("自訂")]
    [DefaultValue("")]
    [Description("按鈕文字")]
    public virtual string Text
    {
      get
      {
        EnsureChildControls();
        return LinkButton1.Text;
      }
      set
      {
        EnsureChildControls();
        LinkButton1.Text = value;
      }
    }

		/// <summary>
		///按鈕在網頁上的可用狀態。
		/// </summary>
		[Category("自訂")]
    [DefaultValue(true)]
    [Description("按鈕在網頁上的可用狀態")]
    public virtual bool Enable
    {
      get
      {
        EnsureChildControls();
        return LinkButton1.Enabled;
      }
      set
      {
        EnsureChildControls();
        LinkButton1.Enabled = value;
      }
    }

		/// <summary>
		///按鈕在網頁上的可見狀態。
		/// </summary>
		[Category("自訂")]
    [DefaultValue(true)]
    [Description("按鈕在網頁上的可見狀態")]
    public virtual bool CanVisible
    {
      get
      { return _Visible; }
      set
      {
        _Visible = value;
        if (_Visible == true)
          _display = "inline-block";
        else
          _display = "none";
      }
    }

		/// <summary>
		///按鈕按一下所要執行的JavaScript指令碼。
		/// </summary>
		[Category("自訂")]
    [DefaultValue(true)]
    [Description("按鈕按一下所要執行的JavaScript指令碼")]
    public virtual string ClientScript
    {
      get
      {
        EnsureChildControls();
        return LinkButton1.OnClientClick;
      }
      set
      {
        EnsureChildControls();
        LinkButton1.OnClientClick = value;
      }
    }

		/// <summary>
		///按鈕按一下是否要做驗證動作。
		/// </summary>
		[Category("自訂")]
    [DefaultValue(true)]
    [Description("按鈕按一下是否要做驗證動作")]
    public virtual bool CausesValidation
    {
      get
      {
        EnsureChildControls();
        return LinkButton1.CausesValidation;
      }
      set
      {
        EnsureChildControls();
        LinkButton1.CausesValidation = value;
      }
    }

		/// <summary>
		///驗證群組名稱。
		/// </summary>
		[DefaultValue(""),
		 Category("自訂"),
		 Description("驗證群組名稱。")]
		public virtual String ValidationGroup
		{
			get
			{
				if (ViewState["ValidationGroup"] == null)
					return "";
				else
					return (String)ViewState["ValidationGroup"];
			}
			set { ViewState["ValidationGroup"] = value; }
		}

		/// <summary>
		///按鈕的命令名稱。
		/// </summary>
		[Category("自訂")]
    [DefaultValue(true)]
    [Description("按鈕的命令名稱")]
    public virtual string CommandName
    {
      get
      {
        EnsureChildControls();
        return LinkButton1.CommandName;
      }
      set
      {
        EnsureChildControls();
        LinkButton1.CommandName = value;
      }
    }

		/// <summary>
		///按鈕的參數資料。
		/// </summary>
		[Category("自訂")]
    [DefaultValue(true)]
    [Description("按鈕的參數資料")]
    public virtual string CommandArgument
    {
      get
      {
        EnsureChildControls();
        return LinkButton1.CommandArgument;
      }
      set
      {
        EnsureChildControls();
        LinkButton1.CommandArgument = value;
      }
    }

		/// <summary>
		///按鈕導向的網頁。
		/// </summary>
		[Category("自訂")]
    [DefaultValue(true)]
    [Description("按鈕導向的網頁")]
		[UrlProperty(), Editor(typeof(UrlEditor), typeof(UITypeEditor))]
    public virtual string PostBackUrl
    {
      get
      {
        EnsureChildControls();
        return LinkButton1.PostBackUrl;
      }
      set
      {
        EnsureChildControls();
        LinkButton1.PostBackUrl = value;
      }
    }

    ///<summary>
    ///returns HTML-ized color strings
    ///</summary>
    private string HtmlColor(System.Drawing.Color color)
    {
      if (color.IsEmpty)
        return "";
      if (color.IsNamedColor)
        return color.ToKnownColor().ToString();
      if (color.IsSystemColor)
        return color.ToString();
      return "#" + color.ToArgb().ToString("x").Substring(2);
    }

    ///<summary>
    ///returns css "style=" tag for this control
    ///based on standard control visual properties
    ///</summary>
    private string CssStyle()
    {
      System.Text.StringBuilder sb = new System.Text.StringBuilder();
      string strColor;
      if (BorderWidth.ToString().Length > 0)
      {
        sb.Append("border-width:");
        sb.Append(BorderWidth.ToString());
        sb.Append(";");
      }
      if (BorderStyle != System.Web.UI.WebControls.BorderStyle.NotSet)
      {
        sb.Append("border-style:");
        sb.Append(BorderStyle.ToString());
        sb.Append(";");
      }
      strColor = HtmlColor(BorderColor);
      if (strColor.Length > 0)
      {
        sb.Append("border-color:");
        sb.Append(strColor);
        sb.Append(";");
      }
      strColor = HtmlColor(BackColor);
      if (strColor.Length > 0)
      {
        sb.Append("background-color:" + strColor + ";");
      }
      strColor = HtmlColor(ForeColor);
      if (strColor.Length > 0)
      {
        sb.Append("color:" + strColor + ";");
      }
      if (Font.Bold)
      {
        sb.Append("font-weight:bold;");
      }
      if (Font.Italic)
      {
        sb.Append("font-style:italic;");
      }
      if (Font.Underline)
      {
        sb.Append("text-decoration:underline;");
      }
      if (Font.Strikeout)
      {
        sb.Append("text-decoration:line-through;");
      }
      if (Font.Overline)
      {
        sb.Append("text-decoration:overline;");
      }

      if (Font.Size.ToString().Length > 0)
      {
        sb.Append("font-size:" + Font.Size.ToString() + ";");
      }

      if (Font.Names.Length > 0)
      {
        sb.Append("font-family:");
        foreach (string strFontFamily in Font.Names)
        {
          sb.Append(strFontFamily);
          sb.Append(",");
        }
        sb.Length = sb.Length - 1;
        sb.Append(";");
      }

      if (Height.ToString() != "")
      {
        sb.Append("height:" + Height.ToString() + ";");
      }

      if (Width.ToString() != "")
      {
        sb.Append("width" + Width.ToString() + ";");
      }
      sb.Append("'");

      if (sb.ToString() == " style=''")
        return "";
      else
        return sb.ToString();
    }

    protected void btn_Click(Object sender, System.EventArgs e)
    {
      if (Click != null)
        Click(sender, e);
    }

    protected void btn_Command(object sender, CommandEventArgs e)
    {
      if (Command != null)
        Command(sender, e);
    }

    protected void Initialize(LinkButton linkbutton)
    {
      EnsureChildControls();
      linkbutton.ID = ButtonID == null || ButtonID == "" ? "LinkButton1" : ButtonID;
      linkbutton.Width = Width;
      linkbutton.Text = Text == "" ? "..." : Text;
      linkbutton.Font.Size = FontUnit.Small;
      linkbutton.Enabled = Enable;
      linkbutton.Visible = CanVisible;
      _display = CanVisible ? "inline-block" : "none";
      linkbutton.OnClientClick = ClientScript;
      linkbutton.CausesValidation = CausesValidation;
			linkbutton.ValidationGroup = ValidationGroup;
      linkbutton.CommandName = CommandName;
      linkbutton.CommandArgument = CommandArgument;
      linkbutton.PostBackUrl = PostBackUrl;
      linkbutton.Style["text-decoration"] = "none";
      linkbutton.Attributes["style"] += CssStyle();
      linkbutton.CssClass = CssClass;
			Tbl.BorderWidth = Unit.Pixel(0);
			Tbl.CellPadding = 0;
			Tbl.CellSpacing = 0;

            if (!this.DesignMode)
            {
                if (Context.Request.Browser.Type.IndexOf("IE") != -1 && float.Parse(Context.Request.Browser.Version) < 8)
                {
                    Tbl.Attributes["style"] += "cursor:hand;display:inline;vertical-align:top;";
                }
                else
                {
                    Tbl.Attributes["style"] += "cursor:hand;display:inline-block;vertical-align:top;";
                }
            }
            else
            {
                Tbl.Attributes["style"] += "cursor:hand;display:inline;vertical-align:top;";
            }
			Tc2.Attributes["nowrap"] = "true";
      Tc2.Attributes["background"] = this.Page !=null ? this.Page.ClientScript.GetWebResourceUrl(this.GetType(), "APTemplate.Resources.button_02.gif") : "";
			Tc2.Attributes["align"] = "center";
			Tc2.BorderStyle = BorderStyle.None;
			Tc1.BorderStyle = BorderStyle.None;
			Tc3.BorderStyle = BorderStyle.None;
			Img1.Width = Unit.Pixel(3);
			Img1.Height = Unit.Pixel(22);
			Img1.ImageUrl = this.Page !=null ? this.Page.ClientScript.GetWebResourceUrl(this.GetType(), "APTemplate.Resources.button_01.gif") : "";
			Img1.Visible = CanVisible;
			Img2.Width = Unit.Pixel(3);
			Img2.Height = Unit.Pixel(22);
			Img2.ImageUrl = this.Page !=null ? this.Page.ClientScript.GetWebResourceUrl(this.GetType(), "APTemplate.Resources.button_03.gif") : "";
			Img2.Visible = CanVisible;
    }

    protected override void CreateChildControls()
    {
      this.Controls.Clear();
      EnsureChildControls();
      Initialize(LinkButton1);

			Tc1.Controls.Add(Img1);
			Tc2.Controls.Add(LinkButton1);
			Tc3.Controls.Add(Img2);

			Tr1.Controls.Add(Tc1);
			Tr1.Controls.Add(Tc2);
			Tr1.Controls.Add(Tc3);

			Tbl.Controls.Add(Tr1);
			this.Controls.Add(Tbl);
		}

    protected override void RenderContents(HtmlTextWriter writer)
    {
			Tbl.RenderControl(writer);
    }
  }
}