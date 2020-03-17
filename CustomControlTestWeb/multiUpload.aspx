<%@ Page Language="C#" AutoEventWireup="true" CodeFile="multiUpload.aspx.cs" Inherits="multiUpload" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <script language="javascript" type="text/javascript">
// <!CDATA[
        function doUpload() {
            var uploadFrmID = window.frameElement.id;
            var progressFrmID = uploadFrmID + "_progress";
            document.getElementById("btn_serverUpload").click();
            window.parent.document.getElementById(progressFrmID).contentWindow.document.getElementById('btn_progress').click();
        }     
// ]]>
    </script>

</head>
<body style="margin-left: 0px; margin-top: 0px; margin-bottom: 0px; margin-right: 0px;">
    <form id="form1" runat="server" enctype="multipart/form-data">
    <div>
        <table cellpadding="0" cellspacing="0">
            <tr>
                <td>
                    <asp:FileUpload ID="fileUpload" runat="server" Height="22px" />
                </td>
                <td>
                    <input id="btn_upload" type="button" value="上傳" onclick="if (document.getElementById('fileUpload').value != '') { doUpload(); }" />
                    <asp:Button ID="btn_serverUpload" runat="server" OnClick="UploadData" Style="display: none;"
                        Text="上傳" />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
