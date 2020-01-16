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
using System.IO;

public partial class Default10 : System.Web.UI.Page
{
	protected void Page_Load(object sender, EventArgs e)
	{
		if (!Page.IsPostBack)
		{

		}
	}
	protected void Button1_Click(object sender, EventArgs e)
	{
		string FilePath = Server.MapPath(@"Menuini\Menu.ini");
		string s = PublicFunc.GetIniInfo(FilePath, "Basic");
		StreamWriter SW = new StreamWriter(Server.MapPath(@"Menuini\test.ini"), true, System.Text.Encoding.GetEncoding(950));
		SW.WriteLine(s);
		SW.Close();
	}
}
