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
    public class FileFilterItem
    {
        /// <summary>
        /// 檔案名稱篩選值
        /// </summary>
        protected string _Value = "";

        #region Public Properties & Methods

        /// <summary>
        /// 工具列項目的啟用狀態。
        /// </summary>
        public string Value
        {
            get { return _Value; }
            set { _Value = value; }
        }
        #endregion

    }
}