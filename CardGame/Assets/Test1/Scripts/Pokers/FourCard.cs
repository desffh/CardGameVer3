using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FourCard : IsStrightPlush, IPokerHandle
{
    public string pokerName => "포 카드";

    public void PokerHandle(List<Card> cards, List<int> saveNum)
    {
        Dictionary<int, int> cardCount = CardCount.Hand(cards);
        

        //풀 하우스, 포카드 처리
        if (cardCount.Count() == 2)
        {
            var lastElement = cardCount.LastOrDefault(); // 마지막 요소
            var firstElement = cardCount.FirstOrDefault(); // 처음 요소

            if (lastElement.Value == 4)
            {
                for (int i = 0; i < 4; i++)
                {
                    saveNum.Add(lastElement.Key);  // 포카드
                }
            }
            else if (firstElement.Value == 4)
            {
                for (int i = 0; i < 4; i++)
                {
                    saveNum.Add(firstElement.Key);  // 포카드
                }
            }
        }
    }
}
