using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameDataManager : MonoBehaviour
{
    public static GameDataManager Instance { get; set; }

    private void Awake()
    {
        Instance = this;

        GameUtil.LoadFullData();
    }

    [Serializable]
    private class SerializationWrapper<T>
    {
        public List<T> data;
    }

    public Dictionary<string, Arcana> ArcanaDataList { get; set; } = new Dictionary<string, Arcana>();
    public Dictionary<string, Scenario> ScenarioDataList { get; set; } = new Dictionary<string, Scenario>();
    public Dictionary<string, Choice> ChoiceDataList { get; set; } = new Dictionary<string, Choice>();
    public Dictionary<string, Character> CharacterDataList { get; set; } = new Dictionary<string, Character>();

    private Dictionary<string, T> LoadData<T>(string tableName) where T : GameDataBase
    {
        string resourcePath = $"JsonOutput/{tableName}";
        TextAsset textAsset = Resources.Load<TextAsset>(resourcePath);

        if (textAsset == null)
        {
            Debug.Log("리소스 X");
            return new Dictionary<string, T>();
        }

        try
        {
            string jsonString = textAsset.text;

            string wrappedJson = "{\"data\":" + jsonString + "}";
            SerializationWrapper<T> wrapper = JsonUtility.FromJson<SerializationWrapper<T>>(wrappedJson);

            if (wrapper != null && wrapper.data != null)
            {
                Debug.Log($"{typeof(T).Name} 데이터를 {wrapper.data.Count}개 로드했습니다.");
                return wrapper.data.ToDictionary(data => data.ID.ToString());
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"[{typeof(T).Name} JSON 로드 오류] {e.Message}");
        }

        return new Dictionary<string, T>();
    }

    public void LoadScenarioData(string jsonPath)
    {
        ScenarioDataList = LoadData<Scenario>(jsonPath);
    }

    public void LoadChoiceData(string jsonPath)
    {
        ChoiceDataList = LoadData<Choice>(jsonPath);
    }

    public void LoadArcanaData(string jsonPath)
    {
        ArcanaDataList = LoadData<Arcana>(jsonPath);
    }

    public void LoadCharacterData(string jsonPath)
    {
        CharacterDataList = LoadData<Character>(jsonPath);
    }

    public Scenario GetScenarioData(string id)
    {
        if (ScenarioDataList == null || string.IsNullOrEmpty(id))
        {
            return null;
        }

        return ScenarioDataList.TryGetValue(id, out var data) ? data : null;
    }

    public Choice GetChoiceData(string id)
    {
        if (ChoiceDataList == null || string.IsNullOrEmpty(id))
        {
            return null;
        }

        return ChoiceDataList.TryGetValue(id, out var data) ? data : null;
    }

    public Arcana GetArcanaData(string id)
    {
        if (ArcanaDataList == null || string.IsNullOrEmpty(id))
        {
            return null;
        }

        return ArcanaDataList.TryGetValue(id, out var data) ? data : null;
    }

    public Character GetCharacterData(string id)
    {
        if (CharacterDataList == null || string.IsNullOrEmpty(id))
        {
            return null;
        }

        return CharacterDataList.TryGetValue(id, out var data) ? data : null;
    }
}
