using UnityEngine;

public enum SpawnType
{
    None,
    Dialogue,
    Monster,
    Battle
}

public class SpawnSpat : MonoBehaviour
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
    }

    private void OnTriggerEnter2D (Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StartSpawn();
        }
    }

    private void StartSpawn()
    {
        switch (_spawnType)
        {
            case SpawnType.Dialogue:
                UIExtension.OpenEndingUI();
                this.gameObject.SetActive(false);
                break;

            case SpawnType.Battle:
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
}
