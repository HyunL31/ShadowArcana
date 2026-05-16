using UnityEngine;

public class OverUI : UIBase
{
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameManager.Instance.DestroyMap();
            GameManager.Instance.DestroyPlayer();

            GameManager.Instance.NewGame();

            UIExtension.OpenLoading();

            UIExtension.CloseInventory();
            UIExtension.CloseOverUI();
        }
    }
}
