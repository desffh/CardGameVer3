using System.Collections.Generic;

public interface IPokerHandle
{
    void PokerHandle(List<Card> cards, List<int> saveNum);
    string pokerName { get; }
}


public abstract class IsStrightPlush
{
    // 스트레이트인지 확인 (ex 1 2 3 4 5)
    public bool isStraight(List<Card> cards)
    {
        if(cards.Count == 5)
        {
            for (int i = 1; i < cards.Count; i++)
            {
                // 현재카드와 바로 앞 카드의 숫자 차이가 1인지 확인
                // 하나라도 다르면 바로 false반환
                if (cards[i].itemdata.id != cards[i - 1].itemdata.id + 1)
                {
                    return false;
                }
            }
        }
        return true;
    }
    
    // 플러시인지 확인 (ex 다이아 5개)
    public bool isFlush(List<Card> cards)
    {
        if(cards.Count == 5)
        {
            // Suit타입을 저장할 변수
            string firstSuit = cards[0].itemdata.suit;
            for (int i = 0; i < cards.Count; i++)
            {
                // [0]번째와 hand 인덱스가 하나라도 다르면 false반환
                if (cards[i].itemdata.suit != firstSuit)
                {
                    return false;
                }
            }
        }
        return true;
    }
}


