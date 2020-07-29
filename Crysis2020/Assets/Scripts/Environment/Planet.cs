using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    private HashSet<Rigidbody> affectedBodies = new HashSet<Rigidbody>();
    [SerializeField] private float gravityForce = 10f;
    [SerializeField] private bool doRotate = false;
    [SerializeField] private float spinSpeed = 1f;



    private void OnTriggerEnter(Collider other)
    {
        //if (other.attachedRigidbody != null)
        //{
        //    affectedBodies.Add(other.attachedRigidbody);
        //}
    }

    private void FixedUpdate()
    {
        if (doRotate)
        {
            transform.Rotate(transform.up, spinSpeed * Time.fixedDeltaTime, Space.Self);
        }
        

        //foreach (Rigidbody body in affectedBodies)
        //{
        //    if (body != null)
        //    {
        //        Vector3 forceDirection = (transform.position - body.position).normalized;
        //        //
        //        //float distanceSqr = (transform.position - body.position).sqrMagnitude;
        //        //float strength = 10 * componentRigidbody.mass * body.mass * distanceSqr;
        //        //body.AddForce(forceDirection * strength);
        //        //
        //        body.AddForce(forceDirection * gravityForce);
        //    }
        //    else
        //    {
        //        affectedBodies.Remove(default(Rigidbody));
        //    }

        //}
    }
}
