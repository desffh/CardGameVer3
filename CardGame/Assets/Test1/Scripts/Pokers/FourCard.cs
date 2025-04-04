using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FourCard : IsStrightPlush, IPokerHandle
{
    public string pokerName => "�� ī��";

    public void PokerHandle(List<Card> cards, List<int> saveNum)
    {
        Dictionary<int, int> cardCount = CardCount.Hand(cards);
        

        //Ǯ �Ͽ콺, ��ī�� ó��
        if (cardCount.Count() == 2)
        {
            var lastElement = cardCount.LastOrDefault(); // ������ ���
            var firstElement = cardCount.FirstOrDefault(); // ó�� ���

            if (lastElement.Value == 4)
            {
                for (int i = 0; i < 4; i++)
                {
                    saveNum.Add(lastElement.Key);  // ��ī��
                }
            }
            else if (firstElement.Value == 4)
            {
                for (int i = 0; i < 4; i++)
                {
                    saveNum.Add(firstElement.Key);  // ��ī��
                }
            }
        }
    }
}
