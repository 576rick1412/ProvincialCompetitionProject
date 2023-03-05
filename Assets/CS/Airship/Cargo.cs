using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cargo : Airship
{
    public override float _HP
    {
        get
        {
            return GameManager.GM.cargoHP;
        }

        set
        {
            if (GameManager.GM.cargoHP - value <= 0)
            {
                GameManager.GM.cargoHP = 0;
                Die();
            }

            else
            {
                GameManager.GM.cargoHP -= value;
                anime.SetTrigger("Hit");
            }
        }
    }

    public override void Awake()
    {
        base.Awake();
    }

    public override void Start()
    {
        
    }

    public override void Update()
    {
        
    }
}
