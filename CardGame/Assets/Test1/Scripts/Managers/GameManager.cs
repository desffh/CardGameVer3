using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public System.Action OnStageChanged; // �������� Ŭ���� ��

    [SerializeField] private bool hold;

    protected override void Awake()
    {
        base.Awake();
        OnStageChanged += KardManager.Instance.SetupNextStage; // ���⼭ �̺�Ʈ ���
    }



    // �ڵ� & ������ ��ư ������ ��� ���� ���� ���� 
    public bool Hold
    {
        get { return hold; }
    }


    // �ش� ��Ƽ ���� ���� 
    public void Execute()
    {
        hold = true;
    }
    
    // �ش� ��Ƽ ����
    public void Finish()
    {
        hold = false;

    }

    // |--------------------------------------------|

    [SerializeField] private bool state = true;

    public bool State
    {
        get { return state; }
    }

    // �� �������� ���۵ɶ� ���� �̺�Ʈ ����
    public void StateOn()
    {
        state = true;


    }



    // |-�������� �÷��� ���� (��������1)-----------------------------------------------|

    [SerializeField] bool playState;

    [SerializeField]
    public bool PlayState
    {
        get { return playState; }
    }

    public void PlayOn()
    {
        playState = true;
        
        if (OnStageChanged != null)
        {
            OnStageChanged.Invoke();
        }
    }

    public void PlayOff()
    {
        playState = false;

        StartCoroutine(PopUpManager.Instance.OnClearPopup());
    }
}
