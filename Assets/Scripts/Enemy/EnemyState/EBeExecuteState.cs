using UnityEngine;

public class EBeExecuteState : EnemyState
{
    public EBeExecuteState(ZombileController zombileController) : base(zombileController)
    {
        stateType = EStateType.BeExecute;
    }

    public override bool CanChangeTo(EStateType type)
    {
        if (type == EStateType.Hit)
        {
            return true;
        }

        return isEnd;
    }

    private float timer = 0;
    private bool isEnd;
    public override void Enter()
    {
        timer = 0;
        isEnd = false;
        animator.SetTrigger("BeExecute");
        Debug.Log("僵尸进入BeExecute");
    }

    public override void Update()
    {
        timer += Time.deltaTime;
        if (timer > 2f)
        {
            isEnd = true;
            zombile.ChangeState(EStateType.Idle);
        }
    }

    public override void Exit()
    {
        
    }
}