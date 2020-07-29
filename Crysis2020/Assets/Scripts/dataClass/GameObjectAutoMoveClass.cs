using System;
using UnityEngine;

[Serializable]
public class GameObjectAutoMoveClass  
{
    public GameObject gameObj;

    public float Speed = 1;
    public float StopDistance = 0.1f;
    public float timeToStep = 1f;
    public bool NoAutoMove = false;

    public bool jump;
    public float jumpForce = 20f;
    public float timeToJump = 5f;
    public float timeErrorJump = 0f;

    public Rigidbody rb;
    public General generalToStep;
    public General generalToJump;

    public enum TypeAutoMove
    {
        follow,
        rnddir,
        nope
    }
    public TypeAutoMove typeAutoMove = TypeAutoMove.follow;


    public GameObjectAutoMoveClass()
    {
        generalToJump = new General();
        generalToStep = new General();
       
    }
    public GameObjectAutoMoveClass(GameObjectAutoMoveClass gameObjectAutoMoveClassCopy)
    {
        generalToJump = new General();
        generalToStep = new General();

       

        gameObj = gameObjectAutoMoveClassCopy.gameObj;

        Speed = gameObjectAutoMoveClassCopy.Speed;
        StopDistance = gameObjectAutoMoveClassCopy.StopDistance;
        timeToStep = gameObjectAutoMoveClassCopy.timeToStep;
        NoAutoMove = gameObjectAutoMoveClassCopy.NoAutoMove;

        jump = gameObjectAutoMoveClassCopy.jump;
        jumpForce = gameObjectAutoMoveClassCopy.jumpForce;
        timeErrorJump = gameObjectAutoMoveClassCopy.timeErrorJump;

        rb = gameObjectAutoMoveClassCopy.rb;
        
        typeAutoMove = gameObjectAutoMoveClassCopy.typeAutoMove;


        if (timeErrorJump > 0f)
        {
            timeToJump += UnityEngine.Random.Range(-timeErrorJump * timeToJump, timeErrorJump * timeToJump);
        }

    }

}
