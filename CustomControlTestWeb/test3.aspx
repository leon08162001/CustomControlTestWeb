<%@ Page Language="C#" AutoEventWireup="true" CodeFile="test3.aspx.cs" Inherits="test3" %>
<%@ Register Assembly="WebCustomControls" Namespace="APTemplate" TagPrefix="wcc" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <wcc:TabsView ID="TabsView1" runat="server" SelectedTabBackColor="239, 239, 239"
            TabBackColor="239, 239, 239" TabButtonBorderColor="'#d4d0c8'" TabPageLeftBackImageHeight="0px"
            TabPageLeftBackImageWidth="0px" TabPageRightBackImageHeight="0px" TabPageRightBackImageWidth="0px"
            TabTextColor="0, 0, 0" TabTextSize="Medium">
            <Tabs>
                <wcc:TabPage runat="server" Text="Tab Item1" PaddingTop="10px" PaddingBottom="10px"
                    PaddingLeft="10px" PaddingRight="10px" ID="TabPage1" Style="padding-top: 10px;
                    padding-bottom: 10px; padding-left: 10px; padding-right: 10px;" BorderColor="Red"
                    BorderStyle="Solid" BorderWidth="1px">
                    <wcc:Menu ID="Menu2" runat="server" ConnectionKey="Default" DataType="DataBase"
                        ItemHoverBgColor="239, 239, 239" MainItemBgColor="255, 255, 255" MainItemForeColor="51, 51, 51"
                        MenuItemWidth="145" MenuLevel="20" MenuPosition="Horizontal" MenuTableBaseName="Menu"
                        MenuType="Stretch_Buttons_Tan" MenuWidth="450" SubItemBgColor="239, 239, 239"
                        SubItemForeColor="85, 85, 85" Target="Frame" FrameName="win1" />
                        <br />
                        <iframe name="win1" id="win1" width="1024" height="768" visible="true"></iframe>
                </wcc:TabPage>
                <wcc:TabPage ID="TabPage2" runat="server" PaddingBottom="10px" PaddingLeft="10px"
                    PaddingRight="10px" PaddingTop="10px" Style="padding-top: 10px; padding-bottom: 10px;
                    padding-left: 10px; padding-right: 10px;" Text="Tab Item2"
                    BorderColor="Red" BorderStyle="Solid" BorderWidth="1px">
                    <wcc:TextBox_Normal ID="TextBox_Normal1" runat="server" HasBorder="True" 
                        IsShowTitle="True" NeedValidation="False" NeedValue="True" ReadOnly="False" 
                        TextBackColor="White" TextBoxHeight="" TextBoxID="" TextBoxWidth="" 
                        TextForeColor="Black" TextLength="5000" TextMode="SingleLine" Title="請輸入標題..." 
                        TitleAlign="left" TitleBackColor="Aqua" TitleForeColor="Sienna" TitleWidth="" 
                        ValidationType="Label" />
                </wcc:TabPage>
            </Tabs>
        </wcc:TabsView>
    </div>
    </form>
</body>
</html>
