# Super Shooter – Unity

Joshua Antonio-Rodriguez:EN.601.257

A small shooter built in Unity following **Hands-On Unity Game Development (4th Edition)**, 

## Controls
- **WASD** – move
- **Mouse** – turn
- **Left click** – shoot

## How to play
Open `Assets/Scenes/test` and press Play. Enemies spawn in waves and head for your base (the cube). Shoot them before they destroy you or the base.
- **Win:** kill every enemy after all waves finish → `WinScreen`
- **Lose:** player or base Life reaches 0 → `LoseScreen`

## What each chapter added

**Chapter 2 – Crafting Scenes and Game Elements**
- Built the scene from primitives (floor, walls, capsules) and organized the Hierarchy
- Transforms, components, and prefabs

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

## Project structure
```
Assets/
  Scripts/           all C# scripts
  Scenes/            test, WinScreen, LoseScreen
  Prefab/            Enemy, Enemy variants, PlayerBullet, Enemy Bullet, Wall
  Material/
  PhysicsMaterials/
```

## Notes
- The Player is based on the Enemy prefab, so enemy-only components (AI child, NavMeshAgent) are disabled on it.
