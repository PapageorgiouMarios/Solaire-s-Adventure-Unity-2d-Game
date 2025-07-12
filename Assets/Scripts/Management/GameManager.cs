using UnityEngine;
using UnityEngine.UI;

/*
 * GameManager is responsible to keep track of data which are needed in all scenes.
 * Basically, we need to avoid to reset some stats when a new scene is loaded. Which is why
 * we are using PlayerPrefs. PlayerPrefs are variables with their own keyes.
 * In our case what we need to keep track of is
 * 1) The players current health
 * 2) The coins the player has collected in total
 * 3) The lives the player has left
 * 4) The number of total enemies defeated by the player
 * 5) The gameplay's total duration
 */

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; } // This instance will be used in all scenes

    string healthKey = "playerHealth"; // Player Health Key
    public int health { get; private set; } // Getter, Setter

    string chancesKey = "playerChances"; // Player Chances Key
    public int chances { get; private set; } // Getter, Setter

    string coinsKey = "coinsCollected"; // Coins Collected Key
    public int coins { get; private set; } // Getter, Setter

    string enemiesKey = "enemiesDefeated"; // Enemies Defeated Key
    public int enemies { get; private set; } // Getter, Setter

    string durationKey = "timePlayed"; // Duration Key
    public int duration { get; private set; } // Getter, Setter

    private float elapsedTime = 0f; // Variable to track elapsed time in seconds

    private string formattedTime = "0:00";
    public Text howManyCoinsText { get; set; }
    public Text howManyExtraLives { get; set; }

    private void Awake()
    {
        instance = this;

        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        health = PlayerPrefs.GetInt(healthKey);
        Debug.Log("Awake Game Manager: Health = " + health);
        chances = PlayerPrefs.GetInt(chancesKey);
        Debug.Log("Awake Game Manager: Chances left = " + chances);
        coins = PlayerPrefs.GetInt(coinsKey);
        Debug.Log("Awake Game Manager: Coins collected = " + coins);
        enemies = PlayerPrefs.GetInt(enemiesKey);
        Debug.Log("Awake Game Manager: Enemies defeated = " + enemies);
        duration = PlayerPrefs.GetInt(durationKey);

        GameObject coinObject = GameObject.Find("How Many Coins");
        GameObject livesObject = GameObject.Find("How Many Extra Lives");

        if(coinObject != null && livesObject != null) 
        {
            howManyCoinsText = coinObject.GetComponent<Text>();
            howManyExtraLives = livesObject.GetComponent<Text>();
        }
    }

    private void Start()
    {
        // Start tracking time when the game starts
        Debug.Log("Game Manager Start");
        Debug.Log("TIME START");
        elapsedTime = PlayerPrefs.GetFloat(durationKey);
    }

    private void Update()
    {
        // Update the duration property with elapsed time
        elapsedTime += Time.deltaTime;
        duration = (int)elapsedTime;

        // Save the updated duration to PlayerPrefs
        PlayerPrefs.SetFloat(durationKey, elapsedTime);
    }

    public void SetHealth(int hp)
    {
        health = hp;
        PlayerPrefs.SetInt(healthKey, hp);
        Debug.Log("Health set to: " + hp);
    }

    public void SetChances(int c)
    {
        chances = c;
        PlayerPrefs.SetInt(chancesKey, c);
        Debug.Log("Chances set to: " + c);
    }

    public void SetCoins(int howMuch)
    {
        coins = howMuch;
        PlayerPrefs.SetInt(coinsKey, howMuch);
        Debug.Log("Coins set to: " + howMuch);
    }

    public void SetDefeatedEnemies(int howMany)
    {
        enemies = howMany;
        PlayerPrefs.SetInt(enemiesKey, howMany);
        Debug.Log("Defeated Enemies set to: " + howMany);
    }

    public void AddDefeatedEnemies() 
    {
        enemies++;
        SetDefeatedEnemies(enemies);
    }

    private void OnDisable()
    {
        // Save the final values when the game is disabled or closed
        PlayerPrefs.SetInt(healthKey, health);
        PlayerPrefs.SetInt(chancesKey, chances);
        PlayerPrefs.SetInt(coinsKey, coins);
        PlayerPrefs.SetInt(enemiesKey, enemies);
        PlayerPrefs.SetFloat(durationKey, elapsedTime);
    }

    private void OnApplicationQuit()
    {
        if (!Application.isEditor) // Check if the game is running in the editor
        {
            // Reset PlayerPrefs values when the application quits
            Debug.Log("Reset Prefs");

            // Calculate minutes, seconds, and milliseconds
            int minutes = (int)(elapsedTime / 60);
            int seconds = (int)(elapsedTime % 60);

            // Format the time as "Minutes minutes Seconds seconds Milliseconds milliseconds"
            formattedTime = string.Format("{0}:{1}", minutes, seconds);
            Debug.Log("TOTAL TIME " + formattedTime);

            ResetPlayerPrefs();
        }
        else
        {
            // Reset elapsedTime only if running in the editor
            Debug.Log("Reset Prefs");

            // Calculate minutes, seconds, and milliseconds
            int minutes = (int)(elapsedTime / 60);
            int seconds = (int)(elapsedTime % 60);

            // Format the time as "Minutes minutes Seconds seconds Milliseconds milliseconds"
            formattedTime = string.Format("{0}:{1}", minutes, seconds);
            Debug.Log("TOTAL TIME " + formattedTime);

            elapsedTime = 0f;
            enemies = 0;
            health = 3;
            chances = 4;
            coins = 0;
        }
    }

    private void ResetPlayerPrefs()
    {
        PlayerPrefs.DeleteAll(); // Delete all PlayerPrefs data
    }

    public void DestroyAfterRestart() 
    {
        instance = null;
        Destroy(gameObject);
    }
}
