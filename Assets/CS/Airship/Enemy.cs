using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Airship
{
    GameObject HPbar;   // ü�¹� ������Ʈ
    float tmpeHP;       // ü�¹� ����� �ӽ� ü�� ����

    [Header("���� ����")]
    public bool isAttack;               // ���� ����
    public bool isDoubleAttack;         // ���� ���� ����
    public GameObject bulletObject;     // �Ѿ� ������Ʈ
    public float attackTime;            // ���� ����
    public float bulletSpeed;           // �Ѿ� �ӵ�

    [HideInInspector]
    public float moveX;
    [HideInInspector]
    public float moveY;
    public override void Awake()
    {
        base.Awake();

        // ü�¹� ������Ʈ
        HPbar = transform.Find("HP_Canvas").gameObject;
    }

    public override void Start()
    {
        tmpeHP = HP;

        if (isAttack)
        {
            if (isDoubleAttack) StartCoroutine(Fire_D());
            else StartCoroutine(Fire_M());
        }
    }

    public override void Update()
    {
        // ���ӿ����� ��ü �ߴ�
        if (GameManager.GM.isGameOver) return;

        if (HP != tmpeHP) HPbar.SetActive(true);
        else tmpeHP = HP;

        Move(new Vector2(moveX, moveY));
        //Move(new Vector2(0f, -1f));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if(!GameManager.GM.isInvincibility)
                collision.gameObject.GetComponent<Airship>()._HP = GameManager.GM.enemyDamage;

            Die();

            return;
        }

        if(collision.gameObject.CompareTag("Cargo"))
        {
            collision.gameObject.GetComponent<Airship>()._HP = GameManager.GM.enemyDamage;
            point = 0;  // �浹�� ���� ������ ����Ʈ�� ���� X , ������ óġ�� ���� x
            Die();

            return;
        }

        if (collision.gameObject.CompareTag("Beam"))
        {
            _HP = GameManager.GM.cargoBeamDamage;
            return;
        }
    }

    IEnumerator Fire_M()
    {
        // ���ӿ����� ������ ���� ����
        while (!GameManager.GM.isGameOver)
        {
            Fire_Mid();
            yield return new WaitForSeconds(attackTime);
        }
    }
    IEnumerator Fire_D()
    {
        // ���ӿ����� ������ ���� ����
        while (!GameManager.GM.isGameOver)
        {
            Fire_Side();
            yield return new WaitForSeconds(attackTime);
        }
    }

    void Fire_Mid()
    {
        GameObject bullet;

        bullet = Instantiate(bulletObject, transform.position, transform.rotation);
        bullet.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, -1) * 10, ForceMode2D.Impulse);
    }   // ��� 1��

    void Fire_Side()
    {
        GameObject bullet;

        bullet = Instantiate(bulletObject, transform.position + new Vector3(-0.2f, 0f, 0f), transform.rotation);
        bullet.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 1) * bulletSpeed, ForceMode2D.Impulse);

        bullet = Instantiate(bulletObject, transform.position + new Vector3(0.2f, 0f, 0f), transform.rotation);
        bullet.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 1) * bulletSpeed, ForceMode2D.Impulse);
    }   // ��� �� 2��
}
