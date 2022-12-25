/*
 * Author: Yitong Hu
 * Created: Dec 25th 2022
 * Description: CineManager plays screens by sequence
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class SCP_CineManager : MonoBehaviour
{
    // References
    private GameObject screenRoot;
    private PlayableDirector director;

    // Item Lists
    [SerializeField] private GameObject[] screens;

    // Variables
    private int screenIndex;

    // Initialization
    private void Awake()
    {
        director = GetComponent<PlayableDirector>();

        screenIndex = 0;
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
    private void Start()
    {
        // Find references in scene
        screenRoot = GameObject.Find("ScreenRoot");

        // Start 1-1-1
        StartScreen(screenIndex);
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    // Called when a timeline finished
    private void OnPlayableDirectorStopped(PlayableDirector pd)
    {
        if (!pd || pd != director) return;
    }

    // To start a screen by index
    private void StartScreen(int index)
    {
        RemoveAllChildren(screenRoot);
        GameObject nextScreen = Instantiate(screens[index]);
        nextScreen.transform.parent = screenRoot.transform;
        Debug.Log("Start screen " + index);
    }

    public void StartNextScreen()
    {
        if (screenIndex >= screens.Length-1)
        {
            Debug.Log("No more screen to play!");
            RemoveAllChildren(screenRoot);
            return;
        }

        screenIndex++;
        Debug.Log("Start next screen " + screenIndex);
        StartScreen(screenIndex);
    }

    // To remove all children objects of an object
    private void RemoveAllChildren(GameObject parentObject)
    {
        while (parentObject.transform.childCount > 0)
            DestroyImmediate(parentObject.transform.GetChild(0).gameObject);
    }
}
