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

    [Header("ī��")]
    public GameObject cargo;
    public float cargoBeamDamage;
    public float setCargoHP;
    public float cargoHP;

    [Header("���Ӱ��â")]
    public uint score;              // ���� ���� ����
    public float runTime;           // ���� ���� �ð�

    [Header("���ӿ���")]
    Transform GMCanvas;             // ���ӿ����� �� ĵ����
    public GameObject overObject;   // ���ӿ����� �˷��� ������Ʈ

    [Header("���� ������ ����")]
    public int gameLevel;           // ���� ���� ����
    public GameObject invincObj;    // ���� UI
    public bool isInvincibility;    // ����ȿ��
    IEnumerator invinrator;         // ���� �ڷ�ƾ ������
    public bool _isInvincibility
    {
        get
        {
            return isInvincibility;
        }

        set
        {   
            StopCoroutine (invinrator);
            invinrator = InvincibilityControl();
            StartCoroutine(invinrator);
        }
    }   // ����ȿ�� ���� ������Ƽ

    public bool isGameOver;         // ���� �� ���� ����
    public bool isBulletDestroy;    // ��� �Ѿ� ����

    [Header("������ ����Ʈ")]
    public GameObject[] items;

    string filePath;    // ���� ���
    public MainDB data;

    void Awake()
    {
        var objs = FindObjectsOfType<GameManager>();
        if (objs.Length == 1) DontDestroyOnLoad(gameObject);
        else Destroy(gameObject);

        GM = this;
        filePath = Application.persistentDataPath + "/MainDB.txt";
        Debug.Log(filePath);
        LoadData();

        GMCanvas = GameObject.Find("GMCanvas").transform;
        invinrator = InvincibilityControl();

        cargoHP = setCargoHP;

        palyerDamage = 15;
        enemyDamage = 10;
    }

    void Start()
    {
        
    }

    void Update()
    {
        // ���Ȯ�ο�
        if (Input.GetKeyDown(KeyCode.V))
        {
            Debug.Log("���Ȯ�ο� �Է�");
            SavaData();
        }
    }

    public void GameOver(string overType)
    {
        isGameOver = true;

        GameObject temp = Instantiate(overObject, GMCanvas.position, Quaternion.identity);
        temp.transform.SetParent(GMCanvas);
        temp.transform.localScale = new Vector3(1, 1, 1);
        temp.GetComponent<GameOver>().overType.text = overType;

        temp.GetComponent<GameOver>().resultText.text =
            "Score : " + CommaText(score) +
            "\nrunTime : " + (int)runTime;
    }
    public void ResetGameData()
    {
        isGameOver = false;

        cargoHP = setCargoHP;

        score = 0;
        runTime = 0;

        palyerDamage = 15;
        enemyDamage = 10;

        gameLevel = 1;
    }

    public void BulletDestroy()
    {
        isBulletDestroy = true;

        StopCoroutine("OnBullet");
        StartCoroutine(OnBullet());
    }   // �Ѿ� ���� �ѱ�
    IEnumerator OnBullet()
    {
        yield return new WaitForSeconds(0.1f);

        isBulletDestroy = false;
        yield return null;
    }   // �Ѿ� ���� ����

    public string CommaText(uint Num) 
    {
        if (Num == 0) return "0";
        return string.Format("{0:#,###}", Num); 
    }


    [System.Serializable]
    public class MainDB
    {
        public Rank[] RankDB;
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

        data.RankDB = new Rank[8];
        for (int i = 0; i < data.RankDB.Length; i++)
            data.RankDB[i].name = "Null";
    }


    [System.Serializable]
    public struct Rank
    {
        public string name;
        public uint score;
    }

    public void LineUpRank(string name, uint score)
    {
        Rank inputRank = new Rank { name = name, score = score };

        for (int i = 0; i < data.RankDB.Length; i++)
        {
            if (data.RankDB[i].score > inputRank.score) continue;
            else
            {
                Rank temp = data.RankDB[i];
                data.RankDB[i] = inputRank;
                inputRank = temp;
            }
        }
    }

    IEnumerator InvincibilityControl()
    {
        isInvincibility = true;
        invincObj.SetActive(true);

        yield return new WaitForSeconds(5f);

        isInvincibility = false;
        invincObj.SetActive(false);
    }
}
