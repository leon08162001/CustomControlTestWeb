using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class Default17 : System.Web.UI.Page
{
  protected string SourceCID = "";
  protected void Page_load(object sender, EventArgs e)
  {
    //Convert.ToDateTime("2008/11/11").AddYears(1911)
    if (!Page.IsPostBack)
    {
      SqlConnection Conn = new SqlConnection(ConfigurationManager.ConnectionStrings["CSFDB"].ConnectionString);
      SqlDataAdapter Sda = new SqlDataAdapter("Select top 500 * from mutmr0745 WHERE db_mutdtl_rbrno = '05'", Conn);
      DataSet DS = new DataSet();
      Sda.Fill(DS, "mutmr0745");
      DataTable DT0745 = DS.Tables["mutmr0745"];
      DataList1.DataSource = DT0745;
      DataList1.DataBind();
    }
    //SourceCID = TextBox_PopupWindow1.FirstTextBoxClientID;
    //TextBox_PopupWindow1.WindowUrl = "Default18.aspx?SourceCID=" + SourceCID;
  }
  
  protected void DataList1_ItemDataBound(object sender, DataListItemEventArgs e)
  {
    SourceCID = ((APTemplate.TextBox_PopupWindow)e.Item.FindControl("TextBox_PopupWindow1")).FirstTextBoxClientID;
    String S = ((APTemplate.TextBox_PopupWindow)e.Item.FindControl("TextBox_PopupWindow1")).Text;
    ((APTemplate.TextBox_PopupWindow)e.Item.FindControl("TextBox_PopupWindow1")).WindowUrl = "Default18.aspx?SourceCID=" + SourceCID + "&Val=" + S;
  }
}
