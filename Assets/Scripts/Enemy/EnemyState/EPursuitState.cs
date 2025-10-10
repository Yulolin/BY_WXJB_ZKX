using UnityEngine;

public class EPursuitState : EnemyState
{
    public EPursuitState(ZombileController zombileController) : base(zombileController)
    {
        stateType = EStateType.Pursuit;
    }

    public override void Enter()
    {
        if (!zombile.isWalk)
        {
            Debug.Log("僵尸进入Pursuit");
            animator.SetTrigger("Scream");
        }
        else
        {
            animator.SetBool("Walk",true);
        }

    }
    float timer = 0;
    public override void Update()
    {
        zombile.UpdateState();
        // 获取玩家位置，但保持Y轴与敌人相同
        Vector3 lookAtPosition = new Vector3(zombile.player.position.x, rb.transform.position.y, zombile.player.position.z);

        if (!zombile.isWalk)
        {
            animator.SetBool("Run",true);
            animator.SetBool("Walk",false);
            if (timer < zombile.screamTime)
            {
                rb.transform.LookAt(lookAtPosition);
                timer += Time.deltaTime;
                return;
            }
        }
        else
        {
            animator.SetBool("Walk",true);
            animator.SetBool("Run",false);

        }
        rb.transform.LookAt(lookAtPosition);
        
        // 计算移动方向
        Vector3 direction = (zombile.player.position - zombile.transform.position).normalized;
        direction.y = 0;
        // 计算目标位置
        Vector3 targetPosition = rb.transform.position + direction * (zombile.isWalk? zombile.walkSpeed : zombile.runSpeed) * Time.deltaTime;
        // 移动Rigidbody
        rb.MovePosition(targetPosition);
        
        timer += Time.deltaTime;
        
    }

    public override void Exit()
    {
        animator.SetBool("Run", false);
        animator.SetBool("Walk",false);
        timer = 0;
    }
}