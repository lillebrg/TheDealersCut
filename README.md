# Echo Runner

Rytmebaseret platformspil bygget i Unity.

## Sådan kører du projektet
1. Klon repo'et
2. Åbn i **Unity 2022 LTS** (samme version som i `ProjectSettings/ProjectVersion.txt`)
3. Åbn scenen: `Assets/Scenes/Main.unity`
4. Play ▶️

## Kontroller
- WASD: gå
- Mus: kig
- E: Interact
- Space / Tap: Hop

## Mappeinfo
- `Assets/` kildedata (scripts, prefabs, scenes) + `.meta` filer
- `Packages/` + `ProjectSettings/` til dependency/version
- `Library/` og builds er **ignoreret**

## Build
Lav build i **File → Build Settings…** og læg output i `Builds/` (IGNORERET).
Upload en zip i **GitHub Releases**.

