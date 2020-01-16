using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using APTemplate;

namespace EeekSoft.PopupTest
{
	/// <summary>
	/// Testing page...
	/// </summary>
	public partial class Default : System.Web.UI.Page
	{
    #region Web Form Designer generated code

    protected System.Web.UI.WebControls.TextBox TextTitle;
    protected PopupWin PopupWin1;
    protected PopupWinAnchor PopupWinAnchor1;
  
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    

		}
		#endregion

		protected void Page_Load(object sender, System.EventArgs e)
    {
      lblMsg.Visible=false;
      popupBob.Visible=popupWin2.Visible=popupWin.Visible=false;
    }

    protected void btnPopup_Click(object sender, System.EventArgs e)
    {
      popupWin.HideAfter=(sender==null)?-1:5000;
      popupWin.Visible=true;
      popupWin.Title=textTitle.Text;
      popupWin.Message=textMsg.Text;
      popupWin.Text=textFull.Text;
      popupWin.DragDrop=(dropDrag.SelectedIndex==0);
      switch(popDocking.SelectedIndex)
      {
        case 0: popupWin.DockMode=PopupDocking.BottomLeft; break;
        case 1: popupWin.DockMode=PopupDocking.BottomRight; break;
      }
      switch(clrStyle.SelectedIndex)
      {
        case 0: popupWin.ColorStyle=PopupColorStyle.Red;break;
        case 1: popupWin.ColorStyle=PopupColorStyle.Green;break;
        case 2: popupWin.ColorStyle=PopupColorStyle.Blue;break;
        case 3: popupWin.ColorStyle=PopupColorStyle.Violet;break;
      }    
      popupWin2.Visible=false;
    }

    protected void btn4Ever_Click(object sender, System.EventArgs e)
    {
      btnPopup_Click(null,null);
    }

    protected void btnTwo_Click(object sender, System.EventArgs e)
    {
      btnPopup_Click(sender,e);
      popupWin2.DockMode=popupWin.DockMode;
      popupWin2.DragDrop=(dropDrag.SelectedIndex==0);
      popupWin2.Visible=true;
    }

    protected void popupWin2_LinkClick(object sender, System.EventArgs e)
    {
      lblMsg.Text="Hey ! You clicked on link on second popup !!";
      lblMsg.Visible=true;
    }

    protected void popupWin2_PopupClose(object sender, System.EventArgs e)
    {
      lblMsg.Text="Hey ! You closed second popup !!";
      lblMsg.Visible=true;
    }

    protected void btnBob_Click(object sender, System.EventArgs e)
    {
      popupBob.Visible=true;
      popupBob.DragDrop=(dropDrag.SelectedIndex==0);
    }
	}
}
