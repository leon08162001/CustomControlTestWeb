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

public partial class Default13 : System.Web.UI.Page
{
  protected void Page_Load(object sender, EventArgs e)
  {
    DateTime s = PopupCalendar2.FirstDate;
  }
  protected void Button1_Click(object sender, EventArgs e)
  {
    string a = TextBox1.Text;
    int CurrentIdx=TabsView1.CurrentTabIndex;
  }
  protected void Button2_Click(object sender, EventArgs e)
  {
    int CurrentIdx = TabsView1.CurrentTabIndex;
  }
}
