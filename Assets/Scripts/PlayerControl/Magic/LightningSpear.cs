using System;
using UnityEngine;

public class LightningSpear:MonoBehaviour
{
    public LightningSpearController spearController;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("技能打到敌人");
            other.GetComponent<IEnemyInterface>().ByAttacked(spearController.everyDamage);
        }
    }
}
