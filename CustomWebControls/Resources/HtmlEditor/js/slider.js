function $(id){ return(document.getElementById(id)); }

function getTargetElement(evt) {
    return (evt.target) ? ((evt.target.nodeType == 3) ? evt.target.parentNode : evt.target) : evt.srcElement;
}

Function.prototype.bind = function(obj) {
    var _method = this;
    return function() {
        return _method.apply(obj, arguments);
    };    
} 

function slider(){
    this.style = {
     backgroundColor:"#828282"
    ,height:"4px"
    ,width:"300px"
    ,borderBottom:"#cfcfcf 1px solid"
    ,borderRight:"#cfcfcf 1px solid"
};
this.pointer = {
top: "0px"
,width:"6px"
,height:"12px"
,backgroundColor:"#008c00"
,step:1
};
this.values = {
min:400
,mid:550
,max:700
,fontSize:"14px"
};
};
var isResizeabilityActive=false;
slider.prototype = { 

	init : function(settings) {
		
		for(var i in settings){
		    if (typeof(this[i])=="undefined") {
		        this[i] = settings[i];
		    }
		    else
		    for(var j in settings[i]){
    		     //if (typeof(this[i][j])=="undefined")
    		            this[i][j]=settings[i][j]; 
    		               		    
		     }				
		}
						
		this.createslider();
		this._event_docMouseMove = this._docMouseMove.bind(this);		
		this._event_docMouseUp = this._docMouseUp.bind(this);		
		this.point.onmousedown = this._mouseDown.bind(this);
		this.slider_bar.onclick = this._sliderClick.bind(this);
		//this.slider_bar.onclick 	
		if (this.fire)
		    this.addEvent(this.slider_bar, 'click', this.fire);	 
	},
	
	createslider:function(){
		var $ = document.getElementById(this.id)
		$.appendChild(this.valuebox());
				
		this.slider_bar = this.sliderbar();		
		this.colorbar =  this.setcolorbar();		
		if (this.pointer.isarrow ==null) {
		this.point = this.setpoint();}
		else{
		this.point = this.arrows();
		}	
			
		this.slider_bar.appendChild(this.colorbar);
		this.slider_bar.appendChild(this.point);
		var div = document.createElement('div');
		div.style.clear="both";
		$.appendChild(div);
		$.appendChild(this.slider_bar);
		
		if (this.hassteparrows) {
		var divfloat = (document.all)  ? "styleFloat" : "cssFloat";
		this.slider_bar.style[divfloat] ="left";
		var step = this.steparrows();
		$.appendChild(step);
		this.addEvent(this.leftarrow,"mouseover",this.hightlight.bind(this));
        this.addEvent(this.leftarrow,"mouseout",this.hightlight.bind(this));
        
        this.addEvent(this.point,"mouseover",this.hightlight.bind(this));
        this.addEvent(this.point,"mouseout",this.hightlight.bind(this));
        
        
		this.addEvent(this.rightarrow,"mouseover",this.hightlight.bind(this));
        this.addEvent(this.rightarrow,"mouseout",this.hightlight.bind(this));
        
        if ((this.fire)&&(document.all)) { 
		this.addEvent(this.leftarrow,"click",this.fire);
		this.addEvent(this.rightarrow,"click",this.fire);
		}
        
        this.addEvent(this.leftarrow,"click",this.setArrowClick.bind(this));
		this.addEvent(this.rightarrow,"click",this.setArrowClick.bind(this));
		
		if ((this.fire)&&(!document.all)) { 
		this.addEvent(this.leftarrow,"click",this.fire);
		this.addEvent(this.rightarrow,"click",this.fire);
		
		}
		
		}
	   
	},
	
	steparrows:function () {
	var div = this.divleft();
	div.style.marginLeft="15px";
	div.style.marginTop ="-5px";
	div.style.width = "50px";
	var divl = this.divleft();
	divl.style.width = "20px";	
	divl.style.marginRight = "5px";
	this.leftarrow = this.arrowleftright(18,18,"left","#cfcfcf");
	
	divl.appendChild(this.leftarrow);
	
	divr = this.divleft();
	divr.style.width = "25px";
	this.rightarrow = this.arrowleftright(18,18,"right","#cfcfcf")
	divr.appendChild(this.rightarrow);
	
	div.appendChild(divl);
	div.appendChild(divr);
	 
	return div;	
	},
	
	hightlight: function (e) {
    e = (e) ? e : ((window.event) ? window.event : "");
    var o = getTargetElement(e);

    var v = (o ==this.leftarrow)||(o==this.rightarrow) || (o==this.point) ? o : o.parentNode;
    
    var d = v.childNodes;
    if (e.type == "mouseover") {
        for (var i=0;i<d.length;i++) {
        d[i].style.backgroundColor = "#858585";
        }
    }
    else if (e.type == "mouseout") {
        for (var i=0;i<d.length;i++) {
        if ((o==this.point)||(o.parentNode==this.point)) d[i].style.backgroundColor = this.pointer.backgroundColor;
        else d[i].style.backgroundColor = "#cfcfcf";
        }
    }
     
    } ,
	divleft: function() {
	var div = document.createElement('div');
	var divfloat = (document.all)  ? "styleFloat" : "cssFloat";
	div.style[divfloat] ="left";
	
	return div;
	},
	
	valuebox:function () {
		var div = document.createElement('div');
		div.style.width = this.style.width;		
		div.style.fontSize =  this.values.fontSize;
		this.min = div.cloneNode(false);
		
		var divfloat = (document.all)  ? "styleFloat" : "cssFloat";
		this.min.style[divfloat] ="left";
		
		this.mid = this.min.cloneNode(false);
		this.display = this.mid;
		this.max = document.createElement('div');
		this.max.style[divfloat]="right";
		if (!this.unit)	this.unit="";	
		this.min.innerHTML = this.values.min+this.unit;
		this.mid.innerHTML = this.values.mid+this.unit;
		this.max.innerHTML = this.values.max+this.unit;
		this.min.style.width = 2/5*parseInt(this.style.width)+"px";
		this.mid.style.width = 2/5*parseInt(this.style.width)+"px";
		
		div.appendChild(this.min);
		div.appendChild(this.mid);
		div.appendChild(this.max);
		return div;
	},
	
	sliderbar: function () {
		var div = document.createElement('div');		
		div.style.position = "relative";
		div.style.backgroundColor = this.style.backgroundColor;
		div.style.height = this.style.height;
		div.style.width = this.style.width;
		div.style.fintSize = "1px";		
		div.style.borderBottom = this.style.borderBottom;
		div.style.borderRight = this.style.borderRight;
		
		return div;
	},
	 
    arrowleftright:function (w,h,up,bkcl) {
    var par = document.createElement('div');
    par.style.width = w+"px";
    t0 = this.divnode();
    var l = parseInt(h/2);
    for (var i=l-1;i>0;i--) {
	    t = t0.cloneNode(false);   	
        t.style.backgroundColor = bkcl;
            	
	    if (up=="right") {
	        t.style.marginLeft = 0+"px";
	        t.style.marginRight =w/l*i+"px";}
    	
	    else{
	        t.style.marginRight = 0+"px";
	        t.style.marginLeft =w/l*i+"px";
	    }
	    par.appendChild(t);
	}
	
	for (var i=1;i<l+1;i++) {
	    t = t0.cloneNode(false);
        t.style.backgroundColor = bkcl;
    
	    if (up=="right") {
	        t.style.marginLeft = 0+"px";
	        t.style.marginRight =w/l*i+"px";}
    	
	    else{
	        t.style.marginRight = 0+"px";
	        t.style.marginLeft =w/l*i+"px";
	    }
	    par.appendChild(t);

	}
    return par;
},
    
    arrows: function () {
        var w =parseInt(this.pointer.width),h=parseInt(this.pointer.height);
        var par = this.setpoint();        
        par.style.backgroundColor = "transparent";
        par.style.top = parseInt(this.style.height)+"px";
        t0 = this.divnode();
        t0.style.backgroundColor =this.pointer.backgroundColor;
        par.appendChild(t0);
        for (var i=1;i<h;i++) {
            t = t0.cloneNode(false);
            t.style.backgroundColor = this.pointer.backgroundColor;
            t.style.marginLeft = 1/2*w/h*i+"px";
            t.style.marginRight = 1/2*w/h*i+"px";
            par.appendChild(t);

        }
        return this.reverseNodes(par);      

},

    divnode: function () {
        var o=document.createElement("div");
        o.style.height = "1px";
        o.style.overflow="hidden";
        return o;
    },

    reverseNodes: function (n) { 
       
        var kids = n.childNodes;
       
        var rekids = n.cloneNode(false);
        var numkids = kids.length;
        for(var i = numkids-1; i >= 0; i--) {       
         
            rekids.appendChild(kids[i]);
           
        }
     return rekids;
    },
    
  

	setcolorbar: function() {
	
		var div = document.createElement('div');	
		div.style.width= parseInt((this.values.mid-this.values.min)/(this.values.max-this.values.min)*parseInt(this.style.width)) + "px";
		div.style.backgroundColor = this.pointer.backgroundColor;
		div.style.height = this.style.height;
		div.style.overflow = "hidden";
		
		return div;
	},
	changeunit:function(ut) {
	this.unit = ut;
	this.mid.innerHTML = this.values.mid+ut;
	this.min.innerHTML = this.values.min+ut;
	this.max.innerHTML = this.values.max+ut;
	},
	 setpoint: function() {
	 	
		var div = document.createElement('div');
		div.style.width = this.pointer.width;
		div.style.backgroundColor = this.pointer.backgroundColor;
		div.style.height = this.pointer.height;
		div.style.overflow = "hidden";		
		div.style.position = "absolute";		
		div.style.top = this.pointer.top;
		div.style.left= parseInt((this.values.mid-this.values.min)/(this.values.max-this.values.min)*parseInt(this.style.width)) - parseInt(this.pointer.width)/2  + "px";
		 return div;
	 },
	_sliderClick: function(e){
	e = (e) ? e : ((window.event) ? window.event : "");
	var o = getTargetElement(e);
	if (o!=this.point) {
	  this.setValuesClick(e);
	   
   }
		    	
		this.noBubbleDefault(e);
	
	}, 
	_mouseDown: function(e) {
	    isResizeabilityActive=true;
		this.addEvent(document, 'mousemove', this._event_docMouseMove);
		if (this.fire)
		    this.addEvent(document, 'mousemove', this.fire);		
		this.addEvent(document, 'mouseup', this._event_docMouseUp);
		this.noBubbleDefault(e);
		
		
	},
	 _docMouseMove: function(e) {
		this.setValuesClick(e)	
		this.noBubbleDefault(e);
		
	   
},

_docMouseUp: function(e) {
isResizeabilityActive=false;
this.removeEvent(document, 'mouseup', this._event_docMouseUp);
this.removeEvent(document, 'mousemove', this._event_docMouseMove);
if (this.fire) {
  this.removeEvent(document, 'mousemove', this.fire);}
if (this.onstop)  this.onstop(); 
 
this.noBubbleDefault(e);


},
setValuesClick: function(e){ 
	var x = e.clientX+document.documentElement.scrollLeft;	
	var offsetX = this.getPosition(this.slider_bar)[0];	
	//var x_x = x - offsetX - parseInt(this.pointer.width)/2
	var x_x = x - offsetX
    this.setPosition(x_x);	
    
 	
},

setArrowClick: function(e){

    e = (e) ? e : ((window.event) ? window.event : "");
	var o = getTargetElement(e);
	var step = (o==this.rightarrow)||(o.parentNode ==this.rightarrow) ? this.pointer.step : -this.pointer.step;
	var mid = this.values.mid + step; 
	if (mid>this.values.max) {
	mid = this.values.max;	
	}else if (mid<this.values.min) {
	mid = this.values.min;
	}
	
    var x_x = parseInt((mid - this.values.min)*parseInt(this.style.width)/(this.values.max - this.values.min));
    this.colorbar.style.width= x_x + "px"; 
    this.point.style.left = x_x - parseInt(this.pointer.width)/2 +"px";
    this.values.mid = mid;
    this.display.innerHTML = mid+this.unit;
    if (this.onstop)  this.onstop(); 

    
},

setPosition: function(x) {
    if (x>parseInt(this.style.width)) {
	 x = parseInt(this.style.width); 
    }	
	else if (x<0) {x = 0;this.point.style.left = x + "px";
	} 
				
				
	this.colorbar.style.width= x + "px"; 
 	this.point.style.left = x - parseInt(this.pointer.width)/2 +"px";
 	this.values.mid = parseInt( x/parseInt(this.style.width)*(this.values.max - this.values.min) + this.values.min)
 	this.display.innerHTML = this.values.mid+this.unit;
 	if(isResizeabilityActive){
 	    var radio=document.getElementsByName("resizerRBT");
 	    var selectedRadio;
 	    for(var i=0;i<radio.length;i++){
 	       if(radio[i].checked==true){
 	            selectedRadio=radio[i].value;
 	        }
        }
        switch(selectedRadio){
            case "both":
                document.getElementById("textEditor").style.height=this.values.mid+"px";
                document.getElementById("sourceTxt").style.height=this.values.mid+"px";
                document.getElementById("textEditor").style.width=this.values.mid+"px";
                document.getElementById("sourceTxt").style.width=this.values.mid+"px";
            break;
            case "vertical":
            //alert(document.getElementById("textEditor").style.width);
                document.getElementById("textEditor").style.width=this.values.mid+"px";
                document.getElementById("sourceTxt").style.width=this.values.mid+"px";
            break;
            case "horizontal":
                document.getElementById("textEditor").style.height=this.values.mid+"px";
                document.getElementById("sourceTxt").style.height=this.values.mid+"px";
            break;
        }
    }
},

noBubbleDefault:function (e)
{

	if (e && e.stopPropagation) {
	 
		e.stopPropagation();
		e.preventDefault();
		 
		}
	else{
		window.event.cancelBubble = true;	
        window.event.returnValue = false;} // IE	
},

addEvent: function ( obj, type, fn ) {
  if ( obj.attachEvent ) {
    obj["e"+type+fn] = fn;
    obj[type+fn] = function() { obj["e"+type+fn]( window.event ); }
    obj.attachEvent( "on"+type, obj[type+fn] );
  } else
    obj.addEventListener( type, fn, false );
},
	
removeEvent : function( obj, type, fn ) {
      if ( obj.detachEvent ) {
        obj.detachEvent( "on"+type, obj[type+fn] );
		    obj[type+fn] = null;
      } else
        obj.removeEventListener( type, fn, false );
},


getPosition: function(el) {   
    var curLeft = 0,curTop = 0, s = this.getScrolls(el);   
  
    if (el.offsetParent) {   
        do {   
            curLeft += el.offsetLeft;          
            curTop += el.offsetTop;   
        } while (el = el.offsetParent);         
        return [ (curLeft - s[0]), (curTop - s[1]) ];   
    }   
},
 
getScrolls: function(el) {  
    var curX = 0, curY = 0;   
    do {   
        curX += el.scrollLeft;   
        curY += el.scrollTop;   
        if (el.nodeName == 'BODY')   
            break;   
    } while (el = el.parentNode);   
    return [curX, curY];   
}

}
resizeFrame=function(e){
    e=e||window.event;
    previousFrameHight=parseInt(document.getElementById("textEditor").style.height.replace("px",""));
    newFrameHight=previousFrameHight+(e.clientY-mouseDownLocation);
    document.getElementById("resizer").style.top=e.clientY+"px";
}
function event5(){
    alert("slider");
}

setTimeout("loaded()", 500);
