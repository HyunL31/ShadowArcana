using UnityEngine;

public enum SpawnType
{
    None,
    Dialogue,
    Battle
}

public class SpawnSpat : MonoBehaviour
{
    [SerializeField] private SpawnType _spawnType;
    [SerializeField] private string _spawnDataID;
    [SerializeField] private Collider2D _spawnCollider;

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
}
