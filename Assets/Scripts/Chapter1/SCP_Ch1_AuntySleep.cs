using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCP_Ch1_AuntySleep : MonoBehaviour
{
    [SerializeField] private float shakingSpeed = 2.0f;
    [SerializeField] private float shakingAmount = 3.0f;

    // Update is called once per frame
    void Update()
    {
        float z = Mathf.Sin(Time.time * shakingSpeed) * shakingAmount;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, z));
    }
}
