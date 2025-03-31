using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Order : MonoBehaviour
{
    [SerializeField] Renderer CardRenderer;
    int originOrder;

    // ī�� ���� (order) -> ī�尡 ������ �� ȣ��� �Լ�
    public void SetOriginOrder(int originOrder)
    {
        this.originOrder = originOrder;
        SetOrder(originOrder);
    }

    public void SetMostFrontOrder(bool isMostFront)
    {
        SetOrder(isMostFront ? 100 : originOrder);
    }

    // ī�尡 ������ ������ * 10
    public void SetOrder(int order)
    {
        int mulOrder = order * 10;

        CardRenderer.sortingOrder = mulOrder;
    }
}
