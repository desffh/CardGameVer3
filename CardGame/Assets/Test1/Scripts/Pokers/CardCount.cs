using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CardCount
{
    // ����� ��� ī�带 ��ȸ (�ִ� 5��)
    static public Dictionary<int, int> Hand(List<Card> cards)
    {
        Dictionary<int,int> cardCount = new Dictionary<int, int>();

        for (int i = 0; i < cards.Count; i++)
        {
            if (cardCount.ContainsKey(cards[i].itemdata.id))
            {
                cardCount[cards[i].itemdata.id]++;
            }
            else
            {
                cardCount[cards[i].itemdata.id] = 1;
            }
        }
        return cardCount;
    }

}
