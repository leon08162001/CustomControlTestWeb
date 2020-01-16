using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading;
using System.ComponentModel;
using AjaxPro;

public partial class test8 : System.Web.UI.Page
{
    BackgroundWorker worker = new BackgroundWorker();
    private static int ProgressNum = 0;
    private static string ProgressResult = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        AjaxPro.Utility.RegisterTypeForAjax(typeof(test8));
        worker.DoWork += new DoWorkEventHandler(DoWork);
        worker.WorkerReportsProgress = true;
        worker.WorkerSupportsCancellation = true;
        worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(WorkerCompleted);
        worker.ProgressChanged += new ProgressChangedEventHandler(worker_ProgressChanged);
        //Button1.OnClientClick = "isInProgress();return false;";
    }

    public void Button1_Click(object sender, EventArgs e)
    {
        if (ProgressNum == 0)
        {
            Thread thread = new Thread(new ParameterizedThreadStart(worker.RunWorkerAsync));
            thread.Name = "worker";
            thread.Start();
            Image1.Visible = true;
            ProgressNum = 1;
            hidden_IsProgress.Value = "true";
        }
        else if (ProgressNum > 0 && ProgressNum < 100)
        {
            Image1.Visible = true;
            hidden_IsProgress.Value = "true";
            //Int32 percentage = Convert.ToInt32(ProgressNum * (Panel2.Width.Value) / 100);
            //Panel1.Width = Unit.Pixel(percentage);
            //Label1.Text = ProgressNum.ToString() + "%";
            //Label1.Text = "執行中請稍候---";
        }
        else if (ProgressNum == 100)
        {
            Int32 percentage = 0;
            Image1.Visible = false;
            hidden_IsProgress.Value = "false";
            Panel1.Width = Unit.Pixel(percentage);
            //Label1.Text = "";
            ProgressNum = percentage;
            //Label1.Text = "執行完成";
        }

        //if (Session["worker"] == null)
        //{
        //    Session["worker"] = worker;
        //    Session["page"] = this.Page;
        //    Thread thread = new Thread(new ParameterizedThreadStart(worker.RunWorkerAsync));
        //    //Session["thread"] = thread;
        //    thread.Start();
        //}
        //else
        //{
        //    Int32 percentage = Convert.ToInt32(ProgressNum * (Panel2.Width.Value) / 100);
        //    Panel1.Width = Unit.Pixel(percentage);
        //    Label1.Text = ProgressNum.ToString() + "%";
        //}
        
        // Calling the DoWork Method Asynchronouslyworker.RunWorkerAsync(); 
    }

    private static void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
    {
 	    ProgressNum = e.ProgressPercentage;

    }

    private static void WorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
    {
 	    ProgressNum =100;
    }

    private static void DoWork(object sender, DoWorkEventArgs e)
    {     
        // Long running background operation
        int i = 100;
        System.Threading.Thread.Sleep(20000);
        //for(int j=1;j<=i;j++)
        //{
        //    System.Threading.Thread.Sleep(1000);
        //    ((BackgroundWorker)sender).ReportProgress(j*100/100);
        //}
    }

    [AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
    public bool IsInProgress()
    {
        bool result = false;
        if (ProgressNum == 0)
        {
            Thread thread = new Thread(new ParameterizedThreadStart(worker.RunWorkerAsync));
            thread.Name = "worker";
            Session["thread"] = thread;
            thread.Start();
            ProgressNum = 1;
            result = true;
        }
        //else if (ProgressNum > 0 && ProgressNum < 100)
        else if ((Session["thread"] as Thread).IsAlive)
        {
            result = true;
        }
        else if ((Session["thread"] as Thread).IsAlive == false)
        //else if (ProgressNum == 100)
        {
            ProgressNum = 0;
            result = false;
        }
        return result;
    }
}
