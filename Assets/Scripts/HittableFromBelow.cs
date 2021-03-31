using System;
using UnityEngine;

public class HittableFromBelow : MonoBehaviour
{
    [SerializeField] protected Sprite _usedSprite;
    Animator _animator;

    void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    protected virtual bool CanUse => true;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (CanUse == false)
            return;

        var player = collision.collider.GetComponent<Player>();
        if (player == null)
            return;

        if (collision.contacts[0].normal.y > 0)
        {
            PlayAnimation();
            Use();

            if (!CanUse)
                GetComponent<SpriteRenderer>().sprite = _usedSprite;
        }
    }

    void PlayAnimation()
    {
        if (_animator != null)
            _animator.SetTrigger("Use");
    }

    protected virtual void Use() {}
}
