using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HashSetObjsControl : MonoBehaviour
{
    private enum TypeOfHashSet
    {
        bases,
        portables,
        constObj,
        nope,
        other
    }
    [SerializeField] private TypeOfHashSet typeOfHashSet = TypeOfHashSet.nope;

    // Start is called before the first frame update
    void Start()
    {
        if (typeOfHashSet == TypeOfHashSet.constObj)
        {
            GameBuilder.Instance.AddConstObjToHashSet(gameObject);
        }
        else if (typeOfHashSet == TypeOfHashSet.bases)
        {
            GameBuilder.Instance.AddBaseToHashSet(gameObject);
        }
        else if (typeOfHashSet == TypeOfHashSet.portables)
        {
            GameBuilder.Instance.AddPortableToHashSet(gameObject);
        }
        else if (typeOfHashSet == TypeOfHashSet.other)
        {
            GameBuilder.Instance.AddOtherObjToHashSet(gameObject);
        }
    }
}
