## How To Use

In Program.cs in the DisposableHeroes base project, you set all of the initial conditions for a simulation run. 
- Set number of runs to simulate
- Set the players that will play in the game (as well as their strategy)
- Run console program so see output

## Player Strategies
To define how a type of player will act, just inherit from IPlayerStrategy and define what action the player will take for each method 
that the interface implements. Then, pass in that strategy for a player in the Program.cs in the Disposableheroes base project to test it out.