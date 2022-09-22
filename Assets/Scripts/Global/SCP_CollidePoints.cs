using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCP_CollidePoints : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("DirtMask"))
            Destroy(gameObject);
    }
}
