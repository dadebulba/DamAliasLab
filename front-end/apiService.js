const URL = "https://localhost:5001/weatherforecast";
let form=document.getElementById('form');

async function getGame() {
    /*let response= await fetch(URL, {
        method: 'GET'
    })
    return response;*/
    
    //mockUP con promise
    let promise = new Promise(function(resolve, reject) {
        let obj = [{"id":"0345","player1":"fede","player2":"tommi"},
        {"id":"0356","player1":"A","player2":"B"},
        {"id":"0356","player1":"C","player2":"D"},
        {"id":"0356","player1":"E","player2":"F"},
        {"id":"0356","player1":"G","player2":"H"}]
        resolve(obj);
    });
    return promise;
    
}

async function postGame() {
    return fetch(bho, {
        method: 'POST',
        /*headers: {
            'Content-Type': 'application/json;charset=utf-8'
        },*/
        body: new FormData(form)
      });
}


export { getGame, postGame };
