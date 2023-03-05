using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy_UI : MonoBehaviour
{
    GameObject target;   // HP 바가 따라다니는 타겟 오브젝트
    Image hpBar;         // 체력바

    float setHP;         // 해당 캐릭터의 HP 저장
    private void Awake()
    {
        // 타겟 오브젝트 지정
        target = this.gameObject.transform.parent.gameObject;

        // 체력바 이미지 지정
        GameObject barObject = transform.Find("HP_Bar").gameObject;
        hpBar = barObject.GetComponent<Image>();

        // 최대상태의 HP 저장
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
