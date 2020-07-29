using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputControl : Singleton<InputControl>
{
    [SerializeField] private VariableJoystick MoveJoy;
    [SerializeField] private bool KeyboardControll;
    [SerializeField] private float TimeKeyJumpDelay = 0.2f;
    [SerializeField] private float TimeKeyHitDelay = 0.2f;
    [SerializeField] private float TimeKeyShootDelay = 1f;
    private ControlKeyAndDelay controlKeyAndDelay;
    private ControlKeyAndDelay.KeyAndDelay JumpKeyAndDelay;
    private ControlKeyAndDelay.KeyAndDelay HitKeyAndDelay;
    private ControlKeyAndDelay.KeyAndDelay ShootKeyAndDelay;

    private float xCoef = 0f;

    private SoundControl soundControl;

    private UI_Control uI_Control;
    private PlayerControl playerControl;

    int i = 1;
    // Start is called before the first frame update
    void Start()
    {
        uI_Control = UI_Control.Instance;
        GameObject ControlKeyObj = new GameObject();
        controlKeyAndDelay = ControlKeyObj.AddComponent<ControlKeyAndDelay>();
        Input.gyro.enabled = true;
        Input.gyro.updateInterval = 0.0167f;
        JumpKeyAndDelay = new ControlKeyAndDelay.KeyAndDelay("Jump", TimeKeyJumpDelay);
        HitKeyAndDelay = new ControlKeyAndDelay.KeyAndDelay("Hit", TimeKeyHitDelay);
        ShootKeyAndDelay = new ControlKeyAndDelay.KeyAndDelay("Shoot", TimeKeyShootDelay);


        soundControl = SoundControl.Instance;
        playerControl = PlayerControl.Instance;

    }

    // Update is called once per frame
    void Update()
    {
        
        InputParams.xAxis = 0f;
        InputParams.zAxis = 0f;
        //zAxis = MoveJoy.Vertical + Input.GetAxis("Vertical");
        if (MoveJoy)
        {
            InputParams.xAxis = MoveJoy.Horizontal;
            InputParams.zAxis = MoveJoy.Vertical;
        }
        
        InputParams.xAxis += Input.GetAxis("Horizontal");
        InputParams.zAxis += Input.GetAxis("Vertical");

        
       
        if (GameStats.useSmartAcceleration)
        {
            //if (InputParams.xAxis == 0 && InputParams.zAxis !=0)
            //{
            //    xCoef = -GameStats.gSensCoef * Input.gyro.gravity.x;
            //}
            InputParams.xAxis += GameStats.gSensCorrective * GameStats.gSensCoef + Input.gyro.gravity.x * GameStats.gSensCoef;
        }
        



        //if (Mathf.Abs(Input.gyro.attitude.x) > 0.02f)
        //{
        //    InputParams.xAxis += Input.gyro.gravity.x;
        //}
        //else if (Input.gyro.attitude.x < -0.05f)
        //{
        //    InputParams.xAxis += 1f;
        //}



        if (KeyboardControll)
        {
            //if ((Input.GetButtonDown("Jump")) && JumpKeyAndDelay.IsAvailable)
            //{
            //    InputParams.jumpButton = true;

            //    //StartKeyDelay?
            //}

            if (Input.GetButton("Jump"))
            {
                InputParams.jumpButton = true;
            }
            if (Input.GetButtonUp("Jump"))
            {
                InputParams.jumpButton = false;
            }



            if (Input.GetButtonDown("Fire1"))
            {
                InputParams.hitButton = true;
                
                //StartKeyDelay?
            }

            if (Input.GetButtonDown("Fire2"))
            {
                ShootButtonDown();
            }
        }

        if (GameStats.shootAviable == false && ShootKeyAndDelay.IsAvailable)
        {
            uI_Control.ShowShootButton(true);
            GameStats.shootAviable = true;
        }
        else if (GameStats.shootAviable == true && !ShootKeyAndDelay.IsAvailable)
        {
            uI_Control.ShowShootButton(false);
            GameStats.shootAviable = false;
        }
    }

    //protected void OnGUI()
    //{
    //    //GUILayout.Label("input.gyro.attitude: " + Input.gyro.attitude.eulerAngles);
    //    //GUILayout.Label("Input.gyro.attitude.x : " + Input.gyro.attitude.eulerAngles.x);
    //    //GUILayout.Label("Input.gyro.attitude.y : " + Input.gyro.attitude.eulerAngles.y);
    //    //GUILayout.Label("Input.gyro.attitude.z : " + Input.gyro.attitude.eulerAngles.z);
    //    //GUILayout.Label("" + GyroToUnity(Input.gyro.attitude).eulerAngles.normalized);

    //    //GUILayout.Label("" + Input.gyro.attitude);
    //    //GUILayout.Label("" + DeviceRotation.Get());
    //    GUILayout.Label("" + Input.gyro.userAcceleration);
    //    //GUILayout.Label("" + Input.gyro.rotationRateUnbiased.y);
    //    //GUILayout.Label("" + Input.gyro.rotationRateUnbiased.x);
    //    //GUILayout.Label("" + Input.gyro.attitude);
    //    //GUILayout.Label("" + Input.gyro.attitude.eulerAngles);
    //    //GUILayout.Label("" + GyroToUnity(Input.gyro.attitude));
    //    //GUILayout.Label("" + Input.gyro.gravity);
    //    GUILayout.Label("" + userAcceleration);


    //}
          

    public void HitButtonDown()
    {
        InputParams.hitButton = true;
        
    }

    public void ShootButtonDown()
    {
        if (ShootKeyAndDelay.IsAvailable)
        {
            InputParams.shootButton = true;
            controlKeyAndDelay.StartKeyDelay(ShootKeyAndDelay);

        }
    }

    public void JumpButtonDown()
    {
        playerControl.JumpPlayer();
    }


    private static Quaternion GyroToUnity(Quaternion q)
    {
        return new Quaternion(q.x, q.y, -q.z, -q.w);
    }
}


public static class InputParams
{
    public static float zAxis;
    public static float xAxis;
    public static float yAxis;
    public static bool jumpButton;
    public static bool hitButton;
    public static bool shootButton;
}
