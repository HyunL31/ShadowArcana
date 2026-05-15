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
        _startButton.onClick.AddListener(OnClickStageStart);
    }

    private void SetStage()
    {
        string stage = $"스테이지 {GameManager.Instance.GetStage()}";
        _stageText.text = stage;
    }

    private void OnClickStageStart()
    {
        string path = $"Map_{GameManager.Instance.GetStage()}";
        GameObject mapResource = Resources.Load<GameObject>(path);
        GameObject map = Instantiate(mapResource);

        string playerPath = "Prefab/Player";
        ResourceManager.Instance.InstantiatePrefab(playerPath, map.transform, (prefab) =>
        {
            CameraMoving camera = Camera.main.GetComponent<CameraMoving>();
            camera.SetPlayer(prefab);
        });

        UIManager.Instance.CloseBackgroundUI();
        this.gameObject.SetActive(false);
    }
}
