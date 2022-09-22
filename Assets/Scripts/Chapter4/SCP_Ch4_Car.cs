using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCP_Ch4_Car : MonoBehaviour
{
    [SerializeField] private float floatingSpeed = 2.0f;
    [SerializeField] private float floatingAmount = 0.5f;
    private float originY;

    private void Start()
    {
        originY = transform.position.y;
    }
    // Update is called once per frame
    void Update()
    {
        float y = Mathf.Sin(Time.time * floatingSpeed) * floatingAmount / 10;
        transform.position = new Vector3(transform.position.x, originY + y, transform.position.z);
    }
}
