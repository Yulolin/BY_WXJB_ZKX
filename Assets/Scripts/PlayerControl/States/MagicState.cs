using UnityEngine;

public class MagicState : PlayerState
{
    public MagicState(PlayerController player) : base(player)
    {
        stateType = StateType.Magic;
    }

    public override void BeAttacked(int damage)
    {
        PlayerManager.Instance.AddHp(-damage);
        PlayerManager.Instance.TryUseSyncRate(player.inputManager.beAttackedSyncReduce);
    }

    public override void Enter()
    {
        // animator.SetBool("Shield", true);
        Debug.Log("进入Magic");
        
    }

    private float timer = 0f;
    public override void Update()
    {
        if (timer > player.inputManager.shieldTime)
        {
            player.ChangeState(StateType.Idle);
        }

        timer += Time.deltaTime;
    }

    public override void Exit()
    {
        timer = 0;
    }
}
