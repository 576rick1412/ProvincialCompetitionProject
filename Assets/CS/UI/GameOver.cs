using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOver : MonoBehaviour
{
    public TextMeshProUGUI overType;
    public TextMeshProUGUI resultText;


    void Start()
    {

    }

    void Update()
    {
        
    }

    public void Back()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void Retry()
    {
        GameManager.GM.ResetGameData();

        SceneManager.LoadScene("inGameScene"); 
    }
    
}
