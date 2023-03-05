using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy_UI : MonoBehaviour
{
    GameObject target;   // HP �ٰ� ����ٴϴ� Ÿ�� ������Ʈ
    Image hpBar;         // ü�¹�

    float setHP;         // �ش� ĳ������ HP ����
    private void Awake()
    {
        // Ÿ�� ������Ʈ ����
        target = this.gameObject.transform.parent.gameObject;

        // ü�¹� �̹��� ����
        GameObject barObject = transform.Find("HP_Bar").gameObject;
        hpBar = barObject.GetComponent<Image>();

        // �ִ������ HP ����
        setHP = target.GetComponent<Enemy>().HP;

        gameObject.SetActive(false);
    }
    void Start()
    {

    }

    void Update()
    {
        float hp = target.GetComponent<Enemy>().HP;

        hpBar.fillAmount = hp / setHP;
    }
}
