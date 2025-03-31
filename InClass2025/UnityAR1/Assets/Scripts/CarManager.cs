using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.EventSystems;

public class CarManager : MonoBehaviour
{
    // -- Variables
    public GameObject indicator;
    private ARRaycastManager arManager;
    private float halfScreen = 0.5f;

    public GameObject myCar;
    private GameObject placedObject; // �𵨸� ��ü�� �ߺ��ǰ� ��Ÿ���� �ʰ� �Ϸ���
    public float relocationDistance = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        // �ε������͸� ��Ȱ��ȭ
        indicator.SetActive(false);

        // AR Raycast Manager�� �����´�
        arManager = GetComponent<ARRaycastManager>();
    }

    // Update is called once per frame
    void Update()
    {
        // �ٴ� ���� �� ǥ�� ��¿� �Լ�
        DetectGround();

        // �ε������Ͱ� Ȱ��ȭ ���� ��, ȭ���� ��ġ�ϸ� �ڵ��� �𵨸� �����ǰ� �ϰ� �ʹ�!

        
        // ����, ���� Ŭ�� �Ǵ� ��ġ�� ������Ʈ�� UI ������Ʈ��� Update() �Լ��� ����
        if(EventSystem.current.currentSelectedGameObject)
        {
            return;
        }
        

        // ����, �ε������Ͱ� Ȱ��ȭ ���̸鼭 ȭ�� ��ġ�� �ִ� ���¶��
        if (indicator.activeInHierarchy && Input.touchCount>0)
        {
            // ù��° ��ġ ���¸� �����´�
            Touch touch = Input.GetTouch(0);

            // ����, ��ġ�� ���۵� ���¶�� �ڵ����� �ε������Ϳ� ������ ���� ����
            if (touch.phase == TouchPhase.Began) 
            { 
                // ����, ������ ������Ʈ�� ���ٸ� �������� ���� �����ϰ� placedObject ������ �Ҵ�
                if(placedObject == null)
                {
                    placedObject = Instantiate(myCar, indicator.transform.position, indicator.transform.rotation);
                }
                else // ������ ������Ʈ�� �ִٸ�, �� ������Ʈ�� ��ġ�� ȸ������ ����
                {
                    // ����, ������ ������Ʈ�� �ε������� ������ �Ÿ��� �ּ� �̵� ���� �̻��̶��
                    if (Vector3.Distance(placedObject.transform.position, indicator.transform.position) > relocationDistance) 
                    {
                        placedObject.transform.SetPositionAndRotation(indicator.transform.position, indicator.transform.rotation);
                    }
                }
            }
        }
    }

    // -- User-defined Methods
    void DetectGround()
    {
        // ��ũ�� �߾� ������ ã�´�
        Vector2 screenSize = new Vector2(Screen.width * halfScreen, Screen.height * halfScreen);

        // ���̿� �ε��� ������ ������ ������ ����Ʈ ������ �����
        List<ARRaycastHit> hitInfos = new List<ARRaycastHit>();

        // ����, ��ũ�� �߾� �������� ���̸� �߻��Ͽ��� �� Plane Ÿ�� ��������� �ִٸ�
        if(arManager.Raycast(screenSize, hitInfos, UnityEngine.XR.ARSubsystems.TrackableType.Planes))
        {
            // ǥ�� ������Ʈ�� Ȱ��ȭ
            indicator.SetActive(true);

            // ǥ�� ������Ʈ�� ��ġ�� ȸ������ ���̰� ���� ������ ��ġ��Ų��
            indicator.transform.position = hitInfos[0].pose.position;
            indicator.transform.rotation = hitInfos[0].pose.rotation;

            // ��ġ�� ���� �������� 0.1���� �ø���
            indicator.transform.position += indicator.transform.up * 0.1f;
        }
        else // �׷��� �ʴٸ� ǥ�� ������Ʈ�� ��Ȱ��ȭ
        {
            indicator.SetActive(false);
        }

    }
}
