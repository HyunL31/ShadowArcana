using System.Collections.Generic;
using UnityEngine;

public class ChoiceContent : UIBase
{
    [SerializeField] private Transform choiceParent;

    private List<string> cards = new List<string>();

    private void OnEnable()
    {
        InitChoice();
    }

    private void InitChoice()
    {
        var data = GameManager.Instance.GetInventory();
        string path = "Prefab/ChoiceSlot";

        foreach (string id in data)
        {
            if (cards.Contains(id))
            {
                continue;
            }

            ResourceManager.Instance.InstantiatePrefab(path, choiceParent, (prefab) =>
            {
                 ChoiceSlot slot = prefab.GetComponent<ChoiceSlot>();

                if (slot != null)
                {
                    slot.SetCardID(id);
                    cards.Add(id);
                }
            });
        }
    }
}