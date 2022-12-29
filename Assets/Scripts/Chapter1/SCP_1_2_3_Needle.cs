/*
 * Author: Yitong Hu
 * Created: Dec 29th 2022
 * Description: Detect needle collision hits baby collision
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCP_1_2_3_Needle : MonoBehaviour
{
    // References
    private SCP_PS_1_2_3 puzzle;
    
    // Start is called before the first frame update
    void Start()
    {
        puzzle = transform.parent.transform.parent.gameObject.GetComponent<SCP_PS_1_2_3>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "SPR_Baby")
            puzzle.NeedleHitButt();
    }
}
