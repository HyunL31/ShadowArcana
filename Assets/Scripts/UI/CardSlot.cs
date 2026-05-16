using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Image _slotImage;
    [SerializeField] private RectTransform _rect;

    private string CardID { get; set; }

    public void SetCardID(string id)
    {
        CardID = id;
    }

    public void SetCardSprite(string id)
    {
        string path = $"Image/Arcana[{id}]";
        ResourceManager.Instance.LoadAsset<Sprite>(path, (sprite) =>
        {
            _slotImage.sprite = sprite;
        });
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
