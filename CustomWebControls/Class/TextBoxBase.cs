using System;
using System.Configuration;
using System.ComponentModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Drawing.Design;

namespace APTemplate
{
	/// <summary>
	/// 列舉-小數位數的長度。

	/// </summary>
  public enum DecimalType
  {
    /// <summary>
    /// 保留輸入小數值

    /// </summary>
    無 = 0,
    /// <summary>
    /// 處理輸入小數值的第一位小數

    /// </summary>
    一位 = 1,
    /// <summary>
    /// 處理輸入小數值的第二位小數

    /// </summary>
    二位 = 2,
    /// <summary>
    /// 處理輸入小數值的第三位小數

    /// </summary>
    三位 = 3,
    /// <summary>
    /// 處理輸入小數值的第四位小數

    /// </summary>
    四位 = 4,
    /// <summary>
    /// 處理輸入小數值的第五位小數

    /// </summary>
    五位 = 5
  }

	/// <summary>
	/// 列舉-小數位數的捨去進位方式。

	/// </summary>
  public enum CarryType
  {
    /// <summary>
    /// 保持輸入數字值不更動
    /// </summary>
    保留 = 0,
    /// <summary>
    /// 根據DecimalType的位數將輸入數字值四捨五入

    /// </summary>
    四捨五入 = 1,
    /// <summary>
    /// 根據DecimalType的位數將輸入數字值無條件進位
    /// </summary>
    無條件進入 = 2,
    /// <summary>
    /// 根據DecimalType的位數將輸入數字值無條件捨去
    /// </summary>
    無條件捨去 = 3
  }

  /// <summary>
  /// 輸入值比較條件

  /// </summary>
  public enum RangeOperator
  {
    /// <summary>
    /// 等於
    /// </summary>
    等於,
    /// <summary>
    /// 不等於

    /// </summary>
    不等於,
    /// <summary>
    /// 大於
    /// </summary>
    大於,
    /// <summary>
    /// 大於等於
    /// </summary>
    大於等於,
    /// <summary>
    /// 小於
    /// </summary>
    小於,
    /// <summary>
    /// 小於等於
    /// </summary>
    小於等於,
    /// <summary>
    /// 不驗證

    /// </summary>
    不驗證

  }

