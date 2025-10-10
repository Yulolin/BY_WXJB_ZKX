using UnityEngine;

public enum EStateType{
    Idle,
    Patrol,
    Pursuit,
    Hit,
    Attack,
}

public abstract class EnemyState
{
    public EStateType stateType;
    protected ZombileController zombile;
    protected Animator animator;
    protected Rigidbody rb;

    public EnemyState(ZombileController zombile)
    {
        this.zombile = zombile;
        animator = zombile.animator;
        rb = zombile.rb;
    }

    public virtual bool CanChangeTo(EStateType type)
    {
        return true;
    }
    public abstract void Enter();
    public abstract void Update();
    public abstract void Exit();
}
