using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxPro;

public partial class test10 : System.Web.UI.Page
{
    static int _Progress;
    static int i;
    protected void Page_Load(object sender, EventArgs e)
    {
        //AjaxPro.Utility.RegisterTypeForAjax(typeof(ImportData));
        AjaxPro.Utility.RegisterTypeForAjax(typeof(test10));
    }

    public int Progress
    {
        get
        {
            return _Progress;
        }

        set
        {
            _Progress = value;
        }
    }


    [AjaxMethod()]
    public string exportRecords()
    {
        //int i;
        for (i = 0; i <= 100; i++)
        {
            System.Threading.Thread.Sleep(100);
            //this.Progress = i;
        }
        return "Export finished";
    }

    [AjaxMethod()]
    public int currentProgress()
    {
        this.Progress = i;
        return this.Progress;
    }
}
