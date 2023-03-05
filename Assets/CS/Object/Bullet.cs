using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public bool isPlayer;
    float Damage;

    string targetTag;
    void Start()
    {
        targetTag = isPlayer ? "Enemy" : "Player";
        Damage = isPlayer ? GameManager.GM.palyerDamage : GameManager.GM.enemyDamage;
    }

    void Update()
    {
        // �Ѿ����Ű� ������ ��� �Ѿ� ����
        if (GameManager.GM.isBulletDestroy) Destroy(gameObject);

        // ���ӿ����� ��ü ����
        if (GameManager.GM.isGameOver) Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // ���ӿ����� ��ü �ߴ�
        if (GameManager.GM.isGameOver) return;

        if (collision.gameObject.CompareTag(targetTag))
        {
            collision.gameObject.GetComponent<Airship>()._HP = Damage;
            Destroy(gameObject);
        }
            
        if (collision.gameObject.CompareTag("Border"))
            Destroy(gameObject);
    }
}
