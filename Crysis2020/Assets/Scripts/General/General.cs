using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class General 
{
    private float enternalTimer = 0f;
    public bool GetTimeOut(float itimeToStep, float TimeDelta)
    {
        if (itimeToStep > 0)
        {
            if (enternalTimer >= itimeToStep)
            {
                enternalTimer = 0;
            }
            else
            {
                enternalTimer += TimeDelta;
            }
        }
        if (enternalTimer == 0)
        {
            return true;
        }
        
        return false;
    }
}
