using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandDelete : Singleton<HandDelete>
{
    [SerializeField] TextManager textManager;

    // ÇÚµå È½¼ö & ¹ö¸®±â È½¼ö

    [SerializeField] int hand;
    [SerializeField] int delete;

    public int Hand { get { return hand; } }
    public int Delete { get { return delete; } }
    


    protected override void Awake()
    {
        base.Awake();

        // È½¼ö ÃÊ±âÈ­
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
