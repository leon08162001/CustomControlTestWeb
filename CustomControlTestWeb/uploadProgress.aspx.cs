using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using AjaxPro;

public partial class uploadProgress : UploadFile
{
    protected void Page_Load(object sender, EventArgs e)
    {
        AjaxPro.Utility.RegisterTypeForAjax(typeof(uploadProgress));
    }

    /// <summary>
    /// Long running background operation
    /// </summary>
    [AjaxMethod()]
    public bool DoWork()
    {
        System.Threading.Thread.Sleep(100);
        string path = Server.MapPath(@"~\Uploads\");
        string fileName = stic_fileName;
        FileInfo FileInfo = new FileInfo(Path.Combine(path, fileName));
        while(!FileInfo.Exists)
        {
            FileInfo = new FileInfo(Path.Combine(path, fileName));
        }
        bool result = false;
        try
        {
            long ContentLength = stic_ContentLength;
            while (FileInfo.Length < ContentLength)
            {
                System.Threading.Thread.Sleep(1);
                FileInfo = new FileInfo(Path.Combine(path, fileName));
            }
            result = true;
        }
        catch (Exception ex)
        {
            result = false;
            throw ex;
        }
        return result;
    }
}
