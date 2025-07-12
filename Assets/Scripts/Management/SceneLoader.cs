using UnityEngine;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    string healthKey = "playerHealth"; // Player Health Key
    string chancesKey = "playerChances"; // Player Chances Key
    string coinsKey = "coinsCollected"; // Coins Collected Key
    string enemiesKey = "enemiesDefeated"; // Enemies Defeated Key
    string durationKey = "timePlayed"; // Duration Key

    [SerializeField] Text coinText;
    [SerializeField] Text livesLeft;
    [SerializeField] Transform whereToStart;

    private void Awake()
    {
        int health = PlayerPrefs.GetInt(healthKey);
        int chances = PlayerPrefs.GetInt(chancesKey);
        int coins = PlayerPrefs.GetInt(coinsKey);
        int enemies = PlayerPrefs.GetInt(enemiesKey);
        int duration = PlayerPrefs.GetInt(durationKey);

        PlayerLife.instance.chances = chances;
        PlayerLife.instance.currentHealth = health;
        PlayerLife.instance.transform.position = whereToStart.position;

        GameObject coinObject = GameObject.Find("How Many Coins");
        GameObject livesObject = GameObject.Find("How Many Extra Lives");

        coinText = coinObject.GetComponent<Text>();
        livesLeft = livesObject.GetComponent<Text>();

        GameManager.instance.howManyCoinsText = coinText;
        GameManager.instance.howManyExtraLives = livesLeft;

        GameManager.instance.howManyCoinsText.text = "Coins: " + coins;

        if (chances == -1 || chances == 0 || chances == 1) 
        {
            GameManager.instance.howManyExtraLives.text = "x0";
            //extra_lives.text = "x0";
        }
        else if (chances > 1 && chances < 4)
        {
            GameManager.instance.howManyExtraLives.text = "x" + (chances - 1);
            //extra_lives.text = "x" + (chances - 1);
        }


        // Print all PlayerPrefs data for debugging purposes
        Debug.Log("-------------------------------------");
        Debug.Log("------Printing PlayerPrefs data------");
        Debug.Log("Player health:  " + health);
        Debug.Log("Player chances:  " + chances);
        Debug.Log("Total coins:  " + coins);
        Debug.Log("Enemies defeated:  " + enemies);
        Debug.Log("Time so far:  " + duration);
        Debug.Log("Text for lives:  " + livesLeft.text + coinText);
        Debug.Log("Text for coins:  " + coinText.text + livesLeft);
        Debug.Log("-------------------------------------");
    }
}
