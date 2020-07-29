using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private float timeOut = 10f;
    [SerializeField] private List<ObjsToSpawnConstruct> objctsToSpwn;

    private ObjsToSpawnConstruct currentSpawnConstruct;
    private bool isCreating = true;
    public bool IsCreating { get => isCreating; set => isCreating = value; }
    [SerializeField] private bool virusControl = true;


    void Start()
    {
        //if (objctsToSpwn.Count>0)
        //{
        //    currentSpawnConstruct = objctsToSpwn[0];
        //    StartCoroutine(LevelControlCoroutine());
        //    StartCoroutine(ObjCreator());
        //}
                
    }


    private void CreateGo(GameObject go, Vector3 pos)
    {

        //pos.x *= transform.localScale.x;
        //pos.y *= transform.localScale.y;
        //pos.z *= transform.localScale.z;
        //Quaternion rot = Quaternion.FromToRotation(go.transform.up, pos.normalized);

        //Instantiate(go, pos, rot, GameBuilder.Instance.transform);
    }

    IEnumerator LevelControlCoroutine()
    {
        yield return new WaitForEndOfFrame();

        for (int i = 0; i < objctsToSpwn.Count; i++)
        {
            currentSpawnConstruct = objctsToSpwn[i];
            yield return new WaitForSeconds(timeOut);
        }
        yield break;
    }

    //IEnumerator ObjCreator()
    //{
    //    //while (isCreating)
    //    //{
    //    //    for (int i = 0; i < currentSpawnConstruct.counts; i++)
    //    //    {
    //    //        if (virusControl )
    //    //        {
    //    //            //if (GameBuilder.Instance.AcceptSpawnVirus())
    //    //            {
    //    //                CreateGo(currentSpawnConstruct.objToSpawn, transform.position);
    //    //            }
    //    //        }
    //    //        else
    //    //        {
    //    //            CreateGo(currentSpawnConstruct.objToSpawn, transform.position);
    //    //        }
                
    //    //        yield return new WaitForEndOfFrame();
    //    //    }
    //    //    yield return new WaitForSeconds(currentSpawnConstruct.timeout);
    //    //}    

    //}


}
