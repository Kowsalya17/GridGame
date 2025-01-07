Game Prototype
How to Play:
a) Objective: Match adjacent tiles of the same color by swapping them. Each successful match will
remove the tiles and score points of adding 5.
b) Controls: Click on a tile to select it, then click on an adjacent or any other tile to swap them. If
the tiles match, they will disappear, and you will score points.
c) Restarting: If you&#39;d like to restart the game, you can reload the scene by calling the
RestartGame() method or through the Restart Button on the UI , depending on your setup.
Setup Instructions:
o Clone or download the project folder.
o Open Unity Hub and click on &quot;Add&quot; to add the project to your Unity workspace.
o Open the project and wait for Unity to load all necessary assets.
o Press the Play button in Unity to test the prototype.
o The grid will generate automatically, and you can begin swapping tiles to form matches.
o You can play the game by using the WebGL link.
Challenges and Solutions:
Challenge: Tile swapping logic was tricky, as it needed to ensure tiles only swapped when another tile
clicked and also needed to update the grid&#39;s state.
Solution: I implemented the AreTilesAdjacent() method to check whether two tiles are adjacent, and
UpdateGrid() to update the grid after a swap.
Challenge: Removing tiles and managing their visual and logical removal required clean-up and ensuring
that the tiles were removed properly.
Solution: I used a combination of spriteRenderer.enabled = false for visual removal and a
DeactivateAfterSound() coroutine to clean up the tile after its removal effect plays.
External Resources:
Audio: Tile click and removal sound effects.
Click sound and removal sound were sourced from Unity Asset store.
Particle Effects: Used for tile click and removal effects.
Particle systems were created using Unity’s default particle system tools.
ChatGPT: Used for generating the grid swapping logic, helping implement the methods for handling tile
swapping and adjacency checks.
WebGL Deployment: The game can be played in a web browser.
Click here to play the game on itch.io, https://kowsalya17.itch.io/gridgame .
