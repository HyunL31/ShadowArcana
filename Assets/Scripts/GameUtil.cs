using UnityEngine;

public static class GameUtil
{
    public static void LoadFullData()
    {
        GameDataManager.Instance.LoadArcanaData("Arcana");
        GameDataManager.Instance.LoadScenarioData("Scenario");
        GameDataManager.Instance.LoadChoiceData("Choice");
        GameDataManager.Instance.LoadCharacterData("Character");
    }
}
