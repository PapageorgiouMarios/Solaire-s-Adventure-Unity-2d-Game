using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{
    public static PlayerLife instance { get; private set; }

    public int starting_health = 3;
    public int currentHealth { get; set; }
    public int chances { get; set; }

    private Rigidbody2D player_body;
    private Animator death_animator { get; set; }
    private BoxCollider2D player_collider;
    private SpriteRenderer player_sprite_rend;

    [SerializeField] private float iFramesDuration;
    [SerializeField] private int numberOfFlashes;
    //[SerializeField] private Text extra_lives;

    [SerializeField] private AudioClip hurtSound;
    [SerializeField] private AudioClip deathSound;

    private bool hurt = false;
    public bool frames_activated = false;

    private void Awake()
    {
        instance = this;

        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        currentHealth = starting_health;
        chances = 4;
        player_body = GetComponent<Rigidbody2D>();
        death_animator = GetComponent<Animator>();
        player_collider = GetComponent<BoxCollider2D>();
        player_sprite_rend = GetComponent<SpriteRenderer>();

        GameManager.instance.howManyExtraLives.text = "x" + (chances - 1);
        GameManager.instance.SetHealth(currentHealth);
        GameManager.instance.SetChances(chances);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Trap") && currentHealth > 0 && !hurt) 
        {
            Debug.Log("Player has been hit!");
            TakeDamage(1);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Trap") && currentHealth > 0 && !hurt)
        {
            Debug.Log("Player has been hit!");
            TakeDamage(1);
        }
    }

    public void TakeDamage(int damage) 
    {
        Debug.Log("Player takes damage!");
        Debug.Log("Player current health: " + currentHealth);

        hurt = true;
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, starting_health);

        GameManager.instance.SetHealth(currentHealth);

        Debug.Log("Player health now: " + currentHealth);

        if (currentHealth > 0)
        {
            death_animator.SetTrigger("hurt");
            SoundManager.instance.PlaySound(hurtSound);
            StartCoroutine(iFramesActivation());
        }
        else if (currentHealth == 0)
        {
            Die();

            if(chances != 0) 
            {
                chances = chances - 1;
                GameManager.instance.SetChances(chances);
            }

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
        }

    }

    private IEnumerator iFramesActivation() 
    {
        frames_activated = true;
        Physics2D.IgnoreLayerCollision(7, 8, true);
        Physics2D.IgnoreLayerCollision(7, 9, true);
        for(int i = 0; i < numberOfFlashes; i++) 
        {
            player_sprite_rend.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
            player_sprite_rend.color = Color.white;
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
            hurt = false;
        }
        Physics2D.IgnoreLayerCollision(7, 8, false);
        Physics2D.IgnoreLayerCollision(7, 9, false);
        frames_activated = false;

    }

    public void AddHealth(int hp) 
    {
        currentHealth = Mathf.Clamp(currentHealth + hp, 0, starting_health);
        GameManager.instance.SetHealth(currentHealth);
    }

    public void AddChance(int chance) 
    {
        chances = chances + chance;
        GameManager.instance.SetChances(chances);
    }

    public void Respawn() 
    {
        player_body.bodyType = RigidbodyType2D.Dynamic;
        player_collider.enabled = true;
        AddHealth(starting_health);
        Debug.Log("Respawn!!!");
        death_animator.ResetTrigger("death");
        death_animator.SetInteger("state", 0);
        StartCoroutine(iFramesActivation());
    }

    private void Die() 
    {
        player_body.bodyType = RigidbodyType2D.Static;
        player_collider.enabled = false;
        SoundManager.instance.PlaySound(deathSound);
        death_animator.SetTrigger("death");
    }

    private void RestartLevel() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void DestroyAfterRestart()
    {
        instance = null;
        Destroy(gameObject);
    }
}
