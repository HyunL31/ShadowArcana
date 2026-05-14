using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    public enum AnimState
    {
        Idle,
        IsMoving,
        IsRunning,
        Jump,
        Attack,
        Dead
    }

    [SerializeField] private Animator _anim;

    public void SetAnim(AnimState state)
    {
        if (state == AnimState.IsMoving)
        {
            _anim.SetBool("IsMoving", true);
        }
        else if (state == AnimState.IsRunning)
        {
            _anim.SetBool("IsRunning", true);
        }
        else if (state == AnimState.Jump)
        {
            _anim.SetTrigger("Jump");
        }
        else if (state == AnimState.Dead)
        {
            _anim.SetTrigger("Dead");
        }
        else if (state == AnimState.Attack)
        {
            _anim.SetTrigger("Attack");
        }
    }

    public void ResetBoolState()
    {
        _anim.SetBool("IsMoving", false);
        _anim.SetBool("IsRunning", false);
    }
}
