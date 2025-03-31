using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    //--- Variables
    public GameObject[] bodyObject;  // LOD별 오브젝트 3개에 대한 GameObject 배열 변수
    public Color32[] colors;   // 색상정보를 담을 Color32 배열 변수
    Material[] carMats;  // 각각이 오브젝트별 Material 배열 변수

    public float rotSpeed = 0.1f;
    

    // Start is called before the first frame update
    void Start()
    {
        // carMats 배열을 자동차 바디 오브젝트의 수만큼 초기화
        carMats = new Material[bodyObject.Length];

        // 자동차 바디 오브젝트의 매터리얼 각각을 carMats 배열에 지정
        for (int i = 0; i < bodyObject.Length; i++)
        {
            carMats[i] = bodyObject[i].GetComponent<MeshRenderer>().material;
        }

        // 색상 배열 0번에는 매터리얼의 초기 색상을 저장
        colors[0] = carMats[0].color;
    }

    // Update is called once per frame
    void Update()
    {
        // 만약, 터치된 부위가 1개 이상이라면 
        if (Input.touchCount > 0) 
        { 
            Touch touch = Input.GetTouch(0);

            // 만약, 터치 상태가 움직이고 있는 중이라면
            if (touch.phase == TouchPhase.Moved) 
            { 
                // 만약, 카메라 위치에서 정면방향으로 레이를 발사하여 부딪힌 대상이
                // 8번 레이어라면 터치 이동량을 구한다
                Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
                RaycastHit hitInfo;
                if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity, 1 << 8)) 
                { 
                    Vector3 deltaPos = touch.deltaPosition;

                    // 직전 프레임에서 현재 프레임까지의 x축 터치 이동량에 비례하여
                    // 로컬 y축 방향으로 회전시킨다
                    transform.Rotate(transform.up, deltaPos.x * -1.0f * rotSpeed);
                }
            }
        }
    }

    //--- User-defined Method
    public void ChangeColor(int num)
    {
        // 각 LOD 매터리얼의 색상ㅇ르 버튼에 저징된 색상으로 변경
        for (int i = 0;i < carMats.Length;i++)
        {
            carMats [i].color = colors [num];
        }
    }
}
