using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;


public class KardManager : Singleton<KardManager>
{
    // ����
    [SerializeField] ItemDataReader ItemDataReader;
    [SerializeField] public Card card;
    [SerializeField] Transform deleteSpawn;

    // ItemData Ÿ���� ���� List ����
    [SerializeField] public List<ItemData> itemBuffer;

    // Card Ÿ���� ���� ����Ʈ (�� ī��)
    [SerializeField] public List<Card> myCards;

    // ī�� ������ġ
    [SerializeField] Transform cardSpawnPoint; 

    // ī�� ���� ����, �� ��ġ
    [SerializeField] Transform myCardLeft;
    [SerializeField] Transform myCardRight;

    // �����Ϳ��� ī�� ������ ���� (Instantiate)
    [SerializeField] GameObject cardPrefabs;
    
    // �θ� ���� ������Ʈ �Ҵ� (��ġ�� ��ġ)
    [SerializeField] GameObject ParentCardPrefab;

    // ���ۿ� ī�� �ֱ�
    void SetupItemBuffer()
    {
        // ũ�� �����Ҵ�
        itemBuffer = new List<ItemData>(52);

        for (int i = 0; i < 52; i++)
        {
            // ������ �������� ��Ʈ ������ ���� ī����� itemBuffer�� ����
            ItemData item = ItemDataReader.DataList[i];

            itemBuffer.Add(item);
        }
        // ������ ���� ���� ī�� ����
        for (int i = 0; i < itemBuffer.Count; i++)
        {
            int rand = Random.Range(i, itemBuffer.Count);
            ItemData temp = itemBuffer[i];
            itemBuffer[i] = itemBuffer[rand];
            itemBuffer[rand] = temp;
        }
    }

    // ���ۿ��� ī�� �̱�
    public ItemData PopItem()
    {
        // �� �̾����� �ٽ� ���� ä��� 
        if (itemBuffer.Count == 0)
        {
            SetupItemBuffer();
        }

        ItemData item = itemBuffer[0];
        itemBuffer.RemoveAt(0); // ����Ʈ �޼��� (0��° ��� ����)
        return item;
    }

    // 8���� ��ġ�� ī�� �߰� 
    void AddCard()
    {
        if(myCards.Count < 8)
        {
            var cardObject = Instantiate(cardPrefabs, cardSpawnPoint.position, Utils.QI); // ���� ������Ʈ Ÿ��

            // �θ��� �Ʒ��� ���� (���̶�Űâ ��������)
            cardObject.transform.SetParent(ParentCardPrefab.transform);
        
            // ���� ������ī�� ������Ʈ
            var card = cardObject.GetComponent<Card>(); // ������ ī���� ��ũ��Ʈ �������� (Card)
            card.Setup(PopItem()); // ���� ī�忡 ItemData ���� ���� & ��������Ʈ ����

            // ���� ������ ī�� ������Ʈ�� Card ��ũ��Ʈ�� ������ �־ CardŸ�� ����Ʈ�� ���� �� �ִ�
            myCards.Add(card);
        }
        // ī�� ����
        SetOriginOrder();
        CardAlignment();
    }

    // ����Ʈ ��ü ���� (���� �߰��� ī�尡 ���� ���ʿ� ����)
    public void SetOriginOrder()
    {
        int count = myCards.Count;

        for (int i = 0; i < count; i++)
        {
            var targetCard = myCards[i];

            // ? -> targerCard�� null�� �ƴϸ� ������Ʈ ��������
            targetCard?.GetComponent<Order>().SetOriginOrder(i);
        }
    }
    
    // ī�� ����
    public void CardAlignment()
    {
        List<PRS> originCardPRSs = new List<PRS>();

        originCardPRSs = RoundAlignment(myCardLeft, myCardRight, myCards.Count, 0.5f, Vector3.one * 0.7f);

        var targetCards = myCards;

        for (int i = 0; i < targetCards.Count; i++)
        {
            var targetCard = targetCards[i];

            targetCard.originPRS = originCardPRSs[i];
            targetCard.MoveTransform(targetCard.originPRS, true, 0.7f);
        }
    }

