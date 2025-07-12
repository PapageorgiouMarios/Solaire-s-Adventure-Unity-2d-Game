using UnityEngine;

/*
 * HealingPotion: Script to object "Healing_Potion"
 * 
 * Parameters: Given in Unity, equals to 1
 * 
 * Usage: Game's health system. When the player isn's at max health 
 *        they are able to increase their health by one heart container
 *        
 */

public class HealingPotion : MonoBehaviour
{
    [SerializeField] private int health_value; // How much health the potion gives
    [SerializeField] AudioClip healSound;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If the player finds a potion and they have no max health
        if (collision.tag == "Player" && collision.GetComponent<PlayerLife>().currentHealth < 3)
        {
            collision.GetComponent<PlayerLife>().AddHealth(health_value); // Add +1 Health point
            SoundManager.instance.PlaySound(healSound);
            gameObject.SetActive(false); // De-activate the healing potion object
        }
        // In case another object collides with the "Healing_Potion" collider
        else if (collision.tag != "Player" || (collision.tag == "Player" && (collision.GetComponent<PlayerLife>().currentHealth == 3)))
        {
            DeactivateAllColliders(); // We de-activate all of the object's colliders
        }
        ReactivateColliders();
    }

    private void DeactivateAllColliders() // Method to de-activate object's colliders
    {
        Collider2D[] colliders = GetComponents<Collider2D>(); // Get all colliders
        foreach (Collider2D collider in colliders) // for every single collider
        {
            collider.enabled = false; // de-activate collider
        }
    }

    private void ReactivateColliders()
    {
        Collider2D[] colliders = GetComponents<Collider2D>();
        foreach (Collider2D collider in colliders)
        {
            collider.enabled = true;
        }
    }
}
