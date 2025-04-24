using UnityEngine;
using System.IO;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;

    private string savePath;
    public GameData data;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            savePath = Path.Combine(Application.persistentDataPath, "gameData.json");
            LoadData();
        }
        else
        {
            Destroy(gameObject);
        }

        Debug.Log(Application.persistentDataPath);
    }

    public void LoadData()
    {
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            data = JsonUtility.FromJson<GameData>(json);
        }
        else
        {
            data = new GameData();
            SaveData(); // 최초 저장
        }
    }

    public void SaveData()
    {
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(savePath, json);
    }

    public void TryUpdateBestCoin(int currentCoin)
    {
        if (currentCoin > data.bestCoinCount)
        {
            data.bestCoinCount = currentCoin;
            SaveData();
            //Debug.Log("신기록 갱신! 저장됨.");
        }
    }

    public int GetBestCoin()
    {
        return data.bestCoinCount;
    }
}
