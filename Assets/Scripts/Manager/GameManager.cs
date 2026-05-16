using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Action OnInventoryChanged;
    public Action OnDialogueEnd;
    public Action OnCloseChoice;

    private string CurrentDialogueID { get; set; }

    private PlayerModel _playerModel = new PlayerModel();
    private GameObject _currentMap;
    private GameObject _currentPlayer;

    private void Awake()
    {
        Instance = this;
    }

    public int GetStage()
    {
        return _playerModel.Stage;
    }

    public void Load()
    {
        _playerModel = NetworkManager.Instance.LoadSaveData();
    }

    public void NewGame()
    {
        _playerModel = NetworkManager.Instance.GetDefaultData();
    }

    public void Save()
    {
        NetworkManager.Instance.SaveData(_playerModel);
    }

    public HashSet<string> GetInventory()
    {
        return _playerModel.Inventory;
    }

    public void AddInventory(string id)
    {
        if (_playerModel.Inventory.Contains(id))
        {
            return;
        }

        _playerModel.Inventory.Add(id);
        OnInventoryChanged?.Invoke();
    }

    public void SetScore(int score)
    {
        if (score < 0)
        {
            _playerModel.Minor++;
        }
        else if (score > 0)
        {
            _playerModel.Major++;
        }
    }

    public int GetMinorScore()
    {
        return _playerModel.Minor;
    }

    public int GetMajorScore()
    {
        return _playerModel.Major;
    }

    public void SetStage()
    {
        _playerModel.Stage++;
    }

    public string GetCurrentDialogueID()
    {
        return CurrentDialogueID;
    }

    public void SetCurrentDialogueID(string id)
    {
        CurrentDialogueID = id;
    }

    public void SetMap()
    {
        string path = $"Map_{GetStage()}";
        GameObject mapResource = Resources.Load<GameObject>(path);
        GameObject _map = Instantiate(mapResource);

        _currentMap = _map;
    }

    public void SetPlayer()
    {
        string playerPath = "Prefab/Player";
        Transform map = _currentMap.transform;

        ResourceManager.Instance.InstantiatePrefab(playerPath, map, (prefab) =>
        {
            CameraMoving camera = Camera.main.GetComponent<CameraMoving>();
            camera.SetPlayer(prefab);

            OnDialogueEnd = () =>
            {
                UIExtension.OpenInventory();
                prefab.SetActive(true);
            };

            _currentPlayer = prefab;
        });
    }

    public void DestroyMap()
    {
        if (_currentMap == null) Debug.Log("d");

        Destroy(_currentMap);

        _currentMap = null;
    }

    public void DestroyPlayer()
    {
        if (_currentMap == null) Debug.Log("a");

        Destroy(_currentPlayer);

        _currentPlayer = null;
    }
}