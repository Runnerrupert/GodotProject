# 🎮 Infinite Progression RPG – Game Design Document

## 🌌 Core Concept

A procedural RPG with infinite or absurdly high progression ceilings. Players create and manage characters, weapons, and items, each with individual levels. Everything can level up, with no caps or extremely high ones. Gameplay revolves around creation, customization, and endless progression.

---

## 🧠 Creation & Progression System

* **Mana (Gold)** is the central resource.

  * Used to create characters, items, and weapons.
  * Used to enhance or upgrade creations.
* **Procedural Missions**:

  * Send creations on missions (AI-controlled or direct control).
* **Infinite Leveling**:

  * All entities (characters, weapons, items) have levels.
  * Progression can make an ant as powerful as a giant with enough investment.
* **Stat-Based Requirements**:

  * Progress unlocks new functionality rather than gating via traditional XP trees.

---

## 🔬 Unlocking Mechanics

* **Scanning System**:

  * Scan creatures, weapons, or items to unlock mechanics (e.g., wings, extra arms).
  * Mechanics require multiple scans or other milestones to fully unlock.
* **Conditional Application**:

  * Mechanics can only be applied if prerequisites are met (e.g., speed stat for wings).
* Avoids grinding while still rewarding progression and exploration.

---

## 🎴 Card System

* Players can convert creations into **cards**:

  * Retains all stats at conversion time.
  * Cards can be traded.
  * Conversion is **irreversible** unless reversed by a specific item.
* When traded:

  * The card **reverts** to its original form in the new player’s world.
  * The **card state is destroyed**.
* **Lifetime Tracking**:

  * Cards track metadata like creation time, trade history, fusion events, and levels.
* **Player Rewards**:

  * Converting to cards grants cosmetics, titles, exclusive materials.

---

## ⚔️ Fusion & Melding System

* Fuse weapons, characters, and items.
* Results in a **“lesser” version** until full Mana cost is paid.
* Allows experimentation without immediate heavy resource consumption.
* Fusion outcomes can be carded.

---

## ⚖️ Progression Balance

* Infinite progression with **meaningful scaling**:

  * Mana costs scale with power.
  * High-level creations usable only if player meets progression thresholds or pays high Mana costs.
* Cards can’t shortcut progression:

  * Must be at the right level or accept major Mana expense.
* **No idle systems**, **offline rewards**, **daily/weekly missions**, or **multiplayer pressure**.
* Rewards **active players**, even those with limited play time.

---

## 🛠️ Tech Stack & Architecture

* **Godot Engine**, scripting in **C#**.
* Design goal: **Maximum modularity**.

  * Even small systems are split into clean, reusable components.
  * Supports easy scaling and reworking later.

---

## 📈 Development Milestones

### 0.0.x – Startup & UI

* 0.0.1: Main Menu with Welcome Message
* 0.0.1.1: Non-functional buttons: Continue, New Game, Load Game, About, Exit
* 0.0.1.2: About Menu (Functional)

### 0.0.2 – Tutorial

* 0.0.2.1: Basic Controls Tutorial
* 0.0.2.2: Difficulty Selection (1 mode active)

### 0.0.3 – User Initialization

* 0.0.3.1: User character initialized

### 0.0.4 – Game Loop UI (Non-functional)

### 0.0.5 – Basic Combat

* Initiate combat (no map gen required yet)

### 0.4.1 – Skeleton Complete

* Bare-bones playthrough from start to finish

---

## ✅ Design Philosophy

* Every creation (no matter how weak) can become meaningful.
* Players should always feel like they’re progressing.
* Game mechanics reward depth, experimentation, and creativity.
* No microtransactions or pay-to-win.
* Everything is earnable through time, effort, and strategic decisions.

## 🧠 Combat Ability Management System (Revised Design)

### Automated Abilities (AI-Driven)
- Each character possesses a unique set of abilities.
- Basic and mid-tier abilities are triggered automatically by AI, based on:
  - Cooldowns
  - Health thresholds
  - Enemy proximity
  - Behavior settings
