using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartLoad : MonoBehaviour
{
    private void Awake()
    {
        DataControl.LoadData();
    }
  
}
