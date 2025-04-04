using System.Collections.Generic;

public interface IPokerHandle
{
    void PokerHandle(List<Card> cards, List<int> saveNum);
    string pokerName { get; }
}


public abstract class IsStrightPlush
{
    // ��Ʈ����Ʈ���� Ȯ�� (ex 1 2 3 4 5)
    public bool isStraight(List<Card> cards)
    {
        if(cards.Count == 5)
        {
            for (int i = 1; i < cards.Count; i++)
            {
                // ����ī��� �ٷ� �� ī���� ���� ���̰� 1���� Ȯ��
                // �ϳ��� �ٸ��� �ٷ� false��ȯ
                if (cards[i].itemdata.id != cards[i - 1].itemdata.id + 1)
                {
                    return false;
                }
            }
        }
        return true;
    }
    
    // �÷������� Ȯ�� (ex ���̾� 5��)
    public bool isFlush(List<Card> cards)
    {
        if(cards.Count == 5)
        {
            // SuitŸ���� ������ ����
            string firstSuit = cards[0].itemdata.suit;
            for (int i = 0; i < cards.Count; i++)
            {
                // [0]��°�� hand �ε����� �ϳ��� �ٸ��� false��ȯ
                if (cards[i].itemdata.suit != firstSuit)
                {
                    return false;
                }
            }
        }
        return true;
    }
}