- Players can set **pre-mission behavior priorities** for each character (e.g., aggressive, support-focused, reactive).
- This system reduces micromanagement while preserving character identity and strategic variety.

### Player-Activated Abilities (“Tactical Input System”)
- Certain **powerful or utility-focused abilities** are manually triggered by the player during combat.
- These abilities:
  - Are tied to specific characters.
  - Have costs (e.g., cooldowns, stamina, resources).
  - Must be **assigned to a limited ability bar** pre-mission.
- Encourages thoughtful **loadout selection** and **real-time tactical decision-making**.

### Combat Role Design Philosophy
- The player acts as a **tactical overseer**, not a direct micromanager.
- Player impact is created through:
  - Smart team setup and ability planning before a mission.
  - Real-time activation of key abilities to shift the tide of battle.
  - Coordination of abilities for maximum efficiency.

### Scalability & Depth
- Designed to support **large squads with minimal micromanagement**.
- Opens the door to:
  - Ability combo mechanics (e.g., stun into AoE).
  - Evolving character kits and synergy systems.
  - Expanded tactical tools like environmental abilities or team commands.
- Easy to expand over time with **new mechanics** or **advanced unit behaviors**.


## 📦 Mission System – Procedural Generation & Gate Keys

### 🎓 Tutorial Missions
- The first few missions are **semi-scripted** tutorials.
- While technically procedural, they follow a **fixed narrative and mechanical learning path**.
- Each mission gradually introduces new systems:
  - Movement → Basic Obstacles → Abilities → Combat → Party Management
- These missions are **not repeatable** via keys, but act as the player's introduction to mechanics and pacing.

---

### 🔑 Gate Keys – Core Concept
- Gate Keys are **items that unlock new procedural missions**.
- Each key acts as a **mission seed**, carrying metadata about:
  - Environment Type (e.g. Jungle, Desert, Ruins)
  - Challenge Focus (e.g. Strength, Puzzle, Combat)
  - Difficulty Level
  - Rarity and Rewards
- Using a key opens a **Mission Portal** to a new map.

#### Key Properties
```text
Name: "Cracked Ember Key"
Biome: Lava Cavern
Primary Stat: Strength 5+
Tags: Puzzle, Verticality, Burn Hazards
Rarity: Rare
Replayable: Yes
```

### 🧩 Completed Mission Key Transformations

Instead of keeping used Gate Keys for mission replays, **fully completed keys** undergo a transformation that rewards the player and clears inventory clutter.

#### 🔄 100% Completion Outcomes
When a player completes all objectives in a mission:

- The Gate Key is **consumed** or **transformed**
- Rewards are based on the mission's tags, biome, and challenges
- Different types of rewards are possible:

| Completion Type           | Reward Example                                   |
|---------------------------|--------------------------------------------------|
| Full Objective Clear      | Biome-themed weapon, gear, or upgrade            |
| All Stat Gates Opened     | Rare related Gate Key or bonus XP                |
| All Secrets Found         | Lore collectible or “God Ability” charge         |
| Optional Boss Defeated    | New passive trait unlocked on party member       |
| Time Challenge Beaten     | Cosmetic or title                                 |

---

#### 🧬 Transformed Key Types
Completed keys can become special resources:

- **Key Core**: Used to synthesize rarer keys or mission recipes
- **Completion Token**: Used as a form of mission-based currency

This creates **extra incentive** to explore and strategize around each mission rather than simply rushing the objective.


### 🧪 Fusion-Driven Key Completion

When a Gate Key is 100% completed, it no longer functions as a mission portal. Instead, it **transforms into a “Key Core”**, which can be used in the game's Fusion System.

Each Key Core retains metadata from the mission it was generated from:
- Biome (e.g. Fire, Ice, Ruins)
- Stat Tags (e.g. Strength, Intelligence, Agility)
- Completion Achievements (Boss Defeated, Puzzle Solved, All Gates Opened)

---

#### 🔧 Key Core Fusion Use Cases

