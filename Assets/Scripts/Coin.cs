using UnityEngine;

public class Coin : MonoBehaviour
{
    public static int CoinsCollected;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponent<Player>();
        if (player == null)
            return;

        gameObject.SetActive(false);
        CoinsCollected++;

        ScoreSystem.Add(100);
    }
}
