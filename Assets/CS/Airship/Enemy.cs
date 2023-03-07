using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Airship
{
    GameObject HPbar;   // ü�¹� ������Ʈ
    float tmpeHP;       // ü�¹� ����� �ӽ� ü�� ����

    public string enemyType;

    public override void Awake()
    {
        base.Awake();

        // ü�¹� ������Ʈ
        HPbar = transform.Find("HP_Canvas").gameObject;
    }

    public override void Start()
    {
        tmpeHP = HP;
    }

    public override void Update()
    {
        // ���ӿ����� ��ü �ߴ�
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
            point = 0;  // �浹�� ���� ������ ����Ʈ�� ���� X , ������ óġ�� ���� x
            Die();
        }
    }
}
