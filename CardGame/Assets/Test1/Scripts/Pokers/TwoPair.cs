using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TwoPair : IPokerHandle
{
    public string pokerName => "투 페어";

    public void PokerHandle(List<Card> cards, List<int> saveNum)
    {
        Dictionary<int, int> cardCount = CardCount.Hand(cards);

        // 투페어 처리
        if (cardCount.Values.Count(v => v == 2) == 2)
        {
            foreach (var item in cardCount.Where(x => x.Value == 2))
            {
                for (int i = 0; i < 2; i++)
                {
                    saveNum.Add(item.Key);  // saveNum 리스트에 카드 숫자 추가
                }
            }
        }
    }
}
