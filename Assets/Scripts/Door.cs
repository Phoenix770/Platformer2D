using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] Sprite _openMid;
    [SerializeField] Sprite _openTop;
    [SerializeField] SpriteRenderer _rendererMid;
    [SerializeField] SpriteRenderer _rendererTop;
    [SerializeField] Door _exit;
    [SerializeField] Canvas _canvas;
    int _requiredCoins = 3;
    bool _open;

    [ContextMenu("Open Door")]
    public void Open()
    {
        _open = true;
        _rendererMid.sprite = _openMid;
        _rendererTop.sprite = _openTop;
        if (_canvas != null)
            _canvas.enabled = false;
    }

    void Update()
    {
        if (_open == false && Coin.CoinsCollected >= _requiredCoins)
            Open();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_open == false)
            return;

        var player = collision.GetComponent<Player>();
        if (player != null && _exit != null)
        {
            player.TeleportTo(_exit.transform.position);
        }
    }
}
