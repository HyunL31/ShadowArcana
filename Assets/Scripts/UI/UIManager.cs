using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Transform _backgroundRoot;
    [SerializeField] private Transform _mainRoot;
    [SerializeField] private Transform _popupRoot;
    [SerializeField] private Transform _frontRoot;

    private Dictionary<UIType, GameObject> _uiType = new Dictionary<UIType, GameObject>();
    private HashSet<UIType> _openedUI = new HashSet<UIType>();

    public static UIManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        UIExtension.InitTitle();
    }

    private Transform GetUIRoot(UIRootType rootType)
    {
        Transform root = null;

        if (rootType == UIRootType.BackgroundUI)
        {
            root = _backgroundRoot;
        }
        else if (rootType == UIRootType.MainUI)
        {
            root = _mainRoot;
        }
        else if (rootType == UIRootType.PopupUI)
        {
            root = _popupRoot;
        }
        else if (rootType == UIRootType.FrontUI)
        {
            root = _frontRoot;
        }

        return root;
    }

    private void CreateUI(UIRootType root, UIType type)
    {
        if (!_uiType.ContainsKey(type))
        {
            string path = UIExtension.GetUIPath(root,type);
            GameObject ui = (GameObject)Resources.Load(path);

            Transform rootTransform = GetUIRoot(root);

            GameObject gobj = Instantiate(ui, rootTransform);
            
            if (gobj != null)
            {
                _uiType[type] = gobj;
                _openedUI.Add(type);
            }
        }
    }

    public void GetCreatUI(UIRootType root, UIType type)
    {
        if (_uiType.ContainsKey(type))
        {
            OpenUI(root, type);
        }
        else
        {
            CreateUI(root, type);
        }
    }

    public void CloseUI(UIType type)
    {
        if (_openedUI.Contains(type))
        {
            _uiType[type].SetActive(false);
            _openedUI.Remove(type);
        }
    }

    public void OpenUI(UIRootType root, UIType type)
    {
        if (_uiType.ContainsKey(type))
        {
            if (!_openedUI.Contains(type))
            {
                _uiType[type].SetActive(true);
                _openedUI.Add(type);
            }
        }
        else
        {
            GetCreatUI(root, type);
        }
    }

    public void CloseBackgroundUI()
    {
        _backgroundRoot.gameObject.SetActive(false);
    }
}
