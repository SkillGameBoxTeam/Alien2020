using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnPlanetPlayerControl : MonoBehaviour
{
    //public Transform PlayerCamera;

    private Rigidbody componentRigidbody;

    private void Start()
    {
        componentRigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        // Movement:
        // down - вектор скорости к центру планеты
        Vector3 down = Vector3.Project(componentRigidbody.velocity, transform.up);
        Vector3 forward = transform.forward * InputParams.zAxis * 4;
        Vector3 right = transform.right * InputParams.xAxis * 4;

        //componentRigidbody.AddForce((forward + right) * Time.fixedDeltaTime * 200);

        componentRigidbody.velocity = down + right + forward;

        // Rotation:
        //transform.Rotate(transform.up, Input.GetAxis("Mouse X") * 2);
        //PlayerCamera.Rotate(-Input.GetAxis("Mouse Y") * 2, 0, 0);
    }

    private void Update()
    {
        // Jumping:
        if (Input.GetKeyDown(KeyCode.Space))
        {
            componentRigidbody.AddForce(transform.up * 600);
        }
    }
}
