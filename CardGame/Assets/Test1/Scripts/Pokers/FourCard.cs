using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class FourCard : IsStrightPlush, IPokerHandle
{
    public string pokerName => "�� ī��";

    public void PokerHandle(List<Card> cards, List<int> saveNum)
    {
        Dictionary<int, int> cardCount = CardCount.Hand(cards);


        //��ī�� ó��

        if (cards.Count == 5)
        {
            if (cardCount.Count() == 2)
            {
                var lastElement = cardCount.LastOrDefault(); // ������ ���
                var firstElement = cardCount.FirstOrDefault(); // ó�� ���

                if (cardCount.Count() == 2)
                {
                    if (lastElement.Value == 4)
                    {
                        for (int i = 0; i < 4; i++)
                        {
                            saveNum.Add(lastElement.Key);  // ��ī��
                        }

                        Debug.Log("��ī��");

                    }
                    else if (firstElement.Value == 4)
                    {
                        for (int i = 0; i < 4; i++)
                        {
                            saveNum.Add(firstElement.Key);  // ��ī��
                        }

                        Debug.Log("��ī��");
                    }

                }
            }
        }
        else if (cardCount.Values.Contains(4))
        {
            Debug.Log("��ī��");

            // 4�� �Ȱ��� �������� ������ ã��
            foreach (var item in cardCount.Where(x => x.Value == 4))
            {
                for (int i = 0; i < 4; i++)
                {
                    saveNum.Add(item.Key);
                }
            }
        }
        return;
    }
}
