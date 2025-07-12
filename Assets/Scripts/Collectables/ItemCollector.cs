using UnityEngine;
using UnityEngine.UI;

/*
 * ItemCollector: Script to object "Player"
 * 
 * Parameters: "How Many Coins" text object
 * 
 * Usage: Collectables system. When the player gets a coin 
 *        the text upper-left shows how many of them are collected in total      
 *        
 */

public class ItemCollector : MonoBehaviour
{
    private int coins = 0; // Number of coins collected
    //[SerializeField] private Text HowManyCoins; // Text shown upper left 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Coin")) // When the player finds a coin
        {
            coins++; // add the coin to the collection
            GameManager.instance.SetCoins(coins);
            GameManager.instance.howManyCoinsText.text = "Coins: " + coins;
            //HowManyCoins.text = "Coins: " + coins; // update the text showing all coins
        }
    }
}
