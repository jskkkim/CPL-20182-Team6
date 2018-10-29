using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanStop : MonoBehaviour {

    private bool result = true;
    private int stop_butterfly_index = 0;
    private Butterfly[] butterfly = new Butterfly[10];
    int i, j;
    // Use this for initialization
    void Start () {
        result = true;
	}

    // Update is called once per frame
    void Update() {
        string index = "";
        int count = 1;
        for (i = 1; i <= 2; i++)
        {
            for (j = 1; j <= 3; j++)
            {
                //모든 나비 객체 받아오기
                index = "Target" + count;
                butterfly[count] = GameObject.Find(index).GetComponent<Butterfly>();
                count++;
            }
        }

        // 아무것도 안멈춰 있는 상태
        if (!result)
        {
            for (i = 1; i < count; i++)
            {
                //멈춘나비 있으면 그 나비1개만 멈추고 result = false로 바꿈
                if (!butterfly[i].isMoving())
                {
                    butterfly[i].stop_animator();
                    stop_butterfly_index = i;
                    result = false;
                }
            }
        }
    }
    // 3초동안 보면 다시 움직이고 멈출수있는 변수 true로 바꿈
    public void Move_again()
    {
        butterfly[stop_butterfly_index].restart();
        result = true;
    }
    // 생명다된 나비가 멈출수 있는지? 1마리만 멈출수있어서
    public bool get_result()
    {
        return result;
    }

    public void set_Stop()
    {
        result = false;
    }
}
