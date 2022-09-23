using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Timeline;
using UnityEngine.Playables;
using TMPro;

public class SCP_Base_CineManager : MonoBehaviour
{
    // Item Lists
    [SerializeField] protected GameObject[] screens;
    [SerializeField] protected TimelineAsset[] timelines;
    [SerializeField] [Multiline] protected string[] subtitleContent;
    [SerializeField] protected AudioClip[] bgms;

    // References
    protected GameObject screen;
    protected GameObject subtitle;
    protected TMP_Text subtitleText;
    protected PlayableDirector director;
    protected AudioSource bgm;

    // Parameters
    protected int screenIndex;
    protected int timelineIndex;
    protected int subtitleIndex;
    protected float subtitleDuration = 5.0f;

    protected virtual void OnEnable()
    {
        screen = GameObject.Find("Screen");
        subtitle = GameObject.Find("PNL_Subtitles");
        subtitleText = subtitle.transform.Find("TXT_Subtitles").gameObject.GetComponent<TMP_Text>();
        director = GetComponent<PlayableDirector>();
        director.stopped += OnPlayableDirectorStopped;
        bgm = GameObject.Find("BGM").GetComponent<AudioSource>();

        screenIndex = 0;
        timelineIndex = 0;
        subtitleIndex = 0;
    }

    protected void OnDisable()
    {
        director.stopped -= OnPlayableDirectorStopped;
    }

    // When a timeline finished
    protected virtual void OnPlayableDirectorStopped(PlayableDirector pd)
    {
        if (!pd || pd != director) return;
    }

    // Start next screen
    protected void StartNextScreen(int index)
    {
        RemoveAllChildren(screen);
        GameObject next = Instantiate(screens[index]);
        next.transform.parent = screen.transform;
        screenIndex = index;
    }

    // Start next subtitle
    protected void StartNextSubtitle(string content)
    {
        RemoveAllChildren(screen);
        subtitle.SetActive(true);
        subtitleText.text = content;
    }

    // Click on subtitle panel to close it and display screen
    protected virtual void CloseSubtitle()
    {
        subtitle.SetActive(false);
    }

    protected void RemoveAllChildren(GameObject obj)
    {
        while (obj.transform.childCount > 0)
            DestroyImmediate(obj.transform.GetChild(0).gameObject);
    }
}
