using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public System.Action OnStageChanged; // 스테이지 클리어 시

    [SerializeField] private bool hold;

    protected override void Awake()
    {
        base.Awake();
        OnStageChanged += KardManager.Instance.SetupNextStage; // 여기서 이벤트 등록
    }



    // 핸드 & 버리기 버튼 누르면 계산 과정 실행 관련 
    public bool Hold
    {
        get { return hold; }
    }


    // 해당 엔티 실행 가능 
    public void Execute()
    {
        hold = true;
    }
    
    // 해당 엔티 종료
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

    // 새 스테이지 시작될때 마다 이벤트 실행
    public void StateOn()
    {
        state = true;


    }



    // |-스테이지 플레이 상태 (스테이지1)-----------------------------------------------|

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
