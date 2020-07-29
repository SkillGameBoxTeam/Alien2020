using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetConstraint : MonoBehaviour
{
    private void FixedUpdate()
    {
        Quaternion rotation = Quaternion.FromToRotation(-transform.up, GameBuilder.Instance.transform.position - transform.position);
        transform.rotation = rotation * transform.rotation;
    }
}
