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

	/// <summary>
	/// Summary description for WebForm1.
	/// </summary>
public partial class WebForm1 : System.Web.UI.Page
	{
		protected CollapsiblePanel CollapsiblePanel1;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
		}

    protected void ToolBar_Click(Object sender, EventArgs e)
    {
      ToolBarButton ToolButton = (ToolBarButton)sender;
      string MediaPath = "http://localhost:90/Media/";
      string MediaFile = ToolButton.ClickArgument;
      MediaPlayer1.Url = MediaPath + MediaFile;
    }

		#region Web Form Designer generated code
    //override protected void OnInit(EventArgs e)
    //{
    //  //
    //  // CODEGEN: This call is required by the ASP.NET Web Form Designer.
    //  //
    //  InitializeComponent();
    //  base.OnInit(e);
    //}
		
    ///// <summary>
    ///// Required method for Designer support - do not modify
    ///// the contents of this method with the code editor.
    ///// </summary>
    //private void InitializeComponent()
    //{    

    //}
		#endregion

	}