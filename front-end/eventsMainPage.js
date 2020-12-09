
import {getID, put, deleteLastMove} from "./apiService.js";
let nameP1=document.getElementById("name-player1");
let nameP2=document.getElementById("name-player2");
let chessboard=document.getElementById("chessboard");
let backBtn = document.getElementById("back-btn");
let selected=false;
let id=0; 

let urlParam = new URLSearchParams(window.location.search);
let IDpartita = urlParam.get('id');

if (IDpartita != "NULL") {
  let body = document.getElementById("body");
  body.onload = initializeGame;
}
else {
  window.location.href = "damaFirstPage.html";
}

function selectPawn() { 
  let target = event.target;
  if (target.classList.contains("piece")) {
    id = target.id;
    target.parentElement.style.backgroundColor = "red";
    chessboard.removeEventListener("click", selectPawn);
    chessboard.addEventListener("click", selectDest);
  }
  if (target.tagName == "TD") {
    alert("Devi selezionare una pedina")
  }
}

async function selectDest() {
  let target = event.target;
  if(target.classList.contains("piece")){
    alert("Mossa non valida");
    //annulla selezione
    document.getElementById(id).parentElement.style.backgroundColor = "#8997a9";
    chessboard.addEventListener("click", selectPawn);
    chessboard.removeEventListener("click", selectDest);
  }
  if (target.tagName == "TD") {
    let dest = target.id;
    document.getElementById(id).parentElement.style.backgroundColor = "#8997a9";

    let obj = {
      target: {
        id: id.substring(1,),
        color: id[0]=="w" ? 0 : 1,  //da chiedere cosa significa 0 e 1 (bianco nero)
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
    movePawn(response);
    updateMoves(response.moves);//lo faccio meglio quando so struttura definitiva mosse
    insertPoints(response.pointsWhite, response.pointsBlack);
    viewTurn(response.turn);

    
    chessboard.addEventListener("click", selectPawn);
    chessboard.removeEventListener("click", selectDest);
    if(document.getElementById("moves-list").childElementCount > 0){
      backBtn.style.display = "block";
    }
  }
}

async function revertLastMove() {
  let response = await deleteLastMove();
  if(response != null){
    let game = response; //await response.json();
    movePawn(game);
    insertPoints(game.pointsWhite, game.pointsBlack);
    viewTurn(game.turn);
    document.getElementById("moves-list").children[0].remove();
    if(game.moves.length == 0){
      backBtn.style.display = "none";
    }
  }
}

function updateMoves(moves){
  let li = document.createElement('li');
  let length=moves.length;
  li.innerHTML=`${moves[length-1]}`;
  document.getElementById("moves-list").prepend(li);
}

function movePawn(partita){
  //document.getElementById(`${move.pawn.color=="WHITE" ? "w" : "b"}${move.pawn.id}`).parentElement.innerHTML="";
  clearChessboard();
  insertBlackPawns(partita.black);
  insertWhitePawns(partita.white);
}

async function initializeGame(){
  let response = await getID(IDpartita);
  if(response != null){

  let result = await response.json();  //oggetto partita  
  
  chessboard.addEventListener("click", selectPawn);
  backBtn.addEventListener('click', revertLastMove);
  insertPlayerNames(result.namePlayer1, result.namePlayer2);
  insertMoves(result.moves); //da modificare quando so la struttura
  insertBlackPawns(result.black);
  insertWhitePawns(result.white);
  insertPoints(result.pointsWhite, result.pointsBlack);
  viewTurn(result.turn);
  }
  else {
    window.location.href = "damaFirstPage.html";
  }
}
  
function viewTurn(turn){
  if(turn==1){
    document.getElementById("turn-box").innerHTML=`TOCCA A: <div id="turn-label" class='white-label'> </div>`;
  }
  else{
    document.getElementById("turn-box").innerHTML=`TOCCA A: <div id="turn-label" class='black-label'> </div>`;
  }

}
function insertPoints(black, white){
  document.getElementById("black-points").innerHTML= `&nbsp&nbsp${black}`;
  document.getElementById("white-points").innerHTML= `&nbsp&nbsp${white}`;
}
function insertPlayerNames(namePlayer1, namePlayer2){
    nameP1.innerHTML=`<div class='label black-label'> </div> ${namePlayer1}: <div id="black-points"> </div>`;
    nameP1.style.fontSize="20px";
    nameP2.innerHTML=`<div class='label white-label'> </div> ${namePlayer2}: <div id="white-points"> </div>`;
    nameP2.style.fontSize="20px";
}

function insertMoves(movesArray){
  for(let key in movesArray){
    let li = document.createElement('li');
    li.innerHTML=`${movesArray[key]}`;
    document.getElementById("moves-list").prepend(li);
  }
}

function insertBlackPawns(black) {
  for (let key in black) {
    let row = black[key].position.row;
    let column = black[key].position.column;
    let elem = document.getElementById(`a${row}${column}`);
    if (black[key].upgraded == false) {
      elem.innerHTML = `<div class='piece black-piece' id="b${black[key].pawnId}"> </div>`;
    }
    else{ 
      elem.innerHTML=`<div class='piece black-upgraded' id="b${black[key].pawnId}"> </div>`;
    }
  }
}
function insertWhitePawns(white) {
  for (let key in white) {
    let row = white[key].position.row;
    let column = white[key].position.column;
    let elem = document.getElementById(`a${row}${column}`);
    if (white[key].upgraded == false) {
      elem.innerHTML = `<div class='piece white-piece' id="w${white[key].pawnId}"> </div>`;
    }
    else{ 
      elem.innerHTML=`<div class='piece white-upgraded' id="w${white[key].pawnId}"> </div>`;
    }
  }
}
function clearChessboard(){
  for(let r=0; r<=7; r++){ //ciclo per righe
    for(let c=0; c<=7; c++){  //ciclo per colonne
      if((r+c)%2==1){
          let elem=document.getElementById(`a${r}${c}`);
          elem.innerHTML="";
      }
    }
  }
}

/*
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
}*/
