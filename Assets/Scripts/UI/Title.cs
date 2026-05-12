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
        Debug.Log("봉");
        UIManager.Instance.CloseUI(UIManager.UIType.Title);
    }
}
