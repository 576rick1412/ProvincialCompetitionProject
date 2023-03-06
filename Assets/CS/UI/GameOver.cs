using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Text.RegularExpressions;

public class GameOver : MonoBehaviour
{
    public TextMeshProUGUI overType;
    public TextMeshProUGUI resultText;

    public TMP_InputField playerNameInput;
    public string playerName = null;

    void Awake()
    {
        playerName = playerNameInput.GetComponent<TMP_InputField>().text;
        playerNameInput.onValueChanged.AddListener((word) => playerNameInput.text = Regex.Replace(word, @"[^0-9a-zA-Z��-�R]", ""));
    }

    void Start()
    {

    }

    void Update()
    {

    }

    public void InputName()
    {
        playerName = playerNameInput.text;
    }

    public void Back()
    {
        Rank_Reset();

        SceneManager.LoadScene("MainScene");
    }

    public void Retry()
    {
        Rank_Reset();

        SceneManager.LoadScene("inGameScene"); 
    }
    
    void Rank_Reset()
    {
        if (playerName.Length > 0) // �Է����� �ʾҴٸ� ��� ����
        {
            GameManager.GM.LineUpRank(playerName, GameManager.GM.score);
            GameManager.GM.SavaData();
        }

        GameManager.GM.ResetGameData();

        Destroy(gameObject);
    }   // ��ũ ���� & �ΰ��� ������ ����
}
