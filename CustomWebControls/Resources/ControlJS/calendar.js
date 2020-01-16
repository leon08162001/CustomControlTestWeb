var sMon = new Array(12);
	sMon[0] = "Jan"
	sMon[1] = "Feb"
	sMon[2] = "Mar"
	sMon[3] = "Apr"
	sMon[4] = "May"
	sMon[5] = "Jun"
	sMon[6] = "Jul"
	sMon[7] = "Aug"
	sMon[8] = "Sep"
	sMon[9] = "Oct"
	sMon[10] = "Nov"
	sMon[11] = "Dec"

	function calendar(t, format) {
  var top = window.event.screenY-window.event.clientY+window.event.y;
  var left = window.event.screenX - window.event.clientX + window.event.x;
	var sPath = '<%= WebResource("APTemplate.Resources.calendar1.htm") %>';
	strFeatures = "dialogWidth=200px;dialogHeight=200px;center=yes;help=no;dialogTop=" + top + "px;dialogLeft=" + left + "px;";
	st = format == "yyyy/mm/dd" ? t.value : format == "yyyy/mm" ? t.value + "/01" : t.value;
	sDate = showModalDialog(sPath,st,strFeatures);

	if (format == "yyyy/mm/dd") {
	  sDate = formatYearMonthDate(sDate);
	}
	else if (format == "yyyy/mm") {
	  sDate = formatYearMonth(sDate);
	}
	else if (format == "yyyy") {
	  sDate = formatYear(sDate);
	}
	else if (format == "yyyy/mm/dd HH:mm:ss") {
	sDate = formatYearMonthDateHourMinuteSecond(sDate);
  }
  else if (format == "yyyy/mm/dd HH:mm") {
  sDate = formatYearMonthDateHourMinute(sDate);
  }
  else if (format == "yyyy/mm/dd HH") {
  sDate = formatYearMonthDateHour(sDate);
  }
	if (sDate!=null && sDate!="")
	{
	  t.value = sDate;
	}	
}

function checkDate(t) {
	dDate = new Date(t.value);
	if (dDate == "NaN") {t.value = ""; return;}

	iYear = dDate.getFullYear()

	if ((iYear > 1899)&&(iYear < 1950)) {

		sYear = "" + iYear + ""
		if (t.value.indexOf(sYear,1) == -1) {
			iYear += 100;
			sDate = (dDate.getMonth() + 1) + "/" + dDate.getDate() + "/" + iYear;
			dDate = new Date(sDate);
		}
	}
	t.value = formatDate(dDate);
}

function formatYearMonthDate(sDate) {
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

function formatYearMonth(sDate) {
  var sScrap = "";
  var dScrap = new Date(sDate);
  if (dScrap == "NaN") return sScrap;

  iMon = dScrap.getMonth();
  iYea = dScrap.getFullYear();
  if ((iMon + 1) <= 9)
  { sMon = "0" + (iMon + 1); }
  else
  { sMon = iMon + 1; }

  sYea = iYea;
  sScrap = sYea + "/" + sMon;
  return sScrap;
}

function formatYear(sDate) {
  var sScrap = "";
  var dScrap = new Date(sDate);
  if (dScrap == "NaN") return sScrap;

  iYea = dScrap.getFullYear();

  sYea = iYea;
  sScrap = sYea;
  return sScrap;
}

function formatYearMonthDateHourMinuteSecond(sDate) {
  var sScrap = "";
  var dScrap = new Date(sDate);
  if (dScrap == "NaN") return sScrap; 
  iDay = dScrap.getDate();
  iMon = dScrap.getMonth();
  iYea = dScrap.getFullYear();
  if ((iMon + 1) <= 9)
  { sMon = "0" + (iMon + 1); }
  else
  { sMon = iMon + 1; }
  if (iDay <= 9)
  { sDay = "0" + iDay; }
  else
  { sDay = iDay; }

  var dDate = new Date();
  iSecond = dDate.getSeconds();
  iMinute = dDate.getMinutes();
  iHour = dDate.getHours();
  var sHour, sMinute, sSecond;

  sHour = iHour <= 9 ? "0" + iHour : iHour;
  sMinute = iMinute <= 9 ? "0" + iMinute : iMinute;
  sSecond = iSecond <= 9 ? "0" + iSecond : iSecond;

  sYea = iYea;
  sScrap = sYea + "/" + sMon + "/" + sDay + " " + sHour + ":" + sMinute + ":" + sSecond;
  return sScrap;
}

function formatYearMonthDateHourMinute(sDate) {
  var sScrap = "";
  var dScrap = new Date(sDate);
  if (dScrap == "NaN") return sScrap;
  iDay = dScrap.getDate();
  iMon = dScrap.getMonth();
  iYea = dScrap.getFullYear();
  if ((iMon + 1) <= 9)
  { sMon = "0" + (iMon + 1); }
  else
  { sMon = iMon + 1; }
  if (iDay <= 9)
  { sDay = "0" + iDay; }
  else
  { sDay = iDay; }

  var dDate = new Date();
  iMinute = dDate.getMinutes();
  iHour = dDate.getHours();
  var sHour, sMinute;

  sHour = iHour <= 9 ? "0" + iHour : iHour;
  sMinute = iMinute <= 9 ? "0" + iMinute : iMinute;

  sYea = iYea;
  sScrap = sYea + "/" + sMon + "/" + sDay + " " + sHour + ":" + sMinute;
  return sScrap;
}

function formatYearMonthDateHour(sDate) {
  var sScrap = "";
  var dScrap = new Date(sDate);
  if (dScrap == "NaN") return sScrap;
  iDay = dScrap.getDate();
  iMon = dScrap.getMonth();
  iYea = dScrap.getFullYear();
  if ((iMon + 1) <= 9)
  { sMon = "0" + (iMon + 1); }
  else
  { sMon = iMon + 1; }
  if (iDay <= 9)
  { sDay = "0" + iDay; }
  else
  { sDay = iDay; }

  var dDate = new Date();
  iHour = dDate.getHours();
  var sHour;

  sHour = iHour <= 9 ? "0" + iHour : iHour;
  
  sYea = iYea;
  sScrap = sYea + "/" + sMon + "/" + sDay + " " + sHour;
  return sScrap;
}

function isDateString(sDate)
{ 
	var iaMonthDays = [31,28,31,30,31,30,31,31,30,31,30,31]
	var iaDate = new Array(3)
	var year, month, day

	if (arguments.length != 1) return false
	iaDate = sDate.toString().split("/")
	if (iaDate.length != 3) return false
	if (iaDate[1].length > 2 || iaDate[2].length > 2 || iaDate[1].length < 1 || iaDate[2].length < 1 || iaDate[0].length != 4) return false

	year = parseFloat(iaDate[0])
	month = parseFloat(iaDate[1])
	day=parseFloat(iaDate[2])

	if (year < 1900 || year > 2100) return false
	if (((year % 4 == 0) && (year % 100 != 0)) || (year % 400 == 0)) iaMonthDays[1]=29;
	if (month < 1 || month > 12) return false
	if (day < 1 || day > iaMonthDays[month - 1]) return false
	return true
} 

function chkDate(sDate)
{
	if(isDateString(sDate))
		return sDate
	else
		{
			alert("Date format error!!");
			tDate = new Date();
			return tDate.getFullYear()+'/'+(tDate.getMonth()+1)+'/'+tDate.getDate();
		}
}