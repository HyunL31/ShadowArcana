
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
    Dialogue,
    Choice,
    HPBar,
    Inventory,
    CardDescription,
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

    public static void CloseTitle()
    {
        UIManager.Instance.CloseUI(UIType.Title);
    }

    public static void OpenLobby()
    {
        UIManager.Instance.OpenUI(UIRootType.MainUI, UIType.Lobby);
    }

    public static void CloseLobby()
    {
        UIManager.Instance.CloseUI(UIType.Lobby);
    }

    public static void OpenLoading()
    {
        UIManager.Instance.OpenUI(UIRootType.FrontUI, UIType.Loading);
    }

    public static void CloseLoading()
    {
        UIManager.Instance.CloseUI(UIType.Loading);
    }

    public static void OpenDialogueUI(string id)
    {
        UIManager.Instance.OpenUI(UIRootType.FrontUI, UIType.Dialogue);

        GameManager.Instance.SetCurrentDialogueID(id);
    }

    public static void CloseDialogueUI()
    {
        UIManager.Instance.CloseUI(UIType.Dialogue);
    }

    public static void OpenInventory()
    {
        UIManager.Instance.OpenUI(UIRootType.MainUI, UIType.Inventory);
    }

    public static void CloseInventory()
    {
        UIManager.Instance.CloseUI(UIType.Inventory);
    }

    public static void OpenChoiceUI()
    {
        UIManager.Instance.OpenUI(UIRootType.PopupUI, UIType.Choice);
    }

    public static void CloseChoiceUI()
    {
        UIManager.Instance.CloseUI(UIType.Choice);
    }

    public static CardDescription OpenCardDescription()
    {
        UIBase uiBase = UIManager.Instance.OpenUI(UIRootType.PopupUI, UIType.CardDescription);
        CardDescription card = uiBase.GetComponent<CardDescription>();

        return card;
    }

    public static void CloseCardDescription()
    {
        UIManager.Instance.CloseUI(UIType.CardDescription);
    }

    public static void OpenClearUI()
    {
        UIManager.Instance.OpenUI(UIRootType.FrontUI, UIType.Win);
    }

    public static void CloseClearUI()
    {
        UIManager.Instance.CloseUI(UIType.Win);
    }

    public static void OpenOverUI()
    {
        UIManager.Instance.OpenUI(UIRootType.FrontUI, UIType.Die);
    }

    public static void CloseOverUI()
    {
        UIManager.Instance.CloseUI(UIType.Die);
    }
}
