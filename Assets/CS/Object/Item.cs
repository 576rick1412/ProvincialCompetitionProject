using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public string itemType; // ������ ����
    /*  ������ ���� , ��Ī ����
     *  
     *  ���ݰ�ȭ    AT
     *  ����ȿ��    IN
     *  ü��ȸ��    HP
     *  ����ȸ��    FU
     *  ī�����    CA
    */

    Player playerCS;  // �÷��̾� ��ũ��Ʈ
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
                    // ��Ÿ�����鵵 �� �� �ȿ� ����
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
