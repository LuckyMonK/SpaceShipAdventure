using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_LvlPanel : MonoBehaviour
{
    public DataManager dm;
    [SerializeField]
    private GameObject btnPrefab;
    [SerializeField]
    private Color blockClr;

    private SaveData data;
    // Start is called before the first frame update
    void Start()
    {
        UpdateLvlsData();
    }


    void UpdateLvlsData() {
        var json = DataStorageHelper.GetJson();
        data = JsonUtility.FromJson<SaveData>(json);
        if (json.Equals(""))
        {
            data = new SaveData();
            data.data = new SaveLevelData[dm.levels.Length];

            for (int i = 0; i < dm.levels.Length; i++) {
                data.data[i] = new SaveLevelData(i, LevelState.Close);
            }

            data.data[0].state = LevelState.Open;
        }

        for (int i = 0; i < dm.levels.Length; i++) {

            var btn = Instantiate(btnPrefab, transform);
            var btncmp = btn.GetComponent<Button>();

            if (data.data[i].state is LevelState.Close)
            {
                btncmp.enabled = false;
                btn.transform.Find("Text").GetComponent<Text>().text = i.ToString();
                btn.GetComponent<Image>().color = blockClr;
            }
            else if (data.data[i].state is LevelState.Passed)
            {
                btn.transform.Find("Text").GetComponent<Text>().text = "Passed";
            }
            else
            {
                btn.transform.Find("Text").GetComponent<Text>().text = i.ToString();
                int j = i;
                btncmp.onClick.AddListener(() => { SceneLoader(j); });
            }
        }
        
    }

    public void SceneLoader(int i)
    {
        data.currentLvl = i;
        var json = JsonUtility.ToJson(data);
        //using (StreamWriter writetext = File.AppendText(DataStorageHelper.GetPath()))
        //{
        //    writetext.Write(json);
        //    writetext.Close();
        //}
        File.WriteAllText(DataStorageHelper.GetPath(), json);
        
        SceneManager.LoadScene(dm.levels[i].sceneNumber, LoadSceneMode.Single);
    }
}
