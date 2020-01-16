<%@ Page Language="C#" AutoEventWireup="true" CodeFile="test11.aspx.cs" Inherits="test11" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script language="javascript" type="text/javascript" src="js/ProgressBar.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
     <div id="ProgressBar2_divProgressBar" 
            style="border: 2px Dotted #FFFF00; padding: 2px; display:inline-block; visibility:hidden; text-align:center; position:relative; background-color:#FFFF00; color:#000000;  top: 0px; left: 0px;">
<%--	<span id="ProgressBar2_progress1">&nbsp;&nbsp;</span> <span id="ProgressBar2_progress2">&nbsp;&nbsp;</span> <span id="ProgressBar2_progress3">&nbsp;&nbsp;</span> <span id="ProgressBar2_progress4">&nbsp;&nbsp;</span> <span id="ProgressBar2_progress5">&nbsp;&nbsp;</span> <span id="ProgressBar2_progress6">&nbsp;&nbsp;</span> <span id="ProgressBar2_progress7">&nbsp;&nbsp;</span> <span id="ProgressBar2_progress8">&nbsp;&nbsp;</span> <span id="ProgressBar2_progress9">&nbsp;&nbsp;</span> 
--%>	
   <asp:Image ID="ImgProgressBar" runat="server" 
             ImageUrl="~/images/progress_bar.gif" />
<br /><span id="ProgressBar2_progressText" style="font-size:10pt;font-weight:normal;font-style:normal;text-decoration:none;">更新中...</span>
</div>
  <input type="submit" name="Button2" value="Button" onclick="ProgressBar2_doWork(this);return false;" id="Button2" />

    </div>
    </form>
    <script language="javascript" type="text/javascript"> 
	 var activeObj;    //促發progressbar的物件 
	 var ProgressBar2= new ProgressBar(); 
	 var isShowSystemExceptionText_ProgressBar2 = true; 
	 var isShowTrueText_ProgressBar2 = true; 
	 var isShowFalseText_ProgressBar2 = true; 
	 var trueText_ProgressBar2 = "更新成功!"; 
	 var falseText_ProgressBar2 = "更新失敗!"; 
	 var timeoutText_ProgressBar2 = "執行時間超過"; 
	 var ProgressBar2_statisticTimer=""; 
	 function ProgressBar2_doWork(disabledObj) { 
	 if(arguments.length==1){ 
	 activeObj = disabledObj; 
	 activeObj.disabled = true; 
	 } 
	 ProgressBar2_startProgressBar('ProgressBar2_divProgressBar'); 
	 AjaxPro.timeoutPeriod = 1000000; 
	 AjaxPro.onError = ProgressBar2_onError; 
	 AjaxPro.onTimeout = ProgressBar2_onTimeout;
	 test11.ProgressBar2_DoWork(ProgressBar2_doWorkCallBack);
	 setTimeout("ProgressBar2_statisticProgress()", 500); 
	 /* 
	 請在.cs檔加入下列程式碼 
	 using AjaxPro; 
	 
	 protected void Page_Load(object sender, EventArgs e) 
	 { 
	   AjaxPro.Utility.RegisterTypeForAjax(typeof(test11)); 
	 } 
	 
	 [AjaxMethod()] 
	 public bool ProgressBar2_DoWork() 
	 { 
	 
	 } 
	 */ 
	 } 
	 
	 function ProgressBar2_doWorkCallBack(res) { 
	 ProgressBar2_stopProgressBar('ProgressBar2_divProgressBar'); 
	 if(activeObj != null){ 
	 activeObj.disabled = false; 
	 } 
	 if(isShowTrueText_ProgressBar2  && res.value){ 
	 window.alert(trueText_ProgressBar2); 
	 } 
	 if(isShowFalseText_ProgressBar2  && res.value == false){ 
	 window.alert(falseText_ProgressBar2); 
	 } 
	 } 
	 
	 function ProgressBar2_statisticProgressCallBack(res) { 
	 var progressTextElement = document.getElementById("ProgressBar2_progressText"); 
	 progressTextElement.innerText ="更新中...(" + res.value + "%)"; 
	 } 
	 
	 function ProgressBar2_onError(error){ 
	 ProgressBar2_stopProgressBar('ProgressBar2_divProgressBar'); 
	 window.alert(error.Message + "\r\n[ExceptionType:" + error.Type + "]"); 
	 } 
	 
	 function ProgressBar2_onTimeout(){ 
	 ProgressBar2_stopProgressBar('ProgressBar2_divProgressBar'); 
	 if(activeObj != null){ 
	 activeObj.disabled = false; 
	 } 
	 if(timeoutText_ProgressBar2 != ""){ 
	 window.alert(timeoutText_ProgressBar2); 
	 } 
	 } 
	 
	 function ProgressBar2_startProgressBar(id) { 
	 document.getElementById(id).style.visibility = "visible"; 
	 //ProgressBar2.update('ProgressBar2'); 
	 }

	 //var i = 0;
	 function ProgressBar2_statisticProgress() {
	 var progressTextElement = document.getElementById("ProgressBar2_progressText");
	 var i = test11.ProgressBar2_StatisticProgress().value; //synchronisch call to retrieve current progress
	 if (i < 100) {
	     progressTextElement.innerText = "更新中...(" + i + "%)";
	     setTimeout("ProgressBar2_statisticProgress()", 1000); //Retrieve current Progress every second
	 }
	 else {
	     progressTextElement.innerText = "更新中...(100%)";
	 }
	 } 
	 
	 /* 
	 Hides the progress bar and corresponding text 
	 */ 
	 function ProgressBar2_stopProgressBar(id) { 
	 clearInterval(ProgressBar2_statisticTimer);
	 var progressTextElement = document.getElementById("ProgressBar2_progressText"); 
	 progressTextElement.innerText ="更新中..."; 
	 document.getElementById(id).style.visibility = "hidden"; 
	 //ProgressBar2.stop('ProgressBar2'); 
	 } 
	 </script> 
</body>
</html>
