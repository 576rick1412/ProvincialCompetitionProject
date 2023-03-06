using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LobyUI : MonoBehaviour
{
    public GameObject rankUI;
    bool isRank = false;

    public GameObject[] rankObj = new GameObject[8];
    public Rnak[] rank = new Rnak[8];

    void Start()
    {
        for (int i = 0; i < rankObj.Length; i++)
        {
            rank[i].name =  rankObj[i].transform.Find("nameText").GetComponent<TextMeshProUGUI>();
            rank[i].score = rankObj[i].transform.Find("scoreText").GetComponent<TextMeshProUGUI>();

            if (GameManager.GM.data.RankDB[i].name == "") rank[i].name.text = "Null";
            else  rank[i].name.text = GameManager.GM.data.RankDB[i].name;

            rank[i].score.text = GameManager.GM.data.RankDB[i].score.ToString();
        }   // 랭크 텍스트 초기화 및 데이터 입력
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K)) popupRank();
    }

    public void popupRank()
    {
        isRank = !isRank;
        rankUI.SetActive(isRank);

        /*
        GameObject temp =  Instantiate(rankUI,transform.position,transform.rotation);
        temp.transform.SetParent(this.transform);
        temp.transform.localScale = new Vector3(1, 1, 1);
        */
    }

    public struct Rnak
    {
        public TextMeshProUGUI name;
        public TextMeshProUGUI score;
    }
}
