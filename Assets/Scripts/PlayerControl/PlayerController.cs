using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private CharacterController characterController;
    public RPGInputActions rpgInputActions;
    public InputManager inputManager;
    public Animator animator;
    private Transform cameraTransform;

    public Vector3 velocity;

    public PlayerState currentState;
    private IdleState idleState;
    private WalkState walkState;
    private RunState runState;
    private DodgeState dodgeState;
    private LightAttackState lightAttackState;
    private HeavyAttackState  heavyAttackState;
    private HitState hitState;
    private FrameworkState frameworkState;
    private MagicState magicState;
    private ExecuteState excuteState;

    private void Awake()
    {
        rpgInputActions = new RPGInputActions();
        cameraTransform = Camera.main.transform;
        characterController = GetComponent<CharacterController>();
        inputManager = GetComponent<InputManager>();

        idleState = new IdleState(this);
        walkState = new WalkState(this);
        runState = new RunState(this);
        dodgeState = new DodgeState(this);
        lightAttackState = new LightAttackState(this);
        heavyAttackState = new HeavyAttackState(this);
        hitState = new HitState(this);
        frameworkState = new FrameworkState(this);
        magicState = new MagicState(this);
        excuteState = new ExecuteState(this);
        
        currentState = idleState;
    }

    private void Start()
    {
        PlayerManager.Instance.player = this;
    }

    private void OnEnable()
    {
        rpgInputActions.Enable();
    }

    private void OnDisable()
    {
        rpgInputActions.Disable();
    }
    private void Update()
    {
        velocity.x = 0;
        velocity.z = 0;
        bool isGrounded = characterController.isGrounded;
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // 保证贴地，不然可能会浮起来
        }

        velocity.y += Time.deltaTime * inputManager.gravity;

        currentState.Update();

        characterController.Move(velocity * Time.deltaTime);

        UpDateTargetEnemy();
    }

    public Transform targetEnemy;
    public float lockOnRange = 50f;  // 锁定范围
    private float centerAreaWidth = 0.5f;  // 屏幕中心区域宽度（例如：0.2表示20%的屏幕宽度）
    private float centerAreaHeight = 1f;  // 屏幕中心区域高度（例如：0.2表示20%的屏幕高度）
