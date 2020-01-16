using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;

namespace APTemplate
{
    /// <summary>
    /// 自定義的另開視窗按鈕
    /// </summary>
    [ToolboxData("<{0}:Button_PopupWindow runat=server></{0}:Button_PopupWindow>")]
    [ToolboxBitmap(typeof(Button_PopupWindow), "Resources.Control_Button.bmp")]
    public class Button_PopupWindow : WebButtonBase
    {
        /// <summary>
        /// 儲存開啟的視窗類型
        /// </summary>
        protected WindowType _WindowType = WindowType.Normal;
        /// <summary>
        /// 儲存開啟的視窗寬度
        /// </summary>
        protected int _width = 1024;
        /// <summary>
        /// 儲存開啟的視窗高度
        /// </summary>
        protected int _height = 700;
        /// <summary>
        /// 儲存開啟的視窗特色
        /// </summary>
        protected string _Features = "status=yes,scrollbars=yes";
        /// <summary>
        /// 儲存執行按鈕是否要取消另開視窗
        /// </summary>
        protected bool _CancelWindow = false;

        public Button_PopupWindow()
        {
            LinkButton1.CausesValidation = false;
        }

        #region Public Properties & Methods

        /// <summary>
        ///開啟的視窗類型。
        /// </summary>
        [Category("自訂")]
        [DefaultValue(WindowType.Normal)]
        [Description("開啟的視窗類型")]
        public WindowType WindowType
        {
            get { return _WindowType; }
            set { _WindowType = value; }
        }

        /// <summary>
        ///開啟的視窗寬度。
        /// </summary>
        [Category("自訂")]
        [DefaultValue(1024)]
        [Description("開啟的視窗寬度")]
        public int WindowWidth
        {
            get { return _width; }
            set { _width = value; }
        }

        /// <summary>
        /// 開啟的視窗高度。
        /// </summary>
        /// <value></value>
        [Category("自訂")]
        [DefaultValue(700)]
        [Description("開啟的視窗高度")]
        public int WindowHeight
        {
            get { return _height; }
            set { _height = value; }
        }

        /// <summary>
        /// 開啟的視窗特色。
        /// </summary>
        [Category("自訂")]
        [DefaultValue("")]
        [Description("開啟的視窗特色")]
        public string Features
        {
            get { return _Features; }
            set { _Features = value; }
        }

        /// <summary>
        /// 執行按鈕是否要取消另開視窗。
        /// </summary>
        [Category("自訂")]
        [DefaultValue(false)]
        [Description("執行按鈕是否要取消另開視窗")]
        public bool CancelWindow
        {
            get { return _CancelWindow; }
            set { _CancelWindow = value; }
        }

        #endregion

        #region Protected Properties & Methods

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            base.Initialize(LinkButton1);
        }

        protected override void OnPreRender(EventArgs e)
        {
            if (base.Enable && CancelWindow == false)
            { 
              if(!this.CausesValidation)
                WebScript.OpenWindow(this, LinkButton1.ID, EventType.OnClick, this.WindowType, this.PostBackUrl.Replace("~/", ""), null, this.Features, this.WindowWidth, this.WindowHeight); 
              else
                WebScript.OpenWindowWithValidation(this, LinkButton1.ID,this.ValidationGroup, EventType.OnClick, this.WindowType, this.PostBackUrl.Replace("~/", ""), null, this.Features, this.WindowWidth, this.WindowHeight); 
            }
            base.OnPreRender(e);
        }

        #endregion
    }
}