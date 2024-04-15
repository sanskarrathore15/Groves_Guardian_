using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class RotateToMouse : MonoBehaviour
{
    public Camera cam;
    public float maximumLenght;
    private Ray rayMouse;
    private Vector3 pos;
    private Vector3 direction;
    private Quaternion rotation;
    //public GameObject PlayerCtrl;
    void Update()
    {
        if (cam != null)
        {
            RaycastHit hit;
            var mousePos = Input.mousePosition;
            rayMouse = cam.ScreenPointToRay(mousePos);
            if (Physics.Raycast(rayMouse.origin, rayMouse.direction, out hit, maximumLenght))
            {
                RotateToMouseDirection(gameObject, hit.point);
            }
            else
            {
                var pos = rayMouse.GetPoint(maximumLenght);
                RotateToMouseDirection(gameObject, pos);
            }
        }
        else
        {
            Debug.Log("No Camera");
        }
        }
    
    void RotateToMouseDirection(GameObject obj, Vector3 destination)
    {
        direction = destination - obj.transform.position;
        rotation = Quaternion.LookRotation(direction);

        // Lock rotation around y and z axes
        Vector3 eulerRotation = rotation.eulerAngles;
        eulerRotation.y = 0f;
        eulerRotation.z = 0f;
        obj.transform.rotation = Quaternion.Euler(eulerRotation);
    }

    public Quaternion GetRotation()
    {
        return rotation;
    }
}