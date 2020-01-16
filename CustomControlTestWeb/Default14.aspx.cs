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

public partial class Default14 : System.Web.UI.Page
{
  protected void Page_Load(object sender, EventArgs e)
  {
    if (!Page.ClientScript.IsClientScriptIncludeRegistered("popcalendar"))
    {
      Page.ClientScript.RegisterClientScriptInclude("popcalendar", "ControlJS/popcalendar.js");
    }
    if (!IsPostBack)
    {
      string strJS = "dateFormat='yyyy/mm/dd'; popUpCalendar(this, document.all('" + TextBox1.ClientID + "'), dateFormat,-1,-1);";
      Image1.Attributes.Add("onclick", strJS);

    }
  }
}
