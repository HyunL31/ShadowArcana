using UnityEngine;
using UnityEngine.UI;

public class Title : MonoBehaviour
{
    [SerializeField] private Button startButton;

    private void Awake()
    {
        startButton.onClick.AddListener(CloseTitle);
    }

    private void CloseTitle()
    {
        //UIManager.Instance.CloseUI(UIManager.UIType.Title);
    }
}
