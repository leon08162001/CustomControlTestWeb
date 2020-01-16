var oNewWindow;
var oParentForm;
var oReturnValue;
var Page_Decimals=new Array();

function unload()
{ 
		try{
			opener.oNewWindow=undefined;
		}
		catch(exception){}
	  try{
			window.dialogArguments.parentWindow.oNewWindow=undefined;
		}
		catch(exception){}
}

function CloseWindow()
{
	try{
			opener.oNewWindow=undefined;
		}
		catch(exception){}
	  try{
			window.dialogArguments.parentWindow.oNewWindow=undefined;
		}
		catch(exception){}
  window.close();
}

 function SendValueToParent(val)
{
 //取得前一頁所傳過來的物件
  var myObj = window.dialogArguments;
 
 //賦值
  myObj.getData.value = val;
 
 //若不須將子視窗關閉，則註解掉下面這行。
  window.close();
}

function GetValueFromParent()
{ 
var argnum=arguments.length;
  //取得前一頁所傳過來的物件
  var myObj = window.dialogArguments;
  if(myObj!=undefined)
  {
		for(i=0;i<argnum;i=i+2)
		{
			var ReceivedObj=document.getElementById(arguments[i]);
			if(!myObj.getElementById)
			{
				ReceivedObj.value=myObj.elements(arguments[i+1]).value;
			}
			else
			{
			  ReceivedObj.value=myObj.getElementById(arguments[i+1]).value;
			}
		}
	}	
}

function GetValueFromShowModal(ReceivedObj,val)
{
	//取得前一頁所傳過來的物件
  var ParentDoc = window.dialogArguments;
  var ParentForm=ParentDoc.getElementById("Form1");
 
  //賦值
  if(val=="&amp;nbsp;")
  {val="";}
  ParentForm.elements[ReceivedObj].value = val;
 
  //若不須將子視窗關閉，則註解掉下面這行。
  //window.close();
}

function GetValueFromNormal(ReceivedObj,val)
{
	window.opener.document.getElementById(ReceivedObj).value=val;
}


function ParentWindowReLoad()
{ 
		//window.open使用
		try
		{
			window.opener.location.reload(true);
		}
		catch(exception){}
		//showmodaldialog 和 showmodalessdialog使用
		try
		{
			window.dialogArguments.location=window.dialogArguments.location;
		}
		catch(exception){}
}

function RadioButtonUnchecked(val)
{
	var Inputs=document.getElementsByTagName("input");
	var key;
	for(key in Inputs)
	{
		if(Inputs[key].type=="radio" && (Inputs[key].id==val || Inputs[key].name==val || Inputs[key].name.indexOf('_'+val)!=-1 || Inputs[key].id.indexOf('_'+val)!=-1))
		{
			Inputs[key].checked=false;
		}
	}
}

function formatDate(sDate) {
	var sScrap = "";
	var dScrap = new Date(sDate);
	if (dScrap == "NaN") return sScrap;
	
	iDay = dScrap.getDate();
	iMon = dScrap.getMonth();
	iYea = dScrap.getFullYear();
  if ((iMon+1)<=9 )
  {sMon="0"+(iMon+1); }
  else
 {sMon=iMon+1; }
  if (iDay<=9 )
  {sDay="0"+iDay; }
  else
  {sDay=iDay; }
    
  sYea=iYea; 
	sScrap = sYea +"/"+  sMon +"/"+ sDay ;
	return sScrap;
}

function dateChange(sValue) {
	window.returnValue = sValue;
	window.close();
}

function SetCalendar(ImageObj,CalendarObj)
{
 var top=ImageObj.offsetTop;
 var left=ImageObj.offsetLeft;
 CalendarObj.style.display='block';
 CalendarObj.style.position='absolute';
 CalendarObj.style.left=left+'px';
 CalendarObj.style.top=top+'px';
}

function checkID(idStr)
{
  // 依照字母的編號排列，存入陣列備用。
  var letters = new Array('A', 'B', 'C', 'D',
      'E', 'F', 'G', 'H', 'J', 'K', 'L', 'M',
      'N', 'P', 'Q', 'R', 'S', 'T', 'U', 'V',
      'X', 'Y', 'W', 'Z', 'I', 'O');
  // 儲存各個乘數
  var multiply = new Array(1, 9, 8, 7, 6, 5,
                           4, 3, 2, 1);
  var nums = new Array(2);
  var firstChar;
  var firstNum;
  var lastNum;
  var total = 0;
  // 撰寫「正規表達式」。第一個字為英文字母，
  // 第二個字為1或2，後面跟著8個數字，不分大小寫。
  var regExpID=/^[a-z](1|2)\d{8}$/i;
  // 使用「正規表達式」檢驗格式
  if (idStr.search(regExpID)==-1) 
  {
    // 基本格式錯誤
   return false;
  } 
  else 
  {
	// 取出第一個字元和最後一個數字。
	firstChar = idStr.charAt(0).toUpperCase();
	lastNum = idStr.charAt(9);
  }
  // 找出第一個字母對應的數字，並轉換成兩位數數字。
  for (var i=0; i<26; i++) 
  {
	  if (firstChar == letters[i]) 
	  {
	    firstNum = i + 10;
	    nums[0] = Math.floor(firstNum / 10);
	    nums[1] = firstNum - (nums[0] * 10);
	    break;
	  }
  }
  // 執行加總計算
  for(var i=0; i<multiply.length; i++)
  {
    if (i<2) 
    {
      total += nums[i] * multiply[i];
    } 
    else 
    {
      total += parseInt(idStr.charAt(i-1)) *
               multiply[i];
    }
  }
  // 和最後一個數字比對
  if ((10 - (total % 10))!= lastNum) 
  {
	  return false;
  }
    return true;
}

