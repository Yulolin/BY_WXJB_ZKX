
using System;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;

    private int hp = 100;

    public int HP
    {
        get { return hp; }
        set
        {
            hp = Mathf.Clamp(value, 0, MaxHP);
            OnHpChanged?.Invoke(hp);
        }
    }
    public int MaxHP = 100;
    public float AttackPower = 10;
    public float HeavyAttackRatio = 2;
    private float syncRate = 100;

    public float SyncRate
    {
        get { return syncRate; }
        set
        {
            syncRate = Mathf.Clamp(value, 0, MaxSyncRate);
            OnSyncChanged?.Invoke(syncRate);
        }
    }
    public float MaxSyncRate = 100;

    public GameObject targetEnemy;
    public bool canExecute = false;

    public PlayerController player;
    
    public Action<int> OnHpChanged;
    public Action<float> OnSyncChanged;

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
