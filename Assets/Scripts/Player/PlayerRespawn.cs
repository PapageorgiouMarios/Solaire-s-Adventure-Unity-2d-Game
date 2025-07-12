using System.Data;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerRespawn : MonoBehaviour
{
    private Transform current_checkpoint;
    private PlayerLife player_life;
    private UIManager uiManager;
    [SerializeField] AudioClip fireLit;

    private void Awake()
    {
        player_life = GetComponent<PlayerLife>(); 
        uiManager = FindObjectOfType<UIManager>();
    }

    private void RespawnAgain() 
    {
        if(player_life.chances == 0) 
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            uiManager.GameOver();
            player.SetActive(false);
            return;
        }
        else 
        {
            transform.position = new Vector3(current_checkpoint.position.x, current_checkpoint.position.y + 1,
            current_checkpoint.position.z);

            player_life.Respawn();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Checkpoint") 
        {
            current_checkpoint = collision.transform;
            collision.GetComponent<Collider2D>().enabled = false;
            collision.GetComponent<Animator>().SetTrigger("reached");
            SoundManager.instance.PlaySound(fireLit);
        }

        if (collision.transform.tag == "EndCP")
        {
            Debug.Log("End Game");
            SceneManager.LoadScene(2);
        }
    }
}
