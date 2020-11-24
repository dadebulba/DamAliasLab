//const URL = "https://jsonplaceholder.typicode.com/todos/1";
//const GET_ENDPOINT = "get";
//const POST_ENDPOINT = "post";
const URL1 = "https://localhost:5001/weatherforecast";
let form=document.getElementById('form');

async function getGame() {
    let response= await fetch(URL1, {
        method: 'GET'
    });
    return response;
}

async function postGame() {
    return fetch(URL, {
        method: 'POST',
        /*headers: {
            'Content-Type': 'application/json;charset=utf-8'
        },*/
        body: new FormData(form)
      });
}


export { getGame, postGame };
