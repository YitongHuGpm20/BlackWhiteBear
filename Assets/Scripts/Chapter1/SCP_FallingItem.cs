/*
 * Author: Yitong Hu
 * Created: Jan 9th 2023
 * Description: This object is falling on the head of panda
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCP_FallingItem : MonoBehaviour
{
    // References
    [SerializeField] private Collider2D collider;
    private SCP_PS_1_3_3 puzzle;

    // Variable
    private bool hittedPanda = false;

    // Start is called before the first frame update
    void Start()
    { 
        puzzle = transform.parent.transform.parent.GetComponent<SCP_PS_1_3_3>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!hittedPanda)
        {
            if (other.gameObject.name == "SPR_Panda")
            {
                hittedPanda = true;
                puzzle.HurtPanda();
            }

            if (other.gameObject.name == "SPR_Floor")
                collider.enabled = false;
        }
    }
}
