using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MultiplyText : NumTextUpdater
{
    [SerializeField] TextMeshProUGUI multiplyText;

    private void Awake()
    {
        multiplyText = GetComponent<TextMeshProUGUI>();
    }

    public override void UpdateText(int text)
    {
        multiplyText.text = text.ToString();
    }
}
