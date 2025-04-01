using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject target;
    public float smooth;
    DoorManager doorManager;
    void Start()
    {
        target = GameObject.Find("Player");
        doorManager = FindAnyObjectByType<DoorManager>();
    }

    
    void Update()
    {
        if (!doorManager.isChoosing)
        {
            transform.position = Vector3.Slerp(transform.position, new Vector3(target.transform.position.x, target.transform.position.y, target.transform.position.z - 10), smooth);

        }
    }
}
