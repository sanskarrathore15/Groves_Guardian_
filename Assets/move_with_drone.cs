using UnityEngine;

public class move_with_drone : MonoBehaviour
{
    public Transform[] waypoints;
    public float moveSpeed = 1.0f;
    private int currentWaypointIndex = 0;
    private bool movingForward = true;

    // Update is called once per frame
    void Update()
    {
        if (waypoints.Length == 0)
        {
            Debug.LogError("No waypoints assigned.");
            return;
        }

        // Calculate direction to move
        Vector3 direction = (waypoints[currentWaypointIndex].position - transform.position).normalized;

        // Move towards the current waypoint
        transform.Translate(direction * moveSpeed * Time.deltaTime);

        // Check if the object has reached the current waypoint
        if (Vector3.Distance(transform.position, waypoints[currentWaypointIndex].position) <= 0.1f)
        {
            // Update current waypoint index
            if (movingForward)
            {
                currentWaypointIndex++;
                // Check if reached the end of waypoints array
                if (currentWaypointIndex >= waypoints.Length)
                {
                    // Set moving direction to backward
                    movingForward = false;
                    // Move to the previous waypoint
                    currentWaypointIndex = waypoints.Length - 2;
                }
            }
            else
            {
                currentWaypointIndex--;
                // Check if reached the beginning of waypoints array
                if (currentWaypointIndex < 0)
                {
                    // Set moving direction to forward
                    movingForward = true;
                    // Move to the next waypoint
                    currentWaypointIndex = 1;
                }
            }
        }
    }
}
