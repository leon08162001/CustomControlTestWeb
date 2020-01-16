<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProgressBarDemo1.aspx.cs"
    Inherits="Demo_ProgressBarDemo1" %>

<%@ Register Assembly="WebCustomControls" Namespace="APTemplate" TagPrefix="wcc" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script language="javascript" type="text/javascript">
        function doProgressWork(i) {
            var qryField = document.getElementById("txtName" + i).value;
            var qryValue = document.getElementById("txtGender" + i).value;
            var sortField = document.getElementById("txtAge" + i).value;
            var params = new Array();
            params[0] = qryField;
            params[1] = qryValue;
            params[2] = sortField;
            var obj = document.getElementById("Button" + i);
            window["ProgressBar" + i + "_doWork"](obj, params);
            //var progressBar = eval("ProgressBar" + i + "_doWork");
            //progressBar(obj, params);
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table>
            <tr>
                <td align="center" style="width:20%">
                    姓名 :
                    <asp:TextBox ID="txtName1" runat="server"></asp:TextBox><br />
                    性別 :
                    <asp:TextBox ID="txtGender1" runat="server"></asp:TextBox><br />
                    年齡 :
                    <asp:TextBox ID="txtAge1" runat="server"></asp:TextBox><br />
                    <input id="Button1" type="button" value="執行" onclick="doProgressWork(1);" />
                    <br />
                    <wcc:ProgressBar ID="ProgressBar1" runat="server" FalseText="系統發生問題!" IsShowFalseText="True"
                        ProgressText="<br/>作業進行中,請稍候---" ProgressType="Image" AjaxServerClass="Demo_ProgressBarDemo1"
                        Enabled="False" BackColor="Yellow" BorderColor="Red" BorderStyle="Groove" BorderWidth="2px"
                        ImageUrl="~/images/progressbar13.gif" IsShowSystemExceptionText="True" ProgressTimeOut="20"
                        TimeOutText="執行時間超過" Font-Size="Smaller" />
                </td>
                <td align="center" style="width:20%">
                    姓名 :
                    <asp:TextBox ID="txtName2" runat="server"></asp:TextBox><br />
                    性別 :
                    <asp:TextBox ID="txtGender2" runat="server"></asp:TextBox><br />
                    年齡 :
                    <asp:TextBox ID="txtAge2" runat="server"></asp:TextBox><br />
                    <input id="Button2" type="button" value="執行" onclick="doProgressWork(2);" />
                    <br />
                    <wcc:ProgressBar ID="ProgressBar2" runat="server" FalseText="系統發生問題!" IsShowFalseText="True"
                        ProgressText="<br/>作業進行中,請稍候---" ProgressType="Image" AjaxServerClass="Demo_ProgressBarDemo1"
                        Enabled="False" BackColor="Yellow" BorderColor="Red" BorderStyle="Groove" BorderWidth="2px"
                        ImageUrl="~/images/progressbar13.gif" IsShowSystemExceptionText="True" ProgressTimeOut="20"
                        TimeOutText="執行時間超過" Font-Size="Smaller" />
                </td>
                <td align="center" style="width:20%">
                    姓名 :
                    <asp:TextBox ID="txtName3" runat="server"></asp:TextBox><br />
                    性別 :
                    <asp:TextBox ID="txtGender3" runat="server"></asp:TextBox><br />
                    年齡 :
                    <asp:TextBox ID="txtAge3" runat="server"></asp:TextBox><br />
                    <input id="Button3" type="button" value="執行" onclick="doProgressWork(3);" />
                    <br />
                    <wcc:ProgressBar ID="ProgressBar3" runat="server" FalseText="系統發生問題!" IsShowFalseText="True"
                        ProgressText="<br/>作業進行中,請稍候---" ProgressType="Image" AjaxServerClass="Demo_ProgressBarDemo1"
                        Enabled="False" BackColor="Yellow" BorderColor="Red" BorderStyle="Groove" BorderWidth="2px"
                        ImageUrl="~/images/progressbar13.gif" IsShowSystemExceptionText="True" ProgressTimeOut="20"
                        TimeOutText="執行時間超過" Font-Size="Smaller" />
                </td>
                <td align="center" style="width:20%">
                    姓名 :
                    <asp:TextBox ID="txtName4" runat="server"></asp:TextBox><br />
                    性別 :
                    <asp:TextBox ID="txtGender4" runat="server"></asp:TextBox><br />
                    年齡 :
                    <asp:TextBox ID="txtAge4" runat="server"></asp:TextBox><br />
                    <input id="Button4" type="button" value="執行" onclick="doProgressWork(4);" />
                    <br />
                    <wcc:ProgressBar ID="ProgressBar4" runat="server" FalseText="系統發生問題!" IsShowFalseText="True"
                        ProgressText="<br/>作業進行中,請稍候---" ProgressType="Image" AjaxServerClass="Demo_ProgressBarDemo1"
                        Enabled="False" BackColor="Yellow" BorderColor="Red" BorderStyle="Groove" BorderWidth="2px"
                        ImageUrl="~/images/progressbar13.gif" IsShowSystemExceptionText="True" ProgressTimeOut="20"
                        TimeOutText="執行時間超過" Font-Size="Smaller" />
                </td>
                <td align="center" style="width:20%">
                    姓名 :
                    <asp:TextBox ID="txtName5" runat="server"></asp:TextBox><br />
                    性別 :
                    <asp:TextBox ID="txtGender5" runat="server"></asp:TextBox><br />
                    年齡 :
                    <asp:TextBox ID="txtAge5" runat="server"></asp:TextBox><br />
                    <input id="Button5" type="button" value="執行" onclick="doProgressWork(5);" />
                    <br />
                    <wcc:ProgressBar ID="ProgressBar5" runat="server" FalseText="系統發生問題!" IsShowFalseText="True"
                        ProgressText="<br/>作業進行中,請稍候---" ProgressType="Image" AjaxServerClass="Demo_ProgressBarDemo1"
                        Enabled="False" BackColor="Yellow" BorderColor="Red" BorderStyle="Groove" BorderWidth="2px"
                        ImageUrl="~/images/progressbar13.gif" IsShowSystemExceptionText="True" ProgressTimeOut="20"
                        TimeOutText="執行時間超過" Font-Size="Smaller" />
                </td>
            </tr>

            <tr>
                <td align="center" style="width:20%">
                    姓名 :
                    <asp:TextBox ID="txtName6" runat="server"></asp:TextBox><br />
                    性別 :
                    <asp:TextBox ID="txtGender6" runat="server"></asp:TextBox><br />
                    年齡 :
                    <asp:TextBox ID="txtAge6" runat="server"></asp:TextBox><br />
                    <input id="Button6" type="button" value="執行" onclick="doProgressWork(6);" />
                    <br />
                    <wcc:ProgressBar ID="ProgressBar6" runat="server" FalseText="系統發生問題!" IsShowFalseText="True"
                        ProgressText="<br/>作業進行中,請稍候---" ProgressType="Image" AjaxServerClass="Demo_ProgressBarDemo1"
                        Enabled="False" BackColor="Yellow" BorderColor="Red" BorderStyle="Groove" BorderWidth="2px"
                        ImageUrl="~/images/progressbar13.gif" IsShowSystemExceptionText="True" ProgressTimeOut="20"
                        TimeOutText="執行時間超過" Font-Size="Smaller" />
                </td>
                <td align="center" style="width:20%">
                    姓名 :
                    <asp:TextBox ID="txtName7" runat="server"></asp:TextBox><br />
                    性別 :
                    <asp:TextBox ID="txtGender7" runat="server"></asp:TextBox><br />
                    年齡 :
                    <asp:TextBox ID="txtAge7" runat="server"></asp:TextBox><br />
                    <input id="Button7" type="button" value="執行" onclick="doProgressWork(7);" />
                    <br />
                    <wcc:ProgressBar ID="ProgressBar7" runat="server" FalseText="系統發生問題!" IsShowFalseText="True"
                        ProgressText="<br/>作業進行中,請稍候---" ProgressType="Image" AjaxServerClass="Demo_ProgressBarDemo1"
                        Enabled="False" BackColor="Yellow" BorderColor="Red" BorderStyle="Groove" BorderWidth="2px"
                        ImageUrl="~/images/progressbar13.gif" IsShowSystemExceptionText="True" ProgressTimeOut="20"
                        TimeOutText="執行時間超過" Font-Size="Smaller" />
                </td>
                <td align="center" style="width:20%">
                    姓名 :
                    <asp:TextBox ID="txtName8" runat="server"></asp:TextBox><br />
                    性別 :
                    <asp:TextBox ID="txtGender8" runat="server"></asp:TextBox><br />
                    年齡 :
                    <asp:TextBox ID="txtAge8" runat="server"></asp:TextBox><br />
                    <input id="Button8" type="button" value="執行" onclick="doProgressWork(8);" />
                    <br />
                    <wcc:ProgressBar ID="ProgressBar8" runat="server" FalseText="系統發生問題!" IsShowFalseText="True"
                        ProgressText="<br/>作業進行中,請稍候---" ProgressType="Image" AjaxServerClass="Demo_ProgressBarDemo1"
                        Enabled="False" BackColor="Yellow" BorderColor="Red" BorderStyle="Groove" BorderWidth="2px"
                        ImageUrl="~/images/progressbar13.gif" IsShowSystemExceptionText="True" ProgressTimeOut="20"
                        TimeOutText="執行時間超過" Font-Size="Smaller" />
                </td>
                 <td align="center" style="width:20%">
                    姓名 :
                    <asp:TextBox ID="txtName9" runat="server"></asp:TextBox><br />
                    性別 :
                    <asp:TextBox ID="txtGender9" runat="server"></asp:TextBox><br />
                    年齡 :
                    <asp:TextBox ID="txtAge9" runat="server"></asp:TextBox><br />
                    <input id="Button9" type="button" value="執行" onclick="doProgressWork(9);" />
                    <br />
                    <wcc:ProgressBar ID="ProgressBar9" runat="server" FalseText="系統發生問題!" IsShowFalseText="True"
                        ProgressText="<br/>作業進行中,請稍候---" ProgressType="Image" AjaxServerClass="Demo_ProgressBarDemo1"
                        Enabled="False" BackColor="Yellow" BorderColor="Red" BorderStyle="Groove" BorderWidth="2px"
                        ImageUrl="~/images/progressbar13.gif" IsShowSystemExceptionText="True" ProgressTimeOut="20"
                        TimeOutText="執行時間超過" Font-Size="Smaller" />
                </td>
                 <td align="center" style="width:20%">
                    姓名 :
                    <asp:TextBox ID="txtName10" runat="server"></asp:TextBox><br />
                    性別 :
                    <asp:TextBox ID="txtGender10" runat="server"></asp:TextBox><br />
                    年齡 :
                    <asp:TextBox ID="txtAge10" runat="server"></asp:TextBox><br />
                    <input id="Button10" type="button" value="執行" onclick="doProgressWork(10);" />
                    <br />
                    <wcc:ProgressBar ID="ProgressBar10" runat="server" FalseText="系統發生問題!" IsShowFalseText="True"
                        ProgressText="<br/>作業進行中,請稍候---" ProgressType="Image" AjaxServerClass="Demo_ProgressBarDemo1"
                        Enabled="False" BackColor="Yellow" BorderColor="Red" BorderStyle="Groove" BorderWidth="2px"
                        ImageUrl="~/images/progressbar13.gif" IsShowSystemExceptionText="True" ProgressTimeOut="20"
                        TimeOutText="執行時間超過" Font-Size="Smaller" />
                </td>
            </tr>

            <tr>
                <td align="center" style="width:20%">
                    姓名 :
                    <asp:TextBox ID="txtName11" runat="server"></asp:TextBox><br />
                    性別 :
                    <asp:TextBox ID="txtGender11" runat="server"></asp:TextBox><br />
                    年齡 :
                    <asp:TextBox ID="txtAge11" runat="server"></asp:TextBox><br />
                    <input id="Button11" type="button" value="執行" onclick="doProgressWork(11);" />
                    <br />
                    <wcc:ProgressBar ID="ProgressBar11" runat="server" FalseText="系統發生問題!" IsShowFalseText="True"
                        ProgressText="<br/>作業進行中,請稍候---" ProgressType="Image" AjaxServerClass="Demo_ProgressBarDemo1"
                        Enabled="False" BackColor="Yellow" BorderColor="Red" BorderStyle="Groove" BorderWidth="2px"
                        ImageUrl="~/images/progressbar13.gif" IsShowSystemExceptionText="True" ProgressTimeOut="20"
                        TimeOutText="執行時間超過" Font-Size="Smaller" />
                </td>
                <td align="center" style="width:20%">
                    姓名 :
                    <asp:TextBox ID="txtName12" runat="server"></asp:TextBox><br />
                    性別 :
                    <asp:TextBox ID="txtGender12" runat="server"></asp:TextBox><br />
                    年齡 :
                    <asp:TextBox ID="txtAge12" runat="server"></asp:TextBox><br />
                    <input id="Button12" type="button" value="執行" onclick="doProgressWork(12);" />
                    <br />
                    <wcc:ProgressBar ID="ProgressBar12" runat="server" FalseText="系統發生問題!" IsShowFalseText="True"
                        ProgressText="<br/>作業進行中,請稍候---" ProgressType="Image" AjaxServerClass="Demo_ProgressBarDemo1"
                        Enabled="False" BackColor="Yellow" BorderColor="Red" BorderStyle="Groove" BorderWidth="2px"
                        ImageUrl="~/images/progressbar13.gif" IsShowSystemExceptionText="True" ProgressTimeOut="20"
                        TimeOutText="執行時間超過" Font-Size="Smaller" />
                </td>
                <td align="center" style="width:20%">
                    姓名 :
                    <asp:TextBox ID="txtName13" runat="server"></asp:TextBox><br />
                    性別 :
                    <asp:TextBox ID="txtGender13" runat="server"></asp:TextBox><br />
                    年齡 :
                    <asp:TextBox ID="txtAge13" runat="server"></asp:TextBox><br />
                    <input id="Button13" type="button" value="執行" onclick="doProgressWork(13);" />
                    <br />
                    <wcc:ProgressBar ID="ProgressBar13" runat="server" FalseText="系統發生問題!" IsShowFalseText="True"
                        ProgressText="<br/>作業進行中,請稍候---" ProgressType="Image" AjaxServerClass="Demo_ProgressBarDemo1"
                        Enabled="False" BackColor="Yellow" BorderColor="Red" BorderStyle="Groove" BorderWidth="2px"
                        ImageUrl="~/images/progressbar13.gif" IsShowSystemExceptionText="True" ProgressTimeOut="20"
                        TimeOutText="執行時間超過" Font-Size="Smaller" />
                </td>
                 <td align="center" style="width:20%">
                    姓名 :
                    <asp:TextBox ID="txtName14" runat="server"></asp:TextBox><br />
                    性別 :
                    <asp:TextBox ID="txtGender14" runat="server"></asp:TextBox><br />
                    年齡 :
                    <asp:TextBox ID="txtAge14" runat="server"></asp:TextBox><br />
                    <input id="Button14" type="button" value="執行" onclick="doProgressWork(14);" />
                    <br />
                    <wcc:ProgressBar ID="ProgressBar14" runat="server" FalseText="系統發生問題!" IsShowFalseText="True"
                        ProgressText="<br/>作業進行中,請稍候---" ProgressType="Image" AjaxServerClass="Demo_ProgressBarDemo1"
                        Enabled="False" BackColor="Yellow" BorderColor="Red" BorderStyle="Groove" BorderWidth="2px"
                        ImageUrl="~/images/progressbar13.gif" IsShowSystemExceptionText="True" ProgressTimeOut="20"
                        TimeOutText="執行時間超過" Font-Size="Smaller" />
                </td>
                 <td align="center" style="width:20%">
                    姓名 :
                    <asp:TextBox ID="txtName15" runat="server"></asp:TextBox><br />
                    性別 :
                    <asp:TextBox ID="txtGender15" runat="server"></asp:TextBox><br />
                    年齡 :
                    <asp:TextBox ID="txtAge15" runat="server"></asp:TextBox><br />
                    <input id="Button15" type="button" value="執行" onclick="doProgressWork(15);" />
                    <br />
                    <wcc:ProgressBar ID="ProgressBar15" runat="server" FalseText="系統發生問題!" IsShowFalseText="True"
                        ProgressText="<br/>作業進行中,請稍候---" ProgressType="Image" AjaxServerClass="Demo_ProgressBarDemo1"
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
