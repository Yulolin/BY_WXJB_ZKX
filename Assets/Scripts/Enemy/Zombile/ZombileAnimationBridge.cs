using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombileAnimationBridge : MonoBehaviour
{
    public Animator animator;
    public ZombileController zombile;

    public GameObject attack;
    public void SetAttackActive()
    {
        attack.SetActive(true);
    }
    public void SetAttackUnActive()
    {
        attack.SetActive(false);
    }

    public void OnAttackEnd()
    {
        zombile.ChangeState(EStateType.Idle);
    }
    public void OnHitEnd()
    {
        zombile.ChangeState(EStateType.Idle);
    }
    
}
