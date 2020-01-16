using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.IO;
using System.Threading;

/// <summary>
/// UploadFile 的摘要描述
/// </summary>
public class UploadFile : System.Web.UI.Page
{
    protected static long stic_ContentLength;
    protected static string stic_fileName;
    protected static bool stic_IsStartUpload = false;
    protected Thread[] threads;
    public UploadFile()
    {
        //
        // TODO: 在此加入建構函式的程式碼
        //
    }

  
}
