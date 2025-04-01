using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Round : Singleton<Round>
{
    [SerializeField] Queue<int> currentStages = new Queue<int>();

    // 현재 라운드 
    [SerializeField] int currentStage;

    // 현재 스테이지 점수 셋팅
    [SerializeField] int[] stageScores = new int[3];


    public int[] stages
    {
        get { return stageScores; }
    }


    // ------- 현재 스테이지 목표 스코어 -------------------
    [SerializeField] int currentScores;

    public int CurrentScores
    {
        get { return currentScores; }
    }


    // ----------- 활성화 할 버튼 ---------------------------
    [SerializeField] Button[] stagebuttons; 


    

    protected override void Awake()
    {
        base.Awake();

        // 큐 점수 셋팅
        currentStages.Enqueue(300); // 300 450 600
        currentStages.Enqueue(800); // 800 1200 1600
        currentStages.Enqueue(1800);// 1800 2700 3600

        // 첫 셋팅
        ScoreSetting();

        for (int i = 0; i < stagebuttons.Length; i++)
        {
            stagebuttons[i].interactable = false;
        }

        stagebuttons[0].interactable = true;
    }

    public void ScoreSetting()
    {
        if (currentStages.Count > 0)
        {
            currentStage = currentStages.Dequeue();
            
            for (int i = 0; i < stageScores.Length; i++)
            {
                stageScores[i] = currentStage + (i * currentStage / 2);
            }
        }
    }

    public void Score(int stage)
    {
        currentScores = stageScores[stage];



        stagebuttons[stage].interactable = false;

        stagebuttons[(stage + 1) % 3].interactable = true;
    }

}
