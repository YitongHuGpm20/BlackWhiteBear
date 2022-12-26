using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SCP_Subtitle : MonoBehaviour
{
    private Button button;
    private GameObject clickTutorial;
    private float floatingSpeed = 5.0f;
    private float floatingAmount = 20.0f;
    private float originY;
    private float clickTutorialTimer = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        button.interactable = false;
        button.onClick.AddListener(OnButtonClick);

        clickTutorial = transform.Find("IMG_ClickTutorial").gameObject;
        originY = clickTutorial.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (clickTutorial.activeSelf)
        {
            float y = Mathf.Sin(Time.time * floatingSpeed) * floatingAmount;
            clickTutorial.transform.position = new Vector3(clickTutorial.transform.position.x, originY + y, clickTutorial.transform.position.z);
        }

        if (clickTutorialTimer > 0)
            clickTutorialTimer -= Time.deltaTime;
        else
            clickTutorial.SetActive(true);
    }

    private void OnEnable()
    {
        StartCoroutine(ActivateClicking());
        clickTutorialTimer = 5.0f;
    }

    public IEnumerator ActivateClicking()
    {
        yield return new WaitForSeconds(1);
        button.interactable = true;
    }

    private void OnButtonClick()
    {
        button.interactable = false;
        clickTutorial.SetActive(false);
        clickTutorialTimer = 5.0f;
    }
}
