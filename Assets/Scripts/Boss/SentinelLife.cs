using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SentinelLife : MonoBehaviour
{
    [Header("How much health the boss has?")]
    [SerializeField] public int health;

    [Header("How long the boss is invinsible after hit?")]
    [SerializeField] private float iFramesDuration;

    [Header("How many times the boss flashes after hit")]
    [SerializeField] private int numberOfFlashes;

    [Header("What other behaviors the boss has?")]
    [SerializeField] private Behaviour[] components;

    [SerializeField] private AudioClip deathSound;
    [SerializeField] private AudioClip hurtSound;

    public int remainingHealth { get; private set; }
    private BoxCollider2D boss_collider;
    private Animator boss_animator;
    private SpriteRenderer boss_sprite_rend;

    SentinelFollowing bossMovement;

    public bool isDead = false;
    public bool frames_activated = false;

    private void Awake()
    {
        remainingHealth = health;
        bossMovement = GetComponentInParent<SentinelFollowing>();
        boss_animator = GetComponent<Animator>();
        boss_collider = GetComponent<BoxCollider2D>();
        boss_sprite_rend = GetComponent<SpriteRenderer>();
    }

    public void ReceiveDamage(int damage)
    {
        health -= damage; // usually damage = 1 from player's hit
        remainingHealth = Mathf.Clamp(remainingHealth - damage, 0, health);

        if (remainingHealth > 0) // if the boss still has hp
        {
            SoundManager.instance.PlaySound(hurtSound);
            StartCoroutine(iFramesActivation());
        }
        else if (remainingHealth == 0) // if it is time for the boss to be eliminated
        {
            Debug.Log("Enemy defeated!");
            Dead();
        }
    }

    // Method used to give to boss their IFrames (Invisibility Frames)
    private IEnumerator iFramesActivation()
    {
        frames_activated = true;
        float default_speed = bossMovement.speed;
        bossMovement.speed += 3f;

        Physics2D.IgnoreLayerCollision(7, 8, true); // ignore collision with "Player" or "Enemy"
        for (int i = 0; i < numberOfFlashes; i++)
        {
            boss_sprite_rend.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
            boss_sprite_rend.color = Color.white;
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
        }
        Physics2D.IgnoreLayerCollision(7, 8, false);
        bossMovement.speed = default_speed;
        frames_activated = false;
    }

    private void Dead()
    {
        if (!isDead)
        {
            isDead = true;
            boss_collider.enabled = false;

            foreach (Behaviour component in components)
            {
                component.enabled = false; // when the boss is eliminated all their components are gone
            }
            boss_animator.SetTrigger("die");
        }
    }

    private void PlayDeathSound() 
    {
        SoundManager.instance.StopMusic();
        SoundManager.instance.PlaySound(deathSound);
    }
}
