using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Web;
using System.Web.UI;
using System.IO;
using System.Web.UI.WebControls;
using System.Drawing;
using Microsoft.Security.Application;

namespace APTemplate
{
    [ToolboxData("<{0}:HtmlEditor runat=server></{0}:HtmlEditor>")]
    [ToolboxBitmap(typeof(HtmlEditor), "Resources.Control_HtmlEditor.bmp")]
    public class HtmlEditor : WebControl
    {
        /// <summary>
        /// This function sets a value to the text editor like some text or html data from database or other resources for edition activities
        /// </summary>
        /// <param name="Value"></param>
        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue("")]
        [Localizable(true)]
        public void SetValue(string Value)
        {
            string script = "disableElement(document.getElementById('textToolsContainer'));;document.getElementById('textEditor').style.display='none';isOnSourceMode=true;isOndesignMode=false;document.getElementById('sourceTxt').style.display='block';document.getElementById('sourceTxt').value='" + Value + "'";
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "valueSetterScript", script, true);
        }

        /// <summary>
        /// This property can be seted as True or false for get high level secured html code from editor
        /// </summary>
        public bool SetHighLevelSecurityForHtmlTags
        {
            set
            {
                Security._SetHighLevelSecurityForHtmlTags = value;
            }
        }
        /// <summary>
        /// This function gets the data which has been provided in text editor for some purposes such as : demonstration in a specified page,storing in database,etc.
        /// </summary>
        /// <returns>string values</returns>
        public string GetValue()
        {
            return this.Page.Server.HtmlDecode(AntiXss.HtmlAttributeEncode(GetDecodedValue()));
        }

        /// <summary>
        /// Gets the Decoded value of a value which is encoded because of some security reasons
        /// </summary>
        /// <returns></returns>
        public string GetDecodedValue()
        {
            SourceActions Source = new SourceActions();
            Source.SourceProvider(this.Page);
            return this.Page.Server.HtmlDecode(Source._SourceCode);
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            Page.Form.Enctype = "multipart/form-data";
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnInit(e);
            Registrar.RegisterScripts(new List<string> { "APTemplate.Resources.HtmlEditor.js.loading.js", "APTemplate.Resources.HtmlEditor.js.testJS.js", "APTemplate.Resources.HtmlEditor.js.Encoder.js", "APTemplate.Resources.HtmlEditor.js.slider.js" }, this.Page, this.GetType());
            string InitializerJS = Page.ClientScript.GetWebResourceUrl(this.GetType(), "APTemplate.Resources.HtmlEditor.js.Initializer.js");
            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "RichText", "<script language=\"javascript\" src='" + InitializerJS + "'></script>");
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            string Title = "<title></title>";

            string Csslink = "<link rel='stylesheet' type='text/css' href='" + Page.ClientScript.GetWebResourceUrl(this.GetType(), "APTemplate.Resources.HtmlEditor.EditorStyles.Style.css") + "' />";
            string RichTextStyle = "<link rel='stylesheet' type='text/css' href='" + Page.ClientScript.GetWebResourceUrl(this.GetType(), "APTemplate.Resources.HtmlEditor.EditorStyles.textEditor.css") + "' />";
            string SliderStyle = "<link rel='stylesheet' type='text/css' href='" + Page.ClientScript.GetWebResourceUrl(this.GetType(), "APTemplate.Resources.HtmlEditor.EditorStyles.slider.css") + "' />";
            this.Page.Header.InnerHtml = Title + Csslink + RichTextStyle + SliderStyle;
        }

        protected override void RenderContents(HtmlTextWriter output)
        {
            if (this.Page.IsPostBack)
            {
                //if there is an uploaded file
                HttpFileCollection UploadFile = this.Page.Request.Files;
                for (int i = 0; i < UploadFile.Count; i++)
                {
                    HttpPostedFile file = UploadFile[i];
                    string FileName = Path.GetFileName(file.FileName);
                    string StrPath = this.Page.MapPath("~/Uploads/");
                    try
                    {
                        file.SaveAs(Path.Combine(StrPath, FileName));
                    }
                    catch (DirectoryNotFoundException DirectoryNullException)
                    {
                        output.Write(DirectoryNullException.Message);
                    }
                }
            }
            Page.ClientScript.GetPostBackEventReference(this.Page, string.Empty);
            HtmlSourceInitializer.InitializeHtmlSource(this.Page);
            output.Write(HtmlSourceInitializer.RichTextHtmlSource.ToString());
        }
    }
}