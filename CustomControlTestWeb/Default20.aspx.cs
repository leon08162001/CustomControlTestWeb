using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class Default20 : System.Web.UI.Page
{
  protected void Page_Load(object sender, EventArgs e)
  {
    if (!Page.IsPostBack)
    {
      SqlConnection Conn = new SqlConnection(ConfigurationManager.ConnectionStrings["CSFDB"].ConnectionString);
      SqlDataAdapter Sda = new SqlDataAdapter("Select top 20 * from mutmr0745 WHERE db_mutdtl_rbrno = '05'", Conn);
      DataSet DS = new DataSet();
      Sda.Fill(DS, "mutmr0745");
      DataTable DT0745 = DS.Tables["mutmr0745"];
      ListView1.DataSource = DT0745;
      ListView1.DataBind();
    }
  }
}
