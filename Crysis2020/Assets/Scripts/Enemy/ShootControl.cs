using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootControl : MonoBehaviour
{
    [SerializeField] GameObject Shell;

    private void Shoot()
    {
        Instantiate(Shell, transform.position, Shell.transform.rotation, GameBuilder.Instance.gameObject.transform);
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Shoot();
        }
    }

}
