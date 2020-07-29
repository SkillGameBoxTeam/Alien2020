using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectAutoMove : MonoBehaviour
{
    [SerializeField] private float Speed = 1;
    [SerializeField] private float StopDistance = 0.1f;
    [SerializeField] private float timeToStep = 1f;
    public bool NoAutoMove = false;

    [SerializeField] private bool jump;
    [SerializeField] private float jumpForce = 20f;
    [SerializeField] private float timeToJump = 5f;
    [SerializeField] private float timeErrorJump = 0f;

    private Rigidbody rb;
    //private float enternalTimer;
    private General generalToStep;
    private General generalToJump;

    private enum TypeAutoMove{
        follow,
        rnddir,
        nope
    }
    [SerializeField] private TypeAutoMove typeAutoMove = TypeAutoMove.follow;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        transform.LookAt(PlayerControl.Instance.transform.position);
        generalToStep = new General();
        generalToJump = new General();
        if (timeErrorJump>0f)
        {
            timeToJump+= Random.Range(-timeErrorJump * timeToJump, timeErrorJump * timeToJump);
        }
    }

    private void FixedUpdate()
    {
        if (NoAutoMove == false)
        {
            Vector3 playerPosition = PlayerControl.Instance.transform.position;

            if (typeAutoMove == TypeAutoMove.follow)
            {
                if (Vector3.Distance(transform.position, playerPosition) > StopDistance)
                {
                    if (generalToStep.GetTimeOut(timeToStep, Time.fixedDeltaTime))
                    {
                        transform.LookAt(playerPosition, transform.up);
                    }
                    Quaternion rotation = Quaternion.FromToRotation(-transform.up, GameBuilder.Instance.transform.position - transform.position);
                    transform.rotation = rotation * transform.rotation;

                    Vector3 down = Vector3.Project(rb.velocity, transform.up);
                    Vector3 forward = transform.forward * Speed * Time.fixedDeltaTime;
                    rb.velocity = down + forward;

                }
                else
                {
                    rb.angularVelocity -= rb.angularVelocity;
                    rb.velocity -= rb.velocity;
                }
            }
            else if (typeAutoMove == TypeAutoMove.rnddir)
            {
                Quaternion rotation = Quaternion.FromToRotation(-transform.up, GameBuilder.Instance.transform.position - transform.position);
                transform.rotation = rotation * transform.rotation;

                if (generalToStep.GetTimeOut(timeToStep, Time.fixedDeltaTime))
                {
                    int rndQ = Random.Range(-1, 1);
                    if (rndQ == 0)
                    {
                        rndQ = 1;
                    }
                    transform.Rotate(transform.up, rndQ * 90);
                }

                Vector3 down = Vector3.Project(rb.velocity, transform.up);
                Vector3 forward = transform.forward * Speed * Time.fixedDeltaTime;
                rb.velocity = down + forward;
            }
            else if (typeAutoMove == TypeAutoMove.nope)
            {
                Quaternion rotation = Quaternion.FromToRotation(-transform.up, GameBuilder.Instance.transform.position - transform.position);
                transform.rotation = rotation * transform.rotation;
            }

            if (jump)
            {
                if (generalToJump.GetTimeOut(timeToJump, Time.fixedDeltaTime))
                {
                    rb.AddForce(transform.up * jumpForce);
                }
            }
        }
    }
}
