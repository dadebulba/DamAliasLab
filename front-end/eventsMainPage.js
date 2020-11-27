let nameP1=document.getElementById("name-player1");
let nameP2=document.getElementById("name-player2");
let newgame=0;

let body=document.getElementById("body");
if(newgame==1){
    body.onload=newGame;
}

let chessboard=document.getElementById("chessboard");
//chessboard.addEventListener("click", f);



  
/*function insertName(){
    nameP1.innerHTML=`<div class='label white-piece'> </div> : 500`;
    nameP2.innerHTML=`<div class='label white-piece'> </div> : 500`;
}*/

function newGame(){
    let w=12;
    let b=1;
    for(let r=1; r<=8; r++){ //ciclo per righe
      for(let c=1; c<=8; c++){  //ciclo per colonne
        if((r+c)%2==1 ){
          if(r<=3){
            let elem=document.getElementById(`a${r}${c}`);
            elem.innerHTML=`<div class='piece black-piece' id="p${b}"> </div>`;
            b++;
          }
          if(r>=6){
            let elem=document.getElementById(`a${r}${c}`);
            elem.innerHTML=`<div class='piece white-piece' id="p${w}"> </div>`;
            w--;
          }
        }
      }
    }
}
