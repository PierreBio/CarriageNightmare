using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorHitBox : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Destroy(other);
        Destroy(gameObject);
    }
}
