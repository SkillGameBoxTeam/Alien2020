using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyEnterControl : MonoBehaviour
{
    [SerializeField] private ParticleSystem boom;
    [SerializeField] private GameObject hideFirst;
    [SerializeField] private float destroyTime = 2f;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(BoomDestroy());
        }
    }

    IEnumerator BoomDestroy()
    {
        GameBuilder.Instance.IncreaseCurr();
        GameBuilder.Instance.DecreaseInGameMoneyPoint();
        hideFirst.SetActive(false);
        boom.Play();
        yield return new WaitForSeconds(destroyTime);
        Destroy(gameObject);
        
    }

}
