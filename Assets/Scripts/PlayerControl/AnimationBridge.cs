using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationBridge : MonoBehaviour
{
    public Animator animator;
    public PlayerController player;
    public GameObject lightAttack;
    public GameObject heavyAttack;
    // Start is called before the first frame update
    public void SetLightAttackActive()
    {
        lightAttack.SetActive(true);
    }
    public void SetLightAttackUnActive()
    {
        lightAttack.SetActive(false);
    }

    public void OnLightAttackEnd()
    {
        player.ChangeState(StateType.Idle);
    }

    public void SetHeavyAttackActive()
    {
        heavyAttack.SetActive(true);
    }

    public void SetHeavyAttackUnActive()
    {
        heavyAttack.SetActive(false);
    }
    public void OnHeavyAttackEnd()
    {
        player.ChangeState(StateType.Idle);
    }

    public void OnHitEnd()
    {
        player.ChangeState(StateType.Idle);
    }

    public void OnShieldEnd()
    {
        player.ChangeState(StateType.Idle);
    }
}