public void UpDateTargetEnemy()
{
    // 获取视野范围内的敌人
    Collider[] hitEnemies = Physics.OverlapSphere(transform.position, lockOnRange);

    Transform closestEnemy = null;
    float closestDistance = Mathf.Infinity;  // 初始化为无穷大，表示我们会寻找最小距离
    float closestDot = -Mathf.Infinity;  // 用来判断敌人是否在视野中心

    foreach (Collider enemy in hitEnemies)
    {
        if (enemy.CompareTag("Enemy"))  // 检查是否是敌人
        {
            Camera playerCamera = Camera.main;
            Vector3 enemyScreenPos = playerCamera.WorldToScreenPoint(enemy.transform.position);

            // 判断敌人是否在摄像头的前方
            Vector3 directionToEnemy = enemy.transform.position - playerCamera.transform.position;
            float dot = Vector3.Dot(playerCamera.transform.forward, directionToEnemy);

            // 如果敌人不在摄像头前方，则跳过
            if (dot < 0)
                continue;  // 如果小于0，说明敌人在摄像头背后，跳过此敌人

            // 计算敌人距离摄像头视野中心的距离（屏幕坐标中的位置）
            Vector3 screenCenter = new Vector3(Screen.width / 2, Screen.height / 2, 0);

            // 判断敌人是否在屏幕中央的区域内
            float minX = (Screen.width * (1 - centerAreaWidth)) / 2;
            float maxX = (Screen.width * (1 + centerAreaWidth)) / 2;
            float minY = (Screen.height * (1 - centerAreaHeight)) / 2;
            float maxY = (Screen.height * (1 + centerAreaHeight)) / 2;

            // 如果敌人的屏幕坐标在中心区域内
            if (enemyScreenPos.x > minX && enemyScreenPos.x < maxX && enemyScreenPos.y > minY &&
                enemyScreenPos.y < maxY)
            {
                // 计算敌人与玩家的距离
                float distanceToPlayer = Vector3.Distance(enemy.transform.position, transform.position);

                // 如果这个敌人在视野内，并且离玩家最近，就锁定
                if (distanceToPlayer < closestDistance)
                {
                    closestDistance = distanceToPlayer;
                    closestEnemy = enemy.transform;
                }
            }
        }
    }

    // 如果找到了最近的敌人，标记为锁定目标
    if (closestEnemy != null)
    {
        targetEnemy = closestEnemy;
        PlayerManager.Instance.targetEnemy = targetEnemy.gameObject;

        // 判断玩家是否在敌人背后并更新标识颜色
        Vector3 directionToEnemy = targetEnemy.position - Camera.main.transform.position;
        float dotToEnemy = Vector3.Dot(targetEnemy.forward, directionToEnemy);

        if (dotToEnemy > 0)  // 玩家在敌人背后
        {
            PlayerManager.Instance.canExecute = true;
        }
        else
        {
            PlayerManager.Instance.canExecute = false;
        }

        // 如果需要让摄像头朝向敌人
        // transform.LookAt(targetEnemy);  // 取消注释以启用摄像头朝向
    }
    else
    {
        // 没有敌人，取消锁定
        targetEnemy = null;
        PlayerManager.Instance.targetEnemy = null;
        PlayerManager.Instance.canExecute = false;
    }
}


    
    public void ChangeState(StateType type)
    {
        if ((type!=StateType.Hit && type == currentState.stateType)||!currentState.CanChangeTo(type))
        {
            return;
        }

        PlayerState newState = null;
        switch (type)
        {
            case StateType.Idle: newState = idleState; break;
            case StateType.Walk: newState = walkState; break;
            case StateType.Run: newState = runState; break;
            case StateType.Dodge: newState = dodgeState; break;
            case StateType.LightAttack: newState = lightAttackState; break;
            case StateType.HeavyAttack: newState = heavyAttackState; break;
            case StateType.Hit:newState = hitState; break;
            case StateType.Framework: newState = frameworkState; break;
            case StateType.Magic: newState = magicState; break;
            case StateType.Execute: newState = excuteState; break;
        }
        currentState?.Exit();
        currentState = newState;
        currentState?.Enter();
    }

    public Vector3 GetMoveDir()
    {
        Vector2 moveVector2 = rpgInputActions.PC.MOVE.ReadValue<Vector2>();
        Vector3 cameraForward = cameraTransform.forward;
        Vector3 cameraRight = cameraTransform.right;
        // 将摄像机的前进方向（忽略垂直）和右方向组合来计算移动方向
        cameraForward.y = 0;  // 防止摄像机的上下旋转影响角色移动
        cameraForward.Normalize();  // 确保方向是单位向量
        
        cameraRight.y = 0;  // 同样忽略上下旋转的影响
        cameraRight.Normalize();  // 确保方向是单位向量
        // 计算相对于摄像机视角的移动方向
        Vector3 movement = cameraForward * moveVector2.y + cameraRight * moveVector2.x;
        return movement;
    }
    public Vector2 TryMove(bool isRun = false)
    {
        Vector2 moveVector2 = rpgInputActions.PC.MOVE.ReadValue<Vector2>();
        // Debug.Log(moveVector2);
        //判断是否有按下对应的Move按键
        if (moveVector2 == Vector2.zero)
        {
            return Vector2.zero;
        }
        float moveSpeed = isRun?inputManager.runSpeed:inputManager.walkSpeed;
        // 获取相对于摄像机的移动方向
        Vector3 movement = GetMoveDir();
        
        // 旋转角色朝向移动方向
        Quaternion targetRotation = Quaternion.LookRotation(movement);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, inputManager.turnSpeed * Time.deltaTime);

        // 改变速度
        velocity.x += movement.x * moveSpeed;  // 使用速度来控制角色移动
        velocity.z += movement.z * moveSpeed;  // 使用速度来控制角色移动
        
        return moveVector2;
    }

    private bool isDodgeClicked = false;
    public bool canDodgeClick = true;
    private bool isJumpClicked = false;
    public bool canJumpClick = true;
    public bool TryDodge()
    {
        if (rpgInputActions.PC.Dodge.IsPressed())
        {
            if (!isDodgeClicked && canDodgeClick)
            {
                isDodgeClicked = true;
                return true;
            }
        }
        else
        {
            isDodgeClicked = false;
            canDodgeClick = true;
        }
        return false;
    }
    public bool TryJump()
    {
        return isJumpClicked;
    }

    public bool canLightAttack = true;
    public bool TryLightAttack()
    {
        if (rpgInputActions.PC.LightAttack.IsPressed())
        {
            if (canLightAttack)
            {
                return true;
            }
        }
        else
        {
            canLightAttack = true;
        }
        return false;
    }
    public bool canHeavyAttack = true;
    public bool TryHeavyAttack()
    {
        if (rpgInputActions.PC.HeavyAttack.IsPressed())
        {
            if (canHeavyAttack)
            {
                return true;
            }
        }
        else
        {
            canHeavyAttack = true;
        }
        return false;
    }

    public bool TryFramework()
    {
        if (rpgInputActions.PC.Framwork.IsPressed()&&PlayerManager.Instance.TryUseSyncRate(inputManager.shieldSyncUse))
        {
            return true;
        }

        return false;
    }

    public bool TryMagic()
    {
        if (rpgInputActions.PC.Magic.IsPressed()&&PlayerManager.Instance.targetEnemy&&PlayerManager.Instance.TryUseSyncRate(inputManager.shieldSyncUse))
        {
            GameObject magic =  Instantiate(inputManager.magicPrefab);
            LightningSpearController magicController = magic.GetComponent<LightningSpearController>();
            magicController.Init(PlayerManager.Instance.targetEnemy);
            return true;
        }

        return false;
    }

    public bool TryExecute()
    {
        if (rpgInputActions.PC.Execute.IsPressed())
        {
            return PlayerManager.Instance.canExecute;
            
        }
        return false;
    }

    public void BeAttacked(int damage)
    {
        currentState.BeAttacked(damage);
    }

    public void AttackEnemy(IEnemyInterface enemy)
    {
        int damage = 0;
        if (currentState.stateType == StateType.LightAttack)
        {
            damage = (int)PlayerManager.Instance.AttackPower;
        }
        else if (currentState.stateType == StateType.HeavyAttack)
        {
            damage = (int)(PlayerManager.Instance.AttackPower * PlayerManager.Instance.HeavyAttackRatio);
        }
        enemy.ByAttacked(damage);
        PlayerManager.Instance.AddSyncRate(inputManager.AttackAddSync);
    }
}
