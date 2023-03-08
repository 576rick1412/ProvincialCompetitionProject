using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public bool isPlayer;
    float Damage;
    void Start()
    {
        Damage = isPlayer ? GameManager.GM.palyerDamage : GameManager.GM.enemyDamage;
    }

    void Update()
    {
        // 총알제거가 켜지면 모든 총알 제거
        if (GameManager.GM.isBulletDestroy && !isPlayer) Destroy(gameObject);

        // 게임오버시 전체 삭제
        if (GameManager.GM.isGameOver) Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 게임오버시 전체 중단
        if (GameManager.GM.isGameOver) return;
        if (collision.gameObject == null) return;

        switch (isPlayer)
        {
            case true:
                if (collision.gameObject.CompareTag("Enemy"))
                {
                    collision.gameObject.GetComponent<Airship>()._HP = Damage;
                    Destroy(gameObject);
                }
                break;

            case false:
                if (collision.gameObject.CompareTag("Player"))
                {
                    if (!GameManager.GM.isInvincibility)
                        collision.gameObject.GetComponent<Airship>()._HP = Damage;
                    Destroy(gameObject);
                }
                break;
        }

        if (collision.gameObject.CompareTag("Cargo") && !isPlayer)
        {
            collision.gameObject.GetComponent<Airship>()._HP = Damage;
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Border"))
            Destroy(gameObject);
    }
}
