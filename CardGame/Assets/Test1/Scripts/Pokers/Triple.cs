using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Triple : IPokerHandle
{
    public string pokerName => "Ʈ����";

    public void PokerHandle(List<Card> cards, List<int> saveNum)
    {
        Dictionary<int, int> cardCount = CardCount.Hand(cards);
    
        //Ʈ���� ó��
            if (cardCount.Values.Contains(3))
            {
                // 3�� �Ȱ��� �������� ������ ã��
                foreach (var item in cardCount.Where(x => x.Value == 3))
                {
                    for (int i = 0; i < 3; i++)
                    {
                        saveNum.Add(item.Key);
                    }
                }
            }
    }
}
