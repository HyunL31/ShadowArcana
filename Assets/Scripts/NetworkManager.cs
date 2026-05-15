using System.IO;
using UnityEngine;

public class NetworkManager : MonoBehaviour
{
    public static NetworkManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    private string GetSavePath()
    {
        return Path.Combine(Application.persistentDataPath, "ShadowArcana.json");
    }

    public void SaveData(PlayerModel data)
    {
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(GetSavePath(), json);
    }

    public PlayerModel LoadSaveData()
    {
        string path = GetSavePath();

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);

            PlayerModel data = JsonUtility.FromJson<PlayerModel>(json);

            return data;
        }
        else
        {
            return GetDefaultData();
        }
    }

    public PlayerModel GetDefaultData()
    {
        var newSaveData = new PlayerModel();

        newSaveData.Stage = 1;
        newSaveData.LastMapPosition = new Vector3(-2.5f, 1.7f, 0);

        return newSaveData;
    }
}