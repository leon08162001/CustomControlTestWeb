using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxPro;

public partial class test11 : System.Web.UI.Page
{
    static int i;
    static DateTime StartTime;
    static DateTime EndTime;
    protected void Page_Load(object sender, EventArgs e)
    {
        AjaxPro.Utility.RegisterTypeForAjax(typeof(test11));
    }

    [AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
    public bool ProgressBar2_DoWork()
    {
        bool result = false;
        // Long running background operation
        try
        {
            StartTime = DateTime.Now;
            EndTime = StartTime.AddSeconds(100);
                for (i = 0; i <= 100; i++)
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
        int Progress = Convert.ToInt32(UsedSeconds * 100 / 100);
        //if (Progress == 100)
        //{
           
        //}
        return Progress;
    }

}
