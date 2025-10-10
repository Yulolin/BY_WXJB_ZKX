
using System;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;
    
    public int HP = 100;
    public float AttackPower = 10;
    public float HeavyAttackRatio = 2;
    public float SyncRate = 100;

    public GameObject targetEnemy;

    public PlayerController player;

    private void Awake()
    {
        Instance = this;
    }

    public void AddHp(int value)
    {
        HP += value;
    }

    public bool TryUseSyncRate(float value)
    {
        if (SyncRate > value)
        {
            SyncRate -= value;
            return true;
        }

        return false;
    }

    public void AddSyncRate(float value)
    {
        SyncRate += value;
    }
}
