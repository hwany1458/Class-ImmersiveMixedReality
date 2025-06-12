using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CylinderControllerScript : MonoBehaviour
{
    //---- Variables
    private Renderer cylinderGameObject;
    [SerializeField] private Transform collisionParticle;
    private AudioSource collisionSound;

    // Start is called before the first frame update
    void Start()
    {
        cylinderGameObject = GetComponent<Renderer>();
        collisionSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger))
        {
            cylinderGameObject.material.color = Color.blue;
        }
        if (OVRInput.GetDown(OVRInput.Button.SecondaryHandTrigger))
        {
            cylinderGameObject.material.color = Color.red;
        }
        if (OVRInput.GetUp(OVRInput.Button.PrimaryHandTrigger))
        {
            cylinderGameObject.material.color = Color.white;
        }
        if (OVRInput.GetUp(OVRInput.Button.SecondaryHandTrigger))
        {
            cylinderGameObject.material.color = Color.white;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("[YongHwan]OnTriggerEnter() is called.................");
        if(other.tag == "TargetItem1")
        {
            TriggerEventOccurred(transform.position);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("[YongHwan]OnCollisionEnter() is called.................");
    }

    //---- User-defined Methods
    void TriggerEventOccurred(Vector3 pos)
    {
        Instantiate(collisionParticle, pos, Quaternion.identity);
        collisionSound.Play();
    }
}
