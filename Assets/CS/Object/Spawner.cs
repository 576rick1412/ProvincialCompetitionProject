using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] spawnObjects;
    public float spawnTime;

    bool isWaveClear;

    void Awake()
    {
        isWaveClear = true;
    }
    void Start()
    {
        StartCoroutine(Spawn());
    }

    void Update()
    {

    }

    IEnumerator Spawn()
    {
        // 게임오버가 거짓일 때만 동작
        while (!GameManager.GM.isGameOver && isWaveClear)
        {
            for (int i = 0; i < 5; i++)     // 6 * 5 = 30s
            {
                StartCoroutine(Mid_Spawn_TypeA_V_Wave());
                yield return new WaitForSeconds(spawnTime);
            }
        }

        yield return null;
    }

    IEnumerator Mid_Spawn_TypeA_V_Wave()
    {
        isWaveClear = false;

        Vector2 pos;
        GameObject temp;
        const int spawnNum = 0;
        float h;

        h = 0;
        {
            pos = new Vector2(h, transform.position.y);
            temp = Instantiate(spawnObjects[spawnNum], pos, transform.rotation);
            temp.GetComponent<Enemy>().moveX = 0;
            temp.GetComponent<Enemy>().moveY = -1f;
        }   // 가운데 1열

        h = 0.5f;
        for (int i = 0; i < 3; i++, h +=0.5f)
        {
            yield return new WaitForSeconds(0.3f);

            // 좌
            pos = new Vector2(-h, transform.position.y);
            temp = Instantiate(spawnObjects[spawnNum], pos, transform.rotation);
            temp.GetComponent<Enemy>().moveX = 0;
            temp.GetComponent<Enemy>().moveY = -1f;

            // 우
            pos = new Vector2(h, transform.position.y);
            temp = Instantiate(spawnObjects[spawnNum], pos, transform.rotation);
            temp.GetComponent<Enemy>().moveX = 0;
            temp.GetComponent<Enemy>().moveY = -1f;
        }

        isWaveClear = true;
        yield return null;
    }
}
