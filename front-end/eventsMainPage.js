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
    alert("Mossa non valida");
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

    let response = await put(IDpartita, obj);
    //let result = await response.json();
    /*
      SE LA MOSSA NON è VALIDA GESTISCO L'ERRORE
      ----> SE LA MOSSA è VALIDA MI TORNA OGGETTO PARTITA, (MOCKATA)
    */
    movePawn(obj,response);
    updateMoves(response.moves);//lo faccio meglio quando so struttura definitiva mosse
    //updatePoints();  //lo faccio quando aggiorna partita mettendoci anche points

    //cambia turno

    
    chessboard.addEventListener("click", selectPawn);
    chessboard.removeEventListener("click", selectDest);
  }  
}

function updateMoves(moves){
  let li = document.createElement('li');
  let length=moves.length;
  li.innerHTML=`${moves[length-1]}`;
  document.getElementById("moves-list").prepend(li);

}
function movePawn(move, partita){
  if(move.pawn.color=="WHITE"){
    for(let key in partita.white){
      if (partita.white[key].pawnId==move.pawn.id){
        document.getElementById(`w${move.pawn.id}`).parentElement.innerHTML="";
        let dest=document.getElementById(`a${partita.white[key].position.row}${partita.white[key].position.column}`);
        if(partita.white[key].upgraded==false){
          dest.innerHTML=`<div class='piece white-piece' id="w${partita.white[key].pawnId}"> </div>`;
        }
        else{
          dest.innerHTML=`<div class='piece white-upgraded' id="w${partita.white[key].pawnId}"> </div>`; 
        }
      }
    }
  }
  if(move.pawn.color=="BLACK"){
    for(let key in partita.black){
      if (partita.black[key].pawnId==move.pawn.id){
        document.getElementById(`b${move.pawn.id}`).parentElement.innerHTML="";
        let dest=document.getElementById(`a${partita.black[key].position.row}${partita.black[key].position.column}`);
        if(partita.black[key].upgraded==false){
          dest.innerHTML=`<div class='piece black-piece' id="b${partita.black[key].pawnId}"> </div>`;
        }
        else{
          dest.innerHTML=`<div class='piece black-upgraded' id="b${partita.black[key].pawnId}"> O </div>`;  
        }
      }
    }
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
  
function insertPlayerNames(namePlayer1, namePlayer2){//anche punteggio come parametro
    nameP1.innerHTML=`<div class='label black-label'> </div> ${namePlayer1} : ${1}`;
    nameP1.style.fontSize="20px";
    nameP2.innerHTML=`<div class='label white-label'> </div> ${namePlayer2} : ${2}`;
    nameP2.style.fontSize="20px";
}

function insertMoves(movesArray){
  for(let key in movesArray){
    let li = document.createElement('li');
    li.innerHTML=`${movesArray[key]}`;
    document.getElementById("moves-list").prepend(li);
  }
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
      elem.innerHTML=`<div class='piece black-upgraded' id="b${black[key].pawnId}"> O </div>`;
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
      elem.innerHTML=`<div class='piece white-upgraded' id="w${white[key].pawnId}"> </div>`;
    }
  }
}