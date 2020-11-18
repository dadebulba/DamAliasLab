const URL = "https://postman-echo.com";
const GET_ENDPOINT = "get";
const POST_ENDPOINT = "post";

async function getGame(foo1, foo2) {
    return fetch(`${URL}/${GET_ENDPOINT}?foo1=${foo1}&foo2=${foo2}`, {
        method: 'GET'
    });
}

async function postGame(body) {
    return fetch(`${URL}/${POST_ENDPOINT}`, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json;charset=utf-8'
        },
        body: JSON.stringify(body)
    });
}

export { getGame, postGame };
