using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class fullHouse : IsStrightPlush, IPokerHandle
{
    public string pokerName => "Ǯ �Ͽ콺";

    public void PokerHandle(List<Card> cards, List<int> saveNum)
    {
        Dictionary<int, int> cardCount = CardCount.Hand(cards);

        if(cards.Count == 5)
        {
            //Ǯ �Ͽ콺, ��ī�� ó��
            if (cardCount.Count() == 2)
            {
                Debug.Log("Ǯ �Ͽ콺");
                var lastElement = cardCount.LastOrDefault(); // ������ ���
                var firstElement = cardCount.FirstOrDefault(); // ó�� ���

                 //Ǯ �Ͽ콺 (3��, 2�� ) ��� �ֱ� 
                 for (int i = 0; i < cards.Count; i++)
                 {
                     saveNum.Add(cards[i].itemdata.id);
                 }
            }
        }
        return;
    }

}
