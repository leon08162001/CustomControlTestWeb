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

public partial class Default6 : System.Web.UI.Page
{
	protected void Page_Load(object sender, EventArgs e)
	{
		if (!Page.IsPostBack)
		{
			SqlConnection conn = new SqlConnection("Data Source=(local);Initial Catalog=BoocManage;User ID=sa;Password=6016");
			SqlCommand cmd = new SqlCommand("select * from Customers where CustomerID<=3", conn);
			SqlCommand cmd1 = new SqlCommand("select * from Customers where CustomerID>3", conn);
			SqlDataAdapter DA = new SqlDataAdapter(cmd);
			SqlDataAdapter DA1 = new SqlDataAdapter(cmd1);
			DataTable DT = new DataTable();
			DataTable DT1 = new DataTable();
			DA.Fill(DT);
			DA1.Fill(DT1);
			ListBoxToListBox1.FirstListBox.DataSource = DT;
			ListBoxToListBox1.FirstListBox.DataValueField = "CustomerID";
			ListBoxToListBox1.FirstListBox.DataTextField = "CustomerName";
			ListBoxToListBox1.FirstListBox.DataBind();
			ListBoxToListBox1.SecondListBox.DataSource = DT1;
			ListBoxToListBox1.SecondListBox.DataValueField = "CustomerID";
			ListBoxToListBox1.SecondListBox.DataTextField = "CustomerName";
			ListBoxToListBox1.SecondListBox.DataBind();

			ListBoxToListBox2.FirstListBox.DataSource = DT;
			ListBoxToListBox2.FirstListBox.DataValueField = "CustomerID";
			ListBoxToListBox2.FirstListBox.DataTextField = "CustomerName";
			ListBoxToListBox2.FirstListBox.DataBind();
			ListBoxToListBox2.SecondListBox.DataSource = DT1;
			ListBoxToListBox2.SecondListBox.DataValueField = "CustomerID";
			ListBoxToListBox2.SecondListBox.DataTextField = "CustomerName";
			ListBoxToListBox2.SecondListBox.DataBind();
		}
	}
}
