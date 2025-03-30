using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject target;
    public float smooth;
    void Start()
    {
        target = GameObject.Find("Player");
    }

    
    void Update()
    {
        transform.position = Vector3.Slerp(transform.position, new Vector3(target.transform.position.x, target.transform.position.y, target.transform.position.z-10 ), smooth);
    }
}
