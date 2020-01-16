<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default17.aspx.cs" Inherits="Default17" %>

<%--<%@ Register Assembly="JihSun" Namespace="JihSun" TagPrefix="cc1" %>
--%>
<%@ Register Assembly="WebCustomControls" Namespace="APTemplate" TagPrefix="wcc" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>未命名頁面</title>
</head>
<body>
  <form id="form1" runat="server">
  <div>
        <asp:DataList ID="DataList1" runat="server" OnItemDataBound="DataList1_ItemDataBound">
          <ItemTemplate>
            <asp:Label ID="Label1" runat="server" Text="客戶姓名:"></asp:Label>
            <asp:Label ID="lblCifName" runat="server" Text='<%# Eval("CIFNAME") %>'></asp:Label>
            <br />
            <asp:Label ID="lbl2" runat="server" Text="分行名稱:"></asp:Label>
            <asp:Label ID="lblbrnonm" runat="server" Height="18px" Width="120px" Text='<%# Eval("cdb_brnonm_cname") %>'></asp:Label>
            <br />
            <asp:Label ID="lbl3" runat="server" Text="客戶地址:"></asp:Label>
            <asp:Label ID="lblCifAdr" runat="server" Height="16px" Width="354px" Text='<%# Eval("CIFADR") %>'></asp:Label>
            <br />
            <asp:Label ID="Label2" runat="server" Text="到期日:"></asp:Label>
            <wcc:PopupCalendar ID="PopupCalendar1" runat="server" IsShowTitle="False" CalendarStyle="Modern"
              ReadOnly="true" FirstDate='<%# Convert.ToDateTime((Convert.ToInt32(Eval("db_mutmr_isday"))+19110000).ToString().Substring(0,4) +"/"+(Convert.ToInt32(Eval("db_mutmr_isday"))+19110000).ToString().Substring(4,2)+"/"+(Convert.ToInt32(Eval("db_mutmr_isday"))+19110000).ToString().Substring(6,2)) %>'
              BorderColor="#669999" ForeColor="Red" TextBoxWidth="80px" TitleBackColor="ButtonFace"
              TitleForeColor="SlateGray" TextBackColor="LightGray" 
              EnableViewState="False" />
            <wcc:Captcha ID="Captcha1" runat="server" CaptchaMaxTimeout="20" 
              CaptchaChars="ACDEFGHJKLNPQRTUVXYZ2346789" CaptchaFont="Times New Roman, 28pt" 
              EnableViewState="False" IsShowTitle="True" Text="請鍵入圖形碼:" />
            <wcc:TextBox_PopupWindow ID="TextBox_PopupWindow1" runat="server" WindowType="ModelessDialog"
              WindowWidth="400" WindowHeight="300" ValidationFormat="日期" Features="status=no;scroll=no;resizable=yes"
              IsShowTitle="true" HasBorder="false" NeedValue="false" />
          </ItemTemplate>
        </asp:DataList>
    
    <wcc:PageMaker ID="PageMaker1" runat="server" PagedControlID="DataList1" 
          PageSize=10 />
    <wcc:Button_ConfirmYesNo ID="Button_ConfirmYesNo1" runat="server" />
    <br />
  </div>
  <wcc:TextBox_PopupWindow ID="TextBox_PopupWindow2" runat="server" />
  </form>
</body>
</html>
