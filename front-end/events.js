import {getGame, postGame, getID} from "./apiService.js";

let newgame=0;
let j=0;
let resumegameButton=document.getElementById('resume');
let newgameButton=document.getElementById('new');
let form=document.getElementById('form');
let conteinerTab=document.getElementById('container-tab');
let tableGames=document.getElementById('table-games');
//let submitButton=document.getElementById("submit");


resumegameButton.addEventListener('click', viewTableGames);
newgameButton.addEventListener('click', viewForm );
tableGames.addEventListener('click', gameChosen);
form.addEventListener('submit', sendForm);


function viewForm(){
    form.style.display='flex'; //view form 
    conteinerTab.style.display='none';  //hide table
}

async function viewTableGames(){
    if(j==0){
        let response = await getGame();
        let obj = await response.json(); // read response body and parse as JSON
        
        for(let key in obj){
            let tr = document.createElement('tr');
            tr.className = `rows`; //poi aggiungere class${key} se serve una classe diversa per ogni riga
            tr.innerHTML=`<td> ${obj[key].id} </td> <td>${obj[key].player1}-${obj[key].player2}</td> <td><button class="lastColumn" id="${key}" >play!</button></td>`;
            let heading=document.getElementById("heading");
            heading.after(tr);
        }
        j++;
    } 
    conteinerTab.style.display='flex'; //view table 
    form.style.display='none'; //hideForm
}

async function gameChosen(){
    let target = event.target;  // where was the click
    if(target.className=="lastColumn"){  //only if click on play button
        let id=target.id;
        
        //devo chiamare GET ID e andare nell'altra pagina
        let response = await getID(id);
        location.href="./damaMainPage.html";
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
    let result = await response.json();


    //NEW GAME
    //location.href="./damaMainPage.html";
    //let newgame=1;
}