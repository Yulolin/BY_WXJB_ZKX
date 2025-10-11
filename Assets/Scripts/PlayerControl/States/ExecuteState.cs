using UnityEngine;

public class ExecuteState :PlayerState
{
    public ExecuteState(PlayerController player) : base(player)
    {
        stateType = StateType.HeavyAttack;
    }

    public override void Enter()
    {
        Vector3 lookAtPosition = new Vector3(PlayerManager.Instance.targetEnemy.transform.position.x, PlayerManager.Instance.targetEnemy.transform.position.y, PlayerManager.Instance.targetEnemy.transform.position.z);
        player.transform.LookAt(lookAtPosition);
        animator.SetTrigger("HeavyAttack");
        Debug.Log("ExcuteState");
        PlayerManager.Instance.targetEnemy.GetComponent<ZombileController>().ChangeState(EStateType.BeExecute);
    }

    public override void Update()
    {

    }

    public override void Exit()
    {
        
    }
}