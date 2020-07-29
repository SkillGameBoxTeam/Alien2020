using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLevelControl : MonoBehaviour
{
    [SerializeField] List<Level> Levels;
    private GameActivityCreator gameActivityCreator;
    private Level currLevel;

    void Start()
    {
        gameActivityCreator = GetComponent<GameActivityCreator>();
        currLevel = Levels[0];
    }

    private void Update()
    {
        if (GameStats.GetHealth()> currLevel.healthPointsToWin)
        {
            //nextLvl
        }

        if (GameStats.GetBasesInGame() < currLevel.maxBasesInGame)
        {
            //createBase
        }


    }

    IEnumerator LevelControlCoroutine()
    {
        //foreach (Level level in Levels) // обработка уровней
        //{
        //    foreach (LevelStep levelStep in level.LevelSteps)//обработка уровня
        //    {
        //        if (levelStep.scriptType == LevelStep.ScriptType.timer)
        //        {
        //            yield return new WaitForSeconds(levelStep.timer);
        //        }
        //        else if (levelStep.scriptType == LevelStep.ScriptType.activityAndBaseSet)
        //        {
        //            gameActivityCreator.CreateBasesAndActivities(levelStep.gameActivitySet, levelStep.basesCount, levelStep.actCount);
        //        }
        //        else if (levelStep.scriptType == LevelStep.ScriptType.baseSet)
        //        {
        //            gameActivityCreator.CreateBases(levelStep.gameActivitySet, levelStep.basesCount);
        //        }
        //        else if (levelStep.scriptType == LevelStep.ScriptType.activitySet)
        //        {
        //            gameActivityCreator.CreateActivities(levelStep.gameActivitySet, levelStep.actCount);
        //        }
        //    }
            //Межуровнвые манипуляции
            //yield return new WaitForEndOfFrame();
        //}
       
        yield break;
    }
}
