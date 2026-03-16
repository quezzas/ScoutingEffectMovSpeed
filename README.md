# ScoutingEffectMovSpeed

A Mount & Blade II: Bannerlord mod that makes the Scouting skill affect party movement speed on the campaign map.

[Link to NexusMods](https://www.nexusmods.com/mountandblade2bannerlord/mods/6347)

Tested on **v1.2.12** and **v1.3.15**

## Features

- Your scout's Scouting skill adds a speed bonus to party movement on the campaign map
- Bonus Scouting XP is gained while moving, scaled by terrain difficulty
- Everything is configurable via **CTXP.config** in the mod folder

## Configuration

### Speed Bonus Factor

Controls how much each point of Scouting skill boosts movement speed.
The bonus is a percentage of base speed, similar to the Cavalry bonus.

```
speedFactor=0.002
```

Example: At Scouting 200 with default factor, you get +40% movement speed.

### Party Filter

Choose which parties receive the scouting speed bonus. Combine multiple with `;` (OR logic).

```
appliesTo=1
```

| Value | Group |
|-------|-------|
| **1** | Your Main Party |
| **2** | Your Faction (all parties in your kingdom) |
| **3** | Everyone (any party with a scout) |
| **4** | All Lord Parties |
| **5** | Your Clan |

Examples: **1;5** = your party + your clan, **1;4;5** = your party + lords + your clan

### Custom Scouting XP

Grants bonus Scouting XP every in-game hour while moving, based on terrain:

```
gainBonusXP=true
```

| Terrain | XP |
|---------|----|
| Water | 20 |
| Mountain / Snow / Dune | 15 |
| Steppe / Desert / Swamp / Forest | 10 |
| Plain / Lake / River / Canyon | 5 |

Set to **false** to disable and only use the game's native XP gain.

## Localization

Translations included for: Spanish, Japanese, Korean, Polish, French, German, Italian, Chinese (Simplified & Traditional), Brazilian Portuguese, Turkish.

## Building

Requires the game DLLs. Set your Bannerlord install path when building:

```
cd src
dotnet build -c Release -p:GameFolder="D:\Your\Path\To\Mount & Blade II Bannerlord"
```

## License

[GPL-3.0](LICENSE)

Original mod by newpaladinx333 (2020-2021), maintained by quezzas (2026).
