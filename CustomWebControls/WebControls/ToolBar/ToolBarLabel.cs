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
    /// 自定義工具列控制項的Label物件類別
    /// </summary>
    [Serializable()]
    public class ToolBarLabel : ToolBarItem
    {
        /// <summary>
        /// 儲存ToolBarLabel的標籤文字
        /// </summary>
        protected string _Text = "";

        #region Public Properties & Methods

        /// <summary>
        ///ToolBarLabel的標籤文字。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("ToolBarLabel的標籤文字。")]
        public string Text
        {
            get { return _Text; }
            set { _Text = value; }
        }

        #endregion
    }
}