using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance {get; private set;}
    public string PlayerName;
    public int Score;
    public Text BestScore;
    public string TopPlayerName;
    public int TopPlayerScore;

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        LoadScore();
    }

    public void GetPlayerName(string name)
    {
        PlayerName = name; 
    }

    [System.Serializable]
    class SaveData
    {
        public int Score;
        public string PlayerName;
    }

    public void SaveScore()
    {
        SaveData data = new SaveData();
        data.PlayerName = TopPlayerName;
        data.Score = TopPlayerScore;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
        Debug.Log("SAVE SCORE" + data.Score);
    }

    public void LoadScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if(File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            TopPlayerName = data.PlayerName;
            TopPlayerScore = data.Score;
        }
        BestScore.text = $"Best Score: {TopPlayerName} : {TopPlayerScore} ";
        Debug.Log("Load SCORE" + TopPlayerScore);
    }
    
}