  /// <summary>
  /// 列舉-對齊方式
  /// </summary>
	public enum Align
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
	}

  /// <summary>
  /// 含有TextBox的自訂控制項的基底類別
  /// </summary>
  /// <remarks>
  /// <list type="bullet">
  /// <item><description>繼承自ValidationBase，為抽象類別，必須繼承使用。</description></item>
  /// </list>
  /// </remarks>
  [ToolboxData("<{0}:TextBoxBase runat=server></{0}:TextBoxBase>")]
  abstract public class TextBoxBase : ValidationBase
  {
		/// <summary>
		/// 控制項內的標籤控制項
		/// </summary>
    protected Label Label1 = new Label();
		/// <summary>
		/// 控制項內的輸入方塊控制項
		/// </summary>
    protected TextBox TextBox1 = new TextBox();
    protected string Script = "";
		/// <summary>
		/// 儲存控制項內輸入方塊的ID名稱
		/// </summary>
    protected string _TextBoxID = null;
		/// <summary>
		/// 儲存控制項內輸入方塊可輸入文字長度

		/// </summary>
    protected int _TextLength = 5000;
		/// <summary>
		/// 儲存輸入方塊標籤文字對齊方式
		/// </summary>
		protected string _TitleAlign ="left";
		/// <summary>
		/// 儲存輸入方塊的網頁上的Name名稱
		/// </summary>
		protected string _FirstTextUniqueID = "";
		/// <summary>
		/// 儲存輸入方塊的網頁上的ID名稱
		/// </summary>
		protected string _FirstTextClientID = "";
        /// <summary>
        /// 儲存輸入方塊對齊方式
        /// </summary>
        protected string _TextAlign = "left";

		/// <summary>
		///輸入方塊是否需要值。

		/// </summary>
		[DefaultValue(""),
		 Category("自訂"),
		 Description("輸入方塊是否需要值。")]
    public virtual bool NeedValue
    {
      get
      {
        if (ViewState["NeedValue"] == null)
          return true;
        else
          return (bool)ViewState["NeedValue"];
      }
      set { ViewState["NeedValue"] = value; }
    }

		/// <summary>
		///輸入方塊的標籤文字。

		/// </summary>
		[DefaultValue(""),
		 Category("自訂"),
		 Description("輸入方塊的標籤文字。")]
    public virtual string Title
    {
      get
      {
        if (ViewState["Title"] == null)
          return "";
        else
          return (string)ViewState["Title"];
      }
      set { ViewState["Title"] = value; }
    }

		/// <summary>
		///輸入方塊的標籤背景顏色。

		/// </summary>
		[DefaultValue(""),
		 Category("自訂"),
		 Description("輸入方塊的標籤背景顏色。")]
		[Editor(typeof(ColorEditor), typeof(UITypeEditor))]
    public virtual Color TitleBackColor
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
		///輸入方塊的標籤文字顏色。。

		/// </summary>
		[DefaultValue(""),
		 Category("自訂"),
		 Description("輸入方塊的文字顏色。")]
		[Editor(typeof(ColorEditor), typeof(UITypeEditor))]
    public virtual Color TitleForeColor
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
		///輸入方塊的標籤文字寬度。

		/// </summary>
		[DefaultValue(""),
		 Category("自訂"),
		 Description("輸入方塊的標籤文字寬度。")]
    public virtual Unit TitleWidth
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
		///輸入方塊標籤文字的對齊方式。

		/// </summary>
		[DefaultValue(""),
		 Category("自訂"),
		 Description("輸入方塊標籤文字的對齊方式。")]
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

        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("輸入方塊的對齊方式。")]
        public virtual Align TextAlign
        {
            get
            {
                if (ViewState["TextAlign"] == null)
                    return Align.left;
                else
                    return (Align)ViewState["TextAlign"];
            }
            set
            {
                ViewState["TextAlign"] = value;
                _TextAlign = (int)value == 1 ? "left" : (int)value == 2 ? "right" : (int)value == 3 ? "center" : "justify";
            }
        }

		/// <summary>
		///是否顯示輸入方塊的標籤文字。

		/// </summary>
		[DefaultValue(""),
		 Category("自訂"),
		 Description("是否顯示輸入方塊的標籤文字。")]
    public virtual bool IsShowTitle
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
		///第一個輸入方塊的控制項ID。

		/// </summary>
		[DefaultValue(""),
		 Category("自訂"),
		 Description("第一個輸入方塊的控制項ID。")]
    public virtual string TextBoxID
    {
      get
      {
        return _TextBoxID;
      }
      set { _TextBoxID = value; }
    }

		/// <summary>
		///輸入方塊的可輸入文字長度。

		/// </summary>
		[DefaultValue(""),
		 Category("自訂"),
		 Description("輸入方塊的可輸入文字長度。")]
    public virtual int TextLength
    {
      get
      {
        return _TextLength;
      }
      set { _TextLength = value; }
    }

		/// <summary>
		///輸入方塊的寬度。
		/// </summary>
		[DefaultValue(""),
		 Category("自訂"),
		 Description("輸入方塊的寬度。")]
    public virtual Unit TextBoxWidth
    {
      get
      {
        if (ViewState["TextBoxWidth"] == null)
          return TextBox1.Width;
        else
          return (Unit)ViewState["TextBoxWidth"];
      }
      set { ViewState["TextBoxWidth"] = value; }
    }

    /// <summary>
    ///輸入方塊的寬度。
    /// </summary>
    [DefaultValue(""),
     Category("自訂"),
     Description("輸入方塊的高度。")]
    public virtual Unit TextBoxHeight
    {
      get
      {
        if (ViewState["TextBoxHeight"] == null)
          return TextBox1.Height;
        else
          return (Unit)ViewState["TextBoxHeight"];
      }
      set { ViewState["TextBoxHeight"] = value; }
    }

    /// <summary>
    ///輸入方塊的文字顏色。

    /// </summary>
    [DefaultValue(""),
     Category("自訂"),
     Description("輸入方塊的文字顏色。")]
    [Editor(typeof(ColorEditor), typeof(UITypeEditor))]
    public virtual Color TextForeColor
    {
      get
      {
        if (ViewState["TextForeColor"] == null)
          return Color.Black;
        else
          return (Color)ViewState["TextForeColor"];
      }
      set { ViewState["TextForeColor"] = value; }
    }

    /// <summary>
    ///輸入方塊的背景顏色。

    /// </summary>
    [DefaultValue(""),
     Category("自訂"),
     Description("輸入方塊的背景顏色。")]
    [Editor(typeof(ColorEditor), typeof(UITypeEditor))]
    public virtual Color TextBackColor
    {
      get
      {
        if (ViewState["TextBackColor"] == null)
          return Color.White;
        else
          return (Color)ViewState["TextBackColor"];
      }
      set { ViewState["TextBackColor"] = value; }
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
    ///輸入方塊是否唯讀。
    /// </summary>
    [DefaultValue(""),
     Category("自訂"),
     Description("輸入方塊是否唯讀。")]
    public virtual bool ReadOnly
    {
      get
      {
        if (ViewState["ReadOnly"] == null)
          return false;
        else
          return (bool)ViewState["ReadOnly"];
      }
      set { ViewState["ReadOnly"] = value; }
    }

    /// <summary>
    ///取得內部第一個TextBox控制項ClientID。
    /// </summary>
    [Browsable(false),ReadOnly(true)]
    public virtual string FirstTextBoxClientID
    {
      get 
      {
          if (!TextBox1.ClientID.Contains(this.ClientID))
          {
              return this.ClientID + "_" + TextBox1.ClientID;
          }
          else { return TextBox1.ClientID; }
      }
    }

		/// <summary>
		///資料的驗證方式。

		/// </summary>
		[DefaultValue(""),
		 Category("自訂"),
		 Description("資料的驗證方式。")]
    public new OptionValidationType ValidationType
    {
      get
      {
        if (ViewState["ValidationType"] == null)
          return OptionValidationType.Label;
        else
          return (OptionValidationType)ViewState["ValidationType"];
      }
      set
      {
        ViewState["ValidationType"] = value;
        if (value == OptionValidationType.Alert)
        {
          ViewState["ValidationSummary"] = ViewState["ValidationSummary"] == null ? new ValidationSummary() : (ValidationSummary)ViewState["ValidationSummary"];
        }
      }
    }

		protected override void OnPreRender(EventArgs e)
		{
			RegisterJavaScript.RegisterContolIncludeScript(Page);
      base.OnPreRender(e);
		}

    protected override void OnUnload(EventArgs e)
    {
			try
			{
				Context.Session["i"] = null;
			}
			catch (Exception ex)
			{ }
    }

    //protected override void LoadViewState(object savedState)
    //{
    //  object[] allStates = (object[])savedState;
    //  _FirstTextUniqueID = (string)allStates[0];
    //  _FirstTextClientID = (string)allStates[1];
    //  TextBox1.Text = Context.Request.Form[_FirstTextUniqueID] == null ? TextBox1.Text : Context.Request.Form[_FirstTextUniqueID].ToString();
    //}

    //protected override object SaveViewState()
    //{
    //  object[] allStates = new object[2];
    //  allStates[0] = TextBox1.UniqueID;
    //  allStates[1] = TextBox1.ClientID;
    //  return allStates;
    //}

    protected override void LoadControlState(object savedState)
    {
      object[] allStates = (object[])savedState;
      _FirstTextUniqueID = (string)allStates[0];
      _FirstTextClientID = (string)allStates[1];
      TextBox1.Text = Context.Request.Form[_FirstTextUniqueID] == null ? TextBox1.Text : Context.Request.Form[_FirstTextUniqueID].ToString();
    }

    protected override object SaveControlState()
    {
      object[] allStates = new object[2];
      allStates[0] = TextBox1.UniqueID;
      allStates[1] = TextBox1.ClientID;
      return allStates;
    }
  }
}