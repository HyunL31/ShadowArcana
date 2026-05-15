using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private Transform _slotParent;

    private void Start()
    {
        InitSlot();
    }

    private void InitSlot()
    {
        foreach (string id in GameManager.Instance.GetInventory())
        {
            string path = $"Prefab/CardSlot";

            ResourceManager.Instance.InstantiatePrefab(path, _slotParent, (prefab) =>
            {
                CardSlot card = prefab.GetComponent<CardSlot>();
                card.SetCardID(id);
            });
        }
    }
}
