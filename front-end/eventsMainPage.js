import {getID, put} from "./apiService.js";
let nameP1=document.getElementById("name-player1");
let nameP2=document.getElementById("name-player2");
let chessboard=document.getElementById("chessboard");
let movesList=document.getElementById("container-list");
let selected=false;
let id=0;

let urlParam = new URLSearchParams(window.location.search); 
let IDpartita = urlParam.get('id');

if(IDpartita!="NULL"){
  let body=document.getElementById("body");
  body.onload=initializeGame;
}

chessboard.addEventListener("click", selectPawn);

function selectPawn(){
  let target = event.target;
  if(target.classList.contains("piece")){
    id=target.id;
    target.parentElement.style.backgroundColor="red";
    chessboard.removeEventListener("click", selectPawn);
    chessboard.addEventListener("click", selectDest);
  }
  if(target.tagName=="TD"){
    alert("Devi selezionare una pedina")
  }
}

async function selectDest(){
  let target = event.target;
  if(target.classList.contains("piece")){
    alert("Devi selezionare una casella vuota");
    //annulla selezione
    document.getElementById(id).parentElement.style.backgroundColor="#8997a9";
    chessboard.addEventListener("click", selectPawn);
    chessboard.removeEventListener("click", selectDest);
  }
  if(target.tagName=="TD"){
    let dest=target.id;
    document.getElementById(id).parentElement.style.backgroundColor="#8997a9";
    
    let obj = {
      pawn: {
        id: id.substring(1,),
        color: id[0]=="w" ? "WHITE" : "BLACK",
      },
      to: {
        row: dest[1], 
        column: dest[2]
      }
    }
    console.log(obj);
    let response = await put(IDpartita, obj);
    let result = await response.json();
    /*
      creo oggetto da mandare con la put 
      SE LA MOSSA NON è VALIDA GESTISCO L'ERRORE
      SE LA MOSSA è VALIDA MI TORNA OGGETTO PARTITA, TOLGO DALLA CHESSBOARD LA PEDINA CON ID SELEZIONATA,
      GUARDO DOVE SI TROVA LA PEDINA CON ID SELEZIONATA(POSITION) E LA VADO AD INSERIRE IN QUELLA POSTAZIONE
      DELLA CHESSBOARD CON ID GIUSTO E GUARDO SE è UPRGRADED
      AGGIORNA MOSSE
      AGGIORNA PUNTEGGIO
    */
    //per cancellare pedina con id
    //document.getElementById(id).hidden=true;
    //oppure  document.getElementById(id).parentElement.innerHTML="";
    //document.getElementById(dest).innerHTML= `<div class='piece black-piece' id="b${id}"> </div>`;
    
    chessboard.addEventListener("click", selectPawn);
    chessboard.removeEventListener("click", selectDest);
  }  
}

async function initializeGame(){
  let response = await getID(IDpartita);
  let result = await response.json();  //oggetto partita  
  insertPlayerNames(result.namePlayer1, result.namePlayer2); //visualizzare anche punteggio
  insertMoves(result.moves); //da modificare quando so la struttura
  insertBlackPawns(result.black);
  insertWhitePawns(result.white);
  //visualizzare punteggio
  //visualizzare turno
}
  

function insertPlayerNames(namePlayer1, namePlayer2){
    nameP1.innerHTML=`<div class='label black-piece'> </div> ${namePlayer1} : 0`;
    nameP1.style.fontSize="20px";
    nameP2.innerHTML=`<div class='label white-piece'> </div> ${namePlayer2} : 0`;
    nameP2.style.fontSize="20px";
}

function insertMoves(movesArray){
  let ul = document.createElement('ul');
  for(let key in movesArray){
    let li = document.createElement('li');
    li.innerHTML=`${movesArray[key]}`;
    ul.prepend(li);
  }
  movesList.prepend(ul);
}

function insertBlackPawns(black){
  for(let key in black){
    let row=black[key].position.row;
    let column=black[key].position.column;
    let elem=document.getElementById(`a${row}${column}`);
    if(black[key].upgraded==false){
      elem.innerHTML=`<div class='piece black-piece' id="b${black[key].pawnId}"> </div>`;
    }
    else{ 
      elem.innerHTML=`<div class='piece black-piece' id="b${black[key].pawnId}"> O </div>`;
    }
  }
}
function insertWhitePawns(white){
  for(let key in white){
    let row=white[key].position.row;
    let column=white[key].position.column;
    let elem=document.getElementById(`a${row}${column}`);
    if(white[key].upgraded==false){
      elem.innerHTML=`<div class='piece white-piece' id="w${white[key].pawnId}"> </div>`;
    }
    else{ 
      elem.innerHTML=`<div class='piece white-piece' id="w${white[key].pawnId}"> O </div>`;
    }
  }
}

function f(){
  let target = event.target;
    
    let id=target.id;
    console.log(id); 
}