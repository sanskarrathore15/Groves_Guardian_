using UnityEngine;

public class cawl_final : MonoBehaviour
{
    public Transform target; // Reference to the target object (the object to crawl on)
    public float speed = 1f; // Speed at which the crawler moves
    public float crawlHeight = 1f; // Height above the target to crawl
    public float range = 1f; // Range of movement along the z-axis

    private Vector3 originalPosition; // Original position of the crawler
    private bool moveForward = true; // Flag to control the direction of movement

    void Start()
    {
        originalPosition = transform.position;
    }

    void Update()
    {
        // Calculate the position along the z-axis in a looping manner
        float newZ = originalPosition.z + Mathf.PingPong(Time.time * speed, range * 2) - range;

        // Set the new position of the crawler
        transform.position = new Vector3(originalPosition.x, originalPosition.y + crawlHeight, newZ);
    }
}
