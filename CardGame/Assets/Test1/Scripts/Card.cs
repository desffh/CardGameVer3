using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UIElements;
using UnityEngine.U2D;
using System.Linq;
using Newtonsoft.Json.Bson;
using UnityEngine.Events;
using System;

// Card�� �� ��ũ��Ʈ
public class Card : MonoBehaviour
{
    private Transform cardPrefabs;
     
    [SerializeField] SpriteRenderer card; // �ո�
    [SerializeField] SpriteRenderer cardBack; // �޸��� ����

    [SerializeField] Sprite cardback; // �޸� �̹���
    [SerializeField] Sprite cardFront;

    [SerializeField] SpriteRenderer spriteCards;
    [SerializeField] SpriteRenderer spriteCards2;

    [SerializeField] BoxCollider2D Collider2D;
    
    public string spriteSheetName;
    public string spriteNameToLoad;

    public PRS originPRS; // ī�� ������ġ�� ���� PRS Ŭ����

    // ��� �ؽ��ĸ� �� �־�� �迭
    Sprite[] sprites;

    // SetUp �Լ��� ���� ī���� ����ü ������ �޾ƿͼ� ����
    public ItemData itemdata;
    
    // ī�尡 ���ȴ��� Ȯ��
    [SerializeField] private bool checkCard = false;

    private void Awake()
    {
        Collider2D = GetComponent<BoxCollider2D>();

        cardPrefabs = GetComponent<Transform>();
    }

    private void Start()
    {
        Collider2D.enabled = true;
    }

    public void Setup(ItemData item)
    {
        spriteCards = transform.GetChild(0).GetComponent<SpriteRenderer>();

        this.itemdata = item;
        // ī�� ��������Ʈ �̸� �޾ƿ�
        string spriteName = item.front;

        // ��� ��������Ʈ �迭�� �� �ֱ�
        sprites = Resources.LoadAll<Sprite>(spriteSheetName);

        if (sprites.Length > 0)
        {
            Sprite selctedSprite = sprites.FirstOrDefault(s => s.name == spriteName);

            if (selctedSprite != null)
            {
                spriteCards.sprite = selctedSprite;  // ������ ��������Ʈ�� ī�忡 ����
            }
        }

        if(cardback != null)
        {
            spriteCards2 = transform.GetChild(1).GetComponent<SpriteRenderer>();
            cardBack.sprite = cardback;
        }
    }

    // ī�� ���� �ִϸ��̼�
    public void MoveTransform(PRS prs, bool useDotween, float dotweenTime = 0)
    {
        if (useDotween)
        {
            transform.DOMove(prs.pos, dotweenTime).SetDelay(0.2f);
            transform.DORotateQuaternion(prs.rot, dotweenTime).SetDelay(0.2f);
            transform.DOScale(prs.scale, dotweenTime).SetDelay(0.2f);
        }
        else
        {
            transform.position = prs.pos;
            transform.rotation = prs.rot;
            transform.localScale = prs.scale;
        }
    }
    
    // ���콺�� Ŭ���ϸ� CardIDdata ����Ʈ�� ī�� �ֱ� (�ִ�5��)
    // �ݶ��̴��� ������ Card������Ʈ�� Ŭ�� �� �� �ִ�
    public void OnMouseDown()
    {
        // ����Ʈ�� �� á�ٸ�
        if (checkCard && PokerManager.Instance.CardIDdata.Count <= 5)
        {
            // �� ��ũ��Ʈ�� �޸� Card�� �Ű������� ����
            PokerManager.Instance.RemoveSuitIDdata(this);
            
            // ���� ��ġ�� �̵�
            cardPrefabs.DOMove(new Vector3(cardPrefabs.transform.position.x,
               cardPrefabs.transform.position.y -0.5f,
               cardPrefabs.transform.position.z), 0.2f);

            checkCard = false;
        }
        // ����Ʈ�� �� á�ٸ�
        else if(PokerManager.Instance.CardIDdata.Count < 5)
        {
            PokerManager.Instance.SaveSuitIDdata(this);
            
            // Ŭ�� �ִϸ��̼�
            cardPrefabs.DOMove(new Vector3(cardPrefabs.transform.position.x,
               cardPrefabs.transform.position.y + 0.5f,
               cardPrefabs.transform.position.z), 0.2f);
            
            checkCard = true;
        }
        else
        {
            Debug.Log("���̻� ī�带 ���� �� ����");

            // ī�带 Ŭ���ϸ� �ִϸ��̼�
            cardPrefabs.DOScale(new Vector3(0.65f, 0.65f, 0.65f), 0.1f).
            OnComplete(() => { cardPrefabs.transform.DOScale(new Vector3(0.7f, 0.7f, 0.7f), 0.2f); });
        }
             
        // ���� Ȯ��
        if (PokerManager.Instance.CardIDdata.Count >= 0)
        {
            PokerManager.Instance.Hand();
            PokerManager.Instance.getHandType();
        }
    }

    // ī�� �ݶ��̴� ��Ȱ��ȭ
    public void QuitCollider()
    {
        //Debug.Log("ī��� �ݶ��̴� ��Ȱ��ȭ");
        
        // �������� ī��
        for (int i = 0; i < KardManager.Instance.myCards.Count; i++)
        {
            KardManager.Instance.myCards[i].Collider2D.enabled = false;
        }

        // ������� ī��
        for (int i = 0; i < PokerManager.Instance.CardIDdata.Count; i++)
        {
            PokerManager.Instance.CardIDdata[i].gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    // �ݶ��̴� Ȱ��ȭ
    public void StartCollider()
    {
        //Debug.Log("ī��� �ݶ��̴� Ȱ��ȭ");

        for(int i = 0; i < KardManager.Instance.myCards.Count; i++)
        {
            KardManager.Instance.myCards[i].Collider2D.enabled = true;  
        }
    }
}