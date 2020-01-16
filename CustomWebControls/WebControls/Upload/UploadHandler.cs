using System;
using System.Web;
using System.Web.UI;
using System.Web.Compilation;
using System.IO;
using System.Text;
using System.Web.UI.WebControls;
using System.Web.Caching;
using System.Drawing;
using System.Collections.Generic;

namespace APTemplate
{
    public class UploadHandler : IHttpHandler, System.Web.SessionState.IRequiresSessionState
    {
        /// <summary>
        /// 您必須在 Web 的 web.config 檔案中設定這個處理常式
        /// 並且向 IIS 註冊，然後才可以使用它。如需詳細資訊，
        ///請參閱下列連結: http://go.microsoft.com/?linkid=8101007
        /// </summary>
        #region IHttpHandler Members

        HttpApplication app;

        public bool IsReusable
        {
            // 如果 Managed 處理常式不可重複使用於其他要求，就傳回 false。
            // 如果保存了每個要求的某些狀態資訊，通常這將會是 false。
            get { return true; }
        }

        public void ProcessRequest(HttpContext context)
        {
            app = context.ApplicationInstance;
            string UploadId = app.Context.Request.QueryString["id"];
            bool IsWithProgress = (bool)app.Context.Cache[UploadId + "_IsWithProgress"];
            string sSourcePath = context.Request.UrlReferrer.GetLeftPart(UriPartial.Path);
            if (IsWithProgress)
            {
                context.Response.Write(GetUploadHtml(IsWithProgress));
            }
            else
            {
                context.Response.Write(GetUploadHtml(false));
            }
            if (sSourcePath.IndexOf("Upload.aspx") != -1)  //
            {
                UploadFile(context);
            }
        }

