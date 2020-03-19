<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UploadDemo1.aspx.cs" Inherits="UploadDemo1"
    Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>
<%@ Register Assembly="WebCustomControls" Namespace="APTemplate" TagPrefix="wcc" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <script language="javascript" type="text/javascript">
        var t;
        var i = 0;
        var maxi;
        function upLoad1() {
            var btn = document.getElementById("Button1");
            btn.disabled = true;
            maxi = window.frames.length - 1;
            var uploadFrame = window.frames[i];
            var progressPercentFrame = window.frames[i].frames[0];
            uploadFrame.document.getElementById("btn_upload").click();
            if (progressPercentFrame != null) {
                progressPercentFrame.document.getElementById("isUploadFinished").value = "false";
                t = window.setInterval("checkUploadFinishedForProgressPercent()", 250);
            }
            else {
                uploadFrame.document.getElementById("isUploadFinished").value = "false";
                t = window.setInterval("checkUploadFinishedForProgressBar()", 250);
            }
        }

        function checkUploadFinishedForProgressPercent() {
            var btn = document.getElementById("Button1");
            var progressPercentFrame = window.frames[i].frames[0];
            if (progressPercentFrame.document.getElementById("progress") != null) {
                if (progressPercentFrame.document.getElementById("isUploadFinished").value == "true") {
                    if (i < maxi) {
                        i++;
                        var uploadFrame = window.frames[i];
                        uploadFrame.document.getElementById("btn_upload").click();
                        progressPercentFrame.document.getElementById("isUploadFinished").value = false;
                    }
                    else {
                        clearInterval(t);
                        btn.disabled = false;
                        window.location.href = window.location.href;
                    }
                }
            }
        }
        function checkUploadFinishedForProgressBar() {
            var btn = document.getElementById("Button1");
            var uploadFrame = window.frames[i];
            if (uploadFrame.document.getElementById("isUploadFinished").value == "true") {
                if (i < maxi) {
                    i++;
                    var uploadFrame = window.frames[i];
                    uploadFrame.document.getElementById("btn_upload").click();
                    uploadFrame.document.getElementById("isUploadFinished").value = false;
                }
                else {
                    clearInterval(t);
                    btn.disabled = false;
                    window.location.href = window.location.href;
                }
            }
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <wcc:AjaxUpload ID="Upload1" runat="server" IsWithProgress="True" IsWithProgressPercent="False"
            UploadDir='<%$ AppSettings:UploadFolder %>' ProgressImageUrl="~/images/progressbar13.gif"
            Font-Names="微軟正黑體" Font-Size="9" ProgressTextFont="微軟正黑體, 9pt, style=Underline"
            ProgressText="上傳中..." ProgressPercentPageUrl="~/Demo/UploadProgress.aspx" ScriptMethodNameForProgressPercent="doProgressWork"
            IsShowUploadButton="False" NofileUploadMessage="請選擇上傳檔案!" IsUseVirtualPath="True" >
            <FileFilterItems>
                <wcc:FileFilterItem Value="*.*" />
            </FileFilterItems>
        </wcc:AjaxUpload>
        <wcc:AjaxUpload ID="Upload2" runat="server" IsWithProgress="True" IsWithProgressPercent="False"
            UploadDir='<%$ AppSettings:UploadFolder %>' ProgressImageUrl="~/images/progressbar13.gif"
            Font-Names="微軟正黑體" Font-Size="9" ProgressTextFont="微軟正黑體, 9pt, style=Underline"
            ProgressText="上傳中..." ProgressPercentPageUrl="~/Demo/UploadProgress.aspx" ScriptMethodNameForProgressPercent="doProgressWork"
            IsShowUploadButton="False" NofileUploadMessage="請選擇上傳檔案!" IsUseVirtualPath="True">
            <FileFilterItems>
                <wcc:FileFilterItem Value="*.*" />
            </FileFilterItems>
        </wcc:AjaxUpload>
        <wcc:AjaxUpload ID="Upload3" runat="server" IsWithProgress="True" IsWithProgressPercent="False"
            UploadDir='<%$ AppSettings:UploadFolder %>' ProgressImageUrl="~/images/progressbar13.gif"
            Font-Names="微軟正黑體" Font-Size="9" ProgressTextFont="微軟正黑體, 9pt, style=Underline"
            ProgressText="上傳中..." ProgressPercentPageUrl="~/Demo/UploadProgress.aspx" ScriptMethodNameForProgressPercent="doProgressWork"
            IsShowUploadButton="False" NofileUploadMessage="請選擇上傳檔案!" IsUseVirtualPath="True">
            <FileFilterItems>
                <wcc:FileFilterItem Value="*.*" />
            </FileFilterItems>
        </wcc:AjaxUpload>
        <%--<wcc:AjaxUpload ID="Upload4" runat="server" IsWithProgress="false" IsWithProgressPercent="true"
            UploadDir='<%$ AppSettings:UploadFolder %>' ProgressImageUrl="~/images/progressbar13.gif"
            Font-Names="微軟正黑體" Font-Size="9" ProgressTextFont="微軟正黑體, 9pt, style=Underline"
            ProgressText="上傳中..." ProgressPercentPageUrl="~/Demo/UploadProgress.aspx" ScriptMethodNameForProgressPercent="doProgressWork"
            IsShowUploadButton="False" NofileUploadMessage="請選擇上傳檔案!" IsUseVirtualPath="False">
            <FileFilterItems>
                <wcc:FileFilterItem Value="*.*" />
            </FileFilterItems>
        </wcc:AjaxUpload>
        <br />
        <wcc:AjaxUpload ID="Upload5" runat="server" IsWithProgress="false" IsWithProgressPercent="true"
            UploadDir='<%$ AppSettings:UploadFolder %>' ProgressImageUrl="~/images/progressbar13.gif"
            Font-Names="微軟正黑體" Font-Size="9" ProgressTextFont="微軟正黑體, 9pt, style=Underline"
            ProgressText="上傳中..." ProgressPercentPageUrl="~/Demo/UploadProgress.aspx" ScriptMethodNameForProgressPercent="doProgressWork"
            IsShowUploadButton="False" NofileUploadMessage="請選擇上傳檔案!" IsUseVirtualPath="False">
            <FileFilterItems>
                <wcc:FileFilterItem Value="*.*" />
            </FileFilterItems>
        </wcc:AjaxUpload>
        <br />
        <wcc:AjaxUpload ID="Upload6" runat="server" IsWithProgress="false" IsWithProgressPercent="true"
            UploadDir='<%$ AppSettings:UploadFolder %>' ProgressImageUrl="~/images/progressbar13.gif"
            Font-Names="微軟正黑體" Font-Size="9" ProgressTextFont="微軟正黑體, 9pt, style=Underline"
            ProgressText="上傳中..." ProgressPercentPageUrl="~/Demo/UploadProgress.aspx" ScriptMethodNameForProgressPercent="doProgressWork"
            IsShowUploadButton="False" NofileUploadMessage="請選擇上傳檔案!" IsUseVirtualPath="False">
            <FileFilterItems>
                <wcc:FileFilterItem Value="*.*" />
            </FileFilterItems>
        </wcc:AjaxUpload>
        <br />--%>
    </div>
    <asp:Button ID="Button1" runat="server" Text="上傳" OnClientClick="upLoad1();return false;" />
    </form>
</body>
</html>
