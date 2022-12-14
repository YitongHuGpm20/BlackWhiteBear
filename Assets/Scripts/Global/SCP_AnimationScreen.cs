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

public class SCP_AnimationScreen : SCP_ScreenBase
{
    // Called when a timeline finished
    protected override void OnPlayableDirectorStopped(PlayableDirector p)
    {
        base.OnPlayableDirectorStopped(p);
        cineManager.StartNextScreen();
    }
}
