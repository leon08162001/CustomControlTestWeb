
//-----------------Variables---------------
var iframeWin = window.frames["textEditor"];
var $_Table;
var $_Form;
var $_TR;
var $_TableId;
var $_TD;
var $_TBody;
var $_MainGeneratedTableForTableCreation;
var $_ContainerDiv;
var $_TransparentDiv;
var InsertedTableTotal=1;
var CarretPosition;
var Current;
var PreviousCol;
var PreviousRow;
var ctrlDown = false;
var isRepeated=false;
var isColsSelectionStop=false;
var isTableCreationFormGenerated=false;
var isButtonActive=false;
var IsObjectCreated=false;
var isOndesignMode=true;
var isOnSourceMode=false;
var isColorPickerInAction = false;
var isAdvanceColorPickerInAction = false;
var isToolBoxContainerDivDisabled = false;
var isResizeabilityActive = false;
var isBoldOnActive = false;
var isItalicOnActive = false;
var isUnderlineOnActive = false;
var unActivegeneratedControlForMove = false;
var node = null;
var radioGroupValue = 0;
var checkBoxPasteTotal=0;
var textAreaPasteTotal=0;
var textBoxPasteTotal=0;
var selectPasteTotal=0;
var passwordPasteTotal=0;
var filePasteTotal=0;
var fileValues;
var createdLinkOptionId;
var selectedText;
var linkTextBox;
var createTableOnDesignButton;
var SetCommandButton;
var SetSWFCommandButton;
var SetSWFUploaderCommandButton;
var SetCommandImageButton;
var SetCommandInsertTableButton;
var cellsIndexTxt;
var rowsIndexTxt;
var SetCommandInsertButton_Button;
var SetCommandCancelInsertingButton_Button;
var insertedButtonValue;
var insertedButtonName;
var nodesList;
var clientName="";
var ActionFor="";
var createdTableString = "";
var colorhex = "#FFFFF0";
var frameContentWindow=window.document.getElementById( "textEditor" ).contentWindow.document;
var mouseDownLocation;
var previousFrameHight;
var textEditorElement=document.getElementById("textEditor");
//-------------------------------

//--------------(Functions)--------


this.window.onload=function(){
    //setTimeout("loaded()", 500);
    
    }
       
     function waitPreloadPage() 
         { //DOM
            if (document.getElementById){
                document.getElementById('prepage').style.visibility='hidden';
            }
            else{
                if (document.layers){
                  document.prepage.visibility = 'hidden';
            }
            else {
                document.all.prepage.style.visibility = 'hidden';
                }
            }
        }
        

function setAdvanceColorPickerValue(hex)
{
    try{
        textEdit('ForeColor',hex);
        var show = function (e) { e = e || window.event; showAdvanceColorPicker(e);}
        show();
    }
    catch(error){
        alert(error.name + ": " + error.message + error.lineNumber);
    }
}

function mouseOutMap() {
    //To Do
}

function def()
{
    try{
        document.getElementById("fonts").selectedIndex=0;
        document.getElementById("size").selectedIndex=1;
    }
    catch(error){
        alert(error.name + ": " + error.message + error.lineNumber);
    }
   // document.getElementById("color").selectedIndex=0;
}

//Set design mode for iframe -- IE
if(document.all){
    try{
        textEditor.document.designMode="on";
        textEditor.document.open();
        textEditor.document.write('<head></head>');
        textEditor.document.close();
        textEditor.focus();
        var usedFrameDocument=window.frames["textEditor"].document;
        var usedFrame=usedFrameDocument.body;
    }
    catch(error){
        alert(error.name + ": " + error.message + error.lineNumber);
    }
}
//Set design mode for iframe -- Other browsers such as FireFox
else{
     try{
         textEditorElement.contentDocument.designMode = "on" ;
         textEditorElement.contentDocument.open();
         textEditorElement.contentDocument.write('<head></head>');
         textEditorElement.contentDocument.close();
         textEditorElement.focus();
         textEditorElement.contentDocument.addEventListener('click', getActiveButtons, true);
         textEditorElement.contentDocument.addEventListener('keydown', getActiveButtons, true);
         var usedFrame=frameContentWindow.body;
     }
     catch(error){
        alert(error.name + ": " + error.message + error.lineNumber);
     }
}
var isRTL=false;
//get last carret position and use it at Another time
function AutoSelect(){
    try{
        var Position= doGetCaretPosition(usedFrame);
        setCursor(usedFrame,Position,Position+1);
    }
    catch(error){
        alert(error.name + ": " + error.message + error.lineNumber);
    }
}

usedFrame.onclick=function(){
    try {
        checkCursor(usedFrame);
        getActiveButtons();
        setDispalyTo("ColorPickerDiv","hide");
        setDispalyTo("advanceColorPickerDiv","hide");
        isColorPickerInAction=false;
        isAdvanceColorPickerInAction=false;
    }
    catch(error){
        alert(error.name + ": " + error.message + error.lineNumber);
    }
}

usedFrame.onkeyup=function(){
    getActiveButtons();
}
//the save as function
function saveAs()
{
    try{
        (document.all)?textEditor.document.execCommand("SaveAs", false, "Sigma text editor generated document.html"):alert('This item is available in Internet Explorer only.');
    }
    catch(error){
        alert(error.name + ": " + error.message + error.lineNumber);
    }
}

//set the cursor on specified position
function setCursor(el,st,end) {
    try{
        if(el.setSelectionRange)
        { 
            el.focus(); 
            el.setSelectionRange(st,end); 
        } 
        else 
        { 
            if(el.createTextRange) { 
                range=el.createTextRange();
                range.collapse(true); 
                range.moveEnd('character',end); 
                range.moveStart('character',st); 
                range.select();
            } 
        } 
    }
    catch(error){
        alert(error.name + ": " + error.message + error.lineNumber);
    }
}

//get carret position        
function doGetCaretPosition (ctrl) {
    try{
        var CaretPos = 0;
        // IE Support
        if (document.selection) {
            ctrl.focus ();
            var Sel = document.selection.createRange ();
            Sel.moveStart ('character', -ctrl.innerText.length);
            CaretPos = Sel.text.length;
        }
        // Firefox support
        else if (ctrl.selectionStart || ctrl.selectionStart == '0')
            CaretPos = ctrl.selectionStart;
     
        return (CaretPos);
    }
    catch(error){
        alert(error.name + ": " + error.message + error.lineNumber);
    }
}       

//=======

//Encodes all html code for preventing of transfering dangerous html codes which is potential for XSS
function HTMLEncode(value){
    try{
        var i=value,
        db=true;
        return Encoder.htmlEncode(i,db);
    }
    catch(error){
        alert(error.name + ": " + error.message + error.lineNumber);
    }
}



//=======

//Sends all elements of textEditor to the server
function sendValue(){
    var innerHtmlData= usedFrame.innerHTML;
    var clearData=innerHtmlData.toLowerCase().replace(/<script[^>]*?>/g,"");
    var clearData = clearData.replace(/<\/script>/g, "");
    clearData = clearData.replace(/javascript/g, "");
    clearData = clearData.replace(/script/g, "");
    clearData = clearData.replace(/<iframe[^>]*?>/g, "");
    clearData = clearData.replace(/<\/iframe>/g, "");
    clearData = Encoder.htmlEncode(clearData);
    __doPostBack('getHtmlData', clearData);
}
var HTMLSource;
//switch to source mode       
function switchToSourceMode(){
    try{
        if(!isOnSourceMode){
            disableElement(document.getElementById("textToolsContainer"));
            document.getElementById("sourceTxt").style.display="block";
            textEditorElement.style.display="none";
            createCompleteHtmlSource();
            document.getElementById("sourceTxt").value=HTMLSource;
            isOnSourceMode=true;
            isOndesignMode=false;
        }
    }
    catch(error){
        alert(error.name + ": " + error.message + error.lineNumber);
    }
}

function createCompleteHtmlSource(){
    try{
        if(isRTL){
            HTMLSource="<HTML dir=rtl><HEAD></HEAD><BODY>"+usedFrame.innerHTML+"</BODY></HTML>";
        }
        else{
            HTMLSource="<HTML dir=ltr><HEAD></HEAD><BODY>"+usedFrame.innerHTML+"</BODY></HTML>";
        }
    }
    catch(error){
        alert(error.name + ": " + error.message + error.lineNumber);
    }
    //alert(HTMLSource)
}

function preview(){
  createCompleteHtmlSource();
  win = window.open(", ", 'popup', 'toolbar = no, status = no,scrollbars=yes,resizable=yes');
  win.document.write("" + HTMLSource + "");
}

//Disables specified element
function disableElement(element){
    try{
       (element!=document.getElementById("GetSWFURL_Text"))?(isToolBoxContainerDivDisabled=element.disabled = element.disabled ? false : true):(element.disabled = element.disabled ? false : true);
       
   }
   catch(error){
        alert(error.name + ": " + error.message + error.lineNumber);
    }
}

//switch to design mode
function switchToDesignMode(){
    try{
        if(!isOndesignMode){
            disableElement(document.getElementById("textToolsContainer"));
            document.getElementById("sourceTxt").style.display="none"
            textEditorElement.style.display="block"
            usedFrame.innerHTML=document.getElementById("sourceTxt").value;
            isOnSourceMode=false;
            isOndesignMode=true;
        }
    }
    catch(error){
        alert(error.name + ": " + error.message + error.lineNumber);
    }
}

