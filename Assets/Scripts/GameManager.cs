using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private PlayerModel playerModel = new PlayerModel();

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Load();

        InitBasicCard();
    }

    private void InitBasicCard()
    {
        playerModel.Inventory.Add("Arcana_00");
        playerModel.Inventory.Add("Arcana_18");
    }

    public int GetStage()
    {
        Debug.Log(playerModel.Stage);
        return playerModel.Stage;
    }

    public void Load()
    {
        playerModel = NetworkManager.Instance.LoadSaveData();
    }

    public void Save()
    {
        NetworkManager.Instance.SaveData(playerModel);
    }

    public HashSet<string> GetInventory()
    {
        return playerModel.Inventory;
    }
}