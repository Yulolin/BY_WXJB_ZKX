using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    
    public Slider HPSlider;
    public Slider SyncSlider;
    public Text SyncText;
    public float UISliderChangeTime = 0.3f;

    public RectTransform TargetEnemy;

    private void Start()
    {
        PlayerManager.Instance.OnSyncChanged += OnSyncChange;
        PlayerManager.Instance.OnHpChanged += OnHPChange;
        HPSlider.maxValue = PlayerManager.Instance.MaxHP;
        SyncSlider.maxValue = PlayerManager.Instance.MaxSyncRate;
        SyncSlider.value = PlayerManager.Instance.SyncRate;
        HPSlider.value = PlayerManager.Instance.HP;
    }

    public void Update()
    {
        if (PlayerManager.Instance.targetEnemy)
        {
            Vector3 screenPos = Camera.main.WorldToScreenPoint(PlayerManager.Instance.targetEnemy.transform.position);
            screenPos.y += 200;
            TargetEnemy.gameObject.SetActive(true);
            TargetEnemy.position = screenPos;
            if (PlayerManager.Instance.canExecute)
            {
                for (int i = 0; i < TargetEnemy.childCount; i++)
                {
                    TargetEnemy.GetChild(i).GetComponent<Image>().color = Color.red;
                }
            }
            else
            {
                for (int i = 0; i < TargetEnemy.childCount; i++)
                {
                    TargetEnemy.GetChild(i).GetComponent<Image>().color = Color.white;
                }
            }
        }
        else
        {
            TargetEnemy.gameObject.SetActive(false);
        }
    }

    public void OnSyncChange(float sync)
    {
        StartCoroutine(SyncChange(sync));
    }
    IEnumerator SyncChange(float sync)
    {
        float currentSync = SyncSlider.value;
        float timer = 0;
        while (timer <  UISliderChangeTime)
        {
            timer+=Time.deltaTime;
            float t = timer / UISliderChangeTime;
            if (timer >= UISliderChangeTime)
            {
                t = 1;
            }
            float s = Mathf.SmoothStep(currentSync, sync, t);
            SyncSlider.value = s;
            SyncText.text = s.ToString() + "%";
            yield return null;
        }
    }
    public void OnHPChange(int hp)
    {
        StartCoroutine(HpChange(hp));
    }

    IEnumerator HpChange(int hp)
    {
        
        int currentHP = (int)HPSlider.value;
        float timer = 0;
        while (timer <  UISliderChangeTime)
        {
            timer+=Time.deltaTime;
            float t = timer / UISliderChangeTime;
            if (timer >= UISliderChangeTime)
            {
                t = 1;
            }
            HPSlider.value = Mathf.SmoothStep(currentHP, hp, t);
            yield return null;
        }
    }
}
