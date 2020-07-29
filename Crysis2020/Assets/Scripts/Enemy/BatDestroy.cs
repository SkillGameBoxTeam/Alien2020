using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatDestroy : MonoBehaviour
{
    private void OnDestroy()
    {
        GameStats.DecreaseBat();
    }
}
