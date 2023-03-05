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

    [Header("카고 체력")]
    public float setCargoHP;
    public float cargoHP;

    [Header("점수")]
    public uint score;   // 현재 게임 점수

    [Header("게임오버")]
    public Transform mainCanvas;    // 게임오버가 들어갈 캔버스
    public GameObject overObject;   // 게임오버를 알려줄 오브젝트
    
    [Header("게임 관리용 변수")]
    public bool isGameOver;         // 참일 시 게임 멈춤
    public bool isBulletDestroy;    // 모든 총알 제거

    string filePath;    // 저장 경로
    public MainDB data;

    void Awake()
    {
        GM = this;
        filePath = Application.persistentDataPath + "/MainDB.txt";
    }

    void Start()
    {
        cargoHP = setCargoHP;
    }

    void Update()
    {
        // 수송선 체력 없을 시 게임오버
        if (cargoHP <= 0) GameOver("Cargo Destroy\nmission failed");
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
    }

    public void GameOver(string overType)
    {
        isGameOver = true;

        GameObject temp = Instantiate(overObject, mainCanvas.position, Quaternion.identity);
        temp.transform.SetParent(mainCanvas);
        temp.transform.localScale = new Vector3(1, 1, 1);
        temp.GetComponent<GameOver>().overType.text = overType;
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
    }   // 총알 제거 켜기

    public string CommaText(uint Num) { return string.Format("{0:#,###}", Num); }

    [System.Serializable]
    public class MainDB
    {

    }
}
