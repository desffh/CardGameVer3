using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandDelete : Singleton<HandDelete>
{
    [SerializeField] TextManager textManager;

    // �ڵ� Ƚ�� & ������ Ƚ��

    [SerializeField] int hand;
    [SerializeField] int delete;

    public int Hand { get { return hand; } }
    public int Delete { get { return delete; } }
    


    protected override void Awake()
    {
        base.Awake();

        // Ƚ�� �ʱ�ȭ
        Counting();
    }
    public void DeCountHand()
    {
        --hand;
        textManager.HandCount(hand);
    }
    public void DeCountDelete()
    {
        --delete;
        textManager.DeleteCount(delete);
    }

    public void Counting()
    {
        hand = 4;
        delete = 4;

        textManager.HandCount(hand);
        textManager.DeleteCount(delete);
    }
}
