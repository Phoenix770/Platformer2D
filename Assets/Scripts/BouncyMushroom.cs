using UnityEngine;

public class BouncyMushroom : MonoBehaviour
{
    [SerializeField] float _bounceVelocity = 7f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var player = collision.collider.GetComponent<Player>();
        if (player != null)
        {
            var rigidbody2D = player.GetComponent<Rigidbody2D>();
            if (rigidbody2D != null)
            {
                rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, rigidbody2D.velocity.y + _bounceVelocity);
            }
        }
    }
}
