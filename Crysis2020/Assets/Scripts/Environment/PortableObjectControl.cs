using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortableObjectControl : MonoBehaviour
{
    private bool doFollow=false;
    private float upDistance = 0.75f;
    [SerializeField] private string tagToTartget;
    [SerializeField] private float delayToDestroy = 3f;
    [SerializeField] private ParticleSystem boom;
    private Rigidbody rb;
    private bool canPort = true;
    [SerializeField] private bool PortableHashSetControl = true;
    private float forceBackToDrop = -1000f;

    private Vector3 startScale;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        startScale = transform.localScale;


    }

    private void Update()
    {
        if (BodyTrigers.shoot && doFollow)
        {
            DropDownObj();
        }
        if (canPort && doFollow )
        {
            transform.rotation = PlayerControl.Instance.transform.rotation;
            Vector3 addPos = PlayerControl.Instance.transform.up * upDistance;
            transform.position = PlayerControl.Instance.transform.position + addPos;
            
        } 
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !doFollow && canPort)
        {
            SoundControl.Instance.TakeObj();
            doFollow = true;
            if (BodyTrigers.currPortableGameObject)
            {
                PortableObjectControl portObjCont = BodyTrigers.currPortableGameObject.GetComponent<PortableObjectControl>();
                if (portObjCont)
                {
                    portObjCont.DropDownObj();
                }
            }
            transform.localScale = startScale * 0.2f;
            BodyTrigers.currPortableGameObject = gameObject;
        }

        if (other.CompareTag(tagToTartget))
        {
            
            //GameBuilder.Instance.SetDestroyObj(gameObject, GameBuilder.TypeOfHashSet.portables, delayToDestroy);
            //boom.Play();
            
            if (doFollow)
            {
                canPort = false;
            }
            doFollow = false;
            //gameObject.SetActive(false);
            transform.position = other.transform.position + other.transform.up;
            rb.drag = 10f;
            rb.angularDrag = 10f;
        }
    }

   

    public void DropDownObj(bool canP = true)
    {
        doFollow = false;
        canPort = canP;
        transform.localScale = startScale;


        if (rb)
        {
            rb.velocity = Vector3.zero;
            rb.AddForce(rb.transform.forward * forceBackToDrop);
            rb.AddForce(-rb.transform.up * forceBackToDrop);
        }

        if (BodyTrigers.currPortableGameObject == gameObject)
        {
            BodyTrigers.currPortableGameObject = null;
        }
    }

    IEnumerator DropFrame(bool canP = true)
    {
        yield return new WaitForEndOfFrame();
        DropDownObj(canP);
    }

    IEnumerator CanPortThrowTime()
    {
        yield return new WaitForSeconds(2);

        canPort = true;
    }


}
