using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI PokerText;

    [SerializeField] TextMeshProUGUI PlusText;
    [SerializeField] TextMeshProUGUI MultiplyText;

    [SerializeField] TextMeshProUGUI TotalScoreText;

    [SerializeField] AnimationManager animationmanager;

    private RectTransform plusTextPosition;
    private RectTransform MultiTextPosition;

    [SerializeField] TextMeshProUGUI HandText;
    [SerializeField] TextMeshProUGUI DeleteText;

    [SerializeField] TextMeshProUGUI TotalCards;
    [SerializeField] TextMeshProUGUI HandCards;

    [SerializeField] TextMeshProUGUI handCount;
    [SerializeField] TextMeshProUGUI deleteCount;


    // 카드 위에 뜰 개별 텍스트 -> 하나의 프리팹으로 숫자만 바꿔서 쓰자
    //[SerializeField] GameObject Indexscore;


    private void Awake()
    {

    }

    private void Start()
    {
        HandText.text = HandDelete.Instance.Hand.ToString();
        DeleteText.text = HandDelete.Instance.Delete.ToString();

        // 일단 한번 호출해서 UI 싹 갱신
        BufferUpdate();
        HandCardUpdate();
    }

    private void Update()
    {
        HandCardUpdate();
    }


    // 리스트에 아무 값도 들어있지 않으면 빈 값 ""
    public void PokerTextUpdate(string pokertext = "")
    {
        PokerText.text = pokertext;
    }
    // 더하기
    public void PlusTextUpdate(int plussum = 0)
    {
        PlusText.text = plussum.ToString();
        animationmanager.CaltransformAnime(PlusText);
    }

    // 곱하기
    public void MultipleTextUpdate(int multisum = 0)
    {
        MultiplyText.text = multisum.ToString();
        animationmanager.CaltransformAnime(MultiplyText);

    }

    // 족보가 완성되면 호출
    public void PokerUpdate(int plus, int multiple)
    {
        PlusText.text = plus.ToString();
        MultiplyText.text = multiple.ToString();
    }

    // 전체 점수
    public void TotalScoreUpdate(int totalscore = 0)
    {
        TotalScoreText.text = totalscore.ToString();
        animationmanager.CaltransformAnime(TotalScoreText);
    }

    public void HandCountUpdate(int handcount)
    {
        HandText.text = handcount.ToString();
        animationmanager.CaltransformAnime(HandText);
    }

    public void DeleteCountUpdate(int deletecount)
    {
        DeleteText.text = deletecount.ToString();
        animationmanager.CaltransformAnime(DeleteText);
    }

    public void BufferUpdate()
    {
        TotalCards.text = KardManager.Instance.itemBuffer.Count.ToString()
            + " / "+ KardManager.Instance.itemBuffer.Capacity.ToString();
    }

    public void HandCardUpdate()
    {
        HandCards.text = (KardManager.Instance.myCards.Capacity - PokerManager.Instance.CardIDdata.Count).ToString() + " / "
            + KardManager.Instance.myCards.Capacity.ToString();
    }

    public void HandCount(int hand)
    {
        handCount.text = hand.ToString();
        animationmanager.CaltransformAnime(handCount);
    }

    public void DeleteCount(int delete)
    {
        deleteCount.text = delete.ToString();
        animationmanager.CaltransformAnime(deleteCount);
    }

}
