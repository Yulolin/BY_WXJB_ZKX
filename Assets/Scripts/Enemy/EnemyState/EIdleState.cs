using UnityEngine;

public class EIdleState : EnemyState
{
    public EIdleState(ZombileController zombileController) : base(zombileController)
    {
        stateType = EStateType.Idle;
    }

    public override void Enter()
    {
        Debug.Log("僵尸进入idle");
    }

    public override void Update()
    {
        zombile.UpdateState();
    }

    public override void Exit()
    {
        
    }
}