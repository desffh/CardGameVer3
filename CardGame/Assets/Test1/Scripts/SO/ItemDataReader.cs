using UnityEngine;
using GoogleSheetsToUnity;
using System.Collections.Generic;
using System;
using UnityEngine.Events;

#if UNITY_EDITOR
using UnityEditor;
#endif

[Serializable]
public struct ItemData
{
    public string name;
    public int id;
    public string suit;
    public string front;
    public ItemData(string name, int id, string suit, string front)
    {
        this.name = name;
        this.id = id;
        this.suit = suit;
        this.front = front;
    }
}

[CreateAssetMenu(fileName = "Reader", menuName = "Scriptable Object/ItemDataReader", order = int.MaxValue)]


// ����ü Ÿ������ �����͸� �޾ƿ��� ItemData����ü Ÿ���� ����Ʈ�� �����Ѵ�
public class ItemDataReader : DataReaderBase
{
    [Header("���������Ʈ���� ������ ����ȭ �� ������Ʈ")]
    [SerializeField] public List<ItemData> DataList = new List<ItemData>();

    internal void UpdateStats(List<GSTU_Cell> list, int itemID)
    {
        int id = 0;
        string name = string.Empty;
        string suit = string.Empty;
        string front = string.Empty;


        for (int i = 0; i < list.Count; i++)
        {
            switch (list[i].columnId)
            {
                case "name":
                    {
                        name = list[i].value;
                        break;
                    }
                case "number":
                    {
                       
                        if (!int.TryParse(list[i].value, out id))
                        {
                            Debug.LogError($"�߸��� id ��: {list[i].value}");
                        }

                        break;
                    }
                case "suit":
                    {
                        suit = list[i].value;
                        break;
                    }
                case "front":
                    {
                        front = list[i].value;
                        break;
                    }
            }
        }

        DataList.Add(new ItemData(name, id, suit, front));
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(ItemDataReader))]
public class ItemDataReaderEditor : Editor
{
    ItemDataReader data;

    void OnEnable()
    {
        data = (ItemDataReader)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GUILayout.Label("\n\n�������� ��Ʈ �о����");

        if (GUILayout.Button("������ �б�(API ȣ��)"))
        {
            data.DataList.Clear();
            UpdateStats(UpdateMethodOne);
        }
    }

    void UpdateStats(UnityAction<GstuSpreadSheet> callback, bool mergedCells = false)
    {
        Debug.Log("�������� ��Ʈ ������ ��û ����");

        SpreadsheetManager.Read(new GSTU_Search(data.associatedSheet, data.associatedWorksheet), callback, mergedCells);
    }

    void UpdateMethodOne(GstuSpreadSheet ss)
    {
        for (int i = data.START_ROW_LENGTH; i <= data.END_ROW_LENGTH; ++i)
        {
            data.UpdateStats(ss.rows[i], i);
        }

        EditorUtility.SetDirty(target);
    }
}
#endif
