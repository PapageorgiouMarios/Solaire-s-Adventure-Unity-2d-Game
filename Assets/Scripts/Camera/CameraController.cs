using UnityEngine;

/*
 * CameraController is used to update the position of the camera 
 * while the player moves 
 */ 

public class CameraController : MonoBehaviour
{
    [Header("Who the camera follows?")]
    [SerializeField] private Transform player; // We give as a parameter the player's X,Y,Z
                                            
    //At the beginning of the game
    private void Start()
    {
        // Console message frequency: Only once at the beginning
        Debug.Log("Main Camera has been set!");
    }

    private void Update()
    {
        // The camera follows the player's position while their current health remains positive
        transform.position = new Vector3(player.position.x, player.position.y, transform.position.z);
    }
}
