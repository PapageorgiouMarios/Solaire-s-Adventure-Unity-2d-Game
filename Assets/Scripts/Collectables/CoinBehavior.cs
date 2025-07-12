using UnityEngine;

/*
 * CoinBehavior is used to give animation and sound to the Coin prefab 
 * whenever the player collects a coin.
 */

public class CoinBehavior : MonoBehaviour
{

    [Header("What sound the coin makes?")]
    [SerializeField] AudioClip collectSound; 
    private Animator coin_animator;

    private void Start()
    {
        coin_animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision) // We gave the Coin prefab a "On Trigger" collider
    {
        if (collision.gameObject.CompareTag("Player")) // When the player collides with a coin
        {
            coin_animator.SetTrigger("collected");
            SoundManager.instance.PlaySound(collectSound);
        }
    }

    // Event used when animation is complete
    private void DestroyedWhenCollected() 
    {
        Destroy(gameObject); // remove the object from the game
    }
}
