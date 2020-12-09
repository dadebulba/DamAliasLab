
import {getID, put} from "./apiService.js";
let nameP1=document.getElementById("name-player1");
let nameP2=document.getElementById("name-player2");
let chessboard=document.getElementById("chessboard");
let id=0; 

let urlParam = new URLSearchParams(window.location.search);
let IDpartita = urlParam.get('id');

if (IDpartita != "NULL") {
  let body = document.getElementById("body");
  body.onload = initializeGame;
}

chessboard.addEventListener("click", selectPawn);


function selectPawn() { 
  let target = event.target;
  if (target.tagName == "TD") {
    alert("Seleziona una pedina!");
  }
  else {
    id = target.id;
    target.parentElement.style.backgroundColor = "red";
    chessboard.removeEventListener("click", selectPawn);
    chessboard.addEventListener("click", selectDest);
  }
}

async function selectDest() {
  let target = event.target;
  let dest;
  if(target.classList.contains("piece")){
    dest = target.parentElement.id;
  }
  if (target.tagName == "TD") {
    dest = target.id;
  }
  document.getElementById(id).parentElement.style.backgroundColor = "#8997a9";
    let obj = {
      target: {
        pawnId: Number(id.substring(1,)),
        color: id[0]=="w" ? 0 : 1, //0 white, 1 black
      },
      to: {
        row: Number(dest[1]),
        column: Number(dest[2])
      }
    }
    
    let response = await put(IDpartita, obj);

    if(response!=null){
      let result = await response.json();
      movePawn(result);
      updateMoves(result.moves);
      insertPoints(result.pointsWhite, result.pointsBlack);
      viewTurn(result.turn);
    }

    chessboard.addEventListener("click", selectPawn);
    chessboard.removeEventListener("click", selectDest);
}

function updateMoves(moves){
  let li = document.createElement('li');
  let length=moves.length;
  li.innerHTML=`${moves[length-1].target.color==1 ? "BLACK" : "WHITE"}
  from: [${moves[length-1].from.row},${moves[length-1].from.column}]
  to: [${moves[length-1].to.row},${moves[length-1].to.column}]`;
  document.getElementById("moves-list").prepend(li);

}
function movePawn(partita){
  clearChessboard();
  insertBlackPawns(partita.black);
  insertWhitePawns(partita.white);
}

async function initializeGame(){
  let response = await getID(IDpartita);
  let result = await response.json(); 
  insertPlayerNames(result.namePlayer1, result.namePlayer2);
  insertMoves(result.moves);
  insertBlackPawns(result.black);
  insertWhitePawns(result.white);
  insertPoints(result.pointsBlack, result.pointsWhite);
  viewTurn(result.turn);
}

function viewTurn(turn){
  if(turn==0){
    document.getElementById("turn-box").innerHTML=`TOCCA A: <div id="turn-label" class='white-label'> </div>`;
  }
  else{
    document.getElementById("turn-box").innerHTML=`TOCCA A: <div id="turn-label" class='black-label'> </div>`;
  }
}
function insertPoints(black, white){
  document.getElementById("black-points").innerHTML= `&nbsp&nbsp${black*25}`;
  document.getElementById("white-points").innerHTML= `&nbsp&nbsp${white*25}`;
}
function insertPlayerNames(namePlayer1, namePlayer2){
    nameP1.innerHTML=`<div class='label black-label'> </div>${namePlayer1}: <div id="black-points"> </div>`;
    nameP1.style.fontSize="20px";
    nameP2.innerHTML=`<div class='label white-label'> </div> ${namePlayer2}: <div id="white-points"> </div>`;
    nameP2.style.fontSize="20px";
}

function insertMoves(movesArray){
  for(let key in movesArray){
    let li = document.createElement('li');
    li.innerHTML=`${movesArray[key].target.color==1 ? "BLACK" : "WHITE"}
    from: [${movesArray[key].from.row},${movesArray[key].from.column}]
    to: [${movesArray[key].to.row},${movesArray[key].to.column}]`;
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
      elem.innerHTML=`<div class='piece black-piece upgraded' style="color:red" id="b${black[key].pawnId}"> &#x265b </div>`;
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
      elem.innerHTML=`<div class='piece white-piece upgraded' style="color:red"  id="w${white[key].pawnId}"> &#x265b </div>`;
    }
  }
}
function clearChessboard(){
  for(let r=0; r<=7; r++){ 
    for(let c=0; c<=7; c++){  
      if((r+c)%2==1){
          let elem=document.getElementById(`a${r}${c}`);
          elem.innerHTML="";
      }
    }
  }
}
