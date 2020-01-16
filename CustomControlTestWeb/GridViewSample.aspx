<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GridViewSample.aspx.cs" Inherits="Default4" %>
<%@ Register Assembly="WebCustomControls" Namespace="APTemplate" TagPrefix="wcc" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
			<asp:GridView ID="GridView1" EnableViewState="false" AllowPaging="false" runat="server" AutoGenerateColumns="False" onrowcreated="GridView1_RowCreated" Width="60%">
				<Columns>
					<asp:TemplateField HeaderText="編輯">
						<ItemTemplate>
							<asp:Button ID="Button1" runat="server" Text="更新" />
							<asp:Button ID="Button2" runat="server" Text="刪除" />
						</ItemTemplate>
						<EditItemTemplate>
							<asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
						</EditItemTemplate>
					</asp:TemplateField>
					<asp:BoundField HeaderText="級距" DataField="級距" />
					<asp:BoundField HeaderText="人工單" DataField="帳列人工單" />
					<asp:BoundField HeaderText="電子單" DataField="帳列電子單" />
					<asp:BoundField HeaderText="人工單" DataField="折讓人工單" />
					<asp:BoundField HeaderText="電子單" DataField="折讓電子單" />
					<asp:BoundField HeaderText="人工單" DataField="實收人工單" />
					<asp:BoundField HeaderText="電子單" DataField="實收電子單" />
				</Columns>
				<RowStyle HorizontalAlign="Center" />
			</asp:GridView>
      <wcc:PageMaker ID="PageMaker1" runat="server" PagedControlID="GridView1" Align="left" PageSize="2" />
      <wcc:Button_Normal ID="Button_Normal1" runat="server" 
        onclick="Button_Normal1_Click" />
			<br />
			<wcc:NewGridView  ID="GridView2" EnableViewState="false" AllowPaging="true" runat="server" AutoGenerateColumns="False" 
				onrowcreated="GridView2_RowCreated" Width="60%" EmptyShowHeader="true" EmptyDataText="No Any Data">
			<Columns>
					<asp:TemplateField HeaderText="編輯">
						<ItemTemplate>
							<asp:Button ID="Button1" runat="server" Text="更新" />
							<asp:Button ID="Button2" runat="server" Text="刪除" />
						</ItemTemplate>
						<EditItemTemplate>
							<asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
						</EditItemTemplate>
					</asp:TemplateField>
					<asp:BoundField HeaderText="級距" DataField="級距" />
					<asp:BoundField HeaderText="人工單" DataField="帳列人工單" />
					<asp:BoundField HeaderText="電子單" DataField="帳列電子單" />
					<asp:BoundField HeaderText="人工單" DataField="折讓人工單" />
					<asp:BoundField HeaderText="電子單" DataField="折讓電子單" />
					<asp:BoundField HeaderText="人工單" DataField="實收人工單" />
					<asp:BoundField HeaderText="電子單" DataField="實收電子單" />
				</Columns>
				<RowStyle HorizontalAlign="Center" />
				<HeaderStyle Font-Bold="true" Font-Size="Medium" />
			</wcc:NewGridView>
			<wcc:PageMaker ID="PageMaker2" runat="server" PagedControlID="GridView2" Align="left" PageSize="100" />
    </div>
    </form>
</body>
</html>
