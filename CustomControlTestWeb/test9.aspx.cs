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
using APTemplate;
using AjaxPro;

public partial class test9 : System.Web.UI.Page
{
    static int i;
    static DateTime StartTime;
    static DateTime EndTime;
    protected void Page_Load(object sender, EventArgs e)
    {
        //AjaxPro.Utility.RegisterTypeForAjax(typeof(AjaxServer.ImportData));
        AjaxPro.Utility.RegisterTypeForAjax(typeof(test9));
        string strjson = @"{""name"":""ctpan"",""company"":""sinopac""}";
        string s = "{\"name\":\"ctpan\",\"company\":\"sinopac\"}";
    }

    [AjaxMethod()]
    public bool ProgressBar1_DoWork()
    {
        bool result = false;
        // Long running background operation
        int i = 3;
        try
        {
            for (int j = 1; j <= i; j++)
            {
                System.Threading.Thread.Sleep(1000);
            }
            result = true;
        }
        catch (Exception ex)
        {
            result = false;
        }
        return result;
    }

    [AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
    public bool ProgressBar2_DoWork()
    {
        bool result = false;
        // Long running background operation
        try
        {
            StartTime = DateTime.Now;
            EndTime = StartTime.AddSeconds(10);
            for (i = 0; i <= 10; i++)
            {
                System.Threading.Thread.Sleep(1000);
            }
            result = true;
        }
        //catch (System.IO.DirectoryNotFoundException direx)
        //{
        //    throw direx ;
        //    result = false;
        //}
        catch (Exception ex)
        {
            throw ex;
            result = false;
        }
        return result;
    }

    [AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
    public int ProgressBar2_StatisticProgress()
    {
        //int Progress = i;
        DateTime CurrentTime = DateTime.Now;
        TimeSpan StartTimeSpan = new TimeSpan(StartTime.Ticks);
        TimeSpan CurrentTimeSpan = new TimeSpan(CurrentTime.Ticks);
        Double UsedSeconds = CurrentTimeSpan.Subtract(StartTimeSpan).TotalSeconds;
        int Progress = Convert.ToInt32(UsedSeconds * 100 / 10);
        return Progress;
    }

}
