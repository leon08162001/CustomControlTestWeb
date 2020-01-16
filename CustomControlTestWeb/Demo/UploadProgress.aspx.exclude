<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UploadProgress.aspx.cs" Inherits="Demo_UploadProgress" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" type="text/css" href="../css/Style.css" />
</head>
<body>
    <form id="form1" runat="server">
    <div id="progress">
        <span id="percent"></span>
        <div id="bar">
        </div>
    </div>
    <%--<input id="hdnGuid" name="hdnGuid" type="hidden" value="" />--%>
    <%--<input id="hdnGuid" name="hdnSessionID" type="hidden" value="" />--%>
    <input id="isUploadFinished" name="isUploadFinished" type="hidden" value="true" />
    </form>
</body>

<script language="javascript" type="text/javascript"> 
    // <!CDATA[
    var timeoutID;
    function doProgressWork() {
        var sGuid = window.parent.document.getElementById("hdnGuid").value;
        var callBackMethod = window.doProgressCallBack;
        window.parent.parent.doProgress(sGuid, callBackMethod);
    }

    function doProgressCallBack(res) {
        clearTimeout(timeoutID);
        var obj = eval(res.value);
        if (obj.percent == -1) {
            document.getElementById("progress").style.display = "inline-block";
            document.getElementById("percent").innerHTML = "上載中...";
            document.getElementById("isUploadFinished").value = "false";
            timeoutID = window.setTimeout("doProgressWork()", 50);
        }
        else if (obj.percent == 100) {
            document.getElementById("progress").style.display = "none";
            document.getElementById("percent").innerHTML = "";
            document.getElementById("isUploadFinished").value = "true";
            return;
        }
        else if (obj.percent >= 0 || obj.percent < 100) {
            document.getElementById("progress").style.display = "inline-block";
            document.getElementById("bar").style.backgroundColor = "yellow";
            document.getElementById("bar").style.width = obj.percent + "%";
            document.getElementById("percent").innerHTML = obj.percent + "%(" + obj.currentSize + "M/" + obj.size + "M)";
            document.getElementById("isUploadFinished").value = "false";
            timeoutID = window.setTimeout("doProgressWork()", 50); //Retrieve current Progress every second
        }
    }
// ]]>
</script>

</html>
