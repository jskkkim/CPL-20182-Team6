using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataController : MonoBehaviour
{
    private static DataController instance;
    public static DataController GetInstance() // Instance 받아오기.
    {
        if (instance == null)
        {
            instance = FindObjectOfType<DataController>();

            if (instance == null)
            {
                GameObject container = new GameObject("DataController");

                instance = container.AddComponent<DataController>();
            }
        }
        return instance;
    }
    static private int score = 0;
    static private int stage = 0;
    static private float total_time = 0;

    void Awake() // DataController가 실행 됐을 때, 내부 PlayerPrefs에 저장 되어 있는 데이터들을 읽음
    {
        score = PlayerPrefs.GetInt("Score");
        stage = PlayerPrefs.GetInt("Stage");
        total_time = PlayerPrefs.GetFloat("Total_time");
    }

    public void Add_Score()
    {
        Debug.Log("점수획득!");
        score += 1;
        PlayerPrefs.SetInt("Score", score);
        PlayerPrefs.Save();
    }
    public int Get_score()
    {
        return score;
    }
    public void Set_Stage(int num)
    {
        PlayerPrefs.SetInt("Stage", num);
        PlayerPrefs.Save();
    }
    public int Get_Stage()
    {
        return stage;
    }
    public void Add_time(float time)
    {
        total_time += time;
        PlayerPrefs.SetFloat("Total_time", total_time);
        PlayerPrefs.Save();
    }
    public float Get_AVGTime()
    {
        return total_time / (float)score;
    }
}
