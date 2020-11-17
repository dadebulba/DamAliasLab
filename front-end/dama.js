function f(){
    let elem=event.target;
    if(elem.tagName=="TD"){
        alert(elem.id);
        elem.innerHTML="<div class='piece black-piece'> </div>"
    }
    else {
        alert("gia occupato");
        elem.hidden=true;
    }
  }