| Fusion Target            | Example Result                                  |
|--------------------------|--------------------------------------------------|
| Key Core + Key           | Generates a hybrid mission (Puzzle + Fire Biome) |
| Key Core + Weapon        | Adds stat-scaling or elemental bonuses           |
| Key Core + Armor         | Infuses passive abilities (burn aura, agility boost) |
| Key Core + Ability Scroll| Enhances or evolves an ability (Ultimate tier)   |

---

#### 🧩 Fusion Examples

- `Puzzle Core + Ice Key` → Ice Temple Puzzle Challenge  
- `Strength Core + Iron Sword` → Crushing Blade (stun chance)  
- `Fire Core + Leather Cloak` → Smolderhide Wrap (burn resist + aura)  
- `Boss Core + Party Buff Scroll` → Ultimate Buff (manual activation)

---

This makes 100% completion of missions not just rewarding, but essential to crafting **powerful gear and unlocking endgame-level content**. Every finished key becomes part of your future strategy.

## 🔑 Gate Key System – Procedural Mission Scaling Framework

### Overview

To scale difficulty and complexity in a meaningful, player-driven way, missions are unlocked through **Gate Keys**, which control both **power level scaling** and **mechanical complexity** via a two-dimensional system:

- **Level** → Affects stat scaling (enemy HP, damage, required strength/intelligence/etc.)
- **Rarity & Tier** → Affects mission complexity, puzzle layers, AI behavior, modifiers, and fusion requirements

This allows for infinite content scaling without simply inflating numbers.

---

### 🧭 Gate Key Components

| Property        | Description                                                                 |
|-----------------|-----------------------------------------------------------------------------|
| **Rarity**       | Controls mission **complexity**, enemy behavior, and mechanics             |
| **Tier**         | Represents the depth within a rarity (i.e. Tier 1–30 within each rarity)    |
| **Level**        | Controls stat difficulty (enemy strength, required abilities, etc.)         |

---

### 🪙 Gate Key Rarity Table

| Rarity        | Tiers       | Complexity Profile                                               |
|---------------|-------------|------------------------------------------------------------------|
| **White**     | 1–5         | Basic traversal, light stat checks, introductory enemy AI       |
| **Green**     | 6–10        | Moderate puzzles, more tactical AI, simple synergy objectives   |
| **Blue**      | 11–15       | Introduces fusion-based gates, multi-path layouts, timed events |
| **Purple**    | 16–20       | Puzzle-enemy interleaving, cross-character coordination needed  |
| **Gold**      | 21–25       | Environment hazards, layered objectives, smarter enemies        |
| **Legendary** | 26–30       | Procedural mini-boss fights, mutator stacking, elite-level design |

Each tier builds on the previous, not just in difficulty, but in **strategic depth** and **interactivity**.

---

### 🔄 Examples of Scaling

- **White Tier 1 - Level 1 Mission**  
  - Weak enemies, basic corridors, optional stat-check obstacle (e.g., wall: `1/5 Strength`)
- **White Tier 1 - Level 10000 Mission**  
  - Same layout & mechanics, but enemies have high HP/damage and stat gates are extreme
- **Gold Tier 25 - Level 10000 Mission**  
  - Multi-phase boss encounter, enemies with fused traits, map-altering hazards, puzzles requiring fusion items

---

### 🧪 Optional Mechanics

- **Key Mutations**: Keys can spawn with **modifiers**, e.g.  
  - `Enemies explode on death`  
  - `Reduced healing`  
  - `Timed escape requirement`

- **Key Evolution**: Keys can be fused with special materials to **evolve rarity/tier**, unlocking harder content.

- **Key Core Conversion (100% Clear Reward)**:  
  Fully completing a mission transforms its key into a **Key Core**—a rare item used in fusion or crafting. These could:
  - Add modifiers to future keys
  - Imbue gear with unique properties
  - Unlock special fusion evolutions

---

### 💡 Design Philosophy

This system ensures that:
- Players have **freedom** to engage at their own pace and skill ceiling
- Missions evolve in depth, not just number inflation
- The endgame isn't just “more HP,” but rather *more thinking, synergy, and creativity*

---

### 🔮 Future Ideas (To Expand Later)

