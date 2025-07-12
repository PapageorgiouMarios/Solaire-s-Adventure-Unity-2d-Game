using UnityEngine;

/*
 * EnemyMelee is used for the melee Serpent Warriors
 * They use one of two attack animations based on a random number generator from 1 to 100
 * The attack is performed with the help of one extra collider. When the player is inside this collider
 * the boss attacks. If the attack is successful, the player loses health
 */
public class EnemyMelee : MonoBehaviour
{
    [Header("How much is melee attack's range?")]
    [SerializeField] private float range;

    [Header("How far is the attack collider from the boss?")]
    [SerializeField] private float colliderDistance;

    [Header("How long it takes for the boss to attack again?")]
    [SerializeField] private float cooldown;

    [Header("How much damage the attack does?")]
    [SerializeField] private int damage;

    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private LayerMask player; // Use the "Player" Layer to handle attack to player

    [SerializeField] private AudioClip attackSound;
    [SerializeField] private AudioClip attackSound2;

    private float cooldownTimer = Mathf.Infinity;
    private Animator serpent_warrior_anim;

    private PlayerLife player_health; // Necessary to use TakeDamage(damage) to Player

    private EnemyPatrol enemyPatrol;

    private void Awake()
    {
        serpent_warrior_anim = GetComponent<Animator>();
        enemyPatrol = GetComponentInParent<EnemyPatrol>();
    }


    private void Update()
    {
        AttackPlayer();

        if (enemyPatrol != null) 
        {
            enemyPatrol.enabled = !HitboxActivated();
        }
    }

    private bool HitboxActivated() 
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
                                             new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z), 0,
                                             Vector2.left, 0, player);
        if(hit.collider != null) 
        {
            player_health = hit.transform.GetComponent<PlayerLife>();
        }

        return hit.collider != null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance 
            , new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }

    private void DamagePlayer() 
    {
        if (HitboxActivated()) 
        {
            if (!player_health.frames_activated) 
            {
                player_health.TakeDamage(damage);
            }
        }
    }

    public void PlayAttackSound()
    {
        SoundManager.instance.PlaySound(attackSound);
    }

    public void PlayAttackSound2()
    {
        SoundManager.instance.PlaySound(attackSound2);
    }

    private void AttackPlayer() 
    {
        int randomAttack = Random.Range(0, 10);

        cooldownTimer += Time.deltaTime;

        if (HitboxActivated())
        {
            Debug.Log("Serpent Warrior detected Player");
            if (cooldownTimer >= cooldown)
            {
                Debug.Log("Serpent Warrior attacks");
                cooldownTimer = 0;

                if (randomAttack < 7)
                {
                    serpent_warrior_anim.SetTrigger("attack_2");
                }
                else
                {
                    serpent_warrior_anim.SetTrigger("attack_1");
                }
            }
        }
    }
}
