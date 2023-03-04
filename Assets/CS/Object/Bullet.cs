using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public bool isPlayer;
    public float Damage;

    string targetTag;
    void Start()
    {
        targetTag = isPlayer ? "Enemy" : "Player";
        Damage = isPlayer ? GameManager.GM.palyerDamage : GameManager.GM.enemyDamage;
    }

    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(targetTag))
        {
            collision.gameObject.GetComponent<Airship>()._HP = Damage;
            Destroy(gameObject);
        }

        if(collision.gameObject.CompareTag("Border"))
            Destroy(gameObject);
    }
}
