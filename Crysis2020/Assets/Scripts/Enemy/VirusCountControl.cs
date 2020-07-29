using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirusCountControl : MonoBehaviour
{
    private void Start()
    {
        GameStats.IncreaseVirus();
    }

    private void OnDisable()
    {
        GameStats.DecreaseVirus();
    }
    
}
