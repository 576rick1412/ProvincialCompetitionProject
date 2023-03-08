using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Airship
{
    GameObject HPbar;   // 체력바 오브젝트
    float tmpeHP;       // 체력바 띄우기용 임시 체력 저장

    public string enemyType;

    [Header("공격 설정")]
    public bool isAttack;               // 공격 여부
    public GameObject bulletObject;     // 총알 오브젝트
    public float attackTime;            // 공격 간격

    public override void Awake()
    {
        base.Awake();

        // 체력바 오브젝트
        HPbar = transform.Find("HP_Canvas").gameObject;
    }

    public override void Start()
    {
        tmpeHP = HP;

        if(isAttack) StartCoroutine(Fire());
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
            if(!GameManager.GM.isInvincibility)
                collision.gameObject.GetComponent<Airship>()._HP = GameManager.GM.enemyDamage;
            point = 0;  // 충돌로 인한 자폭은 포인트로 인정 X , 자폭은 처치로 인정 x
            Die();
        }

        if(collision.gameObject.CompareTag("Cargo"))
        {
            collision.gameObject.GetComponent<Airship>()._HP = GameManager.GM.enemyDamage;
            point = 0;  // 충돌로 인한 자폭은 포인트로 인정 X , 자폭은 처치로 인정 x
            Die();
        }
    }

    IEnumerator Fire()
    {
        // 게임오버가 거짓일 때만 동작
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
    }   // 가운데 1줄
}
