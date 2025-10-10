using UnityEngine;

public class LightAttackState :PlayerState
{
    public LightAttackState(PlayerController player) : base(player)
    {
        stateType = StateType.LightAttack;
    }

    public override void Enter()
    {
        animator.SetTrigger("LightAttack");
        Debug.Log("进入LightAttack");
        player.canLightAttack = false;
    }

    public override void Update()
    {

    }

    public override void Exit()
    {
        player.canLightAttack = false;
    }
}