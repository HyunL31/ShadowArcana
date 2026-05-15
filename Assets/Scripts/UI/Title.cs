using UnityEngine;
using UnityEngine.UI;

public class Title : MonoBehaviour
{
    [SerializeField] private Button startButton;
    [SerializeField] private Button newGameButton;

    private void Start()
    {
        startButton.onClick.AddListener(() =>
        {
            GameManager.Instance.Load();
            UIExtension.OpenLobby();
        });

        newGameButton.onClick.AddListener(UIExtension.OpenLobby);
    }
}
