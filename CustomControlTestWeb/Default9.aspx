<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default9.aspx.cs" Inherits="Default9" %>
<%@ Register Assembly="WebCustomControls" Namespace="APTemplate" TagPrefix="wcc" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>未命名頁面</title>
	<style type="text/css">
		.style1
		{
			width: 231px;
		}
  </style>
</head>
<body>
	<form id="form1" runat="server">
	<div>
		<table>
			<tr>
				<td>
					<wcc:Number_Decimal ID="Number_Decimal1" runat="server" TextBoxWidth="80px" 
						Text="12" ValidationType="NoCheck" />
					<wcc:Identity ID="Identity1" runat="server" TextBoxWidth="80px" 
            IsShowTitle="True" NeedValidation="True" ValidationType="Alert" />
				</td>
				<td>
					<wcc:Email ID="Email1" runat="server" TextBoxWidth="120px" 
						ValidationType="Alert" />
				</td>
				<td>
					<wcc:DropDownList_Date ID="DropDownList_Date1" runat="server" />
				</td>
			</tr>
			<tr>
				<td>
					&nbsp;</td>
				<td>
					<wcc:PopupCalendar ID="PopupCalendar1" runat="server" TextBoxWidth="80px" 
						ValidationType="Alert" />
				</td>
				<td>
					<wcc:NumberRange ID="NumberRange1" runat="server" TextBoxWidth="80px" 
						ValidationType="Alert" />
				</td>
			</tr>
			<tr>
				<td>
					<wcc:DecimalRange ID="DecimalRange1" runat="server" TextBoxWidth="80px" 
						ValidationType="Alert" />
				</td>
				<td>
					<wcc:CalendarRange ID="CalendarRange1" runat="server" TextBoxWidth="80px" 
						ValidationType="Alert" />
				</td>
				<td>
					<wcc:ListBoxToListBox ID="ListBoxToListBox1" runat="server">
						<SecondListItems>
							<asp:ListItem>C</asp:ListItem>
							<asp:ListItem>D</asp:ListItem>
						</SecondListItems>
						<FirstListItems>
							<asp:ListItem>A</asp:ListItem>
							<asp:ListItem>B</asp:ListItem>
						</FirstListItems>
					</wcc:ListBoxToListBox>
				</td>
			</tr>
		</table>
	</div>
	<br />
	<asp:LinkButton ID="LinkButton1" runat="server" onclick="LinkButton1_Click">PostBack</asp:LinkButton>
	</form>
</body>
</html>
