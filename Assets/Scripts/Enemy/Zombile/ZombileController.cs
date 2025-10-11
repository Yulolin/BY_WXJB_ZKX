
using System;
using UnityEngine;
using UnityEngine.UI;

public class ZombileController : MonoBehaviour,IEnemyInterface
{
    public Animator animator;
    public Rigidbody rb;

    [Header("属性")]
    public int HP = 20;
    public float AttackPower = 10f;
    [Header("距离")]
    public float AttackDis = 5f;
    public float AttackRange = 1.5f;
    public float PursuitDis = 10f;
    [Header("移动属性")] 
    public float walkSpeed = 1f;  
    public float runSpeed = 2f;
    public float screamTime = 2.767f;

    private EnemyState currentState;
    private EIdleState idleState;
    private EAttackState attackState;
    private EHitState hitState;
    private EPatrolState patrolState;
    private EPursuitState pursuitState;
    private EBeExecuteState beExecuteState;

    public Transform target;
    public Slider hpSlider;

    private void Awake()
    {
        idleState = new EIdleState(this);
        attackState = new EAttackState(this);
        hitState = new EHitState(this);
        patrolState = new EPatrolState(this);
        pursuitState = new EPursuitState(this);
        beExecuteState = new EBeExecuteState(this);
        
        currentState = idleState;
    }

    private void Start()
    {
        player = PlayerManager.Instance.player.transform;
        hpSlider.maxValue = HP;
    }

    private void Update()
    {
        currentState.Update();
        

        if (gameObject == PlayerManager.Instance.targetEnemy)
        {
            hpSlider.gameObject.SetActive(true);
            Vector3 screenPos = Camera.main.WorldToScreenPoint(target.position);
            screenPos.y += 100;
            hpSlider.GetComponent<RectTransform>().position = screenPos;
        }
        else
        {
            hpSlider.gameObject.SetActive(false);
        }
    }

    public void ChangeState(EStateType type)
    {
        if ((type!=EStateType.Hit&&type == currentState.stateType) || !currentState.CanChangeTo(type))
        {
            return;
        }
        currentState.Exit();
        switch (type)
        {
            case EStateType.Idle:currentState =  idleState; break;
            case EStateType.Patrol:currentState = patrolState; break;
            case EStateType.Pursuit:currentState = pursuitState; break;
            case EStateType.Attack : currentState = attackState; break;
            case EStateType.Hit : currentState = hitState; break;
            case EStateType.BeExecute:currentState = beExecuteState; break;
        }
        currentState.Enter();
    }

    public bool hasAttacked = false;
    public bool isWalk = false;
    public Transform player;
    public void UpdateState()
    {
        float disWithPlayer = (player.position - transform.position).magnitude;
        if (disWithPlayer > AttackDis && disWithPlayer < PursuitDis)
        {
            ChangeState(EStateType.Pursuit);
            if (hasAttacked)
            {
                isWalk = true;
            }
        }
        else if(disWithPlayer < AttackDis)
        {
            ChangeState(EStateType.Attack);
            hasAttacked = true;
        }
        else if (disWithPlayer > PursuitDis)
        {
            ChangeState(EStateType.Idle);
            hasAttacked = false;
        }
    }

    public void ByAttacked(int damage)
    {
        HP -= damage;
        hpSlider.value = HP;
        ChangeState(EStateType.Hit);
    }
}
