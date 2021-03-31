using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] int _playerNumber = 1;
    [Header("Movement")]
    [SerializeField] float _speed = 3;
    [SerializeField] float _slipFactor = 1;
    [Header("Jump")]
    [SerializeField] Transform _feet;
    [SerializeField] float _jumpVelocity = 8;
    [SerializeField] int _maxJumps = 2;
    [SerializeField] float _downPull = 3;
    [SerializeField] float _maxJumpDuration = 0.15f;

    private Vector2 _startPosition;
    int _jumpsRemaining;
    float _fallTimer;
    float _jumpTimer;
    Rigidbody2D _rigidbody2D;
    Animator _animator;
    SpriteRenderer _spriteRenderer;
    float _horizontal;
    bool _isGrounded;

    bool _isOnSlipperySurface;
    string _jumpButton;
    string _horizontalAxis;
    int _layerMask;

    public int PlayerNumber => _playerNumber;

    void Start()
    {
        _startPosition = transform.position;
        _jumpsRemaining = _maxJumps;
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
         _spriteRenderer = GetComponent<SpriteRenderer>();
        _jumpButton = $"P{_playerNumber}Jump";
        _horizontalAxis = $"P{_playerNumber}Horizontal";
        _layerMask = LayerMask.GetMask("Default");
    }

    void Update()
    {
        CalculateIsGrounded();
        ReadHorizontalInput();
        if(_isOnSlipperySurface)
            SlipHorizontal();
        else
            MoveHorizontal();
        UpdateAnimator();
        UpdateSpriteDirection();

        if (ShouldStartJump())
            Jump();
        else if (ShouldContinueJump())
            ContinueJump();

        _jumpTimer += Time.deltaTime;

        if (_isGrounded && _fallTimer > 0)
        {
            _fallTimer = 0;
            _jumpsRemaining = _maxJumps;
        }
        else
        {
            _fallTimer += Time.deltaTime;
            var downForce = _downPull * _fallTimer * _fallTimer;
            _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, _rigidbody2D.velocity.y - downForce);
        }

        if (transform.position.y < -30)
        {
            ResetToStart();
        }
    }

    void ContinueJump()
    {
        _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, _jumpVelocity);
        _fallTimer = 0;
    }

    bool ShouldContinueJump()
    {
        return (Input.GetButton(_jumpButton) && _jumpTimer <= _maxJumpDuration);
    }

    void Jump()
    {
        _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, _jumpVelocity);
        _jumpsRemaining--;
        _fallTimer = 0;
        _jumpTimer = 0;
    }

    bool ShouldStartJump()
    {
        return (Input.GetButton(_jumpButton) && _jumpsRemaining > 0);
    }

    void MoveHorizontal()
    {
        _rigidbody2D.velocity = new Vector2(_horizontal * _speed, _rigidbody2D.velocity.y);

    }

    void SlipHorizontal()
    {
        var desiredVelocity = new Vector2(_horizontal * _speed, _rigidbody2D.velocity.y);
        var smoothedVelocity = Vector2.Lerp(_rigidbody2D.velocity, desiredVelocity, Time.deltaTime / _slipFactor);
        _rigidbody2D.velocity = smoothedVelocity;

    }

    void ReadHorizontalInput()
    {
        _horizontal = Input.GetAxis(_horizontalAxis) * _speed;
    }

    void UpdateSpriteDirection()
    {
        if (_horizontal != 0)
        {
            _spriteRenderer.flipX = _horizontal < 0;
        }
    }

    void UpdateAnimator()
    {
        bool walking = _horizontal != 0;
        _animator.SetBool("Walk", walking);
        _animator.SetBool("Jump", ShouldContinueJump());
    }

    void CalculateIsGrounded()
    {
        var hit = Physics2D.OverlapCircle(_feet.position, 0.1f, _layerMask);
        _isGrounded = hit != null;
        _isOnSlipperySurface = hit?.CompareTag("Slippery") ?? false;
    }

    internal void ResetToStart()
    {
        _rigidbody2D.position = _startPosition;
        _rigidbody2D.velocity = new Vector2(0, 0);
    }

    internal void TeleportTo(Vector3 position)
    {
        _rigidbody2D.position = position;
        _rigidbody2D.velocity = Vector2.zero;
    }
}