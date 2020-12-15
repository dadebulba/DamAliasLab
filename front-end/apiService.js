const URL = "https://localhost:5001";
const VALID_STATUS = /^2[0-9][0-9]$/;

function checkResponseMiddleware(response) {
    if(!VALID_STATUS.test(response.status)) {
        alert(`Error ${response.status}: ${response.statusText}`);
        return null;
    }
    else {
        return response;
    }
} 

async function getGame() {
    try {
        let response = await fetch(`${URL}/api/game`, {
            method: 'GET'
        })
        return checkResponseMiddleware(response);
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
        })
        return checkResponseMiddleware(response);
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
        return checkResponseMiddleware(response);
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
        return checkResponseMiddleware(response);
    }
    catch (error) {
        console.error(error);
        alert(`Error ${error.name}: ${error.message}`);
        return null;
    }
}

async function deleteLastMove(id) {
    try {
        let response = await fetch(`${URL}/api/game/${id}/lastMove`, {
            method: 'DELETE'
        });
        return checkResponseMiddleware(response);
    }
    catch (error) {
        console.error(error);
        alert(`Error ${error.name}: ${error.message}`);
        return null;
    }
}

async function deleteGame(id) {
    try {
        let response = await fetch(`${URL}/api/game/${id}`, {
            method: 'DELETE'
        });
        return checkResponseMiddleware(response);
    }
    catch (error) {
        console.error(error);
        alert(`Error ${error.name}: ${error.message}`);
        return null;
    }
}


export { getGame, postGame, getID, put, deleteLastMove, deleteGame};
