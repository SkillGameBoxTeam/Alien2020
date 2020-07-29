using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ActivitySet", menuName = "Activity Set", order = 51)]
public class GameActivitySet : ScriptableObject
{
    [SerializeField] private GameObject baseGameObjs;
    [SerializeField] private List<GameObject> activities;
    public int countOfActivityToCreate = 1;

    [SerializeField] private List<GameObject> basePoints;
    [SerializeField] private List<GameObject> activityPoints;


    //[SerializeField] private int addedPoints=1;
    //[SerializeField] private int countToWin=1;

    /// <summary>
    /// Возвращает объект базы
    /// </summary>
    /// <returns></returns>
    public GameObject GetBaseGameObj()
    {
        return baseGameObjs;
    }
    /// <summary>
    /// возвращает список точек для создания базы
    /// </summary>
    /// <returns></returns>
    public List<GameObject> GetBasePoints()
    {
        return basePoints;
    }
    /// <summary>
    /// возвращает активности, которые переносятся на базу
    /// </summary>
    /// <returns></returns>
    public List<GameObject> GetActivities()
    {
        return activities;
    }
    /// <summary>
    /// возвращает список точек для спауна активностей
    /// </summary>
    /// <returns></returns>
    public List<GameObject> GetActivityPoints()
    {
        return activityPoints;
    }

    ///// <summary>
    ///// добавляемы очки в случае закрытия базы.
    ///// </summary>
    ///// <returns></returns>
    //public int GetAddedPoints()
    //{
    //    return addedPoints;
    //}

    ///// <summary>
    ///// количеситво активностей, чтобы закрыть базу
    ///// </summary>
    ///// <returns></returns>
    //public int GetСountToWin()
    //{
    //    return countToWin;
    //}
}