//create table element
var transparentDiv;
function CreateTable(TableID,ParentElement){
    try{
        $_Table=document.createElement("table");
        $_Table.id=TableID;
        $_Table.className="InsertedTable";
        $_Table.cellpadding="0px";
        $_Table.cellspacing="0px";
        (ParentElement=='')?document.getElementById("TransparentDiv").appendChild($_Table):document.getElementById(ParentElement).appendChild($_Table);
        makeDragable(TableID);
    }
    catch(error){
        alert(error.name + ": " + error.message + error.lineNumber);
    }
}

//Create form
function CreateForm(FormID){
    try{
        $_Form=document.createElement("form");
        $_Form.id=FormID;
        $_Form.name=FormID;
        $_Form.enctype="multipart/form-data";
        $_Form.method="post";
        $_Form.action="";
        document.body.appendChild($_Form);
    }
    catch(error){
        alert(error.name + ": " + error.message + error.lineNumber);
    }
}
//create tbody element
function createTbody(tBodyID){
    try{
        $_TBody=document.createElement("tbody");
        $_TBody.id=tBodyID;
        $_Table.appendChild($_TBody);
    }
    catch(error){
        alert(error.name + ": " + error.message + error.lineNumber);
    }
}

//create table row element
function CreateTableRow(TRID){
    try{
        $_TR=document.createElement("tr");
        $_TR.id=TRID;
        $_TR.className="InsertedTable-TR";
        $_TBody.appendChild($_TR);
    }
    catch(error){
        alert(error.name + ": " + error.message + error.lineNumber);
    }
}

//create td element
function CreateTableTD(TDID,innerText,cssClassName){
    try{
        $_TD=document.createElement("td");
        $_TD.id=TDID;
        $_TD.align="left";
        (document.all)?$_TD.innerText=innerText:$_TD.textContent=innerText;
        $_TD.className=cssClassName;
        $_TR.appendChild($_TD);
    }
    catch(error){
        alert(error.name + ": " + error.message + error.lineNumber);
    }
}

//get selected text on text editor
function GetSelectedText(){
    try{
        var myRange =  usedFrameDocument.selection.createRange();
        return myRange.text;
    }
    catch(error){
        alert(error.name + ": " + error.message + error.lineNumber);
    }
}
var transparentDiv;
//create a container div 
function craeteContainerElement(){
    try{
        $_ContainerDiv = document.createElement("div");
        $_ContainerDiv.id="ContainerDiv";
        transparentDiv=document.getElementById("TransparentDiv");
        $_ContainerDiv.setAttribute('class','createdDiv');
	    transparentDiv.appendChild($_ContainerDiv);
	    makeDragable($_ContainerDiv.id);
	}
	catch(error){
        alert(error.name + ": " + error.message + error.lineNumber);
    }
}

//create transparent element
function CreateTransparentElement(appenderElement){
    try{
        $_TransparentDiv=document.createElement("div");
        $_TransparentDiv.setAttribute("style","width:"+(document.documentElement.clientWidth)+"px; height:"+((document.body.scrollHeight==0)?document.documentElement.clientHeight:document.body.scrollHeight)+"px");
        $_TransparentDiv.setAttribute('class','TransparentdDiv');
        $_TransparentDiv.setAttribute('id','TransparentDiv');
        if(appenderElement.indexOf("form") != -1){
            document.getElementById(appenderElement).appendChild($_TransparentDiv);
        }
        else{
            document.body.appendChild($_TransparentDiv);
        }
    }
    catch(error){
        alert(error.name + ": " + error.message + error.lineNumber);
    }
}

//createLoder
function createLoder(){
    $_loaderDiv=document.createElement("div");
    $_loaderDiv.setAttribute('class','loading');
    $_loaderDiv.id="loaderDiv";
    $_TransparentDiv.appendChild($_loaderDiv);
}

//create the textbox and button Controls

function createControl(ID,ParentID,ControlType,Value,Title,CssClass){
    try{
        switch(ControlType){
            case "button":
                var $_Control = document.createElement("input");
	            $_Control.setAttribute('type', ControlType);
	            $_Control.id=ID;
	            $_Control.value=Value;
	            $_Control.title=Title;
                $_Control.setAttribute('class', CssClass);
	             document.getElementById(ParentID).appendChild($_Control);
	            break;
	        case "submit":
                var $_Control = document.createElement("input");
	            $_Control.setAttribute('type', ControlType);
	            $_Control.id=ID;
	            $_Control.value=Value;
	            $_Control.title=Title;
                $_Control.setAttribute('class', CssClass);
	             document.getElementById(ParentID).appendChild($_Control);
	            break;
	        case "file":
                var $_Control = document.createElement("input");
	            $_Control.setAttribute('type', ControlType);
	            $_Control.id=ID;
	            $_Control.name=ID;
	            $_Control.value=Value;
	            $_Control.title=Title;
                $_Control.setAttribute('class', CssClass);
	             document.getElementById(ParentID).appendChild($_Control);
	            break;
	        case "text":
	            var $_Control = document.createElement("input");
	            $_Control.setAttribute('type', ControlType);
	            $_Control.id=ID;
	            $_Control.id=ID;
                $_Control.setAttribute('class', CssClass);
	            document.getElementById(ParentID).appendChild($_Control);
	            break;
	        case "dropDownList":
	            var $_Control = document.createElement("select");
	            $_Control.id=ID;
	            createdLinkOptionId=ID;
	            $_Control.setAttribute("class","linkCreatorSelection");
	            var values=Value.split("|");
                for(var i=0;i<values.length;i++){
                    var $_subControl=document.createElement("option");
                    (document.all)?$_subControl.value=$_subControl.innerText=values[i]:$_subControl.value=$_subControl.textContent=values[i];
                    $_subControl.setAttribute("class",CssClass);
                    $_Control.appendChild($_subControl);
	            }
	            document.getElementById(ParentID).appendChild($_Control);
	            break;
	        case "checkBox":
                var $_Control = document.createElement("input");
	            $_Control.setAttribute('type', ControlType);
	            $_Control.id=ID;
	            $_Control.value=Value;
	            $_Control.title=Title;
                $_Control.setAttribute('class', CssClass);
	             document.getElementById(ParentID).appendChild($_Control);
	            break;
        }
    }
    catch(error){
        alert(error.name + ": " + error.message + error.lineNumber);
    }
}

//create innertext
function CtreateInnerText(InnerText,ParentElement,className){
    try{
        var textValue = document.createElement("p");
        textValue.setAttribute("align","center");
        (document.all)?textValue.innerText=InnerText:textValue.textContent=InnerText;
        textValue.setAttribute('class',className);
        document.getElementById(ParentElement).appendChild(textValue);
    }
    catch(error){
        alert(error.name + ": " + error.message + error.lineNumber);
    }
}

//get the selected text range
function GetSelectedTextRange(){
    try{
        if(!isToolBoxContainerDivDisabled){
            if(document.selection){ 
                return usedFrameDocument.selection.createRange();
            }
            else if (window.getSelection) { 
                var selection=window.document.getElementById( "textEditor" ).contentWindow.getSelection();
                if (selection.rangeCount > 0){
                    return selection.getRangeAt (0);
                }
                else{
                    alert("No text has been selected");
                }
            }
        }
    }
    catch(error){
        alert(error.name + ": " + error.message + error.lineNumber);
    }
}

function SetKeysToRTL(){
    document.getElementById("numberedList").setAttribute("class", "NumberedList_RTL");
    document.getElementById("bulletList").setAttribute("class", "BulletList_RTL");
    document.getElementById("outdentButton").setAttribute("class", "indent");
    document.getElementById("indentButton").setAttribute("class", "outdent");
    document.getElementById("rtlButton").setAttribute("class", "rtlButton_on");
    document.getElementById("ltrButton").setAttribute("class", "ltrButton");
}

function SetKeysToLTR(){
    document.getElementById("numberedList").setAttribute("class", "NumberedList");
    document.getElementById("bulletList").setAttribute("class", "BulletList");
    document.getElementById("outdentButton").setAttribute("class", "outdent");
    document.getElementById("indentButton").setAttribute("class", "indent");
    document.getElementById("ltrButton").setAttribute("class", "ltrButton_on");
    document.getElementById("rtlButton").setAttribute("class", "rtlButton");
}


