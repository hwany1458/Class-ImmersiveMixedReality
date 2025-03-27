using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class TouchManager : MonoBehaviour
{
    //------ Variables
    //생성할 객체
    //private GameObject placeObject;
    public GameObject placeObject;
    private ARRaycastManager raycastMgr;
    private List<ARRaycastHit> hits = new List<ARRaycastHit>();

    //------ Methods
    // Start is called before the first frame update
    void Start()
    {
        // 생성할 큐브를 할당
        //placeObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
        // 큐브의 크기, 칼라를 설정
        //placeObject.transform.localScale = Vector3.one * 0.05f;
        //placeObject.GetComponent<Renderer>().material.color = Color.red;
        // AR Raycast Manager 추출
        raycastMgr = GetComponent<ARRaycastManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount == 0) return;
        Touch touch = Input.GetTouch(0);

        if (touch.phase == TouchPhase.Began)
        {
            // 평면으로 인식한 곳만 레이로 검출
            if (raycastMgr.Raycast(touch.position, hits, TrackableType.PlaneWithinPolygon))
            {
                Instantiate(placeObject, hits[0].pose.position, hits[0].pose.rotation);
            }
        }
    }

    //------ User-defined Methods
}
