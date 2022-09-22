using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCP_Ch4_Pen : MonoBehaviour
{
    [SerializeField] private float shakingSpeed = 10.0f;
    [SerializeField] private float shakingAmount = 5.0f;
    private float originRotationZ;

    // Start is called before the first frame update
    void Start()
    {
        //originRotationZ = -20.0f;
    }

    // Update is called once per frame
    void Update()
    {
        float z = Mathf.Sin(Time.time * shakingSpeed) * shakingAmount;
        transform.localRotation = Quaternion.Euler(new Vector3(0, 0, z));
    }
}
