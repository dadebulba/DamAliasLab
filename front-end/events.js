
let elem=document.getElementById('resume');
elem.addEventListener('click', prova);
elem.addEventListener('click',viewGamesTable);

let j=0;

async function viewGamesTable(){
    tab=document.getElementById('container-tab');
    tab.style.display='flex';
}

async function prova(){
    if(j==0){
        let url = 'https://api.github.com/repos/javascript-tutorial/en.javascript.info/commits';
        let response = await fetch(url);
        
        let json = await response.json(); // read response body and parse as JSON
        
        
        let nome=json[0].author.login;
            
        let tr = document.createElement('tr');
        tr.className = "rows";
    
        tr.innerHTML=`<td>ID3</td> <td>${nome}-Player8</td> <td>10-50</td>`;

        let table=document.getElementById("heading");
        table.after(tr);
        j++;
    }
}

/*
function f(){
    alert("bho");
    let elem=event.target;
    if(elem.tagName=="TD"){
        alert(elem.id);
        elem.innerHTML="<div class='piece black-piece'> </div>"
    }
    else {
        alert("gia occupato");
        elem.hidden=true;
    }
}
*/