using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketControler : MonoBehaviour
{
    [SerializeField] private GameObject aim;
    [SerializeField] private GameObject rocket;
    [SerializeField] private GameObject boom;
    [SerializeField] private float timeOutToBoom = 5f;
    [SerializeField] private float timeOutAfterBoom = 5f;

    void Start()
    {
        StartCoroutine(DestroyOnTimeout());
    }

    IEnumerator DestroyOnTimeout()
    {
        yield return new WaitForSeconds(timeOutToBoom);
        aim.SetActive(false);
        rocket.SetActive(false);
        boom.transform.position = aim.transform.position;
        boom.SetActive(true);
        GameBuilder.Instance.DecreaseHealth();
        yield return new WaitForSeconds(timeOutAfterBoom);
        Destroy(gameObject);
    }

}
