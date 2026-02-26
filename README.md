# Aim Trainer

[cite_start]Aim Trainer is a mini-game for reflex training: each round, targets appear at a random position[cite: 12, 13]. [cite_start]The player must click as quickly and accurately as possible before the target disappears[cite: 14].

---

## How to Run

* [cite_start]**Clone the Repository**: Clone the game's GitHub repository to your local machine[cite: 86]. 
* [cite_start]**Open the Project**: Launch the Unity Editor (version 6000.0.60f1 is recommended) and open the folder containing the cloned source code and data assets[cite: 25].
* [cite_start]**Build and Run**: Once the project is successfully loaded in the editor, use the `Build and Run` option to compile and launch the game loop[cite: 4].

---

## Controls

* [cite_start]**Left Mouse Click**: Used to click on targets to determine whether the click position lies within the target circle[cite: 20, 46].
* [cite_start]**ESC Key**: Pause gameplay and display an overlay[cite: 81].

---

## Game Rules

### Game Flow
* [cite_start]**Match Duration**: The game runs for a fixed duration of 60 seconds[cite: 32]. 
* [cite_start]**End of Round**: When the timer ends, the game transitions to a results screen and shows the summary of the current session (Score, Hits, Misses, Accuracy)[cite: 33].

### Target Mechanics
* [cite_start]**Target Spawning**: Only 1 target is active at a time[cite: 35]. [cite_start]Target spawns at a random position (x, y)[cite: 36].
* [cite_start]**Visibility Restrictions**: To ensure the target is fully visible and does not clip the edges, the spawn position satisfies the following constraints[cite: 38]:
  [cite_start]$r<x<(Width-r)$ and $r<y<(Height-r)$ [cite: 37]
* [cite_start]**Lifespan**: The target has a specific radius and a time-to-live (TTL) in milliseconds[cite: 39]. 
* [cite_start]**Hit/Miss**: If the TTL expires without being hit, it counts as a miss (timeout) and a new target spawns[cite: 40]. 

### Scoring and Difficulty
* [cite_start]**Reaction Time**: Reaction time is measured from the moment the target appears to the moment it is hit[cite: 48]. 
* [cite_start]**Base Points**: Each successful hit awards a base amount of points[cite: 61].
* [cite_start]**Reflex Bonus**: A reflex bonus is added to your base points depending on how quickly you click[cite: 62]. The formula used is:
  [cite_start]$$\text{bonus} = \max(0, \frac{\text{TTL} - \text{Reaction Time}}{\text{TTL}} \times \text{bonus\_cap})$$ [cite: 63, 64, 66, 67]
* [cite_start]**Difficulty Progression**: The game becomes harder over time to avoid flat gameplay[cite: 72]. [cite_start]Mechanisms include gradually decreasing the TTL (targets disappear faster) and decreasing the radius (targets become smaller)[cite: 73, 74].
