using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using APTemplate;

public partial class test5 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (!Page.IsPostBack)
        //{
            SqlConnection Conn = new SqlConnection(ConfigurationManager.ConnectionStrings["CSFDB"].ConnectionString);
            SqlDataAdapter Sda = new SqlDataAdapter("Select top 5000 * from dbo.db_ctmr_dds", Conn);
            DataSet DS = new DataSet();
            Sda.Fill(DS, "mutmr0745");
            DataTable DT0745 = DS.Tables["mutmr0745"];
            NewGridView1.DataSource = DT0745;
            NewGridView1.DataBind();
            //PageMaker1.PageSize = 15;

            
        //}
        
    }
}
