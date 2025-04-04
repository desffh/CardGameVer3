using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class straightFlush : IsStrightPlush, IPokerHandle
{
    public string pokerName => "��Ʈ����Ʈ �÷���";

    public void PokerHandle(List<Card> cards, List<int> saveNum)
    {
        Dictionary<int, int> cardCount = CardCount.Hand(cards);

        if (cards.Count == 5)
        {
            if (isStraight(cards) && isFlush(cards))
            {
                Debug.Log("��Ʈ����Ʈ �÷���");

                //��Ʈ����Ʈ �÷���
                for (int i = 0; i < cards.Count; i++)
                {
                    saveNum.Add(cards[i].itemdata.id);
                }
            }
        }
        return;

    }
}
