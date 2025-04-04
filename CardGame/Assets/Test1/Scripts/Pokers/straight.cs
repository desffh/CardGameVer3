using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class straight : IsStrightPlush, IPokerHandle 
{
    public string pokerName => "스트레이트";

    public void PokerHandle(List<Card> cards, List<int> saveNum)
    {
        Dictionary<int, int> cardCount = CardCount.Hand(cards);

        if(isStraight(cards))
        {
             //스트레이트
             for (int i = 0; i < cards.Count; i++)
             {
                 saveNum.Add(cards[i].itemdata.id);
             }
        }
    }
}
