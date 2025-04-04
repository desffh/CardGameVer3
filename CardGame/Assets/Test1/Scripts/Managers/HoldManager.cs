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
    // ��� �̺�Ʈ
    public UnityEvent calculation;

    public Action ActionSetting;


    WaitForSeconds waitForSeconds;

    // ���ڸ� ��� �ϳ��� ���� ���� ť
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
        actionQueue.Clear();  // ���� ť ����

        // ť�� �ʱ�ȭ�ϰų� �����ϰ�, �ʿ��� �۾��� �ٽ� �߰�
        //actionQueue.Enqueue(() => Setting());
        actionQueue.Enqueue(() => PlusCalculation()); // �� ���� ���ϱ�
        actionQueue.Enqueue(() => DelayedTotalScoreCal()); // ��ü ���ھ ����
        actionQueue.Enqueue(() => DelayedMove()); // �ϳ��� ���� ������ �̵�
        actionQueue.Enqueue(() => DelayActive()); // ��Ȱ��ȭ
        

        // ���ο� ī����� �������� �߰�
        // ��: PokerManager.Instance.NewCardsSpawn(); (�ʿ��� ���� �߰�)
    }

    public void StageEnd()
    {
          Debug.Log("�������� ����");
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // �����Ϳ��� �÷��� ��带 ����
#endif

    }


    // �̺�Ʈ : 1. ť�� �� �� �ֱ� 2. ��� �� �ؽ�Ʈ ���� 3. ī�� ���� �� ��Ȱ��ȭ
    public void Calculation()
    {
        calculation.Invoke();
        //Debug.Log("��� ����");
    }

    // ��ϵ� �̺�Ʈ
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

        // ��� �� �ϰ� ����Ʈ �ʱ�ȭ
        PokerManager.Instance.CardIDdata.Clear();
        PokerManager.Instance.saveNum.Clear();

        UIupdate();

        CheckReset();

        KardManager.Instance.AddCardSpawn();

        // �ٽ� �ݶ��̴� Ȱ��ȭ
        KardManager.Instance.card.OnCollider();
        ButtonManager.Instance.ButtonInactive();

        RefillActionQueue();
    }

    // ����� ������ �� & �ٽ� ī�尡 ��ġ�Ǵ� ��
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
            yield return waitForSeconds;  // ���
            TotalScoreCal();  // TotalScoreCal ����
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

        // ����Ʈ �ʱ�ȭ
        PokerManager.Instance.CardIDdata.Clear();
        PokerManager.Instance.saveNum.Clear();

        KardManager.Instance.AddCardSpawn();
        UIupdate();

        // �ٽ� �ݶ��̴� Ȱ��ȭ
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
            // ť���� ���鼭 üũ
            int saveNumber = Num.Dequeue();
            PlusSum += saveNumber;

            // �ִϸ��̼� ȣ��
            animationManager.PlayCardAnime(SaveNumber(saveNumber));
        }
        textManager.PlusTextUpdate(PlusSum);
    }
    
    // �ִϸ��̼��� ȣ���ϱ� ���� ���
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
                // ����Ʈ���� �����ع����� deleteZone���� �̵��� �� ����
                //PokerManager.Instance.SuitIDdata.RemoveAt(i);
                break;
            }
        }
        return game;
    }
    
    

    // �ִϸ��̼� �Ǵܿ��� bool�迭 �ʱ�ȭ
    public void CheckReset()
    {
        for(int i = 0; i < 5; i++)
        {
            savenumberCheck[i] = false;
        }
    }

    // ���� �� ����
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
        // ������ ���� ī���� �ݶ��̴� ��Ȱ��ȭ               
        KardManager.Instance.card.OffCollider();

        // ī�� ��Ȱ��ȭ & �ݶ��̴� Ȱ��ȭ 
        StartCoroutine(deleteCard());
    }
}
