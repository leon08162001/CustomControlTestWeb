using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.Design;
using System.Drawing;
using System.Drawing.Design;

namespace APTemplate
{
    [DefaultProperty("")]
    [ToolboxData("<{0}:PdfViewer runat=server></{0}:PdfViewer>")]
    [ToolboxBitmap(typeof(PdfViewer), "Resources.Control_PDF.bmp")]
    public class PdfViewer : WebControl
    {
        private string mFilePath;

        #region Public Properties & Methods

        [Category("自訂")]
        [Browsable(true)]
        [Description("指定PDF檔案的路徑。")]
        [Editor(typeof(UrlEditor), typeof(UITypeEditor)), UrlProperty()]
        public string FilePath
        {
            get
            {
                return mFilePath;
            }
            set
            {
                if (value == string.Empty)
                {
                    mFilePath = string.Empty;
                }
                else
                {
                    int tilde = -1;
                    tilde = value.IndexOf('~');
                    if (tilde != -1)
                    {
                        mFilePath = value.Substring((tilde + 2)).Trim();
                    }
                    else
                    {
                        mFilePath = value;
                    }
                }
            }
        }

        #endregion

        #region Protected Properties & Methods

        protected override void RenderContents(HtmlTextWriter writer)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("<iframe src=" + FilePath.ToString() + " ");
                sb.Append("width=" + Width.ToString() + " height=" + Height.ToString() + ">");
                sb.Append("<a href=" + FilePath.ToString() + "</a>");
                sb.Append("<a href=" + FilePath.ToString() + "</a> ");
                sb.Append("</iframe>");

                writer.RenderBeginTag(HtmlTextWriterTag.Div);
                writer.Write(sb.ToString());
                writer.RenderEndTag();
            }
            catch (Exception ex)
            {
                // with no properties set, this will render "Display PDF Control" in a
                // a box on the page
                writer.RenderBeginTag(HtmlTextWriterTag.Div);
                writer.Write("PDF瀏覽控制項發生錯誤:" + ex.Message);
                writer.RenderEndTag();
            }  // end try-catch
        }

        #endregion

    }
}
