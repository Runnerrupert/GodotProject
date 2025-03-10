# Level-Up


## RoadMap

✅ - Completed

Steps Until 1.0.0 Release: 

Phase 1: 

1. Startup Completion  
    ✅ 0.0.1: Phase 1 Version Control Planning Complete
    - 0.0.1.1 : Setup Basic Scene Manager for initial Scene Switches
    - 0.0.1.2 : Main Menu with Welcome Message & Short Description of the Game
    - 0.0.1.3 : Continue, New Game, Load Game, About, and Exit options available to player (Non-Functional)
    - 0.0.1.4 : About Menu (Functional)
    - 0.0.2 : New Game Initialization (Functional)

2. Tutorial   
    - 0.0.2.1 : Initial Tutorial - Explaining Controls/Inputs (Functional)
    - 0.0.2.2 : Options for choosing Difficulty (Functional - Only 1 Difficulty to Start)
    - 0.0.3 : Successfully Completes Tutorial and Starts Up Main Gameloop

3. Main GameLoop  
    - 0.0.3.1 : Non-functional User Options (Start Mission, Save Game, Inventory, Tutorial, Main Menu)
    - 0.0.3.2 : Main Menu Successfully returns the player to Previous Section
    - 0.0.3.3 : Tutorial Option routes to regular Tutorial
    - 0.0.3.4 : Inventory Option Successfully Begins Inventory Options
    - 0.0.3.5 : Save Game Option Successfully Begins Save Game Options
    - 0.0.4 : All Options Startup and are Functional

4. Basic Classes  
    - 0.0.4.1 : Player, Enemy, Item and Weapon Classes Started
    - 0.0.4.1.1 : A Few Basic Items and Weapons Created (Items have no effects yet)
    - 0.0.4.2 : Player Class Allows for Equipping Weapons and Showing Basic Character Stats
    - 0.0.4.3 : Enemy Class Allows Equipping Items, Weapons and Showing Basic Enemy Stats
    - 0.0.4.3.1 : A Few Basic Enemies are Created and can be created with Randomized Weapons

5. Inventory Management  
    - 0.0.5.1: Non-functional User Options (Show Character Inventory, Show Item Inventory, Show Weapon Inventory, Back)
    - 0.0.5.2: Back Option Successfully returns the player to Previous Section
    - 0.0.5.3: Show Character Inventory Option Shows a List of the Players Characters (With Back Option)
    - 0.0.5.4: Show Item Inventory Option Shows a List of the Players Items (With Back Option)
    - 0.0.5.5: Show Weapon Inventory Option Shows a List of the Players Weapons (With Back Option)
    - 0.0.5.6: Character Inventory Option allows Interaction (Shows Stats after Selection, Back)
    - 0.0.5.7: Item Inventory Option allows Interaction (Shows Stats after Selection, Back)
    - 0.0.5.8: Weapon Inventory Option allows Interaction - Shows Equipped Weapon/s (Shows Stats after Selection, Equip Weapon, Unequip Weapon (If Equipped), Back)

6. Save Game Management
    - 0.0.6.1: All Saved Data Planned with Modularization
    - 0.0.6.2: Player Inventory is Written to New Files using JSON Format
    - 0.0.6.3: Player Statistics are Written to a New File using JSON Format
    - 0.0.6.4: Player Unlocks are written on a New File using JSON Format

7. Load Game Management
    - 0.0.7.1: Reading and initializing data from .json "save" file created

Phase 2: 

1. Mission Management
    - 0.1.0.1: Phase 2 Version Control Planning Complete
    - 0.1.0.2: 3 Basic Procedural Mission Quests Created and Viewable by the Player

Final Phase Before Polishing: 

?. Skeleton Complete  
    - 0.4.1 : Bare-Bones functionality complete for the game Start to Finish



Open Game --> Startup Screen
     - Continue, New Game, Load Game, About, Quit (Player Options)
        -- Continue --> Load Most Recent Game --> Start Gameloop
        -- New Game --> Startup Tutorial --> Difficulty Selection --> Start Gameloop
        -- Load Game --> Show All Saves (Player Options) --> Load Game --> Start Gameloop
        -- About --> Show Description & Credits
        -- Quit --> Exit Game

Start Gameloop --> Main Menu Selection
    - Start Mission
    - Save Game
    - Inventory --> Show Player Inventory 
        -- Characters --> Inventory Interaction (Characters) --> Check Status (Player Option)
        -- Items --> Inventory Interaction (Items) --> Check Status (Player Option)
        -- Weapons --> Inventory Interaction (Weapons) --> Check Status (Player Option)
        -- Cards --> Inventory Interaction (Cards) --> Check Status (Player Option)
    - Tutorial --> Controls (Number Options & Directional Movement (N,S,E,W))
    - Main Menu (Back) --> Save Game into Current/New Save --> Startup Screen (Player Options)

Start Mission --> Mission Selection --> Mission Generation --> Spawn Location Generation

