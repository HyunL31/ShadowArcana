using Cysharp.Threading.Tasks;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueUI : UIBase
{
    [SerializeField] private GameObject _systemDialogue;
    [SerializeField] private GameObject _characterDialogue;
    [SerializeField] private TextMeshProUGUI _speaker;
    [SerializeField] private TextMeshProUGUI _content;
    [SerializeField] private TextMeshProUGUI _systemText;
    [SerializeField] private Image _background;
    [SerializeField] private Image _character;

    private bool _isTyping = false;

    private CancellationTokenSource cts;

    private void Start()
    {
        GameManager.Instance.OnCloseChoice = () => ShowDialogue(GetCurrentID());
    }

    private void OnEnable()
    {
        ShowDialogue(GetCurrentID());

        if (GetCurrentID().Contains("End"))
        {
            _background.gameObject.SetActive(true);
        }
        else
        {
            _background.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !UIManager.Instance.GetIsOpen(UIType.Choice))
        {
            if (_isTyping)
            {
                _isTyping = false;
            }
            else
            {
                MoveToNext(GetCurrentID());
            }
        }
    }

    private void ShowDialogue(string id)
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

            if (GetCurrentID().Contains("End"))
            {
                _character.gameObject.SetActive(false);
            }
            else
            {
                _character.gameObject.SetActive(true);

                string path = $"Image/{data.CharacterID}";
                ResourceManager.Instance.LoadAsset<Sprite>(path, (sprite) =>
                {
                    _character.sprite = sprite;
                });
            }

            CancellationTokenSource cts = new CancellationTokenSource();
            TypingEffect(cts.Token).Forget();
        }

        if (data.BGM != string.Empty)
        {
            SoundManager.Instance.SetDialogueBGM(data.BGM);
        }

        if (data.Background != string.Empty)
        {
            SetBackground();
        }
    }

    private void MoveToNext(string id)
    {
        string nextID = GameDataManager.Instance.GetScenarioData(id).NextID;
        GameManager.Instance.SetCurrentDialogueID(nextID);

        if (nextID == "Stage" || nextID == "Battle")
        {
            GameManager.Instance.OnDialogueEnd?.Invoke();

            UIExtension.CloseDialogueUI();
        }
        else if (nextID.Contains("Choice"))
        {
            UIExtension.OpenChoiceUI();
        }
        else if (nextID == "End")
        {
            UIExtension.OpenClearUI();
            UIExtension.CloseDialogueUI();
        }
        else if (nextID == "0")
        {
            UIExtension.CloseDialogueUI();
        }

        ShowDialogue(GetCurrentID());
    }

    private async UniTaskVoid TypingEffect(CancellationToken token)
    {
        _isTyping = true;

        string data = GameDataManager.Instance.GetScenarioData(GetCurrentID()).Content;
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

    private string GetCurrentID()
    {
        return GameManager.Instance.GetCurrentDialogueID();
    }

    private void SetBackground()
    {
        string background = GameDataManager.Instance.GetScenarioData(GetCurrentID()).Background;

        string path = $"Image/{background}";

        ResourceManager.Instance.LoadAsset<Sprite>(path, (sprite) =>
        {
            _background.sprite = sprite;
        });
    }
}