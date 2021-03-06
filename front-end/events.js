import { getGame, postGame } from "./apiService.js";

let firstTimeGet = true;
let resumegameButton = document.getElementById('resume');
let newgameButton = document.getElementById('new');
let form = document.getElementById('form');
let conteinerTab = document.getElementById('container-tab');
let tableGames = document.getElementById('table-games');

resumegameButton.addEventListener('click', viewTableGames);
newgameButton.addEventListener('click', viewForm);
tableGames.addEventListener('click', gameChosen);
form.addEventListener('submit', sendForm);


function viewForm() {
    form.style.display = 'flex'; //view form 
    conteinerTab.style.display = 'none';  //hide table
}

async function viewTableGames() {
    let response = await getGame();
    if (response != null) {
        let obj = await response.json(); // read response body and parse as JSON
        if (firstTimeGet) {
            for (let key in obj) {
                let tr = document.createElement('tr');
                tr.className = `rows`;
                tr.innerHTML = `<td> ${obj[key].id} </td> <td>${obj[key].namePlayer1}-${obj[key].namePlayer2}</td> <td><button class="lastColumn" id="${obj[key].id}" >play!</button></td>`;
                let heading = document.getElementById("heading");
                heading.after(tr);
            }
            firstTimeGet = false;
        }
        if(obj.length > 0){
        conteinerTab.style.display = 'flex'; //view table 
        form.style.display = 'none'; //hideForm
        }
        else{
            alert("Non ci sono partite iniziate");
        }
    }
}

async function gameChosen() {
    let target = event.target;  // where is the click
    if (target.className == "lastColumn") {  //only if click on play button
        let id = target.id;
        invia_id(id);
    }
}

async function sendForm(evt) {

    evt.preventDefault();

    let p1 = document.getElementById("player1");
    let p2 = document.getElementById("player2");
    let data = {
        NamePlayer1: p1.value,
        NamePlayer2: p2.value
    }
    let response = await postGame(data);
    if (response != null) {
        let result = await response.json();    
        invia_id(result.id);
    }
}

function invia_id(id) {
    var form = document.createElement("form");
    form.setAttribute("method", "get");
    form.setAttribute("action", "./damaMainPage.html");
    var hiddenField = document.createElement("input");
    hiddenField.setAttribute("type", "hidden");
    hiddenField.setAttribute("name", "id");
    hiddenField.setAttribute("value", id);
    form.appendChild(hiddenField);
    document.body.appendChild(form);
    form.submit();
}
