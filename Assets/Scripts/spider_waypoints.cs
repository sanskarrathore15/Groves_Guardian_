using UnityEngine;

public class spider_waypoints : MonoBehaviour
{
    public Transform[] waypoints;   // Array of waypoints the spider will follow
    public float movementSpeed = 2.0f; // Speed at which the spider moves between waypoints
    public float rotationSpeed = 5.0f; // Speed at which the spider rotates

    private int currentWaypointIndex = 0; // Index of the current waypoint the spider is moving towards
    private Quaternion targetRotation; // Target rotation for smooth rotation

    void Start()
    {
        // Initialize the target rotation
        targetRotation = transform.rotation;
    }

    void Update()
    {
        // Check if there are any waypoints defined
        if (waypoints.Length == 0)
        {
            Debug.LogWarning("No waypoints defined for the spider to follow.");
            return;
        }

        // Move towards the current waypoint
        MoveTowardsWaypoint();

        // Smoothly rotate towards the target rotation
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    void MoveTowardsWaypoint()
    {
        // Calculate the direction towards the current waypoint
        Vector3 direction = (waypoints[currentWaypointIndex].position - transform.position).normalized;

        // Move the spider towards the current waypoint
        transform.Translate(direction * movementSpeed * Time.deltaTime, Space.World);

        // Check if the spider has reached the current waypoint
        if (Vector3.Distance(transform.position, waypoints[currentWaypointIndex].position) < 0.1f)
        {
            // Move to the next waypoint
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;

            // Set the target rotation based on the current waypoint index
            SetTargetRotation();
        }
    }

    void SetTargetRotation()
    {
        // Set the target rotation based on the current waypoint index
        if (currentWaypointIndex == 0)
        {
            targetRotation = Quaternion.Euler(0, 90f, 0f);
        }
        else if (currentWaypointIndex == 8)
        {
            targetRotation = Quaternion.Euler(0f, 90f, -90f);
        }
        else if (currentWaypointIndex == 10)
        {
            targetRotation = Quaternion.Euler(0f, -90f, 180f);
        }
        else if (currentWaypointIndex == 17)
        {
            targetRotation = Quaternion.Euler(180f, 90f, 90f);
        }
    }
}
