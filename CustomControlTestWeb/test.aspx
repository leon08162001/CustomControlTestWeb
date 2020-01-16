<%@ Page Language="C#" AutoEventWireup="true" CodeFile="test.aspx.cs" Inherits="test" %>
<%@ Register Assembly="WebCustomControls" Namespace="APTemplate" TagPrefix="wcc" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title></title>

	<script type="text/javascript">

		function pageLoad() {
		}
    
	</script>

</head>
<body onunload="unload();">
	<form id="form1" runat="server">
	<div>
		<%--<wcc:ToolBar ID="ToolBar1" runat="server">
			<Items>
				<wcc:ToolBarButton Enabled="True" IsSeperator="False" Visible="True" />
				<wcc:ToolBarLabel Enabled="True" Text="assssssss" Visible="True" />
			</Items>
			<CustomControls>
				<wcc:Email ID="Email1" runat="server" HasBorder="True" IsShowTitle="True" NeedValidation="True"
					NeedValue="True" ReadOnly="False" TextBackColor="White" TextBoxHeight="" TextBoxID=""
					TextBoxWidth="" TextForeColor="Black" TextLength="5000" Title="Email: " TitleAlign="left"
					TitleBackColor="Aqua" TitleForeColor="Sienna" TitleWidth="" ValidationType="Label" />
				<wcc:Number ID="Number1" runat="server" HasBorder="True" IsShowTitle="True" NeedValidation="True"
					NeedValue="True" OperatorFormat="等於" ReadOnly="False" Text="0" TextBackColor="White"
					TextBoxHeight="" TextBoxID="" TextBoxWidth="" TextForeColor="Black" TextLength="5000"
					Title="請輸入數字 : " TitleAlign="left" TitleBackColor="Aqua" TitleForeColor="Sienna"
					TitleWidth="" ValidationType="Label" ValueToCompare="-1" />
				<wcc:PopupCalendar ID="PopupCalendar1" runat="server" CalendarStyle="Modern" DateFormat="年月日"
					FirstDate="9999-12-01" HasBorder="True" IsShowTitle="True" NeedValidation="True"
					NeedValue="True" ReadOnly="False" TextBackColor="White" TextBoxHeight="" TextBoxID=""
					TextBoxWidth="" TextForeColor="Black" TextLength="5000" Title="請輸入日期 : " TitleAlign="left"
					TitleBackColor="Aqua" TitleForeColor="Sienna" TitleWidth="" ValidationType="Label" />
				<wcc:Button_Normal ID="Button_Normal1" runat="server" ButtonID="" ClientScript=""
					CommandArgument="" CommandName="" PostBackUrl="" Text="assss" Width="" />
			</CustomControls>
		</wcc:ToolBar>--%>
	</div>
	<wcc:Captcha ID="Captcha1" runat="server" CaptchaFont="標楷體, 24pt, style=Bold, Italic, Underline" />
	<%--	<wcc:BarCode ID="BarCode1" runat="server" BarCodeText="4716413842347"
			BarCodeType="Ean13" BarCodeWeight="Large" IsShowBarText="True"  BarCodeHeight="80"
			IsShowPrice="True" IsShowTitle="True" Scale="_08" TextAlign="Center" 
			TextFont="細明體, 25pt" />
			<wcc:BarCode ID="BarCode2" runat="server" BarCodeText="4710677021531"
			BarCodeType="Ean13" BarCodeWeight="Large" IsShowBarText="True"  BarCodeHeight="80"
			IsShowPrice="True" IsShowTitle="True" Scale="_08" TextAlign="Center" 
			TextFont="細明體, 25pt" />--%>
	<%--    <wcc:BarCode ID="BarCode3" runat="server" BarCodeText="038000356216" 
			BarCodeType="Code128" BarCodeWeight="Medium" />
--%>
	<wcc:BarCode ID="BarCode5" runat="server" BarCodeText="978986181821" BarCodeType="Code128_A"
		BarCodeWeight="Small" BarCodeHeight="50px" Scale="_10" Title="Code128_A" />
	<asp:Label ID="Label1" runat="server" Text="Label">            </asp:Label>
	<wcc:BarCode ID="BarCode4" runat="server" BarCodeText="978986181821" BarCodeType="Code128"
		BarCodeWeight="Small" BarCodeHeight="50px" Scale="_15" Title="Code128" />
	<asp:Label ID="Label4" runat="server" Text="Label">            </asp:Label>
	<wcc:BarCode ID="BarCode8" runat="server" BarCodeText="978986181821" BarCodeType="Code11"
		BarCodeWeight="Small" BarCodeHeight="50px" Scale="_15" Title="Code11" />
	<br />
	<wcc:BarCode ID="BarCode6" runat="server" BarCodeText="978986181821" BarCodeType="Code128_B"
		BarCodeWeight="Small" BarCodeHeight="50px" Scale="_10" Title="Code128_B" />
	<asp:Label ID="Label3" runat="server" Text="Label">            </asp:Label>
	<wcc:BarCode ID="BarCode7" runat="server" BarCodeText="978986181821" BarCodeType="Code128_C"
		BarCodeWeight="Small" BarCodeHeight="50px" Scale="_15" Title="Code128_C" />
	<asp:Label ID="Label5" runat="server" Text="Label">            </asp:Label>
	<wcc:BarCode ID="BarCode9" runat="server" BarCodeText="978986181821" BarCodeType="Code93"
		BarCodeWeight="Small" BarCodeHeight="50px" Scale="_15" Title="Code93" />
	<br />
	<wcc:BarCode ID="BarCode1" runat="server" BarCodeText="978986181825" BarCodeType="UPC_A"
		BarCodeWeight="Small" BarCodeHeight="50px" Scale="_15" Title="UPC_A" />
	<asp:Label ID="Label2" runat="server" Text="Label">            </asp:Label>
	<wcc:BarCode ID="BarCode3" runat="server" BarCodeText="9789861818214" BarCodeType="ISBN"
		BarCodeWeight="Small" BarCodeHeight="50px" Scale="_15" Title="ISBN" />
	<asp:Label ID="Label6" runat="server" Text="Label">            </asp:Label>
	<wcc:BarCode ID="BarCode10" runat="server" BarCodeText="978986181821" BarCodeType="MSI"
		BarCodeWeight="Small" BarCodeHeight="50px" Scale="_15" Title="MSI" />
	<br />
	<wcc:BarCode ID="BarCode2" runat="server" BarCodeText="4716413842347" BarCodeType="Ean13"
		BarCodeWeight="Small" BarCodeHeight="50px" Scale="_15" Title="Ean13" />
	<%--			 <wcc:BarCode ID="BarCode2" runat="server" BarCodeText="038000356216" 
			BarCodeType=Code128_B BarCodeWeight="Medium" />
			 <wcc:BarCode ID="BarCode4" runat="server" BarCodeText="038000356216" 
			BarCodeType=Code128_C BarCodeWeight="Medium" />
--%>
	<br />
	<asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
	<wcc:BarCode ID="BarCode13" runat="server" BarCodeText="4716413842347" BarCodeType="Code39"
		BarCodeWeight="Small" BarCodeHeight="50px" Scale="_10" Title="Code39" />
	<br />
	</form>
</body>
</html>
