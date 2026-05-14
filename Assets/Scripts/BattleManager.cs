using UnityEngine;

public class BattleManager : MonoBehaviour
{
    [SerializeField] private PlayerController player;
    [SerializeField] private Monster monster;

    public static BattleManager instance;

    private void Awake()
    {
        instance = this;
    }

    public void TakeDamage(int monsterATK)
    {
        player.TakeDamage(monsterATK);
    }
}
