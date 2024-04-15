using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class attackMove : MonoBehaviour 
{
    public float speed;
    public float fireRate;
    public GameObject enemys;
    void Start()
    {
        enemys = GameObject.FindGameObjectWithTag("Enemy");
    }
    void Update()
    {
        if (speed != 0)
        {
           transform.position += transform.forward * (speed * Time.deltaTime);
            MoveTowardsPlayer();
        }
        else
        {
            Debug.Log("No Speed");
        }
    }
    public void MoveTowardsPlayer()
    {
        if (Vector3.Distance(transform.position, enemys.transform.position) < 10)
            transform.position = Vector3.MoveTowards(this.transform.position, enemys.transform.position, 40f * Time.deltaTime);

    }
}