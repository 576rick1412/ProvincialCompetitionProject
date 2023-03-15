using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Airship
{
    GameObject HPbar;   // 체력바 오브젝트
    float tmpeHP;       // 체력바 띄우기용 임시 체력 저장

    [Header("공격 설정")]
    public bool isAttack;               // 공격 여부
    public bool isDoubleAttack;         // 더블 공격 여부
    public GameObject bulletObject;     // 총알 오브젝트
    public float attackTime;            // 공격 간격
    public float bulletSpeed;           // 총알 속도

    [HideInInspector]
    public float moveX;
    [HideInInspector]
    public float moveY;
    public override void Awake()
    {
        base.Awake();

        // 체력바 오브젝트
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
        // 게임오버시 전체 중단
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
            point = 0;  // 충돌로 인한 자폭은 포인트로 인정 X , 자폭은 처치로 인정 x
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
        // 게임오버가 거짓일 때만 동작
        while (!GameManager.GM.isGameOver)
        {
            Fire_Mid();
            yield return new WaitForSeconds(attackTime);
        }
    }
    IEnumerator Fire_D()
    {
        // 게임오버가 거짓일 때만 동작
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
    }   // 가운데 1줄

    void Fire_Side()
    {
        GameObject bullet;

        bullet = Instantiate(bulletObject, transform.position + new Vector3(-0.2f, 0f, 0f), transform.rotation);
        bullet.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 1) * bulletSpeed, ForceMode2D.Impulse);

        bullet = Instantiate(bulletObject, transform.position + new Vector3(0.2f, 0f, 0f), transform.rotation);
        bullet.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 1) * bulletSpeed, ForceMode2D.Impulse);
    }   // 가운데 옆 2줄
}
