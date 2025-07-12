using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    private GameObject player;
    private Rigidbody2D projectile_body;

    public float projectile_force;
    public float timer;

    private void Start()
    {
        projectile_body = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");

        Vector3 direction = player.transform.position - transform.position;
        projectile_body.velocity = direction.normalized * projectile_force;
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer > 1.5)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerLife>().TakeDamage(1);
            Destroy(gameObject);
        }
    }
}
