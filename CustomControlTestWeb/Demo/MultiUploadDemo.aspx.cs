using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using AjaxPro;

public partial class Demo_MultiUploadDemo : System.Web.UI.Page
{
    protected void Button2_Click(object sender, EventArgs e)
    {
        MultiUpload1.UploadNmbers += 1;
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        AjaxPro.Utility.RegisterTypeForAjax(typeof(APTemplate.AjaxUploadHandler));
    }
    protected void MultiUpload1_MultiUploadFilesFinished(object sender, APTemplate.MultiUploadFilesFinishedEventArgs e)
    {
        List<FileInfo> UploadedFiles = e.UploadedFiles;
    }
}