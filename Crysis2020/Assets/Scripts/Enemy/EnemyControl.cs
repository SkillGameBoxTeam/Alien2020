using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    [SerializeField] private bool isVirus = false;
    [SerializeField] private float delayToDestroy = 0f;
    [SerializeField] private GameBuilder.TypeOfHashSet typeOfEnemy = GameBuilder.TypeOfHashSet.nope;

    private void Start()
    {
        if (isVirus)
        {
            GameStats.IncreaseVirus();
        }

    }


    public void DestroyObj()
    {
        if (isVirus)
        {
            GameStats.DecreaseVirus();
        }

        GameBuilder.Instance.SetDestroyObj(gameObject, typeOfEnemy, delayToDestroy);
        
    }



}