- Faction-based keys (unlocked via world progression)
- Co-op Gate Missions with hybrid puzzles
- Player-generated Gate Keys via fusion (design your own challenge)

## 🧱 Level-Based Mechanic Unlocks

To preserve player engagement and avoid overwhelming early players, new mechanics are introduced progressively as missions increase in level. This allows procedural content to remain fresh, surprising, and mechanically rich over time, rather than simply scaling enemy stats.

### 🎯 Design Goal:
Rather than only scaling numbers, higher-level missions should introduce entirely new mechanics, puzzles, hazards, and interactions — creating a dynamic and evolving world.

### 🧪 Mechanic Threshold Examples

| **Level Threshold** | **Mechanic Introduced**                | **Description** |
|---------------------|----------------------------------------|-----------------|
| Level 1+            | Basic Movement, Jumping, Light Combat  | Simple terrain and enemies; tutorial-focused |
| Level 5+            | Strength Gates                         | Breakable walls/doors with Strength requirement |
| Level 10+           | Water Traversal                        | Introduces swimming, water hazards, or water-fusion mechanics |
| Level 25+           | Flying Pathways                        | Air-only traversal segments or bypass puzzles |
| Level 50+           | Multi-Switch / Multi-Character Puzzles | Requires team-based interaction and positioning |
| Level 75+           | Status Effect Enemies                  | Burn, Freeze, Poison, Shock introduced through enemies and items |
| Level 100+          | High-Impact Spells (e.g. "Death")      | Enemies and mechanics can now inflict deadly status effects or require counterplay like revival mechanics |
| Level 150+          | Multi-Layered Maps                     | Verticality, environmental phase-shifting, dual-map puzzles |
| Level 200+          | Environmental Instability              | Volatile zones, destructible terrain, unstable fusion chambers |


## 🔬 Scanning System & Trait Discovery

### Overview
The scanning system is designed to allow players to "discover" traits, abilities, and creature types throughout gameplay. Scanning a creature doesn't grant direct access to replicate it, but instead unlocks it as a potential component in procedural creation systems such as fusion or crafting.

---

### 🔍 Scanning Mechanics

- **Scanning** a creature reveals its:
  - Species Type (e.g., Skullbat)
  - Trait Affixes (e.g., Molten, Cursed)
  - Abilities or Skills (if applicable)

- Scanning **adds these components** to a player's **Trait Knowledge Pool**.

- Each scanned trait or type increases a **Discovery Counter**, used to weight future access.

---

### 📚 Trait Knowledge Pool

Scanned traits and types are not directly assignable — they serve as entries in a **randomized fusion pool**, which influences procedural generation.

| Component       | Behavior when Scanned                            |
|----------------|--------------------------------------------------|
| Trait (e.g. Molten) | Added to trait pool; increases fusion weight slightly |
| Species (e.g. Skullbat) | Added to species pool; unlocks base creature archetypes |
| Ability | May unlock possibility of learning it through fusion or replication |

---

### 🎲 Procedural Fusion: Controlled Randomness

When fusing creatures or crafting items, the system draws from the player’s **Trait Pool** to determine the outcome:

- Outcomes are **seeded by inputs**, which include:
  - Scanned traits and creatures
  - Fusion materials or keys
  - Player progression
  - Environmental/crafting conditions (e.g., lava = higher Molten odds)

- **Fusion results are not guaranteed**, preserving randomness and item diversity.

- Some rare traits may require:
  - High Discovery Counters
  - Special fusion materials
  - Specific fusion locations or rituals
  - Unique combinations of components

---

### 🛡️ Legendary Traits and Limitations

To prevent trait abuse and maintain economy health:

- **Ultra-rare traits** (e.g., God of Death, Eternal Flame) cannot be added to fusion pools without meeting **extra criteria**, such as:
  - Boss scans
  - Trait fusion rituals
  - Fusion permits (one-time-use items that allow specific trait inclusion)
  - Scan + Achievement combo unlocks

- Traits like `Flameborn` or `Cursed` have increased odds over time but may **decay in likelihood** after repeated use (to encourage trait variety).

