/*
 * Author: Yitong Hu
 * Created: Dec 26th 2022
 * Description: Parent of all types of screens
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

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
    protected virtual void OnPlayableDirectorStopped(PlayableDirector playableDirector)
    {
        if (!playableDirector || playableDirector != director) return;
    }

    // ***** Repeating Motions for small items ******************************************************

    // To rotate constantly from left to right then right to left
    protected virtual void RotateLeftRotateRight(GameObject gameObj, float rotateSpeed, float rotateAmount)
    {
        if(gameObj == null) return;
        float z = Mathf.Sin(Time.time * rotateSpeed) * rotateAmount;
        gameObj.transform.rotation = Quaternion.Euler(new Vector3(0, 0, z));
    }

    // To float vertically
    protected virtual void MoveUpMoveDown(GameObject gameObj, float originY, float moveSpeed, float moveAmount)
    {
        if (gameObj == null) return;
        float y = Mathf.Sin(Time.time * moveSpeed) * moveAmount / 10;
        gameObj.transform.position = new Vector3(gameObj.transform.position.x, originY + y, gameObj.transform.position.z);
    }

    // To zoom in and out
    protected virtual void ZoomBigZoomSmall(GameObject gameObj, float originX, float originY, float zoomSpeed, float zoomAmount)
    {
        if (gameObj == null) return;
        float zoom = Mathf.Sin(Time.time * zoomSpeed) * zoomAmount / 10;
        gameObj.transform.localScale = new Vector3(originX + zoom, originY + zoom, gameObj.transform.position.z);
    }
}
