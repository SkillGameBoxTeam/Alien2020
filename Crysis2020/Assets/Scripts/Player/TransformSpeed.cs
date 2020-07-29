using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformSpeed : MonoBehaviour
{
    public Vector3 velocityTransform;
    private Vector3 lastTransformPos;
    // Start is called before the first frame update
    void Start()
    {
        velocityTransform = transform.position;
        lastTransformPos = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
       
        velocityTransform = (transform.position - lastTransformPos) / Time.fixedDeltaTime;
        lastTransformPos = transform.position;
    }
}
