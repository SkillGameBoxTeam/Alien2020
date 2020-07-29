using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonRotate : MonoBehaviour
{
    [SerializeField] float AngleInSecond = 1f;
    void Update()
    {
        transform.RotateAround(Vector3.zero, Vector3.forward, AngleInSecond*Time.deltaTime);
    }
}
