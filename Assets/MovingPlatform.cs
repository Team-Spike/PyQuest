using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform pointA;  // Starting point
    public Transform pointB;  // Destination point
    public float speed = 2f;  // Speed of the platform movement
    private Vector3 target;   // The current target position

    void Start()
    {
        // Set the initial target to pointB
        target = pointB.position;
    }

    void Update()
    {
        // Move the platform towards the target
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

        // If the platform reaches the target, switch to the other point
        if (Vector3.Distance(transform.position, target) < 0.1f)
        {
            // Switch target between pointA and pointB
            target = target == pointA.position ? pointB.position : pointA.position;
        }
    }
}
