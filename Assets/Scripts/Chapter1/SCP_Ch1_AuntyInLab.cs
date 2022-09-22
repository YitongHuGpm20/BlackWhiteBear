using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCP_Ch1_AuntyInLab : MonoBehaviour
{
    [SerializeField] private GameObject exitVolume;
    private SCP_Ch1_CineManager cineManager;
    private Vector3 dragOffset;
    private Camera cam;
    private float originY;

    private void Start()
    {
        cineManager = GameObject.Find("CineManager_Ch1").GetComponent<SCP_Ch1_CineManager>();
        cam = Camera.main;
        originY = transform.position.y; 
    }

    private void Update()
    {
        if (transform.position.x > 4)
            transform.position = new Vector3(0, transform.position.y, transform.position.z);
    }

    private void OnMouseDown()
    {
        dragOffset = transform.position - GetMousePos();
    }

    private void OnMouseDrag()
    {
        transform.position = GetMousePos() + dragOffset;
    }

    private Vector3 GetMousePos()
    {
        Vector3 pos = cam.ScreenToWorldPoint(Input.mousePosition);
        pos.z = 0;
        pos.y = originY; 
        return pos;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == exitVolume)
        {
            Debug.Log("Left scientists!");
            Destroy(exitVolume);
            cineManager.LeftScientists();
        }
    }
}
