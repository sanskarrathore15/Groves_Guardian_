using UnityEngine;

public class spider_waypoints : MonoBehaviour
{
    public Transform[] waypoints;   // Array of waypoints the spider will follow
    public float movementSpeed = 2.0f; // Speed at which the spider moves between waypoints

    private int currentWaypointIndex = 0; // Index of the current waypoint the spider is moving towards
    private float previousZPosition; // Previous z-coordinate of the spider

    void Start()
    {
        // Initialize the previous z-coordinate with the initial position
        previousZPosition = transform.position.z;
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
    }

    void MoveTowardsWaypoint()
    {
        // Calculate the direction towards the current waypoint
        Vector3 direction = (waypoints[currentWaypointIndex].position - transform.position).normalized;

        // Move the spider towards the current waypoint
        transform.Translate(direction * movementSpeed * Time.deltaTime, Space.World);
       // transform.LookAt(waypoints[currentWaypointIndex].position);

        // Check if the spider has reached the current waypoint
        if (Vector3.Distance(transform.position, waypoints[currentWaypointIndex].position) < 0.1f)
        {
            // Move to the next waypoint
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;

            if(currentWaypointIndex == 0)
            {
                transform.rotation = Quaternion.Euler(0, 90f, 0f);

            }

            // Check if the spider is moving along the z-axis from a lower to a higher value
            if (currentWaypointIndex == 8)
            {

                // Rotate the spider along the x-axis by 180 degrees
                transform.rotation = Quaternion.Euler(0f,90f,-90f);
            }
            if (currentWaypointIndex == 10)
            {

                // Rotate the spider along the x-axis by 180 degrees
               transform.rotation = Quaternion.Euler(0f,-90f,180f);
            }
            if (currentWaypointIndex == 17)
            {

                // Rotate the spider along the x-axis by 180 degrees
                transform.rotation = Quaternion.Euler(180f, 90f, 90f);
            }

            // Update the previous z-coordinate
            // previousZPosition = transform.position.z;
        }
    }
}
