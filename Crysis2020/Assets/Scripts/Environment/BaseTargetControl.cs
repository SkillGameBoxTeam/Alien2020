using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseTargetControl : MonoBehaviour
{

    [SerializeField] private bool usePortables = true;
    [SerializeField] private ParticleSystem boom;
    [SerializeField] private float delayToBoom = 3f;
    [SerializeField] private string portablesTag;
    public int countToWin = 1;
    public int addedPoints = 1;

    [SerializeField] bool turnOffParticle = false;
    [SerializeField] private ParticleSystem particleToTurnOff;

    /// <summary>
    /// Добавлять и удалять из объект в список контроля построителя мира
    /// </summary>
    [SerializeField] private bool BaseHashSetControl = true;

    private void Start()
    {
        if (BaseHashSetControl)
        {
            GameBuilder.Instance.AddBaseToHashSet(gameObject);
            
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (usePortables)
        {
            if (other.CompareTag(portablesTag) && other.gameObject == BodyTrigers.currPortableGameObject)
            {
                SoundControl.Instance.BaseWin();
                countToWin--;
                if (countToWin <= 0)
                {
                    if (turnOffParticle && particleToTurnOff)
                    {
                        particleToTurnOff.Stop();
                    }

                    if (boom)
                    {
                        boom.Play();
                    }

                    GameBuilder.Instance.IncreaseHealth(addedPoints);

                    if (BaseHashSetControl)
                    {
                        GameBuilder.Instance.SetDestroyObj(gameObject, GameBuilder.TypeOfHashSet.bases, delayToBoom);
                    }
                    else
                    {
                        GameBuilder.Instance.SetDestroyObj(gameObject, GameBuilder.TypeOfHashSet.nope, delayToBoom);
                    }
                    if (BodyTrigers.currPortableGameObject == other.gameObject)
                    {
                        BodyTrigers.currPortableGameObject = null;
                    }
                    
                    GameBuilder.Instance.SetDestroyObj(other.gameObject, GameBuilder.TypeOfHashSet.portables, delayToBoom);
                }

            }

        }
    }

 

}