function AlertValidation() 
{
  var i;
  var Vals=event.srcElement.Validators;
  for (i = 0; i < Vals.length; i++)
  {
    ValidatorValidate(Vals[i]);
    if(Vals[i].isvalid==false)
    {
      if(Vals[i].errormessage!=undefined)
      {
        window.alert(Vals[i].errormessage);
        break ;
      }
    }
  }
  ValidatorUpdateIsValid();    
  Page_BlockSubmit = !Page_IsValid;
  return Page_IsValid;
}

function UpdateErrorMessage() 
{
  var i;
  var Vals=event.srcElement.Validators;
  for (i = 0; i < Vals.length; i++) 
  {
    ValidatorValidate(Vals[i]);
  }
  ValidatorUpdateIsValid();    
  Page_BlockSubmit = !Page_IsValid;
  return Page_IsValid;
}

function isTriDecimal(value,DecimalLen)
{
	if(value!=null&&value!='')
	{
		var decimalIndex=value.indexOf('.');
		if(decimalIndex=='-1')
		{
		  return false;
	  }
	  else
	  {
		  var decimalPart=value.substring(decimalIndex+1,value.length);
		  if(decimalPart.length>DecimalLen-1)
		  {
			  return true;
		  }
		  else
		  {
			  return false;
		  }
	  }
	}
	return false;
}

function CarryProcess()
{
  var i;
  for (i=0;i< Page_Decimals.length;i++)
  {
    var obj=Page_Decimals[i][0];
    var CarryValue=Page_Decimals[i][1];
    var val=obj.value;
    var digit;
    var carryindex=val.indexOf(".");
    if (carryindex==-1)
    {
      digit=0;
      continue;
    }  
    else
    {
      digit= Math.pow(10,carryindex-(val.length-1));
    }  
    var lstdigit=val.substring(val.length-1,val.length);
    var tempval=val.substring(0,val.length-1);
    if(CarryValue==0)
      obj.value=val;
    if(CarryValue==1)
      {obj.value= parseInt(lstdigit)>=5 ? parseFloat(tempval)+(parseFloat(digit)*10.01) : parseFloat(tempval);}  
    if(CarryValue==2)
      {obj.value= parseInt(lstdigit)>0 ? parseFloat(tempval)+(parseFloat(digit)*10) : parseFloat(tempval);}
    if(CarryValue==3)
      {obj.value=parseFloat(tempval);}
    var tempvalue=obj.value.toString();
    obj.value=tempvalue.substring(0,val.length-1);
    if(obj.value.indexOf(".")==(obj.value).length-1)
      {obj.value=(obj.value).replace(".","");}
  }
}

function getBid(s)
{
	return document.getElementById(s);
}
function getBmc(s)
{
	return document.getElementByName(s);
}
		
function showNext(sid,obj1,obj2,level)
{ 
  if(sid==null || sid=="" || sid.length<1)return;
  var slt1 =getBid(obj1);
  var slt2 =getBid(obj2);
	var v;
	if(level==1)
	  v= APTemplate.DropDownList_Multiple.GetSecond_ThirdListData(sid).value;
	if(level==2)
	{
	  v= APTemplate.DropDownList_Multiple.GetThirdListData(sid).value;
	 } 
	if (v != null)
	{      
		if(v != null && typeof(v) == "object" && v.Tables != null)
		{		
		  if(level==1)
		  {
			  if(slt1 !=null)
			    slt1.options.length = 0;
			  if(slt2 !=null)
			    slt2.options.length = 0;
			}
			if(level==2)
			{
			  if(slt1 !=null)
				  slt1.options.length = 0;
		  }		
		  
		  if(level==1)
		  {	
		    if(v.Tables[0] != null)
		    {	
		      for(var i=0; i<v.Tables[0].Rows.length; i++)
			    {
				    var txt = v.Tables[0].Rows[i].Text;
				    var vol = v.Tables[0].Rows[i].Value;
				    slt1.options.add(new Option(txt,vol));
			    }
			  }  
			  
			   if(v.Tables[1] != null)
		     {	
			     for(var i=0; i<v.Tables[1].Rows.length; i++)
			     {
				     var txt = v.Tables[1].Rows[i].Text;
				     var vol = v.Tables[1].Rows[i].Value;
				     slt2.options.add(new Option(txt,vol));
			     }
			   }
			}
			
			if(level==2)
		  {
		    if(v.Tables[0] != null)
		    {	
		      for(var i=0; i<v.Tables[0].Rows.length; i++)
			    {
				    var txt = v.Tables[0].Rows[i].Text;
				    var vol = v.Tables[0].Rows[i].Value;
				    slt1.options.add(new Option(txt,vol));
			    }
			  } 
			}  
		}
	}	
	return;
}

