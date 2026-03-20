# MoteMancer Example Mods

This repository contains example mods for **MoteMancer**, demonstrating how to create, structure, and publish mods.

## Included Examples
- **Infinite Reach** – \<link to steam workshop>
- **Ignore Plane Blocking** – \<link to steam workshop>

---

## Setup

1. Clone or download this repository  
2. Install **MoteMancer** via Steam  
3. Copy required game assemblies:
```
<Steam Directory>\steamapps\common\MoteMancer\MoteMancer_Data\Managed\Assembly-CSharp.dll  
<Steam Directory>\steamapps\common\MoteMancer\MoteMancer_Data\Managed\UnityEngine.CoreModule.dll  
```
4. Place both files into:
```
Dependencies\
```
---

## Mod Structure

A mod consists of:

- DLL – compiled mod code (e.g. IgnorePlaneBlock.dll)  
- mod.json – metadata and configuration  
- Icon.png (optional) – shown in-game  
- preview.png (optional) – used for Steam Workshop  

---

## Mod Entry Class

Every mod must define an entry class:
```
    public class IgnorePlaneBlock {
        private HarmonySupport m_harmonySupport;

        public void OnEnable(Dictionary<string, object> dependencies) {
            if (!dependencies.TryGetValue("harmony_support", out var harmonySupportObj)) {
                throw new Exception("harmony_support not found");
            }
            
            ...
        }

        public void OnDisable() {
            ...
        }
    }
```
### Requirements

- OnEnable(Dictionary<string, object>) is required  
- OnDisable() is required  
- Missing either will prevent the mod from loading  

### Lifecycle

- OnEnable → called when a game with the mod is loaded  
- OnDisable → called when the mod is unloaded or the session ends  

---

## Harmony

These mods use the Harmony framework:  
https://github.com/pardeike/Harmony  

You should not include Harmony yourself. Instead:

- Add "harmony_support" as a dependency  
- Subscribe to it on Steam Workshop \<todo link>

This prevents version conflicts between mods.

### Example Patch
```
    [HarmonyPatch]
    public static class IgnorePlaneBlockPatches {
        [HarmonyPatch(typeof(StructureUtils), nameof(StructureUtils.BlockOpposingPlaneBuilding))]
        [HarmonyPostfix]
        public static void BuildAnyPlane(ref bool __result) {
            __result = false;
        }
    }
```
---

## Configuration (mod.json)

Each mod requires a `mod.json` file:
```
    {
        "id": "ignore_plane_block",
        "name": "Ignore Plane Blocking",
        "description": "Example mod that allows structure building on opposing planes.",
        "assembly": "IgnorePlaneBlock.dll",
        "entryType": "IgnorePlaneBlock",
        "version": "1.0.0",
        "isLibrary": false,
        "dependencies": [
            "harmony_support"
        ],
        "fileId": 0,
        "tags": [
            "Gameplay",
            "Cheat",
            "Example"
        ]
    }
```
### Field Reference

- id – Unique identifier  
- name – Display name (UI + Workshop title)  
- description – Workshop description  
- assembly – DLL filename  
- entryType – Entry class  
- version – Mod version  
- dependencies – Required mods  
- isLibrary – Hidden dependency-only mod  
- fileId – Workshop ID (0 = new upload)  
- tags – Workshop tags  

---

## Testing Mods

1. Build a project (e.g. IgnorePlaneBlock)  
2. Copy:
```
Examples\IgnorePlaneBlock\bin\Release\net48\IgnorePlaneBlock.dll  
Examples\IgnorePlaneBlock\mod.json  
```
3. Paste into:
```
My Documents\My Games\MoteMancer\Mods\IgnorePlaneBlock\
```
4. Optional:  
   - Icon.png  
   - preview.png  

5. Launch the game — the mod will appear in the Foundations menu  

---

## Uploading Mods

Mods in My Documents can be uploaded directly from the game.

### Behavior

- Upload button appears in the Foundations menu (Steam required)  
- First upload → creates a new Workshop item  
- Subsequent uploads → update existing item  

### Notes

- Uploading will overwrite:
  - description  
  - preview image  
- Use the Steam Workshop website for additional customization  

---

## Notes
- Mods run inside the game runtime. Any exceptions could break other parts of the game or cause your mod to not load. 
- Harmony patches are powerful. Use with caution.
- Dependencies must be installed and enabled or your mod will not load. 
