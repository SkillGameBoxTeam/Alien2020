using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : Singleton<PlayerControl>
{
    [SerializeField] private float MoveForce;
    public float MoveForceKoef = 1;
    [SerializeField] private float FallMoveForce;
    [SerializeField] private float JumpDelayBefore;
    [SerializeField] private float DragAfterLand;
    [SerializeField] private float JumpLengthForce;
    [SerializeField] private float JumpUpForce;
    [SerializeField] private float RotateCoef = 20f;
    private PlayerAnimationControl anim;
    public bool CanMove = true;
    
    public bool autorun = false;

    private SoundControl soundControl;

    private enum MoveTypeEnum
    {
        Worldspace,
        LocalTurn
    }

    [SerializeField] private MoveTypeEnum MoveType = MoveTypeEnum.Worldspace;

    [System.NonSerialized] public Rigidbody rb;
    private Vector3 RotVector;
    private float startDrag;
    private BodySurfaceLandControl bodySurfaceLandControl;
    [SerializeField] private float maxHight = 60f;
    private float JumpUpForceInstance = 300f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        startDrag = rb.drag;
        bodySurfaceLandControl = GetComponent<BodySurfaceLandControl>();
        anim = GetComponent<PlayerAnimationControl>();
        soundControl = SoundControl.Instance;

    }

    void Update()
    {
        //bodySurfaceLandControl.PlayerSurfaceAngle();
        FallAnimControl();
        PlayerLandingControl();
    }
    private void FixedUpdate()
    {
        Quaternion rotation = Quaternion.FromToRotation(-transform.up, GameBuilder.Instance.transform.position - transform.position);
        transform.rotation = rotation * transform.rotation;

        PlayerMove();
        BodyTrigers.isOnGround = bodySurfaceLandControl.CheckIsOnGround(BodyTrigers.isOnGround, BodyTrigers.isJump, ref BodyTrigers.doLanding);

    }

    private void FallAnimControl()
    {
        if (BodyTrigers.isOnGround == false && BodyTrigers.isJump == false && rb.velocity.y != 0)
        {
            BodyTrigers.isFall = true;
        }
         
        if (BodyTrigers.isFall == true)
        {
            anim.Fall(true);
        }
        else
        {
            anim.Fall(false);
        }
    }

    private void PlayerLandingControl()
    {
        if (BodyTrigers.doLanding)
        {
            anim.Land();
            BodyTrigers.isJump = false;
            BodyTrigers.isFall = false;
            BodyTrigers.doLanding = false;
        }

    }

    private void PlayerMove()
    {
        if (autorun && InputParams.zAxis >= 0)
        {
            InputParams.zAxis = 1;
        }

        if (InputParams.xAxis != 0f || InputParams.zAxis != 0f)// при ненулевых значения хотябы одной из осей
        {
            anim.SetRunSpeed(Mathf.Abs(InputParams.xAxis));
            RotVector.Set(InputParams.xAxis, 0f, InputParams.zAxis);

            float vmax = Time.fixedDeltaTime * MoveForce * MoveForceKoef;

            //поворот+
            
            Vector3 RotProjection = Vector3.ProjectOnPlane(RotVector, transform.up); 
            float angleTurnY;
            if (MoveType == MoveTypeEnum.Worldspace)
            {
                angleTurnY = Vector3.SignedAngle(transform.forward, RotProjection, transform.up);
                transform.Rotate(0f, angleTurnY * RotateCoef * Time.fixedDeltaTime, 0f, Space.Self);
            }
            else if (MoveType == MoveTypeEnum.LocalTurn)
            {
                //BodyTrigers.isFall == false
                if (true)
                {
                    transform.Rotate(0f, InputParams.xAxis * RotateCoef * Time.fixedDeltaTime, 0f, Space.Self);
                }
               
            }


            //поворот-
            //BodyTrigers.isOnGround && !BodyTrigers.isJump && !BodyTrigers.isFall
            if (true)
            {
                anim.Run(true);
                if ((CanMove)) {
                    if (MoveType == MoveTypeEnum.Worldspace)
                    {
                  
                        float curMaxV = getDivCoefWithMaxLimit(vmax, InputParams.xAxis, InputParams.zAxis);
                        rb.velocity = new Vector3(RotProjection.x * curMaxV, rb.velocity.y, RotProjection.z * curMaxV);

                    }else if (MoveType == MoveTypeEnum.LocalTurn)
                    {
                        Vector3 down = Vector3.Project(rb.velocity, transform.up);
                        Vector3 forward = transform.forward * InputParams.zAxis * vmax;
                        rb.velocity = down + forward;
                        

                    }
                }
            }
                
            
            else if (BodyTrigers.isFall)
            {
                if (MoveType == MoveTypeEnum.Worldspace)
                {
                    rb.AddForce(RotVector * Time.fixedDeltaTime * FallMoveForce, ForceMode.VelocityChange);
                }
                else if (MoveType == MoveTypeEnum.LocalTurn)
                {
                    float curMaxV = getDivCoefWithMaxLimit(FallMoveForce, InputParams.xAxis, InputParams.zAxis);
                    Vector3 down =  Vector3.Project(rb.velocity, transform.up);
                    Vector3 forward = transform.forward * InputParams.zAxis * curMaxV;
                    Vector3 right = transform.right * InputParams.xAxis * curMaxV;
                    rb.velocity = right + down + forward;
                }
                anim.Run(false);
            }

        }
        else
        {
            anim.Run(false);
        }

        if (InputParams.jumpButton && Vector3.Distance(transform.position, Vector3.zero)<= maxHight )
        {
            soundControl.Jump();
            //InputParams.jumpButton = false;

                StartCoroutine(JumpAfterDelay(JumpDelayBefore, true));

        }
    }

    private float getDivCoefWithMaxLimit(float vmax, float koefA, float koefB)
    {
        float curMaxV = vmax;
        float divKoef = Mathf.Abs(koefA) + Mathf.Abs(koefB);
        if (divKoef > 1)
        {
            curMaxV /= divKoef;
        }
        return curMaxV;
    }

    /// <summary>
    /// прыжок с задержко перед ним
    /// </summary>
    /// <param name="delay">время задержки</param>
    /// /// <param name="stopBeforeJump">притормаживать перед прыжком</param>
    /// <returns></returns>
    private IEnumerator JumpAfterDelay(float delay, bool stopBeforeJump = false)
    {
        //BodyTrigers.CanJump()
        if (true)
        {
            BodyTrigers.isJump = true;

            if (stopBeforeJump)
            {
                StartCoroutine(DragAfterLanding(rb, DragAfterLand, (delay / 2)));
            }

            anim.Jump();


            BodyTrigers.isOnGround = false;
            yield return new WaitForSeconds(delay);


            float koef = 1;
            float divKoef = Mathf.Abs(InputParams.xAxis) + Mathf.Abs(InputParams.zAxis);
            if (divKoef != 0)
            {
                koef = JumpLengthForce / divKoef;
            }
            float addJumpForce = 0f;
            if (transform.parent)
            {
                TransformSpeed trSpeed = transform.parent.GetComponent<TransformSpeed>();
                if (trSpeed)
                {
                    addJumpForce = trSpeed.velocityTransform.y / 2;
                }
            }

            if (MoveType == MoveTypeEnum.Worldspace)
            {
                rb.velocity = new Vector3(InputParams.xAxis * koef, JumpUpForce + addJumpForce, InputParams.zAxis * koef);
            }
            else if (MoveType == MoveTypeEnum.LocalTurn)
            {
                rb.AddForce(transform.up * JumpUpForce);
            }

        }

        BodyTrigers.isJump = false;
        BodyTrigers.isFall = true;

    }

    public void JumpPlayer()
    {
        rb.AddForce(transform.up * JumpUpForceInstance, ForceMode.Acceleration);
        
    }

    

    /// <summary>
    /// Торможение персонажа после приземления.
    /// </summary>
    /// <param name="currRb">Тело для контроля</param>
    /// <param name="dragNew">сила торможения</param>
    /// <param name="dragTime">время торможения</param>
    /// <returns></returns>
    private IEnumerator DragAfterLanding(Rigidbody currRb, float dragNew, float dragTime)
    {

        currRb.drag = dragNew;
        yield return new WaitForSeconds(dragTime);
        currRb.drag = startDrag;
    }

   
}


