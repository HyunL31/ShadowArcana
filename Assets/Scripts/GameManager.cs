using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private PlayerModel _playerModel = new PlayerModel();

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
        _playerModel.Inventory.Add("Arcana_00");
        _playerModel.Inventory.Add("Arcana_18");
    }

    public int GetStage()
    {
        return _playerModel.Stage;
    }

    public void Load()
    {
        _playerModel = NetworkManager.Instance.LoadSaveData();
    }

    public void Save()
    {
        NetworkManager.Instance.SaveData(_playerModel);
    }

    public HashSet<string> GetInventory()
    {
        return _playerModel.Inventory;
    }
}