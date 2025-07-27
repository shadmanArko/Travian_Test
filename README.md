# 🏗️ Architecture Overview

## ✅ Key Architectural Principles

### ✅ MonoBehaviour Only for Views

- `PlayerView`, `ObstacleView`, `ScoreView`: Only these inherit from MonoBehaviour.
- Models & Presenters: Pure C# classes, no MonoBehaviour dependency.
- `GameManager`: Exception — inherits from MonoBehaviour for Unity lifecycle management.

### ✅ Zenject ScriptableObjectInstaller

- All dependency bindings are configured in a ScriptableObject.
- No MonoInstaller needed in the scene.
- Cleaner separation between scene setup and dependency configuration.
- Easily shareable between scenes.

### ✅ Automatic Initialization

- Presenters implement `IInitializable` and `IDisposable`.
- Zenject automatically calls `Initialize()` and `Dispose()`.
- No manual bootstrap code needed.
- Views are found via `FindObjectOfType<>()` in presenters.

---

## 🎮 MVP Pattern Implementation

### Player

- **Model (`IPlayerModel`)**: Pure C# class with reactive properties.
- **View (`PlayerView`)**: MonoBehaviour for input, physics, and collision.
- **Presenter (`IPlayerPresenter`)**: Pure C# class; finds view automatically.

### Obstacles

- **Model (`IObstacleModel`)**: Pure C# class managing state.
- **View (`ObstacleView`)**: MonoBehaviour for rendering.
- **Presenter (`IObstaclePresenter`)**: Pure C# class using factory pattern.

### Score

- **Model (`IScoreModel`)**: Pure C# class for score calculation.
- **View (`ScoreView`)**: MonoBehaviour for UI rendering.
- **Presenter (`IScorePresenter`)**: Pure C# class; finds view automatically.

---

## 🔁 Dependency Flow

- `ScriptableObjectInstaller`: Configures all bindings.
- Zenject: Resolves dependencies and calls `IInitializable`.
- Presenters: Find their respective views in the scene.
- Models: Handle business logic with reactive properties.
- Views: Handle Unity-specific rendering and input.

---

## 📡 Event Flow

- `GameStartEvent` → Resets player and score, starts obstacle spawning.
- `PlayerJumpEvent` → Published when player jumps.
- `ObstacleCollisionEvent` → Published when player hits an obstacle.
- `GameOverEvent` → Stops the game; published after collision.
- `ScoreUpdateEvent` → Published when score changes.

---

## 🎮 Controls

- **Space**: Jump (only when grounded)

---

## 🕹️ Game Rules

- Player automatically falls due to gravity.
- Obstacles spawn at random heights every 2 seconds.
- Obstacles move left at a constant speed.
- Score increases based on survival time (10 points per second).
- Game ends when the player collides with an obstacle.
