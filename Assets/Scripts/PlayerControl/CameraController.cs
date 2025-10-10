using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public PlayerController playerController;
    public CinemachineFreeLook  freeLook;
    RPGInputActions rpgInputActions;
    public float RotateSpeed = 5f; // 相机旋转速度
    private Transform targetFollowTransform;
    void Start()
    {
        rpgInputActions = playerController.rpgInputActions;
        targetFollowTransform = playerController.transform;
        
        diffVec3 =  transform.position - playerController.transform.position;
    }

    private Vector3 diffVec3;
    // 视角控制 
    private void GetCameraControlInput()
    {
        // transform.position = targetFollowTransform.position + diffVec3;
        Vector2 cameraOffset = rpgInputActions.PC.CameraControl.ReadValue<Vector2>();
        freeLook.m_XAxis.m_InputAxisValue  = cameraOffset.x;
        freeLook.m_YAxis.m_InputAxisValue  = cameraOffset.y;
        // //判断是否有鼠标是否有产生偏移
        // if (cameraOffset == Vector2.zero)
        // {
        //     return;
        // }
        // //将获取到的鼠标偏移值打印出来
        // // Debug.Log(cameraOffset );
        // //控制相机以targetFollowTransform对象为中心围绕世界坐标垂直轴进行水平旋转，旋转角度为cameraOffset水平方向返回的偏移值
        // transform.RotateAround(targetFollowTransform.position, Vector3.up, cameraOffset.x * RotateSpeed);
        // //控制相机以targetFollowTransform对象围绕相机自身水平轴进行垂直旋转，旋转角度为cameraOffset垂直方向返回的偏移值
        // transform.RotateAround(targetFollowTransform.position, transform.right, -cameraOffset.y * RotateSpeed);
    }
    // Update is called once per frame
    void Update()
    {
        GetCameraControlInput();
    }
}
