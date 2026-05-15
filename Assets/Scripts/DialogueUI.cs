using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;

public class DialogueUI : MonoBehaviour
{
    [SerializeField] private GameObject systemDialogue;
    [SerializeField] private GameObject characterDialogue;
    [SerializeField] private TextMeshProUGUI speaker;
    [SerializeField] private TextMeshProUGUI content;
    [SerializeField] private TextMeshProUGUI systemText;

    private string currentID = "Good_End_01";
    private bool isTyping = false;

    private CancellationTokenSource cts;

    private void Start()
    {
        ShowEndingDialogue(currentID);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (isTyping)
            {
                isTyping = false;
            }
            else
            {
                MoveToNext(currentID);
            }
        }
    }

    private void ShowEndingDialogue(string id)
    {
        var data = GameDataManager.Instance.GetScenarioData(id);
        
        if (data.CharacterID == string.Empty)
        {
            characterDialogue.SetActive(false);

            systemDialogue.SetActive(true);
            systemText.text = data.Content;
        }
        else
        {
            string speakerName = GameDataManager.Instance.GetCharacterData(data.CharacterID).Name;

            systemDialogue.SetActive(false);

            characterDialogue.SetActive(true);
            speaker.text = speakerName;

            CancellationTokenSource cts = new CancellationTokenSource();
            TypingEffect(cts.Token).Forget();
        }
    }

    private void MoveToNext(string id)
    {
        string nextID = GameDataManager.Instance.GetScenarioData(id).NextID;
        currentID = nextID;

        ShowEndingDialogue(currentID);
    }

    private async UniTaskVoid TypingEffect(CancellationToken token)
    {
        isTyping = true;

        string data = GameDataManager.Instance.GetScenarioData(currentID).Content;
        content.text = data;
        content.maxVisibleCharacters = 0;

        while (data.Length > content.maxVisibleCharacters)
        {
            content.maxVisibleCharacters++;

            await UniTask.Delay(30);

            if (!isTyping)
            {
                content.maxVisibleCharacters = data.Length;

                cts?.Cancel();
                cts?.Dispose();
            }
        }
    }
}
