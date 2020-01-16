using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using APTemplate;

public partial class Default7 : System.Web.UI.Page
{
  protected void Page_Load(object sender, EventArgs e)
  {
  }

  protected void ToolBar_Click(Object sender, EventArgs e)
  {
    if (Page.IsPostBack)
    {
      ToolBarButton ToolButton = (ToolBarButton)sender;
      string MediaPath = "http://localhost:90/Media/";
      string MediaFile = ToolButton.ClickArgument;
      TBActiveX1.Params.Add(new ActiveXParam("URL", MediaPath + MediaFile));
      TBActiveX1.Params.Add(new ActiveXParam("autoStart", "true"));
    }
  }
}
