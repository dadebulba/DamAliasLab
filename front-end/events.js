import {getGame, postGame} from "./apiService.js";

let j=0;
let ID;
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
       //let obj = await response.json(); // read response body and parse as JSON

       for(let key in response){
            let tr = document.createElement('tr');
            tr.className = `rows`; //poi aggiungere class${key} se serve una classe diversa per ogni riga
            tr.innerHTML=`<td> ${response[key].id} </td> <td>${response[key].player1}-${response[key].player2}</td> <td><button class="lastColumn" id="${key}" >play!</button></td>`;
            let heading=document.getElementById("heading");
            heading.after(tr);
        }
        j++;
    } 
    conteinerTab.style.display='flex'; //view table 
    form.style.display='none'; //hideForm
}

function gameChosen(){
    let target = event.target;  // where was the click
    if(target.className=="lastColumn"){  //only se click on play button
        ID=target.id;
        location.href="./damaMainPage.html";
        //devo chiamare GET ID e andare nell'altra pagina
    }
}

async function sendForm (evt){
    
    evt.preventDefault();

    /*let player1 = document.getElementById("player1");
    let player2 = document.getElementById("player2");
    console.log(player1.value);
    console.log(player2.value);
    title.innerHTML = `${player1.value} and ${player2.value}`;*/
    
    let response = await postGame();
    let result = await response.json(); 

    alert(result.message);
}
