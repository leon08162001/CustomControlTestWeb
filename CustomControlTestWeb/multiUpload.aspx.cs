using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using System.Threading;

public partial class multiUpload : UploadFile
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void UploadData(object sender, EventArgs e)
    {
        try
        {
            if (this.IsPostBack)
            {
                stic_ContentLength = this.fileUpload.PostedFile.ContentLength;
                long ContentLength = stic_ContentLength;
                stic_fileName = Path.GetFileName(this.fileUpload.PostedFile.FileName);
                stic_IsStartUpload = true;
                string fileName = stic_fileName;
                if (this.fileUpload.PostedFile != null && ContentLength > 0)
                {
                    //build the local path where upload all the files
                    int UploadedLength = 0;
                    string path = this.Server.MapPath(@"Uploads");

                    //set the buffer size to something larger.
                    //the smaller the buffer the longer it will take to download, 
                    //but the more precise your progress bar will be.
                    int bufferSize = 1;
                    byte[] buffer = new byte[bufferSize];

                    //Writing the byte to disk
                    using (FileStream fs = new FileStream(Path.Combine(path, fileName), FileMode.Create, FileAccess.ReadWrite))
                    {
                        //Aslong was we haven't written everything ...
                        while (UploadedLength < ContentLength)
                        {
                            //Fill the buffer from the input stream
                            int bytes = this.fileUpload.PostedFile.InputStream.Read(buffer, 0, bufferSize);
                            //Writing the bytes to the file stream
                            fs.Write(buffer, 0, bytes);
                            //Update the number the webservice is polling on to the session
                            UploadedLength += bytes;
                        }
                    }
                }
                else
                {
                    //Call parent page know we have processed the uplaod
                    //const string js = "window.parent.onComplete(4, 'There was a problem with the file.','','0 of 0 Bytes');";
                    //ScriptManager.RegisterStartupScript(this, typeof(test13), "progress", js, true);
                }
            }
        }
        catch (Exception ex)
        {
            //throw new Exception(ex.Message);
        }
    }
}
