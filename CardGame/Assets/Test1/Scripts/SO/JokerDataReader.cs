using UnityEngine;
using GoogleSheetsToUnity;
using System.Collections.Generic;
using System;
using UnityEngine.Events;

#if UNITY_EDITOR
using UnityEditor;
#endif

[Serializable]
public struct JokerData
{
    public string name;
    public int cost;
    public int multiple;
    public string require;
    public JokerData(string name, int cost, int multiple, string require)
    {
        this.name = name;
        this.cost = cost;
        this.multiple = multiple;
        this.require = require;
    }
}

[CreateAssetMenu(fileName = "Reader", menuName = "Scriptable Object/JokerDataReader", order = int.MaxValue)]


// ����ü Ÿ������ �����͸� �޾ƿ��� JokerData����ü Ÿ���� ����Ʈ�� �����Ѵ�
public class JokerDataReader : JokerReaderBase
{
    [Header("���������Ʈ���� ������ ����ȭ �� ������Ʈ")]
    [SerializeField] public List<JokerData> DataList = new List<JokerData>();

    internal void UpdateStats(List<GSTU_Cell> list, int itemID)
    {
        string name = string.Empty;
        int cost = 0;
        int muliple = 0;
        string require = string.Empty;


        for (int i = 0; i < list.Count; i++)
        {
            switch (list[i].columnId)
            {
                case "JokerName":
                    {
                        name = list[i].value;
                        break;
                    }
                case "Cost":
                    {

                        if (!int.TryParse(list[i].value, out cost))
                        {
                            Debug.LogError($"�߸��� id ��: {list[i].value}");
                        }

                        break;
                    }
                case "Multiply":
                    {

                        if (!int.TryParse(list[i].value, out muliple))
                        {
                            Debug.LogError($"�߸��� id ��: {list[i].value}");
                        }

                        break;
                    }
                case "Require":
                    {
                        require = list[i].value;
                        break;
                    }
            }
        }

        DataList.Add(new JokerData(name, cost, muliple, require));
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(JokerDataReader))]
public class JokerItemDataReaderEditor : Editor
{
    JokerDataReader data;

    void OnEnable()
    {
        data = (JokerDataReader)target;
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
