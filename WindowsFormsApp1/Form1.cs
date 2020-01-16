using DataAccess.DB;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string Sql = "select * from ltrx..LAPLPC where CASE_NO1 =@caseNo1 and STATUS=@status ";
            //string Sql = "select * from ltrx..LAPLPC where CASE_NO1 ='00931060040' and STATUS='1' ";
            Dac DB = new Dac(dbkind.SQL_Server, "Inves_Flow");
            DataSet DS = new DataSet();
            DataTable UserDT = DB.RunSQL(Sql, "LAPLPC", "",new object[] { "00931060040","1" });
            int affectedRows = 0;
            DB.RunSQL(Sql, out affectedRows, "", new object[] { "00931060040", "1" });
            DB.RunSQL("select * from ltrx..LAPLPC where CASE_NO1 ='00931060040' and STATUS='1'", "");
            Sql += ";SELECT CASE_NO,* FROM LTRX..COMH10 where CASE_NO = @CASE_NO " +
                   "and data_dt = (select max(data_dt) FROM LTRX..COMH10 where CASE_NO = @CASE_NO1 )";
            Sql += ";select Collateral_Area_Kind,* from inves_flow..gage " +
                   "where [rece_no] in(@rece_no1 ,@rece_no2 ,@rece_no3 ,@rece_no4 )";
            DB.RunSQL(Sql, ref DS, "LAPLPC", "", new object[] { "00931060040", "1", "015410701181", "015410701181", "00480950231", "00660990068", "00660990068", "00930940067" });
        }
    }
}
