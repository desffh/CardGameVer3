using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Flush : IsStrightPlush, IPokerHandle
{
    public string pokerName => "ÇÃ·¯½¬";

    public void PokerHandle(List<Card> cards, List<int> saveNum)
    {
        Dictionary<int, int> cardCount = CardCount.Hand(cards);

        if (isFlush(cards))
        {
            for (int i = 0; i < cards.Count; i++)
                {
                    saveNum.Add(cards[i].itemdata.id);
                }
        }
    }
}
