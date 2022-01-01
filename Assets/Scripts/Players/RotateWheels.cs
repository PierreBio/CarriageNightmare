using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateWheels : MonoBehaviour
{
    [SerializeField] float speed = 25;

    private void Update()
    {
        transform.Rotate(Vector3.right, speed * Time.deltaTime);
    }
}
