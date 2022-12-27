/*
 * Author: Yitong Hu
 * Created: Dec 26th 2022
 * Description: Wash baby with luffa
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCP_PS_1_1_5 : SCP_PuzzleScreen
{
    // References
    private GameObject mask;
    private GameObject collidePoints;
    private GameObject dirt;
    private GameObject dirtBrush;

    // Variables
    [SerializeField] private Texture2D luffa;
    [SerializeField] private CursorMode cursorMode; //CursorMode.ForceSoftware;
    [SerializeField] private Vector2 cursorHotSpot; //Vector2.zero;
    [SerializeField] private float fadeSpeed; //0.5
    private bool isPressing;
    private bool fadeOut;

    // Initialization
    protected override void Awake()
    {
        base.Awake();

        fadeSpeed = 0.5f;
        isPressing = false;
        fadeOut = false;
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        // Find references
        mask = transform.Find("SPM_DirtMask").gameObject;
        collidePoints = transform.Find("CollidePoints").gameObject;
        dirt = transform.Find("SPR_Dirt").gameObject;
        dirtBrush = transform.Find("DirtBrush").gameObject;

        // Set logic
        Cursor.SetCursor(luffa, cursorHotSpot, cursorMode);
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        // Change cursor to luffa, spawn mask to cover dirt
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pos.z = 0;

        if (Input.GetMouseButtonDown(0))
            isPressing = true;
        else if (Input.GetMouseButtonUp(0))
            isPressing = false;

        if (isPressing)
        {
            GameObject brush = Instantiate(mask, pos, Quaternion.identity);
            brush.transform.parent = dirtBrush.transform;
        }

        if (collidePoints && collidePoints.transform.childCount == 0)
            FinishedCleaning();

        // Fade out remained dirt after cleaned all collide points
        if (fadeOut)
        {
            Color dirtColor = dirt.GetComponent<SpriteRenderer>().color;
            FadeOut(dirt, dirtColor, fadeSpeed);
            if (dirtColor.a <= 0)
            {
                cineManager.RemoveAllChildren(dirtBrush);
                fadeOut = false;
                canPlayNextScreen = true;
            }
        }
    }

    private void FinishedCleaning()
    {
        Cursor.SetCursor(null, Vector2.zero, cursorMode);
        fadeOut = true;
    }
}
