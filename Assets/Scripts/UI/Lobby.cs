using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Lobby : MonoBehaviour
{
    [SerializeField] private Button startButton;
    [SerializeField] private TextMeshProUGUI stageText;

    private void OnEnable()
    {
        Debug.Log("시작");
        SetStage();
        startButton.onClick.AddListener(OnClickStageStart);
    }

    private void SetStage()
    {
        string stage = $"스테이지 {GameManager.Instance.GetStage()}";
        stageText.text = stage;
    }

    private void OnClickStageStart()
    {
        string path = $"Map_{GameManager.Instance.GetStage()}";
        GameObject map = Resources.Load<GameObject>(path);
        Instantiate(map);

        UIManager.Instance.CloseBackgroundUI();
        this.gameObject.SetActive(false);
    }
}
