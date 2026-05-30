# EnemySpawnConfig

Configure enemy spawn rates and difficulty-specific spawn settings in R.E.P.O.

## Features

- Adjust global enemy spawn count multiplier
- Configure per-difficulty enemy spawn multipliers (Difficulty 1, 2, 3)
- Disable all enemy spawning for peaceful/no-combat mode
- Independent scaling for each difficulty tier
- Automatic enemy list synchronization

## Installation

1. Install [BepInEx 5.x](https://github.com/BepInEx/BepInEx/releases) for R.E.P.O.
2. Download the latest `EnemySpawnConfig.dll` from releases
3. Place `EnemySpawnConfig.dll` in `BepInEx/plugins/`
4. Launch the game to generate config file

## Configuration

Configuration file: `BepInEx/config/maxenterme.EnemySpawnConfig.cfg`

| Section | Key | Type | Default | Description |
|---------|-----|------|---------|-------------|
| General | SpawnMultiplier | int | 100 | Global multiplier for enemy group counts (100 = 100%, 0 = no enemies). Range: 0-500 |
| General | DisableAllEnemies | bool | false | Set to true to disable all enemy spawning (peaceful mode). |
| PerDifficulty | Difficulty1Multiplier | int | 100 | Multiplier for Difficulty 1 (easiest) enemy groups (100 = 100%). Range: 0-500 |
| PerDifficulty | Difficulty2Multiplier | int | 100 | Multiplier for Difficulty 2 (medium) enemy groups (100 = 100%). Range: 0-500 |
| PerDifficulty | Difficulty3Multiplier | int | 100 | Multiplier for Difficulty 3 (hardest) enemy groups (100 = 100%). Range: 0-500 |

## Build

```bash
dotnet build -c Release
```

Output: `bin/Release/netstandard2.1/EnemySpawnConfig.dll`


## AI Disclosure

This mod was developed with the assistance of AI (Claude by Anthropic). All code has been reviewed and tested by the developer.

## License

MIT
