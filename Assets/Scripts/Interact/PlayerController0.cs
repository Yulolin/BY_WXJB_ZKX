using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController0 : MonoBehaviour
{
    public float moveSpeed = 5f;  // 移动速度
    public float turnSpeed = 700f;  // 旋转速度
    public float jumpForce = 5f;  // 跳跃力度
    public float RunSpeed = 15f; // 奔跑速度
    public float RotateSpeed = 5f; // 相机旋转速度
    
    public RPGInputActions rpgInputActions;
    
    //获取围绕旋转对象的Transform 
    public Transform targetFollowTransform;
    private Transform cameraTransform;
    
    private Rigidbody rb;

    // Start is called before the first frame update
    void Awake()
    {
        rpgInputActions = new RPGInputActions();
        rb = GetComponent<Rigidbody>();
        cameraTransform = Camera.main.transform;    
    }

    private void OnEnable()
    {
        rpgInputActions.Enable();
    }

    private void OnDisable()
    {
        rpgInputActions.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        GetMoveInput();
        
        GetJumpInput();

        GetCameraControlInput();

        GetDodgeInput();
        
        GetLightAttackInput();
        
        GetHeavyAttackInput();
    }
    // 移动
    private void GetMoveInput()
    {
        Vector2 moveVector2 = rpgInputActions.PC.MOVE.ReadValue<Vector2>();
        //判断是否有按下对应的Move按键
        if (moveVector2 == Vector2.zero)
        {
            return;
        }
        //将获取到的返回值打印出来
        Debug.Log(moveVector2);
        // 计算移动方向
        Vector3 cameraForward = cameraTransform.forward;
        Vector3 cameraRight = cameraTransform.right;
        // 将摄像机的前进方向（忽略垂直）和右方向组合来计算移动方向
        cameraForward.y = 0;  // 防止摄像机的上下旋转影响角色移动
        cameraForward.Normalize();  // 确保方向是单位向量

        cameraRight.y = 0;  // 同样忽略上下旋转的影响
        cameraRight.Normalize();  // 确保方向是单位向量
        // 计算相对于摄像机视角的移动方向
        Vector3 movement = cameraForward * moveVector2.y + cameraRight * moveVector2.x;
        
        // 旋转角色朝向移动方向
        Quaternion targetRotation = Quaternion.LookRotation(movement);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
        
        // 使用 Rigidbody 来移动角色
        rb.velocity = movement * moveSpeed * Time.deltaTime;  // 使用速度来控制角色移动
    }
    // 跳跃
    private void GetJumpInput()
    {
        bool isJump = rpgInputActions.PC.Jump.IsPressed();
        if (!isJump)
        {
            return;
        }
        Debug.Log("跳跃");
        rb.AddForce(Vector3.up * jumpForce);

    }
    // 视角控制 
    private void GetCameraControlInput()
    {
        Vector2 cameraOffset = rpgInputActions.PC.CameraControl.ReadValue<Vector2>();
        //判断是否有鼠标是否有产生偏移
        if (cameraOffset == Vector2.zero)
        {
            return;
        }
        //将获取到的鼠标偏移值打印出来
        Debug.Log(cameraOffset );
        //控制相机以targetFollowTransform对象为中心围绕世界坐标垂直轴进行水平旋转，旋转角度为cameraOffset水平方向返回的偏移值
        cameraTransform.RotateAround(targetFollowTransform.position, Vector3.up, cameraOffset.x * RotateSpeed);
        //控制相机以targetFollowTransform对象围绕相机自身水平轴进行垂直旋转，旋转角度为cameraOffset垂直方向返回的偏移值
        cameraTransform.RotateAround(targetFollowTransform.position, cameraTransform.right, -cameraOffset.y * RotateSpeed);
    }
    // 闪避
    private void GetDodgeInput()
    {
        bool isDodge = rpgInputActions.PC.Dodge.IsPressed();
        if (isDodge)
        {
            Debug.Log("闪避");
        }
    }
    // 轻击
    private void GetLightAttackInput()
    {
        bool isLightAttack = rpgInputActions.PC.LightAttack.IsPressed();
        if (isLightAttack)
        {
            Debug.Log("轻击");
        }
    }
    // 重击
    private void GetHeavyAttackInput()
    {
        bool isHeavyAttack = rpgInputActions.PC.HeavyAttack.IsPressed();
        if (isHeavyAttack)
        {
            Debug.Log("重击");
        }
    }
}
