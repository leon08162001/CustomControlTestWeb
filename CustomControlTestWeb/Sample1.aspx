<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Sample1.aspx.cs" Inherits="Default32" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
			<br />
			<asp:Button ID="Button1" runat="server" Text="Button" onclick="Button1_Click" />
    	<asp:RadioButtonList ID="RadioButtonList1" runat="server" 
				RepeatDirection="Horizontal" style="display:inline;">
				<asp:ListItem Selected="True">扣除</asp:ListItem>
				<asp:ListItem>不扣除</asp:ListItem>
				<asp:ListItem>其它</asp:ListItem>
			</asp:RadioButtonList>
			<asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
    	<asp:CustomValidator ID="CustomValidator1" runat="server" 
				ClientValidationFunction="CheckCharNum" ControlToValidate="TextBox1" 
				Display="Dynamic" ErrorMessage="必須輸入至少10個全形字元!" SetFocusOnError="True" 
				ValidateEmptyText="True"></asp:CustomValidator>
    </div>
    </form>
</body>
<script language="javascript" type="text/javascript">
  function chineseCount(word) {
  	v = 0
  	for (cc = 0; cc < word.length; cc++) {
  		c = word.charCodeAt(cc);
  		if (!(c >= 32 && c <= 126)) v++;
   	}
  	return v
  }

  function WordCount(word) {
  	v = 0
  	v = word.length;
  	return v
  }

  function CheckCharNum(source, arguments) {
  	var val = arguments.Value;
  	if (document.getElementById("RadioButtonList1_2").checked) {
  		if (val == "") {
  			arguments.IsValid = false;
  		}
  		if (chineseCount(val) < 10)
  			arguments.IsValid = false;
  		else
  			arguments.IsValid = true;
  	}
  	else {
  		arguments.IsValid = true;
  	}
  }

</script>
</html>
