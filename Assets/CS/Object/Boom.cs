using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boom : MonoBehaviour
{
    public float destroyTime;

    void Start()
    {
        StartCoroutine(BoomScale());
    }

    void Update()
    {
        
    }

    IEnumerator BoomScale()
    {
        int times = 40;

        for (int i = 0; i < times; i++)
        {
            transform.localScale *= 0.9f;
            yield return new WaitForSeconds(destroyTime / times);
        }

        Destroy(gameObject);
        yield return null;
    }
}