//Executes All Commands
function textEdit(x,y){
    try{
        if(!isToolBoxContainerDivDisabled){
            if(document.all){
                createCompleteHtmlSource();
                checkCursor(usedFrame);
                switch(x){
                    case "dirRTL":
                        isRTL=true;
                        SetKeysToRTL();
                        break;
                    case "dirLTR":
                        isRTL=false;
                        SetKeysToLTR();
                        break;
                    case "paste":
                        checkCursor(usedFrame);
                        break;
                    case "bold":
                        (isBoldOnActive)?isBoldOnActive=false:isBoldOnActive=true;
                        (isBoldOnActive)?document.getElementById("boldButton").setAttribute("class", "BoldButtons_on"):document.getElementById("boldButton").setAttribute("class", "BoldButtons");
                        break;
                    case "italic":
                        (isItalicOnActive)?isItalicOnActive=false:isItalicOnActive=true;
                        (isItalicOnActive)?document.getElementById("italic").setAttribute("class", "ItalicButtons_on"):document.getElementById("italic").setAttribute("class", "ItalicButtons");
                        break;
                    case "underline":
                        (isUnderlineOnActive)?isUnderlineOnActive=false:isUnderlineOnActive=true;
                        (isUnderlineOnActive)?document.getElementById("underline").setAttribute("class", "UnderLineButtons_on"):document.getElementById("underline").setAttribute("class", "UnderLineButtons");
                        break;
                }
                textEditor.document.execCommand(x,"",y);
                textEditor.focus();
            }
            else{
                switch(x){
                    case "dirRTL":
                        frameContentWindow.body.dir="rtl";
                        isRTL=true;
                        SetKeysToRTL();
                        createCompleteHtmlSource();
                        break;
                    case "dirLTR":
                        frameContentWindow.body.dir="ltr";
                        isRTL=false;
                        SetKeysToLTR();
                        createCompleteHtmlSource();
                        break;
                    case "Print":
                        textEditorElement.contentWindow.focus();
                        textEditorElement.contentWindow.print();
                        break;
                    case "bold":
                        (isBoldOnActive)?isBoldOnActive=false:isBoldOnActive=true;
                        (isBoldOnActive)?document.getElementById("boldButton").setAttribute("class", "BoldButtons_on"):document.getElementById("boldButton").setAttribute("class", "BoldButtons");
                        frameContentWindow.execCommand(x,"",y); 
                        break;
                    case "italic":
                        (isItalicOnActive)?isItalicOnActive=false:isItalicOnActive=true;
                        (isItalicOnActive)?document.getElementById("italic").setAttribute("class", "ItalicButtons_on"):document.getElementById("italic").setAttribute("class", "ItalicButtons");
                        frameContentWindow.execCommand(x,"",y); 
                        break;
                    case "underline":
                        (isUnderlineOnActive)?isUnderlineOnActive=false:isUnderlineOnActive=true;
                        (isUnderlineOnActive)?document.getElementById("underline").setAttribute("class", "UnderLineButtons_on"):document.getElementById("underline").setAttribute("class", "UnderLineButtons");
                        frameContentWindow.execCommand(x,"",y); 
                        break;
                    
                    default :
                        try{
                            frameContentWindow.execCommand(x,"",y); 
                         }
                         catch(error){
                            var message=error.message + error.lineNumber;
                            //alert(message)
                            switch(message){
                                case "Access to XPConnect service denied":
                                    alert("Your browser security settings don't permit the editor to automatically execute cutting operations. Please use the keyboard for that (Ctrl/Cmd+X).")
                                    break;
                                    
                                    default: 
                                    alert(message);
                            }
                        }
                    }
                }
            }
        }
    catch(error){
        alert(error.name + ": " + error.message + error.lineNumber);
    }
}

//checks the cursor and return the carret Position
function checkCursor(where){
    try{
        Current=where;
        if (!isToolBoxContainerDivDisabled)
        {
            where.focus();
            if(document.all){
                CarretPosition=document.selection.createRange();
                if(CarretPosition.text==""){
                    where.focus();
                }
            }
        }
    }
    catch(error){
        alert(error.name + ": " + error.message + error.lineNumber);
    }
}

//create the text range
function GetCreateTextRange(){
    try{
        return usedFrame.createTextRange();
    }
    catch(error){
        alert(error.name + ": " + error.message + error.lineNumber);
    }
}

//creates ordered Object
function CreateObject(ObjectType){
    try{
        if(!isToolBoxContainerDivDisabled){
            selectedText=GetSelectedTextRange();
            if(!IsObjectCreated){
                switch(ObjectType){
                    //create link maker form
                    case "CreateLink":
                        checkCursor(usedFrame);
                        CreateTransparentElement('');
                        craeteContainerElement();
                        SetPosition('',"ContainerDiv","Page");
                        CtreateInnerText("Link URL :","ContainerDiv","createdTextLine");
                        createControl("CreateLinkTxt",'ContainerDiv','text','','','createdTextBox');
                        unActiveElementForMove("CreateLinkTxt");
                        createControl("linkOpeningOptions",'ContainerDiv','dropDownList','open link in new window|open link in current window','','createdLinkOptions');
                        unActiveElementForMove("linkOpeningOptions");
                        createControl("setCommand",'ContainerDiv','button','Cancel','','createdButton');
                        unActiveElementForMove("setCommand");
                        linkTextBox=document.getElementById("CreateLinkTxt");
                        linkTextBox.focus();
                        SetCommandButton=document.getElementById("setCommand");
                        CreateEventForGeneratedButton("Link");  
                        linkTextBox.onkeydown=function(e){ LinkTextBoxKeyDownEventHandler(e,SetCommandButton)};
                        linkTextBox.onkeyup=LinkTextBoxKeyUpEventHandler;
                        break;
                    //create Image maker form   
                    case "InsertImage":
                        checkCursor(usedFrame);
                        CreateTransparentElement('');
                        craeteContainerElement();
                        SetPosition('',"ContainerDiv","Page");
                        CtreateInnerText("Image URL :","ContainerDiv","createdTextLine");
                        createControl("CreateLinkTxt",'ContainerDiv','text','','','createdTextBox');
                        unActiveElementForMove("CreateLinkTxt");
                        createControl("setImageCommand",'ContainerDiv','button','Cancel','','createdButton');
                        unActiveElementForMove("setImageCommand");
                        linkTextBox=document.getElementById("CreateLinkTxt");
                        linkTextBox.focus();
                        SetCommandImageButton=document.getElementById("setImageCommand");
                        CreateEventForGeneratedButton("Image");  
                        linkTextBox.onkeydown=function(e){ LinkTextBoxKeyDownEventHandler(e,SetCommandImageButton)};
                        linkTextBox.onkeyup=LinkTextBoxKeyUpEventHandler;
                        break;
                    //create table maker form 
                    case "InsertTable" :
                        //
                        checkCursor(usedFrame);
                        CreateTransparentElement('');
                        CreateTable("table_"+InsertedTableTotal,'');
                        createTbody("tbody"+InsertedTableTotal);
                        SetPosition('',"table_"+InsertedTableTotal,"Page");
                        //
                        CreateTableRow("tr_"+InsertedTableTotal+"_Header");
                        CreateTableTD("td_"+InsertedTableTotal+"_Header","Create Table","createDynamicTable_Header");
                        //
                        CreateTableRow("tr_"+InsertedTableTotal+"_2");
                        CreateTableTD("td_"+InsertedTableTotal+"_2","Rows ","createDynamicTableForm_TD");
                        createControl("RowsIndex","td_"+InsertedTableTotal+"_2",'text','','','TableGeneratorForm_textBox');
                        unActiveElementForMove("RowsIndex");
                        rowsIndexTxt=document.getElementById("RowsIndex");
                        rowsIndexTxt.focus();
                        rowsIndexTxt.onkeyup=checkCreateTableTotalCellAndRowValue;
                        //
                        CreateTableTD("td_"+InsertedTableTotal+"_createTableOnDesign","","InsertedTable-TD");
                        createControl("createTableOnDesignBtn","td_"+InsertedTableTotal+"_createTableOnDesign",'button','','Draw Table','createDynamicTableForm_DesignButton');
                        unActiveElementForMove("createTableOnDesignBtn");
                        createTableOnDesignButton=document.getElementById("createTableOnDesignBtn");
                        createTableOnDesignButton.onclick=creatingTableOnDesignMode;
                        //
                        CreateTableRow("tr_"+InsertedTableTotal+"_1");
                        CreateTableTD("td_"+InsertedTableTotal+"_1"," cells ","createDynamicTableForm_TD");
                        createControl("CellesIndex","td_"+InsertedTableTotal+"_1",'text','','','TableGeneratorForm_textBox');
                        unActiveElementForMove("CellesIndex");
                        cellsIndexTxt=document.getElementById("CellesIndex");
                        cellsIndexTxt.onkeyup=checkCreateTableTotalCellAndRowValue;
                        //
                        CreateTableRow("tr_"+InsertedTableTotal+"_3");
                        CreateTableTD("td_"+InsertedTableTotal+"_3","","InsertedTable-TD");
                        createControl("createTableBtn","td_"+InsertedTableTotal+"_3",'button','Cancel','','createdButton');
                        unActiveElementForMove("createTableBtn");
                        SetCommandInsertTableButton=document.getElementById("createTableBtn");
                        CreateEventForGeneratedButton("insertTable");
                        InsertedTableTotal++;
                        break;
                    //create button maker form
                    case "insertButton":
                        checkCursor(usedFrame);
                        CreateTransparentElement('');
                        CreateTable("table_InsertButton",'');
                        createTbody("tbody");
                        SetPosition('',"table_InsertButton","Page");
                        
                        //create Header
                        CreateTableRow("tr_GenerateButtonHeader");
                        CreateTableTD("td_GenerateButtonHeader","Create Button","createDynamicTable_Header");
                        
                        //create Name Recipient
                        CreateTableRow("table_InsertButtonRow_1");
                        CreateTableTD("buttonPropertiesTd_1_1","Name  ","createDynamicTableForm_TD");
                        createControl("GetButtonName","buttonPropertiesTd_1_1",'text','','','ButtonGeneratorForm_textBox');
                        unActiveElementForMove("GetButtonName");
                        insertedButtonName=document.getElementById("GetButtonName");
                        
                        //create Value Recipient
                        CreateTableRow("table_InsertButtonRow_2");
                        CreateTableTD("buttonPropertiesTd_2_1","Value ","createDynamicTableForm_TD");
                        createControl("GetButtonValue","buttonPropertiesTd_2_1",'text','','','ButtonGeneratorForm_textBox');
                        unActiveElementForMove("GetButtonValue");
                        insertedButtonValue=document.getElementById("GetButtonValue");
                        
                        //create Button Control
                        CreateTableRow("table_InsertButtonRow_4");
                        CreateTableTD("buttonPropertiesTd_4_1","","createDynamicTableForm_TD");
                        createControl("createNewButton","buttonPropertiesTd_4_1",'button','Ok','','createdButtonForInsertButton');
                        unActiveElementForMove("createNewButton");
                        SetCommandInsertButton_Button=document.getElementById("createNewButton");
                        
                        createControl("createCancelButton","buttonPropertiesTd_4_1",'button','Cancel','','createdButtonForInsertButton');
                        unActiveElementForMove("createCancelButton");
                        SetCommandCancelInsertingButton_Button=document.getElementById("createCancelButton");
                        CreateEventForGeneratedButton("insertButton");
                        CreateEventForGeneratedButton("CancelingInsertButtton");
                        break;
                    case "insertSWF":
                        
                    }
                }
            IsObjectCreated=true;
        }
    }
    catch(error){
        alert(error.name + ": " + error.message + error.lineNumber);
    }
}
//Deletes Cookies
function deletecooki(cookieValues){
    var date = new Date();
    document.cookie = cookieValues+";expires=" + date.toGMTString() + ";" + ";";
}

