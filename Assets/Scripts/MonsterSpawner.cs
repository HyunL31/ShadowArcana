using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    [SerializeField] private List<Transform> _monsterPos;

    private void Start()
    {
        for (int i = 0; i < _monsterPos.Count; i++)
        {
            int random = Random.Range(1, 7);

            string path = $"Prefab/Monster_{random}";
            ResourceManager.Instance.InstantiatePrefab(path, _monsterPos[i], (monster) => {});
        }
    }
}
