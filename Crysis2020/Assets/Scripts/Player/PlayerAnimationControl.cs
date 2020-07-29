using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationControl : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private string AnimRunParam = "run";
    [SerializeField] private bool UseRun;
    [SerializeField] private string AnimJumpParam = "jump";
    [SerializeField] private bool UseJump;
    [SerializeField] private string AnimHitParam = "hit";
    [SerializeField] private bool UseHit;
    [SerializeField] private string AnimFallParam = "fall";
    [SerializeField] private bool UseFall;
    [SerializeField] private string AnimLandParam = "land";
    [SerializeField] private bool UseLand;
    [SerializeField] private string RunCoef = "RunCoef";
    [SerializeField] private bool UseRunCoef;


    [SerializeField] private ParticleSystem enj1;
    [SerializeField] private ParticleSystem enj2;


    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Run(bool param)
    {
        if (UseRun)
        {
            animator.SetBool(AnimRunParam, param);
        }
        
    }

    public void Jump()
    {
        if (UseJump)
        {
            animator.SetTrigger(AnimJumpParam);
        }

        enj1.Play();
        enj2.Play();

    }

    public void Hit()
    {
        if (UseHit)
        {
            animator.SetTrigger(AnimHitParam);
        }
        
    }

    public void Fall(bool param)
    {
        if (UseFall)
        {
            animator.SetBool(AnimFallParam, param);
        }
        
    }

    public void Land()
    {
        if (UseLand)
        {
            animator.SetTrigger(AnimLandParam);
        }
        
    }

    public void SetRunSpeed(float param)
    {
        if (UseRunCoef)
        {
            animator.SetFloat(RunCoef, param);
        }
        
    }
}
