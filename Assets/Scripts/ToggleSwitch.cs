using UnityEngine;
using UnityEngine.Events;

public class ToggleSwitch : MonoBehaviour
{
    [SerializeField] ToggleDirection _startingDirection = ToggleDirection.Center;

    [SerializeField] Sprite _leftSprite;
    [SerializeField] Sprite _centerSprite;
    [SerializeField] Sprite _rightSprite;

    [SerializeField] UnityEvent _onLeft;
    [SerializeField] UnityEvent _onRight;
    [SerializeField] UnityEvent _onCenter;

    SpriteRenderer _spriteRenderer;
    ToggleDirection _currentDirection;

    enum ToggleDirection
    {
        Left,
        Center,
        Right,
    }

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        SetToggleDirection(_startingDirection, true);
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        var player = collision.GetComponent<Player>();
        if (player == null)
            return;
        var playerRigidbody = player.GetComponent<Rigidbody2D>();
        if (playerRigidbody == null)
            return;

        bool wasOnRight = collision.transform.position.x > transform.position.x;
        bool playerWalkingRight = playerRigidbody.velocity.x > 0;
        bool playerWalkingLeft = playerRigidbody.velocity.x < 0;

        if (wasOnRight && playerWalkingRight)
        {
            SetToggleDirection(ToggleDirection.Right);
        }
        else if (!wasOnRight && playerWalkingLeft)
        {
            SetToggleDirection(ToggleDirection.Left);
        }
    }

    void SetToggleDirection(ToggleDirection direction, bool force = false)
    {
        if (!force && _currentDirection == direction)
            return;

        _currentDirection = direction;
        switch (direction)
        {
            case ToggleDirection.Left:
                _spriteRenderer.sprite = _leftSprite;
                _onLeft.Invoke();
                break;
            case ToggleDirection.Center:
                _spriteRenderer.sprite = _centerSprite;
                _onCenter.Invoke();
                break;
            case ToggleDirection.Right:
                _spriteRenderer.sprite = _rightSprite;
                _onRight.Invoke();
                break;
            default:
                break;
        }
    }

    void OnValidate()
    {
        
    }
}

