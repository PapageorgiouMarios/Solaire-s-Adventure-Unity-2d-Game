using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Animator player_attack_animator;
    [SerializeField] private float attackCooldown;
    [SerializeField] private AudioClip attackSound;
    private float cooldownTimer = Mathf.Infinity;

    public Transform attackPos;
    public float attackRange;
    public LayerMask whoIsEnemy;
    public int damage;

    private float dirX;
    public bool isAttacking;

    private void Awake()
    {
        player_attack_animator = GetComponent<Animator>();
    }

    private void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal");

        Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whoIsEnemy);

        if (dirX > 0) 
        {
            attackPos.position = transform.position + new Vector3(1f, 0f, 0f);
        }
        else if (dirX < 0) 
        {
            attackPos.position = transform.position + new Vector3(-1f, 0f, 0f);
        }

        if (Input.GetMouseButton(0) && cooldownTimer > attackCooldown)
        {
            Attack();

            for (int i = 0; i < enemiesToDamage.Length; i++)
            {
                if (enemiesToDamage[i] != null)
                {
                    EnemyLife enemyLife = enemiesToDamage[i].GetComponent<EnemyLife>();

                    if (enemyLife != null && enemyLife.frames_activated == false)
                    {
                        enemyLife.ReceiveDamage(damage);
                    }
                    else
                    {
                        Debug.Log("Enemy's IFrames activated!!!!!");
                    }

                    SentinelLife bossLife = enemiesToDamage[i].GetComponent<SentinelLife>();

                    if(bossLife != null && bossLife.frames_activated == false)
                    {
                        bossLife.ReceiveDamage(damage);
                    }
                    else
                    {
                        Debug.Log("Boss' IFrames activated!!!!!");
                    }
                }
                else
                {
                    Debug.LogWarning("Enemy reference is null!");
                }
            }
        }

        cooldownTimer += Time.deltaTime;
    }

    private void Attack()
    {
        player_attack_animator.SetTrigger("attack");
        isAttacking = true;
        cooldownTimer = 0;
    }

    public void PlayAttackSound() 
    {
        SoundManager.instance.PlaySound(attackSound);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(attackPos.position, attackRange);
    }
}
