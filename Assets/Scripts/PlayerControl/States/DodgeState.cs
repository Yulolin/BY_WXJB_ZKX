using UnityEngine;

public class DodgeState :PlayerState
{
    public float dodgeTime = 1f;
    public float invincibleStartTime = 0f;
    public float invincibleTime = 0.5f;
    public float dodgeSpeed;
    public DodgeState(PlayerController player) : base(player)
    {
        stateType = StateType.Dodge;
        dodgeTime = player.inputManager.dodgeTime;
        invincibleStartTime = player.inputManager.invincibleStartTime;
        invincibleTime = player.inputManager.invincibleTime;
        dodgeSpeed =  player.inputManager.dodgeSpeed;
    }
    
    public override void Enter()
    {
        animator.SetTrigger("Dodge");
        Debug.Log("进入Dodge");
        player.canDodgeClick = false;
        dodgeDir = player.GetMoveDir();
        if (dodgeDir.x == 0 && dodgeDir.z == 0)
        {
            dodgeDir = player.transform.forward;
        }
    }
    float timer = 0f;
    private bool isToRun = true;
    // 这里主要防止
    private bool canClick = true;
    private Vector3 dodgeDir;
    public override void Update()
    {
        // 长按shift，进入奔跑，否则进入idle
        if (!player.rpgInputActions.PC.Dodge.IsPressed())
        {
            isToRun = false;
        }
        if (timer >= invincibleStartTime && timer <= invincibleStartTime + invincibleTime)
        {
            // 这里设置无敌帧
        }
        else
        {
            // 这里关闭无敌帧
        }
        
        player.velocity.x += dodgeDir.x * dodgeSpeed;
        player.velocity.z += dodgeDir.z * dodgeSpeed;
        
        if (timer >= dodgeTime)
        {
            // 状态结束
            if (isToRun)
            {
                player.ChangeState(StateType.Run);
            }
            else
            {
                player.ChangeState(StateType.Idle);
            }
        }
        timer += Time.deltaTime;
    }

    public override void Exit()
    {
        timer = 0f;
        isToRun = true;   
    }
}

