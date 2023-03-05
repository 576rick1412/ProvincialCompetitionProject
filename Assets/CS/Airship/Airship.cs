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
    public uint point;      // 처치 시 주는 포인트

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

        Instantiate(boomObject, transform.position, transform.rotation);
        Destroy(gameObject);
    }   // 파괴 시 실행
}
