using UnityEngine;
using UnityEngine.UI;

public class CardSlot : MonoBehaviour
{
    [SerializeField] private Image slotImage;

    private string cardID;

    private void Start()
    {
        SetCardSprite(cardID);
    }

    public void SetCardID(string id)
    {
        cardID = id;
    }

    private void SetCardSprite(string id)
    {
        string path = $"Image/Arcana[{id}]";
        ResourceManager.Instance.LoadAsset<Sprite>(path, (sprite) =>
        {
            slotImage.sprite = sprite;
        });
    }
}
