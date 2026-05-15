using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;

public class Monster : MonoBehaviour
{
    private int MonsterHP { get; set; }
    private int MonsterATK { get; set; }

    private PlayerController _player;
    private Animator _anim;
    private bool _canAttack = false;
    private CancellationTokenSource _tokenSource = new CancellationTokenSource();

    private void OnEnable()
    {
        MonsterHP = 50;
        MonsterATK = 10;
    }

    private void Attack()
    {
        _player.TakeDamage(MonsterATK);
        _anim = GetComponent<Animator>();
    }

    public void TakeDamage(int atk)
    {
        MonsterHP -= atk;

        if (MonsterHP <= 0)
        {
            Die().Forget();
        }
    }

    private async UniTaskVoid AttackRoutine(CancellationToken token)
    {
        while (!token.IsCancellationRequested)
        {
            if (_canAttack)
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
            _player = collision.gameObject.GetComponent<PlayerController>();
        }

        if (_player != null)
        {
            _canAttack = true;

            AttackRoutine(_tokenSource.Token).Forget();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _canAttack = false;

            _tokenSource.Cancel();

            _player = null;
        }
    }

    private async UniTaskVoid Die()
    {
        _anim.SetTrigger("Dead");

        await UniTask.Delay(1000);

        gameObject.SetActive(false);
    }
}
