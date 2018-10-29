using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class timelimit : MonoBehaviour {

    private float LimitTime = 20f;
    public Text text_Timer;
    public Text Score;
    private DataController DataController;

    public Func<Scene> Scene { get; private set; }

    void Start () {
        DataController = DataController.GetInstance();
    }
	

	void Update () {
        LimitTime -= Time.deltaTime;
        text_Timer.text = "시간 : " + Mathf.Round(LimitTime*10)/10;
        Score.text = "점수 : " + DataController.Get_score();
        if(Mathf.Round(LimitTime) <= 0)
        {
            Debug.Log("평균반응시간 : " + DataController.Get_AVGTime());
            int stage = SceneManager.GetActiveScene().buildIndex;
            PlayerPrefs.SetInt("Stage", stage);
            PlayerPrefs.Save();
            SceneChange2();
        }
    }
    public void SceneChange2()
    {
        SceneManager.LoadScene("Result");
    }
}
