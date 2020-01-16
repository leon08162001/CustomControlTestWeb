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
    /// 自定義的一般按鈕
    /// </summary>
    [ToolboxData("<{0}:Button_Normal runat=server></{0}:Button_Normal>")]
    [ToolboxBitmap(typeof(Button_Normal), "Resources.Control_Button.bmp")]
    public class Button_Normal : WebButtonBase
    {
        #region Protected Properties & Methods

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            base.Initialize(LinkButton1);
        }

        #endregion
    }
}