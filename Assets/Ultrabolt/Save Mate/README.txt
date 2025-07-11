# SaveMate System

SaveMate is a flexible and lightweight saving system for Unity that allows you to easily save and load game objects with custom data. It works with a simple `ISaveable` interface and handles position, rotation, prefab reference, and your custom script data.

## 🚀 Features

- Supports saving and loading of any object implementing `ISaveable`.
- Automatically assigns and persists unique IDs.
- Saves object data, position, rotation, and prefab reference.
- Spawns objects if they don't exist on load.
- Easy to extend and integrate.
- Scene-safe file paths.
- Context menu and keybind support (S to save, L to load).
- Save / Load Using Json for easy readable file or Binary for strange hieroglyphic-like language
---

## 💾 Saving and Loading

Press:
- `S` — Save game
- `L` — Load game

Or right-click the `SaveMateManager` in the Inspector and use:
- `Save Game`
- `Load Game`

---

## 📁 Save Location

Saves to: %persistentDataPath%/<SceneName>/<WorldName>.save


You can change the `worldName` in the Inspector or via script.

---

## 🧩 How to Use

### 1. Add `SaveMateManager` to your scene

- Place it on any GameObject.
- Optionally check `Dont Destroy On Load`.

### 2. Implement `ISaveable` on any component you want to save

csharp example

public class MySaveableComponent : MonoBehaviour, ISaveable
{
    public int health;
    public int ammo;

    public string id;
    public string UniqueID => id;

    public string PrefabName => "MyPrefab"; // Must match prefab name inside a `Resources` folder.

    public object SaveData()
    {
        return new MySaveData
        {
            health = this.health,
            ammo = this.ammo
        };
    }

    public void LoadData(object data)
    {
        if (data is MySaveData save)
        {
            this.health = save.health;
            this.ammo = save.ammo;
        }
    }

    [System.Serializable]
    private class MySaveData
    {
        public int health;
        public int ammo;
    }
}


Made with solo indie developer: Abdellah Naili
Feel free to contact me at my gmail account: nopelshinobi00@gmail.com ;)