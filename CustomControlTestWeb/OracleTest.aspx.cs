using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Oracle.DataAccess.Client;
using newDac = DataAccess.DB.Dac;

public partial class OracleTest : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        newDac Dac = new newDac(dbkind.Oracle);
        string sSql = "select wmpromorole,wmecrecord from wmprodorder where wmclientid='0080119' order by BDADDDATE desc";
        string sConn = "Data Source=IPGLAB.TW.YUANTA.COM;Persist Security Info=True;Password=oawmtuat@i$$;User ID=WMT";
        sConn = System.Configuration.ConfigurationManager.ConnectionStrings["WMT_Oracle"].ConnectionString;
        try
        {
            DataTable DT = Dac.RunSQL(sSql, "wmprodorder", sConn);
            foreach (DataRow dr in DT.Rows)
            {
                byte[] sXml = dr["wmecrecord"] as byte[];
                string ss = "p";
            }
            string s = "";
                     
                    //IDataParameter para1 = new OracleParameter("p1", Oracledbkind.Varchar2, ParameterDirection.Input);
                    //IDataParameter para2 = new OracleParameter("p2", Oracledbkind.Varchar2, ParameterDirection.Input);
                    //IDataParameter para3 = new OracleParameter("p3", Oracledbkind.Varchar2, ParameterDirection.Input);
                    //IDataParameter para4 = new OracleParameter("p4", Oracledbkind.RefCursor, ParameterDirection.Output);
                    //para1.Value = "6010";
                    //para2.Value = "WMT20110412000006";
                    //para3.Value = "01";
                    //IDataParameter[] paras = new IDataParameter[] { para1, para2, para3, para4};
                    //DataTable DT = Dac.RunProcedure("PACK_WM_QUERY.PR_GET_ORDERS_DETAIL", paras, "a", sConn);
                    //string s = "";
        }
        catch (Exception ex)
        {
            string sEx = ex.Message;
        }
        finally
        {
            Dac.Dispose();
        }
    }
}
