<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UploadDemo.aspx.cs" Inherits="UploadDemo"
    Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>
<%@ Register Assembly="WebCustomControls" Namespace="APTemplate" TagPrefix="wcc" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <wcc:AjaxUpload ID="Upload1" runat="server" IsWithProgress="false" IsWithProgressPercent="true"
            UploadDir='<%$ AppSettings:UploadFolder %>' ProgressImageUrl="~/images/progressbar13.gif"
            Font-Names="微軟正黑體" Font-Size="9" ProgressTextFont="微軟正黑體, 9pt, style=Underline"
            ProgressText="上傳中..." ProgressPercentPageUrl="~/Demo/UploadProgress.aspx" 
            ScriptMethodNameForProgressPercent="doProgressWork" IsUseVirtualPath="False">
            <FileFilterItems>
                <wcc:FileFilterItem Value="*.*" />
            </FileFilterItems>
        </wcc:AjaxUpload>
        <%--<iframe id="Frame_Progress1" frameborder="0" width="250" height="27" marginwidth="0"
            marginheight="0" scrolling="no" src="UploadProgress.aspx?uploadIframeId=Frame_Upload1"></iframe>--%>
        <br />
        <wcc:AjaxUpload ID="Upload2" runat="server" IsWithProgress="false" IsWithProgressPercent="true"
            UploadDir='<%$ AppSettings:UploadFolder %>' ProgressImageUrl="~/images/progressbar13.gif"
            Font-Names="微軟正黑體" Font-Size="9" ProgressTextFont="微軟正黑體, 9pt, style=Underline"
            ProgressText="上傳中..." ProgressPercentPageUrl="~/Demo/UploadProgress.aspx" 
            ScriptMethodNameForProgressPercent="doProgressWork" IsUseVirtualPath="False">
            <FileFilterItems>
                <wcc:FileFilterItem Value="*.*" />
            </FileFilterItems>
        </wcc:AjaxUpload>
        <%-- <iframe id="Frame_Progress2" frameborder="0" width="250" height="27" marginwidth="0"
            marginheight="0" scrolling="no" src="UploadProgress.aspx?uploadIframeId=Frame_Upload2"></iframe>--%>
        <br />
        <wcc:AjaxUpload ID="Upload3" runat="server" IsWithProgress="false" IsWithProgressPercent="true"
            UploadDir='<%$ AppSettings:UploadFolder %>' ProgressImageUrl="~/images/progressbar13.gif"
            Font-Names="微軟正黑體" Font-Size="9" ProgressTextFont="微軟正黑體, 9pt, style=Underline"
            ProgressText="上傳中..." ProgressPercentPageUrl="~/Demo/UploadProgress.aspx" 
            ScriptMethodNameForProgressPercent="doProgressWork" IsUseVirtualPath="False">
            <FileFilterItems>
                <wcc:FileFilterItem Value="*.*" />
            </FileFilterItems>
        </wcc:AjaxUpload>
        <br />
        <wcc:AjaxUpload ID="Upload4" runat="server" IsWithProgress="false" IsWithProgressPercent="true"
            UploadDir='<%$ AppSettings:UploadFolder %>' ProgressImageUrl="~/images/progressbar13.gif"
            Font-Names="微軟正黑體" Font-Size="9" ProgressTextFont="微軟正黑體, 9pt, style=Underline"
            ProgressText="上傳中..." ProgressPercentPageUrl="~/Demo/UploadProgress.aspx" 
            ScriptMethodNameForProgressPercent="doProgressWork" IsUseVirtualPath="False">
            <FileFilterItems>
                <wcc:FileFilterItem Value="*.*" />
            </FileFilterItems>
        </wcc:AjaxUpload>
        <br />
        <wcc:AjaxUpload ID="Upload5" runat="server" IsWithProgress="false" IsWithProgressPercent="true"
            UploadDir='<%$ AppSettings:UploadFolder %>' ProgressImageUrl="~/images/progressbar13.gif"
            Font-Names="微軟正黑體" Font-Size="9" ProgressTextFont="微軟正黑體, 9pt, style=Underline"
            ProgressText="上傳中..." ProgressPercentPageUrl="~/Demo/UploadProgress.aspx" 
            ScriptMethodNameForProgressPercent="doProgressWork" IsUseVirtualPath="False">
            <FileFilterItems>
                <wcc:FileFilterItem Value="*.*" />
            </FileFilterItems>
        </wcc:AjaxUpload>
        <br />
        <wcc:AjaxUpload ID="Upload6" runat="server" IsWithProgress="false" IsWithProgressPercent="true"
            UploadDir='<%$ AppSettings:UploadFolder %>' ProgressImageUrl="~/images/progressbar13.gif"
            Font-Names="微軟正黑體" Font-Size="9" ProgressTextFont="微軟正黑體, 9pt, style=Underline"
            ProgressText="上傳中..." ProgressPercentPageUrl="~/Demo/UploadProgress.aspx" 
            ScriptMethodNameForProgressPercent="doProgressWork" IsUseVirtualPath="False">
            <FileFilterItems>
                <wcc:FileFilterItem Value="*.*" />
            </FileFilterItems>
        </wcc:AjaxUpload>
        <br />
    </div>
    </form>
</body>
</html>
