using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class PokerManager : Singleton<PokerManager>
{
    private List<IPokerHandle> pokerHands;
    
    // �����ص� ����
    [SerializeField] public List<int> saveNum;
    
    // |------------------------------------------------

    [SerializeField] HandCardPoints deleteCardPoint;

    // ī�� Ÿ���� ���� ����Ʈ (�ִ� 5��)
    [SerializeField] public List<Card> CardIDdata = new List<Card>(5);

    [SerializeField] TextManager TextManager;


    // ���ڰ� ��� �����ϴ��� ������ ��ųʸ� (����, ��� �����ϴ���)
    [SerializeField] private Dictionary<int, int> dictionary;

    protected override void Awake()
    {
        base.Awake();

        saveNum = new List<int>();

        dictionary = new Dictionary<int, int>();
    }

    public PokerManager()
    {
        // ���� ����Ʈ �ʱ�ȭ
        pokerHands = new List<IPokerHandle>
        {
            new RoyalStraightFlush(),
            new FourCard(),
            new fullHouse(),
            new Flush(),
            new straight(),
            new Triple(),
            new TwoPair(),
            new OnePair(),
            new HighCard()
        };
        saveNum = new List<int>();  // saveNum ����Ʈ�� ���⼭ �� ���� ����
    }

    // ī�� ����Ʈ�� ���Ͽ� ���� �̸��� ������ ���
    public void EvaluatePokerHands(List<Card> cards)
    {
        saveNum.Clear();

        foreach (var hand in pokerHands)
        {
            List<int> tempSaveNum = new List<int>(); // �ӽ� ���� ����Ʈ

            hand.PokerHandle(cards, tempSaveNum);

            if (tempSaveNum.Count > 0) // ù ��°�� ��Ī�Ǵ� ������ ����
            {
                saveNum = tempSaveNum;
                Debug.Log($"���� ���õ�: {hand.pokerName}");
                break;
            }
        }
    }

    // ���� �������� ����Ʈ�� ���� (����������) / Card Ÿ���� �ޱ�
    public void SaveSuitIDdata(Card card)
    {
        CardIDdata.Add(card);

        // LinQ�޼��带 ����� ������������ (value �� (���� ����) ��������)
        CardIDdata = CardIDdata.OrderBy(x => x.itemdata.id).ToList();

        if (CardIDdata.Count >= 0)
        {
            EvaluatePokerHands(CardIDdata);
            TextManager.Instance.UpdateText(1, 10);
            TextManager.Instance.UpdateText(2, 10);

        }
    }

    public void RemoveSuitIDdata(Card card)
    {
        // ����Ʈ���� ���� 
        CardIDdata.Remove(card);

        if (CardIDdata.Count > 0)
        {
            EvaluatePokerHands(CardIDdata);
        }
        else
        {
            // ī�尡 �� ���ŵ� ��쿡�� saveNum �ʱ�ȭ
            saveNum.Clear();
            Debug.Log("��� ī�尡 ���ŵǾ� saveNum �ʱ�ȭ��.");
            TextManager.Instance.UpdateText(1);
            TextManager.Instance.UpdateText(2);
        }

    }


    // ����� ��� ī�带 ��ȸ (�ִ� 5��)
    public Dictionary<int, int> Hand()
    {
        dictionary.Clear();

        for (int i = 0; i < CardIDdata.Count; i++)
        {
            if (dictionary.ContainsKey(CardIDdata[i].itemdata.id))
            {
                dictionary[CardIDdata[i].itemdata.id]++;
            }
            else
            {
                dictionary[CardIDdata[i].itemdata.id] = 1;
            }
        }
        return dictionary;
    }

    
    public void QuitCollider2()
    {
        for (int i = 0; i < CardIDdata.Count; i++)
        {
            CardIDdata[i].gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    // �� �Ǻ��ϰ� ������
    public void DeleteMove()
    {
        for (int i = 0; i < CardIDdata.Count; i++)
        {
            CardIDdata[i].transform.GetComponent<Transform>();

            CardIDdata[i].transform.
                DOMove(deleteCardPoint.DeleteCardpos.transform.position, 1).SetDelay(i * 0.2f);

            CardIDdata[i].transform.
                DORotate(new Vector3(-45, -60, -25), 0.5f).SetDelay(i * 0.2f);
        }
    }

    public void DelaySetActive()
    {
        for (int i = 0; i <CardIDdata.Count; i++)
        {
            CardIDdata[i].gameObject.SetActive(false);
        }
    }
}