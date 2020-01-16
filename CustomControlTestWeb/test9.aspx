<%@ Page Language="C#" AutoEventWireup="true" CodeFile="test9.aspx.cs" Inherits="test9" EnableSessionState="True" %>
<%@ Register Assembly="WebCustomControls" Namespace="APTemplate" TagPrefix="wcc" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>未命名頁面</title>
    <%--	<script src="js/WebScript.js" type="text/javascript"></script>
	<script src="js/calendar.js" type="text/javascript"></script>
--%>

    <script type="text/javascript" src="js/csspopup.js"></script>

    <style type="text/css">
        <!
        -- #blanket
        {
            background-color: #777777;
            opacity: 0.65;
            position: absolute;
            z-index: 9001; /*ooveeerrrr nine thoussaaaannnd*/
            top: 0px;
            left: 0px;
            width: 100%;
        }
        #popUpDiv
        {
            position: absolute;
            background-color: #eeeeee;
            width: 300px;
            height: 300px;
            z-index: 9002; /*ooveeerrrr nine thoussaaaannnd*/
        }
        -- ></style>
</head>
<body>
    <form id="form1" runat="server">
    <wcc:ProgressBar ID="ProgressBar2" runat="server" Width="250px" ProgressText="更新中..."
            IsShowSuccessText="True" BorderColor="Red" BorderStyle="Dotted" BorderWidth="2px"
            Enabled="False" BackColor="Yellow" Font-Size="10pt" 
        TimeOutText="執行時間超過" ProgressTimeOut="1000"
            SuccessText="更新成功!" FalseText="更新失敗!" IsShowFalseText="True" IsShowSystemExceptionText="True"
            IsShowTrueText="True" TrueText="更新成功!" Style="top: 0px; left: 0px" ProgressTimeSpan="1"
            ImageUrl="~/images/progressbar9.gif" ProgressType="Image" IsNeedForPercentage="True"
            AjaxServerClass="AjaxServer.ExportData" />
    <%--<asp:Button ID="Button2" runat="server" Text="Button" OnClientClick="ProgressBar2_doWork(this);return false;" />--%>
      <asp:Button ID="Button2" runat="server" Text="Button" OnClientClick="window.open('test12.aspx','','height=130,width=110,top=300,left=300,toolbar=no,menubar=no,scrollbars=no,resizable=no,location=no,status=no,directories=no,titlebar=no');return false;" />
      <%--<asp:Button ID="Button2" runat="server" Text="Button" OnClientClick=" window.showModalDialog('test12.aspx',window,'dialogHeight:120px;dialogWidth:300px;center:yes;scroll:no;resizable:no;status:no;unadorned:no;');return false;" />--%>

    </form>
     <%-- <script language="javascript" type="text/javascript">
          var browserName = navigator.appName;
          var na = navigator.userAgent.toLowerCase();
          var isFireFox = na.indexOf("firefox") != -1 ? true : false;

          window.alert(browserName);
          if (browserName == "Netscape" || browserName == "Opera") //google chrome app.Name
          {
              ProgressBar2_doWork(window.opener.form1.Button2);
          }
    </script>--%>
</body>
</html>
