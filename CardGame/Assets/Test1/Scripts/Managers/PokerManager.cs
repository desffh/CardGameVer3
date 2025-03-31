using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PokerManager : Singleton<PokerManager>
{
    [SerializeField] HandCardPoints deleteCardPoint;

    // ī�� Ÿ���� ���� ����Ʈ (�ִ� 5��)
    [SerializeField] public List<Card> CardIDdata = new List<Card>(5);

    [SerializeField] TextManager TextManager;

    // �����ص� ����
    public List<int> saveNum;

    // ���ڰ� ��� �����ϴ��� ������ ��ųʸ� (����, ��� �����ϴ���)
    [SerializeField] private Dictionary<int, int> dictionary;

    protected override void Awake()
    {
        base.Awake();

        saveNum = new List<int>();

        dictionary = new Dictionary<int, int>();
    }

    // ����Ʈ�� ���� ���ִٸ� ���డ�� ����
    private void Update()
    {
        if(CardIDdata.Count > 0)
        {
            GameManager.Instance.Execute();
        }
        else
        {
            GameManager.Instance.Finish();
            return;
        }
    }

    // ���� �������� ����Ʈ�� ���� (����������) / Card Ÿ���� �ޱ�
    public void SaveSuitIDdata(Card card)
    {
        CardIDdata.Add(card);

        // LinQ�޼��带 ����� ������������ (value �� (���� ����) ��������)
        CardIDdata = CardIDdata.OrderBy(x => x.itemdata.id).ToList();
    }

    public void RemoveSuitIDdata(Card card)
    {
        // ����Ʈ���� ���� 
        CardIDdata.Remove(card);
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

    // ��Ʈ����Ʈ���� Ȯ�� (ex 1 2 3 4 5)
    public bool isStraight()
    {
        for (int i = 1; i < CardIDdata.Count; i++)
        {
            // ����ī��� �ٷ� �� ī���� ���� ���̰� 1���� Ȯ��
            // �ϳ��� �ٸ��� �ٷ� false��ȯ
            if (CardIDdata[i].itemdata.id != CardIDdata[i - 1].itemdata.id + 1)
            {
                return false;
            }
        }
        return true;
    }

    // �÷������� Ȯ�� (ex ���̾� 5��)
    public bool isFlush()
    {
        // SuitŸ���� ������ ����
        string firstSuit = CardIDdata[0].itemdata.suit;
        for (int i = 0; i < CardIDdata.Count; i++)
        {
            // [0]��°�� hand �ε����� �ϳ��� �ٸ��� false��ȯ
            if (CardIDdata[i].itemdata.suit != firstSuit)
            {
                return false;
            }
        }
        return true;
    }

    // �ڵ��� ���� Ȯ��
    public void getHandType()
    {
        saveNum.Clear();
        HoldManager.Instance.PlusSum = 0;
        HoldManager.Instance.MultiplySum = 0;

        // ��ȯ�� ���� ī��Ʈ ����
        Dictionary<int, int> rankCount = Hand();

        bool flush = false;
        bool straight = false;

        if (CardIDdata.Count > 0)
        {
            flush = isFlush();
            straight = isStraight();
        }

        var lastElement = rankCount.LastOrDefault(); // ������ ���
        var firstElement = rankCount.FirstOrDefault(); // ó�� ���

         

        // ��Ʈ����Ʈ �÷��� �� �ξ� ��Ʈ����Ʈ �÷��� ó��
        if (CardIDdata.Count == 5)
        {
            if (straight && flush)
            {
                if (CardIDdata[0].itemdata.id == 10)
                {
                    // �ξ� ��Ʈ����Ʈ �÷���: 10, J, Q, K, A
                    saveNum.Add(lastElement.Key);  // �ξ� ��Ʈ����Ʈ �÷���
                    TextManager.PokerTextUpdate("�ξ� ��Ʈ����Ʈ �÷���");
                    HoldManager.Instance.PokerCalculate(150, 8);
                    //Debug.Log("�ξ� ��Ʈ����Ʈ �÷���");
                }
                else
                {
                    // ��Ʈ����Ʈ �÷���
                    for (int i = 0; i < CardIDdata.Count; i++)
                    {
                        saveNum.Add(CardIDdata[i].itemdata.id);
                    }
                    TextManager.PokerTextUpdate("��Ʈ����Ʈ �÷���");
                    HoldManager.Instance.PokerCalculate(100, 8);
                    //Debug.Log("��Ʈ����Ʈ �÷���");
                }
                return;
            }

            // �÷���
            if (flush)
            {
                for (int i = 0; i < CardIDdata.Count; i++)
                {
                    saveNum.Add(CardIDdata[i].itemdata.id);
                }
                TextManager.PokerTextUpdate("�÷���");
                HoldManager.Instance.PokerCalculate(35, 4);

                //Debug.Log("�÷���");
                return;
            }

            // ��Ʈ����Ʈ
            if (straight)
            {
                for (int i = 0; i < CardIDdata.Count; i++)
                {
                    saveNum.Add(CardIDdata[i].itemdata.id);
                }
                TextManager.PokerTextUpdate("��Ʈ����Ʈ");
                HoldManager.Instance.PokerCalculate(30, 4);

                //Debug.Log("��Ʈ����Ʈ");
                return;
            }

            // Ǯ �Ͽ콺, ��ī�� ó��
            if (rankCount.Count() == 2)
            {
                if (lastElement.Value == 4)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        saveNum.Add(lastElement.Key);  // ��ī��
                    }
                    TextManager.PokerTextUpdate("�� ī��");
                    HoldManager.Instance.PokerCalculate(60, 7);

                    //Debug.Log("��ī��");
                }
                else if (firstElement.Value == 4)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        saveNum.Add(firstElement.Key);  // ��ī��
                    }
                    TextManager.PokerTextUpdate("�� ī��");
                    HoldManager.Instance.PokerCalculate(60, 7);

                    //Debug.Log("��ī��");
                }
                else
                {
                    // Ǯ �Ͽ콺 (3��, 2�� ) ��� �ֱ� 
                    for (int i = 0; i < CardIDdata.Count; i++)
                    {
                        saveNum.Add(CardIDdata[i].itemdata.id);
                    }
                    TextManager.PokerTextUpdate("Ǯ �Ͽ콺");
                    HoldManager.Instance.PokerCalculate(40, 4);

                    //Debug.Log("Ǯ �Ͽ콺");
                }
                return;
            }
        }

        // Ʈ���� ó��
        if (rankCount.Values.Contains(3))
        {
            // 3�� �Ȱ��� �������� ������ ã��
            foreach (var item in rankCount.Where(x => x.Value == 3))
            {
                for (int i = 0; i < 3; i++)
                {
                    saveNum.Add(item.Key);
                }
            }
            TextManager.PokerTextUpdate("Ʈ����");
            HoldManager.Instance.PokerCalculate(30, 3);

            //Debug.Log("Ʈ����");
            return;
        }

        // ����� ó��
        if (rankCount.Values.Count(v => v == 2) == 2)
        {
            foreach (var item in rankCount.Where(x => x.Value == 2))
            {
                for (int i = 0; i < 2; i++)
                {
                    saveNum.Add(item.Key);
                }
            }
            TextManager.PokerTextUpdate("�� ���");
            HoldManager.Instance.PokerCalculate(20, 2);

            //Debug.Log("�� ���");
            return;
        }

        // ����� ó��
        if (rankCount.Values.Contains(2))
        {
            foreach (var item in rankCount.Where(x => x.Value == 2))
            {
                for (int i = 0; i < 2; i++)
                {
                    saveNum.Add(item.Key);
                }
            }
            TextManager.PokerTextUpdate("�� ���");
            HoldManager.Instance.PokerCalculate(10, 2);

            //Debug.Log("�� ���");
            return;
        }

        // ���� ī�� ó��
        if (CardIDdata.Count != 0)
        {
            saveNum.Add(lastElement.Key); // ���� ū ��
            TextManager.PokerTextUpdate("���� ī��");
            HoldManager.Instance.PokerCalculate(5, 1);

            //Debug.Log("���� ī��: " + lastElement.Key);
            return;
        }

        // ����Ʈ�� ����ִٸ� �ؽ�Ʈ�� �� ��
        if (CardIDdata.Count == 0)
        {
            TextManager.PokerTextUpdate("");
            TextManager.PokerUpdate(0, 0);
        }
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