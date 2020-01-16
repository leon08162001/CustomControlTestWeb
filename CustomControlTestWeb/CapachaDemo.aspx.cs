using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class test11 : System.Web.UI.Page
{


  protected void Page_Load(object sender, EventArgs e)
  {
    BarCode1.BarCodeText = "4710088470041";
    BarCode1.Attributes["onclick"] = "window.alert('" + BarCode1.BarCodeText + "')";
    CollapsiblePanel1.Text = "<A href='" + Page.Request.RawUrl + "'>Default100.aspx</a>";

  }
}
