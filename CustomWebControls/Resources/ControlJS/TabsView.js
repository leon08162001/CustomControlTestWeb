/*
  btn = 每個Tab標籤為一個LinkButton
  index = 每個Tab標籤的索引位置
  tabControlId = TabsView控制項的ClientID
  hfId = 隱藏欄位ClientID
  unSelectedCSS = 非選中的Tab標籤 css class名稱
  seletedCSS = 選中的Tab標籤 css class名稱
*/
function OnTabClick(btn, index, tabControlId, hfId, isUseBgImage, unSelectedLeftBackImage, unSelectedCenterBackImage, unSelectedRightBackImage, selectedLeftBackImage, selectedCenterBackImage, selectedRightBackImage, unSelectedBackColor, seletedBackColor) 
{
  var exprArray = "tabButtons_" + tabControlId;
  var length = eval(exprArray + ".length");
  for ( var i=0; i<length;i++)
  {
    var exprElement = "tabButtons_" + tabControlId + "[" + i + "]";
    var btnId = eval(exprElement);
    var contentId = eval("tabContents_" + tabControlId + "[" + i + "]");
    switch (isUseBgImage) 
    {
      case true:
        if (btnId == btn.id)//當迴圈中目前的Tab標籤為所選的Tab標籤時
        {
          if (document.getElementById(btnId + 'l') != null) {
            document.getElementById(btnId + 'l').style.backgroundImage = "url(" + selectedLeftBackImage + ")";
            document.getElementById(btnId + 'l').style.backgroundRepeat = "no-repeat";
          }
          document.getElementById(btnId).style.backgroundImage = "url(" + selectedCenterBackImage + ")";
          document.getElementById(btnId).style.backgroundRepeat = "repeat-x";
          if (document.getElementById(btnId + 'r') != null) {
            document.getElementById(btnId + 'r').style.backgroundImage = "url(" + selectedRightBackImage + ")";
            document.getElementById(btnId + 'r').style.backgroundRepeat = "no-repeat";
          }
          document.getElementById(contentId).style.display = "block";
          document.getElementById(hfId).value = i; //將隱藏欄位值設為目前所選Tab標籤的索引.
        }
        else 
        {
          if (document.getElementById(btnId + 'l') != null) {
            document.getElementById(btnId + 'l').style.backgroundImage = "url(" + unSelectedLeftBackImage + ")";
            document.getElementById(btnId + 'l').style.backgroundRepeat = "no-repeat";
          }
          document.getElementById(btnId).style.backgroundImage = "url(" + unSelectedCenterBackImage + ")";
          document.getElementById(btnId).style.backgroundRepeat = "repeat-x";
          if (document.getElementById(btnId + 'r') != null) {
            document.getElementById(btnId + 'r').style.backgroundImage = "url(" + unSelectedRightBackImage + ")";
            document.getElementById(btnId + 'r').style.backgroundRepeat = "no-repeat";
          }
          document.getElementById(contentId).style.display = "none";
        }
        break;
      case false:
        if (btnId == btn.id)//當迴圈中目前的Tab標籤為所選的Tab標籤時
        {

          if (document.getElementById(btnId + 'l') != null)
          { document.getElementById(btnId + 'l').style.backgroundColor = "#" + seletedBackColor; }
          document.getElementById(btnId).style.backgroundColor = "#" + seletedBackColor;
          if (document.getElementById(btnId + 'r') != null)
          { document.getElementById(btnId + 'r').style.backgroundColor = "#" + seletedBackColor; }
          document.getElementById(contentId).style.display = "block";
          document.getElementById(hfId).value = i; //將隱藏欄位值設為目前所選Tab標籤的索引.  
        }
        else 
        {
          if (document.getElementById(btnId + 'l') != null)
          { document.getElementById(btnId + 'l').style.backgroundColor = "#" + unSelectedBackColor; }
          document.getElementById(btnId).style.backgroundColor = "#" + unSelectedBackColor;
          if (document.getElementById(btnId + 'r') != null)
          { document.getElementById(btnId + 'r').style.backgroundColor = "#" + unSelectedBackColor; }
          document.getElementById(contentId).style.display = "none";
        }
        break;
    }  
  }
 return false;
}

function SelectTab(index, tabControlId, hfId, isUseBgImage, unSelectedLeftBackImage, unSelectedCenterBackImage, unSelectedRightBackImage, selectedLeftBackImage, selectedCenterBackImage, selectedRightBackImage, unSelectedBackColor, seletedBackColor)
{
   var exprElement = "tabButtons_" + tabControlId + "[" + index + "]";
   var btnId = eval(exprElement)
   OnTabClick(document.getElementById(btnId), index, tabControlId, hfId, isUseBgImage, unSelectedLeftBackImage, unSelectedCenterBackImage, unSelectedRightBackImage, selectedLeftBackImage, selectedCenterBackImage, selectedRightBackImage, unSelectedBackColor, seletedBackColor);
}