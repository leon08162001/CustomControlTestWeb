<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProgressBarDemo.aspx.cs"
    Inherits="Demo_ProgressBarDemo" %>

<%@ Register Assembly="WebCustomControls" Namespace="APTemplate" TagPrefix="wcc" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script language="javascript" type="text/javascript">
        function doProgressWork() {
            var qryField = document.getElementById("<%= txtName.ClientID %>").value;
            var qryValue = document.getElementById("<%= txtGender.ClientID %>").value;
            var sortField = document.getElementById("<%= txtAge.ClientID %>").value;
            var params = new Array();
            params[0] = qryField;
            params[1] = qryValue;
            params[2] = sortField;
            var obj = document.getElementById("Button1");
            ProgressBar1_doWork(obj, params);
            //ProgressBar1_doWork(obj);
        }
        function doProgressWork1() {
            var qryField = document.getElementById("<%= txtName1.ClientID %>").value;
            var qryValue = document.getElementById("<%= txtGender1.ClientID %>").value;
            var sortField = document.getElementById("<%= txtAge1.ClientID %>").value;
            var params = new Array();
            params[0] = qryField;
            params[1] = qryValue;
            params[2] = sortField;
            var obj = document.getElementById("Button2");
            ProgressBar2_doWork(obj, params);
            //ProgressBar2_doWork(obj);
        }
        function doProgressWork2() {
            var qryField = document.getElementById("<%= txtName2.ClientID %>").value;
            var qryValue = document.getElementById("<%= txtGender2.ClientID %>").value;
            var sortField = document.getElementById("<%= txtAge2.ClientID %>").value;
            var params = new Array();
            params[0] = qryField;
            params[1] = qryValue;
            params[2] = sortField;
            var obj = document.getElementById("Button3");
            ProgressBar3_doWork(obj, params);
            //ProgressBar3_doWork(obj);
        }
        function doProgressWork3() {
            var qryField = document.getElementById("<%= txtName3.ClientID %>").value;
            var qryValue = document.getElementById("<%= txtGender3.ClientID %>").value;
            var sortField = document.getElementById("<%= txtAge3.ClientID %>").value;
            var params = new Array();
            params[0] = qryField;
            params[1] = qryValue;
            params[2] = sortField;
            var obj = document.getElementById("Button4");
            ProgressBar4_doWork(obj, params);
            //ProgressBar4_doWork(obj);
        }
        function doProgressWork4() {
            var qryField = document.getElementById("<%= txtName4.ClientID %>").value;
            var qryValue = document.getElementById("<%= txtGender4.ClientID %>").value;
            var sortField = document.getElementById("<%= txtAge4.ClientID %>").value;
            var params = new Array();
            params[0] = qryField;
            params[1] = qryValue;
            params[2] = sortField;
            var obj = document.getElementById("Button5");
            ProgressBar5_doWork(obj, params);
            //ProgressBar5_doWork(obj);
        }
        function doProgressWork5() {
            var qryField = document.getElementById("<%= txtName5.ClientID %>").value;
            var qryValue = document.getElementById("<%= txtGender5.ClientID %>").value;
            var sortField = document.getElementById("<%= txtAge5.ClientID %>").value;
            var params = new Array();
            params[0] = qryField;
            params[1] = qryValue;
            params[2] = sortField;
            var obj = document.getElementById("Button6");
            ProgressBar6_doWork(obj, params);
            //ProgressBar6_doWork(obj);
        }
        function doProgressWork6() {
            var qryField = document.getElementById("<%= txtName6.ClientID %>").value;
            var qryValue = document.getElementById("<%= txtGender6.ClientID %>").value;
            var sortField = document.getElementById("<%= txtAge6.ClientID %>").value;
            var params = new Array();
            params[0] = qryField;
            params[1] = qryValue;
            params[2] = sortField;
            var obj = document.getElementById("Button7");
            ProgressBar7_doWork(obj, params);
            //ProgressBar7_doWork(obj);
        }
        function doProgressWork7() {
            var qryField = document.getElementById("<%= txtName7.ClientID %>").value;
            var qryValue = document.getElementById("<%= txtGender7.ClientID %>").value;
            var sortField = document.getElementById("<%= txtAge7.ClientID %>").value;
            var params = new Array();
            params[0] = qryField;
            params[1] = qryValue;
            params[2] = sortField;
            var obj = document.getElementById("Button8");
            ProgressBar8_doWork(obj, params);
            //ProgressBar8_doWork(obj);
        }
        function doProgressWork8() {
            var qryField = document.getElementById("<%= txtName8.ClientID %>").value;
            var qryValue = document.getElementById("<%= txtGender8.ClientID %>").value;
            var sortField = document.getElementById("<%= txtAge8.ClientID %>").value;
            var params = new Array();
            params[0] = qryField;
            params[1] = qryValue;
            params[2] = sortField;
            var obj = document.getElementById("Button9");
            ProgressBar9_doWork(obj, params);
            //ProgressBar9_doWork(obj);
        }
        function doProgressWork9() {
            var qryField = document.getElementById("<%= txtName9.ClientID %>").value;
            var qryValue = document.getElementById("<%= txtGender9.ClientID %>").value;
            var sortField = document.getElementById("<%= txtAge9.ClientID %>").value;
            var params = new Array();
            params[0] = qryField;
            params[1] = qryValue;
            params[2] = sortField;
            var obj = document.getElementById("Button10");
            ProgressBar10_doWork(obj, params);
            //ProgressBar10_doWork(obj);
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table>
            <tr>
                <td style="width:20%">
                    姓名 :
                    <asp:TextBox ID="txtName" runat="server"></asp:TextBox><br />
                    性別 :
                    <asp:TextBox ID="txtGender" runat="server"></asp:TextBox><br />
                    年齡 :
                    <asp:TextBox ID="txtAge" runat="server"></asp:TextBox><br />
                    <input id="Button1" type="button" value="執行" onclick="doProgressWork();" />
                    <wcc:ProgressBar ID="ProgressBar1" runat="server" FalseText="系統發生問題!" IsShowFalseText="True"
                        ProgressText="<br/>作業進行中,請稍候---" ProgressType="Image" AjaxServerClass="Demo_ProgressBarDemo"
                        Enabled="False" BackColor="Yellow" BorderColor="Red" BorderStyle="Groove" BorderWidth="2px"
                        ImageUrl="~/images/progressbar13.gif" IsShowSystemExceptionText="True" ProgressTimeOut="20"
                        TimeOutText="執行時間超過" Font-Size="Smaller" />
                </td>
                <td style="width:20%">
                    姓名 :
                    <asp:TextBox ID="txtName1" runat="server"></asp:TextBox><br />
                    性別 :
                    <asp:TextBox ID="txtGender1" runat="server"></asp:TextBox><br />
                    年齡 :
                    <asp:TextBox ID="txtAge1" runat="server"></asp:TextBox><br />
                    <input id="Button2" type="button" value="執行" onclick="doProgressWork1();" />
                    <wcc:ProgressBar ID="ProgressBar2" runat="server" FalseText="系統發生問題!" IsShowFalseText="True"
                        ProgressText="<br/>作業進行中,請稍候---" ProgressType="Image" AjaxServerClass="Demo_ProgressBarDemo"
                        Enabled="False" BackColor="Yellow" BorderColor="Red" BorderStyle="Groove" BorderWidth="2px"
                        ImageUrl="~/images/progressbar13.gif" IsShowSystemExceptionText="True" ProgressTimeOut="20"
                        TimeOutText="執行時間超過" Font-Size="Smaller" />
                </td>
                <td style="width:20%">
                    姓名 :
                    <asp:TextBox ID="txtName2" runat="server"></asp:TextBox><br />
                    性別 :
                    <asp:TextBox ID="txtGender2" runat="server"></asp:TextBox><br />
                    年齡 :
                    <asp:TextBox ID="txtAge2" runat="server"></asp:TextBox><br />
                    <input id="Button3" type="button" value="執行" onclick="doProgressWork2();" />
                    <wcc:ProgressBar ID="ProgressBar3" runat="server" FalseText="系統發生問題!" IsShowFalseText="True"
                        ProgressText="<br/>作業進行中,請稍候---" ProgressType="Image" AjaxServerClass="Demo_ProgressBarDemo"
                        Enabled="False" BackColor="Yellow" BorderColor="Red" BorderStyle="Groove" BorderWidth="2px"
                        ImageUrl="~/images/progressbar13.gif" IsShowSystemExceptionText="True" ProgressTimeOut="20"
                        TimeOutText="執行時間超過" Font-Size="Smaller" />
                </td>
                <td style="width:20%">
                    姓名 :
                    <asp:TextBox ID="txtName3" runat="server"></asp:TextBox><br />
                    性別 :
                    <asp:TextBox ID="txtGender3" runat="server"></asp:TextBox><br />
                    年齡 :
                    <asp:TextBox ID="txtAge3" runat="server"></asp:TextBox><br />
                    <input id="Button4" type="button" value="執行" onclick="doProgressWork3();" />
                    <wcc:ProgressBar ID="ProgressBar4" runat="server" FalseText="系統發生問題!" IsShowFalseText="True"
                        ProgressText="<br/>作業進行中,請稍候---" ProgressType="Image" AjaxServerClass="Demo_ProgressBarDemo"
                        Enabled="False" BackColor="Yellow" BorderColor="Red" BorderStyle="Groove" BorderWidth="2px"
                        ImageUrl="~/images/progressbar13.gif" IsShowSystemExceptionText="True" ProgressTimeOut="20"
                        TimeOutText="執行時間超過" Font-Size="Smaller" />
                </td>
                <td style="width:20%">
                    姓名 :
                    <asp:TextBox ID="txtName4" runat="server"></asp:TextBox><br />
                    性別 :
                    <asp:TextBox ID="txtGender4" runat="server"></asp:TextBox><br />
                    年齡 :
                    <asp:TextBox ID="txtAge4" runat="server"></asp:TextBox><br />
                    <input id="Button5" type="button" value="執行" onclick="doProgressWork4();" />
                    <wcc:ProgressBar ID="ProgressBar5" runat="server" FalseText="系統發生問題!" IsShowFalseText="True"
                        ProgressText="<br/>作業進行中,請稍候---" ProgressType="Image" AjaxServerClass="Demo_ProgressBarDemo"
                        Enabled="False" BackColor="Yellow" BorderColor="Red" BorderStyle="Groove" BorderWidth="2px"
                        ImageUrl="~/images/progressbar13.gif" IsShowSystemExceptionText="True" ProgressTimeOut="20"
                        TimeOutText="執行時間超過" Font-Size="Smaller" />
                </td>
            </tr>

            <tr>
                <td style="width:20%">
                    姓名 :
                    <asp:TextBox ID="txtName5" runat="server"></asp:TextBox><br />
                    性別 :
                    <asp:TextBox ID="txtGender5" runat="server"></asp:TextBox><br />
                    年齡 :
                    <asp:TextBox ID="txtAge5" runat="server"></asp:TextBox><br />
                    <input id="Button6" type="button" value="執行" onclick="doProgressWork5();" />
                    <wcc:ProgressBar ID="ProgressBar6" runat="server" FalseText="系統發生問題!" IsShowFalseText="True"
                        ProgressText="<br/>作業進行中,請稍候---" ProgressType="Image" AjaxServerClass="Demo_ProgressBarDemo"
                        Enabled="False" BackColor="Yellow" BorderColor="Red" BorderStyle="Groove" BorderWidth="2px"
                        ImageUrl="~/images/progressbar13.gif" IsShowSystemExceptionText="True" ProgressTimeOut="20"
                        TimeOutText="執行時間超過" Font-Size="Smaller" />
                </td>
                <td style="width:20%">
                    姓名 :
                    <asp:TextBox ID="txtName6" runat="server"></asp:TextBox><br />
                    性別 :
                    <asp:TextBox ID="txtGender6" runat="server"></asp:TextBox><br />
                    年齡 :
                    <asp:TextBox ID="txtAge6" runat="server"></asp:TextBox><br />
                    <input id="Button7" type="button" value="執行" onclick="doProgressWork6();" />
                    <wcc:ProgressBar ID="ProgressBar7" runat="server" FalseText="系統發生問題!" IsShowFalseText="True"
                        ProgressText="<br/>作業進行中,請稍候---" ProgressType="Image" AjaxServerClass="Demo_ProgressBarDemo"
                        Enabled="False" BackColor="Yellow" BorderColor="Red" BorderStyle="Groove" BorderWidth="2px"
                        ImageUrl="~/images/progressbar13.gif" IsShowSystemExceptionText="True" ProgressTimeOut="20"
                        TimeOutText="執行時間超過" Font-Size="Smaller" />
                </td>
                <td style="width:20%">
                    姓名 :
                    <asp:TextBox ID="txtName7" runat="server"></asp:TextBox><br />
                    性別 :
                    <asp:TextBox ID="txtGender7" runat="server"></asp:TextBox><br />
                    年齡 :
                    <asp:TextBox ID="txtAge7" runat="server"></asp:TextBox><br />
                    <input id="Button8" type="button" value="執行" onclick="doProgressWork7();" />
                    <wcc:ProgressBar ID="ProgressBar8" runat="server" FalseText="系統發生問題!" IsShowFalseText="True"
                        ProgressText="<br/>作業進行中,請稍候---" ProgressType="Image" AjaxServerClass="Demo_ProgressBarDemo"
                        Enabled="False" BackColor="Yellow" BorderColor="Red" BorderStyle="Groove" BorderWidth="2px"
                        ImageUrl="~/images/progressbar13.gif" IsShowSystemExceptionText="True" ProgressTimeOut="20"
                        TimeOutText="執行時間超過" Font-Size="Smaller" />
                </td>
                 <td style="width:20%">
                    姓名 :
                    <asp:TextBox ID="txtName8" runat="server"></asp:TextBox><br />
                    性別 :
                    <asp:TextBox ID="txtGender8" runat="server"></asp:TextBox><br />
                    年齡 :
                    <asp:TextBox ID="txtAge8" runat="server"></asp:TextBox><br />
                    <input id="Button9" type="button" value="執行" onclick="doProgressWork8();" />
                    <wcc:ProgressBar ID="ProgressBar9" runat="server" FalseText="系統發生問題!" IsShowFalseText="True"
                        ProgressText="<br/>作業進行中,請稍候---" ProgressType="Image" AjaxServerClass="Demo_ProgressBarDemo"
                        Enabled="False" BackColor="Yellow" BorderColor="Red" BorderStyle="Groove" BorderWidth="2px"
                        ImageUrl="~/images/progressbar13.gif" IsShowSystemExceptionText="True" ProgressTimeOut="20"
                        TimeOutText="執行時間超過" Font-Size="Smaller" />
                </td>
                 <td style="width:20%">
                    姓名 :
                    <asp:TextBox ID="txtName9" runat="server"></asp:TextBox><br />
                    性別 :
                    <asp:TextBox ID="txtGender9" runat="server"></asp:TextBox><br />
                    年齡 :
                    <asp:TextBox ID="txtAge9" runat="server"></asp:TextBox><br />
                    <input id="Button10" type="button" value="執行" onclick="doProgressWork9();" />
                    <wcc:ProgressBar ID="ProgressBar10" runat="server" FalseText="系統發生問題!" IsShowFalseText="True"
                        ProgressText="<br/>作業進行中,請稍候---" ProgressType="Image" AjaxServerClass="Demo_ProgressBarDemo"
                        Enabled="False" BackColor="Yellow" BorderColor="Red" BorderStyle="Groove" BorderWidth="2px"
                        ImageUrl="~/images/progressbar13.gif" IsShowSystemExceptionText="True" ProgressTimeOut="20"
                        TimeOutText="執行時間超過" Font-Size="Smaller" />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
