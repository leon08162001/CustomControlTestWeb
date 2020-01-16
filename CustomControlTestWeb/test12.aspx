<%@ Page Language="C#" AutoEventWireup="true" CodeFile="test12.aspx.cs" Inherits="test12" EnableSessionState="True" %>
<%@ Register Assembly="WebCustomControls" Namespace="APTemplate" TagPrefix="wcc" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body onload="ProgressBar2_doWork(window.opener.document.getElementById('Button2'));" onunload="try{ProgressBar2_stopProgressBar('ProgressBar2_divProgressBar');window.opener.document.getElementById('Button2').disabled = false;} catch(ex){};">
    <form id="form1" runat="server">
<wcc:ProgressBar ID="ProgressBar2" runat="server" Width="250px"
      ProgressText="更新中..." IsShowSuccessText="True" 
      BorderColor="Red" BorderStyle="Groove" BorderWidth="2px" Enabled="False"
      BackColor="Yellow" Font-Size="10pt" 
      TimeOutText="執行時間超過" ProgressTimeOut="7200" SuccessText="更新成功!" 
      FalseText="更新失敗!" IsShowFalseText="True" IsShowSystemExceptionText="True" 
      IsShowTrueText="True" TrueText="更新成功!" style="top: 0px; left: 0px" ImageUrl="~/images/progressbar5.gif" 
      ProgressType="Image" IsNeedForPercentage="true" 
      AjaxServerClass="AjaxServer.ExportData" IsNeedForNewWindow="True" 
        Font-Names="微軟正黑體" ProgressTimeSpan="1" />
    </form>
    <script language="javascript" type="text/javascript">
        var browserName = navigator.appName;
        if (browserName == "Netscape" || browserName == "Opera") //google chrome app.Name
        {
            ProgressBar2_doWork(window.opener.document.getElementById("Button2"));
        }

    </script>
</body>
</html>
