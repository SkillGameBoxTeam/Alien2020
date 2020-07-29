using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortableHashSet : MonoBehaviour
{
    private void Start()
    {
        
         GameBuilder.Instance.AddPortableToHashSet(gameObject);

    }
}
