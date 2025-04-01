using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Round : Singleton<Round>
{
    [SerializeField] Queue<int> currentStages = new Queue<int>();

    // ���� ���� 
    [SerializeField] int currentStage;

    // ���� �������� ���� ����
    [SerializeField] int[] stageScores = new int[3];


    public int[] stages
    {
        get { return stageScores; }
    }


    // ------- ���� �������� ��ǥ ���ھ� -------------------
    [SerializeField] int currentScores;

    public int CurrentScores
    {
        get { return currentScores; }
    }


    // ----------- Ȱ��ȭ �� ��ư ---------------------------
    [SerializeField] Button[] stagebuttons; 


    

    protected override void Awake()
    {
        base.Awake();

        // ť ���� ����
        currentStages.Enqueue(300); // 300 450 600
        currentStages.Enqueue(800); // 800 1200 1600
        currentStages.Enqueue(1800);// 1800 2700 3600

        // ù ����
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
