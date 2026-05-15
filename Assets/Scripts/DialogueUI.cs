using Cysharp.Threading.Tasks;
using System.Threading;
using TMPro;
using UnityEngine;

public class DialogueUI : MonoBehaviour
{
    [SerializeField] private GameObject _systemDialogue;
    [SerializeField] private GameObject _characterDialogue;
    [SerializeField] private TextMeshProUGUI _speaker;
    [SerializeField] private TextMeshProUGUI _content;
    [SerializeField] private TextMeshProUGUI _systemText;

    private string CurrentID {  get; set; }
    private bool _isTyping = false;

    private CancellationTokenSource cts;

    private void Start()
    {
        CurrentID = "Good_End_01";
        ShowEndingDialogue(CurrentID);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (_isTyping)
            {
                _isTyping = false;
            }
            else
            {
                MoveToNext(CurrentID);
            }
        }
    }

    private void ShowEndingDialogue(string id)
    {
        var data = GameDataManager.Instance.GetScenarioData(id);
        
        if (data.CharacterID == string.Empty)
        {
            _characterDialogue.SetActive(false);

            _systemDialogue.SetActive(true);
            _systemText.text = data.Content;
        }
        else
        {
            string speakerName = GameDataManager.Instance.GetCharacterData(data.CharacterID).Name;

            _systemDialogue.SetActive(false);

            _characterDialogue.SetActive(true);
            _speaker.text = speakerName;

            CancellationTokenSource cts = new CancellationTokenSource();
            TypingEffect(cts.Token).Forget();
        }
    }

    private void MoveToNext(string id)
    {
        string nextID = GameDataManager.Instance.GetScenarioData(id).NextID;
        CurrentID = nextID;

        ShowEndingDialogue(CurrentID);
    }

    private async UniTaskVoid TypingEffect(CancellationToken token)
    {
        _isTyping = true;

        string data = GameDataManager.Instance.GetScenarioData(CurrentID).Content;
        _content.text = data;
        _content.maxVisibleCharacters = 0;

        while (data.Length > _content.maxVisibleCharacters)
        {
            _content.maxVisibleCharacters++;

            await UniTask.Delay(30);

            if (!_isTyping)
            {
                _content.maxVisibleCharacters = data.Length;

                cts?.Cancel();
                cts?.Dispose();
            }
        }
    }
}
