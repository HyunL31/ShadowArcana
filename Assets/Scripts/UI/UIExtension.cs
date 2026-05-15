using System;
using UnityEngine;

public enum UIRootType
{
    None,
    BackgroundUI,
    MainUI,
    PopupUI,
    FrontUI
}

public enum UIType
{
    None,
    Title,
    Lobby,
    Loading,
    Ending,
    HPBar,
    Win,
    Die
}

public static class UIExtension
{
    public static string GetUIPath(UIRootType root, UIType type)
    {
        string path = $"UI/{root}/{type}";

        return path;
    }

    public static void InitTitle()
    {
        UIManager.Instance.OpenUI(UIRootType.FrontUI, UIType.Title);
    }

    public static void OpenLobby()
    {
        UIManager.Instance.CloseUI(UIType.Title);
        UIManager.Instance.OpenUI(UIRootType.MainUI, UIType.Lobby);
    }

    public static void OpenLoading()
    {
        UIManager.Instance.OpenUI(UIRootType.FrontUI, UIType.Loading);
    }

    public static void CloseLoading()
    {
        UIManager.Instance.CloseUI(UIType.Loading);
    }

    public static void OpenEndingUI()
    {
        UIManager.Instance.OpenUI(UIRootType.MainUI, UIType.Ending);
    }
}
