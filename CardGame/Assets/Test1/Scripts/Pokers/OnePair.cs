using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class OnePair : IPokerHandle
{
    public string pokerName => "盔 其绢";

    public void PokerHandle(List<Card> cards, List<int> saveNum)
    {
        Dictionary<int, int> cardCount = CardCount.Hand(cards);

        // 捧其绢 贸府
        if (cardCount.Values.Contains(2))
        {
            foreach (var item in cardCount.Where(x => x.Value == 2))
            {
                for (int i = 0; i < 2; i++)
                {
                    saveNum.Add(item.Key);
                }
            }
        }
    }
}
