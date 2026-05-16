using UnityEngine;

public enum SpawnType
{
    None,
    Dialogue,
    Monster,
    Boss,
    Card
}

public class SpawnSpot : MonoBehaviour
{
    [SerializeField] private SpawnType _spawnType;
    [SerializeField] private string _spawnDataID;
    [SerializeField] private Collider2D _spawnCollider;

    private void Start()
    {
        if (_spawnType == SpawnType.Monster)
        {
            MonsterSpawn();
        }
        else if (_spawnType == SpawnType.Card)
        {
            CardSpawn();
        }
        else if (_spawnType == SpawnType.Boss)
        {
            BossSpawn();
        }
    }

    private void OnTriggerEnter2D (Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            CollisionSpawn(other);
        }
    }

    private void CollisionSpawn(Collider2D player)
    {
        switch (_spawnType)
        {
            case SpawnType.Dialogue:
                GameManager.Instance.SetCurrentDialogueID(_spawnDataID);
                UIExtension.OpenDialogueUI(_spawnDataID);
                player.gameObject.SetActive(false);
                this.gameObject.SetActive(false);
                break;

            case SpawnType.Card:
                GameManager.Instance.AddInventory(_spawnDataID);
                this.gameObject.SetActive(false);
                break;
        }
    }

    private void MonsterSpawn()
    {
        int random = Random.Range(1, 7);

        string path = $"Prefab/Monster_{random}";
        ResourceManager.Instance.InstantiatePrefab(path, this.gameObject.transform, (monster) => { });
    }

    private void CardSpawn()
    {
        int random = Random.Range(0, 22);

        string slotPath = "Prefab/RandomCard";
        ResourceManager.Instance.InstantiatePrefab(slotPath, this.gameObject.transform, (card) =>
        {
            _spawnDataID = $"Arcana_{random}";
        });
    }

    private void BossSpawn()
    {
        string path = $"Prefab/Monster_Boss";
        ResourceManager.Instance.InstantiatePrefab(path, this.gameObject.transform, (monster) => { });
    }
}