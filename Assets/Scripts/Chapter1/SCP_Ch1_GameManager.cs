using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCP_Ch1_GameManager : MonoBehaviour
{
    private SCP_Ch1_CineManager cineManager;

    // Cleaning Mini Game
    [SerializeField] private GameObject screen4;
    [SerializeField] private GameObject subtitle;
    private GameObject mask;
    private GameObject collidePoints;
    private GameObject dirt;
    private GameObject dirtBrush;

    // Cleaning cursor customization
    [SerializeField] private Texture2D luffa;
    [SerializeField] private CursorMode cursorMode = CursorMode.ForceSoftware;
    [SerializeField] private Vector2 hotSpot = Vector2.zero;

    // Status
    [HideInInspector] public bool miniGameCleaning = false;
    private bool isPressed = false;
    private bool fadeOut = false;
    [SerializeField] private float fadeSpeed = 0.5f;

    private void Start()
    {
        cineManager = GameObject.Find("CineManager_Ch1").GetComponent<SCP_Ch1_CineManager>();

        mask = screen4.transform.Find("SPM_S1-4_DirtMask").gameObject;
        collidePoints = screen4.transform.Find("CollidePoints").gameObject;
        dirt = screen4.transform.Find("SPR_S1-4_Dirt").gameObject;
        dirtBrush = screen4.transform.Find("DirtBrush").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        // Spawn brushes on dirt during Cleaning mini game
        if (miniGameCleaning && screen4.activeSelf && !subtitle.activeSelf)
        {
            Cursor.SetCursor(luffa, hotSpot, cursorMode);

            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pos.z = 0;

            if (isPressed)
            {
                GameObject brush = Instantiate(mask, pos, Quaternion.identity);
                brush.transform.parent = dirtBrush.transform;
            }

            if (Input.GetMouseButtonDown(0))
                isPressed = true;
            else if (Input.GetMouseButtonUp(0))
                isPressed = false;

            if (collidePoints.transform.childCount == 0)
                FinishedCleaning();
        }
        else
        {
            Cursor.SetCursor(null, Vector2.zero, cursorMode);
        }

        // Fade out remained dirt after Cleaning mini game
        if (fadeOut)
        {
            Color dirtColor = dirt.GetComponent<SpriteRenderer>().color;
            float fadeAmount = dirtColor.a - (fadeSpeed * Time.deltaTime);
            dirtColor = new Color(dirtColor.r, dirtColor.g, dirtColor.b, fadeAmount);
            dirt.GetComponent<SpriteRenderer>().color = dirtColor;
            if (dirtColor.a <= 0)
            {
                while (dirtBrush.transform.childCount > 0)
                    DestroyImmediate(dirtBrush.transform.GetChild(0).gameObject);
                fadeOut = false;
                StartCoroutine(cineManager.StartRealizedBWB());
            }   
        }
    }

    private void FinishedCleaning()
    {
        miniGameCleaning = false;
        fadeOut = true;
    }
}
