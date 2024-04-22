using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class spider_shoot: MonoBehaviour
{
    public GameObject bullet;
    public Transform bulletPos;

    public Transform player_pos;
    private float timer;
    public float range = 10;
    public float shoot_time = .5f;
    private float distance;
    // Start is called before the first frame update
    void Start()
    {
        // player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(transform.position, player_pos.transform.position);


        


    }

    void shoot()
    {
        Instantiate(bullet, bulletPos.position, Quaternion.identity);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (distance < range)
            {
                timer += Time.deltaTime;
                if (timer > shoot_time)
                {
                    timer = 0;
                    shoot();
                }
            }
        }
    }
}
