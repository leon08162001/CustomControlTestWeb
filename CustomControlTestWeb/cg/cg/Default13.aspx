<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default13.aspx.cs" Inherits="Default13" %>

<%--<%@ Register assembly="JihSun" namespace="JihSun" tagprefix="cc1" %>--%>
<%@ Register Assembly="WebCustomControls" Namespace="APTemplate" TagPrefix="wcc" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>未命名頁面</title>
  <%--    <link rel=stylesheet href="css/css.css" type="text/css" />
--%></head>
<body>
  <form id="form1" runat="server">
  <div>
    <asp:Image ID="Image1" runat="server" />
    <wcc:Email ID="Email1" runat="server" NeedValidation="False" 
      NeedValue="False" />
    <wcc:Identity ID="Identity2" runat="server" NeedValidation="False" 
      NeedValue="False" />
    <wcc:TabsView ID="TabsView1" runat="server" TabPageCenterBackImageUrl="~/images/button_02.gif"
      TabPageLeftBackImageHeight="22px" TabPageLeftBackImageWidth="3px" TabPageRightBackImageWidth="3px"
      TabPageRightBackImageHeight="22px" TabTextColor="Red" SelectedTabBackColor="255, 128, 255"
      TabPageRightBackImageUrl="~/images/button_03.gif" 
      TabPageLeftBackImageUrl="~/images/button_01.gif" IsUseTabBackImage="True" 
      TabBackColor="Green" TabButtonBorderColor="'#d4d0c8'" TabTextSize="Medium" 
      TabTextBold="True" >
      <Tabs>
        <wcc:TabPage ID="Tab1" runat="server" BorderColor="Red" BorderStyle="Solid" 
          BorderWidth="1px" PaddingBottom="10px" PaddingLeft="10px" PaddingRight="10px" 
          PaddingTop="10px" Text="AA" Width="477px" 
          BackImageUrl="~/images/km_login_04.gif" Height="353px" 
          
          
          
          
          style="padding-top: 10px; padding-bottom: 20px; padding-left: 30px; padding-right: 10px; padding-top: 10px; padding-bottom: 10px; padding-left: 10px; padding-right: 10px; padding-top:10px;padding-bottom:10px; padding-left:10px; padding-right:10px;">
          <span ID="TabsView1">
          <wcc:PopupCalendar ID="PopupCalendar2" runat="server" CalendarStyle="Modern" 
            IsShowTitle="False" NeedValidation="True" NeedValue="True" Text="" 
            TextBoxWidth="" TextLength="5000" Title="請輸入日期 : " TitleAlign="left" 
            TitleBackColor="Aqua" TitleForeColor="Sienna" TitleWidth="" 
            ValidationType="Label" TextBoxID="" />
          <br />
          </span>
          <wcc:Menu ID="Menu5" runat="server" ConnectionKey="default" 
            DataType="DataBase" ItemHoverBgColor="239, 239, 239" 
            MainItemBgColor="255, 255, 255" MainItemForeColor="51, 51, 51" 
            MenuItemWidth="145" MenuLevel="10" MenuPosition=Horizontal
            MenuTableBaseName="Menu" MenuType=Basic MenuWidth="450" 
            SubItemBgColor="239, 239, 239" SubItemForeColor="85, 85, 85" Target="self" />
            <br />
            <br />
          <br />
          <br />
          <br />
          <br />
            <br />
            <wcc:Menu ID="Menu1" runat="server" ConnectionKey="default" 
            DataType="DataBase" ItemHoverBgColor="239, 239, 239" 
            MainItemBgColor="255, 255, 255" MainItemForeColor="51, 51, 51" 
            MenuItemWidth="145" MenuLevel="20" MenuPosition=Horizontal
            MenuTableBaseName="Menu" MenuType=Basic MenuWidth="450" 
            SubItemBgColor="239, 239, 239" SubItemForeColor="85, 85, 85" Target="self" />
        </wcc:TabPage>
        <wcc:TabPage runat="server" ID="Tab2" Text="B" Height="189px" Width="600px" BorderColor="Red"
          BorderStyle="Solid" BorderWidth="1px" PaddingTop="10px" PaddingBottom="10px" PaddingLeft="10px"
          PaddingRight="10px" BackImageUrl="~/images/km_login_04.gif" 
          
          
          style="padding-top:10px;padding-bottom:10px;padding-left:10px;padding-right:10px;padding-top:10px;padding-bottom:10px;padding-left:20px; padding-right:10px;">
          <wcc:Identity ID="Identity1" runat="server" NeedValidation="False" NeedValue="False"
            TextBoxID="" />
          <br />
          <wcc:PopupCalendar ID="PopupCalendar1" runat="server" IsShowTitle="True" NeedValidation="True"
            NeedValue="False" Text="" TextBoxWidth="" CalendarStyle="Modern" TextLength="5000"
            Title="請輸入日期 : " TitleAlign="left" TitleBackColor="Aqua" TitleForeColor="Sienna"
            TitleWidth="" ValidationType="Label" />
          <wcc:CalendarRange ID="CalendarRange1" runat="server" CalendarStyle="Modern" FirstText=""
            IsShowTitle="True" NeedValidation="True" NeedValue="True" SecondText="" TextBoxWidth=""
            TextLength="5000" Title="請輸入日期區間 : " TitleAlign="left" TitleBackColor="Aqua" TitleForeColor="Sienna"
            TitleWidth="" ValidationType="Label" />
          <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Button" />
          <br />
        </wcc:TabPage>
        <wcc:TabPage runat="server" ID="Tab3" Text="C" Height="60px" Width="205px" 
          BackImageUrl="~/images/km_login_04.gif" 
          style="padding-top:10px;padding-bottom:10px;padding-left:10px;padding-right:10px;padding-top:10px;padding-bottom:10px;padding-left:10px;padding-right:10px;">
        </wcc:TabPage>
      </Tabs>
    </wcc:TabsView>
    <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
          <asp:TextBox ID="TextBox1" runat="server" Height="19px" Width="132px">ABC</asp:TextBox>
          <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Button" />
  </div>
  </form>
</body>
</html>
