## Building the Project
 **Prerequisites:**
  - Unity Editor 6000.0.60f1
  - Visual Studio or Visual Studio Code
  - .NET Standard 2.1

 **1. Clone the Repository** to your local machine.
 
 **2. Launch the Unity Hub** and add the project folder `Whack-a-zombie` as a new project (6000.0.60f1).
 
 **3. Launch the project on the Editor**
 
 **4.** Once the project is successfully loaded in the editor, go to `File` -> `Build and Run` to compile and launch the game. The resulting game will be stored in the `Build` folder in the root folder of the repo.

---
## Controls

*  **LMB** to interact with the UI and shoot targets.
*  **ESC** to toggle the pause menu as well as exiting settings.

---

## Game Rules

### Game Flow
*  **Match Duration**: By default, the game runs for a fixed duration of 69 seconds, but this duration can be changed manually (in Settings) to two values: 38 or 100 seconds. 
*  **End of Round**: When the timer ends, the game transitions to a results screen and shows the summary of the current session (Score, Hits, Misses, Accuracy). You can hit ESC then exit to menu if you wish to attempt another run.

### Target Mechanics
*  **Target Spawning**: A target will spawn every certain interval. This interval will decreases as time goes on.
*  **Lifespan**: The target has a specific radius and a time-to-live. The target despawns after their lifetime runs out
*  **Hit/Miss**: A hit is counted after a left click happens within the area of the target. If the TTL expires without being hit, it counts as a miss (timeout) and a new target(s) spawns. 

### Scoring and Difficulty
  - **Combo points**: Each successful hit awards 1 point of combo, but combo will be reset to 0 if a miss occurs, or if any target despawns. A successful hit is signalled with a different shooting sound accompanied by a distinct 'ding'.
  - **Score Points**: Each successful hit gains the player 5 points, plus a bonus based on the combo count: `score gain = 5 + combo`
  - **Reloading**: Every shot will be followed with a reload period, during this time the player will have to wait until the reload to finish to be able to fire again. The reload finishing is signalled with a gun cocking sound.
  - **Difficulty Progression**: The game becomes harder over time by gradually decreasing the spawn time (or cooldown time) to make targets appear faster. Past a certain point (>80s) the spawn rate will be the same as the reload time, maxing out the difficulty.

### Asset sources
*  All SFX and music resources are sourced from *Limbus Company*
