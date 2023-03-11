using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class IngameUI : MonoBehaviour
{
    GameObject player;

    [Header("ü��,����,ī�� ��")]
    public Image HPBar;
    public Image HPBarPlus;
    public Image oilBar;
    public Image cargoBar;

    float maxHealValue;

    [Header("���� ����")]
    public TextMeshProUGUI ammoText;
    public Image reloadBar;

    [Header("ȸ����ų ����")]
    public Image plusGreenBar;
    public Image plusWhiteBar;

    [Header("���� ���ھ�")]
    public TextMeshProUGUI scoreText;
    void Awake()
    {
        player = GameObject.Find("Player");
        maxHealValue = player.GetComponent<Player>().healHPValue;
    }

    void Start()
    {

    }

    void Update()
    {
        UI_Update();
    }

    void UI_Update()
    {
        HPBar.fillAmount = player.GetComponent<Player>()._HP / player.GetComponent<Player>().setHP;
        oilBar.fillAmount = player.GetComponent<Player>().oil / player.GetComponent<Player>().setOil;
        cargoBar.fillAmount = GameManager.GM.cargoHP / GameManager.GM.setCargoHP;

        if (player.GetComponent<Player>().attack.nowAMMO == 0) ammoText.text = "-" + " / " + player.GetComponent<Player>().attack.maxAMMO;
        else ammoText.text = player.GetComponent<Player>().attack.nowAMMO + " / " + player.GetComponent<Player>().attack.maxAMMO;
            
        reloadBar.fillAmount = player.GetComponent<Player>().attack.reloadBar / player.GetComponent<Player>().attack.reloadTime;

        scoreText.text = "Score\n";
        scoreText.text += GameManager.GM.score == 0? "0" : GameManager.GM.CommaText(GameManager.GM.score);

        HPBarPlus.fillAmount = (player.GetComponent<Player>()._HP + maxHealValue) / player.GetComponent<Player>().setHP;
        plusGreenBar.fillAmount = player.GetComponent<Player>().greenBarTimes / player.GetComponent<Player>().setGreenTimes;
        plusWhiteBar.fillAmount = player.GetComponent<Player>().whiteBar / player.GetComponent<Player>().setWhiteBar;
    }
}
