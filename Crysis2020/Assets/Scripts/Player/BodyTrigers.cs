using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class BodyTrigers 
{
    public static bool CanControl = true;
    public static bool isOnGround = true;
    public static bool isJump = false;
    public static bool isFall = false;
    public static bool doLanding = false;
    public static bool privilegeJump = false;
    public static bool isAttack = false;
    public static bool shoot = false;
    public static GameObject currPortableGameObject;

    public static bool CanJump()
    {
        if (isOnGround && !isJump && !isFall)
        {
            return true;
        }
        else if (privilegeJump)
        {
            privilegeJump = false;
            return true;
        }
        else
        {
            return false;
        }
        
    }
}
