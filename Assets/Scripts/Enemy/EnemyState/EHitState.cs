using UnityEngine;

public class EHitState : EnemyState
{
    public EHitState(ZombileController zombileController) : base(zombileController)
    {
        stateType = EStateType.Hit;
    }

    public override bool CanChangeTo(EStateType type)
    {
        return !isDie;
    }

    private bool isDie = false;
    public override void Enter()
    {
        Debug.Log("僵尸进入Hit");
        if (zombile.HP <= 0)
        {
            isDie = true;
            animator.SetBool("Death", true);
        }
        animator.SetTrigger("Hit");
    }

    public override void Update()
    {
        
    }

    public override void Exit()
    {
        
    }
}