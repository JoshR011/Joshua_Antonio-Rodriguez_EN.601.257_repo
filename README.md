# Super Shooter – Unity

Joshua Antonio-Rodriguez:EN.601.257

A small shooter built in Unity following **Hands-On Unity Game Development (4th Edition)**, Chapters 2–15 and 17.

## Controls
- **WASD** – move
- **Mouse** – turn
- **Left click** – shoot (hold to fire continuously)
- **Esc** – pause

## How to play
Open `Assets/Scenes/test` and press Play. A short camera flyover of the base plays first, then the camera follows the player from behind. Enemies spawn in waves and head for your base (the BaseDamagePoint in the middle of the ProBuilder base). Shoot them before they destroy you or the base. The game takes place on a rainy night.
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

**Chapter 11 – Particle Systems and Visual Effect Graph**
- Imported Unity's Particle Pack for smoke, fire, and muzzle flash textures/effects
- `Explosion` particle system (prefab):
  - Sphere shape, one burst of 100 particles, no looping, Stop Action = Destroy
  - `Explosion` material (URP Particles/Unlit, Transparent + Additive) using the `SmokeLoop` sprite sheet with Texture Sheet Animation
  - Color over Lifetime fade in/out, Limit Velocity over Lifetime (dampen), Rotation over Lifetime
- `Waterfall` particle system (prefab): Edge shape, gravity modifier, white → blue Color over Lifetime, placed on a hill
- `Bonfire` particle system (prefab): `Fire` material with an animated flame sprite sheet, plus a child smoke system
- Installed the Visual Effect Graph package (version matching URP 16)
- `Rain` VFX Graph (`Assets/VFX/Rain`):
  - 10,000 particles/sec in local space, manual bounds covering the base
  - Set Velocity Random (falling down), Set Position Random (spread over the base), Set Scale (stretched drops)
  - Gravity/Linear Drag removed, Orient Along Velocity, `SmokePuff` flipbook with Additive blending
- Scripted effects:
  - `ExplosionOnDeath` on the Enemy prefab – subscribes to `Life.onDeath` and spawns the `Explosion` prefab
  - `PlayerShooting` – new `muzzleEffect` field, plays `MuzzleFlash01` (under ShootPoint) when firing
  - `EnemyFSM` – new `muzzleEffect` field, plays `MuzzleFlash01` (under the AI object) at the end of `Shoot()`