//Checks the table rows and cells values
function checkCreateTableTotalCellAndRowValue()
{
    (cellsIndexTxt.value!=null && rowsIndexTxt.value!=null && rowsIndexTxt.value!="" && cellsIndexTxt.value!="" && rowsIndexTxt.value.length>=0 && cellsIndexTxt.value.length>=0 &&  (parseInt(cellsIndexTxt.value)>0 && parseInt(rowsIndexTxt.value)>0))?SetCreateTableButtonValueTo("Ok"):SetCreateTableButtonValueTo("Cancel");
}

//creates SWF Maker
function createSWFMaker(){
    checkCursor(usedFrame);
    CreateTransparentElement('form1');
    CreateTable("table_"+InsertedTableTotal,'');
    createTbody("tbody"+InsertedTableTotal);
    SetPosition('',"table_"+InsertedTableTotal,"Page");
    CreateTableRow("tr_"+InsertedTableTotal+"_SWFBodyRow1");
    CreateTableTD("td_"+InsertedTableTotal+"_SWFBodyLeft","","insertSWF_Header_Left");
    CtreateInnerText("URL","td_"+InsertedTableTotal+"_SWFBodyLeft","createdTextLineForTD");
    CreateTableTD("td_"+InsertedTableTotal+"_SWFBodyRight","","");
    createControl("GetSWFURL_Text","td_"+InsertedTableTotal+"_SWFBodyRight",'text','','','SWFFileURL');
    linkTextBox=document.getElementById("GetSWFURL_Text");
    unActiveElementForMove("GetSWFURL_Text");
    CreateTableRow("tr_"+InsertedTableTotal+"_SWFBodyRow2");
    CreateTableTD("td_"+InsertedTableTotal+"_SWFBodyLeft2","","insertSWF_Header_Left");
    createControl("GetSWFURL_FileUploader","td_"+InsertedTableTotal+"_SWFBodyLeft2",'button','Upload','','createUploadButton');
    unActiveElementForMove("GetSWFURL_FileUploader");
    CreateTableTD("td_"+InsertedTableTotal+"_SWFBodyRight2","","insertSWF_Header_Right");
    createControl("file","td_"+InsertedTableTotal+"_SWFBodyRight2",'file','','','uploadFile');
    unActiveElementForMove("file");
    //IE Support
    if(document.all){
        document.getElementById("file").onchange=function(){
            fileValues=this.value.split("\\");
            deletecooki();
        }
    }
    SetSWFUploaderCommandButton=document.getElementById("GetSWFURL_FileUploader");
    CreateEventForGeneratedButton("uploadSWF");
    CreateTableRow("tr_"+InsertedTableTotal+"_SWFBodyRow4");
    CreateTableTD("td_"+InsertedTableTotal+"_SWFBodyLeft4","","");
    CtreateInnerText("Preview","td_"+InsertedTableTotal+"_SWFBodyLeft4","createdTextLineForTD");
    CreateTableTD("td_"+InsertedTableTotal+"_SWFBodyRight4","","");
    var SWFPreviewDiv=document.createElement("div");
    SWFPreviewDiv.setAttribute("class","SWFPreviewDiv");
    SWFPreviewDiv.id="priviewDiv";
    document.getElementById("td_"+InsertedTableTotal+"_SWFBodyRight4").appendChild(SWFPreviewDiv);
    if(document.cookie.length!=0){
        usedFrame.innerHTML=getCookiesInformation(document.cookie,"HTMLSource");
        document.getElementById("priviewDiv").innerHTML="<object classid=\"clsid:d27cdb6e-ae6d-11cf-96b8-444553540000\" codebase=\"http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=6,0,40,0\" width=\"235\" height=\"100\" id=\"mymoviename\"><param name=\"movie\" value=\"Uploads/"+getCookiesInformation(document.cookie,"SWFFile")+"\" /><param name=\"quality\" value=\"high\" /><param name=\"bgcolor\" value=\"#ffffff\" /><embed src=\"Uploads/"+getCookiesInformation(document.cookie,"SWFFile")+"\" quality=\"high\"  width=\"235\" height=\"100\" name=\"mymoviename\" align=\"\" type=\"application/x-shockwave-flash\" pluginspage=\"http://www.macromedia.com/go/getflashplayer\"></embed></object>";
    }
    CreateTableRow("checkBoxRow");
    CreateTableTD("td_"+InsertedTableTotal+"_SWFBodyLeft3","","insertSWF_Header_Left");
    createControl("SWFConfrimCheckBox","td_"+InsertedTableTotal+"_SWFBodyLeft3",'checkBox','','','SWFConfrimCheckBox');
    var SWFConfrimCheckBox=document.getElementById("SWFConfrimCheckBox");
    SWFConfrimCheckBox.onclick=function(){
        if(SWFConfrimCheckBox.checked){
            document.getElementById("GetSWFURL_FileUploaderButton").value="Ok";
            disableElement(linkTextBox);
        }
        else if(!SWFConfrimCheckBox.checked &&linkTextBox.value==''){    
            document.getElementById("GetSWFURL_FileUploaderButton").value="Cancel";
            disableElement(linkTextBox);
        }
        else if(!SWFConfrimCheckBox.checked && linkTextBox.value!=''){
            document.getElementById("GetSWFURL_FileUploaderButton").value="Ok";
            disableElement(linkTextBox);
        }
    }
    CreateTableTD("bluckTD","","");
    CreateTableRow("tr_"+InsertedTableTotal+"_SWFBodyRow4");
    CreateTableTD("SWFWidthTD","","");
    CtreateInnerText("Width","SWFWidthTD","createdTextLineForTD");
    CreateTableTD("SWFWidth","","");
    createControl("SWFWidthText","SWFWidth",'text','','','SWFWidthTextBox');
    document.getElementById("SWFWidthText").value="100";
    document.getElementById("SWFWidthText").onkeypress=function(event){
        return isNumberKey(event);
    }
    CreateTableRow("tr_"+InsertedTableTotal+"_SWFBodyRow5");
    CreateTableTD("SWFHeightTD","","");
    CtreateInnerText("Height","SWFHeightTD","createdTextLineForTD");
    CreateTableTD("SWFHeight","","");
    createControl("SWFHightText","SWFHeight",'text','','','SWFHeightTextBox');
    document.getElementById("SWFHightText").value="100";
    document.getElementById("SWFHightText").onkeypress=function(event){
        return isNumberKey(event);
    }
    CreateTableRow("tr_"+InsertedTableTotal+"_SWFBodyRow3");
    CreateTableTD("td_"+InsertedTableTotal+"_SWFBodyleft3","","");
    CreateTableTD("td_"+InsertedTableTotal+"_SWFBodyRight3","","cxb");
    createControl("GetSWFURL_FileUploaderButton","td_"+InsertedTableTotal+"_SWFBodyRight3",'button','','','createUploadButton');
    if(isPreviewElementsInnerHTMLNullOrEmpty("priviewDiv")){
        document.getElementById("GetSWFURL_FileUploaderButton").value="Cancel";
        disableElement(SWFConfrimCheckBox);
    }
    else if(linkTextBox.value!=''){
        document.getElementById("GetSWFURL_FileUploaderButton").value="Ok";
        disableElement(linkTextBox);
    }
    else{
        document.getElementById("GetSWFURL_FileUploaderButton").value="Cancel";
    }
    SetSWFCommandButton=document.getElementById("GetSWFURL_FileUploaderButton");
    CreateEventForGeneratedButton("insertSWF");
    unActiveElementForMove("GetSWFURL_FileUploaderButton");
    unActiveElementForMove("SWFConfrimCheckBox");
    unActiveElementForMove("SWFHightText");
    unActiveElementForMove("SWFWidthText");
    linkTextBox.onkeydown=function(e){ LinkTextBoxKeyDownEventHandler(e,SetSWFCommandButton)};
    linkTextBox.onkeyup=LinkTextBoxKeyUpEventHandler;
}

function isNumberKey(evt){
    evt = (evt) ? evt : window.event;
    var charCode = (evt.which) ? evt.which : evt.keyCode
    if (charCode > 31 && (charCode < 48 || charCode > 57)) {
        status = "This field accepts numbers only."
        return false
    }
    return true
}

function isPreviewElementsInnerHTMLNullOrEmpty(elementName){
    if(document.getElementById(elementName).innerHTML!=''){
        return false;
    }
    return true;
}

function SetInsertSWFButtonCommandEventHandler(){
    if(document.getElementById("SWFConfrimCheckBox").checked){
        pasteHtmlElements("internalSWF","",getCookiesInformation(document.cookie,"SWFFile"),'height="'+document.getElementById("SWFHightText").value+'";width="'+document.getElementById("SWFWidthText").value+'"');
    }
    else if(!document.getElementById("SWFConfrimCheckBox").checked && linkTextBox.value!=''){
        pasteHtmlElements("externalSWF","",linkTextBox.value,'');
    }
    deletecooki(document.cookie);
    RemoveCreatedElement("form1");
    IsObjectCreated=false;
    isToolBoxContainerDivDisabled=false;
}

