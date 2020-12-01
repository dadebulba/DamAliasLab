const URL = "http://localhost:6015";
let form=document.getElementById('form');

async function getGame() {
    let response= await fetch(`${URL}/api/game`, {
        method: 'GET'
    })
    return response;
}

async function postGame(data) {
    let response = fetch(`bho`, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json;'
        },
        body: JSON.stringify(data)
      });
    return response;
}

async function getID(id){
    let response= await fetch(URL, {
        method: 'GET'
    })
    return response;
}


export { getGame, postGame, getID};
