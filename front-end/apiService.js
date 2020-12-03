const URL = "http://localhost:52953";

async function getGame() {
    let response= await fetch(`${URL}/api/game`, {
        method: 'GET'
    })
    return response;
}

async function postGame(data) {
    let response = fetch(`${URL}/api/game`, {
        method: 'POST',
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(data)
      });
    return response;
}

async function getID(id){
    let response= await fetch(`${URL}/api/game/${id}`, {
        method: 'GET'
    })
    return response;
}

async function put(id,data){
    let response = fetch(`${URL}/api/game/${id}`, {
        method: 'PUT',
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(data)
      });
    return response;
}


export { getGame, postGame, getID, put};
