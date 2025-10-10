using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("达到敌人");
            PlayerManager.Instance.player.AttackEnemy(other.GetComponent<IEnemyInterface>());
        }
    }
}
