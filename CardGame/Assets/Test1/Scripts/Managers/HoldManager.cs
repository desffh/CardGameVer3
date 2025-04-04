using DG.Tweening;
using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class HoldManager : Singleton<HoldManager>
{
    // 계산 이벤트
    public UnityEvent calculation;

    public Action ActionSetting;


    WaitForSeconds waitForSeconds;

    // 숫자를 담고 하나씩 빼기 위한 큐
    private Queue<int> Num;

    [SerializeField] public int PlusSum;
    [SerializeField] public int MultiplySum;
    private int totalScore;

    [SerializeField] TextManager textManager;
    [SerializeField] AnimationManager animationManager;

    private Queue<Func<IEnumerator>> actionQueue = new Queue<Func<IEnumerator>>();

    protected override void Awake()
    {
        base.Awake();
        ActionSetting += Setting;
    }

    private void Start()
    {
        waitForSeconds = new WaitForSeconds(1.0f);

        PlusSum = 0;
        MultiplySum = 0;

        Num = new Queue<int>();

        RefillActionQueue();
    }

    public void RefillActionQueue()
    {
        actionQueue.Clear();  // 기존 큐 비우기

        // 큐를 초기화하거나 리셋하고, 필요한 작업을 다시 추가
        //actionQueue.Enqueue(() => Setting());
        actionQueue.Enqueue(() => PlusCalculation()); // 각 값을 더하기
        actionQueue.Enqueue(() => DelayedTotalScoreCal()); // 전체 스코어에 갱신
        actionQueue.Enqueue(() => DelayedMove()); // 하나씩 삭제 존으로 이동
        actionQueue.Enqueue(() => DelayActive()); // 비활성화
        

        // 새로운 카드들이 나오도록 추가
        // 예: PokerManager.Instance.NewCardsSpawn(); (필요한 로직 추가)
    }

    public void StageEnd()
    {
          Debug.Log("스테이지 종료");
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // 에디터에서 플레이 모드를 종료
#endif

    }


    // 이벤트 : 1. 큐에 값 다 넣기 2. 계산 후 텍스트 누적 3. 카드 날라간 뒤 비활성화
    public void Calculation()
    {
        calculation.Invoke();
        //Debug.Log("계산 시작");
    }

    // 등록된 이벤트
    public void CalSetting()
    {
        for (int i = 0; i < PokerManager.Instance.saveNum.Count; i++)
        {
            Num.Enqueue(PokerManager.Instance.saveNum[i]);
        }

        StartCoroutine(ExecuteActions());

        
        if (HandDelete.Instance.Hand <= 0)
        {
            StageEnd();
        }


    }

    private IEnumerator ExecuteActions()
    {

        while (actionQueue.Count > 0)
        {
            yield return StartCoroutine(actionQueue.Dequeue().Invoke());
        }


        if(ScoreManager.Instance.Calculation() == false)
        {
            ActionSetting.Invoke();
        }
    }

    public void Setting()
    {

        // 계산 다 하고 리스트 초기화
        PokerManager.Instance.CardIDdata.Clear();
        PokerManager.Instance.saveNum.Clear();

        UIupdate();

        CheckReset();

        KardManager.Instance.AddCardSpawn();

        // 다시 콜라이더 활성화
        KardManager.Instance.card.OnCollider();
        ButtonManager.Instance.ButtonInactive();

        RefillActionQueue();
    }

    // 계산이 끝나는 곳 & 다시 카드가 배치되는 곳
    IEnumerator DelayActive()
    {
        yield return waitForSeconds;
        PokerManager.Instance.DelaySetActive();
    }

    IEnumerator PlusCalculation()
    {
        while (true)
        {
            if(Num.Count <= 0)
            {
                yield break;
            }
            yield return waitForSeconds;

            Calculate();
        }
    }
    
    IEnumerator DelayedTotalScoreCal()
    {
        if(Num.Count == 0)
        {
            yield return waitForSeconds;  // 대기
            TotalScoreCal();  // TotalScoreCal 실행
        }
        
    }
    IEnumerator DelayedMove()
    {
        yield return waitForSeconds;
        PokerManager.Instance.DeleteMove();
    }
    

    // ---------------------------------------------


    IEnumerator deleteCard()
    {
        yield return waitForSeconds;
        PokerManager.Instance.DelaySetActive();

        // 리스트 초기화
        PokerManager.Instance.CardIDdata.Clear();
        PokerManager.Instance.saveNum.Clear();

        KardManager.Instance.AddCardSpawn();
        UIupdate();

        // 다시 콜라이더 활성화
        KardManager.Instance.card.OnCollider();
        ButtonManager.Instance.ButtonInactive();
        yield break;
    }

    // ---------------------------------------------


    public void TotalScoreCal()
    {

        totalScore = PlusSum * MultiplySum;

        ScoreManager.Instance.TotalScore = totalScore; 
       
        textManager.TotalScoreUpdate(ScoreManager.Instance.TotalScore);
    }
    

    public void UIupdate()
    {
        textManager.PokerTextUpdate();
        textManager.PlusTextUpdate();
        textManager.MultipleTextUpdate();
        textManager.BufferUpdate();
    }
    public void TotalScoreupdate()
    {
        textManager.TotalScoreUpdate();
    }
    
    public void Calculate()
    {
        if (Num.Count > 0)
        {
            // 큐에서 빼면서 체크
            int saveNumber = Num.Dequeue();
            PlusSum += saveNumber;

            // 애니메이션 호출
            animationManager.PlayCardAnime(SaveNumber(saveNumber));
        }
        textManager.PlusTextUpdate(PlusSum);
    }
    
    // 애니메이션을 호출하기 위해 사용
    private GameObject game;

    private bool[] savenumberCheck = new bool[5];
    public GameObject SaveNumber(int saveNumber)
    {
        for (int i = 0; i < PokerManager.Instance.CardIDdata.Count; i++)
        {
            if (savenumberCheck[i] == false && PokerManager.Instance.CardIDdata[i].itemdata.id == saveNumber)
            {
                game = PokerManager.Instance.CardIDdata[i].gameObject;

                savenumberCheck[i] = true;
                // 리스트에서 제거해버리면 deleteZone으로 이동할 수 없음
                //PokerManager.Instance.SuitIDdata.RemoveAt(i);
                break;
            }
        }
        return game;
    }
    
    

    // 애니메이션 판단여부 bool배열 초기화
    public void CheckReset()
    {
        for(int i = 0; i < 5; i++)
        {
            savenumberCheck[i] = false;
        }
    }

    // 족보 룰 점수
    public void PokerCalculate(int plus, int multiple)
    {
        if (PokerManager.Instance.CardIDdata.Count > 0)
        {
            PlusSum += plus;
            
            MultiplySum += multiple;
        }
        textManager.PokerUpdate(PlusSum, MultiplySum);
    }

    public void StartDeleteCard()
    {
        // 버리는 동안 카드의 콜라이더 비활성화               
        KardManager.Instance.card.OffCollider();

        // 카드 비활성화 & 콜라이더 활성화 
        StartCoroutine(deleteCard());
    }
}
