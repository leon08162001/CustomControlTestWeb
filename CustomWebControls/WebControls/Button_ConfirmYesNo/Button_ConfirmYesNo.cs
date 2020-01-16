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
    /// 自定義的訊息確認按鈕
    /// </summary>
    [ToolboxData("<{0}:Button_ConfirmYesNo runat=server></{0}:Button_ConfirmYesNo>")]
    [ToolboxBitmap(typeof(Button_ConfirmYesNo), "Resources.Control_Button.bmp")]
    public class Button_ConfirmYesNo : WebButtonBase
    {
        /// <summary>
        /// 儲存按鈕按一下所顯示的訊息方塊文字
        /// </summary>
        protected string _Message = "";
        /// <summary>
        /// 儲存按鈕是否顯示訊息方塊文字
        /// </summary>
        protected bool _CancelConfirm = false;

        #region Public Properties & Methods

        /// <summary>
        ///按鈕按一下所顯示的訊息方塊文字。
        /// </summary>
        [Category("自訂")]
        [DefaultValue("")]
        [Description("按鈕按一下所顯示的訊息方塊文字")]
        public string Message
        {
            get { return _Message; }
            set { _Message = value; }
        }

        /// <summary>
        ///按鈕是否顯示訊息方塊文字。
        /// </summary>
        [Category("自訂")]
        [DefaultValue(true)]
        [Description("按鈕是否顯示訊息方塊文字")]
        public bool CancelConfirm
        {
            get { return _CancelConfirm; }
            set { _CancelConfirm = value; }
        }

        #endregion

        #region Protected Properties & Methods

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            base.Initialize(LinkButton1);
            if (base.Enable && CancelConfirm == false)
                WebScript.AddClientMessageToControl(this, LinkButton1.ID, EventType.OnClick, MessageType.Confirm, this.Message);
        }

        #endregion
    }
}