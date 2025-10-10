using System;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public float turnSpeed = 700f; // 旋转速度

    public float walkSpeed = 1.5f;
    public float runSpeed = 3.0f;
    public float gravity = -9.8f;

    public float dodgeTime = 1f;
    public float invincibleStartTime = 0f;
    public float invincibleTime = 0.5f;
    public float dodgeSpeed = 2f;
    public float shieldTime = 1f;
    public float shieldMoveDis = 3f;
    public float shieldSyncUse = 10f;
    public float beAttackedSyncReduce = 5f;

    public float magicTime = 2f;
    public GameObject magicPrefab;
    
    public float AttackAddSync = 5f;
    private void Awake()
    {
        
    }

    private void Update()
    {
        
    }
}