function SetSWFUploaderButtonCommandEventHandler(){
    if(document.getElementById("file").value!=''){
        var isSuffixCorrect=false;
        if(document.all){
           document.cookie=fileValues[fileValues.length-1]+"|"+usedFrame.innerHTML;
           (suffixChecker(fileValues[fileValues.length-1],'swf'))?isSuffixCorrect=true:isSuffixCorrect=false;
        }
        else{
            document.cookie=document.getElementById("file").value+"|"+usedFrame.innerHTML;
            (suffixChecker(document.getElementById("file").value,'swf'))?isSuffixCorrect=true:isSuffixCorrect=false;
        }
        
        (isSuffixCorrect)?document.form1.submit():alert('The Selected File Is Not In An Acceptable Format');
    }
    else{
        alert("No file have boon choosed");
    }
}

function suffixChecker(fileValue,suffix){
    var opc=fileValue.split(".");
    if(opc[opc.length-1]==suffix)
        return true;
    return false;
}

function getCookiesInformation(cookie,dataType){
    var cookieObject=cookie.split("|");
    if(dataType=="SWFFile")
    {
        return cookieObject[0].replace("|","");
    }
    else if(dataType=="HTMLSource"){
        return cookieObject[1].replace(/__/g,"");
    }
    else if(dataType=="carretPosition"){
        return cookieObject[2];
    }
}


function unActiveElementForMove(elementName){
    document.getElementById(elementName).onmousedown=function(){
        unActivegeneratedControlForMove=true;
    }
    document.getElementById(elementName).onmouseup=function(){
        unActivegeneratedControlForMove=false;
    }
}

//Gets dispay of main table of table maker object
var MainGeneratedTableForTableCreationDisplay=false;

//Shows table maker object
function creatingTableOnDesignMode(e){
    try{
       if(isTableCreationFormGenerated==false){
            showTableOnDesignState(CreateDimensionalArrays(6,6));
            SetPosition(e,"MainGeneratedTableForTableCreation","Mouse");
            MainGeneratedTableForTableCreationDisplay=true;
        }
        else if(!isColsSelectionStop){
            document.getElementById("TransparentDiv").removeChild($_MainGeneratedTableForTableCreation);
            document.getElementById("CellesIndex").value=0;
            document.getElementById("RowsIndex").value=0;
            SetCreateTableButtonValueTo("Cancel");
            isTableCreationFormGenerated=false;
            isColsSelectionStop=false;
        }
        else if(MainGeneratedTableForTableCreationDisplay){
                document.getElementById("MainGeneratedTableForTableCreation").style.display="none";
                SetCreateTableButtonValueTo("Ok");
                MainGeneratedTableForTableCreationDisplay=false;
        }
        else{
            document.getElementById("MainGeneratedTableForTableCreation").style.display="block";
            SetCreateTableButtonValueTo("Ok");
            MainGeneratedTableForTableCreationDisplay=true;
        }
    }
    catch(error){
        alert(error.name + ": " + error.message + error.lineNumber + error.lineNumber);
    }
}

//Sets created table for current object maker
function SetCreateTableButtonValueTo(value){
    try{
        document.getElementById("createTableBtn").value=value;
        switch(value){
            case"Ok":
                isButtonActive=true;
                break;
            case"Cancel":
                isButtonActive=false;
        }
    }
    catch(error){
        alert(error.name + ": " + error.message + error.lineNumber);
    }
    
}

//Sets the position of object maker
function SetPosition(e,elementName,Related){
    try {
        e = (e) ? e : ((window.event) ? window.event : "");
        var ContainerElement=document.getElementById(elementName);
        switch(Related){
            case "Page":
                if(ContainerElement!=null){
                    ContainerElement.style.left=(document.body.clientWidth)/2-100 +"px";
                    ContainerElement.style.top=(document.body.clientHeight)/2-100 +"px";
                }
            break;
            case "Mouse":
                    document.getElementById(elementName).style.display="block";
                    document.getElementById(elementName).style.left=e.clientX+10+"px";
                    document.getElementById(elementName).style.top=e.clientY +"px";
            break;
        }
    }
    catch(error){
        alert(error.name + ": " + error.message + error.lineNumber);
    }
}

//The event handler of LinkTextBox onkeyDown event
function LinkTextBoxKeyDownEventHandler(event,commandSetterName){
    try{
        var ctrlKey = 17, vKey = 86, cKey = 67;
        event=event||window.event;
        if (event.keyCode == ctrlKey) ctrlDown = true;
        if (ctrlDown && (event.keyCode == vKey)){
            (commandSetterName!=null)?commandSetterName.value="Ok":commandSetterName.value="Ok";
        }
        else if(NullOrEmpty(linkTextBox.value)||linkTextBox.value.length<=1){
            (commandSetterName!=null)?commandSetterName.value="Cancel":commandSetterName.value="Cancel";
        }
        else if(commandSetterName!=null){
            commandSetterName.value="Ok";
        }
    }
    catch(error){
        alert(error.name + ": " + error.message + error.lineNumber)
    }
 }
 
 //Checks the string is Null or Empty and return true or false value
 function NullOrEmpty(string){
    try{
        if(string==null|| string=="") 
            return true;
        else 
            return false;
    }
    catch(error){
        alert(error.name + ": " + error.message + error.lineNumber)
    }
}
 
 //The event handler of LinkTextBox onkeyUp event
 function LinkTextBoxKeyUpEventHandler(){
    ctrlDown = false;
 }

//Reconfigs the position of object maker
window.onresize=function(){
    SetPosition('',"ContainerDiv","Page");
    if($_TransparentDiv!=null){
        $_TransparentDiv.setAttribute("style","width:"+(document.documentElement.clientWidth)+"px; height:"+((document.body.scrollHeight==0)?document.documentElement.clientHeight:document.body.scrollHeight)+"px");
    }
}

//Creates an event for generated button related to button type  
function CreateEventForGeneratedButton(ActionType){
    try{
        switch(ActionType){
            case "Link":
               (document.all)?SetCommandButton.attachEvent ("onclick",SetLinkCommandEventHandler):SetCommandButton.addEventListener ("click",SetLinkCommandEventHandler,false);
               break;
            case "Image":
               (document.all)?SetCommandImageButton.attachEvent ("onclick",SetImageCommandEventHandler):SetCommandImageButton.addEventListener ("click",SetImageCommandEventHandler,false);
                break;
            case "insertTable":
               (document.all)?SetCommandInsertTableButton.attachEvent ("onclick",SetInsertTableCommandEventHandler):SetCommandInsertTableButton.addEventListener ("click",SetInsertTableCommandEventHandler,false);
                break;
            case "insertButton":
               (document.all)?SetCommandInsertButton_Button.attachEvent ("onclick",SetInsertButtonCommandEventHandler): SetCommandInsertButton_Button.addEventListener ("click",SetInsertButtonCommandEventHandler,false);
                break;
            case "insertSWF":
                isToolBoxContainerDivDisabled=false;
               (document.all)?SetSWFCommandButton.attachEvent ("onclick",SetInsertSWFButtonCommandEventHandler): SetSWFCommandButton.addEventListener ("click",SetInsertSWFButtonCommandEventHandler,false);
                break;
            case "uploadSWF":
               (document.all)?SetSWFUploaderCommandButton.attachEvent ("onclick",SetSWFUploaderButtonCommandEventHandler): SetSWFUploaderCommandButton.addEventListener ("click",SetSWFUploaderButtonCommandEventHandler,false);
                break;
            case "CancelingInsertButtton":
               (document.all)?SetCommandCancelInsertingButton_Button.attachEvent ("onclick",SetCancelingInsertButtonCommandEventHandler):SetCommandCancelInsertingButton_Button.addEventListener ("click",SetCancelingInsertButtonCommandEventHandler,false);
                break;
        }
    }
    catch(error){
        alert(error.name + ": " + error.message + error.lineNumber);
    }
}

//Sets the events of InsertTableCommandEventHandler
function SetInsertTableCommandEventHandler(){
    try{
        if(isButtonActive){
            insertTable(parseInt(cellsIndexTxt.value),parseInt(rowsIndexTxt.value));
            RemoveCreatedElement('');
            IsObjectCreated=false;
            isButtonActive=false;
            isTableCreationFormGenerated=false;
            isColsSelectionStop=false;
            createdTableString="";
        }
        else{
            isTableCreationFormGenerated=false;
            RemoveCreatedElement('');
            IsObjectCreated=false;
            isButtonActive=false;
        }
    }
    catch(error){
        alert(error.name + ": " + error.message + error.lineNumber)
    }
}

//Sets the events for InsertButtonCommandEventHandler
function SetInsertButtonCommandEventHandler(){
    try{
        pasteHtmlElements("button",insertedButtonName.value,insertedButtonValue.value,'')
        IsObjectCreated=false;
        RemoveCreatedElement('');
    }
    catch(error){
        alert(error.name + ": " + error.message + error.lineNumber)
    }
}

//Set the events for CancelingInsertButtonCommandEventHandler
function SetCancelingInsertButtonCommandEventHandler(){
    try{
        IsObjectCreated=false;
        RemoveCreatedElement('');
    }
    catch(error){
        alert(error.name + ": " + error.message + error.lineNumber);
    }
}

