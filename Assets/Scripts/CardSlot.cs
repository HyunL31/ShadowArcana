using UnityEngine;
using UnityEngine.UI;

public class CardSlot : MonoBehaviour
{
    [SerializeField] private Image _slotImage;

    private string CardID { get; set; }

    private void Start()
    {
        SetCardSprite(CardID);
    }

    public void SetCardID(string id)
    {
        CardID = id;
    }

    private void SetCardSprite(string id)
    {
        string path = $"Image/Arcana[{id}]";
        ResourceManager.Instance.LoadAsset<Sprite>(path, (sprite) =>
        {
            _slotImage.sprite = sprite;
        });
    }
}
