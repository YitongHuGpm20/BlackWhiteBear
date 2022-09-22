using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCP_Ch1_Stars : MonoBehaviour
{
    [SerializeField] private float zoomSpeed = 10.0f;
    [SerializeField] private float zoomAmount = 0.2f;
    private float originScaleX;
    private float originScaleY; 

    // Start is called before the first frame update
    void Start()
    {
        originScaleX = transform.localScale.x;
        originScaleY = transform.localScale.y;
    }

    // Update is called once per frame
    void Update()
    {
        float zoom = Mathf.Sin(Time.time * zoomSpeed) * zoomAmount / 10;
        transform.localScale = new Vector3(originScaleX + zoom, originScaleY + zoom, transform.position.z);
    }
}
