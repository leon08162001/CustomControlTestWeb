<%@ Page Language="C#" AutoEventWireup="true" CodeFile="test10.aspx.cs" Inherits="test10" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <div id="exportInProgress" style="display:none;">
        <asp:Label ID="lblcurrentProgress" runat="server" Text=""></asp:Label>
    </div>
            <asp:Label ID="resultFile" runat="server" Text=""></asp:Label>
        <asp:Button ID="Button1" runat="server" Text="Button" OnClientClick="startExport();return false;" />
    </div>
    </form>
     <script language="javascript" type="text/javascript">
         function startExport() {
        AjaxPro.timeoutPeriod = 2000000;
        test10.exportRecords(startExport_CallBack); //Asynchronisch call to my method
        //document.getElementById("resultFile").style.display = 'none';
        document.getElementById("exportInProgress").style.display = 'inline-block';
        setTimeout("Progress()", 500);     
        }
    
    var i = 0;
    function Progress(){
        i = test10.currentProgress().value; //synchronisch call to retrieve current progress
        if(i<100){
            document.getElementById("resultFile").innerText = i + "%";
            document.getElementById("lblcurrentProgress").style.width = i * 2;
            setTimeout("Progress()", 1000); //Retrieve current Progress every second
        }
    }
    
    function startExport_CallBack(result){
        document.getElementById("exportInProgress").style.display = 'none';
        //document.getElementById("resultFile").style.display = 'inline-block';
        document.getElementById("resultFile").innerText = result.value;
    }
    </script>

</body>
</html>
