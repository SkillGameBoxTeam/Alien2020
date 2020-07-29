using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    
    [SerializeField] private float forceShift;
    [SerializeField] private float periodToForce = 1f;
    [SerializeField] private ParticleSystem forceParticle;
    [SerializeField] private int cost = 1;
    [SerializeField] private GameObject sphereCollider;
    private bool isForce;

    private SoundControl soundControl;
    [SerializeField] private float maxDistanceOfShoot = 20f;
    [SerializeField] private LayerMask shootLayerMask;
    [SerializeField] private float radiusOFShoot = 3f;

    [SerializeField] private GameObject shootModel;
    [SerializeField] private float timerForShootShow = 0.3f;

    private GameBuilder gameBuilder;

    [SerializeField] private Transform shootAncor;

    [SerializeField] private GameObject expsionPrefab;

    private GameObject exposion;
    private ParticleSystem pSexposion;
    private void Start()
    {
        soundControl = SoundControl.Instance;
        gameBuilder = GameBuilder.Instance;

        exposion = Instantiate(expsionPrefab);
        pSexposion = exposion.GetComponent<ParticleSystem>();
        exposion.SetActive(false);
    }

    void Update()
    {
        if (InputParams.hitButton)
        {
            if (isForce == false && GameStats.GetСurrency() >0)
            {
                StartCoroutine(DoForce());
            }
            InputParams.hitButton = false;
        }
        if (InputParams.shootButton)
        {
            Shoot();

            InputParams.shootButton = false;
        }

        
    }

    private void Shoot()
    {
        Vector3 placeExpl = new Vector3();

        StartCoroutine(ShowShoot());
        Ray ray = new Ray(shootAncor.position, shootAncor.forward);
        RaycastHit[] raycastHits = Physics.SphereCastAll(ray, radiusOFShoot, maxDistanceOfShoot, shootLayerMask);
        foreach (RaycastHit hit in raycastHits)
        {
            EnemyControl curEnemyControl = hit.collider.GetComponent<EnemyControl>();
            if (curEnemyControl)
            {
                if (placeExpl == default)
                {
                    placeExpl = hit.collider.transform.position;
                }

                curEnemyControl.DestroyObj();

                gameBuilder.IncreaseEnemyHealth();
            }
        }
        if (placeExpl!= Vector3.zero)
        {
            exposion.SetActive(true);
            exposion.transform.position = placeExpl;
            //exposion.transform.position = transform.position;
            exposion.transform.rotation = transform.rotation;
            pSexposion.Play();
            StartCoroutine(StopExplotion());
        }    
        
        //Debug.DrawRay(shootAncor.position, shootAncor.forward * maxDistanceOfShoot, Color.red, 1000f);
    }

    IEnumerator ShowShoot()
    {
        soundControl.Shoot();
        BodyTrigers.shoot = true;
        shootModel.SetActive(true);
        yield return new WaitForSeconds(timerForShootShow);
        shootModel.SetActive(false);
        BodyTrigers.shoot = false;
    }

    IEnumerator DoForce()
    {
        if (GameStats.playAttackSound)
        {
            soundControl.Attack();
        }
        
        sphereCollider.SetActive(true);
        forceParticle.Play();
        GameBuilder.Instance.DecreaseCurr(cost);
        isForce = true;
        BodyTrigers.isAttack = true;
        bool prevAutoRun = PlayerControl.Instance.autorun;
        float prevMFK = PlayerControl.Instance.MoveForceKoef;
        PlayerControl.Instance.autorun = true;
        PlayerControl.Instance.MoveForceKoef = forceShift;
        yield return new WaitForSeconds(periodToForce);

        PlayerControl.Instance.MoveForceKoef = prevMFK;
        yield return new WaitForSeconds(periodToForce);
        PlayerControl.Instance.autorun = prevAutoRun;
        isForce = false;
        BodyTrigers.isAttack = false;
        sphereCollider.SetActive(false);
        yield return new WaitForEndOfFrame();
    }

    IEnumerator StopExplotion()
    {
        yield return new WaitForSeconds(periodToForce);
        pSexposion.Stop();
    }

}
