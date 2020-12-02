import {getData, postData} from './apiService.js';

let getBtn = document.getElementById('sendGet');
let postBtn = document.getElementById('sendPost');
let pageContent = document.getElementById('pageContent')

getBtn.addEventListener('click', async (event) => {
    console.log("Event: ", event.target);
    let response = await getData("hello", "world");
    let obj = await response.json();
    pageContent.innerHTML = JSON.stringify(obj.args);
})

postBtn.addEventListener('click', async (event) => {
    console.log("Event: ", event.target);
    let response = await postData("Hello world");
    let obj = await response.json();
    pageContent.innerHTML = JSON.stringify(obj.data);
})
