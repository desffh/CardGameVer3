using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ButtonManager : Singleton<ButtonManager>
{
    [SerializeField] Button Handbutton; // �ڵ� �÷���
    [SerializeField] Button Treshbutton;//     ������

    [SerializeField] HandCardPoints HandCardPoints;

    [SerializeField] GameObject PopUpCanvas;
    [SerializeField] GameObject EndPanel;

    

    // ��ư Ȱ��ȭ ���� ����
    private bool isButtonActive = true;

    private void Start()
    {
        Handbutton.interactable = false;
        Treshbutton.interactable = false;

        PopUpCanvas.SetActive(false);
    }

    private void Update()
    {
       if(isButtonActive == true && HandDelete.Instance.Hand > 0 && GameManager.Instance.Hold == true) 
       {
            Handbutton.interactable = true;
       }
       else
       {
            Handbutton.interactable = false;
       }
       if(isButtonActive == true && HandDelete.Instance.Delete > 0 && GameManager.Instance.Hold == true)
        {
            Treshbutton.interactable = true;
        }
       else
        {
            Treshbutton.interactable = false;

        }
    }

    // �ڵ��ư�� Ŭ������ ��
    public void OnHandButtonClick()
    {
        if(GameManager.Instance.Hold == true)
        {
            for (int i  = 0; i < PokerManager.Instance.CardIDdata.Count; i++)
            {
                // ����� ī���� ��ũ��Ʈ ��������
                Card selectedCard =  PokerManager.Instance.CardIDdata[i].gameObject.GetComponent<Card>();

                // ����� �������� ��ġ���� �����ϱ� ���� ������Ʈ ��������
                PokerManager.Instance.CardIDdata[i].gameObject.GetComponent<Transform>();
            
                // ��ġ�� HandCardPoints�� �̵�
                PokerManager.Instance.CardIDdata[i].gameObject.transform.
                    DOMove(HandCardPoints.HandCardpos[i].transform.position, 0.5f);
               // ȸ�� 0
                PokerManager.Instance.CardIDdata[i].gameObject.transform.rotation = Quaternion.identity;

                // myCards ����Ʈ���� �ش� ī�� ���� (���ۿ��� �����ͼ� �����ϴ� ��)
                if (selectedCard != null && KardManager.Instance.myCards.Contains(selectedCard))
                {
                    KardManager.Instance.myCards.Remove(selectedCard);
                }
            }
            // ���� ī��� ������ �Ǳ�
            KardManager.Instance.SetOriginOrder();
            KardManager.Instance.CardAlignment();

            // ���ϱ� ���
            HoldManager.Instance.Calculation();
        }
    }

    // ������ ��ư�� Ŭ������ ��
    public void OnDeleteButtonClick()
    {

        if (GameManager.Instance.Hold == true)
        {
            isButtonActive = false;

            for (int i = 0; i < PokerManager.Instance.CardIDdata.Count; i++)
            {
                // ����� ī���� ��ũ��Ʈ ��������
                Card selectedCard = PokerManager.Instance.CardIDdata[i].gameObject.GetComponent<Card>();

                // ����� �������� ��ġ���� �����ϱ� ���� ������Ʈ ��������
                PokerManager.Instance.CardIDdata[i].gameObject.GetComponent<Transform>();
                // ��ġ�� HandCardPoints�� �̵�
                PokerManager.Instance.CardIDdata[i].gameObject.transform.
                    DOMove(HandCardPoints.DeleteCardpos.transform.position, 0.5f);

                PokerManager.Instance.CardIDdata[i].gameObject.transform.
                    DORotate(new Vector3(58, 122, 71), 3);

                // myCards ����Ʈ���� �ش� ī�� ���� (���ۿ��� �����ͼ� �����ϴ� ��)
                if (selectedCard != null && KardManager.Instance.myCards.Contains(selectedCard))
                {
                    KardManager.Instance.myCards.Remove(selectedCard);
                }
            }

            HoldManager.Instance.StartDeleteCard();

            // ���� ī��� ������ �Ǳ�
            KardManager.Instance.SetOriginOrder();
            KardManager.Instance.CardAlignment();
        }
    }

    // �̺�Ʈ�� �� �Լ� -> ����� ���۵Ǹ� ��ư ��ȣ�ۿ� ��Ȱ��ȭ
    public void ButtonActive()
    {
        Handbutton.interactable = false;
        Treshbutton.interactable = false;
        
        isButtonActive = false;
    }

    public void ButtonInactive()
    {
        isButtonActive = true;
    }


    public void RunOnClick()
    {
        PopUpCanvas.SetActive(true);
    }

    public void RunDeleteClick()
    {
        PopUpCanvas.SetActive(false);
    }

    

}
