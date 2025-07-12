using UnityEngine;

/*
 * PendulumMovement: Script to object "Pendulum Blade" which is inside the "Pendulum" object
 * 
 * Parameters: pendulum_body -> RigidBody of the Pendulum
 *             leftPushRange -> Left limit of the pendulum
 *             rightPushRange -> Right limit of the pendulum
 *             velocityThreshold -> Sets the angular velocity of the Pendulum
 * 
 * Usage: Moves the pendulum trap right and left
 *        
 */

public class PendulumMovement : MonoBehaviour
{
    public Rigidbody2D pendulum_body;
    public float leftPushRange; 
    public float rightPushRange; 
    public float velocityThreshold;

    private void Start()
    {
        pendulum_body = GetComponent<Rigidbody2D>();
        pendulum_body.angularVelocity = velocityThreshold;
    }

    private void Update()
    {
        Push(); // updates the angular velocity of the pendulum
    }
    private void Push() 
    {
        if (transform.rotation.z > 0
            && transform.rotation.z < rightPushRange
            && (pendulum_body.angularVelocity > 0)
            && pendulum_body.angularVelocity < velocityThreshold) 
        {
            pendulum_body.angularVelocity = velocityThreshold;
        }
        else if (transform.rotation.z < 0
            && transform.rotation.z > leftPushRange
            && (pendulum_body.angularVelocity < 0)
            && pendulum_body.angularVelocity > velocityThreshold * -1) 
        {
            pendulum_body.angularVelocity = velocityThreshold * -1;
        }
    }
}
