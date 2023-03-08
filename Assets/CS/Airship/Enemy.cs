using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Airship
{
    GameObject HPbar;   // ü�¹� ������Ʈ
    float tmpeHP;       // ü�¹� ����� �ӽ� ü�� ����

    public string enemyType;

    [Header("���� ����")]
    public bool isAttack;               // ���� ����
    public GameObject bulletObject;     // �Ѿ� ������Ʈ
    public float attackTime;            // ���� ����

    public override void Awake()
    {
        base.Awake();

        // ü�¹� ������Ʈ
        HPbar = transform.Find("HP_Canvas").gameObject;
    }

    public override void Start()
    {
        tmpeHP = HP;

        if(isAttack) StartCoroutine(Fire());
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
            if(!GameManager.GM.isInvincibility)
                collision.gameObject.GetComponent<Airship>()._HP = GameManager.GM.enemyDamage;
            point = 0;  // �浹�� ���� ������ ����Ʈ�� ���� X , ������ óġ�� ���� x
            Die();
        }

        if(collision.gameObject.CompareTag("Cargo"))
        {
            collision.gameObject.GetComponent<Airship>()._HP = GameManager.GM.enemyDamage;
            point = 0;  // �浹�� ���� ������ ����Ʈ�� ���� X , ������ óġ�� ���� x
            Die();
        }
    }

    IEnumerator Fire()
    {
        // ���ӿ����� ������ ���� ����
        while (!GameManager.GM.isGameOver)
        {
            Fire_Mid();
            yield return new WaitForSeconds(attackTime);
        }
    }

    void Fire_Mid()
    {
        GameObject bullet;

        bullet = Instantiate(bulletObject, transform.position, transform.rotation);
        bullet.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, -1) * 10, ForceMode2D.Impulse);
    }   // ��� 1��
}
