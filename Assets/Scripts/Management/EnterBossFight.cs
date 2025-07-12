using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EnterBossFight : MonoBehaviour
{
    [SerializeField] Image darken_screen;
    public float transitionSpeed = 30f;
    public bool transitioning = false;
    public bool changeSceneNow = false;

    private Animator player_animator;
    private PlayerMovement player_movement;
    private Rigidbody2D player_body;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player" && !transitioning)
        {
            transitioning = true;
            DontDestroyOnLoad(collision.gameObject);

            player_animator = collision.gameObject.GetComponent<Animator>();
            player_movement = collision.gameObject.GetComponent<PlayerMovement>();
            player_body = collision.gameObject.GetComponent<Rigidbody2D>();
            
            player_animator.SetInteger("state", 0);
            player_body.constraints = RigidbodyConstraints2D.FreezePositionX;
            player_movement.enabled = false;

            StartCoroutine(DarkenScreen());
        }
    }

    IEnumerator DarkenScreen() 
    {
        while(darken_screen.color.a < 1.0f) 
        {
            Color newcolor = darken_screen.color;
            newcolor.a += Time.deltaTime * transitionSpeed;
            darken_screen.color = newcolor;
            yield return null;
        }

        player_body.constraints = RigidbodyConstraints2D.None;
        Vector3 newPos = player_body.position;
        newPos.z = 0f;
        player_body.position = newPos;
        player_movement.enabled = true;

        BossRoom();
    }

    IEnumerator LightScreen()
    {
        Color newcolor = darken_screen.color;
        newcolor.a = 0;
        darken_screen.color = newcolor;
        yield return null;

        player_body.constraints = RigidbodyConstraints2D.None;
        Vector3 newPos = player_body.position;
        newPos.z = 0f;
        player_body.position = newPos;
        player_movement.enabled = true;

        BossRoom();
    }

    private void BossRoom() 
    {
        PlayerPrefs.Save();
        SceneManager.LoadScene(2);
        StartCoroutine(LightScreen());
    }
}
