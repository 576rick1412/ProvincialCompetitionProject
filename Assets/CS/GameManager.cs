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

    public string CommaText(uint Num) { return string.Format("{0:#,###}", Num); }

    [System.Serializable]
    public class MainDB
    {

    }
}
