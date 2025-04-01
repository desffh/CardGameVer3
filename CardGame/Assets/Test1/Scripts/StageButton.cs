using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class StageButton : MonoBehaviour
{

    [SerializeField] Button button;

    [SerializeField] Button nextEnty;

    [SerializeField] Canvas stagecanvas;

    [SerializeField] Canvas Entycanvas;

    private void Awake()
    {
        button = GetComponent<Button>();
    }

    public void OnClick1()
    {
        Stage1Click(0);
    }
    public void OnClick2()
    {
        Stage1Click(1);
    }
    public void OnClick3()
    {
        Stage1Click(2);
    }
    public void Stage1Click(int stage)
    {
        // ���� �������� ��ǥ ���� ����
        Round.Instance.Score(stage);
        
        // �������� �÷��� ����
        GameManager.Instance.PlayOn();
        
        // �������� â ��Ȱ��ȭ
        stagecanvas.gameObject.SetActive(false);

        // �ʱ�ȭ
        KardManager.Instance.SetupNextStage();
    }


    // -------------------- ĳ�� �ƿ� ��ư -----------------------

    public void NextEntyOn()
    {
        Entycanvas.gameObject.SetActive(false);
        stagecanvas.gameObject.SetActive(true);


        if(Round.Instance.CurrentScores == Round.Instance.stages[2])
        {
            Round.Instance.ScoreSetting();
        }

    }

    public void NextEntyOff()
    {
        nextEnty.interactable = false;
    }
}
