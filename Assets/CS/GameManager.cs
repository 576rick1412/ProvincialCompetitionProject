using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GameManager : MonoBehaviour
{
    public static GameManager GM;

    public int palyerDamage;
    public int enemyDamage;

    string filePath;    // 저장 경로
    public MainDB data;
    void Awake()
    {
        GM = this;
        filePath = Application.persistentDataPath + "/MainDB.txt";
    }

    void Start()
    {

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

    [System.Serializable]
    public class MainDB
    {

    }
}
