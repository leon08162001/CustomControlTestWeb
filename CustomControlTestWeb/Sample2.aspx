<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Sample2.aspx.cs" Inherits="Sample2" %>
<%@ Register Assembly="WebCustomControls" Namespace="APTemplate" TagPrefix="wcc" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    </div>
    <asp:Label ID="Label1" runat="server" Text="1.傳統單合併最低業績："></asp:Label>
    &nbsp;<wcc:Number ID="Number1" runat="server" HasBorder="False" 
      IsShowTitle="False" Text="0" TextBoxID="A" />
    <asp:CheckBoxList ID="CheckBoxList1" runat="server" 
      RepeatDirection="Horizontal" style="display:inline;">
      <asp:ListItem>合併業績找落點</asp:ListItem>
      <asp:ListItem>不合併業績找落點</asp:ListItem>
    </asp:CheckBoxList>
    <br />
    <asp:Button ID="Button1" runat="server" Text="Button" onclick="Button1_Click" />
    </form>
</body>
<script language="javascript" type="text/javascript">
  function CheckBoxList_Click(sender) {
    var container = sender.parentNode;
    if (container.tagName.toUpperCase() == "TD") { //chekboxlist控制項預設為<table>起始標籤，否则使用flow layout
      container = container.parentNode.parentNode; // 層次： <table><tr><td><input />
    }
    var chkList = container.getElementsByTagName("input");
    var senderState = sender.checked;
    for (var i = 0; i < chkList.length; i++) {
      chkList[i].checked = false;
    }
    sender.checked = senderState;
  }

  //設定check的CheckBoxList   setChkCheck(ClientID, '二進制')
  function setChkCheck(id, tf) {
    var chkObj = document.getElementById(id);
    var chkList = chkObj.getElementsByTagName('input');

    for (var i = 0; i < chkList.length; i++) {
      if (tf) {
        chkList[i].checked = true;
      } else {
        chkList[i].checked = false;
      }
    }
  }

  //設定CheckBoxList的Enable setChkEnable(ClientID, ture | false)
  function setChkEnable(id, tf) {
    var chkObj = document.getElementById(id);
    var chkList = chkObj.getElementsByTagName('input');

    for (var i = 0; i < chkList.length; i++) {
      if (tf) {
        chkList[i].disabled = "";
      } else {
        chkList[i].disabled = "disabled";
      }
    }
  }


  </script>

</html>
