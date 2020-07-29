using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageControl : MonoBehaviour
{
    public int damage;
    public TypeOfTrigger destroyOnEnter = TypeOfTrigger.destroyOnEnter;
    [System.NonSerialized] public bool destroyObj = false;
    [SerializeField] private float delayToDamage = 0f;
    [SerializeField] bool makeBoom = false;
    [SerializeField] private ParticleSystem boom;
    [SerializeField] private bool mometalInactive = true;
    [SerializeField] EnemyControl enemyControl;
    [SerializeField] bool destroyOnlyInJump = false;


    [SerializeField] private bool addPointsOnDestroy = false;
    [SerializeField] private int pointsOnDestroy = 1;

    public enum TypeOfTrigger
    {
        destroyOnEnter,
        destroyOnAttackEnter,
        dontDestroy

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            bool playerAttack =
                   BodyTrigers.isAttack && (!destroyOnlyInJump || (destroyOnlyInJump && BodyTrigers.isJump));
            if (destroyOnEnter == TypeOfTrigger.dontDestroy)
            {

                if (damage > 0 && !playerAttack)
                {
                    StartCoroutine(DoDamage(delayToDamage, damage));
                }
            }
            else
            {
                if (playerAttack)
                {
                    if (makeBoom)
                    {
                        boom.Play();
                    }
                
                    if (addPointsOnDestroy)
                    {
                        GameBuilder.Instance.IncreaseHealth(pointsOnDestroy);
                    }
                }
                else
                {
                    if (damage>0)
                    {
                        StartCoroutine(DoDamage(delayToDamage, damage));
                    }
                }

                if (destroyOnEnter == TypeOfTrigger.destroyOnEnter ||
                    (destroyOnEnter == TypeOfTrigger.destroyOnAttackEnter && playerAttack))
                {
                    if (makeBoom)
                    {
                        boom.Play();
                    }
                    destroyObj = true;
                    if (mometalInactive)
                    {
                        gameObject.SetActive(false);
                    }
                    enemyControl.DestroyObj();
                }
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (destroyOnEnter == TypeOfTrigger.destroyOnAttackEnter && BodyTrigers.isAttack)
            {
                if (makeBoom)
                {
                    boom.Play();
                }
                destroyObj = true;
                if (mometalInactive)
                {
                    gameObject.SetActive(false);
                }
                enemyControl.DestroyObj();
            }
        }
    }

    private IEnumerator DoDamage(float delay = 0f, int currDamage = 0)
    {
        if (delay > 0)
        {
            yield return new WaitForSeconds(delay);
        }
        GameBuilder.Instance.DecreaseHealth(currDamage);
        yield break;
    }

   

}
