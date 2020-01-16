<%@ Page Title="首頁" Language="vb" MasterPageFile="~/Site.Master" AutoEventWireup="false"
  CodeFile="Default.aspx.vb" Inherits="_Default" %>

<%@ Register Assembly="WebCustomControls" Namespace="APTemplate" TagPrefix="wcc" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
  <h2>
    歡迎使用 ASP.NET!
  </h2>
  <p>
    若要進一步了解 ASP.NET，請造訪 <a href="http://www.asp.net" title="ASP.NET 網站">www.asp.net</a>。
  </p>
  <p>
    您也可以尋找 <a href="http://go.microsoft.com/fwlink/?LinkID=152368" title="MSDN ASP.NET 文件">
      MSDN 上有關 ASP.NET 的文件</a>。
  </p>
  <p>
    <%--<wcc:CalendarRange ID="CalendarRange1" runat="server" />--%>
    <wcc:MediaPlayer ID="MediaPlayer1" runat="server" Url="~/Styles/mov03.wmv" Height="300px"
      Width="400px" AutoStart="False">
      <Params>
        <wcc:ActiveXParam Name="URL" Value="Styles/mov03.wmv"></wcc:ActiveXParam>
        <wcc:ActiveXParam Name="autoStart" Value="False"></wcc:ActiveXParam>
      </Params>
    </wcc:MediaPlayer>
    <br />
    <wcc:BarCode ID="BarCode1" runat="server" BarCodeText="324356563" Scale="_12" />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
      <ContentTemplate>
        <wcc:Captcha ID="Captcha1" runat="server" CaptchaLength="1" CaptchaBackgroundNoise="Extreme"
          CaptchaFont="Trebuchet MS, 27.75pt" CaptchaFontWarping="High" Font-Names="標楷體"
          CaptchaLineNoise="Extreme" CaptchaWidth="120" CaptchaMaxTimeout="10" CaptchaMinTimeout="2"
          CaptchaChars="0123456789" />
      </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
      <ContentTemplate>
        <wcc:Captcha ID="Captcha2" runat="server" CaptchaLength="1" CaptchaBackgroundNoise="Extreme"
          CaptchaFont="Trebuchet MS, 27.75pt" CaptchaFontWarping="High" Font-Names="標楷體"
          CaptchaLineNoise="Extreme" CaptchaWidth="120" CaptchaMaxTimeout="10" CaptchaMinTimeout="2"
          CaptchaChars="0123456789" />
          <wcc:CalendarRange ID="CalendarRange1" runat="server" />
      </ContentTemplate>
    </asp:UpdatePanel>
    <asp:Button ID="Button1" runat="server" Text="Button" />
    <br />
    <br />
    <br />
    <br />
    <br />
    <wcc:Flash ID="Flash1" runat="server" Height="390px" MovieUrl="http://www.youtube.com/v/ReUJEwjc97o?version=3"
      Width="640px" Quality="Best">
      <Params>
        <wcc:ActiveXParam Name="allowFullScreen" Value="true" />
        <wcc:ActiveXParam Name="allowScriptAccess" Value="always" />
        <wcc:ActiveXParam Name="movie" Value="http://www.youtube.com/v/ReUJEwjc97o?version=3" />
        <wcc:ActiveXParam Name="quality" Value="best" />
      </Params>
    </wcc:Flash>
    <wcc:Flash ID="Flash2" runat="server" Height="390px" MovieUrl="http://www.youtube.com/v/NnX9qZWauMk?version=3"
      Width="640px" Quality="Best">
      <Params>
        <wcc:ActiveXParam Name="allowFullScreen" Value="true" />
        <wcc:ActiveXParam Name="allowScriptAccess" Value="always" />
        <wcc:ActiveXParam Name="movie" Value="http://www.youtube.com/v/NnX9qZWauMk?version=3" />
        <wcc:ActiveXParam Name="quality" Value="best" />
      </Params>
    </wcc:Flash>
    <asp:PlaceHolder ID="PlaceHolder1" runat="server">
      <asp:TextBox ID="AA" runat="server"></asp:TextBox>
    </asp:PlaceHolder>
    &nbsp;</p>
</asp:Content>
