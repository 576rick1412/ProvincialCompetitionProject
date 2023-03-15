using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBG : MonoBehaviour
{
    public float startPos;
    public float endPos;

    public bool isCargo;
    public float speed;

    SpriteRenderer SR;
    public Sprite[] Sprites;
    private void Start()
    {
        if (isCargo)
        {
            speed = Random.Range(-1f, -3.5f);
            float scale = Random.Range(0.2f, 0.75f);
            transform.localScale = new Vector3(scale, scale, scale);
        }

        SR = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // ���ӿ����� ��ü �ߴ�
        if (GameManager.GM.isGameOver) return;

        transform.position += new Vector3(0, speed * -1, 0) * Time.deltaTime;

        if (isCargo)
        {
            if (transform.position.y >= endPos)
            {
                float h = Random.Range(-2.5f, 2.5f);
                speed = Random.Range(-1f, -3.5f);
                float scale = Random.Range(0.2f, 0.75f);
                transform.localScale = new Vector3(scale, scale, scale);

                transform.position += new Vector3(0, startPos * 2, 0);
                transform.position  = new Vector3(h, transform.position.y, transform.position.z); 

                if(!isCargo)
                {
                    switch(GameManager.GM.gameLevel)
                    {
                        case 1:
                            SR.sprite = Sprites[0];
                            break;

                        case 2:
                            SR.sprite = Sprites[1];
                            break;

                        case 3:
                            SR.sprite = Sprites[2];
                            break;
                    }
                }
            }
        }   // ����ü ������Ʈ üũ

        else
        {
            if (transform.position.y <= endPos)
                transform.position += new Vector3(0, startPos * 2, 0);
        }
    }
}
