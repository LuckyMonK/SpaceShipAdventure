using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/DataManager", order = 1)]
public class DataManager : ScriptableObject
{
    public LevelData[] levels;
}
//сущность для scriptable object для указания сцены для запуска из эдитора
[Serializable]
public class LevelData {
    public int levelNumber;
    public int sceneNumber;
}
//сущность для сохранения состояния
[Serializable]
public class SaveLevelData
{
    public int levelNumber;
    public LevelState state;

    public SaveLevelData(int levelNumber, LevelState state)
    {
        this.levelNumber = levelNumber;
        this.state = state;
    }
}

[Serializable]
public class SaveData {
    public int currentLvl;
    public SaveLevelData[] data;
}
public enum LevelState { 
    Open,
    Close,
    Passed
}