using UnityEngine;

public class Fly : MonoBehaviour
{
    [SerializeField] Vector2 _direction = Vector2.up;
    [SerializeField] float _maxDistance = 2;
    [SerializeField] float _speed = 2;

    private Vector2 _startPosition;

    void Start()
    {
        _startPosition = transform.position;
    }

    void Update()
    {
        transform.Translate(_direction.normalized * Time.deltaTime * _speed);
        var distance = Vector2.Distance(_startPosition, transform.position);
        if(distance >= _maxDistance)
        {
            transform.position = _startPosition + (_direction.normalized * _maxDistance);
            _direction *= -1;
        }
    }
}