using UnityEngine;

public enum StateType{
    Idle,
    Walk,
    Run,
    Dodge,
    LightAttack,
    HeavyAttack,
    Hit,
    Framework,
    Magic,
    Execute
}

public abstract class PlayerState
{
    public StateType stateType;
    protected PlayerController player;
    protected Animator animator;

    public PlayerState(PlayerController player)
    {
        this.player = player;
        animator = player.animator;
    }

    public virtual bool CanChangeTo(StateType type)
    {
        return true;
    }

    public virtual void BeAttacked(int damage)
    {
        PlayerManager.Instance.AddHp(-damage);
        player.ChangeState(StateType.Hit);
        PlayerManager.Instance.TryUseSyncRate(player.inputManager.beAttackedSyncReduce);
    }
    
    public abstract void Enter();
    public abstract void Update();
    public abstract void Exit();
}
