using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] spawnObjects;

    public float spawnTime;
    void Start()
    {
        StartCoroutine(RandomSpawn());
    }


    void Update()
    {
        
    }

    IEnumerator RandomSpawn()
    {
        while (true)
        {


            float h = Random.Range(-2f, 2f);
            Vector2 pos = new Vector2(h, transform.position.y);
            Instantiate(spawnObjects[0], pos, transform.rotation);

            yield return new WaitForSeconds(spawnTime);
        }
        //yield return null;
    }
}
