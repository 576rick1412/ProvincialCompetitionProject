using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GameManager : MonoBehaviour
{
    public static GameManager GM;

    [Header("������")]
    public int palyerDamage;
    public int enemyDamage;

    [Header("ī�� ü��")]
    public float setCargoHP;
    public float cargoHP;

    [Header("���Ӱ��â")]
    public uint score;              // ���� ���� ����
    public float runTime;           // ���� ���� �ð�
    public uint desMeteo;           // � �ı� 
    public uint desSmallAircraft;   // ������ �ı�
    public uint desMediumAircraft;  // ������ �ı�
    public uint desLargeAircraft;   // ������ �ı�

    [Header("���ӿ���")]
    Transform mainCanvas;    // ���ӿ����� �� ĵ����
    public GameObject overObject;   // ���ӿ����� �˷��� ������Ʈ
    
    [Header("���� ������ ����")]
    public bool isGameOver;         // ���� �� ���� ����
    public bool isBulletDestroy;    // ��� �Ѿ� ����

    string filePath;    // ���� ���
    public MainDB data;

    void Awake()
    {
        GM = this;
        filePath = Application.persistentDataPath + "/MainDB.txt";

        mainCanvas = GameObject.Find("MainCanvas").transform;
    }

    void Start()
    {
        cargoHP = setCargoHP;
    }

    void Update()
    {
        // ���ۼ� ü�� ���� �� ���ӿ���
        if (cargoHP <= 0) GameOver("Cargo Destroy\nmission failed");
    }

    public void SavaData()
    {
        var save = JsonUtility.ToJson(data);
        File.WriteAllText(filePath, save);
    }   // Json ����
    public void LoadData()
    {
        if (!File.Exists(filePath)) { ResetMainDB(); return; }

        var load = File.ReadAllText(filePath);
        data = JsonUtility.FromJson<MainDB>(load);
    }   // Json �ε�

    void ResetMainDB()
    {
        data = new MainDB();
    }

    public void GameOver(string overType)
    {
        isGameOver = true;

        GameObject temp = Instantiate(overObject, mainCanvas.position, Quaternion.identity);
        temp.transform.SetParent(mainCanvas);
        temp.transform.localScale = new Vector3(1, 1, 1);
        temp.GetComponent<GameOver>().overType.text = overType;

        temp.GetComponent<GameOver>().resultText.text =
            "Score : " +   CommaText(score) +
            "\nrunTime : " + (int)runTime +
            "\nMeteo : " + CommaText(desMeteo) +
            "\nSmall : " + CommaText(desSmallAircraft) +
            "\nMedium : "+ CommaText(desMediumAircraft) +
            "\nLarge : " + CommaText(desLargeAircraft);
    }
    public void ResetGameData()
    {
        score = 0;
        runTime = 0;
        desMeteo = 0;
        desSmallAircraft = 0;
        desMediumAircraft = 0;
        desLargeAircraft = 0;
    }

    public void BulletDestroy()
    {
        isBulletDestroy = true;

        StopCoroutine("OnBullet");
        StartCoroutine(OnBullet());
    }
    IEnumerator OnBullet()
    {
        yield return new WaitForSeconds(0.1f);

        isBulletDestroy = false;
        yield return null;
    }   // �Ѿ� ���� �ѱ�

    public string CommaText(uint Num) 
    {
        if (Num == 0) return "0";
        return string.Format("{0:#,###}", Num); 
    }

    [System.Serializable]
    public class MainDB
    {

    }
}
