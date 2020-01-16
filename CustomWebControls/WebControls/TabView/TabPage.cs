using System;
using System.Web.UI;
using System.Drawing;
using System.Web.UI.WebControls;
using System.ComponentModel;
using System.Drawing.Design;

namespace APTemplate
{
    /// <summary>
    /// 自定義頁籤控制項的頁籤物件類別
    /// </summary>
    [ToolboxItem(false)]
    public class TabPage : Panel, INamingContainer
    {
        private Unit _PaddingTop = Unit.Pixel(10);
        private Unit _PaddingBottom = Unit.Pixel(10);
        private Unit _PaddingRight = Unit.Pixel(10);
        private Unit _PaddingLeft = Unit.Pixel(10);

        #region Public Properties & Methods

        public TabPage()
        {
            this.Text = "Tab Item";
        }

        /// <summary>
        /// Tab標籤的文字。
        /// </summary>
        [DefaultValue(""),
             Category("自訂"),
         Description("Tab標籤的文字。")]
        public string Text
        {
            get
            {
                object val = this.ViewState["Text"];
                if (val == null) return "";
                return val.ToString();
            }
            set
            {
                this.ViewState["Text"] = value;
            }
        }

        /// <summary>
        /// Tab標籤裏物件離Tab標籤的最上緣間距。
        /// </summary>
        [DefaultValue(10),
         Category("自訂"),
         Description("ab標籤裏物件離Tab標籤的最上緣間距。")]
        public Unit PaddingTop
        {
            get { return _PaddingTop; }
            set { _PaddingTop = value; }
        }

        /// <summary>
        /// Tab標籤裏物件離Tab標籤的最下緣間距。
        /// </summary>
        [DefaultValue(10),
         Category("自訂"),
         Description("Tab標籤裏物件離Tab標籤的最下緣間距。")]
        public Unit PaddingBottom
        {
            get { return _PaddingBottom; }
            set { _PaddingBottom = value; }
        }

        /// <summary>
        /// Tab標籤裏物件離Tab標籤的最左緣間距。
        /// </summary>
        [DefaultValue(10),
         Category("自訂"),
         Description("Tab標籤裏物件離Tab標籤的最左緣間距。")]
        public Unit PaddingLeft
        {
            get { return _PaddingLeft; }
            set { _PaddingLeft = value; }
        }

        /// <summary>
        /// Tab標籤裏物件離Tab標籤的最右緣間距。
        /// </summary>
        [DefaultValue(10),
         Category("自訂"),
         Description("Tab標籤裏物件離Tab標籤的最右緣間距。")]
        public Unit PaddingRight
        {
            get { return _PaddingRight; }
            set { _PaddingRight = value; }
        }

        #endregion

        #region Protected Properties & Methods

        protected override void AddAttributesToRender(HtmlTextWriter writer)
        {
            base.Attributes["style"] = String.Format("padding-top:{0};padding-bottom:{1};padding-left:{2};padding-right:{3};", PaddingTop, PaddingBottom, PaddingLeft, PaddingRight);
            base.AddAttributesToRender(writer);
        }

        #endregion

    }
}