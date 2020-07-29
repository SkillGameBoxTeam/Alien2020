using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketFollow : MonoBehaviour
{
    [SerializeField] private float distanceToPlayer = 3f;
    [SerializeField] private float lerpKoef = 1.5f;
    [SerializeField] private int secondsToBoom = 5;
    [SerializeField] private typesOfFollow typeOfFollow = typesOfFollow.simple;
    private enum typesOfFollow
    {
        simple,
        lerp
    };
    // Start is called before the first frame update
    void Start()
    {
        if (typeOfFollow == typesOfFollow.lerp)
        {
            StartCoroutine(RocketTimer());
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (typeOfFollow == typesOfFollow.lerp)
        {
            //transform.LookAt(PlayerControl.Instance.transform);
            Vector3 currPos = PlayerControl.Instance.transform.position;
            currPos += PlayerControl.Instance.transform.up * distanceToPlayer;
            if (distanceToPlayer > 0)
            {
                transform.position = Vector3.Lerp(transform.position, currPos, Time.deltaTime * lerpKoef);
            }
            else
            {
                transform.position = PlayerControl.Instance.transform.position;
            }
            transform.position.Set(PlayerControl.Instance.transform.position.x, PlayerControl.Instance.transform.position.y, PlayerControl.Instance.transform.position.z - 1f);
            transform.rotation = Quaternion.Lerp(transform.rotation, PlayerControl.Instance.transform.rotation, Time.deltaTime);
        }
        else if (typeOfFollow == typesOfFollow.simple)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, PlayerControl.Instance.transform.rotation, Time.deltaTime);
            transform.position = PlayerControl.Instance.transform.position;
        }
            



    }

    IEnumerator RocketTimer()
    {
        float deltaDistance = distanceToPlayer / secondsToBoom;

        for (int i = 0; i < secondsToBoom; i++)
        {
            yield return new WaitForSeconds(1f);
            lerpKoef += 0.5f;
            distanceToPlayer -= deltaDistance;
        }


        if (distanceToPlayer <0)
        {
            distanceToPlayer = 0f;
        }
    }

}
