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

namespace APTemplate
{
    [Serializable()]
    public abstract class ToolBarItem
    {
        /// <summary>
        /// 儲存工具列項目的啟用狀態
        /// </summary>
        protected bool _Enabled = true;
        /// <summary>
        /// 儲存工具列項目的可見狀態
        /// </summary>
        protected bool _Visible = true;

        #region Public Properties & Methods

        /// <summary>
        /// 工具列項目的啟用狀態。
        /// </summary>
        public bool Enabled
        {
            get { return _Enabled; }
            set { _Enabled = value; }
        }
        /// <summary>
        /// 工具列項目的可見狀態。
        /// </summary>
        public bool Visible
        {
            get { return _Visible; }
            set { _Visible = value; }
        }

        #endregion

    }
}