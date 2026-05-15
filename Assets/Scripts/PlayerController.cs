using Cysharp.Threading.Tasks;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private AnimatorController animController;

    public int hp = 100;
    public int atk = 20;

    public float walkSpeed = 5f;
    public float runSpeed = 8f;
    public float jumpForce = 5f;

    private Rigidbody2D _rb;
    private Monster _target;
    private bool isRunning = false;
    private bool isGround = true;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        float inputX = Input.GetAxis("Horizontal");

        Move(inputX);

        if (Input.GetKeyDown(KeyCode.Space) && isGround)
        {
            animController.SetAnim(AnimatorController.AnimState.Jump);
            Jump();
        }

        SetDirection(inputX);

        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }
    }

    private void Move(float inputX)
    {
        if (inputX == 0)
        {
            isRunning = false;

            animController.ResetBoolState();

            _rb.linearVelocity = new Vector2(0, _rb.linearVelocity.y);

            return;
        }

        animController.SetAnim(AnimatorController.AnimState.IsMoving);

        float moveSpeed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed;
        isRunning = (moveSpeed == runSpeed);

        if (isRunning)
        {
            animController.SetAnim(AnimatorController.AnimState.IsRunning);
        }

        _rb.linearVelocity = new Vector2(inputX * moveSpeed, _rb.linearVelocity.y);
    }

    private void Jump()
    {
        isGround = false;

        _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, jumpForce);
    }

    private void SetDirection(float inputX)
    {
        if (inputX == 0)
        {
            return;
        }

        if (inputX < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (inputX > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGround = true;
        }
    }

    private void Attack()
    {
        animController.SetAnim(AnimatorController.AnimState.Attack);

        if (_target != null)
        {
            _target.TakeDamage(atk);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Monster"))
        {
            _target = collision.gameObject.GetComponent<Monster>();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Monster"))
        {
            _target = null;
        }
    }

    public void TakeDamage(int atk)
    {
        hp -= atk;

        if (hp <= 0)
        {
            Die().Forget();
        }
    }

    private async UniTaskVoid Die()
    {
        animController.SetAnim(AnimatorController.AnimState.Dead);

        await UniTask.Delay(500);

        gameObject.SetActive(false);
    }
}