using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverPopUp : MonoBehaviour
{
    [SerializeField] GameObject GameOverPanel;

    private void Awake()
    {
        GameOverPanel = this.gameObject; // ���� ��ũ��Ʈ�� ���� ��ü ���� (������ �Ҵ絵 ����)
    }

    private void Start()
    {
        GameOverPanel.SetActive(false);
    }

    // �ڵ尡 0�̰� ����Ǿ�����,
    public void GameOver()
    {

    }
}
