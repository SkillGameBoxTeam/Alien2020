using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameActivityCreator : MonoBehaviour
{
    /// <summary>
    /// Создает базы и активности
    /// </summary>
    /// <param name="BasesCount">количество баз</param>
    /// <param name="ActCount">количество активностей</param>
    public void CreateBasesAndActivities(GameActivitySet gameActivitySet, int basesCount = 1, int actCount = 1)
    {
        CreateBase(gameActivitySet, basesCount);
        CreateActivities(gameActivitySet, actCount);
    }

    /// <summary>
    /// создает базу
    /// </summary>
    /// <param name="basesCount"></param>
    public GameObject CreateBase(GameActivitySet gameActivitySet, int basesCount = 1)
    {
        return CreateGameObjsInPoints(gameActivitySet.GetBaseGameObj(), gameActivitySet.GetBasePoints(), basesCount);
    }

    /// <summary>
    /// Создает активности
    /// </summary>
    /// <param name="actCount"></param>
    public void CreateActivities(GameActivitySet gameActivitySet, int actCount = 1)
    {
        CreateGameObjsInPoints(gameActivitySet.GetActivities(), gameActivitySet.GetActivityPoints(), actCount);
    }


    /// <summary>
    /// Создает объекты из списка в случайном месте из списка
    /// </summary>
    /// <param name="gameObjects">список объектов на создание</param>
    /// <param name="basesObjects">список точек для спавна</param>
    /// <param name="countActivity"> количество создаваемых объектов</param>
    private void CreateGameObjsInPoints(List<GameObject> gameObjects, List<GameObject> basesObjects, int countActivity = 1)
    {

        if (countActivity == 1)
        {
            GameObject go = GetRndListGO(gameObjects);
            Vector3 pos = GetRndListGO(basesObjects).transform.position;
            CreateGo(go, pos);
        }
        else if (countActivity > 1)
        {
            List<GameObject> currListOfPoints = new List<GameObject>(basesObjects);

            int maxCount;

            if (countActivity>= basesObjects.Count)
            {
                maxCount = basesObjects.Count;
            }
            else
            {
                maxCount = countActivity;
            }

            for (int i = 0; i < maxCount; i++)
            {
                GameObject go = GetRndListGO(gameObjects);
                GameObject posGO = GetRndListGO(currListOfPoints);
                CreateGo(go, posGO.transform.position);
                currListOfPoints.Remove(posGO);
            }
        }
    }

    private GameObject CreateGameObjsInPoints(GameObject gameObj, List<GameObject> basesObjects, int countActivity = 1)
    {
        Vector3 pos = GetRndListGO(basesObjects).transform.position;
        return CreateGo(gameObj, pos);
    }

    /// <summary>
    /// создает объект в указанной
    /// </summary>
    /// <param name="go"></param>
    /// <param name="pos"></param>
    private GameObject CreateGo(GameObject go, Vector3 pos)
    {
        pos.x *= transform.localScale.x;
        pos.y *= transform.localScale.y;
        pos.z *= transform.localScale.z;
        Quaternion rot = Quaternion.FromToRotation(go.transform.up, pos.normalized);

        return Instantiate(go, pos, rot, transform);
    }

    /// <summary>
    /// Возращает случайный объект из списка
    /// </summary>
    /// <param name="goList"></param>
    /// <returns></returns>
    private GameObject GetRndListGO(List<GameObject> goList)
    {
        if (goList.Count == 1)
        {
            return goList[0];
        }
        else if (goList.Count >1)
        {
            return goList[Random.Range(0, goList.Count)];
        }
        else
        {
            return null;     
        }
    }
}
