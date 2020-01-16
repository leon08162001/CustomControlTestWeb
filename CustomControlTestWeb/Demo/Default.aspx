<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Demo_Default" %>
<%@ Register Assembly="WebCustomControls" Namespace="APTemplate" TagPrefix="wcc" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script language="javascript" type="text/javascript">
        //金额千分位自动分位
        function comdify(thisobj) {
            thisobj.value = thisobj.value.replace(/,/g, "");
//            if (thisobj.value.length > 10) {
//                thisobj.value = thisobj.value.substring(0, 10);
//            }
            var re = /\d{1,3}(?=(\d{3})+$)/g;
            var n1 = thisobj.value.replace(/^(\d+)((\.\d+)?)$/, function (s, s1, s2) { return s1.replace(re, "$&,") + s2; });
            return n1;
        }
        function replaceCommaWithNullCharacter(thisobj) {
            thisobj.value = thisobj.value.replace(/,/g, "");
        }
        function checkisNaN(val) {
            return isNaN(val);
        }
        function changeInputLength(id) {
            document.getElementById(id).setAttribute("maxlength", "10");
        }
  </script> 
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:TextBox ID="TextBox1" runat="server" onblur="this.value=comdify(this);" onfocus="replaceCommaWithNullCharacter(this);"></asp:TextBox>
        <asp:TextBox ID="TextBox2" runat="server" MaxLength="2"></asp:TextBox>
        <%--<wcc:Number ID="Number1" runat="server" TextAlign="right" />
        <wcc:DecimalRange ID="DecimalRange1" runat="server" DecimalLength="三位" />--%>
        <asp:DropDownList ID="DropDownList1" runat="server">
        </asp:DropDownList>
        <wcc:HtmlEditor ID="HtmlEditor1" runat="server" />
        <asp:Button ID="Button1" runat="server" Text="Button" OnClientClick="changeInputLength('TextBox2');return false;" onclick="Button1_Click" />
    </div>
    </form>
</body>
</html>
