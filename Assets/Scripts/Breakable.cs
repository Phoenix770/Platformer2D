using UnityEngine;

public class Breakable : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.GetComponent<Player>() == null)
            return;
        if (collision.contacts[0].normal.y > 0)
            TakeHit();
    }

    private void TakeHit()
    {
        var particleSystem = GetComponent<ParticleSystem>();
        particleSystem.Play();
        GetComponent<Collider2D>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;
    }
}