    List<PRS> RoundAlignment(Transform leftTr, Transform rightTr, int objCount, float height, Vector3 scale)
    {
        float[] objLerps = new float[objCount];
        List<PRS> results = new List<PRS>(objCount); // objCount ��ŭ �뷮 �̸� �Ҵ�

        switch (objCount)
        {
            // ������ : 1,2,3�� �϶� (ȸ���� ���� ����)
            case 1: objLerps = new float[] { 0.5f }; break;
            case 2: objLerps = new float[] { 0.27f, 0.73f }; break;
            case 3: objLerps = new float[] { 0.1f, 0.5f, 0.9f }; break;

            // ī�尡 4�� �̻��϶� ���� ȸ������ ��
            default:
                float interval = 1f / (objCount - 1);
                for (int i = 0; i < objCount; i++)
                    objLerps[i] = interval * i;
                break;
        }

        // ���� ������
        for (int i = 0; i < objCount; i++)
        {
            var targetPos = Vector3.Lerp(leftTr.position, rightTr.position, objLerps[i]);
            var targetRot = Utils.QI;

            // ī�尡 4�� �̻��̶�� ȸ���� ��
            if (objCount >= 4)
            {
                float curve = Mathf.Sqrt(Mathf.Pow(height, 2) - Mathf.Pow(objLerps[i] - 0.5f, 2));
                curve = height >= 0 ? curve : -curve;
                targetPos.y += curve;
                targetRot = Quaternion.Slerp(leftTr.rotation, rightTr.rotation, objLerps[i]);
            }
            results.Add(new PRS(targetPos, targetRot, scale));
        }
        return results;
    }

    // ���߿� ����
    public void AddCardSpawn()
    {
        for (int i = myCards.Count; i < 8; i++) // 8������� ī�带 ����
        {
            AddCard(); // ī�� ���� �Լ� ȣ��
        }
    }

    public void Allignment()
    {
        myCards = myCards.OrderBy(card => card.itemdata.id).ToList();
        SetOriginOrder();
        CardAlignment();
    }

    
    

    public void SetupNextStage()
    {
        ScoreManager.Instance.TotalScore = 0;

        // ����Ʈ �ʱ�ȭ
        PokerManager.Instance.CardIDdata.Clear();
        PokerManager.Instance.saveNum.Clear();

        // ī�� ä���
        SetupItemBuffer();

        // ī�� �Ѹ���
        AddCardSpawn();

        // �ݶ��̴� Ȱ��ȭ
        card.StartCollider();
        ButtonManager.Instance.ButtonInactive();

        // �ڵ� & ������ ī��Ʈ �ʱ�ȭ 
        HandDelete.Instance.Counting();

        // UI ��� ������Ʈ
        HoldManager.Instance.UIupdate();
        HoldManager.Instance.TotalScoreupdate();

        HoldManager.Instance.CheckReset();

        HoldManager.Instance.RefillActionQueue();

    }

    // ��ġ�� ī�� & ������� ī�� �ݶ��̴� ��Ȱ��ȭ
    public void ColliderQuit()
    {   
        card.QuitCollider();
        PokerManager.Instance.QuitCollider2();
    }

    public IEnumerator DeleteCards()
    {
        yield return CoroutineCache.WaitForSeconds(3.0f);
        deleteCards();
    }

    public void deleteCards()
    {
        for(int i = 0; i< myCards.Count; i++)
        {
            myCards[i].transform.
                DOMove(deleteSpawn.transform.position, 1).SetDelay(i * 0.2f);

            myCards[i].transform.
                DORotate(new Vector3(-45, -60, -25), 0.5f).SetDelay(i * 0.2f);
        }
    }

}