//Inserts table
function insertTable(cellsTotal,rowsTotal){
    try{
        createdTableString+="<table border='1'>";
        for(var thisRowsIndex=0;thisRowsIndex<rowsTotal;thisRowsIndex++){
            createdTableString+="<tr>";
                for(var thisCellsIndex=0;thisCellsIndex<cellsTotal;thisCellsIndex++){
                    createdTableString+="<td width='30px' height='30px'>";
                    createdTableString+="</td>";
                }
            createdTableString+="</tr>";
        }
        createdTableString+="</table>";
        pasteHtmlElements("table",'','','');
    }
    catch(error){
        alert(error.name + ": " + error.message + error.lineNumber);
    }
}
//Pastes html elements
function pasteHtmlElements(elementType,Name,Value,Properties){
    try{
        //IE-FireFox-chrome
        if(!isToolBoxContainerDivDisabled){
          (document.all)?window.frames["textEditor"].focus():textEditorElement.focus();
            switch (elementType){
                case "horizontalLine":
                    (document.all)?usedFrameDocument.selection.createRange().pasteHTML("<hr />"):frameContentWindow.execCommand('insertHTML',false,"<hr/>");
                    break;
                case "pageBreaker":
                    (document.all)?usedFrameDocument.selection.createRange().pasteHTML("<hr style='page-break-before: always' SIZE=6></hr>"):frameContentWindow.execCommand('insertHTML',false,"<hr style='page-break-before: always' SIZE=6></hr>");
                    break;
                case "image":
                    if(!NullOrEmpty(linkTextBox.value)){
                        (document.all)?CarretPosition.pasteHTML('<img src='+linkTextBox.value.toString()+' />'):frameContentWindow.execCommand('insertHTML',false,'<img src='+linkTextBox.value.toString()+' />');
                    }
                    break;
                case "button":
                     (document.all)?CarretPosition.pasteHTML("<input type='button' value='"+Value+"' name='"+Name+"' />"):frameContentWindow.execCommand('insertHTML',false,"<input type='button' value='"+Value+"' name='"+Name+"' />");
                    break;
                case "table":
                     (document.all)?CarretPosition.pasteHTML(createdTableString):frameContentWindow.execCommand('insertHTML',false,createdTableString);
                    break;
                case "checkBox":
                     checkCursor(usedFrame);
                     checkBoxPasteTotal++;
                     (document.all)?CarretPosition.pasteHTML("<input type='checkbox' id='checkBox"+checkBoxPasteTotal+"' />"):frameContentWindow.execCommand('insertHTML',false,"<input type='checkbox' id='checkBox"+checkBoxPasteTotal+"' />");
                    break;
                case "password":
                    checkCursor(usedFrame);
                    passwordPasteTotal++;
                    (document.all)?CarretPosition.pasteHTML("<input type='password' id='password"+passwordPasteTotal+"' />"):frameContentWindow.execCommand('insertHTML',false,"<input type='password' id='password"+passwordPasteTotal+"' />");
                    break;
                case "textArea":
                    checkCursor(usedFrame);
                    textAreaPasteTotal++;
                    (document.all)?CarretPosition.pasteHTML("<textarea id='textArea"+textAreaPasteTotal+"'></textarea>"):frameContentWindow.execCommand('insertHTML',false,"<textarea id='textArea"+textAreaPasteTotal+"'></textarea>"); ;
                    break;
                case "comboBox":
                    checkCursor(usedFrame);
                    selectPasteTotal++;
                    (document.all)?CarretPosition.pasteHTML("<select id='select"+selectPasteTotal+"'><option id='option1' value='option1'>option1</option><option id='option2' value='option2'>option2</option></select>"):frameContentWindow.execCommand('insertHTML',false,"<select id='select"+selectPasteTotal+"'><option id='option1' value='option1'>option1</option><option id='option2' value='option2'>option2</option></select>");
                    break;
                case "file":
                    checkCursor(usedFrame);
                    filePasteTotal++;
                    (document.all)?CarretPosition.pasteHTML("<input type='file' id='password"+filePasteTotal+"' />"):frameContentWindow.execCommand('insertHTML',false,"<input type='file' id='password"+filePasteTotal+"' />");
                    break;
                case "textBox":
                    checkCursor(usedFrame);
                    textBoxPasteTotal++;
                    (document.all)?CarretPosition.pasteHTML("<input type='text' id='textBox"+textBoxPasteTotal+"' />"):frameContentWindow.execCommand('insertHTML',false,"<input type='text' id='textBox"+textBoxPasteTotal+"' />");
                    break; 
                case "radioButton":
                    checkCursor(usedFrame);
                    radioGroupValue++;
                    (document.all)?CarretPosition.pasteHTML("<input type='radio' name='group"+radioGroupValue+"' id='radio"+radioGroupValue+1 +"' /><input type='radio' name='group"+radioGroupValue+"' id='radio"+radioGroupValue+2 +"' /><input type='radio' name='group"+radioGroupValue+"' id='radio3"+radioGroupValue+3 +"' />"):frameContentWindow.execCommand('insertHTML',false,"<input type='radio' name='group"+radioGroupValue+"' id='radio"+radioGroupValue+1 +"' /><input type='radio' name='group"+radioGroupValue+"' id='radio"+radioGroupValue+2 +"' /><input type='radio' name='group"+radioGroupValue+"' id='radio"+radioGroupValue+3 +"' />");
                    break;
                case "specialCharacters":
                    (document.all)?CarretPosition.pasteHTML(Value):frameContentWindow.execCommand('insertHTML',false,Value);
                    break;
                    
                case "internalSWF":
                    var properties=Properties.split(";");
                    var range;
                    if(document.selection){
                        var SWFobject="<object classid=\"clsid:d27cdb6e-ae6d-11cf-96b8-444553540000\" codebase=\"http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=6,0,40,0\" "+properties[0]+" "+properties[1]+" id=\"mymoviename\"><param name=\"movie\" value=\"Uploads/"+Value+"\" /><param name=\"quality\" value=\"high\" /><param name=\"bgcolor\" value=\"#ffffff\" /><embed src=\"Uploads/"+Value+"\" quality=\"high\"  width=\"235\" height=\"100\" name=\"mymoviename\" align=\"\" type=\"application/x-shockwave-flash\" pluginspage=\"http://www.macromedia.com/go/getflashplayer\"></embed></object>";
                        range=document.selection.createRange();
                        range.pasteHTML(SWFobject);
                    }
                    else{
                        var SWFobject="<object classid=\"clsid:d27cdb6e-ae6d-11cf-96b8-444553540000\" border='50px' class=\"cke_flash\" codebase=\"http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=6,0,40,0\" "+properties[0]+" "+properties[1]+" id=\"mymoviename\"><param name=\"movie\" value=\"Uploads/"+Value+"\" /><param name=\"quality\" value=\"high\" /><param name=\"bgcolor\" value=\"#ffffff\" /><embed src=\"Uploads/"+Value+"\" quality=\"high\"  width=\"235\" height=\"100\" name=\"mymoviename\" align=\"\" type=\"application/x-shockwave-flash\" pluginspage=\"http://www.macromedia.com/go/getflashplayer\"></embed></object>";
                        frameContentWindow.execCommand('insertHTML',false,SWFobject);
                    }
                    break;
                case "externalSWF":
                    var properties=Properties.split(";");
                    if(document.selection){
                        var range=document.selection.createRange();
                    }
                    var SWFobject="<object border='50px' classid=\"clsid:d27cdb6e-ae6d-11cf-96b8-444553540000\" codebase=\"http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=6,0,40,0\" "+properties[0]+" "+properties[1]+" id=\"mymoviename\"><param name=\"movie\" value="+Value+" /><param name=\"quality\" value=\"high\" /><param name=\"bgcolor\" value=\"#ffffff\" /><embed src="+Value+" quality=\"high\"  width=\"235\" height=\"100\" name=\"mymoviename\" align=\"\" type=\"application/x-shockwave-flash\" pluginspage=\"http://www.macromedia.com/go/getflashplayer\"></embed></object>";
                    (document.all)?range.pasteHTML(SWFobject):
                        frameContentWindow.execCommand('insertHTML',false,SWFobject);
                    break;
            }
        }
    }
    catch(error){
        alert(error.name + ": " + error.message + error.lineNumber);
    }
}

//sets event for LinkCommandEventHandler
function SetLinkCommandEventHandler(){
    try{
        if(!NullOrEmpty(linkTextBox.value)){
            var openingInNewLink=(document.getElementById(createdLinkOptionId).value=="open link in new window")? true:false;
            var Target = '_blank';
            if(document.all){
                selectedText.execCommand("CreateLink",false,linkTextBox.value);
                if(openingInNewLink)selectedText.parentElement().setAttribute("target", Target);
            }
            else{
               frameContentWindow.execCommand("CreateLink",false,linkTextBox.value);
                 var linkFinder = frameContentWindow.getElementsByTagName('a');
                  for(i=0;i<linkFinder.length;i++){
                      if(linkFinder[i].href==linkTextBox.value && openingInNewLink){
                          linkFinder[i].setAttribute("target",Target);
                      }
                  }
             }
        }
        RemoveCreatedElement('');
        IsObjectCreated=false;
    }
    catch(error){
        alert(error.name + ": " + error.message + error.lineNumber);
    }
}

// sets event for ImageCommandEventHandler
function SetImageCommandEventHandler(){
    try{
        if(!NullOrEmpty(linkTextBox.value)){
            pasteHtmlElements("image",'','','');
        }
        RemoveCreatedElement('');
        IsObjectCreated=false;
    }
    catch(error){
        alert(error.name + ": " + error.message + error.lineNumber);
    }
}

