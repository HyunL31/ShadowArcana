using UnityEngine;

public class ClearUI : UIBase
{
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameManager.Instance.SetCurrentDialogueID(SetEndingID());
            GameManager.Instance.SetStage();

            GameManager.Instance.Save();

            GameManager.Instance.DestroyMap();
            GameManager.Instance.DestroyPlayer();
            UIExtension.CloseInventory();

            UIExtension.OpenLoading();
            UIExtension.OpenDialogueUI(GameManager.Instance.GetCurrentDialogueID());

            UIExtension.CloseClearUI();
        }
    }

    private string SetEndingID()
    {
        int minor = GameManager.Instance.GetMinorScore();
        int major = GameManager.Instance.GetMajorScore();

        if (minor > major)
        {
            return "Bad_End_01";
        }
        else if (major > minor)
        {
            return "Good_End_01";
        }
        else
        {
            return "Normal_End_01";
        }
    }
}
