/*
 * Author: Yitong Hu
 * Created: Dec 25th 2022
 * Description: A screen displays one line of subtitle, click anywhere to continue
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SCP_SubtitleScreen : SCP_ScreenBase
{
    // References
    private Button button; // the whole panel is a button
    private GameObject clickPrompt;
    private TMP_Text subtitle;

    // Variables
    [Multiline] [SerializeField] private string subtitleContent;
    [SerializeField] private float displayDuration; //3
    [SerializeField] private float floatingSpeed; //5
    [SerializeField] private float floatingAmount; //20
    private float originY;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        // Find references
        button = transform.GetChild(0).transform.GetChild(0).GetComponent<Button>();
        clickPrompt = button.gameObject.transform.Find("IMG_ClickPrompt").gameObject;
        subtitle = button.gameObject.transform.Find("TXT_Subtitles").gameObject.GetComponent<TMP_Text>();

        // Set variables and functions
        button.interactable = false;
        button.onClick.AddListener(OnButtonClick);
        subtitle.text = subtitleContent;
        originY = clickPrompt.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (clickPrompt.activeSelf)
        {
            float y = Mathf.Sin(Time.time * floatingSpeed) * floatingAmount;
            clickPrompt.transform.position = new Vector3(clickPrompt.transform.position.x, originY + y, clickPrompt.transform.position.z);
        }

        if (displayDuration > 0)
            displayDuration -= Time.deltaTime;
        else
        {
            button.interactable = true;
            clickPrompt.SetActive(true);
        }   
    }

    // Called when click anywhere
    private void OnButtonClick()
    {
        button.interactable = false;
        Debug.Log("hi!");
        cineManager.StartNextScreen();
    }
}
