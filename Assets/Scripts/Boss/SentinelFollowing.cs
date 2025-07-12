using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SentinelFollowing : MonoBehaviour
{
    [Header("Left edge")]
    [SerializeField] public Transform leftEdge;

    [Header("Right edge")]
    [SerializeField] public Transform rightEdge;

    [Header("Whose position it takes?")]
    [SerializeField] private Transform boss;

    [Header("How much fast the boss patrols?")]
    [SerializeField] public float speed;

    private Vector3 scale;
    private bool left;
    private Animator boss_animator;

    private bool playerInBounds;

    private void Awake()
    {
        boss_animator = GetComponent<Animator>();
        scale = boss.localScale;
    }

    private void OnDisable()
    {
        boss_animator.SetBool("walking", false);
    }

    private void Update()
    {
        playerInBounds = (PlayerLife.instance.transform.position.x >= leftEdge.position.x &&
                          PlayerLife.instance.transform.position.x <= rightEdge.position.x);

        if (playerInBounds)
        {
            // Calculate movement towards the player without the speed factor
            Vector3 targetEnemyPosition = new Vector3(PlayerLife.instance.transform.position.x, boss.position.y, boss.position.z);
            Vector3 directionToPlayer = (targetEnemyPosition - boss.position).normalized;
            boss.position += directionToPlayer * speed * Time.deltaTime;

            if (directionToPlayer.x > 0)
            {
                boss.localScale = new Vector3(Mathf.Abs(scale.x), scale.y, scale.z);
            }
            else if (directionToPlayer.x < 0)
            {
                boss.localScale = new Vector3(-Mathf.Abs(scale.x), scale.y, scale.z);
            }
        }
        else
        {
            if (left)
            {
                if (boss.position.x >= leftEdge.position.x)
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
                if (boss.position.x <= rightEdge.position.x)
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
        boss_animator.SetBool("walking", false);
        left = !left;
    }

    private void MoveToDirection(int direction)
    {
        boss_animator.SetBool("walking", true);

        boss.localScale = new Vector3(Mathf.Abs(scale.x) * direction, scale.y, scale.z);

        boss.position = new Vector3(boss.position.x + Time.deltaTime * direction * speed,
                                     boss.position.y, boss.position.z);
    }
}
