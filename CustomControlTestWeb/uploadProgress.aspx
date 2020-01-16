<%@ Page Language="C#" AutoEventWireup="true" CodeFile="uploadProgress.aspx.cs" Inherits="uploadProgress" %>
<%@ Register Assembly="WebCustomControls" Namespace="APTemplate" TagPrefix="wcc" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script language="javascript" type="text/javascript">
// <!CDATA[
        function doProgressWork() {
            var progressFrmID = window.frameElement.id;
            var uploadFrmID = progressFrmID.replace("_progress", "");
            var obj = window.parent.document.getElementById(uploadFrmID).contentWindow.document.getElementById('btn_upload');
            ProgressBar1_doWork(obj);
        }
// ]]>
    </script>
</head>
<body style=" margin-left:0px; margin-top:0px; margin-bottom:0px; margin-right:0px;">
    <form id="form1" runat="server">
    <div>
    <wcc:ProgressBar ID="ProgressBar1" runat="server" Width="200px" ProgressText="上傳中,請稍候..."
            IsShowSuccessText="false" BorderColor="Red" BorderStyle="Groove" BorderWidth="2px"
            Enabled="False" BackColor="Yellow" Font-Size="9pt" TimeOutText="執行時間超過" ProgressTimeOut="7200"
            SuccessText="上傳完成!" FalseText="上傳失敗!" IsShowFalseText="True" IsShowSystemExceptionText="True"
            IsShowTrueText="false" TrueText="上傳完成!" Style="top: 0px; left: 0px" ImageUrl="~/images/progressbar13.gif"
            ProgressType="Image" IsNeedForPercentage="false" AjaxServerClass="uploadProgress" IsNeedForNewWindow="false"
            Font-Names="微軟正黑體" ProgressTimeSpan="1" 
            ProgressTextDirection="horizontal" />
    <asp:Button ID="btn_progress" runat="server" Text="進度" OnClientClick="doProgressWork();return false;" style="display:none;" />
    </div>
    </form>
</body>
</html>
