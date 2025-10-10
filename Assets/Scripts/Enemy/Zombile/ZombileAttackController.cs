using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombileAttackController : MonoBehaviour
{
    public ZombileController zombile;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerController>().BeAttacked((int)zombile.AttackPower);
        }
    }
}
