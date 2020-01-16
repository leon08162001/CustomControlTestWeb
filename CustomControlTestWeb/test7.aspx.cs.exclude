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
using CipherHelper;
using XMLHelper;

public partial class test7 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
      if (!Page.IsPostBack)
      {
        PopupCalendar1.FirstDate = System.DateTime.Now;
        CalendarRange1.FirstDate = System.DateTime.Now.AddDays(-1);
        CalendarRange1.SecondDate = System.DateTime.Now;
      }
      String CalendarDate = PopupCalendar1.FirstDate.ToString("yyyy/MM/dd HH:mm:ss");
      byte[] byt = B64.AesEncrypt(System.Text.Encoding.UTF8.GetBytes(CalendarDate), 1, null);
      string EncodeS = System.Text.Encoding.UTF8.GetString(byt, 0, byt.Length);
      byte[] byt1 = B64.AesDecrypt(byt, 1, null);
      string DecodeS = System.Text.Encoding.UTF8.GetString(byt1, 0, byt1.Length);

      System.IO.StreamReader SR = new System.IO.StreamReader(@"D:\XMLTEST\NTFXG4400_Rq2.xml", System.Text.Encoding.GetEncoding(950));
      String AllContents = SR.ReadToEnd();
      SR.Close();

      XmlRecord XmlRec = new XmlRecord(AllContents, "SvcRq/Rq");
      //DataTable DT = XmlRec.ToDataTable("Acct='00100100016384' and Name='w'", "");
      DataTable DT = XmlRec.ToDataTable();
      DataTable DT1 = XmlRec.BottomDataTable(10);
     
      XmlRec.GoToRecord(2);
      String Val = XmlRec.get_Fields("Type");
      DataSet DS = XmlRec.DataSet;
      int RowsCount = DT.Rows.Count;
      NewGridView1.DataSource = DT1;
      NewGridView1.DataBind();
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        int count = NewGridView1.Columns.Count;
    }
}
