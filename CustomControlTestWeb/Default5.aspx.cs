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

public partial class Default5 : System.Web.UI.Page
{
  protected void Page_Load(object sender, EventArgs e)
  {
    TableCell1.Attributes["style"] += "background-image:url('images/PageTab_left.gif');";
    TableCell2.Attributes["style"] += "background-image:url('images/PageTab_center.gif');";
    TableCell3.Attributes["style"] += "background-image:url('images/PageTab_right.gif');";
    TableCell4.Attributes["style"] += "background-image:url('images/PageTab_left.gif');";
    TableCell5.Attributes["style"] += "background-image:url('images/PageTab_center.gif');";
    TableCell6.Attributes["style"] += "background-image:url('images/PageTab_right.gif');";
    TableCell7.Attributes["style"] += "background-image:url('images/PageTab_left.gif');";
    TableCell8.Attributes["style"] += "background-image:url('images/PageTab_center.gif');";
    TableCell9.Attributes["style"] += "background-image:url('images/PageTab_right.gif');";
  }
}
