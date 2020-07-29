using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenguinTargetControl : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PenguinTarget"))
        {

        }
    }
}
