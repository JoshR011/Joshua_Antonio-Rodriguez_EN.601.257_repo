# Super Shooter – Unity

Joshua Antonio-Rodriguez:EN.601.257

A small shooter built in Unity following **Hands-On Unity Game Development (4th Edition)**, Chapters 2–10.

## Controls
- **WASD** – move
- **Mouse** – turn
- **Left click** – shoot

## How to play
Open `Assets/Scenes/test` and press Play. Enemies spawn in waves and head for your base (the BaseDamagePoint in the middle of the ProBuilder base). Shoot them before they destroy you or the base.
- **Win:** kill every enemy after all waves finish → `WinScreen`
- **Lose:** player or base Life reaches 0 → `LoseScreen`

## What each chapter added

**Chapter 2 – Crafting Scenes and Game Elements**
- Built the scene from primitives (floor, walls, capsules) and organized the Hierarchy
- Transforms, components, and prefabs

**Chapter 3 – Terrain and ProBuilder**
- Terrain with hills (Raise/Lower, Set Height, Smooth Height) and painted Terrain Layers (Grass, Mud, Rock)
- ProBuilder base replacing the primitive floor and walls: subdivided plane, extruded walls, beveled edges, and a sunken pit in the middle

**Chapter 4 – Importing and Integrating Assets**
- Imported Asset Store packs: Sci-Fi Styled Modular Pack, SciFiWarrior PBR HP Polyart, Realistic Terrain Textures Lite
- Converted pack materials to URP with the Render Pipeline Converter
- Replaced primitives with models (robot on Player/Enemy, base damage point, cabinet, console, art props), keeping the original colliders and scripts
- Re-baked the NavMesh on the ProBuilder base

**Chapter 5 – C# and Visual Scripting**
- First script (`MyFirstScript`), fields, and Unity event functions

**Chapter 6 – Movement and Spawning**
- `PlayerMovement` and `PlayerShooting` using the new Input System
- `ForwardMovement` bullets, `AutoDestroy`, and `WaveSpawner` enemy waves

**Chapter 7 – Collisions and Health**
- Colliders and physics profiles (static, dynamic, kinematic, trigger)
- Layers (Player, Enemy, PlayerBullet, EnemyBullet) + Layer Collision Matrix
- `Life`, `ContactDamager`, `ContactDestroyer`
- Rigidbody-based `PlayerMovement` with drag, constraints, and a frictionless physics material

**Chapter 8 – Victory or Defeat**
- Singleton managers: `ScoreManager`, `EnemyManager`, `WavesManager`
- `ScoreOnDeath`, `Enemy`, updated `WaveSpawner`
- `WavesGameMode` with win/lose conditions and scene loading
- UnityEvents (`Life.onDeath`, managers' `onChanged`)

**Chapter 9 – Enemy AI**
- `Sight` sensor (distance, view angle, line-of-sight) with gizmos
- `EnemyFSM` with four states: GoToBase, AttackBase, ChasePlayer, AttackPlayer
- Baked NavMesh + NavMeshAgent pathfinding
- Enemies face their target and shoot

**Chapter 10 – URP and Shader Graph**
- `Disintegrate` material (URP Simple Lit, cloud noise texture with Alpha From Gray Scale, Alpha Clipping) on `DisintegrationTest`
- `Water` Lit Shader Graph:
  - two scrolling textures (Time → Multiply → Tiling And Offset) combined with Add
  - transparent surface with an `Opacity` property
  - vertex waves (Position → Split → Sine → Combine → Vertex Position)
- `WaterMaterial` on the water plane in the base's pit

## Project structure
```
Assets/
  Scripts/           all C# scripts
  Scenes/            test, WinScreen, LoseScreen
  Prefab/            Enemy, Enemy variants, PlayerBullet, Enemy Bullet, Wall
  Material/          materials + Water shader graph
  Textures/          noise + water textures
  Terrain/           GameTerrain + terrain layers
  PhysicsMaterials/
  Imported packs:    Sci-Fi Styled Modular Pack, SciFiWarriorPBRHPPolyart, ObjectiveEnvironment_Assets
```

## Notes
- The Player is based on the Enemy prefab, so enemy-only components (AI child, NavMeshAgent) are disabled on it.
- The Player Rigidbody has gravity off and Y position frozen (Chapter 7), so the player can move over the pit.
- Oversized demo files from the terrain texture pack were removed to stay under GitHub's 100 MB limit.