using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.UI;

public class UIManager : MonoBehaviour
{
    public enum UIType
    {
        None,
        Title
    }

    [SerializeField] private Transform canvas;
    private Dictionary<UIType, GameObject> _uiType = new Dictionary<UIType, GameObject>();

    public static UIManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        InitTitle();
    }

    private void InitTitle()
    {
        CreateUI(UIType.Title);
    }

    private string GetUIPath(UIType type)
    {
        if (type == UIType.Title)
        {
            return "UI/Title";
        }

        return null;
    }

    public void CreateUI(UIType type)
    {
        if (_uiType.ContainsKey(type))
        {
            _uiType[type].SetActive(true);
        }
        else
        {
            ResourceManager.Instance.InstantiatePrefab(GetUIPath(type), canvas, (ui) =>
            {
                _uiType.Add(type, ui);
            });
        }
    }

    public void CloseUI(UIType type)
    {
        if (_uiType.ContainsKey(type))
        {
            _uiType[type].SetActive(false);
        }
    }
}
