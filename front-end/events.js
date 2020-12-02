import {getGame, postGame, getID} from "./apiService.js";

let newgame=0;
let firstTimeGet=true;
let resumegameButton=document.getElementById('resume');
let newgameButton=document.getElementById('new');
let form=document.getElementById('form');
let conteinerTab=document.getElementById('container-tab');
let tableGames=document.getElementById('table-games');
let chessBoard=document.getElementById('chessboard');


resumegameButton.addEventListener('click', viewTableGames);
newgameButton.addEventListener('click', viewForm );
tableGames.addEventListener('click', gameChosen);
form.addEventListener('submit', sendForm);


//chessBoard.addEventListener('click', ciao);



function viewForm(){
    form.style.display='flex'; //view form 
    conteinerTab.style.display='none';  //hide table
}

async function viewTableGames(){
    let response = await getGame();
    let obj = await response.json(); // read response body and parse as JSON
    if(firstTimeGet){    
        for(let key in obj){
            let tr = document.createElement('tr');
            tr.className = `rows`;
            tr.innerHTML=`<td> ${obj[key].id} </td> <td>${obj[key].namePlayer1}-${obj[key].namePlayer2}</td> <td><button class="lastColumn" id="${obj[key].id}" >play!</button></td>`;
            let heading=document.getElementById("heading");
            heading.after(tr);
        }
        firstTimeGet=false;
    }
    conteinerTab.style.display='flex'; //view table 
    form.style.display='none'; //hideForm
}

async function gameChosen(){
    let target = event.target;  // where was the click
    if(target.className=="lastColumn"){  //only if click on play button
        let id=target.id;
        let response = await getID(id);
        let result = await response.json();  //oggetto partita  
        console.log(result);
        
        //location.href="./damaMainPage.html";
        
    }
}

async function sendForm (evt){

    evt.preventDefault();

    let p1 = document.getElementById("player1");
    let p2 = document.getElementById("player2");
    let data = {
        nameOfPlayer1: p1.value,
        nameOfPlayer2: p2.value
    }
    let response = await postGame(data);
    let result = await response.json(); //result è oggetto nuova partita    

    //NEW GAME
    //location.href="./damaMainPage.html";
}

function ciao(){
    alert("ciao");
}