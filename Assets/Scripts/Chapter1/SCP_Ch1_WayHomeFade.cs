using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCP_Ch1_WayHomeFade : MonoBehaviour
{
    [SerializeField] private float fadeSpeed = 0.2f;

    [HideInInspector] public bool fadeIn = false;
    [HideInInspector] public bool fadeOut = false;

    private SCP_Ch1_CineManager cineManager;

    // Start is called before the first frame update
    void Start()
    {
        cineManager = GameObject.Find("CineManager_Ch1").GetComponent<SCP_Ch1_CineManager>();
    }

    // Update is called once per frame
    void Update()
    {
        Color color = GetComponent<SpriteRenderer>().color;
        
        if (fadeIn)
        {
            float a = color.a - (fadeSpeed * Time.deltaTime);
            color = new Color(color.r, color.g, color.b, a);
            GetComponent<SpriteRenderer>().color = color;
            if (color.a <= 0)
            {
                fadeIn = false;
                cineManager.DisplayEndDialog();
            }
        }
        
        if (fadeOut)
        {
            float a = color.a + (fadeSpeed * Time.deltaTime);
            color = new Color(color.r, color.g, color.b, a);
            GetComponent<SpriteRenderer>().color = color;
            if (color.a >= 1)
            {
                fadeOut = false;
                Debug.Log("Chapter 1 ended!");
                cineManager.StartTravel();
            }
        }
    }

    public IEnumerator FadeIn()
    {
        yield return new WaitForSeconds(1);
        fadeIn = true;
    }

    public IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(1);
        fadeOut = true;
    }
}
