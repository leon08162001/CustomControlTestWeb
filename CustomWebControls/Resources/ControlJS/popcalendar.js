//	written	by Tan Ling	Wee	on 2 Dec 2001
//	last updated 23 June 2002
//	email :	fuushikaden@yahoo.com

//	if(typeof vLangue == 'undefined')
//	{
//		vLangue = 1;
//	}
//	if(typeof vWeekManagement == 'undefined')
//	{
//		vWeekManagement = 1;
//	}	
	
var ieVer = function () {
    var v = 4, //原作者的此处代码是3，考虑了IE5的情况，我改为4。
        div = document.createElement('div'),
        i = div.getElementsByTagName('i');
    do {
        div.innerHTML = '<!--[if gt IE ' + (++v) + ']><i></i><![endif]-->';
    } while (i[0]);
    return v > 5 ? v : false; //如果不是IE，之前返回undefined，现改为返回false。
}();
    var browserlang = window.navigator.userLanguage || window.navigator.language; 
    var languages = browserlang.substr(0,2);	        // Default Language: en - english ; es - spanish; de - german
	languages = (languages!='zh' && languages!='en' && languages!='es' && languages!='de') ? "zh" : languages;
	var	fixedX = -1;			                    // x position (-1 if to appear below control)
	var	fixedY = -1;			                    // y position (-1 if to appear below control)
	var startAt = 0;                                // 0 - sunday ; 1 - monday
	var showWeekNumber = 1;			                // 0 - don't show; 1 - show
	var showToday = 1;				                // 0 - don't show; 1 - show
	var imgDir = "ControlImages/";	                // directory for images ... e.g. var imgDir="/img/"
