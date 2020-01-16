using System;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.Design;
using System.Web.UI.WebControls;
using System.Web.UI.Design.WebControls;
using System.Drawing;
using System.Drawing.Design;

namespace APTemplate
{
    /// <summary>
    /// 自定義工具列控制項的按鈕物件類別
    /// </summary>
    [Serializable()]
    public class ToolBarButton : ToolBarItem
    {
        /// <summary>
        /// 儲存按鈕的圖片位置
        /// </summary>
        private string _ImageUrl = "";
        /// <summary>
        /// 儲存移到按鈕上的圖片位置
        /// </summary>
        private string _OverImageUrl = "";
        /// <summary>
        /// 作用中按鈕的圖片位置
        /// </summary>
        private string _ActiveImageUrl = "";
        /// <summary>
        /// 儲存滑鼠移到按鈕上時的提示文字
        /// </summary>
        private string _ToolTip = "";
        /// <summary>
        /// 儲存按鈕按一下時要執行的JavaScript程式碼
        /// </summary>
        private string _OnClientClick = "";
        /// <summary>
        /// 儲存按鈕按一下時要回傳的網頁
        /// </summary>
        private string _PostBackUrl = "";
        /// <summary>
        /// 儲存按鈕的可用狀態
        /// </summary>
        private Boolean _Enabled = true;
        /// <summary>
        /// 儲存按鈕的可見狀態
        /// </summary>
        private Boolean _Visible = true;
        /// <summary>
        /// 儲存是否為分隔線
        /// </summary>
        private Boolean _IsSeperator = false;
        /// <summary>
        /// 儲存按鈕Click事件名稱
        /// </summary>
        private string _ClickArgument = "";
        /// <summary>
        /// 是否造成驗證
        /// </summary>
        private bool _CauseValidation = false;

        #region Public Properties & Methods

        public ToolBarButton()
        {

        }

        /// <summary>
        ///按鈕的圖片位置。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("按鈕的圖片位置。")]
        [UrlProperty()]
        [Editor(typeof(ImageUrlEditor), typeof(UITypeEditor))]
        public string ImageUrl
        {
            get { return _ImageUrl; }
            set { _ImageUrl = value; }
        }

        /// <summary>
        ///移到按鈕上的圖片位置。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("移到按鈕上的圖片位置。")]
        [UrlProperty()]
        [Editor(typeof(ImageUrlEditor), typeof(UITypeEditor))]
        public string OverImageUrl
        {
            get { return _OverImageUrl; }
            set { _OverImageUrl = value; }
        }

        /// <summary>
        ///作用中按鈕的圖片位置。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("作用中按鈕的圖片位置。")]
        [UrlProperty()]
        [Editor(typeof(ImageUrlEditor), typeof(UITypeEditor))]
        public string ActiveImageUrl
        {
            get { return _ActiveImageUrl; }
            set { _ActiveImageUrl = value; }
        }

        /// <summary>
        ///滑鼠移到按鈕上時的提示文字。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("滑鼠移到按鈕上時的提示文字。")]
        public string ToolTip
        {
            get { return _ToolTip; }
            set { _ToolTip = value; }
        }

        /// <summary>
        ///按鈕按一下時要執行的JavaScript程式碼。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("按鈕按一下時要執行的JavaScript程式碼。")]
        public string OnClientClick
        {
            get { return _OnClientClick; }
            set { _OnClientClick = value; }
        }

        /// <summary>
        ///按鈕按一下時要回傳的網頁。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("按鈕按一下時要回傳的網頁。")]
        [UrlProperty()]
        [Editor(typeof(UrlEditor), typeof(UITypeEditor))]
        public string PostBackUrl
        {
            get { return _PostBackUrl; }
            set { _PostBackUrl = value; }
        }

        /// <summary>
        ///按鈕的可用狀態。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("按鈕的可用狀態。")]
        public Boolean Enabled
        {
            get { return _Enabled; }
            set { _Enabled = value; }
        }

        /// <summary>
        ///按鈕的可見狀態。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("按鈕的可見狀態。")]
        public Boolean Visible
        {
            get { return _Visible; }
            set { _Visible = value; }
        }

        /// <summary>
        ///是否為分隔線。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
             Description("是否為分隔線。")]
        public Boolean IsSeperator
        {
            get { return _IsSeperator; }
            set { _IsSeperator = value; }
        }

        /// <summary>
        ///按鈕Click事件名稱。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("按鈕Click事件名稱。")]
        public string ClickArgument
        {
            get { return _ClickArgument; }
            set { _ClickArgument = value; }
        }

        /// <summary>
        ///是否造成驗證。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
        Description("是否造成驗證。")]
        public bool CauseValidation
        {
            get { return _CauseValidation; }
            set { _CauseValidation = value; }
        }

        #endregion

    }
}