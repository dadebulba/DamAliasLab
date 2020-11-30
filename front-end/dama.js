function viewGamesTable(){
    elem=document.getElementById('container-tab');
    elem.style.display='flex';
}
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