//Removes the generated object maker 
function RemoveCreatedElement(elementName){
   if($_ContainerDiv!=null || !isButtonActive || isButtonActive){
        (elementName=='')?document.body.removeChild($_TransparentDiv):document.getElementById(elementName).removeChild(document.getElementById("TransparentDiv"));          
        SetCommandButton=null;
    }
}

function createSpecialCharacters(e){
    var listOfSpecialCharactes=["!",
    "'","#","$","%","&","(",")","!","@","^","*","+","-","/","~","`",":",
    ",","|","[","]","{","}","_","€","“","—","¢","£","¤","¥","¦","§","¨","©",
    "«","¬","®","±","µ","¶","¸","»","¼","½","¿","À","Á","Â","Ã","Ä","Å","Æ",
    "Ç","È","Ê","Ë","Ì","Î","Ï","Ð","Ñ","Ò","Ó","Ô","Õ","Ö","×","é","ê","ë",
    "ì","í","î","ï","ð","ñ","ò","ó","ô","õ","ö","÷","ø","ù","ú","û","ü","ý",
    "þ","Œ","œ","Ŵ","ŵ","ŷ","„","…","™","►","•","→","⇒","⇔","♦","≈"];
    CreateTransparentElement('');
    GenerateTable(Math.round(listOfSpecialCharactes.length/10),10,document.getElementById("TransparentDiv"),listOfSpecialCharactes,"unActiveSpecialCharactersTable");
    SetPosition(e,"specialCharactersTable","Mouse");
}


 
//Creates Dimensional Arrays   
function CreateDimensionalArrays(arrayRows,arrayCols){
    try{
        var dimensionalArray = new Array(arrayRows) 
        for (i = 0; i < dimensionalArray.length; ++ i){
            dimensionalArray[i] = new Array(arrayCols);
        }
        return dimensionalArray;
    }
    catch(error){
        alert(error.name + ": " + error.message + error.lineNumber);
    }
}
	       
//Draws table
function showTableOnDesignState(array){ 
    try{
        var row;
        $_MainGeneratedTableForTableCreation=document.createElement("table") ;
        $_MainGeneratedTbodyForTableCreation=document.createElement("tbody") ;
        $_MainGeneratedTableForTableCreation.id="MainGeneratedTableForTableCreation";
        $_MainGeneratedTableForTableCreation.className="createDynamicTableForm_Table";
        for (row = 0; row < array.length; ++row){ 
            var currentRow=document.createElement ("tr");
            currentRow.id="row"+row
            var col; 
            for (col = 0; col < array[row].length; ++col){ 
                var currentCol=document.createElement ("td");
                currentCol.id="row"+row+"_col"+col ;
                currentCol.className="unSelectedCols";
                currentCol.setAttribute('class','unSelectedCols');
                currentCol.setAttribute("onMouseOver","getSubCells("+row+","+col+",'SelectedsubCols'),setCellsAndRowsDesign("+(row+1)+","+(col+1)+")");
                currentCol.setAttribute("onClick",'ColsSelectionState()');
                currentCol.setAttribute("title",(row+1)+" x "+(col+1));
                currentRow.appendChild(currentCol);
            }
            $_MainGeneratedTbodyForTableCreation.appendChild(currentRow);
            $_MainGeneratedTableForTableCreation.appendChild($_MainGeneratedTbodyForTableCreation);
        }
        document.getElementById("TransparentDiv").appendChild($_MainGeneratedTableForTableCreation);
        isTableCreationFormGenerated=true;
    }
    catch(error){
        alert(error.name + ": " + error.message + error.lineNumber);
    }
}

//generates the table
function GenerateTable(RowsIndex,CellsIndex,parentElement,arrayName,ClassName) {
    try{
        // get the reference for the body
        var body = document.getElementsByTagName("body")[0];
        var cellsCounter=0;
        
        // creates a <table> element and a <tbody> element
        var tbl= document.createElement("table");
        tbl.className=ClassName;
        tbl.id="specialCharactersTable";
        var tblBody = document.createElement("tbody");
        
        //row
        var headerRow = document.createElement("tr");
        var headerCell=document.createElement("td");
        headerCell.align="center";
        (document.all)?headerCell.innerText="Close":headerCell.textContent="Close";
        headerCell.className="header_SpecialCharacterTable";
        headerCell.align="center";
        //bigSize
        var showBigSize_Table=document.createElement("table");
        showBigSize_Table.width="70px";
        var showBigSize_tr=document.createElement("tr");
        var showBigSize_td=document.createElement("td");
        showBigSize_td.className="showBigSize";
        showBigSize_td.align="center";
        showBigSize_Table.appendChild(showBigSize_tr);
        showBigSize_tr.appendChild(showBigSize_td);
        headerCell.onclick=function(){
            RemoveCreatedElement('');
        }
        var leftTD=document.createElement("TD");
        var rightTD=document.createElement("TD");
        rightTD.className="specialChar_RightTd";
        
        headerRow.appendChild(headerCell);
        for (var j = 0; j < RowsIndex; j++) {
            // creates a table row
            var row = document.createElement("tr");
            for (var i = 0; i < CellsIndex; i++) {
                // Create a <td> element and a text node, make the text
                // node the contents of the <td>, and put the <td> at
                // the end of the table row
                if(cellsCounter<arrayName.length){
                    var cell = document.createElement("td");
                    cell.className="unActiveSpecialCharactersCell";
                    (document.all)?cell.innerText=arrayName[cellsCounter]:cell.textContent=arrayName[cellsCounter];
                    cell.onclick=function(){
                        (document.all)?pasteHtmlElements("specialCharacters","",this.innerText,''):pasteHtmlElements("specialCharacters","",this.textContent,'');
                    }
                    cell.onmouseover=function(){
                        this.className="activeSpecialCharactersCell";
                        (document.all)?showBigSize_td.innerText=this.innerText:showBigSize_td.textContent=this.textContent;
                    }
                    cell.onmouseout=function(){this.className="unActiveSpecialCharactersCell"}
                    row.appendChild(cell);
                    cellsCounter++;
                }
            }
            // add the row to the end of the table body
            leftTD.appendChild(row);
            rightTD.appendChild(headerRow);
            rightTD.appendChild(showBigSize_Table);
            tblBody.appendChild(leftTD);
            tblBody.appendChild(rightTD);   
        }
        // put the <tbody> in the <table>
        tbl.appendChild(tblBody);
        // appends <table> into <body>
        parentElement.appendChild(tbl);
        // sets the border attribute of tbl to 2;
        tbl.setAttribute("border", "2");
    }
    catch(error){
        alert(error.name + ": " + error.message + error.lineNumber);
    }        
}


//Gets the sub cells of current cell
function getSubCells(currentRow,currentCol,cssClassName){
    try{
        if(!isColsSelectionStop){
            if(isRepeated){resetSubColsStyle(PreviousRow,PreviousCol,"unSelectedCols")}
            for(var subRowsIndex=currentRow;subRowsIndex>=0;subRowsIndex--){
                for(var subColsIndex=currentCol;subColsIndex>=0;subColsIndex--){
                    document.getElementById("row"+subRowsIndex+"_col"+subColsIndex).className=cssClassName;
                }
            }
            PreviousCol=currentCol;
            PreviousRow=currentRow;
            isRepeated=true;
        }
    }
    catch(error){
        alert(error.name + ": " + error.message + error.lineNumber);
    }
}

//Gets the state of cols selection
//is cols selection was Definitived? 
function ColsSelectionState(){
    try{
        (isColsSelectionStop==false)?isColsSelectionStop=true:isColsSelectionStop=false;
    }
    catch(error){
        alert(error.name + ": " + error.message + error.lineNumber);
    }
    
}

//Resets the sub cols style
function resetSubColsStyle(Rows,Cols,cssClassName){
    try{
        for(var subRowsIndex=Rows;subRowsIndex>=0;subRowsIndex--){
            for(var subColsIndex=Cols;subColsIndex>=0;subColsIndex--){
                document.getElementById("row"+subRowsIndex+"_col"+subColsIndex).className=cssClassName;
            }
        }
    }
    catch(error){
        alert(error.name + ": " + error.message + error.lineNumber);
    }
}

//sets the value of selected cells and rows
function setCellsAndRowsDesign(Rows,Colls){
    try{
        if(!isColsSelectionStop){
            document.getElementById("CellesIndex").value=Colls;
            document.getElementById("RowsIndex").value=Rows;
        }
    }
    catch(error){
        alert(error.name + ": " + error.message + error.lineNumber);
    }
    
}

//implements all functions about ColorPicker
function colorPickerAction(e,client,actionFor){
    try {
        e = (e) ? e : ((window.event) ? window.event : "");
        if(!isToolBoxContainerDivDisabled){
            setDispalyTo("advanceColorPickerDiv","hide");
            isAdvanceColorPickerInAction=false;
            (!isColorPickerInAction) ? SetPosition(e,actionFor,'Mouse'):setDispalyTo(actionFor,"hide");
            (isColorPickerInAction) ? isColorPickerInAction=false:isColorPickerInAction=true;
            clientName=client;
            ActionFor=actionFor;
        }
    }
    catch(error){
        alert(error.name + ": " + error.message + error.lineNumber);
    }
}

//sets display to an specified element
function setDispalyTo(elementName,setTo){
    try{
        switch(setTo){
            case "show":
                document.getElementById(elementName).style.display="block";
            break;
            
            case "hide":
                document.getElementById(elementName).style.display="none";
            break;
        }
    }
    catch(error){
        alert(error.name + ": " + error.message + error.lineNumber);
    }
}

