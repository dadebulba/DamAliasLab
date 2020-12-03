import {getID} from "./apiService.js";
let nameP1=document.getElementById("name-player1");
let nameP2=document.getElementById("name-player2");
let chessboard=document.getElementById("chessboard");
let movesList=document.getElementById("container-list");

let urlParam = new URLSearchParams(window.location.search); 
let IDpartita = urlParam.get('id');

//Gestire anche il caso nuova partita

if(IDpartita!="NULL"){
  let body=document.getElementById("body");
  body.onload=resumeGame;
}

async function resumeGame(){
  let response = await getID(IDpartita);
  let result = await response.json();  //oggetto partita  
  insertPlayerNames(result.namePlayer1, result.namePlayer2); 
  insertMoves(result.moves)
  insertBlackPawns(result.black);
  insertWhitePawns(result.white);
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
      elem.innerHTML=`<div class='piece white-piece' id="b${white[key].pawnId}"> </div>`;
    }
    else{ 
      elem.innerHTML=`<div class='piece white-piece' id="b${white[key].pawnId}"> O </div>`;
    }
  }
}

/*function insertBlackPawns(black){
  for(let r=0; r<=7; r++){ //ciclo per righe
    for(let c=0; c<=7; c++){  //ciclo per colonne
      if((r+c)%2==1){
        for(let key in black){
          if(black[key].position.row==r && black[key].position.column==c){
            let elem=document.getElementById(`a${r}${c}`);
            if(black[key].upgraded==false){
              elem.innerHTML=`<div class='piece black-piece' id="b${black[key].pawnId}"> </div>`;
            }
            else{ 
              elem.innerHTML=`<div class='piece black-piece' id="b${black[key].pawnId}"> O </div>`;
            }
          }
        }
      }
    }
  }
}



function newGame(){
    let w=0;
    let b=0;
    for(let r=0; r<=7; r++){ //ciclo per righe
      for(let c=0; c<=7; c++){  //ciclo per colonne
        if((r+c)%2==1){
          if(r<=2){
            let elem=document.getElementById(`a${r}${c}`);
            elem.innerHTML=`<div class='piece black-piece' id="b${b}"> </div>`;
            b++;
          }
          if(r>=5){
            let elem=document.getElementById(`a${r}${c}`);
            elem.innerHTML=`<div class='piece white-piece' id="w${w}"> </div>`;
            w++;
          }
        }
      }
    }
}






function f(){
  let target = event.target;
    
    let id=target.id;
    console.log(id); 
}*/