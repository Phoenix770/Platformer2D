using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    [SerializeField] float _fallSpeed = 13;
    [SerializeField] float _fallAfterTime = 2;
    [SerializeField] float _wiggleAfterTime = 0.25f;
    [Range(0.02f,0.1f)][SerializeField] float _shakeAxis = 0.075f;
    [Tooltip("Resets the wiggle timer when no players are on platform")]
    [SerializeField] bool _resetOnEmpty;

    bool _playerInside;
    Coroutine _coroutine;
    HashSet<Player> _playersInTrigger = new HashSet<Player>();
    Vector3 _initialPosition;
    bool _falling;
    float _wiggleTimer;

    private void Start()
    {
        _initialPosition = transform.position;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponent<Player>();
        if (player == null)
            return;

        _playersInTrigger.Add(player);
        _playerInside = true;

        if(_playersInTrigger.Count == 1)
            _coroutine = StartCoroutine(WiggleAndFall());
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (_falling)
            return;
        var player = collision.GetComponent<Player>();
        if (player == null)
            return;

        _playersInTrigger.Remove(player);
        if(_playersInTrigger.Count == 0)
        {
            _playerInside = false;
            StopCoroutine(_coroutine);

            if (_resetOnEmpty)
                _wiggleTimer = 0;
        }
    }

    IEnumerator WiggleAndFall()
    {
        yield return new WaitForSeconds(_wiggleAfterTime);
        
        while(_wiggleTimer < _fallAfterTime)
        {
            float randomX = Random.Range(-_shakeAxis, _shakeAxis);
            float randomY = Random.Range(-_shakeAxis, _shakeAxis);
            transform.position = _initialPosition + new Vector3(randomX, randomY);
            float randomDelay = Random.Range(0.05f, 0.01f);
            yield return new WaitForSeconds(randomDelay);
            _wiggleTimer += randomDelay;
        }
        _falling = true;
        foreach (var collider in GetComponents<Collider2D>())
        {
            collider.enabled = false;
        }

        float fallTimer = 0;
        while(fallTimer <3f)
        {
            transform.position += Vector3.down * Time.deltaTime * _fallSpeed;
            fallTimer += Time.deltaTime;
            yield return null;
        }

        Destroy(gameObject);
    }
}
