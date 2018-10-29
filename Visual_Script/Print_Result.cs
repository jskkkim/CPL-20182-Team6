using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Print_Result : MonoBehaviour {
    private DataController DataController;
    public Text stage;
    public Text accuracy;
    public Text res_time;
    // Use this for initialization
    void Start () {
        DataController = DataController.GetInstance();
        stage = GameObject.Find("Text_Stage").GetComponent<Text>();
        accuracy = GameObject.Find("Text_Accuracy").GetComponent<Text>();
        res_time = GameObject.Find("Text_ResponseTime").GetComponent<Text>();
        this.print();
    }

    void print ()
    {
        stage.text = DataController.Get_Stage().ToString();
        //accuracy.text = DataController.Get;
        res_time.text = DataController.Get_AVGTime().ToString() + "s";
        Debug.Log(stage.text);
        //Debug.Log(accuracy.text);
        Debug.Log(res_time.text);

        //Data 초기화
        PlayerPrefs.SetInt("Score", 0);
        PlayerPrefs.SetInt("Stage", 0);
        PlayerPrefs.SetFloat("Total_time", 0);
        PlayerPrefs.Save();
    }
	
}
