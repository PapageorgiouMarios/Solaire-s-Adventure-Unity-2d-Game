using UnityEngine;
using UnityEngine.UIElements;

/*
 * EnemyPatrol is used from Melee Enemies. There are two waypoints on opposite sides
 * the boss walks between these two waypoints
 */

public class EnemyPatrol : MonoBehaviour
{
    [Header("Left edge")]
    [SerializeField] public Transform leftEdge;

    [Header("Right edge")]
    [SerializeField] public Transform rightEdge;

    [Header("Whose position it takes?")]
    [SerializeField] private Transform enemy;

    [Header("How much fast the boss patrols?")]
    [SerializeField] public float speed;

    [Header("How long the boss stays put when they reach waypoint?")]
    [SerializeField] private float standing_duration;

    private float standing_timer; // time counter 

    private Vector3 scale;
    private bool left;
    private Animator enemy_animator;

    [Header("What object the boss follows?")]
    [SerializeField] PlayerLife player;

    [Header("What is the player's position")]
    [SerializeField] Transform playerTransform;

    private bool playerInBounds;

    private void Awake()
    {
        enemy_animator = GetComponent<Animator>();
        scale = enemy.localScale;
    }

    private void OnDisable()
    {
        enemy_animator.SetBool("moving", false);
    }

    private void Update()
    {
        playerInBounds = (playerTransform.position.x >= leftEdge.position.x && 
                          playerTransform.position.x <= rightEdge.position.x);

        if (playerInBounds)
        {
            // Calculate movement towards the player without the speed factor
            Vector3 targetEnemyPosition = new Vector3(playerTransform.position.x, enemy.position.y, enemy.position.z);
            Vector3 directionToPlayer = (targetEnemyPosition - enemy.position).normalized;
            enemy.position += directionToPlayer * speed * Time.deltaTime;

            if (directionToPlayer.x > 0)
            {
                enemy.localScale = new Vector3(Mathf.Abs(scale.x), scale.y, scale.z);
            }
            else if (directionToPlayer.x < 0)
            {
                enemy.localScale = new Vector3(-Mathf.Abs(scale.x), scale.y, scale.z);
            }
        }
        else 
        {
            if (left)
            {
                if (enemy.position.x >= leftEdge.position.x)
                {
                    MoveToDirection(-1);
                }
                else
                {
                    ChangeDirection();
                }
            }
            else
            {
                if (enemy.position.x <= rightEdge.position.x)
                {
                    MoveToDirection(1);
                }
                else
                {
                    ChangeDirection();
                }
            }
        }
    }

    private void ChangeDirection() 
    {
        enemy_animator.SetBool("moving", false);
        standing_timer += Time.deltaTime;

        if (standing_timer > standing_duration)
        {
            left = !left;
        }
    }

    private void MoveToDirection(int direction) 
    {
        standing_timer = 0;

        enemy_animator.SetBool("moving",  true);

        enemy.localScale = new Vector3(Mathf.Abs(scale.x) * direction, scale.y, scale.z);

        enemy.position = new Vector3(enemy.position.x + Time.deltaTime * direction * speed,
                                     enemy.position.y, enemy.position.z);
    }

}
