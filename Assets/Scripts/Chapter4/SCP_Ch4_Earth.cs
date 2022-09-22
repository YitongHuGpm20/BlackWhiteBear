using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCP_Ch4_Earth : MonoBehaviour
{
    [SerializeField] private float rotateSpeed = 0.5f;

    // Update is called once per frame
    void Update()
    {
        //float z = transform.localRotation.z + rotateSpeed * 10; 
        //transform.localRotation = Quaternion.Euler(new Vector3(0, 0, z));
        transform.Rotate(new Vector3(0, 0, rotateSpeed));
    }
}
