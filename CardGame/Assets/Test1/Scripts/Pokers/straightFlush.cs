using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class straightFlush : IsStrightPlush, IPokerHandle
{
    public string pokerName => "스트레이트 플러쉬";

    public void PokerHandle(List<Card> cards, List<int> saveNum)
    {
        Dictionary<int, int> cardCount = CardCount.Hand(cards);

        if (isStraight(cards) && isFlush(cards))
        {
            //스트레이트 플러쉬
            for (int i = 0; i < cards.Count; i++)
            {
                saveNum.Add(cards[i].itemdata.id);
            }
        }
    }
}
