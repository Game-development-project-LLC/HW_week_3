## Changes and Additions

- **Score as fixed HUD**  
  Moved the score display from a floating text above the player to a UI TextMeshPro element anchored to the top-right corner of the screen (Canvas with “Scale With Screen Size”), so it stays in the correct position on any resolution.

- **Ammo system + fire cooldown**  
  Added limited ammo per level and a 0.5 second cooldown between shots. When ammo reaches zero, the player can no longer shoot and the game automatically loads the next scene. At the start of each level the ammo is refilled.

- **Ammo display in top-left corner**  
  Added a HUD element (TextMeshPro + NumberField) in the top-left corner that shows the current remaining ammo in real time.

- **Low-ammo color warning**  
  Implemented a visual warning on the ammo HUD: the ammo text changes color when ammo is low (e.g., yellow under a certain threshold) and turns red when ammo is empty, giving the player clear feedback before running out of shots.

- **Pause system**  
  Added a simple pause feature: pressing `Esc` or `P` toggles pause. When the game is paused, time is frozen (Time.timeScale = 0) and a “PAUSED” message is displayed in the center of the screen; pressing the key again resumes the game.



# Unity week 2: Formal elements

A project with step-by-step scenes illustrating some of the formal elements of game development in Unity, including: 

* Prefabs for instantiating new objects;
* Colliders for triggering outcomes of actions;
* Coroutines for setting time-based rules.

Text explanations are available 
[here](https://github.com/gamedev-at-ariel/gamedev-5782) in folder 04.

## Cloning
To clone the project, you may need to install git lfs first (if it is not already installed):

    git lfs install 

To clone faster, you can limit the depth to 1 like this:

    git clone --depth=1 https://github.com/<repository-name>.git

When you first open this project, you may not see the text in the score field.
This is because `TextMeshPro` is not in the project.
The Unity Editor should hopefully prompt you to import TextMeshPro;
once you do this, re-open the scenes, and you should be able to see the texts.



## Credits

Programming:
* Maoz Grossman
* Erel Segal-Halevi

Online courses:
* [The Ultimate Guide to Game Development with Unity 2019](https://www.udemy.com/the-ultimate-guide-to-game-development-with-unity/), by Jonathan Weinberger

Graphics:
* [Matt Whitehead](https://ccsearch.creativecommons.org/photos/7fd4a37b-8d1a-4d4c-80a2-4ca4a3839941)
* [Kenney's space kit](https://kenney.nl/assets/space-kit)
* [Ductman's 2D Animated Spacehips](https://assetstore.unity.com/packages/2d/characters/2d-animated-spaceships-96852)
* [Franc from the Noun Project](https://commons.wikimedia.org/w/index.php?curid=64661575)
* [Greek-arrow-animated.gif by Andrikkos is licensed under CC BY-SA 3.0](https://search.creativecommons.org/photos/2db102af-80d0-4ec8-9171-1ac77d2565ce)