        protected string GetUploadHtml(bool HasProgress)
        {
            string UploadId = app.Context.Request.QueryString["id"];
            string UploadDir = (string)app.Context.Cache[UploadId + "_UploadDir"];
            string ProgressText = (string)app.Context.Cache[UploadId + "_ProgressText"];
            Font ProgressTextFont = (Font)app.Context.Cache[UploadId + "_ProgressTextFont"];
            Unit ProgressWidth = (Unit)app.Context.Cache[UploadId + "_ProgressWidth"];
            string ProgressImageUrl = (string)app.Context.Cache[UploadId + "_ProgressImageUrl"];
            bool IsWithProgressPercent = (bool)app.Context.Cache[UploadId + "_IsWithProgressPercent"];
            string ProgressPercentPageUrl = (string)app.Context.Cache[UploadId + "_ProgressPercentPageUrl"];
            string ScriptMethodNameForProgressPercent = (string)app.Context.Cache[UploadId + "_ScriptMethodNameForProgressPercent"];
            bool IsNeedConfirmMessage = (bool)app.Context.Cache[UploadId + "_IsNeedConfirmMessage"];
            bool IsShowUploadButton = (bool)app.Context.Cache[UploadId + "_IsShowUploadButton"];
            string UploadButtonStyle = IsShowUploadButton ? "" : "display:none;";
            string Guid = System.Guid.NewGuid().ToString();

            string FontWeight = ProgressTextFont.Bold ? "bold" : "normal";
            string FontStyle = ProgressTextFont.Italic ? "italic" : "normal";
            string TextDecoration = ProgressTextFont.Underline ? "underline" : "none";

            StringBuilder sHtml = new StringBuilder();
            sHtml.Append("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">" + Environment.NewLine);
            sHtml.Append("<html xmlns=\"http://www.w3.org/1999/xhtml\">" + Environment.NewLine);
            sHtml.Append("<head><title></title>" + Environment.NewLine);
            sHtml.Append(GetScript(HasProgress));
            sHtml.Append("</head>" + Environment.NewLine);
            sHtml.Append("<body style=\"margin-left: 0px; margin-top: 0px; margin-bottom: 0px; margin-right: 0px;\">" + Environment.NewLine);
            sHtml.Append("<form name=\"form1\" method=\"post\" action=\"Upload.aspx\" id=\"form1\" enctype=\"multipart/form-data\">" + Environment.NewLine);
            sHtml.Append("<div>" + Environment.NewLine);
            sHtml.Append(" <table cellpadding=\"0\" cellspacing=\"0\">" + Environment.NewLine);
            sHtml.Append("<tr>" + Environment.NewLine);
            sHtml.Append("<td>" + Environment.NewLine);
            sHtml.Append("<input type=\"file\" name=\"fileUpload\" id=\"fileUpload\" style=\"height:22px;\" />" + Environment.NewLine);
            sHtml.Append("</td>" + Environment.NewLine);
            sHtml.Append("<td>" + Environment.NewLine);
            if (IsWithProgressPercent)
            {
                if (IsNeedConfirmMessage)
                {
                    //sHtml.Append("<input id=\"btn_upload\" type=\"button\" value=\"上傳\" onclick=\"if(doConfirm()==true){window.frames['FrameProgress_" + UploadId + "']." + ScriptMethodNameForProgressPercent + "();doUpload();}\" />" + Environment.NewLine);
                    sHtml.Append("<input id=\"btn_upload\" type=\"button\" style=\"" + UploadButtonStyle + "\" value=\"上傳\" onclick=\"if(doConfirm()==true){window.frames['FrameProgress_" + UploadId + "']." + ScriptMethodNameForProgressPercent + "();doUpload();}\" />" + Environment.NewLine);
                }
                else
                {
                    //sHtml.Append("<input id=\"btn_upload\" type=\"button\" value=\"上傳\" onclick=\"window.frames['FrameProgress_" + UploadId + "']." + ScriptMethodNameForProgressPercent + "();doUpload();\" />" + Environment.NewLine);
                    sHtml.Append("<input id=\"btn_upload\" type=\"button\" style=\"" + UploadButtonStyle + "\" value=\"上傳\" onclick=\"window.frames['FrameProgress_" + UploadId + "']." + ScriptMethodNameForProgressPercent + "();doUpload();\" />" + Environment.NewLine);
                }
            }
            else
            {
                if (IsNeedConfirmMessage)
                {
                    //sHtml.Append("<input id=\"btn_upload\" type=\"button\" value=\"上傳\" onclick=\"if(doConfirm()==true){doUpload();}\" />" + Environment.NewLine);
                    sHtml.Append("<input id=\"btn_upload\" type=\"button\" style=\"" + UploadButtonStyle + "\" value=\"上傳\" onclick=\"if(doConfirm()==true){doUpload();}\" />" + Environment.NewLine);
                }
                else
                {
                    //sHtml.Append("<input id=\"btn_upload\" type=\"button\" value=\"上傳\" onclick=\"doUpload();\" />" + Environment.NewLine);
                    sHtml.Append("<input id=\"btn_upload\" type=\"button\" style=\"" + UploadButtonStyle + "\" value=\"上傳\" onclick=\"doUpload();\" />" + Environment.NewLine);
                }
            }
            sHtml.Append("<input type=\"submit\" name=\"btn_serverUpload\" value=\"上傳\" id=\"btn_serverUpload\" style=\"display: none;\" />" + Environment.NewLine);
            sHtml.Append("<input id=\"hdnGuid\" name=\"hdnGuid\" type=\"hidden\" value=\"" + Guid + "\" />" + Environment.NewLine);
            sHtml.Append("<span id=\"errorMsg\" style=\"font-family:" + ProgressTextFont.Name + ";font-size:" + ProgressTextFont.Size.ToString() + "pt;font-weight:" + FontWeight + ";font-style:" + FontStyle + ";text-decoration:" + TextDecoration + ";\"></span>");
            if (HasProgress)
            {
                sHtml.Append("<div id=\"ProgressBar1_divProgressBar\" style=\"display:none;text-align:center;position:relative;border-color:#FFFF00;border-width:2px;border-style:Groove;padding-left:0px;padding-right:0px;padding-top:0px;padding-bottom:0px;background-color:#FFFF00;color:#000000;\">" + Environment.NewLine);
                sHtml.Append("<img id=\"ImgProgressBar\" src=\"" + app.Context.Request.ApplicationPath + "/" + ProgressImageUrl + "\" style=\"border-width:0px;\" />" + Environment.NewLine);
                sHtml.Append("<span id=\"ProgressBar1_progressText\" style=\"font-family:" + ProgressTextFont.Name + ";font-size:" + ProgressTextFont.Size.ToString() + "pt;font-weight:" + FontWeight + ";font-style:" + FontStyle + ";text-decoration:" + TextDecoration + ";\">" + ProgressText + "</span>" + Environment.NewLine);
                sHtml.Append("</div>" + Environment.NewLine);
            }
            if (IsWithProgressPercent)
            {
                sHtml.Append("<iframe id=\"FrameProgress_" + UploadId + "\" name=\"FrameProgress_" + UploadId + "\" frameborder=\"0\" width=\"280\" height=\"20\" marginwidth=\"0\" marginheight=\"0\" scrolling=\"no\" style=\"text-align:center;vertical-align:text-top;\" src=\"" + app.Context.Request.ApplicationPath + "/" + ProgressPercentPageUrl + "?uploadIframeId=Frame_" + UploadId + "\"></iframe>" + Environment.NewLine);
            }            
            sHtml.Append("</td>" + Environment.NewLine);
            sHtml.Append("</tr>" + Environment.NewLine);
            sHtml.Append("</table>" + Environment.NewLine);
            sHtml.Append("</div>" + Environment.NewLine);
            sHtml.Append("</form>" + Environment.NewLine);
            sHtml.Append("</body>" + Environment.NewLine);
            sHtml.Append("</html>" + Environment.NewLine);
            return sHtml.ToString();
        }

        protected void UploadFile(HttpContext context)
        {
            string sGuid = app.Context.Request.Form["hdnGuid"];
            try
            {
                string UploadId = app.Context.Request.QueryString["id"];
                string UploadDir = (string)app.Context.Cache[UploadId + "_UploadDir"];
                bool IsUseVirtualPath = (bool)app.Context.Cache[UploadId + "_IsUseVirtualPath"];
                string NofileUploadMessage = (string)app.Context.Cache[UploadId + "_NofileUploadMessage"];
                List<FileFilterItem> FileFilterItems = (List<FileFilterItem>)app.Context.Cache[UploadId + "_FileFilterItems"];
                if (context.Request.Files.Count < 1)
                {
                    NofileUploadMessage = context.Server.HtmlEncode(NofileUploadMessage.Replace(@"\", @"\\"));
                    context.Response.Write(NofileUploadMessage);
                }
                else if (context.Request.Files.Count >= 1)
                {
                    for (int i = 0; i < context.Request.Files.Count; ++i)
                    {
                        HttpPostedFile file = context.Request.Files[i];
                        if (file.FileName != "")
                        {
                            FileInfo FileInfo = new FileInfo(file.FileName);
                            //app.Context.Cache[sGuid + "_FileLength"] = file.ContentLength;
                            //app.Context.Cache[sGuid + "_UploadFile"] = IsUseVirtualPath ? Path.Combine(context.Server.MapPath(@"~\" + UploadDir), FileInfo.Name) : Path.Combine(@UploadDir, FileInfo.Name);
                            HttpContext.Current.Cache.Insert(sGuid + "_FileLength", file.ContentLength, null, DateTime.Now.AddMinutes(600), Cache.NoSlidingExpiration);
                            HttpContext.Current.Cache.Insert(sGuid + "_UploadFile", IsUseVirtualPath ? Path.Combine(context.Server.MapPath(@"~\" + UploadDir), FileInfo.Name) : Path.Combine(@UploadDir, FileInfo.Name), null, DateTime.Now.AddMinutes(600), Cache.NoSlidingExpiration);
                            bool IsAllowedUpload = false;
                            foreach (FileFilterItem FileFilterItem in FileFilterItems)
                            {
                                string[] FileItems = FileFilterItem.Value.Split(new char[] { '|' });
                                foreach (string Item in FileItems)
                                {
                                    string val = Item.Replace("*", "");
                                    if (file.FileName.IndexOf(val, StringComparison.CurrentCultureIgnoreCase) != -1)
                                    {
                                        IsAllowedUpload = true;
                                        break;
                                    }
                                    else
                                    {
                                        continue;
                                    }
                                }
                                if (IsAllowedUpload)
                                {
                                    break;
                                }
                                else
                                {
                                    continue;
                                }

                            }
                            string ErrorMsg = "";
                            if (!IsAllowedUpload)
                            {
                                ErrorMsg = context.Server.HtmlEncode("附檔名為" + file.FileName.Substring(file.FileName.Length - 4) + "的檔案禁止上傳");
                                context.Response.Write("<script>document.getElementById(\"errorMsg\").innerHTML=\"" + ErrorMsg + "\";</script>" + Environment.NewLine);
                                return;
                            }
                            //build the local path where upload all the files
                            int UploadedLength = 0;
                            string path = IsUseVirtualPath ? context.Server.MapPath(@"~\" + UploadDir) : @UploadDir;
                            int bufferSize = 10;
                            byte[] buffer = new byte[bufferSize];
                            string FileName = file.FileName.Substring(file.FileName.LastIndexOf(@"\") + 1);
                            //using (FileStream fs = new FileStream(Path.Combine(path, file.FileName), FileMode.Create, FileAccess.ReadWrite))
                            DirectoryInfo PathInfo = new DirectoryInfo(path);
                            if (!PathInfo.Exists) PathInfo.Create();
                            using (FileStream fs = new FileStream(Path.Combine(path, FileName), FileMode.Create, FileAccess.ReadWrite))
                            {
                                while (UploadedLength < file.ContentLength)
                                {
                                    int bytes = context.Request.Files[i].InputStream.Read(buffer, 0, bufferSize);
                                    fs.Write(buffer, 0, bytes);
                                    UploadedLength += bytes;
                                    //app.Context.Cache[sGuid + "_CurrentFileLength"] = UploadedLength;
                                    HttpContext.Current.Cache.Insert(sGuid + "_CurrentFileLength", UploadedLength, null, DateTime.Now.AddMinutes(600), Cache.NoSlidingExpiration);
                                }
                            }
                        }
                        else
                        {
                            NofileUploadMessage = context.Server.HtmlEncode(NofileUploadMessage.Replace(@"\", @"\\"));
                            context.Response.Write("<script>document.getElementById(\"errorMsg\").innerHTML=\"" + NofileUploadMessage + "\";</script>");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string exMsg = context.Server.HtmlEncode(ex.Message.Replace(@"\", @"\\").Replace(System.Environment.NewLine, "\\r\\n"));
                context.Response.Write("<script>document.getElementById(\"errorMsg\").innerHTML=\"" + exMsg + "\";</script>");
            }
            finally
            {
                app.Context.Cache.Remove(sGuid + "_FileLength");
                app.Context.Cache.Remove(sGuid + "_UploadFile");
                app.Context.Cache.Remove(sGuid + "_CurrentFileLength");
            }
        }

        private string GetScript(bool HasProgress)
        {
            string UploadId = app.Context.Request.QueryString["id"];
            string UploadDir = (string)app.Context.Cache[UploadId + "_UploadDir"];
            bool IsNeedConfirmMessage = (bool)app.Context.Cache[UploadId + "_IsNeedConfirmMessage"];
            string ConfirmMessage = (string)app.Context.Cache[UploadId + "_ConfirmMessage"];
            StringBuilder sScript = new StringBuilder();
            sScript.Append("<script language=\"javascript\" type=\"text/javascript\">" + Environment.NewLine);
            sScript.Append("// <!CDATA[" + Environment.NewLine);
            sScript.Append("function doUpload() {" + Environment.NewLine);
            sScript.Append("var uploadFrmID = window.frameElement.id;" + Environment.NewLine);
            if (HasProgress)
            {
                sScript.Append("document.getElementById(\"ProgressBar1_divProgressBar\").style.display = \"inline\";" + Environment.NewLine);
            }
            sScript.Append("document.getElementById(\"form1\").action = \"Upload.aspx?id=" + UploadId + "\";" + Environment.NewLine);
            sScript.Append("document.getElementById(\"errorMsg\").innerHTML=\"\";" + Environment.NewLine);
            sScript.Append("document.getElementById(\"btn_upload\").disabled=true;" + Environment.NewLine);
            sScript.Append("document.getElementById(\"btn_serverUpload\").click();" + Environment.NewLine);
            sScript.Append("}" + Environment.NewLine);

            if (IsNeedConfirmMessage)
            {
                sScript.Append("function doConfirm() {" + Environment.NewLine);
                sScript.Append("return window.confirm(\"" + ConfirmMessage + "\");" + Environment.NewLine);
                sScript.Append("}" + Environment.NewLine);
            }

            sScript.Append("// ]]>" + Environment.NewLine);
            sScript.Append("</script>" + Environment.NewLine);
            return sScript.ToString();
        }

        #endregion
    }
}
