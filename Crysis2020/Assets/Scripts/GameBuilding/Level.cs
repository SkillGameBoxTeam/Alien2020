using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "Level", order = 53)]
public class Level : ScriptableObject
{
    [TextArea()] 
    public string description = "Win the level!";
    public List<GameActivitySet> ActivitySets;
    public float timer = 0f;
    public int startHealthPoints = 1;
    public int healthPointsToWin = 10;
    public int healthPointToSubtruct = 0;
    public int startEnemyHealthPoints = 1;
    public int healthEnemyPointsToWin = 10;

    public float secondsForHealthPointToSubtruct = 0f;
    public int maxBasesInGame = 0;
    //public int maxBatInGame = 0;
    public int maxMoneyInGame = 10;
    //public int maxCountOfVirus = 10;

    public List<ObjsToSpawnConstruct> constantGameObjs;

    public List<GameObjectAutoMove> moveableObjs;
    public float maxCountOfMovableObjs;

}
