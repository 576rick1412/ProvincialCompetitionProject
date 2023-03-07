using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Airship
{
    GameObject HPbar;   // 체력바 오브젝트
    float tmpeHP;       // 체력바 띄우기용 임시 체력 저장

    public string enemyType;

    public override void Awake()
    {
        base.Awake();

        // 체력바 오브젝트
        HPbar = transform.Find("HP_Canvas").gameObject;
    }

    public override void Start()
    {
        tmpeHP = HP;
    }

    public override void Update()
    {
        // 게임오버시 전체 중단
        if (GameManager.GM.isGameOver) return;

        if (HP != tmpeHP) HPbar.SetActive(true);
        else tmpeHP = HP;

        Move(new Vector2(0f, -1f));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Airship>()._HP = GameManager.GM.enemyDamage;
            point = 0;  // 충돌로 인한 자폭은 포인트로 인정 X , 자폭은 처치로 인정 x
            Die();
        }
    }
}
