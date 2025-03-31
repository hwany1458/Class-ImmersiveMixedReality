using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    //--- Variables
    public GameObject[] bodyObject;  // LOD�� ������Ʈ 3���� ���� GameObject �迭 ����
    public Color32[] colors;   // ���������� ���� Color32 �迭 ����
    Material[] carMats;  // ������ ������Ʈ�� Material �迭 ����

    public float rotSpeed = 0.1f;
    

    // Start is called before the first frame update
    void Start()
    {
        // carMats �迭�� �ڵ��� �ٵ� ������Ʈ�� ����ŭ �ʱ�ȭ
        carMats = new Material[bodyObject.Length];

        // �ڵ��� �ٵ� ������Ʈ�� ���͸��� ������ carMats �迭�� ����
        for (int i = 0; i < bodyObject.Length; i++)
        {
            carMats[i] = bodyObject[i].GetComponent<MeshRenderer>().material;
        }

        // ���� �迭 0������ ���͸����� �ʱ� ������ ����
        colors[0] = carMats[0].color;
    }

    // Update is called once per frame
    void Update()
    {
        // ����, ��ġ�� ������ 1�� �̻��̶�� 
        if (Input.touchCount > 0) 
        { 
            Touch touch = Input.GetTouch(0);

            // ����, ��ġ ���°� �����̰� �ִ� ���̶��
            if (touch.phase == TouchPhase.Moved) 
            { 
                // ����, ī�޶� ��ġ���� ����������� ���̸� �߻��Ͽ� �ε��� �����
                // 8�� ���̾��� ��ġ �̵����� ���Ѵ�
                Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
                RaycastHit hitInfo;
                if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity, 1 << 8)) 
                { 
                    Vector3 deltaPos = touch.deltaPosition;

                    // ���� �����ӿ��� ���� �����ӱ����� x�� ��ġ �̵����� ����Ͽ�
                    // ���� y�� �������� ȸ����Ų��
                    transform.Rotate(transform.up, deltaPos.x * -1.0f * rotSpeed);
                }
            }
        }
    }

    //--- User-defined Method
    public void ChangeColor(int num)
    {
        // �� LOD ���͸����� ���󤷸� ��ư�� ��¡�� �������� ����
        for (int i = 0;i < carMats.Length;i++)
        {
            carMats [i].color = colors [num];
        }
    }
}
