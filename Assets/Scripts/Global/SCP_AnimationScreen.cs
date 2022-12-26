/*
 * Author: Yitong Hu
 * Created: Dec 25th 2022
 * Description: A screen plays animation and automatically start next screen
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class SCP_AnimationScreen : MonoBehaviour
{
    // References
    [SerializeField] protected TimelineAsset timeline;
    private PlayableDirector director;
    private SCP_CineManager cineManager;

    // Initialization
    private void Awake()
    {
        director = GetComponent<PlayableDirector>();
    }

    // OnEnable is called everytime CineManager is activated
    private void OnEnable()
    {
        // Enable playable director
        director.stopped += OnPlayableDirectorStopped;
    }

    // OnDisable is called everytime CineManager is deactivated
    private void OnDisable()
    {
        // Disable playable director
        director.stopped -= OnPlayableDirectorStopped;
    }

    // Start is called before the first frame update
    void Start()
    {
        // Find references
        cineManager = GameObject.Find("CineManager").GetComponent<SCP_CineManager>();

        // Set variables and functions
        director.Play(timeline);
    }

    // Called when a timeline finished
    private void OnPlayableDirectorStopped(PlayableDirector pd)
    {
        if (!pd || pd != director) return;
        cineManager.StartNextScreen();
    }
}
