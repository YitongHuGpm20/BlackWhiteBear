using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCP_Ch4_FadeInColor : MonoBehaviour
{
    [SerializeField] private float fadeSpeed = 1.0f;
    [HideInInspector] public bool fadeIn = false;
    private SCP_Ch1_CineManager cineManager;

    // Start is called before the first frame update
    void Start()
    {
        cineManager = GameObject.Find("CineManager_Ch1").GetComponent<SCP_Ch1_CineManager>();
    }

    private void OnEnable()
    {
        //fadeIn = true;
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
                cineManager.DisplayEndDialog4();
            }
        }
    }
}
