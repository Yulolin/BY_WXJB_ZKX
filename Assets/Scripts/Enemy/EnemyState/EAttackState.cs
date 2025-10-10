using UnityEngine;

public class EAttackState : EnemyState
{
    public EAttackState(ZombileController zombileController) : base(zombileController)
    {
        stateType = EStateType.Attack;
    }

    public override void Enter()
    {
        Debug.Log("僵尸进入Attack");
        // 获取玩家位置，但保持Y轴与敌人相同
        Vector3 lookAtPosition = new Vector3(zombile.player.position.x, rb.transform.position.y, zombile.player.position.z);
        rb.transform.LookAt(lookAtPosition);
        animator.SetTrigger("Attack");
    }

    public override void Update()
    {
        
    }

    public override void Exit()
    {
        
    }
}