function AddListOneToListCallBack(SourceClientID,DestinationClientID,PositionType,ControlID)
{
  var Result;
  var idx=document.getElementById(SourceClientID).selectedIndex;
  Result= APTemplate.ListBoxToListBox.AddListOneToList(PositionType,idx,ControlID).value;
  if (Result != null)
	{
	  if(Result == true)
	  {
	     IE_AddListOneToList(SourceClientID,DestinationClientID);
	  }
	} 
}

function AddListAllToListCallBack(SourceClientID,DestinationClientID,PositionType,ControlID)
{
  var Result;
  Result= APTemplate.ListBoxToListBox.AddListAllToList(PositionType,ControlID).value;
  if (Result != null)
	{
	  if(Result == true)
	  {
	     IE_AddListAllToList(SourceClientID,DestinationClientID);
	  }
	} 
}

var sel1
var sel2
function IE_AddListOneToList(SourceClientID,DestinationClientID)
{
	sel1 = document.getElementById(SourceClientID);
	sel2 = document.getElementById(DestinationClientID);
    if (sel1.selectedIndex!=-1)
    {
			var num=sel1.selectedIndex;
			var option=new Option(sel1.options[num].text);
			var item=sel2.options.length;
			sel2.options[item]=option;
			
			sel1.remove(num);
	  }
}

function IE_AddListAllToList(SourceClientID,DestinationClientID)
{
	sel1 = document.getElementById(SourceClientID);
	sel2 = document.getElementById(DestinationClientID);
	var i;
    for (i=0;i<sel1.options.length;i++)
    {
      var option=new Option(sel1.options[i].text,sel1.options[i].value);
      sel2.options[sel2.options.length]=option;
	  }
	  var len=sel1.options.length;
	  for (i=0;i<len;i++)
    {
      sel1.remove(0);
    }
}

function SetDaysInMonthCallBack(yearobj,monthobj,dateobj)
{
  var Result;
  var yearidx=yearobj.selectedIndex;
  var monthidx=monthobj.selectedIndex;
  var yearval=yearobj.options[yearidx].value;
  var monthval=monthobj.options[monthidx].value;
  Result= APTemplate.DropDownList_Date.SetDaysInMonth(parseInt(yearval),parseInt(monthval)).value;
    if (Result != null)
	  {
	    var len=dateobj.options.length;
	    for(i=0;i<len;i++)
	    {
	      dateobj.options.remove(0);
	    }
	    var datearry=Result.split(";");
	    for(i=0;i<datearry.length;i++)
	    {
	      var oOption = document.createElement("OPTION");
        dateobj.options.add(oOption);
        oOption.innerText = datearry[i];
        oOption.value = datearry[i];
	    }
	  }
}

function SetHiddenCalValue(yearobj,monthobj,dateobj,hiddenobj)
{
  var yearidx=yearobj.selectedIndex;
  var monthidx=monthobj.selectedIndex;
  var dateidx=dateobj.selectedIndex;
  var yearval=yearobj.options[yearidx].value;
  var monthval=monthobj.options[monthidx].value;
  var dateval=dateobj.options[dateidx].value;
  hiddenobj.value=yearval + "/" + monthval + "/" + dateval;
}

function CaptchaTimeoutCheck(MinTimeOut,MaxTimeOut)
{
  var d,t,utchr,localhr;
  d = new Date();
  t = d.getTime();
  utchr=d.getUTCHours();
  localhr=d.getHours();
  if(parseFloat(t) < (parseFloat(MinTimeOut) - parseFloat((localhr-utchr)*3600*1000)))
  {
    window.alert('你送出的時間太快,可能是機器人操作行為!');
    window.location.reload(false);
    return false;
  }
  if(MaxTimeOut!=null)
  {
    if(parseFloat(t) > (parseFloat(MaxTimeOut) - parseFloat((localhr-utchr)*3600*1000)))
    {
      window.alert('你送出的時間太慢,可能是駭客操作行為!');
      window.location.reload(false);
      return false;
    }
  }
  return true;
}

function getXPosition(obj)
{
  var e=obj;
  var x=e.offsetLeft;
  while(e=e.offsetParent)
  {
    x+=e.offsetLeft;
  }
  return x;
}
