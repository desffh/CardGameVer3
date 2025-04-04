using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TwoPair : IPokerHandle
{
    public string pokerName => "�� ���";

    public void PokerHandle(List<Card> cards, List<int> saveNum)
    {
        Dictionary<int, int> cardCount = CardCount.Hand(cards);

        // ����� ó��
        if (cardCount.Values.Count(v => v == 2) == 2)
        {
            foreach (var item in cardCount.Where(x => x.Value == 2))
            {
                for (int i = 0; i < 2; i++)
                {
                    saveNum.Add(item.Key);  // saveNum ����Ʈ�� ī�� ���� �߰�
                }
            }
        }
    }
}
