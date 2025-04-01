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




    // |-�������� �÷��� ���� (��������1)-----------------------------------------------|

    [SerializeField] bool playState;


    [SerializeField]
    public bool PlayState
    {
        get { return playState; }
    }

    public void PlayOn()
    {
        Debug.Log("PlayOn ȣ��");

        //playState = true;
        
        if (OnStageChanged != null)
        {
            OnStageChanged.Invoke();
        }
    }

    public void PlayOff()
    {
        Debug.Log("PlayOff ȣ��");

        //playState = false;

        StartCoroutine(PopUpManager.Instance.OnClearPopup());
    }
}
