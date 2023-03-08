using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Airship : MonoBehaviour
{
    public virtual float _HP 
    {
        get 
        { 
            return HP; 
        }

        set 
        {
            if (HP - value <= 0)
            {
                HP = 0;
                Die();
            }

            else
            {
                HP -= value;
                anime.SetTrigger("Hit");
            }
        }
    }

    public float HP;
    public float speed; 
    public uint point;      // óġ �� �ִ� ����Ʈ

    [HideInInspector]
    public Animator anime;
    public GameObject boomObject;
    public virtual void Awake()
    {
        anime = GetComponent<Animator>();
    }

    public virtual void Start()
    {
        
    }

    public virtual void Update()
    {
        
    }

    public void Move(Vector2 pos)
    {
        transform.position += new Vector3(pos.x, pos.y, 0) * speed * Time.deltaTime;
    }

    public virtual void Hit(float Damage) { _HP = Damage; }

    public virtual void Die()
    {
        GameManager.GM.score += point;

        if(gameObject.CompareTag("Enemy"))
        {
            int x = Random.Range(0, 10);
            if (x == 0) Instantiate(GameManager.GM.items[Random.Range(0, GameManager.GM.items.Length)], 
                transform.position, transform.rotation);
            
        }   // ���� ���� ���� �� ������ ���

        Instantiate(boomObject, transform.position, transform.rotation);
        Destroy(gameObject);
    }   // �ı� �� ����
}
