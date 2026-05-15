using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;

public class Monster : MonoBehaviour
{
    private int monsterHP = 50;
    private int monsterATK = 10;

    private PlayerController player;
    private Animator _anim;
    private bool canAttack = false;
    private CancellationTokenSource tokenSource = new CancellationTokenSource();

    private void Attack()
    {
        player.TakeDamage(monsterATK);
        _anim = GetComponent<Animator>();
    }

    public void TakeDamage(int atk)
    {
        monsterHP -= atk;

        if (monsterHP <= 0)
        {
            Die().Forget();
        }
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

    private async UniTaskVoid Die()
    {
        _anim.SetTrigger("Dead");

        await UniTask.Delay(1000);

        gameObject.SetActive(false);
    }
}
