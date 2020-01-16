<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DropDownListSeriesDemo.aspx.cs" Inherits="Demo_DropDownListSeriesDemo" %>
<%@ Register Assembly="WebCustomControls" Namespace="APTemplate" TagPrefix="wcc" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
       <wcc:DropDownList_Multiple ID="DropDownList_Multiple" ConnectionKey="Default"
      runat="server" DropDownListLevel="Three" HasInitialItem="false" FirstTitle="客戶/訂單日/購買商品:"
      TitleAlign="right" IsShowTitle="True" />
    <wcc:DropDownList_Date ID="DropDownList_Date2" runat="server" FirstTitle="出生年/月/日:" />
        <wcc:DropDownList_YearMonth ID="DropDownList_YearMonth1" runat="server" />
        <wcc:NewDropDownList ID="NewDropDownList1" runat="server" Title="英文字母清單 :">
            <Items>
                <asp:ListItem>A</asp:ListItem>
                <asp:ListItem>B</asp:ListItem>
                <asp:ListItem>C</asp:ListItem>
                <asp:ListItem>D</asp:ListItem>
                <asp:ListItem>E</asp:ListItem>
                <asp:ListItem>F</asp:ListItem>
            </Items>
        </wcc:NewDropDownList>
    </div>
    </form>
</body>
</html>
