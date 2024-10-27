using UnityEngine;
using System.Collections;

public class CameraFollow2D : MonoBehaviour
{
    public float interpVelocity;
    public float minDistance;
    public float followDistance;
    public GameObject target;
    public Vector3 offset;
    public float yAxisThreshold = 0f;  // The y-axis value after which the y-offset is set to 0
    private float initialYOffset;      // Store the original y-offset
    Vector3 targetPos;

    // Use this for initialization
    void Start()
    {
        targetPos = transform.position;
        initialYOffset = offset.y;     // Store the initial y-offset value at the start
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (target)
        {
            // Check if the player's y-position crosses the threshold
            if (target.transform.position.y > yAxisThreshold)
            {
                // Set the y-offset to 0 once the player crosses the y-axis threshold
                offset.y = 0f;
            }
            else if (target.transform.position.y <= yAxisThreshold)
            {
                // Reset the y-offset to the initial value when the player is below the threshold
                offset.y = initialYOffset;
            }

            Vector3 posNoZ = transform.position;
            posNoZ.z = target.transform.position.z;

            Vector3 targetDirection = (target.transform.position - posNoZ);

            // Increase the interpVelocity multiplier for a faster response
            interpVelocity = targetDirection.magnitude * 10f;  // Increased from 5f to 10f for faster movement

            targetPos = transform.position + (targetDirection.normalized * interpVelocity * Time.deltaTime);

            // Increase the Lerp factor for faster interpolation
            transform.position = Vector3.Lerp(transform.position, targetPos + offset, 0.5f);  // Increased from 0.25f to 0.5f
        }
    }
}
