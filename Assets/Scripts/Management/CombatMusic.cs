using UnityEngine;

public class CombatMusic : MonoBehaviour
{
    [SerializeField] Transform from;
    [SerializeField] Transform to;
    [SerializeField] PlayerLife player;

    [SerializeField] AudioClip defaultMusic;
    [SerializeField] AudioClip combatMusic;

    private Transform playerPosition;
    private bool isPlayingCombatMusic = false;

    private void Awake()
    {
        playerPosition = player.transform;
    }

    private void Update()
    {
        bool isWithinBounds = playerPosition.position.x >= from.position.x && playerPosition.position.x <= to.position.x;

        if (isWithinBounds && !isPlayingCombatMusic)
        {
            isPlayingCombatMusic = true;
            SoundManager.instance.ChangeMusicClip(combatMusic);
        }
        else if (!isWithinBounds && isPlayingCombatMusic)
        {
            isPlayingCombatMusic = false;
            SoundManager.instance.ChangeMusicClip(defaultMusic);
        }
    }
}
