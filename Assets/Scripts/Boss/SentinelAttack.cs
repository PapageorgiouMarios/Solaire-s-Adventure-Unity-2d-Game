using UnityEngine;

public class SentinelAttack : MonoBehaviour
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

    private float cooldownTimer = Mathf.Infinity;
    private Animator boss_anim;
    private PlayerLife player_health; // Necessary to use TakeDamage(damage) to Player
    private SentinelFollowing bossPatrol;

    [SerializeField] private AudioClip attackSound;

    private void Awake()
    {
        boss_anim = GetComponent<Animator>();
        bossPatrol = GetComponentInParent<SentinelFollowing>();
        player_health = PlayerLife.instance;
    }


    private void Update()
    {
        AttackPlayer();

        if (bossPatrol != null)
        {
            bossPatrol.enabled = !HitboxActivated();
        }
    }

    private bool HitboxActivated()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
                                             new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z), 0,
                                             Vector2.left, 0, player);
        if (hit.collider != null)
        {
            player_health = hit.transform.GetComponent<PlayerLife>();
        }

        return hit.collider != null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
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

    private void AttackPlayer()
    {
        cooldownTimer += Time.deltaTime;

        if (HitboxActivated())
        {
            if (cooldownTimer >= cooldown)
            {
                cooldownTimer = 0;
                boss_anim.SetTrigger("attack");
            }
        }
    }
}
