using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Airship : MonoBehaviour
{
    public float _HP 
    {
        get 
        { 
            return HP; 
        }

        set 
        {
            if(HP - value <= 0) Die();

            else
            {
                HP -= value;
                anime.SetTrigger("Hit");
            }
        }
    }

    public float HP;
    public float speed;

    Animator anime;

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
        anime.SetInteger("Move", (int)pos.x);
    }

    public virtual void Hit(float Damage) { _HP = Damage; }

    public virtual void Die()
    {
        anime.SetTrigger("Die");
    }   // ÆÄ±« ½Ã ½ÇÇà
}