//gets selected color
function getColor(block){   
    try{
        var s_url=block.href;
        var pColor=s_url.substr(s_url.indexOf("#"));
        switch(clientName){
            case "backColor":
                SetBackColor(pColor);
                setDispalyTo(ActionFor,"hide");
                isColorPickerInAction=false;
            break;
        }
    }
    catch(error){
        alert(error.name + ": " + error.message + error.lineNumber);
    }
}

//sets returned color from getColor() to selected text
function SetBackColor(colorName){
    if(document.all){
        try{
            var selection=textEditor.document.selection.createRange();
            selection.execCommand("backColor", true, colorName);
            textEditor.document.selection.createRange().collapse();
        }
        catch(error){
            alert(error.name + ": " + error.message + error.lineNumber);
        }
    }
    else{
        alert("this feature is not available for your browser now");  
    }
}

function setResizerTdPosition(){
    //document.getElementById("resizer").style.left= document.documentElement.clientWidth/4 +"px";
}

document.getElementById("iframeDiv").onmousemove=function(e){
    try{
        if(isResizeabilityActive){
            resizeFrame();    
        }    
    }
    catch(error){
        alert(error.name + ": " + error.message + error.lineNumber);
    }
}

document.body.onmousemove=function(e){
    if(isResizeabilityActive){
        resizeFrame();    
    }
}

function showAdvanceColorPicker(e) {
    e = e || window.event;
    setDispalyTo("ColorPickerDiv","hide");
    isColorPickerInAction=false;
    if(!isAdvanceColorPickerInAction){
        SetPosition(e,"advanceColorPickerDiv","Mouse");
        setDispalyTo("advanceColorPickerDiv","show");
    }
    else{
        setDispalyTo("advanceColorPickerDiv","hide");
    }
    (!isAdvanceColorPickerInAction)?isAdvanceColorPickerInAction=true:isAdvanceColorPickerInAction=false;
}

var newFrameHight;
resizeFrame=function(e){
    e=e||window.event;
   // previousFrameHight=parseInt(textEditorElement.style.height.replace("px",""));
   // newFrameHight=previousFrameHight+(e.clientY-mouseDownLocation);
   //document.getElementById("resizer").style.top=e.clientY+"px";
}
var elementActiveForMove=false;
var ContainerDiv;
function makeDragable(element){
    ContainerDiv =document.getElementById(element);
    ContainerDiv.onmousedown=mouseDown;
    ContainerDiv.onmouseup=mouseUp;
    document.body.onmousemove=mouseMove;
}

var cx,cy;
var mouseDown=function(e){
    e=e||window.event;
    elementActiveForMove=true;
    cx = e.clientX - ContainerDiv.offsetLeft;
    cy = e.clientY - ContainerDiv.offsetTop;
}

function mouseUp(){
    elementActiveForMove=false;
}

var mouseMove=function(e){
    try{
        e=e||window.event;
        if(elementActiveForMove && !unActivegeneratedControlForMove){
             ContainerDiv.style.top=(e.clientY-cy)+"px";
             ContainerDiv.style.left=(e.clientX-cx)+"px";
        }
    }
    catch(error){
        alert(error.name + ": " + error.message + error.lineNumber);
    }
}

function getRangeNode(win){
      var retrivedString="";
      checkCursor(usedFrame);
      if (window.getSelection){
        node = win.getSelection().anchorNode;
        var nodeStyleAttribute=node.parentNode.getAttributeNode("style").nodeValue.toString();
        //alert(nodeStyleAttribute);
        var nodeStyleAttributeChildes=nodeStyleAttribute.split(";");
        retrivedString=nodeStyleAttributeChildes;
//        xmlstring+="<nodesList>";
//        for(var i=0;i<nodeStyleAttributeChildes.length;i++){
//            xmlstring+="<nodeAttribute>";
//            xmlstring+=nodeStyleAttributeChildes[i];
//            xmlstring+="</nodeAttribute>";
//        }
//        xmlstring+="<nodesList>";
      }
      else if (win.document.selection){
        var range = win.document.selection.createRange();
        if (range){
            node = range.parentElement();
            //alert(node.parentNode.parentNode.parentNode.parentNode);
            nodesList="";
            retrivedString= "<nodesList>"+getParentNodesList(node)+"<nodeName>"+node.nodeName+"</nodeName>"+"</nodesList>";
        }
      }
  return retrivedString;
}
var nodeValue=null;
function  getParentNodesList(node){
    nodeValue=node.parentNode;
    nodesList+="<nodeName>"+nodeValue.nodeName+"</nodeName>";
    while(nodeValue!=null){
        try{
            getParentNodesList(nodeValue);
        }
        catch(e){
        
        }
    }
    //nodesList+="------>"+node.nodeName;
    return nodesList;
}

function getActiveButtons(){
    isColorPickerInAction=isAdvanceColorPickerInAction=false;
    var isBold=false;var isItalic=false;var isUnderline=false;
    try{
       if(document.all){
           var xmlDocument=StringtoXML(getRangeNode(window.frames["textEditor"]));
           var richTextElementsnodes=xmlDocument.documentElement.getElementsByTagName("nodeName");
           var NodesCount=richTextElementsnodes.length;
           for(var i=0;i<NodesCount;i++){
                var innerNodeValue=richTextElementsnodes[i].firstChild.nodeValue;
                //alert(innerNodeValue)
                switch(innerNodeValue){
                    case "STRONG":
                        isBold=true;
                        break;
                    case "EM":
                        isItalic=true;
                        break;
                    case "U":
                        isUnderline=true;
                        break;
                }
           }
       }
       else{
            var retrivedData=getRangeNode(textEditorElement.contentWindow);
            for(var i=0;i<retrivedData.length;i++){
                switch(removeSpaces(retrivedData[i])){
                    case "font-style:italic":
                        isItalic=true;
                        break;
                    
                    case "text-decoration:underline":
                        isUnderline=true;
                        break;
                    
                    case "font-weight:bold":
                        isBold=true;
                        break;
                }
            }
       }
   }
   catch(e){
        
   }
   setActivationStatus(isBold,isItalic,isUnderline);
}

function removeSpaces(string) {
    return string.split(' ').join('');
}

function setActivationStatus(isBold,isItalic,isUnderline){
   if(isBold&&isItalic&&isUnderline){
        document.getElementById("boldButton").setAttribute("class", "BoldButtons_on");
        document.getElementById("italic").setAttribute("class", "ItalicButtons_on");
        document.getElementById("underline").setAttribute("class", "UnderLineButtons_on");
   }
   else if(isBold&&isItalic&&!isUnderline){
        document.getElementById("boldButton").setAttribute("class", "BoldButtons_on");
        document.getElementById("italic").setAttribute("class", "ItalicButtons_on");
        document.getElementById("underline").setAttribute("class", "UnderLineButtons");
        isUnderlineOnActive=false;
        isItalicOnActive=true;
        isBoldOnActive=true;
   }
   else if(isBold&&isUnderline&&!isItalic){
        document.getElementById("boldButton").setAttribute("class", "BoldButtons_on");
        document.getElementById("italic").setAttribute("class", "ItalicButtons");
        document.getElementById("underline").setAttribute("class", "UnderLineButtons_on");
        isItalicOnActive=false;
        isUnderlineOnActive=true;
        isBoldOnActive=true;
   }
   else if(!isBold&&isUnderline&&isItalic){
        document.getElementById("boldButton").setAttribute("class", "BoldButtons");
        document.getElementById("italic").setAttribute("class", "ItalicButtons_on");
        document.getElementById("underline").setAttribute("class", "UnderLineButtons_on");
        isItalicOnActive=true;
        isUnderlineOnActive=true;
        isBoldOnActive=false;
   }
   else if(isBold&&!isItalic&&!isUnderline){
        document.getElementById("boldButton").setAttribute("class", "BoldButtons_on");
        document.getElementById("italic").setAttribute("class", "ItalicButtons");
        document.getElementById("underline").setAttribute("class", "UnderLineButtons");
        isItalicOnActive=false;
        isUnderlineOnActive=false;
        isBoldOnActive=true;
   }
   else if(!isBold&&!isItalic&&isUnderline){
        document.getElementById("boldButton").setAttribute("class", "BoldButtons");
        document.getElementById("italic").setAttribute("class", "ItalicButtons");
        document.getElementById("underline").setAttribute("class", "UnderLineButtons_on");
        isItalicOnActive=false;
        isBoldOnActive=false;
        isUnderlineOnActive=true;
   }
   else if(!isBold&&isItalic&&!isUnderline){
        document.getElementById("boldButton").setAttribute("class", "BoldButtons");
        document.getElementById("italic").setAttribute("class", "ItalicButtons_on");
        document.getElementById("underline").setAttribute("class", "UnderLineButtons");
        isUnderlineOnActive=false;
        isItalicOnActive=true;
        isBoldOnActive=true;
   }
   else{
        document.getElementById("boldButton").setAttribute("class", "BoldButtons");
        document.getElementById("italic").setAttribute("class", "ItalicButtons");
        document.getElementById("underline").setAttribute("class", "UnderLineButtons");
        isUnderlineOnActive=false;
        isItalicOnActive=false;
        isBoldOnActive=false;
   }
}

//Convert string to XML
function StringtoXML(text){
    var doc;
    if (window.ActiveXObject){
      doc=new ActiveXObject('Microsoft.XMLDOM');
      doc.async='false';
      doc.loadXML(text);
    } 
    else{
      parser=new DOMParser();
      doc=parser.parseFromString(text,"text/xml");
    }
    return doc;
}
setTimeout("loaded()", 500);

//                                      |---------------------------------------------------------|
//--------------------------------------|Developed By: Amir Jalilifard |Amir.Jalilifard@gmail.com |-----------------------------------------
//                                      |---------------------------------------------------------|

