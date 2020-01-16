using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using APTemplate;

public partial class Demo_ToolBar_MediaPlayerDemo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void ToolBar_Click(object sender, EventArgs e)
    {
        ToolBarButton ToolButton = (ToolBarButton)sender;
        //string MediaPath = "http://localhost:90/Media/";
        string MediaPath = "http://www.youtube.com/watch?";
        //string MediaFile = ToolButton.ClickArgument;
        string MediaFile = ToolButton.ClickArgument;
        MediaPlayer1.Url = MediaPath + MediaFile;
    }
    protected void ToolBar1_Button_ConfirmYesNoClick(object sender, EventArgs e)
    {
        string a = "";
    }
}
