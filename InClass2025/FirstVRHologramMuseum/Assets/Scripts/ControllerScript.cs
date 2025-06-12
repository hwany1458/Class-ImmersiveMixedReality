using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerScript : MonoBehaviour
{
    //---- Member variables
    public Camera sceneCamera;  //represents the camera that the scene uses
    private Vector3 targetPosition;  //represents the position of the camera
    private Quaternion targetRotation; //represents the rotation of the camera
    private float step;  // helps with animating the GameObject

    //---- Methods
    // Start is called before the first frame update
    void Start()
    {
        //define the initial GameObject’s position
        transform.position = sceneCamera.transform.position + sceneCamera.transform.forward * 3.0f;
    }

    // Update is called once per frame
    void Update()
    {
        //define your step value to animate the GameObject
        step = 5.0f * Time.deltaTime;

        //Receive input from right index trigger (오른쪽 인덱스 트리거)
        //While user holds the right index trigger, center the cube and turn it to face user
        if (OVRInput.Get(OVRInput.RawButton.RIndexTrigger)) centerCube();

        //Receive input from right index thumbstick
        //While thumbstick of right controller is currently pressed to the left rotate cube to the left
        if (OVRInput.Get(OVRInput.RawButton.RThumbstickLeft)) transform.Rotate(0, 5.0f * step, 0);
        //While thumbstick of right controller is currently pressed to the right rotate cube to the right
        if (OVRInput.Get(OVRInput.RawButton.RThumbstickRight)) transform.Rotate(0, -5.0f * step, 0);

        //Receive input from A button and add haptic feedback
        //If user has just released Button A of right controller in this frame
        if (OVRInput.GetUp(OVRInput.Button.One))
        {
            // Play short haptic on right controller
            OVRInput.SetControllerVibration(1, 1, OVRInput.Controller.RTouch);
        }

        //Receive input from left hand trigger
        //While user holds the left hand trigger
        if (OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger) > 0.0f)
        {
            //Assign left controller's position and rotation to GameObject
            transform.position = OVRInput.GetLocalControllerPosition(OVRInput.Controller.LTouch);
            transform.rotation = OVRInput.GetLocalControllerRotation(OVRInput.Controller.LTouch);
        }
    }

    //---- User-defined Methods

    //smoothly places the GameObject in front of the user,
    //at the center of their viewport,
    //and rotates the GameObject according to the user’s headpose (camera)
    void centerCube()
    {
        targetPosition = sceneCamera.transform.position + sceneCamera.transform.forward * 3.0f;
        targetRotation = Quaternion.LookRotation(transform.position - sceneCamera.transform.position);

        transform.position = Vector3.Lerp(transform.position, targetPosition, step);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, step);
    }

    // 다음을 수정해 봅니다
    //(1) 외부 캐릭터를 다운받아, 스크립트를 연결
    //  해당 캐릭터는 시작 시점에서 비활성화
    //  캐릭터에는 애니메이션이 붙어있도록
    //(2) 왼쪽 컨트롤러의 X를 (한번) 클릭하면,
    //  해당 캐릭터가 활성화되고 눈 앞에 나타남
    //  X를 한번더 클릭하면, 해당 캐릭터는 다시 비활성화
    //  활성화될 때는 특정거리를 유지한 시야에서 보이도록
    //(3) 카메라가 이동하면 캐릭터로 특정거리를 유지한 시야에서 계속 따라다니도록 함
}
