using UnityEngine;

public class EnemyRanged : MonoBehaviour
{
    public GameObject projectile;
    public Transform projectile_position;

    private float timer;
    private GameObject player;

    [SerializeField] private Transform leftEdge;
    [SerializeField] private Transform rightEdge;
    [SerializeField] private AudioClip lightningSound;

    private Animator serpent_sage_anim;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        serpent_sage_anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (player.transform.position.x >= leftEdge.position.x &&
            player.transform.position.x <= rightEdge.position.x) 
        {
            timer += Time.deltaTime;

            if (timer > 0.6 && player.GetComponent<PlayerLife>().currentHealth != 0)
            {
                serpent_sage_anim.SetTrigger("shoot");
                timer = -1f;
            }
        }

        if (player.transform.position.x < transform.position.x)
        {
            transform.localScale = new Vector3(-1f, transform.localScale.y, transform.localScale.z);
        }
        else
        {
            transform.localScale = new Vector3(1f, transform.localScale.y, transform.localScale.z);
        }
    }

    private void Shoot() 
    {
        Instantiate(projectile, projectile_position.position, Quaternion.identity);
        SoundManager.instance.PlaySound(lightningSound);
    }
}