**Chapter 12 – Lighting with URP**
- Night skybox (AllSkyFree) and regenerated ambient lighting
- Directional Light dimmed to dark gray, No Shadows, Realtime mode
- Two spotlights over opposite corners of the base:
  - Inner/Outer angle 90/120, Range 50, Intensity 300 (lowered from the book's 1000 to avoid washing out the floor)
  - Soft Shadows with Custom bias (Depth 0.25, Normal 0.25), Mixed mode
- `URP-HighFidelity` Additional Lights Shadow Atlas Resolution set to 1024
- Generate Lightmap UVs enabled on the Sci-Fi Styled Modular Pack models
- Base, cabinet, console, art props, and base damage point marked Static
- `Terrain` layer excluded from the spotlights' Culling Mask
- `New Lighting Settings` asset (reduced samples, Lightmap Resolution 20) and baked lightmaps
- 3×3 grid of Light Probe Groups (under `LightingProbes`) so moving objects pick up baked lighting

**Chapter 13 – Post-Processing**
- `PP Volume` (Global Volume) with `PP Volume Profile` in `Assets/Settings`
- Post Processing enabled on the Main Camera; HDR confirmed on in `URP-HighFidelity`
- Basic effects: Chromatic Aberration (0.25), Vignette (0.3 / smoothness 0.5), Motion Blur (0.25), Film Grain (1), White Balance (temperature −20)
- Advanced effects: Bloom (threshold 0.7, intensity 0.2), Depth of Field (Gaussian, start 10, end 20, max radius 1.5), Shadows Midtones Highlights (subtle adjustments)

**Chapter 14 – Audio**
- Imported music, a rain ambience loop, a gunshot, and an explosion sound into `Assets/Audio`
- Import settings: music and rain use Streaming (stereo); sound effects use Force To Mono, Decompress On Load, and Vorbis
- `music` and `Rain Ambience` objects with looping 2D AudioSources
- `Main Mixer` Audio Mixer with SFX and Music groups, a Lowpass Simple effect on Master, and three snapshots: default, `Pause` (lower SFX), and `Bomb Stun` (cutoff 1000 Hz)
- Explosion prefab plays its sound on spawn (Play On Awake, 3D)
- `PlayerShooting` and `EnemyFSM` – new `shootSound` field, played with the muzzle flash

**Chapter 15 – User Interface**
- Canvas with Canvas Scaler (Scale With Screen Size, 1920×1080, match height) and an EventSystem using the Input System UI module
- Imported the PixelUISciFiFree pack; 9-sliced the panel and button sprites; used its TextMesh Pro font
- HUD: player avatar, player health bar and base health bar (Filled images), and Score, Bullets, Waves, and Enemies labels, anchored to the screen corners
- Pause menu: sliced panel with a title and Resume/Quit buttons, hidden by default
- Scripts:
  - `LifeBar` – fills a bar from a target `Life` (player and base)
  - `PlayerBulletsUI`, `ScoreUI`, `EnemiesUI`, `WavesUI` – update the labels (Enemies/Waves use the managers' `onChanged` events)
  - `PlayerShooting` – limited `bulletsAmount`
  - `Pause` – Esc sets `Time.timeScale` to 0, shows the menu and cursor; Resume restores them
  - `QuitButton` – calls `Application.Quit()`
  - Player and enemies can't shoot while paused

**Chapter 17 – Animation, Cinemachine, and Timeline**
- `Player` Animator Controller (`Assets/Animations`) with `Velocity` (float) and `Shooting` (bool) parameters:
  - `LowerBody` layer: Idle ↔ Run
  - `UpperBody` layer: Idle, Run, and Idle_Shoot, using `UpperBodyMask` (legs masked out) with Override blending
- Controller applied to the robot on both the Player and the Enemy prefab, with Root Motion off
- Fire input set to Press And Release
- Scripts:
  - `PlayerShooting` – hold-to-fire with `fireRate` (`InvokeRepeating`) and sets the `Shooting` parameter
  - `VelocityAnimator` – sends the player's Rigidbody speed to the animator
  - `NavMeshAnimator` – sends the enemy's NavMeshAgent speed to the animator
  - `EnemyFSM` – turns `Shooting` on when firing and off while moving
- Cinemachine: `CM vcam1` follows the player from behind (Transposer offset (0, 3, -3), Composer aim)
- Dolly track intro: `Dolly Track` path over the base, `Dolly Cart`, and `CM vcam2` riding the cart and looking at the base
- Timeline `Intro` on the `Director` object: an Animation track plays `IntroDollyTrack` (cart from start to end in 5 seconds) and a Cinemachine track blends from `CM vcam2` to `CM vcam1`; Wrap Mode Hold
- Wave spawner start time delayed so enemies arrive after the intro

## Project structure
```
Assets/
  Scripts/           all C# scripts
  Audio/             music, rain, gunshot, explosion + Main Mixer
  Animations/        Player controller, UpperBodyMask, IntroDollyTrack
  Cutscene/          Intro timeline
  Sprites/           avatar image
  Scenes/            test, WinScreen, LoseScreen
  Prefab/            Enemy, Enemy variants, PlayerBullet, Enemy Bullet, Wall,
                     Explosion, Waterfall, Bonfire, MuzzleFlash01
  Material/          materials + Water shader graph, Explosion, Fire, RainySky
  Textures/          noise + water textures, Unity Particle Pack
  VFX/               Rain visual effect graph
  Settings/          URP assets, Lighting Settings, PP Volume Profile
  Terrain/           GameTerrain + terrain layers
  PhysicsMaterials/
  Imported packs:    Sci-Fi Styled Modular Pack, SciFiWarriorPBRHPPolyart, ObjectiveEnvironment_Assets, AllSkyFree, PixelUISciFiFree
```

## Notes
- The Player is based on the Enemy prefab, so enemy-only components (AI child, NavMeshAgent) are disabled on it.
- The Player Rigidbody has gravity off and Y position frozen (Chapter 7), so the player can move over the pit.
- Oversized demo files from the terrain texture pack were removed to stay under GitHub's 100 MB limit.
- The Player was moved out of the `Enemies` object, and its Rigidbody freezes X/Z rotation so it doesn't tilt.
- Before Chapter 17 the game used a first-person camera; Chapter 17 replaced it with the Cinemachine follow camera.
- The Player inherits `NavMeshAnimator` from the Enemy prefab; it does nothing there because the Player's NavMeshAgent is disabled.
