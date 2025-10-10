using UnityEngine;

public class HitState:PlayerState
{
    public HitState(PlayerController player) : base(player)
    {
        stateType = StateType.Hit;
    }

    public override void Enter()
    {
        animator.SetTrigger("Hit");
        Debug.Log("进入Hit");
    }

    public override void Update()
    {
        
    }

    public override void Exit()
    {
        
    }
}
