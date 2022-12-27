/*
 * Author: Yitong Hu
 * Created: Dec 26th 2022
 * Description: A screen has at least one puzzle to be solved and then starts next screen automatically
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCP_PuzzleScreen : SCP_ScreenBase
{
    // Variables
    protected float playNextScreenTimer = 2.0f;
    protected bool canPlayNextScreen = false;

    // Play every frame
    protected virtual void Update()
    {
        if(canPlayNextScreen)
        {
            if (playNextScreenTimer > 0)
                playNextScreenTimer -= Time.deltaTime;
            else
                cineManager.StartNextScreen();
        }
    }
}
