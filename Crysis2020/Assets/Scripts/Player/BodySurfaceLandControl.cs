using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodySurfaceLandControl : MonoBehaviour
{
    [SerializeField] private float SurfaceRotateCoef = 20f;
    [SerializeField] private float DownSideDistance;
    [SerializeField] private List<Transform> RayPointTransfoms;
    [SerializeField] private Transform CentralRayPointTransfom;
    private List<RayHit> rayHits;
    private RayHit RayHitC;

    void Start()
    {
        RayHitC = new RayHit(CentralRayPointTransfom);
        rayHits = new List<RayHit>();

        foreach (Transform item in RayPointTransfoms)
        {
            rayHits.Add(new RayHit(item));
        }

    }

    private void Update()
    {
        RayCastsDownMaker();
    }

    public void PlayerSurfaceAngle()
    {
        float angleNormalZ = 0f;
        if (RayHitC.wasHit)
        {
            angleNormalZ = Vector3.SignedAngle(transform.up, RayHitC.RaycastHit.normal, Vector3.forward);

        }
        else
        {
            angleNormalZ = Vector3.SignedAngle(transform.up, Vector3.up, Vector3.forward);
        }
        transform.Rotate(0f, 0f, angleNormalZ * SurfaceRotateCoef * Time.fixedDeltaTime, Space.World);

    }

    private class RayHit
    {
        public Transform trPoint;
        public bool wasHit;
        public RaycastHit RaycastHit;
        public RayHit(Transform incomeTrPoint)
        {
            trPoint = incomeTrPoint;
            wasHit = false;
        }

    }

    private void RayCastsDownMaker()
    {
        RayHitC.RaycastHit = RaycastHitDown(RayHitC.trPoint, out RayHitC.wasHit);

        foreach (RayHit rayhit in rayHits)
        {
            rayhit.RaycastHit = RaycastHitDown(rayhit.trPoint, out rayhit.wasHit, DownSideDistance);
        }
   
    }

    private RaycastHit RaycastHitDown(Transform rayCastTrans, out bool WasHit, float RayDistace = 1f)
    {
        int layerMask = 1 << 8;
        layerMask = ~layerMask;

        WasHit = Physics.Raycast(new Ray(rayCastTrans.position, -rayCastTrans.up), out RaycastHit rayDownHit, RayDistace, layerMask, QueryTriggerInteraction.Ignore);
        return rayDownHit;
    }

    public bool CheckIsOnGround(bool isOnGround, bool isJump, ref bool doLanding )
    {

        List<RayHit> DoneRayHits = new List<RayHit>();

        foreach (RayHit rayhit in rayHits)
        {
            if (rayhit.wasHit) //&& rayhit.RaycastHit.distance <= DownSideDistance
            {
                DoneRayHits.Add(rayhit);
            }
  
        }

        if (DoneRayHits.Count > 0)
        {
            if (!isOnGround && !isJump)
            {
                doLanding = true;
            }
            isOnGround = true;
        }
        else
        {
            isOnGround = false;
        }
        return isOnGround;
     
    }
}
