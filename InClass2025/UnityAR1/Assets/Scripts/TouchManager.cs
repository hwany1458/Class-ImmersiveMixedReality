using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class TouchManager : MonoBehaviour
{
    //------ Variables
    //������ ��ü
    //private GameObject placeObject;
    public GameObject placeObject;
    private ARRaycastManager raycastMgr;
    private List<ARRaycastHit> hits = new List<ARRaycastHit>();

    //------ Methods
    // Start is called before the first frame update
    void Start()
    {
        // ������ ť�긦 �Ҵ�
        //placeObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
        // ť���� ũ��, Į�� ����
        //placeObject.transform.localScale = Vector3.one * 0.05f;
        //placeObject.GetComponent<Renderer>().material.color = Color.red;
        // AR Raycast Manager ����
        raycastMgr = GetComponent<ARRaycastManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount == 0) return;
        Touch touch = Input.GetTouch(0);

        if (touch.phase == TouchPhase.Began)
        {
            // ������� �ν��� ���� ���̷� ����
            if (raycastMgr.Raycast(touch.position, hits, TrackableType.PlaneWithinPolygon))
            {
                Instantiate(placeObject, hits[0].pose.position, hits[0].pose.rotation);
            }
        }
    }

    //------ User-defined Methods
}