---

### 🎟️ Fusion Permits

Some traits may require **Trait Permits** to access during fusion:

- These are rare crafting blueprints or drops.
- Each permit allows **one use** of a high-tier trait.
- Encourages trading, rarity, and strategic use.

---

### 📈 Trait Tracking & Scan Tiering

- Each trait and species has a **Scan Tier**, which increases each time that category is scanned.
- Example:

| Scan Action | Result |
|-------------|--------|
| Scan "Molten Skullbat" | +1 to "Molten", +1 to "Skullbat" |
| Scan "Cursed Skullbat" | +1 to "Cursed", +1 to "Skullbat" (now Skullbat = 2) |

- Scan Tier affects:
  - Procedural inclusion weight
  - Access to fusions or new discoveries
  - Event unlocks or scan-based objectives

---

### 🔄 Fusion Behavior Summary

| Player Action | Result |
|---------------|--------|
| Scans a new trait | Adds to pool with low weight |
| Repeated scans | Increase weight (up to soft cap) |
| Crafts using common materials | Low trait variance, common results |
| Crafts with rare components or permits | Higher chance of rare traits, more complex outcomes |
| Fusion spam of same trait | Diminished odds (optional system for balancing) |

---

### 🧬 Future Considerations

- **Fusion Seeds**: Fusions generate a unique hash or seed for each result, ensuring traceability and trade legitimacy.
- **Scan Milestones**: Reaching high scan counts may unlock perks or special passive benefits.
- **AI/Procedural Naming**: Discovered traits and creatures may evolve in name and appearance as scan tiers increase.


## 🧭 Save System Structure: Online vs Offline Profiles

To support both freeform single-player gameplay and a secure, cheat-resistant online economy, the game will feature two distinct save profile types:

---

### 🟢 Online Save Profile (Verified)
This profile type is synced with the server and adheres to strict rules to maintain fairness in trading and global interactions.

#### Key Features:
- ✅ Secure, server-validated saves.
- ✅ Access to trading, player market, and online multiplayer content.
- ✅ Blueprint and item validation via server-side hashing/signatures.
- ✅ Shared scan/progression data tracked and approved server-side.
- ❌ Restricted from unauthorized file tampering or modding.

#### Use Case:
Designed for players who want to engage in the global economy, trade powerful creations, and participate in competitive or social online missions.

---

### 🔴 Offline Save Profile (Unverified)
This profile is local to the player's machine and offers unrestricted gameplay but does not interact with the verified online ecosystem.

#### Key Features:
- ✅ Full procedural and sandbox gameplay.
- ✅ Modding-friendly and flexible for testing ideas or custom experiences.
- ❌ No access to online marketplace or public trade systems.
- ❌ Items and characters cannot be transferred to the online ecosystem.
- ⚠️ Potential for tampering or cheat-based item generation.

#### Use Case:
Ideal for players who prefer single-player experiences, modders, or those without internet access.

---

### 🔐 Integrity System

To clearly separate and identify item origins, all items will include metadata showing their validation state:

| Status | Tag | Description |
|--------|-----|-------------|
| ✅ Verified | 🟢 Green | Crafted and validated via online profile. Eligible for trading. |
| ⚠️ Pending | 🟡 Yellow | Created but not yet verified. Requires server sync. |
| ❌ Unverified | 🔴 Red | Offline-mode or modded content. Not eligible for trading. |

---

### ⚖️ Summary of Advantages

| Feature | Online Save | Offline Save |
|--------|-------------|--------------|
| Secure Trading | ✅ Yes | ❌ No |
| Modding Support | ❌ No | ✅ Yes |
| Online Missions | ✅ Yes | ❌ No |
| Scan Data Tracking | ✅ Synced | ✅ Local |
| Economy Safety | ✅ Enforced | ❌ No guarantee |

---

### 💡 Design Note:
Scans or data collected in offline mode could potentially be synced to online saves **with restrictions** (e.g., passive unlocks, not direct item blueprints). However, **items must always be created and verified in online mode** to maintain fairness.
