using UnityEngine;

public class FrameworkState :PlayerState
{
    public FrameworkState(PlayerController player) : base(player)
    {
        stateType = StateType.Framework;
    }

    public override void BeAttacked(int damage)
    {
        timer = 0;
        isAttacked = true;
    }

    public override void Enter()
    {
        animator.SetBool("Shield", true);
        Debug.Log("进入FrameworkState");
        moveDir = -player.transform.forward;
        speed = player.inputManager.shieldMoveDis / moveTime;
    }

    private float timer = 0f;
    private bool isAttacked;
    private float moveTime = 0.2f;
    private Vector3 moveDir;
    private float speed;
    public override void Update()
    {
        if (timer > player.inputManager.shieldTime)
        {
            player.ChangeState(StateType.Idle);
        }

        if (isAttacked)
        {
            if (timer < moveTime)
            {
                player.velocity.x += moveDir.x * speed;
                player.velocity.z += moveDir.z * speed;
            }
        }

        timer += Time.deltaTime;
    }

    public override void Exit()
    {
        timer = 0;
        isAttacked = false;
    }
}