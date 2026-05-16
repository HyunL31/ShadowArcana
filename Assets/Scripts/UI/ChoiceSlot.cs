using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ChoiceSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Image slotImage;
    [SerializeField] private Button choiceButton;
    [SerializeField] private RectTransform _rect;

    public string CardID { get; set; }

    private void Start()
    {
        choiceButton.onClick.AddListener(() =>
        {
            OnClickChoiceButton(CardID);
        });
    }

    public void SetCardID(string id)
    {
        CardID = id;

        SetCardImage();
    }

    private void SetCardImage()
    {
        string path = $"Image/Arcana[{CardID}]";

        ResourceManager.Instance.LoadAsset<Sprite>(path, (sprite) =>
        {
            slotImage.sprite = sprite;
        });
    }

    private void OnClickChoiceButton(string id)
    {
        GameManager.Instance.SetCurrentDialogueID(GetReturnID(id));

        int score = GameDataManager.Instance.GetArcanaData(id).Score;
        GameManager.Instance.SetScore(score);

        UIExtension.CloseChoiceUI();
        GameManager.Instance.OnCloseChoice?.Invoke();

        SoundManager.Instance.SetOnClickSFX("Click");

        UIExtension.CloseCardDescription();
    }

    private string GetReturnID(string id)
    {
        string choiceID = GameManager.Instance.GetCurrentDialogueID();
        List<string> arcanaID = GameDataManager.Instance.GetChoiceData(choiceID).ArcanaID;
        List<string> returnID = GameDataManager.Instance.GetChoiceData(choiceID).ReturnID;

        bool isMatch = false;
        int index = 0;

        for (int i = 0; i < arcanaID.Count; i++)
        {
            if (id == arcanaID[i])
            {
                isMatch = true;
                index = i;

                break;
            }
        }

        if (isMatch)
        {
            return returnID[index];
        }
        else
        {
            return returnID[returnID.Count - 1];
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        CardDescription card = UIExtension.OpenCardDescription();

        card.SetCardInfo(CardID);
        card.SetPosition(_rect);

        SoundManager.Instance.SetOnClickSFX("Click");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        UIExtension.CloseCardDescription();
    }
}