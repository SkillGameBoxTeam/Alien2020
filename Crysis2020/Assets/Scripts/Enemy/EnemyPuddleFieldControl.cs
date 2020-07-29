using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPuddleFieldControl : MonoBehaviour
{

    [SerializeField] private float slowKoef = 0.5f;
    [SerializeField] private int damage = 1;
    [SerializeField] private int timeout = 2;
    private bool doDamage = false;
    private PlayerControl playeInst;

    private void Start()
    {
        playeInst = PlayerControl.Instance;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playeInst.MoveForceKoef = slowKoef;
            doDamage = true;
            StartCoroutine(ActAfterDelay());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playeInst.MoveForceKoef = 1;
            doDamage = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playeInst.MoveForceKoef = slowKoef;
            doDamage = true;
        }
    }

    private IEnumerator ActAfterDelay()
    {
        while (doDamage)
        {
            yield return new WaitForSeconds(timeout);
            if (doDamage)
            {
                GameBuilder.Instance.DecreaseHealth(damage);
            }
            
        }
        
       
    }
}
