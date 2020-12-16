# DamAliasLab
DamAliasLab is a Web application to play the game of checkers

## Table of Content

- [Key Features](#key-features)
    - `First page`
    - `New Game button`
    - `Resume Game button`
    - `Main page`
    - `Make a move`
    - `Delete button`
    - `Reset Last Move button`
    - `End a game`

- [Database](#database)    
- [Usage and Installation](#usage-and-installation)
- [How to contribute to the project](#how-to-contribute-to-the-project)
- [Authors](#authors)
- [License](#license)

## Key Features

- `First page`: This is the first page, we can choose between two options (with two different buttons), start a new game or resume a game that has already started.

![image](/front-end/screen/firstPage.png)

- `New Game button`: if we click on 'New Game' button a form appears and we can enter the names of the players. When the players have finished to insert their names, if they press the start button the client will sent a POST request to the server in order to create a new game session. After the server has created the game session it will return the object to the client.

![image](/front-end/screen/firstPage-newGame.png)

- `Resume Game button`: if we click on 'Resume Game' button there are two possibility, if there are no games started an alert message appears, otherwise if there are games saved in memory, a table appears with the list of these games. We can resume playing one of these by clicking on the 'play button' on the right of each game. In order to retrieve a game session the client will send a GET request to the server with the identifier of that game. The server will retrieve the specified game session.

![image](/front-end/screen/firstPage-noStartedGames.png)

![image](/front-end/screen/firstPage-resumeGame.png)

- `Main page`: by clicking the play button we reach the main page with the checkers board in the center, the menu of the moves made on the left and the score on the right. There are also two red button on the right, for deleting or saving the current game.

![image](/front-end/screen/mainPage.png)

- `Make a move`: making a move is simple, first you have to click on the piece to be moved (the cell will become red) and then on the destination cell. If the move is legal, the pawn will move to the desired position, otherwise we will have an error message. In order to make a move the client will make a PUT request to the server. The server will evaluate the move declared in the body of the PUT request and if the move is valid will retrieve an updated object with the current game session.

![image](/front-end/screen/mainPage-selectedPawn.png) 
![image](/front-end/screen/mainPage-moveDone.png)

- `Delete button`: this button is used to delete the current game from the memory, an alert message appears if the operation was successful. In order to make this operation the client will send a DELETE request to the server with the identifier of the game session. The server will retrieve the deleted game session.

![image](/front-end/screen/mainPage-deleteGame.png) 

- `Save and Exit button`: if this button is clicked, you will return to the first page saving the current game. After that you could find this game in the resume game table.

- `Upgraded Pawn`: Upgraded pawns have a red crown in the center. A pawn will be upgraded when it will reach the opposite side of the chessboard. This type of pawns have the abilities to move for all four diagonals and cannot be eaten from pawns which are not upgraded. The pawns are upgraded by the server.

![image](/front-end/screen/mainPage-upgradedPawn.png) 

- `Reset Last Move button`: this button gives you the possibility to reset the last move made updating both the board and the move menu on the left. In order to make this operation the client will send a DELETE request to the server. The server will evaluate the lists of pawns on the grid and it will return an uptaded object of the game session.

- `End a game`: a game ends with 3 possible outcomes: either white or black can win or we can have a draw if too many moves without eating anything have been made. For every possible outcome we will have a different alert message and after that the finished match will be deleted from the memory.

![image](/front-end/screen/mainPage-gameFinished.png) 


## Database

In order to memorize all the game session we decided to use Entity Framework as an In-Memory Database. In this database we have two main relationships:
- 'GameSessions': memorize all the info about a GameSession so the players names and the valid moves made.
- 'Moves': memorize all the info about a Move so the pawn that has been moved, the coordinates from where it started the move and the coordinates where it stopped the move.

- GameSessions(_IdGameSessionId_:long, NamePlayer1:string, NamePlayer2:string)
- Moves(_IdGameSessionId_:long, _IdMove_:long)
>-Moves.IdGameSessionId FK -> GameSessions.IdGameSessionId
- Moves_Target(_IdMove_:long, IdPawn:long, Color:int, Upgraded:bool)
>-Moves_Target.IdMove FK -> Moves.IdMove
- Moves_From(_IdMove_:long, Row:int, Column:int)
>-Moves_From.IdMove FK -> Moves.IdMove
- Moves_To(_IdMove_:long, Row:int, Column:int)
>-Moves_To.IdMove FK -> Moves.IdMove

## Usage and installation

- Server:
>* In order to execute the server you need to install Visual Studio 2019 (https://visualstudio.microsoft.com/it/vs/). 
>* After you installed Visual Studio 2019 you need to import the project. The directory of the project is 'back-end/VivaLaDama'.
>* In order to execute the server you need to run the project.










## How to contribute to the project
if you want to contribute to the project you can simply create a new branch, write the new code on it and create a Pull Request.
After a careful review, if the code is all right, the repository administrator will be able to merge this branch to its tree master (main).









## Authors
Luca Giacominelli,
Federico Diprima,
Davide Bulbarelli

## License
OpenSource
