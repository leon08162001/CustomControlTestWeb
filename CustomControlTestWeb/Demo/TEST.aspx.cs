using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Demo_TEST : System.Web.UI.Page
{
    public string htmlCode = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        HtmlEditor1.SetHighLevelSecurityForHtmlTags = true;
        htmlCode = HtmlEditor1.GetValue();
        string decodedCode = HtmlEditor1.GetDecodedValue();
    }
}