using UnityEngine;

public class HeavyAttackState :PlayerState
{
    public HeavyAttackState(PlayerController player) : base(player)
    {
        stateType = StateType.HeavyAttack;
    }

    public override void Enter()
    {
        animator.SetTrigger("HeavyAttack");
        Debug.Log("HeavyAttack");
        player.canHeavyAttack = false;
    }

    public override void Update()
    {

    }

    public override void Exit()
    {
        player.canHeavyAttack = false;
    }
}