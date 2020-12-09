const URL = "https://localhost:44307";
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

async function put(id,data){
    /*let response = fetch(`${URL}/api/game/${id}`, {
        method: 'PUT',
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(data)
      });
    return response;*/
    let promise = new Promise(function(resolve, reject) {
        let obj = {"id":1,"namePlayer1":"Federico","namePlayer2":"Tommaso","moves":["from X to Y"],
        "black":[{"position":{"row":0,"column":1},"pawnId":0,"color":1,"upgraded":false},
        {"position":{"row":0,"column":3},"pawnId":1,"color":1,"upgraded":false},
        {"position":{"row":0,"column":5},"pawnId":2,"color":1,"upgraded":false},
        {"position":{"row":0,"column":7},"pawnId":3,"color":1,"upgraded":false},
        {"position":{"row":1,"column":0},"pawnId":4,"color":1,"upgraded":false},
        {"position":{"row":1,"column":2},"pawnId":5,"color":1,"upgraded":false},
        {"position":{"row":1,"column":4},"pawnId":6,"color":1,"upgraded":false},
        {"position":{"row":1,"column":6},"pawnId":7,"color":1,"upgraded":false},
        {"position":{"row":2,"column":1},"pawnId":8,"color":1,"upgraded":false},
        {"position":{"row":2,"column":3},"pawnId":9,"color":1,"upgraded":false},
        {"position":{"row":2,"column":5},"pawnId":10,"color":1,"upgraded":false},
        {"position":{"row":2,"column":7},"pawnId":11,"color":1,"upgraded":false}],
        "white":[{"position":{"row":4,"column":1},"pawnId":0,"color":0,"upgraded":false},
        {"position":{"row":5,"column":2},"pawnId":1,"color":0,"upgraded":false},
        {"position":{"row":5,"column":4},"pawnId":2,"color":0,"upgraded":false},
        {"position":{"row":5,"column":6},"pawnId":3,"color":0,"upgraded":false},
        {"position":{"row":6,"column":1},"pawnId":4,"color":0,"upgraded":false},
        {"position":{"row":6,"column":3},"pawnId":5,"color":0,"upgraded":false},
        {"position":{"row":6,"column":5},"pawnId":6,"color":0,"upgraded":false},
        {"position":{"row":6,"column":7},"pawnId":7,"color":0,"upgraded":false},
        {"position":{"row":7,"column":0},"pawnId":8,"color":0,"upgraded":false},
        {"position":{"row":7,"column":2},"pawnId":9,"color":0,"upgraded":false},
        {"position":{"row":7,"column":4},"pawnId":10,"color":0,"upgraded":false},
        {"position":{"row":7,"column":6},"pawnId":11,"color":0,"upgraded":false}],
        "turn":0,"pointsWhite":20,"pointsBlack":0}
        resolve(obj);
    });
    return promise;
}

async function deleteLastMove() {
    /*
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
    */
    let promise = new Promise(function(resolve, reject) {
        let obj = {
            "id": 1,
            "namePlayer1": "gino",
            "namePlayer2": "pino",
            "moves": [],
            "black": [
                {
                    "position": {
                        "row": 0,
                        "column": 1
                    },
                    "pawnId": 0,
                    "color": 1,
                    "upgraded": false
                },
                {
                    "position": {
                        "row": 0,
                        "column": 3
                    },
                    "pawnId": 1,
                    "color": 1,
                    "upgraded": false
                },
                {
                    "position": {
                        "row": 0,
                        "column": 5
                    },
                    "pawnId": 2,
                    "color": 1,
                    "upgraded": false
                },
                {
                    "position": {
                        "row": 0,
                        "column": 7
                    },
                    "pawnId": 3,
                    "color": 1,
                    "upgraded": false
                },
                {
                    "position": {
                        "row": 1,
                        "column": 0
                    },
                    "pawnId": 4,
                    "color": 1,
                    "upgraded": false
                },
                {
                    "position": {
                        "row": 1,
                        "column": 2
                    },
                    "pawnId": 5,
                    "color": 1,
                    "upgraded": false
                },
                {
                    "position": {
                        "row": 1,
                        "column": 4
                    },
                    "pawnId": 6,
                    "color": 1,
                    "upgraded": false
                },
                {
                    "position": {
                        "row": 1,
                        "column": 6
                    },
                    "pawnId": 7,
                    "color": 1,
                    "upgraded": false
                },
                {
                    "position": {
                        "row": 2,
                        "column": 1
                    },
                    "pawnId": 8,
                    "color": 1,
                    "upgraded": false
                },
                {
                    "position": {
                        "row": 2,
                        "column": 3
                    },
                    "pawnId": 9,
                    "color": 1,
                    "upgraded": false
                },
                {
                    "position": {
                        "row": 2,
                        "column": 5
                    },
                    "pawnId": 10,
                    "color": 1,
                    "upgraded": false
                },
                {
                    "position": {
                        "row": 2,
                        "column": 7
                    },
                    "pawnId": 11,
                    "color": 1,
                    "upgraded": false
                }
            ],
            "white": [
                {
                    "position": {
                        "row": 5,
                        "column": 0
                    },
                    "pawnId": 0,
                    "color": 0,
                    "upgraded": false
                },
                {
                    "position": {
                        "row": 5,
                        "column": 2
                    },
                    "pawnId": 1,
                    "color": 0,
                    "upgraded": false
                },
                {
                    "position": {
                        "row": 5,
                        "column": 4
                    },
                    "pawnId": 2,
                    "color": 0,
                    "upgraded": false
                },
                {
                    "position": {
                        "row": 5,
                        "column": 6
                    },
                    "pawnId": 3,
                    "color": 0,
                    "upgraded": false
                },
                {
                    "position": {
                        "row": 6,
                        "column": 1
                    },
                    "pawnId": 4,
                    "color": 0,
                    "upgraded": false
                },
                {
                    "position": {
                        "row": 6,
                        "column": 3
                    },
                    "pawnId": 5,
                    "color": 0,
                    "upgraded": false
                },
                {
                    "position": {
                        "row": 6,
                        "column": 5
                    },
                    "pawnId": 6,
                    "color": 0,
                    "upgraded": false
                },
                {
                    "position": {
                        "row": 6,
                        "column": 7
                    },
                    "pawnId": 7,
                    "color": 0,
                    "upgraded": false
                },
                {
                    "position": {
                        "row": 7,
                        "column": 0
                    },
                    "pawnId": 8,
                    "color": 0,
                    "upgraded": false
                },
                {
                    "position": {
                        "row": 7,
                        "column": 2
                    },
                    "pawnId": 9,
                    "color": 0,
                    "upgraded": false
                },
                {
                    "position": {
                        "row": 7,
                        "column": 4
                    },
                    "pawnId": 10,
                    "color": 0,
                    "upgraded": false
                },
                {
                    "position": {
                        "row": 7,
                        "column": 6
                    },
                    "pawnId": 11,
                    "color": 0,
                    "upgraded": false
                }
            ]
        }
        resolve(obj);
    });
    return promise;
}

/*async function put(id, data) {
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
}*/


export { getGame, postGame, getID, put, deleteLastMove };
