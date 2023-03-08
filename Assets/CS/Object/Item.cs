using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public string itemType; // 아이템 종류
    /*  아이템 종류 , 명칭 정리
     *  
     *  공격강화    AT
     *  무적효과    IN
     *  체력회복    HP
     *  연료회복    FU
     *  카고수리    CA
    */

    Player playerCS;  // 플레이어 스크립트
    void Start()
    {
        playerCS = GameObject.Find("Player").GetComponent<Player>();
    }

    void Update()
    {
        transform.position += new Vector3(0, -1) * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            GameManager.GM.score += 1000;

            switch(itemType)
            {
                case "AT":
                    playerCS._attackReinforce = 1;
                    // 기타설정들도 다 이 안에 있음
                    break;

                case "IN":
                    GameManager.GM._isInvincibility = true;
                    break;

                case "HP":
                    playerCS.HP += 30f;
                    if (playerCS.HP > playerCS.setHP) 
                        playerCS.HP = playerCS.setHP;
                    break;

                case "FU":
                    playerCS.oil += 30f;
                    if (playerCS.oil > playerCS.setOil)
                        playerCS.oil = playerCS.setOil;
                    break;

                case "CA":
                    GameManager.GM.cargoHP += 300f;
                    if (GameManager.GM.cargoHP > GameManager.GM.setCargoHP)
                        GameManager.GM.cargoHP = GameManager.GM.setCargoHP;
                    break;
            }

            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Border"))
            Destroy(gameObject);
    }
}
