using Gameplay.Helpers;
using Gameplay.ShipControllers.CustomControllers;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private DataManager dm;

    [SerializeField]
    private float timeLimit = 60f;

    [SerializeField]
    private Text timerTxt;

    private PlayerShipController player;
    private bool block = true;
    // Start is called before the first frame update
    void Start()
    {
        GameAreaHelper.NewSceneInit();
    }

    // Update is called once per frame
    void Update()
    {
        if (timeLimit <= 0 && block) {
            block = false;
            Win();
        }
    }

    private void FixedUpdate()
    {
        timeLimit -= Time.deltaTime;
        timerTxt.text = ((int)timeLimit).ToString();
    }

    void Win() {
        UpdateNextLevelState();

        SceneManager.LoadScene(0);
    }

    void UpdateNextLevelState()
    {
        SaveData json = JsonUtility.FromJson<SaveData>(DataStorageHelper.GetJson());
        json.data[json.currentLvl].state = LevelState.Passed;
        if (json.currentLvl + 1 < json.data.Length && json.data[json.currentLvl + 1].state != LevelState.Open) {
            json.data[json.currentLvl + 1].state = LevelState.Open;
        }

        var str = JsonUtility.ToJson(json);
        File.WriteAllText(DataStorageHelper.GetPath(), str);
    }
}
