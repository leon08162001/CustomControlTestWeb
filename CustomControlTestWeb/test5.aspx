<%@ Page Language="C#" AutoEventWireup="true" CodeFile="test5.aspx.cs" Inherits="test5"
  EnableViewState="false" %>
<%@ Register Assembly="WebCustomControls" Namespace="APTemplate" TagPrefix="wcc" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title></title>
</head>

<script language="javascript" type="text/javascript">
  function A() {
    var a = "hello";
    window.alert(a);
  }
</script>

<body>
  <form id="form1" runat="server">
  <asp:ScriptManager ID="ScriptManager1" runat="server">
  </asp:ScriptManager>
  <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
      <div>
         <wcc:NewGridView ID="NewGridView1" runat="server" AllowPaging="true" CustomFontSize="Medium" MouseOverBackColor="Yellow"
                    MouseOverCellBackColor="Cyan" EnableViewState="false">
                </wcc:NewGridView>
       <%-- <asp:GridView ID="GridView1" runat="server" AllowPaging="true" EnableViewState="true">
        </asp:GridView>--%>
        <wcc:PageMaker ID="PageMaker1" runat="server" PagedControlID="NewGridView1" PageSize="10" />
        <%-- <asp:GridView ID="GridView1" runat="server">
        </asp:GridView>--%>
      </div>
      <asp:Button ID="Button1" runat="server" Text="Button" OnClientClick="A();return false;" />
    </ContentTemplate>
  </asp:UpdatePanel>
  </form>
</body>
</html>
