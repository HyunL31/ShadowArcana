using UnityEngine;
using UnityEngine.UI;

public class Title : MonoBehaviour
{
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _newGameButton;

    private void Start()
    {
        _startButton.onClick.AddListener(() =>
        {
            GameManager.Instance.Load();
            UIExtension.OpenLobby();
        });

        _newGameButton.onClick.AddListener(UIExtension.OpenLobby);
    }
}
