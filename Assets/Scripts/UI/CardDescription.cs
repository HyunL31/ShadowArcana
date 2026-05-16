using TMPro;
using UnityEngine;

public class CardDescription : UIBase
{
    [SerializeField] private TextMeshProUGUI cardName;
    [SerializeField] private TextMeshProUGUI cardDescription;

    public void SetCardInfo(string id)
    {
        string name = GameDataManager.Instance.GetArcanaData(id).Name;
        string description = GameDataManager.Instance.GetArcanaData(id).Description;

        cardName.text = name;
        cardDescription.text = description;
    }

    public void SetPosition(RectTransform rect)
    {
        Vector3 targetPos = rect.position + new Vector3(rect.rect.width - 30f, 0, 0);

        this.transform.position = targetPos;
    }
}
