using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using AjaxPro;

public partial class Demo_ProgressBarDemo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        AjaxPro.Utility.RegisterTypeForAjax(typeof(Demo_ProgressBarDemo));
    }
    /// <summary>
        /// Long running background operation
        /// </summary>
    [AjaxMethod()]
    public bool DoWork(string[] Params)
    {
        string sName = Params[0];
        string sGender = Params[1];
        string sAge = Params[2];
        string sMessage = "姓名:" + sName + ";性別:" + sGender + ";年齡:" + sAge;
        bool Result = ExecuteWork(sMessage);
        return Result;
    }

    private bool ExecuteWork(string Message)
    {
        bool Result = false;
        string path = Server.MapPath("~");
        FileInfo FI = new FileInfo(Server.MapPath("~") + @"\temp.txt");
        StreamWriter SW;
        if (!FI.Exists)
        {
            SW = FI.CreateText();
            try
            {
                SW.WriteLine(Message);
                Result = true;
            }
            catch (Exception ex)
            {
                string s = ex.Message;
                Result = false;
            }
            finally
            {
                SW.Close();
            }
        }
        else
        {
            SW = new StreamWriter(Server.MapPath("~") + @"\temp.txt", true);
            try
            {
                SW.WriteLine(Message);
                Result = true;
            }
            catch (Exception ex)
            {
                string s = ex.Message;
                Result = false;
            }
            finally
            {
                SW.Close();
            }
        }
        System.Threading.Thread.Sleep(5000);
        return Result;
    }
}
