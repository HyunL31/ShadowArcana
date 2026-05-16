using System.Collections.Generic;
using UnityEngine;

public class Inventory : UIBase
{
    [SerializeField] private Transform _slotParent;

    private List<string> cards = new List<string>();

    private void OnEnable()
    {
        InitSlot();
        GameManager.Instance.OnInventoryChanged = InitSlot;
    }

    private void InitSlot()
    {
        foreach (string id in GameManager.Instance.GetInventory())
        {
            if (cards.Contains(id))
            {
                continue;
            }

            string path = $"Prefab/CardSlot";

            ResourceManager.Instance.InstantiatePrefab(path, _slotParent, (prefab) =>
            {
                CardSlot card = prefab.GetComponent<CardSlot>();
                card.SetCardID(id);
                card.SetCardSprite(id);
                cards.Add(id);
            });
        }
    }
}