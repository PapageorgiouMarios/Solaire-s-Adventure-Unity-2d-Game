using UnityEngine;

public class BossRoomController : MonoBehaviour
{
    [SerializeField] private float speed;
    private float currentPosX;
    private Vector3 velocity = Vector3.zero;

    private void Awake()
    {
        Debug.Log("Rooftop camera set!");
    }

    private void Update()
    {
        transform.position = Vector3.SmoothDamp(transform.position,
                                                new Vector3(currentPosX, transform.position.y, transform.position.z),
                                                ref velocity, speed * Time.deltaTime);
    }

    public void MoveCamera(Transform newRoom) 
    {
        Debug.Log("Move camera");
        currentPosX = newRoom.position.x;
    }
}
