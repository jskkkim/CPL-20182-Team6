using System.Collections;
using System.Collections.Generic;
using Tobii.Gaming;
using UnityEngine;
using System.Diagnostics;
using debug = UnityEngine.Debug;

public class Butterfly : MonoBehaviour {
    private bool isMove, c;
    private string str;
    private int life;

    GameObject child; //하위에 있는 Animator 받기위한 변수
    private GazeAware _gazeAwareComponent;
    private CanStop _check;
    private DataController DataController;

    Stopwatch sw = new Stopwatch(); //반응시간 체크타이머
    Stopwatch gazetime = new Stopwatch(); //응시시간 체크타이머
    private int need_gazetime = 3;//최소응시시간(seconds)

    public int min_flytime, max_flytime;

	void Start () {
        _gazeAwareComponent = GetComponent<GazeAware>();
        _check = GameObject.Find("Content").GetComponent<CanStop>(); //나비가 멈춰도 되는지 검사하는 오브젝트
        DataController = DataController.GetInstance();
        c = _check.get_result(); //누가 멈춰있는지 체크, 어떤나비가 멈춰있으면 true

        isMove = true; //어떤나비라도 멈춰있으면 비행시간 남아있어도 false가 되지못함.

        child = transform.GetChild(0).gameObject; //하위에있는 Animator 오브젝트
        life = Random.Range(min_flytime, max_flytime); // 비행시간 랜덤
	}


	void Update () {

        //비행체력 남아있을경우 계속 감소
        if (life > 0) {
            life--;
        }
        
        //비행체력이 0인데 다른나비가 멈춰있어서 isMove = true인 경우
        else if(isMove && life == 0)
        {
            c = _check.get_result(); // 멈출수 있는 상황인지?

            // 멈출수있는 상황이라면 isMove를 false로 설정함.
            // 그 후 멈췄으니 다른나비는 못멈추게 set_Stop()호출한 후 c에 false저장
            if (c && isMove)
            {
                isMove = false;
                _check.set_Stop();
                c = _check.get_result();
            }
            
            //다른나비 날고 있을 경우에는 계속 난다
            else
            {
                this.Start();
            }   
        }

        //비행체력이 0이 된 후에, 멈춰도 된다고 함수호출로 확인한 경우
        else if (!isMove)
        {
            sw.Start(); // 멈추면 스탑워치 시작
            if (_gazeAwareComponent.HasGazeFocus) //응시 시작
            {
                gazetime.Start();// 응시시간 기록

                // 3초이상 응시하면
                if (gazetime.ElapsedMilliseconds / 1000 >= need_gazetime)
                {
                    sw.Stop(); //물체다시 움직이기전에 스탑워치 스탑
                    DataController.Add_time((float)(sw.ElapsedMilliseconds-3000) / (float)1000);
                    sw.Reset(); //시간 초기화
                    DataController.Add_Score();
                    _check.Move_again(); // 다시 멈출수 있는 상태로 만들기
                    c = _check.get_result(); // 다시 상태받기
                    this.restart(); // 나비 다시 움직임
                }

            }
            else
                gazetime.Reset(); // 중간에 시선떼면 초기화
        }
    }


    public bool isMoving()
    {
        return this.isMove;
    }
    public void stopmovestate()
    {
        this.isMove = false;
    }
    public void restart()
    {
        child.GetComponent<Animator>().speed = 1;
        Start();
    }
    public Animator get_animator()
    {
        return child.GetComponent<Animator>();
    }
    public void stop_animator()
    {
        child.GetComponent<Animator>().speed = 0;
    }

}
