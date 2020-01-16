using System;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.Design.WebControls;
using System.Drawing;
using System.Drawing.Design;

namespace APTemplate
{
    /// <summary>
    /// 自定義工具列控制項的TextBox物件類別
    /// </summary>
    [Serializable()]
    public class ToolBarTextBox : ToolBarItem
    {
        /// <summary>
        /// 儲存ToolBarTtextBox的內容文字
        /// </summary>
        protected string _Text = "";
        /// <summary>
        /// 儲存ToolBarTtextBox的識別ID項
        /// </summary>
        protected string _ID = "";
        /// <summary>
        /// 儲存ToolBarTtextBox的寬度
        /// </summary>
        protected Unit _Width = Unit.Pixel(50);

        #region Public Properties & Methods

        /// <summary>
        ///ToolBarTtextBox的內容文字。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("ToolBarTtextBox的內容文字。")]
        public string Text
        {
            get { return _Text; }
            set { _Text = value; }
        }

        /// <summary>
        ///ToolBarTtextBox的識別ID項。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("ToolBarTtextBox的識別ID項。")]
        public string ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        /// <summary>
        ///ToolBarTtextBox的寬度。
        /// </summary>
        [Category("自訂"),
         Description("ToolBarTtextBox的寬度。")]
        public Unit Width
        {
            get { return _Width; }
            set { _Width = value; }
        }

        #endregion

    }
}