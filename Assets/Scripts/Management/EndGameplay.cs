using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndGameplay : MonoBehaviour
{
    [SerializeField] SentinelLife boss;
    private bool isAccessible = false;

    [SerializeField] Image darken_screen;
    public float transitionSpeed = 40f;
    public bool transitioning = false;
    public bool changeSceneNow = false;

    private Animator player_animator;
    private PlayerMovement player_movement;
    private Rigidbody2D player_body;

    private void Update()
    {
        if (boss.isDead) 
        {
            isAccessible = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && isAccessible)
        {
            transitioning = true;
            DontDestroyOnLoad(collision.gameObject);

            player_animator = collision.gameObject.GetComponent<Animator>();
            player_movement = collision.gameObject.GetComponent<PlayerMovement>();
            player_body = collision.gameObject.GetComponent<Rigidbody2D>();

            player_animator.SetInteger("state", 0);
            player_body.constraints = RigidbodyConstraints2D.FreezePositionX;
            player_movement.enabled = false;

            EnterEndScene();
        }
    }

    private void EnterEndScene()
    {
        UIManager.instance.DestroyInComplete();
        GameManager.instance.DestroyAfterRestart();
        PlayerLife.instance.DestroyAfterRestart();
        SceneManager.LoadScene(3);
    }

}
