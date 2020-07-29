using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthSpin : MonoBehaviour
{
    [SerializeField] float runSpeed = 1f;
    private void Update()
    {
        transform.Rotate(transform.up, runSpeed * Time.deltaTime, Space.Self);
    }
}
