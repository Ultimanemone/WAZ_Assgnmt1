## How to Run
 **Step1. Clone the Repository**: Clone the game's GitHub repository to your local machine. 
 
 **Step2. Open the Project**: Launch the Unity Editor (version 6000.0.60f1). After that, add the game folder into the editor by using `New Project`.
 
 **Step3. Build and Run**: Click the game project. Once the project is successfully loaded in the editor, go to 'File' -> `Build and Run` to compile and launch the game.

---
## Controls

*  **Left Mouse Click**: Used to click on targets when they appear to determine whether the click position lies within the target's area.
*  **ESC Key**: Pause gameplay and display an overlay.

---

## Game Rules

### Game Flow
*  **Match Duration**: By default, the game runs for a fixed duration of 69 seconds, but this duration can be changed manually (in Settings) to two values: 38 or 100 seconds. 
*  **End of Round**: When the timer ends, the game transitions to a results screen and shows the summary of the current session (Score, Hits, Misses, Accuracy).

### Target Mechanics
*  **Target Spawning**: At the beginning, there is 1 target is active at a time and it spawns at a random position. However, that number will grow up overtime.  
*  **Lifespan**: The target has a specific radius and a time-to-live (TTL) in milliseconds. 
*  **Hit/Miss**: A hit is counted after the left click happens within the area of the target. If the TTL expires without being hit, it counts as a miss (timeout) and a new target(s) spawns. 

### Scoring and Difficulty
*  **Combo points**: Each successful hit awards 1 point of combo, but combo will be reset to 0 if a miss occurs. 
*  **Score Points**: Each successful hit gains an amount of score based on the following formula:
`score = old_score + 5 + combo`
*  **Difficulty Progression**: The game becomes harder over time by gradually decreasing the spawn time (or cooldown time) to make targets appear faster.

### Asset sources
*  SFX and music resources were taken from Limbus Company