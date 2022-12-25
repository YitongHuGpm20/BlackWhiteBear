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

public class SCP_SubtitleScreen : MonoBehaviour
{
    // References
    private Button button;
    private GameObject clickPrompt;
    private TMP_Text subtitle;

    // Variables
    [Multiline]
    [SerializeField] private string subtitleContent;
    
    // Start is called before the first frame update
    void Start()
    {
        // Find references
        button = transform.GetChild(0).transform.GetChild(0).GetComponent<Button>();
        clickPrompt = button.gameObject.transform.Find("IMG_ClickPrompt").gameObject;
        subtitle = button.gameObject.transform.Find("TXT_Subtitles").gameObject.GetComponent<TMP_Text>();

        // Set variables
        subtitle.text = subtitleContent;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
