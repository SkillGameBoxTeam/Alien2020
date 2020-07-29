using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ObjsToSpawnConstruct", menuName = "ObjsToSpawnConstruct", order = 54)]
public class ObjsToSpawnConstruct : ScriptableObject
{
    public int counts = 1;
    public GameObject objToSpawn;
    public float timeout;

    public bool addToAutoMove = false;
    public GameObjectAutoMove gameObjectAutoMove;
}
