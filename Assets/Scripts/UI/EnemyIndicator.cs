using System;
using UnityEngine;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class EnemyIndicator : MonoBehaviour
{
    public Transform enemy;  // 敌人位置
    public RectTransform indicator;  // UI 标识图标
    public Image indicatorImage;
    private void Start()
    {
        indicator =  GetComponent<RectTransform>();
    }

    void Update()
    {
        // 判断敌人是否在摄像头的前方
        Vector3 directionToEnemy = enemy.position - Camera.main.transform.position;
        float dot = Vector3.Dot(Camera.main.transform.forward, directionToEnemy);
        // 如果敌人不在摄像头前方，则跳过
        if (dot < 0)
        {
            indicatorImage.color = Color.clear;
        }
        else
        {
            indicatorImage.color = Color.white;
        }
        Vector3 screenPos = Camera.main.WorldToScreenPoint(enemy.position);
        screenPos.y += 30;
        indicator.position = screenPos;
    }
}