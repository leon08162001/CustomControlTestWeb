using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class Default21 : System.Web.UI.Page
{
  protected void Page_Load(object sender, EventArgs e)
  {
    if (!Page.IsPostBack)
    {
      DataSet LeftDS=new DataSet();
      SqlConnection Conn=new SqlConnection("Data Source=(local);Initial Catalog=BoocManage;User ID=sa;Password=6016");
      SqlDataAdapter DA=new SqlDataAdapter("Select BarCode from Product",Conn);
      DA.Fill(LeftDS,"AllBarCode");
      DataTable DT=LeftDS.Tables[0];
      ListBoxToListBox1.FirstListBox.DataSource = DT;
      ListBoxToListBox1.FirstListBox.DataTextField = "BarCode";
      ListBoxToListBox1.DataBind();
    }
  }
  protected void Btn_Confirm_Click(object sender, EventArgs e)
  {
    string FilteredBarCodes = "";
    for (int i = 0; i < ListBoxToListBox1.SecondListItems.Count; i++)
    {
      FilteredBarCodes += ListBoxToListBox1.SecondListItems[i].Value + ",";
    }
    FilteredBarCodes = FilteredBarCodes.Substring(0, FilteredBarCodes.Length - 1);
    DataList1.RepeatColumns = Convert.ToInt32(DDLColTimes.SelectedItem.Value);
    SqlConnection Conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Default"].ConnectionString);
    SqlCommand Cmd = new SqlCommand("PrintBarCodeOfProduct", Conn);
    Cmd.CommandType = CommandType.StoredProcedure;
    Cmd.Parameters.AddWithValue("@Count", Convert.ToInt32(DDLLabelTimes.SelectedItem.Value));
    Cmd.Parameters.AddWithValue("@FilteredBarCode", Convert.ToString(FilteredBarCodes));
    SqlDataAdapter Sda = new SqlDataAdapter(Cmd);
    DataSet DS = new DataSet();
    Sda.Fill(DS, "Product");
    DataTable Product = DS.Tables["Product"];
    DataList1.DataSource = Product;
    DataList1.DataBind();
    PageMaker1.RePaging();
  }
}
