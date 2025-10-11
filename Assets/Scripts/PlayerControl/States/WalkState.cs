using UnityEngine;

public class WalkState :PlayerState
{
    public WalkState(PlayerController player) : base(player)
    {
        stateType = StateType.Walk;
    }

    public override void Enter()
    {
        animator.SetBool("Walk", true);
        Debug.Log("进入walk");
    }

    public override void Update()
    {
        if (player.TryMove().sqrMagnitude < 1e-6)
        {
            player.ChangeState(StateType.Idle);
        }
        if (player.TryJump())
        {
            
        }

        if (player.TryDodge())
        {
            player.ChangeState(StateType.Dodge);
        }
        
        if(player.TryLightAttack())
        {
            player.ChangeState(StateType.LightAttack);
        }
        if(player.TryHeavyAttack())
        {
            player.ChangeState(StateType.HeavyAttack);
        }

        if (player.TryFramework())
        {
            player.ChangeState(StateType.Framework);
        }

        if (player.TryMagic())
        {
            player.ChangeState(StateType.Magic);
        }
        if(player.TryExecute())
        {
            player.ChangeState(StateType.Execute);
        }
    }

    public override void Exit()
    {
        animator.SetBool("Walk", false);
    }
}