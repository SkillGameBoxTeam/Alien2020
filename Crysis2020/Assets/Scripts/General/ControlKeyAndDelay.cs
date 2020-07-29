using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlKeyAndDelay : MonoBehaviour
{
    public class KeyAndDelay
    {

        /// <summary>
        /// имя клавиши, например "Jump"
        /// </summary>
        public string Key;
        /// <summary>
        /// время задержки перед повторным нажатием
        /// </summary>
        public float Delay;
        /// <summary>
        /// текущее время задержки
        /// </summary>

        public bool IsAvailable;

        public KeyAndDelay(string KeyName, float inputDelay)
        {
            Key = KeyName;
            Delay = inputDelay;
            IsAvailable = true;
        }

    }
   
    /// <summary>
    /// старт таймера задержки нажатия клавиши
    /// </summary>
    /// <param name="keyAndDelay"></param>
    public void StartKeyDelay(KeyAndDelay keyAndDelay)
    {
        StartCoroutine(DelayCount(keyAndDelay));
    }

    /// <summary>
    /// корутина задержки таймера нажатия клавиши
    /// </summary>
    /// <param name="keyAndDelay">экзэмпляр класса клавиши задержки нажатия</param>
    /// <returns></returns>
    IEnumerator DelayCount(KeyAndDelay keyAndDelay)
    {
        keyAndDelay.IsAvailable = false;
        yield return new WaitForSeconds(keyAndDelay.Delay);
        keyAndDelay.IsAvailable = true;
    }

}
