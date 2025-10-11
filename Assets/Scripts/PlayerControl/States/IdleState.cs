
using UnityEngine;

public class IdleState :PlayerState
{
    public IdleState(PlayerController player) : base(player)
    {
        stateType = StateType.Idle;
        Debug.Log("进入idle");
    }

    public override void Enter()
    {
        animator.SetBool("Idle", true);
        Debug.Log("进入idle");
    }

    public override void Update()
    {
        if (player.TryMove().sqrMagnitude > 1e-6)
        {
            player.ChangeState(StateType.Walk);
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
        
    }
}
