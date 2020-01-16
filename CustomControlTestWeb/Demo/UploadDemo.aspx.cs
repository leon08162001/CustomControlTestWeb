using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UploadDemo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //this.Upload2.UploadDir = this.Upload2.UploadDir + @"\20120906";
        //this.Upload3.UploadDir = this.Upload3.UploadDir + @"\20120906";
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        //this.Upload1.UploadDir = @"\\10.1.4.20\F$\LeonLee\";
        //this.Upload2.UploadDir = this.Upload2.UploadDir + @"\LeonLee";
        //this.Upload3.UploadDir = this.Upload3.UploadDir + @"\LeonLee";
        //this.Upload4.UploadDir = this.Upload4.UploadDir + @"\LeonLee";
        //this.Upload5.UploadDir = this.Upload5.UploadDir + @"\LeonLee";
        //this.Upload6.UploadDir = this.Upload6.UploadDir + @"\LeonLee";
    }
}
