using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextManager : Singleton<TextManager>
{
    // ���� UI Ŭ������ �迭�� ����
    [SerializeField] private NumTextUpdater[] uiUpdaters;


    // �ؽ�Ʈ UI ������Ʈ ȣ��
    public void UpdateText(int index, int value = 0)
    {
        if (index >= 0 && index < uiUpdaters.Length)
        {
            uiUpdaters[index].UpdateText(value);  // �ε����� ���� �����Ͽ� �ؽ�Ʈ ����
        }
        else
        {
            Debug.LogWarning("Invalid UI index: " + index);
        }
    }



    // |-------------------------------------------------

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

   private void Start()
   {
       HandText.text = HandDelete.Instance.Hand.ToString();
       DeleteText.text = HandDelete.Instance.Delete.ToString();
   
       // �ϴ� �ѹ� ȣ���ؼ� UI �� ����
       BufferUpdate();
       HandCardUpdate();
   }

    private void Update()
    {
        HandCardUpdate();
    }


    // ����Ʈ�� �ƹ� ���� ������� ������ �� �� ""
    public void PokerTextUpdate(string pokertext = "")
    {
        PokerText.text = pokertext;
    }
   // ���ϱ�
   public void PlusTextUpdate(int plussum = 0)
   {
       PlusText.text = plussum.ToString();
       animationmanager.CaltransformAnime(PlusText);
   }

   // ���ϱ�
   public void MultipleTextUpdate(int multisum = 0)
   {
       MultiplyText.text = multisum.ToString();
       animationmanager.CaltransformAnime(MultiplyText);
   
   }

    // ������ �ϼ��Ǹ� ȣ��
    public void PokerUpdate(int plus, int multiple)
    {
        PlusText.text = plus.ToString();
        MultiplyText.text = multiple.ToString();
    }

    // ��ü ����
    public void TotalScoreUpdate(int totalscore = 0)
    {
        TotalScoreText.text = totalscore.ToString();
        animationmanager.CaltransformAnime(TotalScoreText);
    }

    //public void HandCountUpdate(int handcount)
    //{
    //    HandText.text = handcount.ToString();
    //    animationmanager.CaltransformAnime(HandText);
    //}
    //
    //public void DeleteCountUpdate(int deletecount)
    //{
    //    DeleteText.text = deletecount.ToString();
    //    animationmanager.CaltransformAnime(DeleteText);
    //}

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
