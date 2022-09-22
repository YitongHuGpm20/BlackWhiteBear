using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCP_Ch4_Greyscale : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private float duration = 1.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine(GreyscaleRoutine(duration, true));
    }

    public void StartGreyscaleRoutine()
    {
        StartCoroutine(GreyscaleRoutine(duration, true));
    }

    private IEnumerator GreyscaleRoutine(float d, bool isGreyscale)
    {
        float time = 0;
        while(d > time)
        {
            float durationFrame = Time.deltaTime;
            float ratio = time / d;
            float greyAmount = isGreyscale ? ratio : 1 - ratio;
            SetGreyscale(greyAmount);
            time += durationFrame;
            yield return null;
        }
        SetGreyscale(1);
    }

    public void SetGreyscale(float amount = 1)
    {
        spriteRenderer.material.SetFloat("greyscaleAmount", amount);
    }
}
