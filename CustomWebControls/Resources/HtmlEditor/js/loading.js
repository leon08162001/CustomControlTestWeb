var is_chrome = navigator.userAgent.toLowerCase().indexOf('chrome') > -1;
if(is_chrome){
    hidePercent();
    showRichTextBox();
}
else{
    var sourcesDB=new Array();
    var loadedCount=0;
    var percent=0;
    addJs("slider.js");
    addJs("RichText.js");
    addJs("currentPage");
    addJs("loading.js");
    addJs("Encoder.js");
}
function addJs(fileName){
    sourcesDB.push(fileName);
}
var Total=sourcesDB.length;
function loaded() {
    ///------Optional : If You want to have loading in object initializing process, remove comments -----///

    //loadedCount++;
    //percent=Math.round((loadedCount/Total)*100);
    //if(percent<100){
    //    //setPercentageValue();
    //    try{
    //        //document.getElementById("progressBar").style.width=(percent*parseInt(document.getElementById("loaderParent").style.width))/100+"px";
    //    }
    //    catch(e){
        
    //    }
    //}
    //else{
        //hidePercent();
        showRichTextBox();
    //}
}

function setPercentageValue(){
    (document.all)?document.getElementById("infoProgress").innerText=percent+"%":document.getElementById("infoProgress").textContent=percent+"%";
}

function hidePercent(){
    //document.getElementById("loadingZone").style.display="none";
}

function showRichTextBox(){
    document.getElementById("centerElement").style.display="block";
    if(document.cookie.length!=0){
        //createSWFMaker();
    }
}

//setTimeout("loaded()", 500);