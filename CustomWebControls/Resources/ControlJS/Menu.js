function SetChildCheckBoxsChecked(eventobj,currnode)
{
  var i=0;
  for(i=0;i<currnode.childNodes.length;i++)
  {
    var objnode=currnode.childNodes[i];
    if(objnode.nodeName.toUpperCase()=="A")
    {
      var nxtsibnode = objnode.nextSibling;
      for (var j = 0; j < objnode.childNodes.length; j++) {
        if (objnode.childNodes[j].nodeName.toUpperCase() == "INPUT" && objnode.childNodes[j].type=="checkbox") 
        {
          objnode.childNodes[j].checked = eventobj.checked;
        }
      }
      if (nxtsibnode != null)
      {
        if(nxtsibnode.nodeName.toUpperCase()=="DIV")
        {
            SetChildCheckBoxsChecked(eventobj, nxtsibnode);
        }
      }
    }
  }
}

function SetChildCheckBoxsChecked1(eventobj, currnode) {
  var i = 0;
  var ul = currnode.childNodes[0].childNodes[0];
  for (i = 0; i < ul.childNodes.length; i++) {
    var objnode = ul.childNodes[i].childNodes[0];
    if (objnode.nodeName.toUpperCase() == "A") {
      var nxtsibnode = objnode.nextSibling;
      for (var j = 0; j < objnode.childNodes.length; j++) {
        if (objnode.childNodes[j].nodeName.toUpperCase() == "INPUT" && objnode.childNodes[j].type == "checkbox") {
          objnode.childNodes[j].checked = eventobj.checked;
        }
      }
      if (nxtsibnode != null) {
        if (nxtsibnode.nodeName.toUpperCase() == "DIV") {
          SetChildCheckBoxsChecked1(eventobj, nxtsibnode);
        }
      }
    }
  }
}