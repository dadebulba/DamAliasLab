const URL = "http://localhost:52953";

async function getGame() {
    try {
        let response = await fetch(`${URL}/api/game`, {
            method: 'GET'
        })
        return response;
    }
    catch (error) {
        console.error(error);
        alert(`Error ${error.name}: ${error.message}`);
        return null;
    }
}

async function postGame(data) {
    try {
        let response = await fetch(`${URL}/api/game`, {
            method: 'POST',
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify(data)
        });
        return response;
    }
    catch (error) {
        console.error(error);
        alert(`Error ${error.name}: ${error.message}`);
        return null;
    }
}

async function getID(id) {
    try {
        let response = await fetch(`${URL}/api/game/${id}`, {
            method: 'GET'
        })
        return response;
    }
    catch (error) {
        console.error(error);
        alert(`Error ${error.name}: ${error.message}`);
        return null;
    }
}

async function put(id, data) {
    try {
        let response = await fetch(`${URL}/api/game/${id}`, {
            method: 'PUT',
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify(data)
        });
        return response;
    }
    catch (error) {
        console.error(error);
        alert(`Error ${error.name}: ${error.message}`);
        return null;
    }
}


export { getGame, postGame, getID, put };