//	var gotoStrings = "前往目前月份";
//	var todayStrings = "今天是";
//	var weekStrings = "週";
//	var scrollLeftMessagess = "點擊這捲動到上一個月份.如果按住滑鼠左鍵不放會自動捲動.";
//	var scrollRightMessages = "點擊這捲動到下一個月份.如果按住滑鼠左鍵不放會自動捲動.";
//	var selectMonthMessages = "選擇一個月份.";
//	var selectYearMessages = "選擇一個年份.";
//	var selectDateMessages = "選擇 [date] 這天."; // do not replace [date], it will be replaced by date.
//  var altCloseCalendars = "關閉日曆";
    var gotoStrings = {
	    zh : '前往目前月份',
		en : 'Go To Current Month',
		es : 'Ir al Mes Actual',
		de : 'Gehe zu aktuellem Monat'
		
	};
	var todayStrings = {
	    zh : '今天是',
		en : 'Today is',
		es : 'Hoy es',
		de : 'Heute ist'
	};
	var weekStrings = {
	    zh : '週',
		en : 'Wk',
		es : 'Sem',
		de : 'KW'		
	};
	var scrollLeftMessages = {
	    zh : '點擊這捲動到上一個月份.如果按住滑鼠左鍵不放會自動捲動.',
		en : 'Click to scroll to previous month. Hold mouse button to scroll automatically.',
		es : 'Presione para pasar al mes anterior. Deje presionado para pasar varios meses.',
		de : 'Klicken um zum vorigen Monat zu gelangen. GedrÃ¼ckt halten, um automatisch weiter zu scrollen.'
		
	};
	var scrollRightMessages = {
		zh : '點擊這捲動到下一個月份.如果按住滑鼠左鍵不放會自動捲動.',
		en : 'Click to scroll to next month. Hold mouse button to scroll automatically.',
		es : 'Presione para pasar al siguiente mes. Deje presionado para pasar varios meses.',
		de : 'Klicken um zum nÃ¤chsten Monat zu gelangen. GedrÃ¼ckt halten, um automatisch weiter zu scrollen.'
	};
	var selectMonthMessages = {
	    zh : '選擇一個月份.',
		en : 'Click to select a month.',
		es : 'Presione para seleccionar un mes',
		de : 'Klicken um Monat auszuwÃ¤hlen'
	};
	var selectYearMessages = {
	    zh : '選擇一個年份.',
		en : 'Click to select a year.',
		es : 'Presione para seleccionar un aÃ±o',
		de : 'Klicken um Jahr auszuwÃ¤hlen'
	};
	var selectDateMessages = {		// do not replace [date], it will be replaced by date.
	    zh : '選擇 [date] 這天.',
		en : 'Select [date] as date.',
		es : 'Seleccione [date] como fecha',
		de : 'WÃ¤hle [date] als Datum.'
	};
	var altCloseCalendars = {
	    zh : '關閉日曆.',
		en : 'Close calendar.',
		es : 'Close calendar.',
		de : 'Close calendar.'
	};
	//var	monthNames =	new	Array("January","February","March","April","May","June","July","August","September","October","November","December")
	//var	monthNames =	new	Array("一月","二月","三月","四月","五月","六月","七月","八月","九月","十月","十一月","十二月");
	var	monthNames = {
	    zh : new Array('一月','二月','三月','四月','五月','六月','七月','八月','九月','十月','十一月','十二月'),
		en : new Array('January','February','March','April','May','June','July','August','September','October','November','December'),
		es : new Array('Enero','Febrero','Marzo','Abril','Mayo','Junio','Julio','Agosto','Septiembre','Octubre','Noviembre','Diciembre'),
		de : new Array('Januar','Februar','MÃ¤rz','April','Mai','Juni','Juli','August','September','Oktober','November','Dezember')
	};
	
	//dayNames = new Array	("Sun","Mon","TueG","Wed","Thu","Fri","Sat");
	//dayNames = new Array	("日","一","二","三","四","五","六");
	if (startAt==0) {
		dayNames = {
		    zh : new Array('日','一','二','三','四','五','六'),
			en : new Array('Sun','Mon','Tue','Wed','Thu','Fri','Sat'),
			es : new Array('Dom','Lun','Mar','Mie','Jue','Vie','Sab'),
			de : new Array('So','Mo','Di','Mi','Do','Fr','Sa')
		};
	} else {
		dayNames = {
		    zh : new Array('一','二','三','四','五','六','日'),
			en : new Array('Mon','Tue','Wed','Thu','Fri','Sat','Sun'),
			es : new Array('Lun','Mar','Mie','Jue','Vie','Sab','Dom'),
			de : new Array('Mo','Di','Mi','Do','Fr','Sa','So')
		};
	}
		
	//arrTemp = dayNames.slice(startAt,7);
	//dayNames = arrTemp.concat(dayNames.slice(0,startAt));	
	var	crossobj, crossMonthObj, crossYearObj, monthSelected, yearSelected, dateSelected, omonthSelected, oyearSelected, odateSelected, monthConstructed, yearConstructed, intervalID1, intervalID2, timeoutID1, timeoutID2, ctlToPlaceValue, ctlNow, dateFormat, nStartingYear;

	var	bPageLoaded=false;
	var	ie=document.all;
	var	dom=document.getElementById;

	var	ns4=document.layers;
	var	today =	new	Date();
	var	dateNow	 = today.getDate();
	var	monthNow = today.getMonth();
	var yearNow = ieVer!=9 ? today.getYear() : 1900 + today.getYear();
	var	imgsrc = new Array("drop1.gif","drop2.gif","left1.gif","left2.gif","right1.gif","right2.gif");
	var	img	= new Array();

	var bShow = false;

    /* hides <select> and <applet> objects (for IE only) */
	function hideElement( elmID, overDiv )
	{
		if( ie )
		{
			for( i = 0; i < document.all.tags( elmID ).length; i++ )
			{
				obj = document.all.tags( elmID )[i];
				if( !obj || !obj.offsetParent )
				{
					continue;
				}
      
				// Find the element's offsetTop and offsetLeft relative to the BODY tag.
				objLeft   = obj.offsetLeft;
				objTop    = obj.offsetTop;
				objParent = obj.offsetParent;
          
				while( objParent.tagName.toUpperCase() != "BODY" )
				{
					objLeft  += objParent.offsetLeft;
					objTop   += objParent.offsetTop;
					objParent = objParent.offsetParent;
				}
      
				objHeight = obj.offsetHeight;
				objWidth = obj.offsetWidth;
      
				if(( overDiv.offsetLeft + overDiv.offsetWidth ) <= objLeft );
				else if(( overDiv.offsetTop + overDiv.offsetHeight ) <= objTop );
				else if( overDiv.offsetTop >= ( objTop + objHeight ));
				else if( overDiv.offsetLeft >= ( objLeft + objWidth ));
				else
				{
				  obj.style.visibility = "hidden";
				}
			}
		}
	}
     
    /*
    * unhides <select> and <applet> objects (for IE only)
    */
    function showElement( elmID )
    {
      if( ie )
      {
        for( i = 0; i < document.all.tags( elmID ).length; i++ )
        {
          obj = document.all.tags( elmID )[i];
          
          if( !obj || !obj.offsetParent )
          {
            continue;
          }
        
          obj.style.visibility = "";
        }
      }
    }

	function HolidayRec (d, m, y, desc)
	{
		this.d = d
		this.m = m
		this.y = y
		this.desc = desc
	}

	var HolidaysCounter = 0
	var Holidays = new Array()

	function addHoliday (d, m, y, desc)
	{
		Holidays[HolidaysCounter++] = new HolidayRec ( d, m, y, desc )
	}

	if (dom)
	{
		for	(i=0;i<imgsrc.length;i++)
		{
			img[i] = new Image
			img[i].src = imgDir + imgsrc[i]
		}
		document.write("<div onclick='bShow=true' id='calendar'	style='z-index:+9999999;position:absolute;visibility:hidden;'><table	width=" + ((showWeekNumber == 1) ? 250 : 220) + " style='font-family:arial;font-size:11px;border-width:1;border-style:solid;border-color:#a0a0a0;font-family:arial; font-size:11px}' bgcolor='#ffffff'><tr bgcolor='#0000aa'><td><table width='" + ((showWeekNumber == 1) ? 248 : 218) + "'><tr><td style='padding:2px;font-family:arial; font-size:11px;'><font color='#ffffff'><B><span id='caption'></span></B></font></td><td align=right><a href='javascript:hideCalendar()'><IMG SRC='<%= WebResource("APTemplate.Resources.close.gif") %>' name=close WIDTH='15' HEIGHT='13' BORDER='0' ALT='" + altCloseCalendars[languages] + "'></a></td></tr></table></td></tr><tr><td style='padding:5px' bgcolor=#ffffff><span id='content'></span></td></tr>")
			
		if (showToday==1)
		{
			document.write ("<tr bgcolor=#f0f0f0><td style='padding:5px' align=center><span id='lblToday'></span></td></tr>")
		}

		document.write("</table></div><div id='selectMonth' style='z-index:+9999999;position:absolute;visibility:hidden;'></div><div id='selectYear' style='z-index:+9999999;position:absolute;visibility:hidden;'></div>");
	}

	var	styleAnchor="text-decoration:none;color:black;"
	var	styleLightBorder="border-style:solid;border-width:1px;border-color:#a0a0a0;"

	function swapImage(srcImg, destImg){
		if (ie)	
		{
		  if(destImg=="left1.gif")
		    document.getElementById(srcImg).setAttribute("src",'<%= WebResource("APTemplate.Resources.left1.gif") %>');
		  if(destImg=="left2.gif")
		    document.getElementById(srcImg).setAttribute("src",'<%= WebResource("APTemplate.Resources.left2.gif") %>');
		  if(destImg=="right1.gif")
		    document.getElementById(srcImg).setAttribute("src",'<%= WebResource("APTemplate.Resources.right1.gif") %>');
		  if(destImg=="right2.gif")
		    document.getElementById(srcImg).setAttribute("src",'<%= WebResource("APTemplate.Resources.right2.gif") %>');
		  if(destImg=="drop1.gif")
		    document.getElementById(srcImg).setAttribute("src",'<%= WebResource("APTemplate.Resources.drop1.gif") %>');
		  if(destImg=="drop2.gif")
		    document.getElementById(srcImg).setAttribute("src",'<%= WebResource("APTemplate.Resources.drop2.gif") %>'); 
		      
		}
	}

	function init()	
	{
		if (!ns4)
		{
			if (!ie) { yearNow += 1900	}

			crossobj=(dom)?document.getElementById("calendar").style : ie? document.all.calendar : document.calendar
			hideCalendar()

			crossMonthObj=(dom)?document.getElementById("selectMonth").style : ie? document.all.selectMonth	: document.selectMonth

			crossYearObj=(dom)?document.getElementById("selectYear").style : ie? document.all.selectYear : document.selectYear

			monthConstructed=false;
			yearConstructed=false;

			if (showToday==1)
			{
			    document.getElementById("lblToday").innerHTML =	todayStrings[languages] + " <a onmousemover='window.status=\""+gotoStrings[languages]+"\"' onmouseout='window.status=\"\"' title='"+gotoStrings[languages]+"' style='"+styleAnchor+"' href='javascript:monthSelected=monthNow;yearSelected=yearNow;constructCalendar();'>"+dayNames[languages][firstdayofweek(today.getDay())]+", " + dateNow + " " + monthNames[languages][monthNow].substring(0,3)	+ "	" +	yearNow	+ "</a>"
			}
			sHTML1="<span id='spanLeft'	style='border-style:solid;border-width:1;border-color:#3366FF;cursor:pointer' onmouseover='swapImage(\"changeLeft\",\"left2.gif\");this.style.borderColor=\"#88AAFF\";window.status=\""+scrollLeftMessages[languages]+"\"' onclick='javascript:decMonth()' onmouseout='clearInterval(intervalID1);swapImage(\"changeLeft\",\"left1.gif\");this.style.borderColor=\"#3366FF\";window.status=\"\"' onmousedown='clearTimeout(timeoutID1);timeoutID1=setTimeout(\"StartDecMonth()\",500)'	onmouseup='clearTimeout(timeoutID1);clearInterval(intervalID1)'>&nbsp<IMG id='changeLeft' SRC='<%= WebResource("APTemplate.Resources.left1.gif") %>' width=10 height=11 BORDER=0>&nbsp</span>&nbsp;"
			sHTML1+="<span id='spanRight' style='border-style:solid;border-width:1;border-color:#3366FF;cursor:pointer'	onmouseover='swapImage(\"changeRight\",\"right2.gif\");this.style.borderColor=\"#88AAFF\";window.status=\""+scrollRightMessages[languages]+"\"' onmouseout='clearInterval(intervalID1);swapImage(\"changeRight\",\"right1.gif\");this.style.borderColor=\"#3366FF\";window.status=\"\"' onclick='incMonth()' onmousedown='clearTimeout(timeoutID1);timeoutID1=setTimeout(\"StartIncMonth()\",500)'	onmouseup='clearTimeout(timeoutID1);clearInterval(intervalID1)'>&nbsp<IMG id='changeRight' SRC='<%= WebResource("APTemplate.Resources.right1.gif") %>'	width=10 height=11 BORDER=0>&nbsp</span>&nbsp"
			sHTML1+="<span id='spanMonth' style='border-style:solid;border-width:1;border-color:#3366FF;cursor:pointer'	onmouseover='swapImage(\"changeMonth\",\"drop2.gif\");this.style.borderColor=\"#88AAFF\";window.status=\""+selectMonthMessages[languages]+"\"' onmouseout='swapImage(\"changeMonth\",\"drop1.gif\");this.style.borderColor=\"#3366FF\";window.status=\"\"' onclick='popUpMonth()'></span>&nbsp;"
			sHTML1+="<span id='spanYear' style='border-style:solid;border-width:1;border-color:#3366FF;cursor:pointer' onmouseover='swapImage(\"changeYear\",\"drop2.gif\");this.style.borderColor=\"#88AAFF\";window.status=\""+selectYearMessages[languages]+"\"'	onmouseout='swapImage(\"changeYear\",\"drop1.gif\");this.style.borderColor=\"#3366FF\";window.status=\"\"'	onclick='popUpYear()'></span>&nbsp;"		
			document.getElementById("caption").innerHTML  =	sHTML1

			bPageLoaded=true
		}
	}
	function firstdayofweek(day)
	{
	day -= startAt
	if (day < 0){day = 7 + day}
	return day
	}

	function hideCalendar()	{
		crossobj.visibility="hidden"
		if (crossMonthObj != null){crossMonthObj.visibility="hidden"}
		if (crossYearObj !=	null){crossYearObj.visibility="hidden"}

	    showElement( 'SELECT' );
		showElement( 'APPLET' );
	}

	function padZero(num) {
		return (num	< 10)? '0' + num : num ;
	}

	function constructDate(d,m,y)
	{
		sTmp = dateFormat;	
		//sTmp = "yyyy/mm/dd hh:mm:ss";
		sTmp = sTmp.replace	("dd","<e>");
		sTmp = sTmp.replace	("d","<d>");
		sTmp = sTmp.replace	("<e>",padZero(d));
		sTmp = sTmp.replace	("<d>",d);
		sTmp = sTmp.replace	("mmm","<o>");
		sTmp = sTmp.replace	("mm","<n>");
//		sTmp = sTmp.replace	("m","<m>");
//		sTmp = sTmp.replace	("<m>",m+1);
		sTmp = sTmp.replace	("<n>",padZero(m+1));
		sTmp = sTmp.replace	("<o>",monthNames[languages][m]);
		sTmp = sTmp.replace ("yyyy",y);
		var dDate = new Date();
		if (sTmp.indexOf("HH")>-1)
		{
		  var hh = dDate.getHours().toString(10);
		  if(hh.length==1) hh="0" + hh;
		  sTmp = sTmp.replace	("HH",hh);
		}
		if (sTmp.indexOf("mm")>-1)
		{
		 var mm = dDate.getMinutes().toString(10);
		 if(mm.length==1) mm="0" + mm;
		  sTmp = sTmp.replace	("mm",mm);
		}
		if (sTmp.indexOf("ss")>-1)
		{
		 var ss = dDate.getSeconds().toString(10);
		 if(ss.length==1) ss="0" + ss;
		  sTmp = sTmp.replace	("ss",ss);
		}
		return sTmp;
	}

	function closeCalendar() {
		var	sTmp

		hideCalendar();
		ctlToPlaceValue.value =	constructDate(dateSelected,monthSelected,yearSelected)
	}

	/*** Month Pulldown	***/

	function StartDecMonth()
	{
		intervalID1=setInterval("decMonth()",80)
	}

	function StartIncMonth()
	{
		intervalID1=setInterval("incMonth()",80)
	}

	function incMonth () {
		monthSelected++
		if (monthSelected>11) {
			monthSelected=0
			yearSelected++
		}
		constructCalendar()
	}

	function decMonth () {
		monthSelected--
		if (monthSelected<0) {
			monthSelected=11
			yearSelected--
		}
		constructCalendar()
	}

	function constructMonth() {
		popDownYear()
		if (!monthConstructed) {
			sHTML =	""
			for	(i=0; i<12;	i++) {
				sName =	monthNames[languages][i];
				if (i==monthSelected){
					sName =	"<B>" +	sName +	"</B>"
				}
				sHTML += "<tr><td id='m" + i + "' onmouseover='this.style.backgroundColor=\"#FFCC99\"' onmouseout='this.style.backgroundColor=\"\"' style='cursor:pointer' onclick='monthConstructed=false;monthSelected=" + i + ";constructCalendar();popDownMonth();event.cancelBubble=true'>&nbsp;" + sName + "&nbsp;</td></tr>"
			}

			document.getElementById("selectMonth").innerHTML = "<table width=52	style='font-family:arial; font-size:11px; border-width:1; border-style:solid; border-color:#a0a0a0;' bgcolor='#FFFFDD' cellspacing=0 onmouseover='clearTimeout(timeoutID1)'	onmouseout='clearTimeout(timeoutID1);timeoutID1=setTimeout(\"popDownMonth()\",100);event.cancelBubble=true'>" +	sHTML +	"</table>"

			monthConstructed=true
		}
	}

	function popUpMonth() {
		constructMonth()
		crossMonthObj.visibility = (dom||ie)? "visible"	: "show"
		if(typeof InstallTrigger !== 'undefined')
		{ 
		    crossMonthObj.left = parseInt(crossobj.left.replace("px","")) + 60 + "px";
            crossMonthObj.top = parseInt(crossobj.top.replace("px","")) + 26 + "px";
		}
		else
		{
            crossMonthObj.left = parseInt(crossobj.left) + 60 + "px";
            crossMonthObj.top = parseInt(crossobj.top) + 26 + "px";
		}

		hideElement( 'SELECT', document.getElementById("selectMonth") );
		hideElement( 'APPLET', document.getElementById("selectMonth") );			
	}

	function popDownMonth()	{
		crossMonthObj.visibility= "hidden"
	}

	/*** Year Pulldown ***/

	function incYear() {
		for	(i=0; i<7; i++){
			newYear	= (i+nStartingYear)+1
			if (newYear==yearSelected)
			{ txtYear =	"&nbsp;<B>"	+ newYear +	"</B>&nbsp;" }
			else
			{ txtYear =	"&nbsp;" + newYear + "&nbsp;" }
			document.getElementById("y"+i).innerHTML = txtYear
		}
		nStartingYear ++;
		bShow=true
	}

	function decYear() {
		for	(i=0; i<7; i++){
			newYear	= (i+nStartingYear)-1
			if (newYear==yearSelected)
			{ txtYear =	"&nbsp;<B>"	+ newYear +	"</B>&nbsp;" }
			else
			{ txtYear =	"&nbsp;" + newYear + "&nbsp;" }
			document.getElementById("y"+i).innerHTML = txtYear
		}
		nStartingYear --;
		bShow=true
	}

	function selectYear(nYear) {
		yearSelected=parseInt(nYear+nStartingYear);
		yearConstructed=false;
		constructCalendar();
		popDownYear();
	}

	function constructYear() {
		popDownMonth()
		sHTML =	""
		if (!yearConstructed) {

			sHTML =	"<tr><td align='center'	onmouseover='this.style.backgroundColor=\"#FFCC99\"' onmouseout='clearInterval(intervalID1);this.style.backgroundColor=\"\"' style='cursor:pointer'	onmousedown='clearInterval(intervalID1);intervalID1=setInterval(\"decYear()\",30)' onmouseup='clearInterval(intervalID1)'>-</td></tr>"

			j =	0
			nStartingYear =	yearSelected-3
			for	(i=(yearSelected-3); i<=(yearSelected+3); i++) {
				sName =	i;
				if (i==yearSelected){
					sName =	"<B>" +	sName +	"</B>"
				}

				sHTML += "<tr><td id='y" + j + "' onmouseover='this.style.backgroundColor=\"#FFCC99\"' onmouseout='this.style.backgroundColor=\"\"' style='cursor:pointer' onclick='selectYear("+j+");event.cancelBubble=true'>&nbsp;" + sName + "&nbsp;</td></tr>"
				j ++;
			}

			sHTML += "<tr><td align='center' onmouseover='this.style.backgroundColor=\"#FFCC99\"' onmouseout='clearInterval(intervalID2);this.style.backgroundColor=\"\"' style='cursor:pointer' onmousedown='clearInterval(intervalID2);intervalID2=setInterval(\"incYear()\",30)'	onmouseup='clearInterval(intervalID2)'>+</td></tr>"

			document.getElementById("selectYear").innerHTML	= "<table width=52 style='font-family:arial; font-size:11px; border-width:1; border-style:solid; border-color:#a0a0a0;'	bgcolor='#FFFFDD' onmouseover='clearTimeout(timeoutID2)' onmouseout='clearTimeout(timeoutID2);timeoutID2=setTimeout(\"popDownYear()\",100)' cellspacing=0>"	+ sHTML	+ "</table>"

			yearConstructed	= true
		}
	}

	function popDownYear() {
		clearInterval(intervalID1)
		clearTimeout(timeoutID1)
		clearInterval(intervalID2)
		clearTimeout(timeoutID2)
		crossYearObj.visibility= "hidden"
	}

	function popUpYear() {
		var	leftOffset

		constructYear()
		crossYearObj.visibility	= (dom||ie)? "visible" : "show"
		leftOffset = parseInt(crossobj.left) + document.getElementById("spanYear").offsetLeft + 8
        if(typeof InstallTrigger !== 'undefined')
		{
            crossYearObj.left = parseInt(crossobj.left.replace("px","")) + document.getElementById("spanYear").offsetLeft + 8 + "px";
		    crossYearObj.top = parseInt(crossobj.top.replace("px","")) + 26 + "px";
		}
		else
		{
            crossYearObj.left = leftOffset + "px";
            crossYearObj.top = parseInt(crossobj.top) + 26 + "px";

		}
	}

	/*** calendar ***/
   function WeekNbr(n) {
      // Algorithm used:
      // From Klaus Tondering's Calendar document (The Authority/Guru)
      // hhtp://www.tondering.dk/claus/calendar.html
      // a = (14-month) / 12
      // y = year + 4800 - a
      // m = month + 12a - 3
      // J = day + (153m + 2) / 5 + 365y + y / 4 - y / 100 + y / 400 - 32045
      // d4 = (J + 31741 - (J mod 7)) mod 146097 mod 36524 mod 1461
      // L = d4 / 1460
      // d1 = ((d4 - L) mod 365) + L
      // WeekNumber = d1 / 7 + 1
 
      year = n.getFullYear();
      month = n.getMonth() + 1;
	  /*
      if (startAt == 0) {
         day = n.getDate() + 1;
      }
      else {
         day = n.getDate();
      }*/
	  day = n.getDate() + 1-startAt;
 
      a = Math.floor((14-month) / 12);
      y = year + 4800 - a;
      m = month + 12 * a - 3;
      b = Math.floor(y/4) - Math.floor(y/100) + Math.floor(y/400);
      J = day + Math.floor((153 * m + 2) / 5) + 365 * y + b - 32045;
      d4 = (((J + 31741 - (J % 7)) % 146097) % 36524) % 1461;
      L = Math.floor(d4 / 1460);
      d1 = ((d4 - L) % 365) + L;
      week = Math.floor(d1/7) + 1;
 
      return week;
   }

	function constructCalendar () {
		var aNumDays = Array (31,0,31,30,31,30,31,31,30,31,30,31)

		var dateMessage
		var	startDate =	new	Date (yearSelected,monthSelected,1)
		var endDate

		if (monthSelected==1)
		{
			endDate	= new Date (yearSelected,monthSelected+1,1);
			endDate	= new Date (endDate	- (24*60*60*1000));
			numDaysInMonth = endDate.getDate()
		}
		else
		{
			numDaysInMonth = aNumDays[monthSelected];
		}

		datePointer	= 0
		//dayPointer = startDate.getDay()
		dayPointer = firstdayofweek(startDate.getDay())
		/*
		switch (startAt)
			{
			case (0): dayPointer = dayPointer
			break;
			case (1): dayPointer--
			break;
			case (6): dayPointer++
			break;
			}	
			*/
		//dayPointer = startDate.getDay()// - startAt
		
		if (dayPointer<0)
		{
			//dayPointer = 6
		}

		sHTML =	"<table	 border=0 style='font-family:verdana;font-size:10px;'><tr>"

		if (showWeekNumber==1)
		{
			sHTML += "<td width=27><b>" + weekStrings[languages] + "</b></td><td width=1 rowspan=7 bgcolor='#d0d0d0' style='padding:0px'></td>"
		}

		for	(i=0; i<7; i++)	{
			sHTML += "<td width='27' align='right'><B>"+ dayNames[languages][i]+"</B></td>"
		}
		sHTML +="</tr><tr>"
		
		if (showWeekNumber==1)
		{
			sHTML += "<td align=right>" + WeekNbr(startDate) + "&nbsp;</td>"
		}

		for	( var i=1; i<=dayPointer;i++ )
		{
			sHTML += "<td>&nbsp;</td>"
		}
	
		for	( datePointer=1; datePointer<=numDaysInMonth; datePointer++ )
		{
			dayPointer++;
			sHTML += "<td align=right>"
			sStyle=styleAnchor
			if ((datePointer==odateSelected) &&	(monthSelected==omonthSelected)	&& (yearSelected==oyearSelected))
			{ sStyle+=styleLightBorder }

			sHint = ""
			for (k=0;k<HolidaysCounter;k++)
			{
				if ((parseInt(Holidays[k].d)==datePointer)&&(parseInt(Holidays[k].m)==(monthSelected+1)))
				{
					if ((parseInt(Holidays[k].y)==0)||((parseInt(Holidays[k].y)==yearSelected)&&(parseInt(Holidays[k].y)!=0)))
					{
						sStyle+="background-color:#FFDDDD;"
						sHint+=sHint==""?Holidays[k].desc:"\n"+Holidays[k].desc
					}
				}
			}

			var regexp= /\"/g
			sHint=sHint.replace(regexp,"&quot;")

			dateMessage = "onmousemover='window.status=\""+selectDateMessages[languages].replace("[date]",constructDate(datePointer,monthSelected,yearSelected))+"\"' onmouseout='window.status=\"\"' "

			if ((datePointer==dateNow)&&(monthSelected==monthNow)&&(yearSelected==yearNow))
			{ sHTML += "<b><a "+dateMessage+" title=\"" + sHint + "\" style='"+sStyle+"' href='javascript:dateSelected="+datePointer+";closeCalendar();'><font color=#ff0000>&nbsp;" + datePointer + "</font>&nbsp;</a></b>"}
			else if	(dayPointer % 7 == (startAt * -1)+1)
			{ sHTML += "<a "+dateMessage+" title=\"" + sHint + "\" style='"+sStyle+"' href='javascript:dateSelected="+datePointer + ";closeCalendar();'>&nbsp;<font color=#909090>" + datePointer + "</font>&nbsp;</a>" }
			else
			{ sHTML += "<a "+dateMessage+" title=\"" + sHint + "\" style='"+sStyle+"' href='javascript:dateSelected="+datePointer + ";closeCalendar();'>&nbsp;" + datePointer + "&nbsp;</a>" }

			sHTML += ""
			if ((dayPointer+startAt) % 7 == startAt) { 
				sHTML += "</tr><tr>" 
				if ((showWeekNumber==1)&&(datePointer<numDaysInMonth))
				{
					sHTML += "<td align=right>" + (WeekNbr(new Date(yearSelected,monthSelected,datePointer+1))) + "&nbsp;</td>"
				}
			}
		}

		document.getElementById("content").innerHTML   = sHTML
		document.getElementById("spanMonth").innerHTML = "&nbsp;" +	monthNames[languages][monthSelected] + "&nbsp;<IMG id='changeMonth' SRC='<%= WebResource("APTemplate.Resources.drop1.gif") %>' WIDTH='12' HEIGHT='10' BORDER=0>"
		document.getElementById("spanYear").innerHTML =	"&nbsp;" + yearSelected	+ "&nbsp;<IMG id='changeYear' SRC='<%= WebResource("APTemplate.Resources.drop1.gif") %>' WIDTH='12' HEIGHT='10' BORDER=0>"
	}

	function popUpCalendar(ctl,	ctl2, format, top, left) 
	{
		var	leftpos = left;
		var	toppos = top;
		
		if (isNaN(left))
		{
			leftpos = -235; //-208
		}	
		if (isNaN(top))
		{
			toppos = 0;
		}
		if (bPageLoaded)
		{
			if ( crossobj.visibility ==	"hidden" ) 
			{
				ctlToPlaceValue	= ctl2;
				dateFormat=format;

				formatChar = " ";
				aFormat	= dateFormat.split(formatChar);
				if (aFormat.length<3)
				{
					formatChar = "/";
					aFormat	= dateFormat.split(formatChar);
					if (aFormat.length<3)
					{
						formatChar = ".";
						aFormat	= dateFormat.split(formatChar);
						if (aFormat.length<3)
						{
							formatChar = "-";
							aFormat	= dateFormat.split(formatChar);
							if (aFormat.length<3)
							{
								// invalid date	format
								formatChar="/";
							}
						}
					}
				}
        
				tokensChanged =	0;
				if ( formatChar	!= "" )
				{
					// use user's date
					aData =	ctl2.value.split(formatChar);

					for	(i=0;i<3;i++)
					{
						if ((aFormat[i]=="d") || (aFormat[i]=="dd"))
						{
							dateSelected = parseInt(aData[i], 10);
							tokensChanged ++;
						}
						else if	(aFormat[i]=="yyyy/mm")
						{
							yearSelected = parseInt(aData[0], 10);
							tokensChanged ++;
							monthSelected =	parseInt(aData[1], 10) - 1;
							tokensChanged ++;
						}
						else if	((aFormat[i]=="m") || (aFormat[i]=="mm"))
						{
							monthSelected =	parseInt(aData[i], 10) - 1;
							tokensChanged ++;
						}
						else if	(aFormat[i]=="yyyy")
						{
							yearSelected = parseInt(aData[i], 10);
							tokensChanged ++;
						}
						else if	(aFormat[i]=="mmm")
						{
							for	(j=0; j<12;	j++)
							{
								if (aData[i]==monthNames[languages][j])
								{
									monthSelected=j;
									tokensChanged ++;
								}
							}
						}
					}
				}

				if ((tokensChanged!=3 && tokensChanged!=2)||isNaN(monthSelected)||isNaN(yearSelected))
				{
					dateSelected = dateNow;
					monthSelected =	monthNow;
					yearSelected = yearNow;
				}

				odateSelected=dateSelected;
				omonthSelected=monthSelected;
				oyearSelected=yearSelected;

				aTag = ctl;
				do {
					aTag = aTag.offsetParent;
					leftpos	+= aTag.offsetLeft;
					toppos += aTag.offsetTop;
				} while(aTag.tagName!="BODY");

                crossobj.left = fixedX==-1 ? ctl.offsetLeft	+ leftpos + "px" :	fixedX + "px";
				crossobj.top = fixedY==-1 ?	ctl.offsetTop +	toppos + ctl.offsetHeight +	2 + "px" :	fixedY + "px";

//                if(typeof InstallTrigger !== 'undefined')
//		        {
//				    crossobj.left = fixedX==-1 ? ctl.offsetLeft	+ leftpos + "px" :	fixedX + "px";
//				    crossobj.top = fixedY==-1 ?	ctl.offsetTop +	toppos + ctl.offsetHeight +	2 + "px" :	fixedY + "px";
//		        }
//		        else
//		        {
//		            crossobj.left = fixedX==-1 ? ctl.offsetLeft	+ leftpos + "px" :	fixedX + "px";
//				    crossobj.top = fixedY==-1 ?	ctl.offsetTop +	toppos + ctl.offsetHeight +	2 + "px" :	fixedY + "px";
//		        }
				constructCalendar (1, monthSelected, yearSelected);
				crossobj.visibility=(dom||ie)? "visible" : "show";

				hideElement( 'SELECT', document.getElementById("calendar") );
				hideElement( 'APPLET', document.getElementById("calendar") );			

				bShow = true;
			}
			else
			{
				hideCalendar();
				if (ctlNow!=ctl)
				{
					popUpCalendar(ctl, ctl2, format);
				}
			}
			ctlNow = ctl;
		}
	}

	document.onkeypress = function hidecal1 () 
	{ 
		/*if (event.keyCode==27) 
		{
			hideCalendar();
		}*/
	}
	document.onclick = function hidecal2 () 
	{
		if (!bShow)
		{
			hideCalendar()
		}
		bShow = false
	}

	if(ie)
	{
		init();
	}
	else
	{
		window.onload=init();
	}