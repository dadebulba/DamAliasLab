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
    
    
- [Usage and Installation](#usage-and-installation)
- [How to contribute to the project](#how-to-contribute-to-the-project)
- [Authors](#authors)
- [License](#license)

## Key Features

- `First page`: This is the first page, we can choose between two options (with two different buttons), start a new game or resume a game that has already started.

![image](/front-end/screen/firstPage.png)

- `New Game button`: if we click on New Game button a form appears and we can enter the names of the players.

![image](/front-end/screen/firstPage-newGame.png)

- `Resume Game button`: if we click on Resume Game button there are two possibility, if there are no games started an alert message appears, otherwise if there are games saved in memory, a table appears with the list of these games. We can resume playing one of these by clicking on the 'play button' on the right of each game.

![image](/front-end/screen/firstPage-noStartedGames.png)

![image](/front-end/screen/firstPage-resumeGame.png)

- `Main page`: by clicking the play button we reach the main page with the checkers board in the center, the menu of the moves made on the left and the score on the right. There are also two red button on the right, for deleting or saving the current game.

![image](/front-end/screen/mainPage.png)

- `Make a move`: making a move is simple, first you have to click on the piece to be moved (the cell will become red) and then on the destination cell. If the move is legal, the pawn will move to the desired position, otherwise we will have an error message.

![image](/front-end/screen/mainPage-selectedPawn.png) 
![image](/front-end/screen/mainPage-moveDone.png)

- `Delete button`: this button is used to delete the current game from the memory, an alert message appears if the operation was successful.

![image](/front-end/screen/mainPage-deleteGame.png) 

- `Save and Exit button`: if you click this button you will return to the first page saving the current game. After that you could find this game in the resume game table.

- `Upgraded Pawn`: Upgraded pawns have a red crown in the center.

![image](/front-end/screen/mainPage-upgradedPawn.png) 

- `Reset Last Move button`: this button gives you the possibility to reset the last move made updating both the board and the move menu on the left.

- `End a game`: a game ends with 3 possible outcomes: either white or black can win or we can have a draw if too many moves without eating anything have been made. For every possible outcome we will have a different alert message and after that the finished match will be deleted from the memory.

![image](/front-end/screen/mainPage-gameFinished.png) 









## Usage and installation
Come si installa per provarlo in locale e come si usa










## How to contribute to the project
if you want to contribute to the project you can simply create a new branch, write the new code on it and create a Pull Request.
After a careful review, if the code is all right, the repository administrator will be able to merge this branch to its tree master.









## Authors
Luca Giacominelli
Federico Diprima
Davide Bulbarelli

## License
OpenSource
