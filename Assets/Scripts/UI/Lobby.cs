using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Lobby : UIBase
{
    [SerializeField] private Button _startButton;
    [SerializeField] private TextMeshProUGUI _stageText;

    private void OnEnable()
    {
        SetStage();
        SoundManager.Instance.SetDialogueBGM("Calm");
        _startButton.onClick.AddListener(OnClickStageStart);
    }

    private void SetStage()
    {
        string stage = $"스테이지 {GameManager.Instance.GetStage()}";
        _stageText.text = stage;
    }

    private void OnClickStageStart()
    {
        GameManager.Instance.SetMap();
        GameManager.Instance.SetPlayer();

        UIManager.Instance.CloseBackgroundUI();
        SoundManager.Instance.SetOnClickSFX("Click");

        UIExtension.CloseLobby();
    }
}
