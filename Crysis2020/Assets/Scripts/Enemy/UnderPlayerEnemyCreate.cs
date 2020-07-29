using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnderPlayerEnemyCreate : MonoBehaviour
{
    [SerializeField] GameObject OilField;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameBuilder.Instance.CreateObjUnderPlayer(OilField);
        }
        
    }

    

}
