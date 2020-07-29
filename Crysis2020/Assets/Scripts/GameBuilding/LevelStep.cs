using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelStep", menuName = "LevelStep", order = 52)]
public class LevelStep : ScriptableObject
{
    public enum ScriptType
    {
        timer,
        activitySet,
        activityAndBaseSet,
        baseSet
    }
    public ScriptType scriptType = ScriptType.activitySet;

    //activitySet,
    //activityAndBaseSet,
    //baseSet
    public GameActivitySet gameActivitySet;
    /// <summary>
    /// количество создаваемых баз
    /// </summary>
    public int basesCount;
    /// <summary>
    /// количество создаваемых активностей
    /// </summary>
    public int actCount;

    //timer
    public float timer = 0f;

}
