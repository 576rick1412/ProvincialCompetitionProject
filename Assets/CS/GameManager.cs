using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GameManager : MonoBehaviour
{
    public static GameManager GM;

    [Header("데미지")]
    public int palyerDamage;
    public int enemyDamage;

    [Header("카고")]
    public GameObject cargo;
    public float cargoBeamDamage;
    public float setCargoHP;
    public float cargoHP;

    [Header("게임결과창")]
    public uint score;              // 현재 게임 점수
    public float runTime;           // 게임 진행 시간

    [Header("게임오버")]
    Transform GMCanvas;             // 게임오버가 들어갈 캔버스
    public GameObject overObject;   // 게임오버를 알려줄 오브젝트

    [Header("게임 관리용 변수")]
    public int gameLevel;           // 현재 게임 레벨
    public GameObject invincObj;    // 무적 UI
    public bool isInvincibility;    // 무적효과
    IEnumerator invinrator;         // 무적 코루틴 넣을곳
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
    }   // 무적효과 관리 프로퍼티

    public bool isGameOver;         // 참일 시 게임 멈춤
    public bool isBulletDestroy;    // 모든 총알 제거

    [Header("아이템 리스트")]
    public GameObject[] items;

    string filePath;    // 저장 경로
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
        // 기능확인용
        if (Input.GetKeyDown(KeyCode.V))
        {
            Debug.Log("기능확인용 입력");
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
    }   // 총알 제거 켜기
    IEnumerator OnBullet()
    {
        yield return new WaitForSeconds(0.1f);

        isBulletDestroy = false;
        yield return null;
    }   // 총알 제거 끄기

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
    }   // Json 저장
    public void LoadData()
    {
        if (!File.Exists(filePath)) { ResetMainDB(); return; }

        var load = File.ReadAllText(filePath);
        data = JsonUtility.FromJson<MainDB>(load);
    }   // Json 로딩

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
