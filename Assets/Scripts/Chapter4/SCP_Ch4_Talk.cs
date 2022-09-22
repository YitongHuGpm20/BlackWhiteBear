using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SCP_Ch4_Talk : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private GameObject cineManager;
    [SerializeField] private float floatingSpeed = 1.0f;
    [SerializeField] private float floatingAmount = 0.2f;
    private float originY;

    private void Start()
    {
        originY = transform.position.y;
        AddPhysics2DRaycaster();
    }

    // Update is called once per frame
    void Update()
    {
        float y = Mathf.Sin(Time.time * floatingSpeed) * floatingAmount;
        transform.position = new Vector3(transform.position.x, originY + y, transform.position.z);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Clicked: " + eventData.pointerCurrentRaycast.gameObject.name);
        cineManager.GetComponent<SCP_Ch1_CineManager>().AskZoologist();
    }

    private void AddPhysics2DRaycaster()
    {
        Physics2DRaycaster physicsRaycaster = FindObjectOfType<Physics2DRaycaster>();
        if (physicsRaycaster == null)
            Camera.main.gameObject.AddComponent<Physics2DRaycaster>();
    }
}
