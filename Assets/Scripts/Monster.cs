using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;

public class Monster : MonoBehaviour
{
    [SerializeField] private int monsterHP = 50;
    [SerializeField] private int monsterATK = 50;

    private PlayerController player;
    private Animator _anim;
    private bool canAttack = false;
    private CancellationTokenSource tokenSource = new CancellationTokenSource();

    private void Attack()
    {
        player.TakeDamage(monsterATK);
        _anim = GetComponent<Animator>();
    }

    private async UniTaskVoid AttackRoutine(CancellationToken token)
    {
        while (!token.IsCancellationRequested)
        {
            if (canAttack)
            {
                Attack();
                _anim.SetTrigger("Attack");

                await UniTask.Delay(1500, cancellationToken: token);
            }
            else
            {
                await UniTask.Yield(token);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            player = collision.gameObject.GetComponent<PlayerController>();
        }

        if (player != null)
        {
            canAttack = true;

            AttackRoutine(tokenSource.Token).Forget();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            canAttack = false;

            tokenSource.Cancel();

            player = null;
        }
    }
}
