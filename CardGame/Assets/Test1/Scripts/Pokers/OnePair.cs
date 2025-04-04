using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class OnePair : IPokerHandle
{
    public string pokerName => "�� ���";

    public void PokerHandle(List<Card> cards, List<int> saveNum)
    {
        Dictionary<int, int> cardCount = CardCount.Hand(cards);

        // ����� ó��
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
