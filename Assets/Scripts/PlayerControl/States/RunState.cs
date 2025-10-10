using UnityEngine;

public class RunState :PlayerState
{
    public RunState(PlayerController player) : base(player)
    {
        stateType = StateType.Run;
    }

    public override void Enter()
    {
        animator.SetBool("Run", true);
        Debug.Log("进入Run");
    }

    public override void Update()
    {
        if (player.TryMove(true).sqrMagnitude < 1e-6)
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
    }

    public override void Exit()
    {
        animator.SetBool("Run", false);
    }
}