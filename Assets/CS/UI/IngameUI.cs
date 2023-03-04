using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class IngameUI : MonoBehaviour
{
    public GameObject player;

    public float setHP;
    public float setOil;

    public Image HPBar;
    public Image oilBar;

    public TextMeshProUGUI ammoText;
    public Image reloadBar;

    void Awake()
    {
        player = GameObject.Find("Player");

        setHP = player.GetComponent<Player>()._HP;
        setOil = player.GetComponent<Player>().oil;
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
        HPBar.fillAmount = player.GetComponent<Player>()._HP / setHP;
        oilBar.fillAmount = player.GetComponent<Player>().oil / setOil;

        if(player.GetComponent<Player>().attack.nowAMMO == 0)
        {
            ammoText.text = "-" + " / " + player.GetComponent<Player>().attack.maxAMMO;
        }
        else
        {
            ammoText.text = player.GetComponent<Player>().attack.nowAMMO + " / " + player.GetComponent<Player>().attack.maxAMMO;
        }
            
        reloadBar.fillAmount = player.GetComponent<Player>().attack.reloadBar / player.GetComponent<Player>().attack.reloadTime;
    }
}
