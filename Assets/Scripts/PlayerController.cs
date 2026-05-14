using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private AnimatorController animController;

    public int hp = 100;
    public int atk = 10;

    public float walkSpeed = 5f;
    public float runSpeed = 8f;
    public float jumpForce = 5f;

    private Rigidbody2D _rb;
    public bool isMoving = false;
    public bool isRunning = false;
    public bool isGround = true;

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
    }

    private void Move(float inputX)
    {
        if (inputX == 0)
        {
            isMoving = false;
            isRunning = false;

            animController.ResetBoolState();

            _rb.linearVelocity = new Vector2(0, _rb.linearVelocity.y);

            return;
        }

        isMoving = true;
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

        _rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
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

    public void TakeDamage(int atk)
    {
        hp -= atk;
    }
}