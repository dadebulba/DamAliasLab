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
    /*let response = fetch(`${URL}/api/game/${id}`, {
        method: 'PUT',
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(data)
      });
    return response;*/
    let promise = new Promise(function(resolve, reject) {
        let obj = {"id":id,"namePlayer1":"fede","namePlayer2":"tommi","moves":["from X to Y"],
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
        {"position":{"row":7,"column":6},"pawnId":11,"color":0,"upgraded":false}]}
        resolve(obj);
    });
    return promise;
}


export { getGame, postGame, getID, put};
