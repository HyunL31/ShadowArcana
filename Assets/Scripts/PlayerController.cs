using Cysharp.Threading.Tasks;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private AnimatorController _animController;

    private int PlayerHP { get; set; }
    private int PlayerATK { get; set; }

    public float _walkSpeed = 5f;
    public float _runSpeed = 8f;
    public float _jumpForce = 5f;

    private int _maxHP = 100;
    private Rigidbody2D _rb;
    private Monster _target;
    private bool _isRunning = false;
    private bool _isGround = true;
    private HPBar _hpBar;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();

        PlayerHP = _maxHP;
        PlayerATK = 15;
    }

    private void Start()
    {
        _hpBar = UIManager.Instance.OpenHPBarUI(_maxHP, this.gameObject);
    }

    private void Update()
    {
        float inputX = Input.GetAxis("Horizontal");

        Move(inputX);

        if (Input.GetKeyDown(KeyCode.Space) && _isGround)
        {
            _animController.SetAnim(AnimatorController.AnimState.Jump);
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
            _isRunning = false;

            _animController.ResetBoolState();

            _rb.linearVelocity = new Vector2(0, _rb.linearVelocity.y);

            return;
        }

        _animController.SetAnim(AnimatorController.AnimState.IsMoving);

        float moveSpeed = Input.GetKey(KeyCode.LeftShift) ? _runSpeed : _walkSpeed;
        _isRunning = (moveSpeed == _runSpeed);

        if (_isRunning)
        {
            _animController.SetAnim(AnimatorController.AnimState.IsRunning);
        }

        _rb.linearVelocity = new Vector2(inputX * moveSpeed, _rb.linearVelocity.y);
    }

    private void Jump()
    {
        _isGround = false;

        _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, _jumpForce);
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
            _isGround = true;
        }
    }

    private void Attack()
    {
        _animController.SetAnim(AnimatorController.AnimState.Attack);

        if (_target != null)
        {
            _target.TakeDamage(PlayerATK);
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
        PlayerHP -= atk;
        _hpBar.UpdateValue(PlayerHP);

        if (PlayerHP <= 0)
        {
            UIManager.Instance.CloseHPBar(_hpBar);
            Die().Forget();
        }
    }

    private async UniTaskVoid Die()
    {
        _animController.SetAnim(AnimatorController.AnimState.Dead);

        await UniTask.Delay(500);

        gameObject.SetActive(false);
    }
}