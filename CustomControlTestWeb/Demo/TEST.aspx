<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TEST.aspx.cs" Inherits="Demo_TEST" %>
<%@ Register Assembly="WebCustomControls" Namespace="APTemplate" TagPrefix="wcc" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body id="body1">
    <form id="form1" runat="server">
    <div style="text-align:left">
            <wcc:TabsView ID="TabsView1" runat="server" SelectedTabBackColor="Blue" TabBackColor="Yellow">
            <Tabs>
                <wcc:TabPage ID="TabPage1" runat="server" PaddingBottom="10px" PaddingLeft="10px" PaddingRight="10px" PaddingTop="10px" Text="首頁" BorderColor="Red" BorderStyle="Solid" BorderWidth="1px" BackColor="#FFFF66" Width="1024px">
                    <wcc:Email ID="Email1" runat="server" HasBorder="True" IsShowTitle="True" NeedValidation="True" NeedValue="True" ReadOnly="False" TextAlign="left" TextBackColor="White" TextBoxHeight="" TextBoxWidth="" TextForeColor="Black" TextLength="5000" Title="Email: " TitleAlign="left" TitleBackColor="Aqua" TitleForeColor="Sienna" TitleWidth="" ValidationGroup="A" ValidationType="Label" TextBoxID="" />
                    <asp:Button ID="Button1" runat="server" Text="Button" ValidationGroup="A" OnClick="Button1_Click" OnClientClick="if(Page_ClientValidate('A')){sendValue();}" />
<%--                    <asp:TextBox ID="TextBox1" runat="server" ValidationGroup="A"></asp:TextBox><asp:Button ID="Button1" runat="server" Text="Button" ValidationGroup="A" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TextBox1" Display="Dynamic" ErrorMessage="首頁需要值!" ValidationGroup="A"></asp:RequiredFieldValidator> ValidationGroup="A" --%>
                    <br />
                        <div style="position: relative;"><div style="position: absolute; right: 0;margin-top:15px"><wcc:HtmlEditor ID="HtmlEditor1" runat="server" /></div></div>
                </wcc:TabPage>
                <wcc:TabPage ID="TabPage2" runat="server" PaddingBottom="10px" PaddingLeft="10px" PaddingRight="10px" PaddingTop="10px" style="padding-top:10px;padding-bottom:10px;padding-left:10px;padding-right:10px;" Text="密碼變更" Height="95%" Width="95%" BorderColor="Red" BorderStyle="Solid" BorderWidth="1px" BackColor="#FF5050">
                    <wcc:Email ID="Email2" runat="server" HasBorder="True" IsShowTitle="True" NeedValidation="True" NeedValue="True" ReadOnly="False" TextAlign="left" TextBackColor="White" TextBoxHeight="" TextBoxWidth="" TextForeColor="Black" TextLength="5000" Title="Email: " TitleAlign="left" TitleBackColor="Aqua" TitleForeColor="Sienna" TitleWidth="" ValidationGroup="B" ValidationType="Label" />
                    <asp:Button ID="Button2" runat="server" Text="Button" ValidationGroup="B" />
                <%--<asp:TextBox ID="TextBox2" runat="server" ValidationGroup="B"></asp:TextBox><asp:Button ID="Button2" runat="server" Text="Button" ValidationGroup="B" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TextBox2" ErrorMessage="密碼變更需要值!" ValidationGroup="B"></asp:RequiredFieldValidator>--%>
                </wcc:TabPage>
            </Tabs>
        </wcc:TabsView>
    </div>
    </form>
</body>
</html>
