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
    public bool ProgressBar1_DoWork()
    {
        while (stic_IsStartUpload == false)
        {
            System.Threading.Thread.Sleep(1);
        }
        stic_IsStartUpload = false;
        bool result = false;
        try
        {
            string path = @"F:\自訂控制項\WebCustomControlsTestWeb\Uploads\";
            string fileName = stic_fileName;
            long ContentLength = stic_ContentLength;
            if (fileName != "")
            {

                FileInfo FileInfo = new FileInfo(Path.Combine(path, fileName));
                while (FileInfo.Length < ContentLength)
                {
                    System.Threading.Thread.Sleep(1);
                    FileInfo = new FileInfo(Path.Combine(path, fileName));
                }
                result = true;
            }
            else if (fileName == "")
            {
                result = false;
                throw new Exception("尚未指定入上傳檔案!");
            }
        }
        catch (Exception ex)
        {
            result = false;
            throw ex;
        }
        return result;
    }
}
