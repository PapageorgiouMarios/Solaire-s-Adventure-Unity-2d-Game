using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddChance : MonoBehaviour
{
    [SerializeField] private int chance_value; 
    [SerializeField] AudioClip chanceSound;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && collision.GetComponent<PlayerLife>().chances < 4)
        {
            collision.GetComponent<PlayerLife>().AddChance(chance_value);
            SoundManager.instance.PlaySound(chanceSound);
            gameObject.SetActive(false);

            GameObject livesObject = GameObject.Find("How Many Extra Lives");

            if (livesObject != null)
            {
                GameManager.instance.howManyExtraLives.text = "x" + (collision.GetComponent<PlayerLife>().chances - 1);
            }
        }
        else if (collision.tag != "Player" || (collision.tag == "Player" && (collision.GetComponent<PlayerLife>().chances == 4)))
        {
            DeactivateAllColliders();
        }
        ReactivateAllColliders();
    }

    private void DeactivateAllColliders()
    {
        Collider2D[] colliders = GetComponents<Collider2D>();
        foreach (Collider2D collider in colliders) 
        {
            collider.enabled = false; 
        }
    }

    private void ReactivateAllColliders()
    {
        Collider2D[] colliders = GetComponents<Collider2D>();
        foreach (Collider2D collider in colliders)
        {
            collider.enabled = true;
        }
    }

}
