using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetScore : MonoBehaviour {

    private int score = 0;
    private float[] times = new float[100];
    private int count = 0;

    private void Start()
    {
        times.Initialize();
    }
    public void plus () {
        Debug.Log("점수획득!");
        score += 1;
	}


    public int get_score()
    {
        return score;
    }


    public void store_time(float time)
    {
        times[count++] = time;
    }
	
    public float Get_avg_time()
    {
        int i = 0;
        float sum = 0;
        
        for(i=0;times[i]!=0;i++)
        {
            sum += times[i];
        }

        return sum / (float)(i + 1);
    }
}
