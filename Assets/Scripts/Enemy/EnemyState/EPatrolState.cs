
using UnityEngine;

public class EPatrolState : EnemyState
{
    public EPatrolState(ZombileController zombileController) : base(zombileController)
    {
        stateType = EStateType.Patrol;
    }

    public override void Enter()
    {
        Debug.Log("僵尸进入Patrol");
    }

    public override void Update()
    {
        
    }

    public override void Exit()
    {
        
    }
}
