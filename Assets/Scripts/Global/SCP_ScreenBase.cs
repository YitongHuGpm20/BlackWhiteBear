/*
 * Author: Yitong Hu
 * Created: Dec 26th 2022
 * Description: Parent of all types of screens
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class SCP_ScreenBase : MonoBehaviour
{
    // References
    protected SCP_CineManager cineManager;
    protected PlayableDirector director;

    // Initialization
    protected virtual void Awake()
    {
        if(GetComponent<PlayableDirector>())
            director = GetComponent<PlayableDirector>();
    }

    // OnEnable is called everytime CineManager is activated
    protected virtual void OnEnable()
    {
        // Enable playable director
        if(director)
            director.stopped += OnPlayableDirectorStopped;
    }

    // OnDisable is called everytime CineManager is deactivated
    protected virtual void OnDisable()
    {
        // Disable playable director
        if(director)
            director.stopped -= OnPlayableDirectorStopped;
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        cineManager = GameObject.Find("CineManager").GetComponent<SCP_CineManager>();
    }

    // Called when a timeline finished
    protected virtual void OnPlayableDirectorStopped(PlayableDirector pd)
    {
        if (!pd || pd != director) return;
    }
}
