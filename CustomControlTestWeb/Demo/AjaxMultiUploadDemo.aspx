<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AjaxMultiUploadDemo.aspx.cs" Inherits="AjaxMultiUploadDemo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script language="javascript" type="text/javascript">
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <iframe id="uploadFrame1" frameborder="0" height="25" width="290" marginwidth="0px"
            marginheight="0px" scrolling="no" src="../multiUpload.aspx"></iframe>
        <iframe id="uploadFrame1_progress" frameborder="0" height="25" width="250" marginwidth="0px"
            marginheight="0px" scrolling="no" src="../uploadProgress.aspx"></iframe>
        <br />
        <iframe id="uploadFrame2" frameborder="0" height="25" width="290" marginwidth="0px"
            marginheight="0px" scrolling="no" src="../multiUpload.aspx"></iframe>
        <iframe id="uploadFrame2_progress" frameborder="0" height="25" width="250" marginwidth="0px"
            marginheight="0px" scrolling="no" src="../uploadProgress.aspx"></iframe>
        <br />
        <iframe id="uploadFrame3" frameborder="0" height="25" width="290" marginwidth="0px"
            marginheight="0px" scrolling="no" src="../multiUpload.aspx"></iframe>
        <iframe id="uploadFrame3_progress" frameborder="0" height="25" width="250" marginwidth="0px"
            marginheight="0px" scrolling="no" src="../uploadProgress.aspx"></iframe>
       <%-- <br />
        <iframe id="uploadFrame4" frameborder="0" height="25" width="270" marginwidth="0px"
            marginheight="0px" scrolling="no" src="multiUpload.aspx"></iframe>
        <iframe id="uploadFrame4_progress" frameborder="0" height="25" width="250" marginwidth="0px"
            marginheight="0px" scrolling="no" src="uploadProgress.aspx"></iframe>
        <br />
        <iframe id="uploadFrame5" frameborder="0" height="25" width="270" marginwidth="0px"
            marginheight="0px" scrolling="no" src="multiUpload.aspx"></iframe>
        <iframe id="uploadFrame5_progress" frameborder="0" height="25" width="250" marginwidth="0px"
            marginheight="0px" scrolling="no" src="uploadProgress.aspx"></iframe>--%>
        <%--<br />
        <iframe id="uploadFrame6" frameborder="0" height="25" width="270" marginwidth="0px"
            marginheight="0px" scrolling="no" src="multiUpload.aspx"></iframe>
        <iframe id="uploadFrame6_progress" frameborder="0" height="25" width="250" marginwidth="0px"
            marginheight="0px" scrolling="no" src="uploadProgress.aspx"></iframe>
            <br />
        <iframe id="uploadFrame7" frameborder="0" height="25" width="270" marginwidth="0px"
            marginheight="0px" scrolling="no" src="multiUpload.aspx"></iframe>
        <iframe id="uploadFrame7_progress" frameborder="0" height="25" width="250" marginwidth="0px"
            marginheight="0px" scrolling="no" src="uploadProgress.aspx"></iframe>
            <br />
        <iframe id="uploadFrame8" frameborder="0" height="25" width="270" marginwidth="0px"
            marginheight="0px" scrolling="no" src="multiUpload.aspx"></iframe>
        <iframe id="uploadFrame8_progress" frameborder="0" height="25" width="250" marginwidth="0px"
            marginheight="0px" scrolling="no" src="uploadProgress.aspx"></iframe>
            <br />
        <iframe id="uploadFrame9" frameborder="0" height="25" width="270" marginwidth="0px"
            marginheight="0px" scrolling="no" src="multiUpload.aspx"></iframe>
        <iframe id="uploadFrame9_progress" frameborder="0" height="25" width="250" marginwidth="0px"
            marginheight="0px" scrolling="no" src="uploadProgress.aspx"></iframe>
            <br />
        <iframe id="uploadFrame10" frameborder="0" height="25" width="270" marginwidth="0px"
            marginheight="0px" scrolling="no" src="multiUpload.aspx"></iframe>
        <iframe id="uploadFrame10_progress" frameborder="0" height="25" width="250" marginwidth="0px"
            marginheight="0px" scrolling="no" src="uploadProgress.aspx"></iframe>--%>
        <asp:Button ID="Button1" runat="server" Text="Button" onclick="Button1_Click" />
        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="http://tw.yahoo.com">yahoo!</asp:HyperLink>
    </div>
    </form>
</body>
</html>
