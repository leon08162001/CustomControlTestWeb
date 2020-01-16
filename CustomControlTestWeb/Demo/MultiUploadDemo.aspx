<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MultiUploadDemo.aspx.cs" Inherits="Demo_MultiUploadDemo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <wcc:AjaxMultiUpload ID="MultiUpload1" runat="server" UploadNmbers="2" 
            IsWithProgress="False" IsWithProgressPercent="True"
            UploadDir='<%$ AppSettings:UploadFolder %>' ProgressImageUrl="~/images/progressbar13.gif"
            Font-Names="微軟正黑體" Font-Size="9" ProgressTextFont="微軟正黑體, 9pt, style=Underline"
            ProgressText="上傳中..." ProgressPercentPageUrl="~/Demo/UploadProgress.aspx" ScriptMethodNameForProgressPercent="doProgressWork"
            IsShowUploadButton="False" NofileUploadMessage="請選擇上傳檔案!" 
            IsUseVirtualPath="False" IsTriggerUploadFilesFinishedEvent="True" 
            onmultiuploadfilesfinished="MultiUpload1_MultiUploadFilesFinished">
            <FileFilterItems>
                <wcc:FileFilterItem Value="*.*" />
            </FileFilterItems>
        </wcc:AjaxMultiUpload>
        <asp:Button ID="Button2" runat="server" Text="選擇更多檔案" onclick="Button2_Click" />
    </div>
    </form>
</body>
</html>
