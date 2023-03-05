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

    [Header("����")]
    public uint score;   // ���� ���� ����

    string filePath;    // ���� ���
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

    public string CommaText(uint Num) { return string.Format("{0:#,###}", Num); }

    [System.Serializable]
    public class MainDB
    {

    }
}
