using UnityEngine;
using UnityEngine.UI;

public class Title : UIBase
{
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _newGameButton;

    private void OnEnable()
    {
        _startButton.onClick.AddListener(() =>
        {
            SoundManager.Instance.SetOnClickSFX("Click");

            GameManager.Instance.Load();
            UIExtension.OpenLobby();
            UIExtension.CloseTitle();
        });

        _newGameButton.onClick.AddListener(() =>
        {
            SoundManager.Instance.SetOnClickSFX("Click");

            GameManager.Instance.NewGame();
            UIExtension.OpenLobby();
            UIExtension.CloseTitle();
        });
    }
}
