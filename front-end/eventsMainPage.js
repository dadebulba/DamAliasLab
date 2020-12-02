let nameP1=document.getElementById("name-player1");
let nameP2=document.getElementById("name-player2");


let body=document.getElementById("body");
body.onload=newGame;

let chessboard=document.getElementById("chessboard");
chessboard.addEventListener("click", f);



  
/*function insertName(){
    nameP1.innerHTML=`<div class='label white-piece'> </div> : 500`;
    nameP2.innerHTML=`<div class='label white-piece'> </div> : 500`;
}*/

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
  
}