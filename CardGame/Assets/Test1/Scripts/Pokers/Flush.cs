using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Flush : IsStrightPlush, IPokerHandle
{
    public string pokerName => "�÷���";

    public void PokerHandle(List<Card> cards, List<int> saveNum)
    {
        Dictionary<int, int> cardCount = CardCount.Hand(cards);

        if (cards.Count == 5)
        {
            if (isFlush(cards))
            {
                Debug.Log("�÷���");
                for (int i = 0; i < cards.Count; i++)
                {
                    saveNum.Add(cards[i].itemdata.id);
                }
            }
        }
        return;
    }
}
